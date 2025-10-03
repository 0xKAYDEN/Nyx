// Decompiled with JetBrains decompiler
// Type: System.Collections.Concurrent.SafeConcurrentDictionary`2
// Assembly: SystemExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0A40C0D2-CA2F-4935-A384-ADAE2A9DCBE0
// Assembly location: D:\Nyx\Server\net9.0-windows\SystemExtensions.dll

namespace System.Collections.Concurrent;

public class SafeConcurrentDictionary<T, T2> : ConcurrentDictionary<T, T2>
{
  public SafeConcurrentDictionary() => Class1.Class0.smethod_0();

  public new T2 this[T key]
  {
    set => base[key] = value;
    get => this.ContainsKey(key) ? base[key] : default (T2);
  }
}
