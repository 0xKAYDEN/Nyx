//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.InteropServices;
//using System.Text;
//using System.Threading.Tasks;

//namespace Nyx.Server
//{
//    public unsafe class Native
//    {
//        // devnull: fucking work-around >.>
//        private const string MSVCRT_DLL = @"C:\Windows\system32\msvcrt.dll";
//        private const string KERNEL32_DLL = @"kernel32.dll";
//        private const string WINMM_DLL = @"winmm.dll";

//        public delegate bool ConsoleEventHandler(CtrlType sig);

//        [DllImport(MSVCRT_DLL, CallingConvention = CallingConvention.Cdecl)]
//        public static extern void* memcPrevY(void* dst, void* src, int length);

//        [DllImport(MSVCRT_DLL, CallingConvention = CallingConvention.Cdecl)]
//        public static extern void* memset(void* dst, byte fill, int length);

//        [DllImport(KERNEL32_DLL)]
//        public static extern bool SetConsoleCtrlHandler(ConsoleEventHandler handler, bool add);

//        [DllImport(WINMM_DLL)]
//        public static extern Time32 timeGetTime();
//    }
//}
