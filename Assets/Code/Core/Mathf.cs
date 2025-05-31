using UnityEngine;
using Unity.Mathematics;

public enum Direction
{
    Top, Right, Left, Bottom
}

public static class mathf
{
    public static readonly float2 up = new(0f, 1f);
    public static readonly float2 down = new(0f, -1f);
    public static readonly float2 left = new(-1f, 0f);
    public static readonly float2 right = new(1f, 0f);
    public static readonly float2 one = new(1f, 1f);
    public static readonly float2 zero = new(0f, 0f);
}

public static class MathExtension
{
    public static float2 GetDirection(this Direction dir)
    {
        switch (dir)
        {
            case Direction.Top: return mathf.up;
            case Direction.Right: return mathf.right;
            case Direction.Left: return mathf.left;
            case Direction.Bottom: return mathf.down;
            default: return mathf.zero;
        }
    }
    public static bool Compare(this float3 direction, float3 other)
    {
        direction = math.round(direction);
        float3 absolute = math.abs(direction);

        bool uniqueDir = other.Equals(absolute);
        return uniqueDir || absolute.x == other.x || absolute.z == other.z;
    }

    public static Color SetColorValue(Color target, float alpha)
    {
        float3 colors;
        Color.RGBToHSV(target, out colors.x, out colors.y, out colors.z); colors.z = alpha;
        return Color.HSVToRGB(colors.x, colors.y, colors.z);
    }
    public static float GetColorValue(Color current)
    {
        float3 colors;
        Color.RGBToHSV(current, out colors.x, out colors.y, out colors.z);
        return colors.z;
    }

    public static float InverseLerp(float2 a, float2 b, float2 value)
    {
        float2 ab = b - a;
        float2 av = value - a;
        return Mathf.Clamp01(math.dot(av, ab) / math.dot(ab, ab));
    }
    public static float InverseLerp(float3 a, float3 b, float3 value)
    {
        float3 ab = b - a;
        float3 av = value - a;
        return Mathf.Clamp01(math.dot(av, ab) / math.dot(ab, ab));
    }
}