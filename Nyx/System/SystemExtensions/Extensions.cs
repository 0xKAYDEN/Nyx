// Decompiled with JetBrains decompiler
// Type: System.Extensions
// Assembly: SystemExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0A40C0D2-CA2F-4935-A384-ADAE2A9DCBE0
// Assembly location: D:\Nyx\Server\net9.0-windows\SystemExtensions.dll

using System.Collections.Generic;

#nullable disable
namespace System;

public static class Extensions
{
  public static T[] Transform<T>(this object[] strs)
  {
    T[] objArray = new T[strs.Length];
    for (int index = 0; index < strs.Length; ++index)
      objArray[index] = (T) Convert.ChangeType(strs[index], typeof (T));
    return objArray;
  }

  public static void Add<T, T2>(this IDictionary<T, T2> dict, T key, T2 value) => dict[key] = value;

  public static void Remove<T, T2>(this IDictionary<T, T2> dict, T key) => dict.Remove(key);
}
