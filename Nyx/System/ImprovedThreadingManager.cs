using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Timer = System.Threading.Timer;

namespace Nyx.Server
{
    public class ThreadingMetrics
    {
        public string ThreadPoolName { get; set; }
        public int ActiveThreads { get; set; }
        public int TotalThreads { get; set; }
        public int QueuedTasks { get; set; }
        public long CompletedTasks { get; set; }
        public double AverageTaskTime { get; set; }
        public DateTime LastUpdate { get; set; }
    }

    public class ImprovedThreadingManager
    {
        private static ImprovedThreadingManager _instance;
        private static readonly object _lock = new object();
        
        // Thread pools with optimized sizes
        private readonly StaticPool _gameLogicPool;
        private readonly StaticPool _networkPool;
        private readonly StaticPool _databasePool;
        private readonly StaticPool _backgroundPool;
        private readonly StaticPool _highPriorityPool;
        
        // Performance monitoring
        private readonly ConcurrentDictionary<string, ThreadingMetrics> _metrics;
        private readonly Timer _metricsTimer;
        private readonly Timer _healthCheckTimer;
        
        // Configuration
        private const int GameLogicThreads = 16; // Game logic processing
        private const int NetworkThreads = 32;   // Network operations
        private const int DatabaseThreads = 8;   // Database operations
        private const int BackgroundThreads = 12; // Background tasks
        private const int HighPriorityThreads = 4; // Critical operations
        
        private const int MetricsIntervalMs = 30000; // 30 seconds
        private const int HealthCheckIntervalMs = 60000; // 1 minute

