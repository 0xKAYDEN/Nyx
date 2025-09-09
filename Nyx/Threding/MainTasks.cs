using BigBOs_Lib.Tasks;
using Nyx.Server.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyx.Server.Threding
{
    public class MainTasks
    {
        public static TaskExtensions<GameClient> FloorSpell, Buffers, Attack, Character,
            Monster_BuffersCallback, Monster_GuardsCallback, Monster_AliveMonstersCallback;
        public MainTasks()
        {
            //FloorSpell = new TaskExtensions<GameClient>(PoolProcesses.FloorCallback, 100, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            //Buffers = new TaskExtensions<GameClient>(PoolProcesses.BuffersCallback, 500, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            //Attack = new BigBOs_Lib.Tasks.TaskExtensions<GameClient>(PoolProcesses.AutoAttackCallback, 500, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            //Character = new BigBOs_Lib.Tasks.TaskExtensions<GameClient>(PoolProcesses.CharactersCallback, 1000, TaskCreationOptions.LongRunning, TaskScheduler.Default);

            //Monster_BuffersCallback = new BigBOs_Lib.Tasks.TaskExtensions<GameClient>(Game.MsgMonster.PoolProcesses.BuffersCallback, 1000, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            //Monster_GuardsCallback = new BigBOs_Lib.Tasks.TaskExtensions<GameClient>(Game.MsgMonster.PoolProcesses.GuardsCallback, 1000, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            //Monster_AliveMonstersCallback = new BigBOs_Lib.Tasks.TaskExtensions<GameClient>(Game.MsgMonster.PoolProcesses.AliveMonstersCallback, 1000, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }
    }
}
