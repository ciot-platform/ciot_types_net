namespace Ciot
{
    public class MsgDataUnion <ConfigDataType, StatusDataType, RequestDataType>
        where ConfigDataType : struct
        where StatusDataType : struct
        where RequestDataType : struct
    {
        protected byte[] data;

        public ConfigDataType Config
        {
            get => Serializer.Deserialize<ConfigDataType>(data);
            set => data = Serializer.Serialize(value);
        }

        public StatusDataType Status
        {
            get => Serializer.Deserialize<StatusDataType>(data);
            set => data = Serializer.Serialize(value);
        }

        public RequestDataType Request
        {
            get => Serializer.Deserialize<RequestDataType>(data);
            set => data = Serializer.Serialize(value);
        }

        public MsgDataUnion(byte[] data)
        {
            this.data = data;
        }
    }
}
