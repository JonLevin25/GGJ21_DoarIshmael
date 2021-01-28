using System;

namespace Utils
{
    public static class RandomUtils
    {
        private static readonly Random _random = new Random();
        
        public static T Choice<T>(this T[] items)
        {
            if (items.Length == 0)
            {
                return default(T);
            }
            return items[_random.Next(0, items.Length)];
        }
        public static T ChoiceFrom<T>(params T[] items)
        {
            return items.Choice();
        }
    }
}