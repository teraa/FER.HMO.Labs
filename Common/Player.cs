// ReSharper disable InconsistentNaming
namespace Common;

public record Player(
    int Id,
    Position Position,
    string Name,
    string Club,
    int Points,
    decimal Price
);

public enum Position
{
    GK = 0,
    DEF = 1,
    MID = 2,
    FW = 3,
}
