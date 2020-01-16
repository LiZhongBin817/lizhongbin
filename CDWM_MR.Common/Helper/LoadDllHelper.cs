using CDWM_MR.Common.LogHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Loader;
using System.Text;

namespace CDWM_MR.Common.Helper
{
    public class LoadDllHelper
    {
       [DllImport("new_water_end.dll", EntryPoint = "stctarr", CallingConvention = CallingConvention.Cdecl)]
      // [DllImport("easy_dll.dll", EntryPoint = "stctarr", CallingConvention = CallingConvention.Cdecl)]
        public static extern void stctarr(ref ArrayStruct data);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr LoadLibraryEx(IntPtr dllToLoad, IntPtr load, uint loadpath);
        //public static extern IntPtr LoadLibrary(IntPtr dllToLoad);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, IntPtr procedureName);

        [DllImport("kernel32.dll")]
        public static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("kernel32.dll")]
        public static extern UIntPtr GlobalSize(IntPtr hMem);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void dlgtStructRef(ref LoadDllHelper.ArrayStruct arr);

        private static dlgtStructRef stctref;

        private static IntPtr pDll;

        private static IntPtr pAddressOfFunctionToCall;

         [StructLayout(LayoutKind.Sequential)]
        public struct ArrayStruct
        {
            public string img_path;
            public string temp_path;
            public bool flag;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public int[] datas;

        }
        public static void TryLoadAssembly()
        {
           Assembly entry = Assembly.GetEntryAssembly();
           string dir = Path.Combine(Path.GetDirectoryName(entry.Location), "new_water_end.dll");
           //string dir = Path.Combine(Path.GetDirectoryName(entry.Location), "easy_dll.dll");
            pDll = LoadLibraryEx(Marshal.StringToHGlobalAnsi(dir), IntPtr.Zero, 0);
            //pDll = LoadLibrary(Marshal.StringToHGlobalAnsi(dir));
            pAddressOfFunctionToCall = GetProcAddress(pDll, Marshal.StringToHGlobalAnsi("stctarr"));
            stctref = (dlgtStructRef)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(dlgtStructRef));

        }

        public static string Juge()
        {
            Assembly entry = Assembly.GetEntryAssembly();
            string dir = Path.Combine(Path.GetDirectoryName(entry.Location), "new_water_end.dll");
            return dir;

        }
        public static string ImgORCMethod(string imagepath)
        {                
            var stct = new LoadDllHelper.ArrayStruct();
            stct.temp_path = "http://129.204.96.9:1235/modelphoto/";
            //stct.temp_path = "C:\\Users\\34688\\Desktop\\template\\template\\";
            //stct.temp_path = "C:\\Users\\34688\\Desktop\\template\\template\\";
            stct.img_path = imagepath;
            stct.flag = false;
            stctref(ref stct);
            //string rstr = string.Empty;
            string rstr ="";
            var t = new StringBuilder();
            for (int i = 0; i < stct.datas.Length; i++)
            {
                t.Append(stct.datas[i]);
            }
            rstr = t.ToString();
            bool b = FreeLibrary(pDll);
            return rstr;
        }
        

    }
}
