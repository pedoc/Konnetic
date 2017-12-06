/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;

namespace Konnetic.Sip.Headers
{
    internal class HeaderFieldReader
    {
        #region Fields

    /// <summary>
    /// 
    /// </summary>
        private string _originalString;
        /// <summary>
        /// 
        /// </summary>
        private string _tempString;

        #endregion Fields

        #region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="HeaderFieldReader"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <overloads>
		/// <summary>The method has one overload.</summary>
		/// </overloads>
        public HeaderFieldReader(string source)
        {
            PropertyVerifier.ThrowOnNullArgument(source,"source");
            _originalString = source;
        }

        #endregion Constructors
    }
}