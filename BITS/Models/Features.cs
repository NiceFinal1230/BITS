using System.ComponentModel;

namespace BITS.Models;
public enum Features
{
    [Description("Cloud Saves")]
    CloudSaves = 1,
    [Description("Co-op")]
    Coop = 2,
    [Description("Multiplayer")]
    Multiplayer = 3,
    [Description("Single Player")]
    SinglePlayer = 4
}