        public static ImprovedThreadingManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        _instance ??= new ImprovedThreadingManager();
                    }
                }
                return _instance;
            }
        }

        private ImprovedThreadingManager()
        {
            // Initialize optimized thread pools
            _gameLogicPool = new StaticPool(GameLogicThreads).Run();
            _networkPool = new StaticPool(NetworkThreads).Run();
            _databasePool = new StaticPool(DatabaseThreads).Run();
            _backgroundPool = new StaticPool(BackgroundThreads).Run();
            _highPriorityPool = new StaticPool(HighPriorityThreads).Run();
            
            _metrics = new ConcurrentDictionary<string, ThreadingMetrics>();
            
            // Initialize metrics
            InitializeMetrics();
            
            // Start monitoring
            _metricsTimer = new Timer(UpdateMetrics, null, MetricsIntervalMs, MetricsIntervalMs);
            _healthCheckTimer = new Timer(HealthCheck, null, HealthCheckIntervalMs, HealthCheckIntervalMs);
            
            LoggingService.SystemInfo("Threading", "Improved threading manager initialized");
        }

        private void InitializeMetrics()
        {
            _metrics.TryAdd("GameLogic", new ThreadingMetrics { ThreadPoolName = "GameLogic" });
            _metrics.TryAdd("Network", new ThreadingMetrics { ThreadPoolName = "Network" });
            _metrics.TryAdd("Database", new ThreadingMetrics { ThreadPoolName = "Database" });
            _metrics.TryAdd("Background", new ThreadingMetrics { ThreadPoolName = "Background" });
            _metrics.TryAdd("HighPriority", new ThreadingMetrics { ThreadPoolName = "HighPriority" });
        }

        #region Thread Pool Access

        public StaticPool GameLogicPool => _gameLogicPool;
        public StaticPool NetworkPool => _networkPool;
        public StaticPool DatabasePool => _databasePool;
        public StaticPool BackgroundPool => _backgroundPool;
        public StaticPool HighPriorityPool => _highPriorityPool;

        #endregion

        #region Task Execution Methods

        public void ExecuteGameLogic(Action<int> action, int timeout = 0)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                _gameLogicPool.Subscribe(new LazyDelegate(action, timeout, ThreadPriority.Normal));
            }
            finally
            {
                stopwatch.Stop();
                UpdateTaskMetrics("GameLogic", stopwatch.ElapsedMilliseconds);
            }
        }

        public void ExecuteNetwork(Action<int> action, int timeout = 0)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                _networkPool.Subscribe(new LazyDelegate(action, timeout, ThreadPriority.AboveNormal));
            }
            finally
            {
                stopwatch.Stop();
                UpdateTaskMetrics("Network", stopwatch.ElapsedMilliseconds);
            }
        }

        public void ExecuteDatabase(Action<int> action, int timeout = 0)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                _databasePool.Subscribe(new LazyDelegate(action, timeout, ThreadPriority.BelowNormal));
            }
            finally
            {
                stopwatch.Stop();
                UpdateTaskMetrics("Database", stopwatch.ElapsedMilliseconds);
            }
        }

        public void ExecuteBackground(Action<int> action, int timeout = 0)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                _backgroundPool.Subscribe(new LazyDelegate(action, timeout, ThreadPriority.Lowest));
            }
            finally
            {
                stopwatch.Stop();
                UpdateTaskMetrics("Background", stopwatch.ElapsedMilliseconds);
            }
        }

        public void ExecuteHighPriority(Action<int> action, int timeout = 0)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                _highPriorityPool.Subscribe(new LazyDelegate(action, timeout, ThreadPriority.Highest));
            }
            finally
            {
                stopwatch.Stop();
                UpdateTaskMetrics("HighPriority", stopwatch.ElapsedMilliseconds);
            }
        }

        #endregion

        #region Timer Management

        public IDisposable SubscribeGameLogic(Action<int> action, int period = 1000)
        {
            return _gameLogicPool.Subscribe(new TimerRule(action, period, ThreadPriority.Normal));
        }

        public IDisposable SubscribeNetwork(Action<int> action, int period = 1)
        {
            return _networkPool.Subscribe(new TimerRule(action, period, ThreadPriority.AboveNormal));
        }

        public IDisposable SubscribeDatabase(Action<int> action, int period = 5000)
        {
            return _databasePool.Subscribe(new TimerRule(action, period, ThreadPriority.BelowNormal));
        }

        public IDisposable SubscribeBackground(Action<int> action, int period = 10000)
        {
            return _backgroundPool.Subscribe(new TimerRule(action, period, ThreadPriority.Lowest));
        }

        public IDisposable SubscribeHighPriority(Action<int> action, int period = 100)
        {
            return _highPriorityPool.Subscribe(new TimerRule(action, period, ThreadPriority.Highest));
        }

        #endregion

        #region Performance Monitoring

        private void UpdateTaskMetrics(string poolName, long executionTime)
        {
            if (_metrics.TryGetValue(poolName, out var metrics))
            {
                lock (metrics)
                {
                    metrics.CompletedTasks++;
                    metrics.AverageTaskTime = (metrics.AverageTaskTime * (metrics.CompletedTasks - 1) + executionTime) / metrics.CompletedTasks;
                    metrics.LastUpdate = DateTime.Now;
                }
            }
        }

        private void UpdateMetrics(object state)
        {
            try
            {
                UpdatePoolMetrics("GameLogic", _gameLogicPool);
                UpdatePoolMetrics("Network", _networkPool);
                UpdatePoolMetrics("Database", _databasePool);
                UpdatePoolMetrics("Background", _backgroundPool);
                UpdatePoolMetrics("HighPriority", _highPriorityPool);

                LogThreadingStatistics();
            }
            catch (Exception ex)
            {
                LoggingService.SystemError("Threading", "Error updating threading metrics", ex);
            }
        }

        private void UpdatePoolMetrics(string poolName, StaticPool pool)
        {
            if (_metrics.TryGetValue(poolName, out var metrics))
            {
                lock (metrics)
                {
                    // Update basic metrics (these would need to be exposed by StaticPool)
                    metrics.LastUpdate = DateTime.Now;
                }
            }
        }

        private void LogThreadingStatistics()
        {
            var totalCompleted = _metrics.Values.Sum(m => m.CompletedTasks);
            var avgTaskTime = _metrics.Values.Average(m => m.AverageTaskTime);

            LoggingService.SystemDebug("Threading", 
                $"Threading Stats - Total Tasks: {totalCompleted}, Avg Time: {avgTaskTime:F2}ms");

            // Log warnings for slow pools
            foreach (var kvp in _metrics)
            {
                if (kvp.Value.AverageTaskTime > 100) // Tasks taking >100ms on average
                {
                    LoggingService.SystemWarning("Threading", 
                        $"Slow thread pool {kvp.Key}: {kvp.Value.AverageTaskTime:F2}ms avg task time");
                }
            }
        }

        private void HealthCheck(object state)
        {
            try
            {
                // Check for thread pool health
                var totalThreads = GameLogicThreads + NetworkThreads + DatabaseThreads + BackgroundThreads + HighPriorityThreads;
                var activeThreads = ThreadPool.ThreadCount;
                
                if (activeThreads > totalThreads * 1.5) // 50% more than expected
                {
                    LoggingService.SystemWarning("Threading", 
                        $"High thread count detected: {activeThreads} active threads");
                }

                // Log overall threading health
                LoggingService.SystemInfo("Threading", 
                    $"Threading Health - Active: {activeThreads}, Pools: {_metrics.Count}");
            }
            catch (Exception ex)
            {
                LoggingService.SystemError("Threading", "Error in threading health check", ex);
            }
        }

        public List<ThreadingMetrics> GetThreadingMetrics()
        {
            return _metrics.Values.ToList();
        }

        public ThreadingMetrics GetPoolMetrics(string poolName)
        {
            return _metrics.TryGetValue(poolName, out var metrics) ? metrics : null;
        }

        #endregion

        #region Workload Distribution

        public void DistributeWorkload<T>(IEnumerable<T> items, Action<T> processor, ThreadPoolType poolType = ThreadPoolType.GameLogic)
        {
            var pool = GetPoolByType(poolType);
            var batchSize = GetOptimalBatchSize(poolType);
            
            var batches = items.Chunk(batchSize);
            
            foreach (var batch in batches)
            {
                pool.Subscribe(new LazyDelegate(time =>
                {
                    foreach (var item in batch)
                    {
                        try
                        {
                            processor(item);
                        }
                        catch (Exception ex)
                        {
                            LoggingService.SystemError("Threading", $"Error processing item in batch", ex);
                        }
                    }
                }, 0, GetThreadPriority(poolType)));
            }
        }

        private StaticPool GetPoolByType(ThreadPoolType poolType)
        {
            return poolType switch
            {
                ThreadPoolType.GameLogic => _gameLogicPool,
                ThreadPoolType.Network => _networkPool,
                ThreadPoolType.Database => _databasePool,
                ThreadPoolType.Background => _backgroundPool,
                ThreadPoolType.HighPriority => _highPriorityPool,
                _ => _gameLogicPool
            };
        }

        private int GetOptimalBatchSize(ThreadPoolType poolType)
        {
            return poolType switch
            {
                ThreadPoolType.GameLogic => 50,
                ThreadPoolType.Network => 100,
                ThreadPoolType.Database => 25,
                ThreadPoolType.Background => 200,
                ThreadPoolType.HighPriority => 10,
                _ => 50
            };
        }

        private ThreadPriority GetThreadPriority(ThreadPoolType poolType)
        {
            return poolType switch
            {
                ThreadPoolType.GameLogic => ThreadPriority.Normal,
                ThreadPoolType.Network => ThreadPriority.AboveNormal,
                ThreadPoolType.Database => ThreadPriority.BelowNormal,
                ThreadPoolType.Background => ThreadPriority.Lowest,
                ThreadPoolType.HighPriority => ThreadPriority.Highest,
                _ => ThreadPriority.Normal
            };
        }

        #endregion

        #region Shutdown

        public void Shutdown()
        {
            try
            {
                _metricsTimer?.Dispose();
                _healthCheckTimer?.Dispose();
                
                LoggingService.SystemInfo("Threading", "Improved threading manager shutdown complete");
            }
            catch (Exception ex)
            {
                LoggingService.SystemError("Threading", "Error during threading manager shutdown", ex);
            }
        }

        #endregion
    }

    public enum ThreadPoolType
    {
        GameLogic,
        Network,
        Database,
        Background,
        HighPriority
    }
} 