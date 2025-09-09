using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nyx.Server
{
    //public static class Console
    //{
    //    private static DateTime NOW = DateTime.Now;
    //    private static Time32 NOW32 = Time32.Now;
    //    public static string Title
    //    {
    //        get
    //        {
    //            return System.Console.Title;
    //        }
    //        set
    //        {
    //            System.Console.Title = value;
    //        }
    //    }

    //    public static int WindowWidth
    //    {
    //        get
    //        {
    //            return System.Console.WindowWidth;
    //        }
    //        set
    //        {
    //            System.Console.WindowWidth = value; ;
    //        }
    //    }

    //    public static int WindowHeight
    //    {
    //        get
    //        {
    //            return System.Console.WindowHeight;
    //        }
    //        set
    //        {
    //            System.Console.WindowHeight = value; ;
    //        }
    //    }


    //    public static void WriteLine(object line, ConsoleColor color = ConsoleColor.Yellow)
    //    {

    //        if (line.ToString() == "" || line.ToString() == " ")
    //            System.Console.WriteLine();
    //        else
    //        {
    //            ForegroundColor = ConsoleColor.Green;
    //            System.Console.Write(TimeStamp() + " ");
    //            ForegroundColor = color;
    //            System.Console.Write(line + "\n");
    //        }
    //    }

    //    public static void WriteLine()
    //    {
    //        System.Console.WriteLine();
    //    }

    //    public static string ReadLine()
    //    {
    //        return System.Console.ReadLine();
    //    }

    //    public static ConsoleColor BackgroundColor
    //    {
    //        get
    //        {
    //            return System.Console.BackgroundColor;
    //        }
    //        set
    //        {
    //            System.Console.BackgroundColor = value;
    //        }
    //    }

    //    public static void Clear()
    //    {
    //        System.Console.Clear();
    //    }

    //    public static ConsoleColor ForegroundColor
    //    {
    //        get
    //        {
    //            return System.Console.ForegroundColor;
    //        }
    //        set
    //        {
    //            System.Console.ForegroundColor = value;
    //        }
    //    }

    //    public static void DrawProgressBar(uint complete, uint maxVal, int barSize, char progressCharacter)
    //    {
    //        System.Console.CursorVisible = false;
    //        int Left = System.Console.CursorLeft;
    //        decimal perc = (decimal)complete / (decimal)maxVal;
    //        int chars = (int)Math.Floor(perc / ((decimal)1 / (decimal)barSize));
    //        string p1 = String.Empty, p2 = String.Empty;

    //        for (int i = 0; i < chars; i++) p1 += progressCharacter;
    //        for (int i = 0; i < barSize - chars; i++)

    //            Console.ForegroundColor = ConsoleColor.Red;
    //        System.Console.Write(p1);
    //        System.Console.ForegroundColor = ConsoleColor.Black;
    //        System.Console.Write(p2);
    //        System.Console.ResetColor();
    //        System.Console.Write("\b ", (perc * 1));
    //        System.Console.CursorLeft = Left;
    //    }
    //    public static string TimeStamp()
    //    {
    //        return "<<[Nyx.Server]>>";
    //    }
    //}
}
