using System.ComponentModel;

namespace BPLISBN.Extensions
{
    public static class EnumExtensions
    {
        public static string GetEnumDescription(this Enum? enumValue)
        {
            var fieldInfo = enumValue?.GetType().GetField(enumValue.ToString());
            DescriptionAttribute[] descriptionAttributes = Array.Empty<DescriptionAttribute>();

            if (fieldInfo != null)
            {
                descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            }

            return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumValue.ToString();
        }
    }
}
