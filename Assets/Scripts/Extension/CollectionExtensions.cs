using System;
using System.Collections.Generic;

namespace Extensions {
    public static class CollectionExtensions {
        /// <summary>
        /// 打乱数组
        /// 洗牌算法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Shuffle<T>(this IList<T> list) {
            if (list == null)
                return;

            Random _rand = new Random(DateTime.Now.Millisecond);
            int n = list.Count;
            while (n > 1) {
                n--;
                var k = _rand.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static T TryGetAt<T>(this IList<T> list, int index) {
            if (list == null || list.Count < index)
                return default;
            return list[index];
        }

    }
}