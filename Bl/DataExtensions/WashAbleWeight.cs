

namespace Bl.DataExtensions;

public static class WashAbleWeight
{
    public static Dictionary<string, double> WashAblesWeight { get; set; } = new() { { "sock", 4 }, { "towel", 5 } };
    public static double GetWeight(string washAbleType) 
    {
        try
        {
            return WashAblesWeight[washAbleType];
        }
        catch (KeyNotFoundException) { return 0; };   
    }
}

