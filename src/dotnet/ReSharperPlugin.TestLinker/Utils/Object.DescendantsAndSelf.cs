// Copyright Matthias Koch 2017.
// Distributed under the MIT License.
// https://github.com/matkoch/Nuke/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JetBrains.Annotations;

namespace ReSharperPlugin.TestLinker.Utils
{
    public static class EnumerableExtensions
    {
        [DebuggerNonUserCode]
        [DebuggerStepThrough]
        [DebuggerHidden]
        public static IEnumerable<T> DescendantsAndSelf<T> (
            this T obj,
            Func<T, T> selector,
            [CanBeNull] Func<T, bool> traverse = null)
        {
            yield return obj;

            foreach (var p in obj.Descendants(selector, traverse))
                yield return p;
        }

        [DebuggerNonUserCode]
        [DebuggerStepThrough]
        [DebuggerHidden]
        public static IEnumerable<T> Descendants<T> (
            this T obj,
            Func<T, T> selector,
            [CanBeNull] Func<T, bool> traverse = null)
        {
            if (traverse != null && !traverse(obj))
                yield break;

            var next = selector(obj);
            if (traverse == null && Equals(next, default(T)))
                yield break;

            foreach (var nextOrDescendant in next.DescendantsAndSelf(selector, traverse))
                yield return nextOrDescendant;
        }

        [DebuggerNonUserCode]
        [DebuggerStepThrough]
        [DebuggerHidden]
        public static IEnumerable<T> DescendantsAndSelf<T> (
            this T obj,
            Func<T, IEnumerable<T>> selector,
            [CanBeNull] Func<T, bool> traverse = null)
        {
            yield return obj;

            foreach (var p in Descendants(obj, selector, traverse))
                yield return p;
        }

        [DebuggerNonUserCode]
        [DebuggerStepThrough]
        [DebuggerHidden]
        public static IEnumerable<T> Descendants<T> (
            this T obj,
            Func<T, IEnumerable<T>> selector,
            [CanBeNull] Func<T, bool> traverse = null)
        {
            foreach (var child in selector(obj).Where(x => traverse == null || traverse(x)))
            foreach (var childOrDescendant in child.DescendantsAndSelf(selector, traverse))
                yield return childOrDescendant;
        }
    }
}
