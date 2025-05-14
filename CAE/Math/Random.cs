using System.Collections.Generic;
using System.Linq;
using CAE.Extensions;
using UnityEngine;

namespace CAE.Math
{
    /// <summary>
    /// Static class providing random number generation utilities.
    /// </summary>
    public static class RNG
    {
        static int _seed = System.DateTime.Now.Millisecond;
        static Random _random = new (_seed);
       
        /// <summary>
        /// Generates a count from a float range.
        /// </summary>
        /// <param name="range">The range to generate the count from.</param>
        /// <returns>A float representing the count.</returns>
        public static float CountFromFloat(float range) => _random.CountFromFloat(range);
         
        /// <summary>
        /// Generates a random float between the specified range.
        /// </summary>
        /// <param name="minOrMax">The minimum value or maximum value if max is not specified.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>A random float.</returns>
        public static float Float(float? minOrMax = null, float? max = null) => _random.Float(minOrMax, max);
        
        /// <summary>
        /// Generates a random integer between the specified range (inclusive).
        /// </summary>
        /// <param name="minOrMax">The minimum value or maximum value if max is not specified.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>A random integer.</returns>
        public static int Inclusive(int minOrMax, int? max = null) => _random.Inclusive(minOrMax, max);
        
        /// <summary>
        /// Generates a normally distributed random float.
        /// </summary>
        /// <returns>A normally distributed random float.</returns>
        public static float Normal() => _random.Normal();
        
        /// <summary>
        /// Determines if a random event occurs with a 1 in N chance.
        /// </summary>
        /// <param name="chance">The chance denominator.</param>
        /// <returns>True if the event occurs, otherwise false.</returns>
        public static bool OneIn(int chance) => _random.OneIn(chance);
        
        /// <summary>
        /// Determines if a random event occurs with a specified percentage chance.
        /// </summary>
        /// <param name="chance">The percentage chance.</param>
        /// <returns>True if the event occurs, otherwise false.</returns>
        public static bool Percent(int chance) => _random.Percent(chance);
        
        /// <summary>
        /// Generates a random integer between the specified range.
        /// </summary>
        /// <param name="minOrMax">The minimum value or maximum value if max is not specified.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>A random integer.</returns>
        public static int Range(int minOrMax, int? max = null) => _random.Range(minOrMax, max);
        
        /// <summary>
        /// Rounds a float to the nearest integer.
        /// </summary>
        /// <param name="value">The float value to round.</param>
        /// <returns>The rounded integer.</returns>
        public static int Round(float value) => _random.Round(value);

        /// <summary>
        /// Sets the seed for the random number generator.
        /// </summary>
        /// <param name="seed">The seed value.</param>
        public static void SetSeed(int seed)
        {
            _seed = seed;
            _random.SetSeed(seed);
        }
        
        /// <summary>
        /// Gets the current seed value used by the random number generator.
        /// </summary>
        /// <returns>The current seed value.</returns>
        public static int GetSeed() => _seed;
        
        /// <summary>
        /// Generates a tapered random integer starting from a specified value.
        /// </summary>
        /// <param name="start">The starting value.</param>
        /// <param name="chanceOfIncrement">The chance of incrementing the value.</param>
        /// <returns>The tapered random integer.</returns>
        public static int Taper(int start, int chanceOfIncrement) => _random.Taper(start, chanceOfIncrement);
        
        /// <summary>
        /// Selects a random item from an enumerable collection.
        /// </summary>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        /// <param name="enumerable">The collection to select from.</param>
        /// <returns>A random item from the collection.</returns>
        public static T Item<T>(IEnumerable<T> enumerable) => _random.Item(enumerable);
            
        /// <summary>
        /// Selects a random item from an array of probabilities.
        /// </summary>
        /// <typeparam name="T">The type of the items.</typeparam>
        /// <param name="probabilities">An array of probabilities.</param>
        /// <returns>A randomly selected item based on the given probabilities.</returns>
        public static T Chance<T>(IEnumerable<Probability<T>> probabilities) => _random.Chance(probabilities);
        
        
    }
    
    /// <summary>
    /// Class providing random number generation utilities.
    /// </summary>
    public class Random
    {
        System.Random _random = new ();

        /// <summary>
        /// Initializes a new instance of the Random class with a specified seed.
        /// </summary>
        /// <param name="seed">The seed value.</param>
        public Random(int seed)
        {
            _random = new System.Random(seed);
        }
        
        /// <summary>
        /// Generates a count from a float range.
        /// </summary>
        /// <param name="range">The range to generate the count from.</param>
        /// <returns>A float representing the count.</returns>
        public float CountFromFloat(float range)
        {
            var count = range.Floor();
            if(Float(1.0f) < range - count)
                count++;
            return count;
        }
        
