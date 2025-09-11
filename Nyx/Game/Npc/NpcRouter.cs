//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using Nyx.Server.Client;
//using Nyx.Server.Network.GamePackets;

//namespace Nyx.Server.Game.Npc
//{
//	public static class NpcRouter
//	{
//		private static readonly Dictionary<uint, Action<GameClient, NpcRequest, NpcDialog>> Handlers
//			= new Dictionary<uint, Action<GameClient, NpcRequest, NpcDialog>>();

//		public static void Initialize(params Assembly[] assemblies)
//		{
//			foreach (var asm in assemblies.Distinct())
//			{
//				foreach (var type in asm.GetTypes())
//				{
//					foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
//					{
//						var attr = method.GetCustomAttribute<NpcAttribute>();
//						if (attr == null) continue;

//						var parameters = method.GetParameters();
//						if (parameters.Length == 3 &&
//							parameters[0].ParameterType == typeof(GameClient) &&
//							parameters[1].ParameterType == typeof(NpcRequest) &&
//							parameters[2].ParameterType == typeof(NpcDialog))
//						{
//							var del = (Action<GameClient, NpcRequest, NpcDialog>)Delegate.CreateDelegate(
//								typeof(Action<GameClient, NpcRequest, NpcDialog>), method);
//							Handlers[attr.NpcId] = del;
//						}
//					}
//				}
//			}
//		}

//		public static bool TryHandle(GameClient client, NpcRequest req)
//		{
//			if (req == null || client == null)
//				return false;
//			if (Handlers.TryGetValue(req.NpcID, out var handler))
//			{
//				var dialog = new NpcDialog(client, req.NpcID);
//				handler(client, req, dialog);
//				return true;
//			}
//			return false;
//		}
//	}
//}

