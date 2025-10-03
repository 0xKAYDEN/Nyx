// Decompiled with JetBrains decompiler
// Type: Class2
// Assembly: SystemExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0A40C0D2-CA2F-4935-A384-ADAE2A9DCBE0
// Assembly location: D:\Nyx\Server\net9.0-windows\SystemExtensions.dll

using System;
using System.Reflection;
using System.Threading;

#nullable disable
internal abstract class Class2 : IDisposable
{
  internal static volatile int int_0 = int.MinValue;
  internal bool bool_0;
  internal bool bool_1;
  internal Time32 time32_0;
  protected int int_1;

  public Class2()
  {
    ++Class2.int_0;
    this.int_1 = Class2.int_0;
    this.bool_0 = true;
    this.bool_1 = false;
    this.method_1(0);
  }

  internal abstract void vmethod_0();

  ~Class2() => ((IDisposable) this).Dispose();

  internal bool method_0() => Time32.Now > this.time32_0;

  internal void method_1(int int_2) => this.time32_0 = Time32.Now.AddMilliseconds(int_2);

  void IDisposable.Dispose()
  {
    this.bool_0 = false;
    this.vmethod_1();
  }

  internal abstract void vmethod_1();

  internal abstract MethodInfo vmethod_2();

  internal abstract ThreadPriority vmethod_3();

  public override int GetHashCode() => this.int_1;
}
