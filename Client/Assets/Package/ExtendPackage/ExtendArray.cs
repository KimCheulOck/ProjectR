using System;

public static class ExtendArray
{
    //public static object Find(this object[] array, Predicate<object> match)
    //{
    //    return Array.Find(array, match);
    //}

    public static T Find<T>(this T[] array, Predicate<T> match)
    {
        return Array.Find(array, match);
    }

    public static T[] FindAll<T>(this T[] array, Predicate<T> match)
    {
        return Array.FindAll(array, match);
    }
}
