

namespace System.Collections.Generic;

public class CareDictionary<T, T2> : Dictionary<T, T2>
{
  public CareDictionary() => Class1.Class0.smethod_0();

  public CareDictionary(int nulledNumber) => Class1.Class0.smethod_0();

  public new T2 this[T key]
  {
    set => base[key] = value;
    get => this.ContainsKey(key) ? base[key] : default (T2);
  }

  public new void Add(T key, T2 value) => base[key] = value;
}
