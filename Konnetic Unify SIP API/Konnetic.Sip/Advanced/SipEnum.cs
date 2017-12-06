/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.ComponentModel;
using System.Reflection;

namespace Konnetic.Sip
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal static class Enum<T>
    {
        #region Methods

        /// <summary>
        /// Descriptions the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        internal static string Description(T value)
        {
            DescriptionAttribute[] da = (DescriptionAttribute[])(typeof(T).GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false));
            return da.Length > 0 ? da[0].Description : value.ToString();
        }

        /// <summary>
        /// Values the of.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        internal static T ValueOf(string text)
        {
            string[] names = Enum.GetNames(typeof(T));
            foreach(string name in names)
                {
                if(Description((T)Enum.Parse(typeof(T), name)).Equals(text.ToUpperInvariant()))
                    {
                    return (T)Enum.Parse(typeof(T), name);
                    }
                }
            return (T)(Enum.GetValues(typeof(T)).GetValue(0));
        }

        #endregion Methods
    }
}