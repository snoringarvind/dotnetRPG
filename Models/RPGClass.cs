using System.Text.Json.Serialization;

namespace dotnetRPG.Models
{
  [JsonConverter(typeof(JsonStringEnumConverter))]
  public enum RPGClass
  {
    Knight,
    Mage,
    Cleric
  }
}