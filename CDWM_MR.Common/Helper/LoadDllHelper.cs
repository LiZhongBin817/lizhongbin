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
        [DllImport("sum.dll", EntryPoint = "sum", CallingConvention = CallingConvention.Cdecl)]
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
        private delegate void dlgtStructRef(ref ArrayStruct arr);

        private static dlgtStructRef stctref;

        public static void TryLoadAssembly()
        {
            //string path = "http://129.204.96.9:8088/images/Type_2/201911/Reader_CB001/Taskid_4/LCB0012019112917391924.jpg";
            IntPtr pDll = LoadLibrary(@"C:\Users\34688\Desktop\opencvdll\sum\x64\Debug\sum.dll");
            IntPtr pAddressOfFunctionToCall = GetProcAddress(pDll, "stctarr");
            stctref = (dlgtStructRef)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(dlgtStructRef));

        }

        public static string ImgORCMethod(string imagepath)
        {
            var stct = new ArrayStruct();
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

        private struct ArrayStruct
        {
            public string img_path;
            public bool flag;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public int[] datas;

        }

    }
}
