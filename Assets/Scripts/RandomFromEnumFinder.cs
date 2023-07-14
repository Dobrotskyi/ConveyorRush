using System;

public static class RandomFromEnumFinder
{
    public static Enum GetRandomFromEnum<T>() where T : Enum
    {
        Random random = new Random();

        Type type = typeof(T);
        Array values = type.GetEnumValues();
        int index = random.Next(values.Length);
        T value = (T)values.GetValue(index);
        return value;
    }
}
