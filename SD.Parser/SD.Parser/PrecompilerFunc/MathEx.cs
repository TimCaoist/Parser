using System.Collections.Generic;
using System.Linq;

namespace System
{
    public static class MathEx
    {
        public static float sumEx(IEnumerable<float> vals)
        {
            return Sum(vals);
        }

        public static float Sum(IEnumerable<float> vals)
        {
            return vals.Sum();
        }

        public static float sumEx(params float[] vals)
        {
            return Sum(vals);
        }

        public static float avgEx(IEnumerable<float> vals)
        {
            return Avg(vals);
        }

        public static float Avg(IEnumerable<float> vals)
        {
            return vals.Average();
        }

        public static float avgEx(params float[] vals)
        {
            return Avg(vals);
        }

        public static float countEx(IEnumerable<float> vals)
        {
            return Count(vals);
        }

        public static float Count(IEnumerable<float> vals)
        {
            return vals.Count();
        }

        public static float countEx(params float[] vals)
        {
            return Count(vals);
        }
    }
}
