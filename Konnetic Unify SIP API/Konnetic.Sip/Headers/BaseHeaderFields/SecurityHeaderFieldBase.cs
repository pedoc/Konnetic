/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;

namespace Konnetic.Sip.Headers
{
    /// <summary> 
    /// The <see cref="T:Konnetic.Sip.Headers.SecurityHeaderFieldBase"/> provides the base class for all authentication/authorization header fields.
    /// </summary>
    /// <remarks>
    /// <b>Standards: RFC3261, RFC2616</b> 
    /// <para/>This base class act in little more capacity than as an indentifier for Security based HeaderFields. 
    /// <para/>The abstract base class is used by <see cref="T:Konnetic.Sip.Headers.SchemeAuthHeaderFieldBase"/> and <see cref="T:Konnetic.Sip.Headers.AuthenticationInfoHeaderField"/> headers.  
    /// <para> 
    /// <note type="implementnotes">Security header fields breaks the general rules about multiple HeaderField values. Although not a comma-separated list, this HeaderField name may be present multiple times, and must not be combined into a single header line using the usual rules. See <see cref="T:Konnetic.Sip.Headers.AuthHeaderFieldGroup{T}"/> for grouping of this HeaderField.</note> 
    /// <example>
    /// <list type="bullet">
    /// <item>Authentication-Info: nextnonce="47364c23432d2e131a5fb210812c"</item>
    /// <item>WWW-Authenticate: Digest realm="atlanta.com", domain="sip:boxesbybob.com", qop="auth", nonce="f84f1cec41e6cbe5aea9c8e88d359", opaque="", algorithm=MD5</item>  
    /// </list>  
    /// </example>
    /// </remarks> 
    /// <seealso cref="T:Konnetic.Sip.Headers.AuthenticationInfoHeaderField"/> 
    /// <seealso cref="T:Konnetic.Sip.Headers.SchemeAuthHeaderFieldBase"/>
    public abstract class SecurityHeaderFieldBase : ParamatizedHeaderFieldBase
    {
        #region Fields

        private const string SEPERATOR = ", ";

        #endregion Fields

        #region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="SecurityHeaderFieldBase"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks> 
        protected SecurityHeaderFieldBase()
            : base(SEPERATOR)
        {
        IncludeLeadingSeperatorInOutput = false;
        }

        #endregion Constructors

        #region Methods
        /// <summary>
        /// Validates this instance against the standard. Indicated whether it reaches minimum compliance.
        /// </summary>
        /// <remarks>This member overrides the <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/> instance.</remarks>
        /// <returns>
        /// 	<c>true</c> if instance represents a valid HeaderField; otherwise, <c>false</c>.
        /// </returns>
        /// <threadsafety static="true" instance="false" />
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public override bool IsValid()
        {
            return base.IsValid() && HeaderParameters.Count > 0;
        }
        /// <summary>
        /// Parses string representation of the HeaderField.
        /// </summary>
        /// <remarks>  
        /// </remarks>
        /// <param name="value">The HeaderField string to parse.</param>
        /// <exception cref="T:Konnetic.Sip.SipParseException">Thrown when an invalid (non-standard) value is encountered.</exception>
        /// <exception cref="T:Konnetic.Sip.SipException">Thrown when a processing exception is encountered.</exception>
        /// <threadsafety static="true" instance="false" />
        public override void Parse(string value)
        {
            if(value != null)
                {
                if(!string.IsNullOrEmpty(value))
                    {
                    string s = value;
                    Regex _headerReplace = new Regex(@"(?<=^\s*)[\w""-.!%_*+`'~]+(?=\s+[\w-.!%_*+`'~])", RegexOptions.Compiled | RegexOptions.IgnoreCase );
                    s = _headerReplace.Replace(s, string.Empty);
                    //Add a comma to start as the ParamatizedHeaderField Parse method is expecting it.
                    value = SEPERATOR + s.TrimStart();
                    }
                    base.Parse(value);
                }
        }
        ///// <summary>
        ///// Gets a string representation of the HeaderField value. The HeaderField name is excluded.
        ///// </summary>
        ///// <remarks>This method overrides the <c>GetStringValue</c> method in <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>. </remarks>
        ///// <returns>String representation of the HeaderField value</returns>
        ///// <threadsafety static="true" instance="false" />
        //protected override string GetStringValueNoParams()
        //    {
        //    StringBuilder sb = new StringBuilder();
        //    string s = base.GetStringValue();
        //    if(!string.IsNullOrEmpty(s))
        //        {
        //        //Remove a comma from start as the ParamatizedHeaderField adds it.
        //        s = s.TrimStart(SEPERATOR.ToCharArray());
        //        sb.Append(s);
        //        }
        //    return sb.ToString();
        //    }

        #endregion Methods
    }
}