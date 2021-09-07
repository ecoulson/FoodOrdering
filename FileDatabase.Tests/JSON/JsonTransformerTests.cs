using System;
using FileDatabase.JSON;
using Xunit;

namespace FileDatabase.Tests.JSON
{
    public class JsonTransformerTests
    {
        class A
        {
            public A()
            {
                Foo = "Bar";
            }

            public string Foo;
        }

        private static readonly string SerializedObject = "{\"Foo\":\"Bar\"}";

        private readonly IJsonTransformer jsonTransformer;

        public JsonTransformerTests()
        {
            jsonTransformer = new JsonTransformer();
        }

        [Fact]
        public void WHEN_SerializingJson_SHOULD_ReturnSerializedJSON()
        {
            var serialized = jsonTransformer.Serialize<A>(new A());

            Assert.Equal(SerializedObject, serialized);
        }

        [Fact]
        public void WHEN_DeserializingJSON_SHOULD_ReturnDeserializedJSON()
        {
            var deserialized = jsonTransformer.Deserialize<A>(SerializedObject);

            Assert.Equal("Bar", deserialized.Foo);
        }
    }
}
