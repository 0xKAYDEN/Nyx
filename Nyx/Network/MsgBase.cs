using Nyx.Server.Client;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyx.Server.Network
{
    //public abstract class MsgBase : IPacket
    //{
    //    public ushort Length { get; protected set; }
    //    public PacketType Type { get; protected set; }

    //    public virtual void Decode(byte[] bytes)
    //    {
    //        var length = BitConverter.ToUInt16(bytes, 0);
    //        var type = (PacketType)BitConverter.ToUInt16(bytes, 2);
    //        throw new NotImplementedException($"Packet decode not implemented!\nLength:{length}\tType{type}");
    //    }

    //    public virtual byte[] Encode()
    //    {
    //        throw new NotImplementedException("Packet Encode not implemented!");
    //    }

    //    public virtual Task ProcessAsync(GameClient client)
    //    {
    //        Log.Information("Process packet not being handled!!!\nPacketType: {0} Length: {1}", Type, Length);
    //        return Task.CompletedTask;
    //    }
    //}
}