        /// <summary>
        /// Generates a random float between the specified range.
        /// </summary>
        /// <param name="minOrMax">The minimum value or maximum value if max is not specified.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>A random float.</returns>
        public float Float(float? minOrMax = null, float? max = null)
        {
            float value = (float)_random.NextDouble();
            if (minOrMax == null)
                return value;
            if (max == null)
                return value * minOrMax.Value;
            return value * (max.Value - minOrMax.Value) + minOrMax.Value;
        }
        
        /// <summary>
        /// Generates a random integer between the specified range (inclusive).
        /// </summary>
        /// <param name="minOrMax">The minimum value or maximum value if max is not specified.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>A random integer.</returns>
        public int Inclusive(int minOrMax, int? max = null)
        {
            if (max == null)
            {
                max = minOrMax;
                minOrMax = 0;
            }
            
            return _random.Next(max.Value - minOrMax) + minOrMax;
        }
        
        /// <summary>
        /// Selects a random item from an enumerable collection.
        /// </summary>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        /// <param name="enumerable">The collection to select from.</param>
        /// <returns>A random item from the collection.</returns>
        public T Item<T>(IEnumerable<T> enumerable)
        {
            if(enumerable == null)
                return default;
            var enumerable1 = enumerable as T[] ?? enumerable.ToArray();
            return enumerable1.ElementAt(Range(0, enumerable1.Count()));
        }

        /// <summary>
        /// Generates a normally distributed random float.
        /// </summary>
        /// <returns>A normally distributed random float.</returns>
        public float Normal()
        {
            float u, v, lengthSquared;

            do
            {
                u = Float(-1.0f, 1.0f);
                v = Float(-1.0f, 1.0f);
                lengthSquared = u * u + v * v;
            } while (lengthSquared >= 1.0f);
            return u * (float)System.Math.Sqrt(-2.0f * System.Math.Log(lengthSquared) / lengthSquared);
        }

        /// <summary>
        /// Determines if a random event occurs with a 1 in N chance.
        /// </summary>
        /// <param name="chance">The chance denominator.</param>
        /// <returns>True if the event occurs, otherwise false.</returns>
        public bool OneIn(int chance) => Range(chance) == 0;
        
        /// <summary>
        /// Determines if a random event occurs with a specified percentage chance.
        /// </summary>
        /// <param name="chance">The percentage chance.</param>
        /// <returns>True if the event occurs, otherwise false.</returns>
        public bool Percent(int chance) => Range(100) < chance;
        
        /// <summary>
        /// Generates a random integer between the specified range.
        /// </summary>
        /// <param name="minOrMax">The minimum value or maximum value if max is not specified.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>A random integer.</returns>
        public int Range(int minOrMax, int? max = null)
        {
            if (!max.HasValue)
            {
                max = Mathf.Abs(minOrMax);
                minOrMax = 0;
            }
            if(max < minOrMax)
                return _random.Next(minOrMax - max.Value) + max.Value;
            return _random.Next(max.Value-minOrMax) + minOrMax;
        }

        /// <summary>
        /// Rounds a float to the nearest integer.
        /// </summary>
        /// <param name="value">The float value to round.</param>
        /// <returns>The rounded integer.</returns>
        public int Round(float value)
        {
            int result = (int)value.Floor();
            if(Float(1f) < value - result)
                result++;
            return result;
        }
        
        /// <summary>
        /// Sets the seed for the random number generator.
        /// </summary>
        /// <param name="seed">The seed value.</param>
        public void SetSeed(int seed) => _random = new System.Random(seed);

        /// <summary>
        /// Generates a tapered random integer starting from a specified value.
        /// </summary>
        /// <param name="start">The starting value.</param>
        /// <param name="chanceOfIncrement">The chance of incrementing the value.</param>
        /// <returns>The tapered random integer.</returns>
        public int Taper(int start, int chanceOfIncrement)
        {
            while (OneIn(chanceOfIncrement))
                start++;
            return start;
        }

        /// <summary>
        /// Generates a random integer within a triangular distribution.
        /// </summary>
        /// <param name="center">The center value.</param>
        /// <param name="range">The range value.</param>
        /// <returns>A random integer within the triangular distribution.</returns>
        public int TriangleInt(int center, int range)
        {
            if(range < 0)
                throw new System.ArgumentException("Range must be zero or greater");

            int x = Inclusive(range);
            int y = Inclusive(range);

            if (x <= y)
                return center + x;
            return center - range - 1 + x;
        }        
        
        /// <summary>
        /// Selects a random item from an array of probabilities.
        /// </summary>
        /// <typeparam name="T">The type of the items.</typeparam>
        /// <param name="probabilities">An array of probabilities.</param>
        /// <returns>A randomly selected item based on the given probabilities.</returns>
        public T Chance<T>(IEnumerable<Probability<T>> probabilities)
        {
            var enumerable = probabilities as Probability<T>[] ?? probabilities.ToArray();
            float total = enumerable.Sum(probability => probability.probability);
            float roll = Float(total);
            float sum = 0;
            foreach (var probability in enumerable)
            {
                sum += probability.probability;
                if (roll < sum)
                    return probability.value;
            }
            return enumerable.Last().value;
        }
    }
}
