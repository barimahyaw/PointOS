using PointOS.Common.Attributes;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace PointOS.Common.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the enum from string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <remarks>Animal giantPanda = EnumHelper.GetEnumFromString<Animal/>("Giant Panda")</remarks>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">The value ' + value + ' does not match a valid enum name or description.</exception>
        public static T GetEnumFromString<T>(string value)
        {
            if (Enum.IsDefined(typeof(T), value))
            {
                return (T)Enum.Parse(typeof(T), value, true);
            }
            else
            {
                var enumNames = Enum.GetNames(typeof(T));
                foreach (var enumName in enumNames)
                {
                    var e = Enum.Parse(typeof(T), enumName);
                    if (value == ((Enum)e).GetAttributeDescription()) // GetDescription((Enum)e))
                    {
                        return (T)e;
                    }
                }
            }
            throw new ArgumentException("The value '" + value + "' does not match a valid enum name or description.");
        }

        /// <summary>
        /// Gets an attribute on an enum field value
        /// </summary>
        /// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
        /// <param name="enumVal">The enum value</param>
        /// <remarks>Usage would then be: private string desc = myEnumVariable.GetAttributeOfType<DescriptionAttribute/>().Description</remarks>
        /// <returns>The attribute of type T that exists on the enum value</returns>
        public static T GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return attributes.Length > 0 ? (T)attributes[0] : null;
        }

        /// <summary>
        /// This extension method is broken out so you can use a similar pattern with 
        /// other MetaData elements in the future. This is your base method for each.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);

            // check if no attributes have been specified.
            if (attributes.Length > 0)
            {
                return (T)attributes[0];
            }

            return null;
        }

        /// <summary>
        /// Gets an attribute on an enum field value
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
        /// <param name="value">The enum value</param>
        /// <returns>
        /// The attribute of type T that exists on the enum value
        /// </returns>
        private static T GetAttributeOfType<TEnum, T>(this TEnum value)
            where TEnum : struct, IConvertible
            where T : Attribute
        {

            return value.GetType()
                        .GetMember(value.ToString())
                        .First()
                        .GetCustomAttributes(false)
                        .OfType<T>()
                        .LastOrDefault();
        }



        /// <summary>
        /// Gets the description attribute of an enum field value.
        /// </summary>
        /// <param name="enumValue">The enum value.</param>
        /// <returns>The description of the enum field value</returns>
        public static string GetAttributeDescription(this Enum enumValue)
        {
            var attribute = enumValue.GetAttributeOfType<DescriptionAttribute>();

            return attribute == null ? string.Empty : attribute.Description;
        }

        /// <summary>
        /// Gets the string value attribute of an enum field value.
        /// </summary>
        /// <param name="enumValue">The enum value.</param>
        /// <returns>The string value of the enum field value</returns>
        public static string GetAttributeStringValue(this Enum enumValue)
        {
            var attribute = enumValue.GetAttributeOfType<StringValueAttribute>();

            return attribute == null ? string.Empty : attribute.StringValue;
        }

        /// <summary>
        /// Gets the integer value attribute of an enum field value.
        /// </summary>
        /// <param name="enumValue">The enum value.</param>
        /// <returns>The integer value of the enum field value</returns>
        public static int GetAttributeIntegerValue(this Enum enumValue)
        {
            var attribute = enumValue.GetAttributeOfType<IntegerValueAttribute>();

            return attribute?.IntegerValue ?? 0;
        }

        ///// <summary>
        ///// Gets the table name attribute of an enum field value.
        ///// </summary>
        ///// <param name="enumValue">The enum value.</param>
        ///// <returns>The table name of the enum field value</returns>
        //public static string GetAttributeTableName(this Enum enumValue)
        //{
        //    var attribute = enumValue.GetAttributeOfType<TableNameAttribute>();

        //    return attribute == null ? String.Empty : attribute.TableName;
        //}

        ///// <summary>
        ///// Gets the table rowGuid name attribute of an enum field value.
        ///// </summary>
        ///// <param name="enumValue">The enum value.</param>
        ///// <returns>The table rowGuid of the enum field value</returns>
        //public static string GetAttributeTableRowGuidFieldName(this Enum enumValue)
        //{
        //    var attribute = enumValue.GetAttributeOfType<TableRowGuidFieldNameAttribute>();

        //    return attribute == null ? String.Empty : attribute.TableRowGuidFieldName;
        //}

        ///// <summary>
        ///// Gets the table rowid name attribute of an enum field value.
        ///// </summary>
        ///// <param name="enumValue">The enum value.</param>
        ///// <returns>The table rowid of the enum field value</returns>
        //public static string GetAttributeTableRowIdFieldName(this Enum enumValue)
        //{
        //    var attribute = enumValue.GetAttributeOfType<TableRowIdFieldNameAttribute>();

        //    return attribute == null ? String.Empty : attribute.TableRowIdFieldName;
        //}

        /// <summary>
        /// This method creates a specific call to the method (GetAttributeOfType), requesting the
        /// Description MetaData attribute.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ToName(this Enum value)
        {
            var attribute = value.GetAttribute<DescriptionAttribute>();
            return attribute == null ? value.ToString() : attribute.Description;
        }

        /// <summary>
        /// Find the enum from the description attribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="desc"></param>
        /// <returns></returns>
        public static T FromName<T>(this string desc) where T : struct
        {
            var found = false;
            var result = (T)Enum.GetValues(typeof(T)).GetValue(0);

            foreach (var enumVal in Enum.GetValues(typeof(T)))
            {
                var attr = ((Enum)enumVal).ToName();

                if (attr != desc) continue;
                result = (T)enumVal;
                found = true;
                break;
            }

            if (!found)
            {
                throw new Exception();
            }

            return result;
        }

        /// <summary>
        /// Gets the StringValue attribute of the enum value 
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ToStringValue(this Enum value)
        {
            var attribute = value.GetAttribute<StringValueAttribute>();
            return attribute == null ? value.ToString() : attribute.StringValue;
        }

        /// <summary>
        /// Find the enum from the string value attribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T FromStringValue<T>(this string value) where T : struct
        {
            string attr;
            Boolean found = false;
            T result = (T)Enum.GetValues(typeof(T)).GetValue(0);

            foreach (object enumVal in Enum.GetValues(typeof(T)))
            {
                attr = ((Enum)enumVal).ToStringValue();

                if (attr == value)
                {
                    result = (T)enumVal;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                throw new Exception();
            }

            return result;
        }

        ///// <summary>
        ///// Get the Name value of the Display attribute if the   
        ///// enum has one, otherwise use the value converted to title case.
        ///// </summary>
        ///// <typeparam name="TEnum">The type of the enum.</typeparam>
        ///// <param name="value">The value.</param>
        ///// <returns></returns>
        //public static string GetDisplayName<TEnum>(this TEnum value)
        //    where TEnum : struct, IConvertible
        //{
        //    var attr = value.GetAttributeOfType<TEnum, DisplayAttribute>();
        //    return attr == null ? value.ToString().ToSpacedTitleCase() : attr.Name;
        //}

        ///// <summary>
        ///// Get the ShortName value of the Display attribute if the   
        ///// enum has one, otherwise use the value converted to title case.
        ///// </summary>
        ///// <typeparam name="TEnum">The type of the enum.</typeparam>
        ///// <param name="value">The value.</param>
        ///// <returns></returns>
        //public static string GetDisplayShortName<TEnum>(this TEnum value)
        //    where TEnum : struct, IConvertible
        //{
        //    var attr = value.GetAttributeOfType<TEnum, DisplayAttribute>();
        //    return attr == null ? value.ToString().ToSpacedTitleCase() : attr.ShortName;
        //}



        /// <summary>
        /// Converts camel case or pascal case to separate words with title case
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToSpacedTitleCase(this string s)
        {
            //http://stackoverflow.com/a/155486/150342
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            return textInfo.ToTitleCase(Regex.Replace(s, "([a-z](?=[A-Z0-9])|[A-Z](?=[A-Z][a-z]))", "$1 "));
        }
    }
}
