namespace Useful.Extensions;

public static class EnumExtensions
{
    public static string GetDescription<TEnum>(this TEnum value) where TEnum : Enum
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();

        return attribute == null ? string.Empty : attribute.Description;
    }
}