using Implementation.Abstracts.Common;
using Newtonsoft.Json;

namespace Implementation.Concretes.Common
{
    public class DefaultJsonSerializer : ISerializer
    {
        public string Serialize(object value)
        {
            var serializedValue = JsonConvert.SerializeObject(value);
            return serializedValue;
        }

        public T Deserialize<T>(string value)
        {
            var deserializedValue = JsonConvert.DeserializeObject<T>(value);
            return deserializedValue;
        }
    }
}
