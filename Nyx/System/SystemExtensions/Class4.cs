// Decompiled with JetBrains decompiler
// Type: Class4
// Assembly: SystemExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0A40C0D2-CA2F-4935-A384-ADAE2A9DCBE0
// Assembly location: D:\Nyx\Server\net9.0-windows\SystemExtensions.dll

using System;
using System.Reflection;
using System.Threading;

#nullable disable
internal sealed class Class4 : Class2
{
  private TimerRule timerRule_0;

  public Class4(TimerRule timerRule_1) => this.timerRule_0 = timerRule_1;

  internal override void vmethod_0()
  {
    if (this.timerRule_0 == null)
      return;
    this.timerRule_0.action_0(Time32.Now.Value);
    if (this.timerRule_0 == null)
      return;
    if (!this.timerRule_0.bool_0)
      ((IDisposable) this).Dispose();
    else
      this.method_1(this.timerRule_0.int_0);
  }

  internal override void vmethod_1() => this.timerRule_0 = (TimerRule) null;

  internal override MethodInfo vmethod_2() => this.timerRule_0.action_0.Method;

  internal override ThreadPriority vmethod_3() => this.timerRule_0.threadPriority_0;
}
