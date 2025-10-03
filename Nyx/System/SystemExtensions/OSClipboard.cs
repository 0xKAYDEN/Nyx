// Decompiled with JetBrains decompiler
// Type: System.OSClipboard
// Assembly: SystemExtensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0A40C0D2-CA2F-4935-A384-ADAE2A9DCBE0
// Assembly location: D:\Nyx\Server\net9.0-windows\SystemExtensions.dll

using System.Runtime.InteropServices;

#nullable disable
namespace System;

public class OSClipboard
{
  public const uint GMEM_DDESHARE = 8192 /*0x2000*/;
  public const uint GMEM_MOVEABLE = 2;

  [DllImport("user32.dll")]
  public static extern bool OpenClipboard(IntPtr hWndNewOwner);

  [DllImport("user32.dll")]
  public static extern bool EmptyClipboard();

  [DllImport("user32.dll")]
  public static extern IntPtr GetClipboardData(uint uFormat);

  [DllImport("user32.dll")]
  public static extern IntPtr SetClipboardData(uint uFormat, IntPtr hMem);

  [DllImport("user32.dll")]
  public static extern bool CloseClipboard();

  [DllImport("Kernel32.dll")]
  public static extern void RtlMoveMemory(IntPtr dest, IntPtr src, int size);

  [DllImport("kernel32.dll")]
  public static extern IntPtr GlobalAlloc(uint uFlags, UIntPtr dwBytes);

  [DllImport("kernel32.dll")]
  public static extern IntPtr GlobalLock(IntPtr hMem);

  [DllImport("kernel32.dll")]
  public static extern IntPtr GlobalUnlock(IntPtr hMem);

  [DllImport("kernel32.dll")]
  public static extern IntPtr GlobalFree(IntPtr hMem);

  [DllImport("kernel32.dll")]
  public static extern UIntPtr GlobalSize(IntPtr hMem);

  public static string GetText()
  {
    OSClipboard.OpenClipboard(IntPtr.Zero);
    string stringUni = Marshal.PtrToStringUni(OSClipboard.GetClipboardData(13U));
    OSClipboard.CloseClipboard();
    return stringUni;
  }

  public static bool SetText(string text)
  {
    if (!OSClipboard.OpenClipboard(IntPtr.Zero))
      return false;
    OSClipboard.EmptyClipboard();
    IntPtr hMem = OSClipboard.GlobalAlloc(8194U, (UIntPtr) (ulong) (2 * (text.Length + 1)));
    IntPtr destination = OSClipboard.GlobalLock(hMem);
    if (text.Length > 0)
      Marshal.Copy(text.ToCharArray(), 0, destination, text.Length);
    OSClipboard.GlobalUnlock(hMem);
    OSClipboard.SetClipboardData(13U, hMem);
    OSClipboard.CloseClipboard();
    return true;
  }
}
