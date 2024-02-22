using System;
using System.Collections.Generic;
using System.Text;

namespace CiotSerializer
{
    public interface ISerializer
    {
        T Deserialize<T>(byte[] data, int offset = 0);
        byte[] Serialize<T>(T data);
    }
}
