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
    GK,
    DEF,
    MID,
    FW
}
