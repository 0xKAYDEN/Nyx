
public struct Time32
{
  private int int_0;
  private static uint uint_0;
  public static readonly Time32 NULL = new Time32(0);

  public Time32(int Value) => this.int_0 = Value;

  public Time32(uint Value) => this.int_0 = (int) Value;

  public Time32(long Value) => this.int_0 = (int) Value;

  public static Time32 Now
  {
    get
    {
      Class1.Class0.smethod_0();
      return Time32.timeGetTime();
    }
  }

  public int TotalMilliseconds => this.int_0;

  public int Value => this.int_0;

  public Time32 AddMilliseconds(int Amount) => new Time32(this.int_0 + Amount);

  public int AllMilliseconds() => this.GetHashCode();

  public Time32 AddSeconds(int Amount) => this.AddMilliseconds(Amount * 1000);

  public int AllSeconds() => this.AllMilliseconds() / 1000;

  public Time32 AddMinutes(int Amount) => this.AddSeconds(Amount * 60);

  public int AllMinutes() => this.AllSeconds() / 60;

  public Time32 AddHours(int Amount) => this.AddMinutes(Amount * 60);

  public int AllHours() => this.AllMinutes() / 60;

  public Time32 AddDays(int Amount) => this.AddHours(Amount * 24);

  public int AllDays() => this.AllHours() / 24;

  public bool Next(int due = 0, int time = 0)
  {
    if (time == 0)
      time = Time32.timeGetTime().int_0;
    return this.int_0 + due <= time;
  }

  public void Set(int due, int time = 0)
  {
    if (time == 0)
      time = Time32.timeGetTime().int_0;
    this.int_0 = time + due;
  }

  public void SetSeconds(int due, int time = 0) => this.Set(due * 1000, time);

  public override bool Equals(object obj)
  {
    return obj is Time32 time32 ? time32 == this : base.Equals(obj);
  }

  public override string ToString() => this.int_0.ToString();

  public override int GetHashCode() => this.int_0;

  public static bool operator ==(Time32 t1, Time32 t2) => t1.int_0 == t2.int_0;

  public static bool operator !=(Time32 t1, Time32 t2) => t1.int_0 != t2.int_0;

  public static bool operator >(Time32 t1, Time32 t2) => t1.int_0 > t2.int_0;

  public static bool operator <(Time32 t1, Time32 t2) => t1.int_0 < t2.int_0;

  public static bool operator >=(Time32 t1, Time32 t2) => t1.int_0 >= t2.int_0;

  public static bool operator <=(Time32 t1, Time32 t2) => t1.int_0 <= t2.int_0;

  public static Time32 operator -(Time32 t1, Time32 t2) => new Time32(t1.int_0 - t2.int_0);

  //[DllImport("winmm.dll")]
  public static extern Time32 timeGetTime();
}
