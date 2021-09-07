using System;
namespace FileDatabase.JSON
{
    public interface IJsonTransformer
    {
        string Serialize<T>(T obj);
        T Deserialize<T>(string json);
    }
}
