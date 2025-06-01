using Unity.Mathematics;

namespace UnityEngine.UI
{
    public enum Orientation { Horizontal, Vertical }
    public enum AdvancedOrientation { Horizontal, Vertical, DiagonalRight, DiagonalLeft }

    public static class UIExtension
    {
        public static float2 GetDirection(this Image image)
        {
            switch (image.fillMethod)
            {
                case Image.FillMethod.Horizontal: return image.fillOrigin == 0 ? mathf.left : mathf.right;
                case Image.FillMethod.Vertical: return image.fillOrigin == 0 ? mathf.down : mathf.up;
                default: return mathf.zero;
            }
        }

        public static bool IsDirection(this Orientation orientation, Orientation reference)
        {
            return orientation == reference;
        }
        public static float2 GetOrientation(this Orientation orientation)
        {
            switch (orientation)
            {
                case Orientation.Horizontal: return mathf.right;
                case Orientation.Vertical: return mathf.up;
                default: return mathf.zero;
            }
        }
        public static float2 GetOrientation(this AdvancedOrientation orientation)
        {
            switch(orientation)
            {
                case AdvancedOrientation.Horizontal: return mathf.right;
                case AdvancedOrientation.Vertical: return mathf.up;
                case AdvancedOrientation.DiagonalRight: return mathf.one;
                case AdvancedOrientation.DiagonalLeft: return new Vector2(-1, 1);
                default: return mathf.zero;
            }
        }

        public static void Fade(this Image image, float alpha)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }
        public static void SetValue(this Slider slider, float value)
        {
            slider.value = value;
        }
        public static void AddValue(this Slider slider, float value)
        {
            slider.value += value;
        }
    }
}