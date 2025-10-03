// Decompiled with JetBrains decompiler
// Type: System.FastRandom
// Assembly: SystemExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0A40C0D2-CA2F-4935-A384-ADAE2A9DCBE0
// Assembly location: D:\Nyx\Server\net9.0-windows\SystemExtensions.dll

#nullable disable
namespace System;

public class FastRandom
{
  private uint uint_0;
  private uint uint_1;
  private uint uint_2;
  private uint uint_3;
  private object object_0;

  public FastRandom()
    : this(Time32.Now.TotalMilliseconds)
  {
    Class1.Class0.smethod_0();
  }

  public FastRandom(int seed)
  {
    this.object_0 = new object();
    this.Reinitialise(seed);
  }

  public void Reinitialise(int seed)
  {
    lock (this.object_0)
    {
      this.uint_0 = (uint) seed;
      this.uint_1 = 842502087U;
      this.uint_2 = 3579807591U;
      this.uint_3 = 273326509U;
    }
  }

  public int Next()
  {
    lock (this.object_0)
    {
      uint num1 = this.uint_0 ^ this.uint_0 << 11;
      this.uint_0 = this.uint_1;
      this.uint_1 = this.uint_2;
      this.uint_2 = this.uint_3;
      this.uint_3 = (uint) ((int) this.uint_3 ^ (int) (this.uint_3 >> 19) ^ (int) num1 ^ (int) (num1 >> 8));
      uint num2 = this.uint_3 & (uint) int.MaxValue;
      return num2 == (uint) int.MaxValue ? this.Next() : (int) num2;
    }
  }

  public int Next(int upperBound)
  {
    lock (this.object_0)
    {
      if (upperBound < 0)
        upperBound = 0;
      uint num = this.uint_0 ^ this.uint_0 << 11;
      this.uint_0 = this.uint_1;
      this.uint_1 = this.uint_2;
      this.uint_2 = this.uint_3;
      return (int) (4.6566128730773926E-10 * (double) (int.MaxValue & (int) (this.uint_3 = (uint) ((int) this.uint_3 ^ (int) (this.uint_3 >> 19) ^ (int) num ^ (int) (num >> 8)))) * (double) upperBound);
    }
  }

  public int Sign() => this.Next(0, 2) == 0 ? -1 : 1;

  public int Next(int lowerBound, int upperBound)
  {
    lock (this.object_0)
    {
      if (lowerBound > upperBound)
      {
        int num = lowerBound;
        lowerBound = upperBound;
        upperBound = num;
      }
      uint num1 = this.uint_0 ^ this.uint_0 << 11;
      this.uint_0 = this.uint_1;
      this.uint_1 = this.uint_2;
      this.uint_2 = this.uint_3;
      int num2 = upperBound - lowerBound;
      return num2 < 0 ? lowerBound + (int) (2.3283064365386963E-10 * (double) (this.uint_3 = (uint) ((int) this.uint_3 ^ (int) (this.uint_3 >> 19) ^ (int) num1 ^ (int) (num1 >> 8))) * (double) ((long) upperBound - (long) lowerBound)) : lowerBound + (int) (4.6566128730773926E-10 * (double) (int.MaxValue & (int) (this.uint_3 = (uint) ((int) this.uint_3 ^ (int) (this.uint_3 >> 19) ^ (int) num1 ^ (int) (num1 >> 8)))) * (double) num2);
    }
  }

  public double NextDouble()
  {
    lock (this.object_0)
    {
      uint num = this.uint_0 ^ this.uint_0 << 11;
      this.uint_0 = this.uint_1;
      this.uint_1 = this.uint_2;
      this.uint_2 = this.uint_3;
      return 4.6566128730773926E-10 * (double) (int.MaxValue & (int) (this.uint_3 = (uint) ((int) this.uint_3 ^ (int) (this.uint_3 >> 19) ^ (int) num ^ (int) (num >> 8))));
    }
  }

  public unsafe void NextBytes(byte[] buffer)
  {
    lock (this.object_0)
    {
      if (buffer.Length % 8 != 0)
      {
        new Random().NextBytes(buffer);
      }
      else
      {
        uint num1 = this.uint_0;
        uint num2 = this.uint_1;
        uint num3 = this.uint_2;
        uint num4 = this.uint_3;
        fixed (byte* numPtr = buffer)
        {
          int index1 = 0;
          for (int index2 = buffer.Length >> 2; index1 < index2; index1 += 2)
          {
            uint num5 = num1 ^ num1 << 11;
            uint num6 = num2;
            uint num7 = num3;
            uint num8 = num4;
            uint num9;
            ((uint*) numPtr)[index1] = num9 = (uint) ((int) num4 ^ (int) (num4 >> 19) ^ (int) num5 ^ (int) (num5 >> 8));
            uint num10 = num6 ^ num6 << 11;
            num1 = num7;
            num2 = num8;
            num3 = num9;
            ((uint*) numPtr)[index1 + 1] = num4 = (uint) ((int) num9 ^ (int) (num9 >> 19) ^ (int) num10 ^ (int) (num10 >> 8));
          }
        }
      }
    }
  }

  public uint NextUInt()
  {
    lock (this.object_0)
    {
      uint num = this.uint_0 ^ this.uint_0 << 11;
      this.uint_0 = this.uint_1;
      this.uint_1 = this.uint_2;
      this.uint_2 = this.uint_3;
      return this.uint_3 = (uint) ((int) this.uint_3 ^ (int) (this.uint_3 >> 19) ^ (int) num ^ (int) (num >> 8));
    }
  }

  public int NextInt()
  {
    lock (this.object_0)
    {
      uint num = this.uint_0 ^ this.uint_0 << 11;
      this.uint_0 = this.uint_1;
      this.uint_1 = this.uint_2;
      this.uint_2 = this.uint_3;
      return int.MaxValue & (int) (this.uint_3 = (uint) ((int) this.uint_3 ^ (int) (this.uint_3 >> 19) ^ (int) num ^ (int) (num >> 8)));
    }
  }
}
