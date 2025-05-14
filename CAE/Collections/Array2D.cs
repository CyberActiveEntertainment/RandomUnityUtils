using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace CAE.Collections.Generic
{
    /// <summary>
    /// A generic 2D array class that supports serialization and provides various utility methods.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    [Serializable]
    public class Array2D<T> : IEnumerable<T>
    {
        [SerializeField] private T[] array;
        [NonSerialized] private readonly int width;
        [NonSerialized] private readonly int height;
        
        [NonSerialized] private readonly bool isClass = typeof(T).IsClass;

        /// <summary>
        /// Gets or sets a value indicating whether bounds checking is enabled.
        /// </summary>
        public bool SafeMode { get; set; } = true;

        /// <summary>
        /// Gets the width of the 2D array.
        /// </summary>
        public int Width => width;

        /// <summary>
        /// Gets the height of the 2D array.
        /// </summary>
        public int Height => height;
        
        public int Length => array.Length;
        
        

        /// <summary>
        /// Initializes a new instance of the <see cref="Array2D{T}"/> class with the specified width and height.
        /// </summary>
        /// <param name="width">The width of the array.</param>
        /// <param name="height">The height of the array.</param>
        public Array2D(int width, int height)
        {
            this.width = width;
            this.height = height;
            array = new T[width * height];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Array2D{T}"/> class by copying from another instance.
        /// </summary>
        /// <param name="source">The source <see cref="Array2D{T}"/> to copy from.</param>
        public Array2D(Array2D<T> source)
        {
            width = source.width;
            height = source.height;
            array = new T[width * height];
            Array.Copy(source.array, array, array.Length);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Array2D{T}"/> class with the specified array, width, and height.
        /// </summary>
        /// <param name="mArray">The 1D array to initialize the 2D array with.</param>
        /// <param name="width">The width of the array.</param>
        /// <param name="height">The height of the array.</param>
        public Array2D(T[] mArray, int width, int height)
        {
            this.array = mArray;
            this.width = width;
            this.height = height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Array2D{T}"/> class with the specified 2D array.
        /// </summary>
        /// <param name="array">The 2D array to initialize the 2D array with.</param>
        /// <remarks>
        /// This constructor swaps the order of indices when copying elements from the source array (`array[y, x]` instead of `array[x, y]`)
        /// to improve cache locality. By iterating through rows (`y`) first and storing elements linearly in a 1D array, we increase the
        /// likelihood that consecutive elements will be adjacent in memory. This improves performance due to better cache utilization,
        /// especially when accessing elements sequentially or in nested loops.
        /// </remarks>
        public Array2D(T[,] array)
        {
            width = array.GetLength(0);
            height = array.GetLength(1);
            this.array = new T[width * height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    this.array[x + y * width] = array[y, x]; // Swapped indices for better cache locality
                }
            }

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Array2D{T}"/> class with the specified size.
        /// </summary>
        /// <param name="size">The size of the array.</param>
        public Array2D(Vector2Int size) : this(size.x, size.y) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="Array2D{T}"/> class with the specified size and array.
        /// </summary>
        /// <param name="size">The size of the array.</param>
        /// <param name="mArray">The 1D array to initialize the 2D array with.</param>
        public Array2D(Vector2Int size, T[] mArray) : this(mArray, size.x, size.y) {}

        /// <summary>
        /// Converts the specified (x, y) coordinates to a 1D index.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <returns>The 1D index corresponding to the specified coordinates.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)] private int ToIndex(int x, int y) => x + y * width;

        /// <summary>
        /// Converts the specified position to a 1D index.
        /// </summary>
        /// <param name="pos">The position.</param>
        /// <returns>The 1D index corresponding to the specified position.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)] private int ToIndex(Vector2Int pos) => ToIndex(pos.x, pos.y);

        /// <summary>
        /// Gets or sets the element at the specified (x, y) coordinates.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <returns>The element at the specified coordinates.</returns>
        public T this[int x, int y]
        {
            get
            {
                if (SafeMode && ((uint)x >= (uint)width || (uint)y >= (uint)height))
                    throw new IndexOutOfRangeException($"Index ({x}, {y}) is out of bounds");
                return array[x + y * width];
            }
            set
            {
                if (SafeMode && ((uint)x >= (uint)width || (uint)y >= (uint)height))
                    throw new IndexOutOfRangeException($"Index ({x}, {y}) is out of bounds");
                array[x + y * width] = value;
            }
        }

        /// <summary>
        /// Gets or sets the element at the specified position.
        /// </summary>
        /// <param name="index">The position.</param>
        /// <returns>The element at the specified position.</returns>
        public T this[Vector2Int index]
        {
            get => this[index.x, index.y];
            set => this[index.x, index.y] = value;
        }

        /// <summary>
        /// Converts the 2D array to a 1D array.
        /// </summary>
        /// <returns>A 1D array containing all elements of the 2D array.</returns>
        public T[] ToArray() => array;

        /// <summary>
        /// Gets the first non-null element in the array.
        /// </summary>
        /// <returns>The first non-null element, or the default value if no non-null element is found.</returns>
        public T First()
        {
            if(isClass)
            {
                foreach (var item in array)
                    if (item != null)
                        return item;
                return default;
            }
            return array[0];
        }

        /// <summary>
        /// Gets the last non-null element in the array.
        /// </summary>
        /// <returns>The last non-null element, or the default value if no non-null element is found.</returns>
        public T Last()
        {
            if(isClass)
            {
                for (int i = array.Length - 1; i >= 0; i--)
                    if (array[i] != null)
                        return array[i];
                return default;
            }
            return array[^1];
        }

        /// <summary>
        /// Fills the array with the specified value.
        /// </summary>
        /// <param name="value">The value to fill the array with.</param>
        public void Fill(T value) => array.AsSpan().Fill(value);

        /// <summary>
        /// Gets the element at the specified (x, y) coordinates, or the default value if the coordinates are out of bounds.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <param name="defaultValue">The default value to return if the coordinates are out of bounds.</param>
        /// <returns>The element at the specified coordinates, or the default value if out of bounds.</returns>
        public T GetOrDefault(int x, int y, T defaultValue)
        {
            return IsInBounds(x, y) ? array[ToIndex(x, y)] : defaultValue;
        }

        /// <summary>
        /// Determines whether the specified (x, y) coordinates are within the bounds of the array.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <returns><c>true</c> if the coordinates are within bounds; otherwise, <c>false</c>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsInBounds(int x, int y) => (uint)x < (uint)width && (uint)y < (uint)height;

        /// <summary>
        /// Determines whether the specified position is within the bounds of the array.
        /// </summary>
        /// <param name="pos">The position.</param>
        /// <returns><c>true</c> if the position is within bounds; otherwise, <c>false</c>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsInBounds(Vector2Int pos) => IsInBounds(pos.x, pos.y);

        /// <summary>
        /// Gets the element at the specified (x, y) coordinates.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <returns>The element at the specified coordinates.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Get(int x, int y)
        {
            if (SafeMode && !IsInBounds(x,y))
                throw new IndexOutOfRangeException($"Index ({x}, {y}) is out of bounds");
            return array[ToIndex(x, y)];
        }

        /// <summary>
        /// Gets the element at the specified position.
        /// </summary>
        /// <param name="pos">The position.</param>
        /// <returns>The element at the specified position.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Get(Vector2Int pos) => Get(pos.x, pos.y);

        /// <summary>
        /// Sets the element at the specified (x, y) coordinates.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <param name="value">The value to set.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(int x, int y, T value)
        {
            if (SafeMode && !IsInBounds(x,y))
                throw new IndexOutOfRangeException($"Index ({x}, {y}) is out of bounds");
            array[ToIndex(x, y)] = value;
        }

        /// <summary>
        /// Sets the element at the specified position.
        /// </summary>
        /// <param name="pos">The position.</param>
        /// <param name="value">The value to set.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(Vector2Int pos, T value) => Set(pos.x, pos.y, value);
        
        /// <summary>
        /// Determines whether the element at the specified (x, y) coordinates exists and is not null.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <returns><c>true</c> if the element exists and is not null; otherwise, <c>false</c>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Exists(int x, int y) => IsInBounds(x, y) && array[ToIndex(x, y)] != null;
        
        /// <summary>
        /// Determines whether the element at the specified position exists and is not null.
        /// </summary>
        /// <param name="pos">The position.</param>
        /// <returns><c>true</c> if the element exists and is not null; otherwise, <c>false</c>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Exists(Vector2Int pos) => Exists(pos.x, pos.y);
        
        
        /// <summary>
        /// Determines whether any element in the array matches the conditions defined by the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate that defines the conditions of the elements to search for.</param>
        /// <returns><c>true</c> if one or more elements match the conditions defined by the specified predicate; otherwise, <c>false</c>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Exists(Predicate<T> predicate) => Array.Exists(array, predicate);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGet(int x, int y, out T value)
        {
            if (SafeMode && !IsInBounds(x,y))
            {
                value = default;
                return false;
            }
            value = array[ToIndex(x, y)];
            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGet(Vector2Int pos, out T value) => TryGet(pos.x, pos.y, out value);

        /// <summary>
        /// Executes the specified action on all elements in the array.
        /// </summary>
        /// <param name="action">The action to execute on each element..</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ForEach(Action<T> action)
        {
            if (action == null || array.Length == 0) return;
            
            for (int i = 0; i < array.Length; i++)
            {
                if(array[i] != null)
                    action(array[i]);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ForEachIndex(Action<Vector2Int, T> action)
        {
            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    action(new Vector2Int(x, y), array[ToIndex(x, y)]);
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ForEachIndex(Action<int, int, T> action)
        {
            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    action(x, y, array[ToIndex(x, y)]);
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ForEachIndex(Action<(int x, int y), T> action)
        {
            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    action((x, y), array[ToIndex(x, y)]);
                }
            }
        }

        /// <summary>
        /// Executes the specified action on each element in the array in parallel.
        /// </summary>
        /// <param name="action">The action to execute on each element with the index.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ParallelForEach(Action<T, int> action)
        {
            if(action == null || array.Length == 0) return;
            System.Threading.Tasks.Parallel.For(0, array.Length, i => action(array[i], i));
        }
        
        public (Vector2Int index, T value)[] GetAsArrayWithIndex()
        {
            var result = new (Vector2Int index, T value)[array.Length];
            int index = 0;
            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    result[index] = (new Vector2Int(x, y), array[ToIndex(x, y)]);
                    index++;
                }
            }
            
            return result;
        }
        
        public IEnumerable<(Vector2Int index, T value)> GetEnumerableWithIndex()
        {
            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    yield return (new Vector2Int(x, y), array[ToIndex(x, y)]);
                }
            }
        }


        /// <summary>
        /// Returns an enumerator that iterates through the array.
        /// </summary>
        /// <returns>An enumerator for the array.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<T> GetEnumerator() => new Array2DEnumerator(array);

        //public IEnumerator<T> GetEnumerator() => new Array2DEnumerator(array);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        /// <summary>
        /// Returns a span representing the 2D array.
        /// </summary>
        /// <returns>A span of the array.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Span<T> AsSpan() => array.AsSpan();


        /// <summary>
        /// Custom enumerator for the <see cref="Array2D{T}"/> class.
        /// </summary>
        private class Array2DEnumerator : IEnumerator<T>
        {
            private readonly T[] array;
            private int index = -1;

            /// <summary>
            /// Initializes a new instance of the <see cref="Array2DEnumerator"/> class with the specified array.
            /// </summary>
            /// <param name="array">The array to enumerate.</param>
            
            public Array2DEnumerator(T[] array) => this.array = array;

            /// <summary>
            /// Gets the current element in the array.
            /// </summary>
            public T Current => array[index];

            object IEnumerator.Current => Current;

            /// <summary>
            /// Advances the enumerator to the next element in the array.
            /// </summary>
            /// <returns><c>true</c> if the enumerator was successfully advanced to the next element; <c>false</c> if the enumerator has passed the end of the array.</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)] public bool MoveNext() => ++index < array.Length;

            /// <summary>
            /// Sets the enumerator to its initial position, which is before the first element in the array.
            /// </summary>
            [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Reset() => index = -1;

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Dispose() { }
        }
    }
}
