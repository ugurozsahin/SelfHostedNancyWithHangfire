using Implementation.Abstracts.Lifestyles;

namespace Implementation.Abstracts.Common
{
    public interface ISerializer : ISingletonService
    {
        string Serialize(object value);
        T Deserialize<T>(string value);
    }
}