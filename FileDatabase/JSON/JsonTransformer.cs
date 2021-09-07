using Newtonsoft.Json;

namespace FileDatabase.JSON
{
    public class JsonTransformer: IJsonTransformer
    {
        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
