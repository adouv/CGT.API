using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CGT.DDD.Extension
{
    /// <summary>
    /// IEnumerable扩展类
    /// </summary>
    public static class IEnumerableExtension
    {
        public static string Join(this IEnumerable<string> source, string separator = null)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (source.Any())
            {
                separator = separator ?? string.Empty;
                if (source.Count() > 10)
                {
                    var build = new StringBuilder();
                    var etor = source.GetEnumerator();
                    var count = source.Count();
                    var pos = 1;
                    while (etor.MoveNext())
                    {
                        build.Append(etor.Current);
                        if (pos < count)
                        {
                            build.Append(separator);
                        }
                        pos++;
                    }
                    return build.ToString();
                }
                return source.Aggregate((x, y) => x + separator + y);
            }
            return string.Empty;
        }
        public static string Join<TSource>(this IEnumerable<TSource> source, string separator, Func<TSource, string> map)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (map == null)
                throw new ArgumentNullException("map");

            return source.Any() ? Join(source.Select(map), separator) : string.Empty;
        }

        public static void Foreach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (action == null)
                throw new ArgumentNullException("action");

            foreach (var item in source)
            {
                action(item);
            }
        }
        public static void ForeachWhile<TSource>(this IEnumerable<TSource> source, Action<TSource> action, Func<TSource, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (action == null)
                throw new ArgumentNullException("action");

            if (predicate == null)
            {
                Foreach(source, action);
            }
            else
            {
                foreach (var item in source)
                {
                    if (!predicate(item))
                        return;
                    action(item);
                }
            }
        }
        /// <summary>
        /// 循环测试指定条件，并在满足时为序列中的元素执行指定操作，否则退出循环。
        /// </summary>
        /// <typeparam name="TSource">序列中的元素的类型。</typeparam>
        /// <param name="sequence">要为元素执行操作的序列。</param>
        /// <param name="action">要为元素执行的操作。</param>
        /// <param name="predicate">测试条件。</param>
        public static void ForeachWhile<TSource>(this IEnumerable<TSource> sequence, Action<TSource, int> action, Func<TSource, int, bool> predicate = null)
        {
            if (sequence == null)
                throw new ArgumentNullException("sequence");
            if (action != null)
            {
                if (predicate == null)
                {
                    sequence.For(action);
                }
                else
                {
                    var array = sequence.ToArray();
                    for (var i = 0; i < array.Length; i++)
                    {
                        if (predicate(array[i], i))
                        {
                            action(array[i], i);
                            continue;
                        }
                        break;
                    }
                }
            }
        }
        public static void ForeachWhen<TSource>(this IEnumerable<TSource> source, Action<TSource> action, Func<TSource, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (action == null)
                throw new ArgumentNullException("action");
            if (predicate == null)
                throw new ArgumentNullException("predicate");

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    action(item);
                }
            }
        }
        public static void ForeachExcept<TSource>(this IEnumerable<TSource> source, Action<TSource> action, Func<TSource, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (action == null)
                throw new ArgumentNullException("action");

            if (predicate == null)
            {
                Foreach(source, action);
            }
            else
            {
                foreach (var item in source)
                {
                    if (!predicate(item))
                    {
                        action(item);
                    }
                }
            }
        }

        public static TSource MinElement<TSource, TValue>(this IEnumerable<TSource> source, Func<TSource, TValue> selector, IComparer<TValue> comparer = null)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (source.Any())
                return MinOrDefaultElement(source, selector, comparer);
            throw new InvalidOperationException("序列 source 中不包含任何元素");
        }
        public static TSource MinOrDefaultElement<TSource, TValue>(this IEnumerable<TSource> source, Func<TSource, TValue> selector, IComparer<TValue> comparer = null)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (selector == null)
                throw new ArgumentNullException("selector");

            var minElement = default(TSource);
            var minValue = default(TValue);
            comparer = comparer ?? Comparer<TValue>.Default;
            var hasValue = false;
            foreach (var item in source)
            {
                var curValue = selector(item);
                if (hasValue)
                {
                    if (comparer.Compare(curValue, minValue) < 0)
                    {
                        minValue = curValue;
                        minElement = item;
                    }
                }
                else
                {
                    minValue = curValue;
                    minElement = item;
                    hasValue = true;
                }
            }
            return minElement;
        }
        public static TSource MaxElement<TSource, TValue>(this IEnumerable<TSource> source, Func<TSource, TValue> selector, IComparer<TValue> comparer = null)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (source.Any())
                return MaxOrDefaultElement(source, selector, comparer);
            throw new InvalidOperationException("序列 source 中不包含任何元素");
        }
        public static TSource MaxOrDefaultElement<TSource, TValue>(this IEnumerable<TSource> source, Func<TSource, TValue> selector, IComparer<TValue> comparer = null)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (selector == null)
                throw new ArgumentNullException("selector");

            var maxElement = default(TSource);
            var maxValue = default(TValue);
            comparer = comparer ?? Comparer<TValue>.Default;
            var hasValue = false;
            foreach (var item in source)
            {
                var curValue = selector(item);
                if (hasValue)
                {
                    if (comparer.Compare(curValue, maxValue) > 0)
                    {
                        maxValue = curValue;
                        maxElement = item;
                    }
                }
                else
                {
                    maxValue = curValue;
                    maxElement = item;
                    hasValue = true;
                }
            }
            return maxElement;
        }

        public static TSource MinElement<TSource>(this IEnumerable<TSource> source, IComparer<TSource> comparer = null)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (source.Any())
                return MinOrDefaultElement(source, comparer);
            throw new InvalidOperationException("序列 source 中不包含任何元素");
        }
        public static TSource MinOrDefaultElement<TSource>(this IEnumerable<TSource> source, IComparer<TSource> comparer = null)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            var minElement = default(TSource);
            comparer = comparer ?? Comparer<TSource>.Default;
            var hasValue = false;
            foreach (var item in source)
            {
                if (hasValue)
                {
                    if (comparer.Compare(item, minElement) < 0)
                    {
                        minElement = item;
                    }
                }
                else
                {
                    minElement = item;
                    hasValue = true;
                }
            }
            return minElement;
        }
        public static TSource MaxElement<TSource>(this IEnumerable<TSource> source, IComparer<TSource> comparer = null)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (source.Any())
                return MaxOrDefaultElement(source, comparer);
            throw new InvalidOperationException("序列 source 中不包含任何元素");
        }
        public static TSource MaxOrDefaultElement<TSource>(this IEnumerable<TSource> source, IComparer<TSource> comparer = null)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            var maxElement = default(TSource);
            comparer = comparer ?? Comparer<TSource>.Default;
            var hasValue = false;
            foreach (var item in source)
            {
                if (hasValue)
                {
                    if (comparer.Compare(item, maxElement) > 0)
                    {
                        maxElement = item;
                    }
                }
                else
                {
                    maxElement = item;
                    hasValue = true;
                }
            }
            return maxElement;
        }

        public static IEnumerable<TSource> MaxElements<TSource, TValue>(this IEnumerable<TSource> source, Func<TSource, TValue> selector, IComparer<TValue> comparer = null)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (selector == null)
                throw new ArgumentNullException("selector");

            if (source.Any())
            {
                comparer = comparer ?? Comparer<TValue>.Default;
                var maxElement = source.First();
                var maxValue = selector(maxElement);
                var result = new List<TSource> { maxElement };
                var skip = true;
                foreach (var item in source)
                {
                    if (skip)
                    {
                        skip = false;
                        continue;
                    }
                    var curValue = selector(item);
                    var compareResult = comparer.Compare(curValue, maxValue);
                    if (compareResult > 0)
                    {
                        result.Clear();
                        result.Add(item);
                        maxValue = curValue;
                    }
                    else if (compareResult == 0)
                    {
                        result.Add(item);
                    }
                }
                return result;
            }
            return Enumerable.Empty<TSource>();
        }
        public static IEnumerable<TSource> MinElements<TSource, TValue>(this IEnumerable<TSource> source, Func<TSource, TValue> selector, IComparer<TValue> comparer = null)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (selector == null)
                throw new ArgumentNullException("selector");

            if (source.Any())
            {
                comparer = comparer ?? Comparer<TValue>.Default;
                var minElement = source.First();
                var minValue = selector(minElement);
                var result = new List<TSource> { minElement };
                var skip = true;
                foreach (var item in source)
                {
                    if (skip)
                    {
                        skip = false;
                        continue;
                    }
                    var curValue = selector(item);
                    var compareResult = comparer.Compare(curValue, minValue);
                    if (compareResult < 0)
                    {
                        result.Clear();
                        result.Add(item);
                        minValue = curValue;
                    }
                    else if (compareResult == 0)
                    {
                        result.Add(item);
                    }
                }
                return result;
            }
            return Enumerable.Empty<TSource>();
        }

        public static IEnumerable<TSource> SelectElements<TSource>(this IEnumerable<IEnumerable<TSource>> source, Func<TSource, bool> predicate = null)
        {
            return (from sitem in source
                    from item in sitem
                    where predicate == null || predicate(item)
                    select item).ToList();
        }

        public static IEnumerable<TSource> RemoveNullElements<TSource>(this IEnumerable<TSource> source) where TSource : class
        {
            return source.Where(item => item != null).ToList();
        }
        public static IEnumerable<string> RemoveNullOrEmptyElements(this IEnumerable<string> source)
        {
            return source.Where(item => !string.IsNullOrEmpty(item)).ToList();
        }
        public static IEnumerable<string> RemoveNullOrWhiteSpaceElements(this IEnumerable<string> source)
        {
            return source.Where(item => !string.IsNullOrWhiteSpace(item)).ToList();
        }

        /// <summary>
        /// 为当前序列中的每一个元素执行指定的操作。
        /// </summary>
        /// <typeparam name="TSource">序列中元素的类型。</typeparam>
        /// <param name="sequence">要为元素执行操作的序列。</param>
        /// <param name="action">要为元素执行的操作。</param>
        public static void For<TSource>(this IEnumerable<TSource> sequence, Action<TSource, int> action)
        {
            if (sequence == null)
                throw new ArgumentNullException("sequence");
            if (action != null)
            {
                var array = sequence.ToArray();
                for (var i = 0; i < array.Length; i++)
                {
                    action(array[i], i);
                }
            }
        }
    }
}
