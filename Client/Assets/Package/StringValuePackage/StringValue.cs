using System;
using System.Reflection;

public class StringValue : Attribute
{
    public StringValue(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }

    public static string GetString(Enum enumValue)
    {
        Type type = enumValue.GetType();
        FieldInfo fieldInfo = type.GetField(enumValue.ToString());
        StringValue[] stringValues = fieldInfo.GetCustomAttributes(typeof(StringValue), false) as StringValue[];

        if (stringValues != null && stringValues.Length > 0)
            return stringValues[0].Value;

        return string.Empty;
    }
}