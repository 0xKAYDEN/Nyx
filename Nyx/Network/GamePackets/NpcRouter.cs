using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Nyx.Server.Client;
using Nyx.Server.Network;
using Serilog;

namespace Nyx.Server.Network.GamePackets
{
	public static class NpcRouter
	{
		private static readonly Dictionary<uint, Action<GameClient, NpcRequest, NpcDialog>> Handlers
			= new Dictionary<uint, Action<GameClient, NpcRequest, NpcDialog>>();

		public static void Initialize(params Assembly[] assemblies)
		{
			foreach (var asm in assemblies.Distinct())
			{
				foreach (var type in asm.GetTypes())
				{
					foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
					{
						var attr = method.GetCustomAttribute<Game.Npc.NpcAttribute>();
						if (attr == null) continue;

						var parameters = method.GetParameters();
						if (parameters.Length == 3 &&
							parameters[0].ParameterType == typeof(GameClient) &&
							parameters[1].ParameterType == typeof(NpcRequest) &&
							parameters[2].ParameterType == typeof(NpcDialog))
						{
							var del = (Action<GameClient, NpcRequest, NpcDialog>)Delegate.CreateDelegate(
								typeof(Action<GameClient, NpcRequest, NpcDialog>), method);
							Handlers[attr.NpcId] = del;
							Log.Information($"[NpcRouter] Registered handler for NPC {attr.NpcId} -> {type.Name}.{method.Name}");
						}
					}
				}
			}
			Log.Information($"[NpcRouter] Total handlers registered: {Handlers.Count}");
		}

		public static bool TryHandle(GameClient client, NpcRequest req)
		{
			if (req == null || client == null)
				return false;
			
			// Use ActiveNpc if req.NpcID is 0 (option selection)
			uint npcId = req.NpcID == 0 ? client.ActiveNpc : req.NpcID;
			
			Log.Information($"[NpcRouter] TryHandle: NPC {req.NpcID} -> {npcId}, Option {req.OptionID}");
			
			if (Handlers.TryGetValue(npcId, out var handler))
			{
				Log.Information($"[NpcRouter] Found handler for NPC {npcId}, invoking...");
				var dialog = new NpcDialog(client, npcId);
				handler(client, req, dialog);
				return true;
			}
			else
			{
				Log.Information($"[NpcRouter] No handler found for NPC {npcId}");
			}
			return false;
		}
	}
}

