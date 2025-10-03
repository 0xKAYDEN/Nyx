// Decompiled with JetBrains decompiler
// Type: System.Threading.TimerRule
// Assembly: SystemExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0A40C0D2-CA2F-4935-A384-ADAE2A9DCBE0
// Assembly location: D:\Nyx\Server\net9.0-windows\SystemExtensions.dll

#nullable disable
namespace System.Threading;

public class TimerRule
{
  internal Action<int> action_0;
  internal int int_0;
  internal bool bool_0;
  internal ThreadPriority threadPriority_0;

  public TimerRule(Action<int> action, int period, ThreadPriority priority = ThreadPriority.Normal)
  {
    this.action_0 = action;
    this.int_0 = period;
    this.bool_0 = true;
    this.threadPriority_0 = priority;
  }

  ~TimerRule() => this.action_0 = (Action<int>) null;
}
