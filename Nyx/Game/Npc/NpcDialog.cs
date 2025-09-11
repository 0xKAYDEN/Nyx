//using Nyx.Server.Client;
//using Nyx.Server.Network.GamePackets;

//namespace Nyx.Server.Game.Npc
//{
//	public sealed class NpcDialog
//	{
//		private readonly GameClient _client;
//		private readonly uint _npcId;

//		public NpcDialog(GameClient client, uint npcId)
//		{
//			_client = client;
//			_npcId = npcId;
//		}

//		public void Avatar(ushort avatarId)
//		{
//			var reply = new NpcReply();
//			reply.InteractType = NpcReply.Avatar;
//			reply.OptionID = 0;
//			reply.DontDisplay = true;
//			reply.Text = string.Empty;
//			reply.Send(_client);
//		}

//		public void Text(string message, bool display = true)
//		{
//			var reply = new NpcReply();
//			reply.InteractType = NpcReply.Dialog;
//			reply.OptionID = 0;
//			reply.DontDisplay = display ? true : false;
//			reply.Text = message ?? string.Empty;
//			reply.Send(_client);
//		}

//		public void Option(string text, byte optionId)
//		{
//			var reply = new NpcReply();
//			reply.InteractType = NpcReply.Option;
//			reply.OptionID = optionId;
//			reply.DontDisplay = true;
//			reply.Text = text ?? string.Empty;
//			reply.Send(_client);
//		}

//		public void Input(string prompt, ushort maxLength = 32)
//		{
//			var reply = new NpcReply();
//			reply.InteractType = NpcReply.Input;
//			reply.InputMaxLength = maxLength;
//			reply.DontDisplay = true;
//			reply.Text = prompt ?? string.Empty;
//			reply.Send(_client);
//		}

//		public void Finish()
//		{
//			var reply = new NpcReply();
//			reply.InteractType = NpcReply.Finish;
//			reply.DontDisplay = false;
//			reply.Text = string.Empty;
//			reply.Send(_client);
//		}
//	}
//}

