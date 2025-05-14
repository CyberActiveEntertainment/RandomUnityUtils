namespace CAE.Extensions
{
    public static class StandardExtensions
    {
        public static float Floor(this float f) => (float)System.Math.Floor(f);
        public static int FloorToInt(this float f) => (int)System.Math.Floor(f);
        public static float Ceil(this float f) => (float)System.Math.Ceiling(f);
        
        public static double Floor(this double i) => System.Math.Floor(i);
        public static double Ceil(this double i) => System.Math.Ceiling(i);
        
        public static float Abs(this float f) => System.Math.Abs(f);
        public static int Abs(this int i) => System.Math.Abs(i);
        public static double Abs(this double d) => System.Math.Abs(d);
        
        public static float Clamp(this float f, float min, float max) => f < min ? min : f > max ? max : f;
        public static int Clamp(this int i, int min, int max) => i < min ? min : i > max ? max : i;
        public static double Clamp(this double d, double min, double max) => d < min ? min : d > max ? max : d;
        
        public static float Lerp(this float a, float b, float t) => a + (b - a) * t;
        public static double Lerp(this double a, double b, double t) => a + (b - a) * t;
        
        public static float Map(this float value, float from1, float to1, float from2, float to2) => (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        public static double Map(this double value, double from1, double to1, double from2, double to2) => (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        
        public static float Remap(this float value, float from1, float to1, float from2, float to2) => value.Map(from1, to1, from2, to2);
        public static double Remap(this double value, double from1, double to1, double from2, double to2) => value.Map(from1, to1, from2, to2);
        
        public static float Round(this float f) => (float)System.Math.Round(f);
        public static double Round(this double d) => System.Math.Round(d);
        
        public static float Sqrt(this float f) => (float)System.Math.Sqrt(f);
        public static double Sqrt(this double d) => System.Math.Sqrt(d);
        
        public static float Pow(this float f, float p) => (float)System.Math.Pow(f, p);
        public static double Pow(this double d, double p) => System.Math.Pow(d, p);
        
        public static float Sin(this float f) => (float)System.Math.Sin(f);
        public static double Sin(this double d) => System.Math.Sin(d);
        
        public static float Cos(this float f) => (float)System.Math.Cos(f);
        public static double Cos(this double d) => System.Math.Cos(d);
        
        public static float Tan(this float f) => (float)System.Math.Tan(f);
        public static double Tan(this double d) => System.Math.Tan(d);
        
        public static float Asin(this float f) => (float)System.Math.Asin(f);
        public static double Asin(this double d) => System.Math.Asin(d);
        
        public static float Acos(this float f) => (float)System.Math.Acos(f);
        public static double Acos(this double d) => System.Math.Acos(d);
        
        public static float Atan(this float f) => (float)System.Math.Atan(f);
        public static double Atan(this double d) => System.Math.Atan(d);
        
        public static float Atan2(this float y, float x) => (float)System.Math.Atan2(y, x);
        public static double Atan2(this double y, double x) => System.Math.Atan2(y, x);
        
        public static float Exp(this float f) => (float)System.Math.Exp(f);
        public static double Exp(this double d) => System.Math.Exp(d);
        
        public static float Log(this float f) => (float)System.Math.Log(f);
        public static double Log(this double d) => System.Math.Log(d);
        
        public static float Log(this float f, float newBase) => (float)System.Math.Log(f, newBase);
        public static double Log(this double d, double newBase) => System.Math.Log(d, newBase);
        
        public static float Log10(this float f) => (float)System.Math.Log10(f);
        public static double Log10(this double d) => System.Math.Log10(d);
        
        public static float Max(this float a, float b) => System.Math.Max(a, b);
        public static double Max(this double a, double b) => System.Math.Max(a, b);
        
        public static float Min(this float a, float b) => System.Math.Min(a, b);
        public static double Min(this double a, double b) => System.Math.Min(a, b);
        
        public static float Sign(this float f) => System.Math.Sign(f);
        public static int Sign(this int i) => System.Math.Sign(i);
        public static double Sign(this double d) => System.Math.Sign(d);
        
        public static float Truncate(this float f) => (float)System.Math.Truncate(f);
        public static double Truncate(this double d) => System.Math.Truncate(d);
        
        public static float Round(this float f, int decimals) => (float)System.Math.Round(f, decimals);
        public static double Round(this double d, int decimals) => System.Math.Round(d, decimals);
        
        public static float Clamp01(this float f) => f.Clamp(0, 1);
        public static double Clamp01(this double d) => d.Clamp(0, 1);
        
        public static float PingPong(this float t, float length) => length - System.Math.Abs((t % (length * 2)) - length);
        public static double PingPong(this double t, double length) => length - System.Math.Abs((t % (length * 2)) - length);
        
        public static float Repeat(this float t, float length) => t % length;
        public static double Repeat(this double t, double length) => t % length;
        
        public static float InverseLerp(this float a, float b, float value) => (value - a) / (b - a);
        public static double InverseLerp(this double a, double b, double value) => (value - a) / (b - a);
        
        public static float SmoothStep(this float a, float b, float t) => t * t * (3 - 2 * t);
        public static double SmoothStep(this double a, double b, double t) => t * t * (3 - 2 * t);
        
        public static float Step(this float edge, float x) => x < edge ? 0 : 1;
        public static double Step(this double edge, double x) => x < edge ? 0 : 1;
    }
}
