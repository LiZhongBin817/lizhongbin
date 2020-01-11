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
        [DllImport("水表-试运行.dll", EntryPoint = "水表-试运行", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Sum(int a, int b);

        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string dllToLoad);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

        [DllImport("kernel32.dll")]
        public static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("kernel32.dll")]
        public static extern UIntPtr GlobalSize(IntPtr hMem);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void dlgtStructRef(ref LoadDllHelper.ArrayStruct arr);

        private static dlgtStructRef stctref;

        private static IntPtr pDll;

        private static IntPtr pAddressOfFunctionToCall;

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
            pDll = LoadLibrary(dir);

            pAddressOfFunctionToCall = GetProcAddress(pDll, "stctarr");
            stctref = (dlgtStructRef)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(dlgtStructRef));

        }

        public static string ImgORCMethod(string imagepath)
        {
            var stct = new LoadDllHelper.ArrayStruct();
            //stct.temp_path = "http://129.204.96.9:1235/modelphoto/";
            stct.temp_path = "C:\\Users\\34688\\Desktop\\template\\template\\";
            stct.img_path = imagepath;
            stctref(ref stct);
            string rstr = string.Empty;
            var t = new StringBuilder();
            for (int i = 0; i < stct.datas.Length; i++)
            {
                t.Append(stct.datas[i]);
            }
            rstr = t.ToString();
            return rstr;
        }
        

    }
}
