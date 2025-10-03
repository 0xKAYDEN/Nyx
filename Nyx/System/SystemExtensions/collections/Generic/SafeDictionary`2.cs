
namespace System.Collections.Generic;

public class SafeDictionary<T, T2> : Dictionary<T, T2>
{
  public SafeDictionary() => Class1.Class0.smethod_0();

  public SafeDictionary(int nulledNumber) => Class1.Class0.smethod_0();

  public new T2 this[T key]
  {
    set => base[key] = value;
    get => this.ContainsKey(key) ? base[key] : default (T2);
  }

  public new void Add(T key, T2 value) => base[key] = value;
}
