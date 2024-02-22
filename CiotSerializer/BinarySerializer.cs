using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CiotSerializer
{
    public class BinarySerializer : ISerializer
    {
        public int endIdx = 0;
        public int flagBit = 0;

        public T Deserialize<T>(byte[] data, int offset = 0)
        {
            int idx = offset;
            var type = typeof(T);
            var obj = Activator.CreateInstance(type);

            foreach (var prop in type.GetProperties())
            {
                var val = DeserializeValue(prop.PropertyType, data, ref idx, prop);
                prop.SetValue(obj, val);
            }

            endIdx = idx;

            return (T)obj;
        }

        public byte[] Serialize<T>(T data)
        {
            throw new NotImplementedException();
        }

        private dynamic DeserializeValue(Type type, byte[] data, ref int idx, PropertyInfo prop = null)
        {
            if (type.IsEnum)
            {
                return DeserializeEnum(type, data, ref idx);
            }

            if (type == typeof(byte) || type == typeof(bool))
            {
                var number = Convert.ChangeType(data[idx], type);
                idx += sizeof(byte);
                return number;
            }

            if (type == typeof(sbyte))
            {
                var number = (sbyte)data[idx];
                idx += sizeof(sbyte);
                return number;
            }

            if (type == typeof(short))
            {
                var number = BitConverter.ToInt16(data, idx);
                idx += sizeof(short);
                return Convert.ChangeType(number, type);
            }

            if (type == typeof(ushort))
            {
                var number = BitConverter.ToUInt16(data, idx);
                idx += sizeof(ushort);
                return Convert.ChangeType(number, type);
            }

            if (type == typeof(uint))
            {
                var number = BitConverter.ToUInt32(data, idx);
                idx += sizeof(uint);
                return Convert.ChangeType(number, type);
            }

            if (type == typeof(int))
            {
                var number = BitConverter.ToInt32(data, idx);
                idx += sizeof(uint);
                return Convert.ChangeType(number, type);
            }

            if (type == typeof(ulong))
            {
                var number = BitConverter.ToUInt64(data, idx);
                idx += sizeof(ulong);
                return Convert.ChangeType(number, type);
            }

            if (type == typeof(long))
            {
                var number = BitConverter.ToInt64(data, idx);
                idx += sizeof(long);
                return Convert.ChangeType(number, type);
            }

            if (type == typeof(float))
            {
                var number = BitConverter.ToSingle(data, idx);
                idx += sizeof(float);
                return Convert.ChangeType(number, type);
            }

            if (type == typeof(double))
            {
                var number = BitConverter.ToDouble(data, idx);
                idx += sizeof(double);
                return Convert.ChangeType(number, type);
            }

            if (type == typeof(string))
            {
                DeserializeString(data, ref idx, prop);
            }

            //if (type == typeof(ISerializable))
            //{
            //    var obj = Activator.CreateInstance(type) as ISerializable;
            //    return obj.Deserialize(this, data, idx);
            //}

            //if (typeof(IUnion).IsAssignableFrom(type))
            //{
            //    var obj = Activator.CreateInstance(type) as IUnion;
            //    var array = new byte[data.Length - idx];
            //    Array.Copy(data, idx, array, 0, data.Length - idx);
            //    obj.SetData(array);
            //    idx += obj.GetData().Length;
            //    obj.SetSerializer(this);
            //    return obj;
            //}

            if (type.IsArray)
            {
                var size = (SizeAttribute)Attribute.GetCustomAttribute(prop, typeof(SizeAttribute));
                dynamic arr = Array.CreateInstance(type.GetElementType(), size.Value);
                for (int i = 0; i < size.Value; i++)
                {
                    arr[i] = DeserializeValue(type.GetElementType(), data, ref idx);
                }
                return arr;
            }

            if (type.IsClass)
            {
                MethodInfo method = typeof(BinarySerializer).GetMethod("Deserialize").MakeGenericMethod(type);
                var obj = method.Invoke(this, new object[] { data, idx });
                idx = endIdx;
                return obj;
            }

            return 0;
        }

        private dynamic DeserializeEnum(Type type, byte[] data, ref int idx)
        {
            Type underlyingType = Enum.GetUnderlyingType(type);
            dynamic enumValue;
            int byteSize = 0;

            if (underlyingType == typeof(byte))
            {
                enumValue = data[idx];
                idx += sizeof(byte);
            }
            else if (underlyingType == typeof(ushort))
            {
                enumValue = BitConverter.ToUInt16(data, idx);
                idx += sizeof(ushort);
            }
            else if (underlyingType == typeof(short))
            {
                enumValue = BitConverter.ToInt16(data, idx);
                idx+= sizeof(short);
            }
            else if (underlyingType == typeof(uint))
            {
                enumValue = BitConverter.ToUInt32(data, idx);
                byteSize = sizeof(uint);
            }
            else if (underlyingType == typeof(int))
            {
                enumValue = BitConverter.ToInt32(data, idx);
                byteSize = sizeof(int);
            }
            else
            {
                throw new ArgumentException();
            }

            if (idx + byteSize > data.Length)
            {
                throw new ArgumentException();
            }

            idx += byteSize;
            return Enum.ToObject(type, enumValue);
        }

        string DeserializeString(byte[] data, ref int idx, PropertyInfo prop)
        {
            var size = (SizeAttribute)Attribute.GetCustomAttribute(prop, typeof(SizeAttribute));
            string text = "";
            if (size != null)
            {
                for (int i = 0; i < size.Value; i++)
                {
                    text += (char)data[idx++];
                }
                return text;
            }
            else
            {
                while (data[idx] != '\0')
                {
                    text += (char)data[idx++];
                }
                idx++;
                return text;
            }
        }
    }
}
