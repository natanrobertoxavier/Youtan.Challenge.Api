using System.Text.Json;
using System.Text.Json.Serialization;
using Youtan.Challenge.Communication.Enums;

namespace Youtan.Challenge.Communication.Filters;

public class ItemsTypeConverter : JsonConverter<ItemType>
{
    public override ItemType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var itemTypeString = reader.GetString();

        if (Enum.TryParse(typeof(ItemType), itemTypeString, ignoreCase: true, out var result) && result is ItemType itemType)
        {
            return itemType;
        }

        throw new JsonException($"Invalid value for ItemType: {itemTypeString}");
    }

    public override void Write(Utf8JsonWriter writer, ItemType value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
