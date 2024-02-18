using System;
using System.Runtime.InteropServices;

namespace Ciot
{
    public static class Serializer
    {
        public static byte[] Serialize<T>(T obj) where T : struct
        {
            int size = Marshal.SizeOf(obj); 
            byte[] bytes = new byte[size]; 
            IntPtr ptr = Marshal.AllocHGlobal(size); 

            try
            {
                Marshal.StructureToPtr(obj, ptr, false);
                Marshal.Copy(ptr, bytes, 0, size);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr); 
            }

            return bytes;
        }

        
        public static T Deserialize<T>(byte[] data) where T : struct
        {
            int size = data.Length; 
            IntPtr ptr = Marshal.AllocHGlobal(size); 

            try
            {
                Marshal.Copy(data, 0, ptr, size);
                return Marshal.PtrToStructure<T>(ptr);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr); 
            }
        }
    }
}
