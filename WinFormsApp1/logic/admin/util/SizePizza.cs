namespace WinFormsApp1.logic.user;

public enum SizePizza
{
    Small,
    Medium,
    Large,
}

public static class SizeExtensions
{
    public static int Slices(this SizePizza size) => size switch
    {
        SizePizza.Small => 6,
        SizePizza.Medium => 8,
        SizePizza.Large => 12,
        _ => 6
    };
    public static decimal Multiplier(this SizePizza sizePizza) => sizePizza switch
    {
        SizePizza.Small => 0.5m,
        SizePizza.Medium => 1.0m,
        SizePizza.Large => 1.5m,
        _ => 1.0m,
    };
}