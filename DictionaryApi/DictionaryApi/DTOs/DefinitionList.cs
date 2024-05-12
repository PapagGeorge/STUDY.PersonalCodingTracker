using Domain;
using System.Text.Json.Serialization;
using System.Collections;

namespace DictionaryApi.DTOs;
public class DefinitionList
{
    [JsonPropertyName("list")]
    public List<DefinitionModel> DefinitionModelList { get; set; }
}

