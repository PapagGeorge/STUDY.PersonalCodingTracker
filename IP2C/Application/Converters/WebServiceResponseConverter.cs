using Domain.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.Converters
{
    public class WebServiceResponseConverter : JsonConverter<WebServiceResponse>
    {
        public override WebServiceResponse Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using JsonDocument document = JsonDocument.ParseValue(ref reader);

            string name = document.RootElement.GetProperty("Name").GetString() ?? string.Empty;
            string twoLetterCode = document.RootElement.GetProperty("TwoLetterCode").ToString() ?? string.Empty;
            string threeLetterCode = document.RootElement.GetProperty("ThreeLetterCode").ToString() ?? string.Empty;

            return WebServiceResponse.Create(name, twoLetterCode, threeLetterCode);
        }

        public override void Write(Utf8JsonWriter writer, WebServiceResponse value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
