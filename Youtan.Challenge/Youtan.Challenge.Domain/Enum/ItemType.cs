using System.ComponentModel;

namespace Youtan.Challenge.Domain.Enum;

public enum ItemType
{
    [Description("Veículo")]
    Vehicle = 0,
    [Description("Imóvel")]
    Property = 1,
}
