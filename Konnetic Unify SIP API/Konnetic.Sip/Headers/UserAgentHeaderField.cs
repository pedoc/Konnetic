﻿/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using Konnetic.Sip.Messages;

namespace Konnetic.Sip.Headers
    {
    /// <summary>The User-Agent HeaderField contains information about the client originating the request. 
    /// </summary>
    /// <remarks> 
    /// <b>Standards: RFC3261, RFC2616</b>
    /// <para/>
    /// The User-Agent request-HeaderField contains information about the user agent originating the request. This is for statistical purposes, the tracing of protocol violations, and automated recognition of user agents for the sake of tailoring responses to avoid particular user agent limitations. User agents should include this field with requests. The field can contain multiple product tokens (section 3.8) and comments identifying the agent and any subproducts which form a significant part of the user agent. By convention, the product tokens are listed in order of their significance for identifying the application.
    /// <para/>
    /// Revealing the specific software version of the user agent might allow the user agent to become more vulnerable to attacks against software that is known to contain security holes. Implementers should make the User-Agent HeaderField a configurable option.
    /// <para/>
    /// <b>RFC 3261 Syntax:</b> 
    /// <table >
    /// <tr><td colspan="2" style="border-bottom:none">"User-Agent" ":" server-val *(LWS server-val)</td></tr> 
    /// <tr><td style="border-bottom:none">server-val = </td><td style="border-bottom:none">token [SLASH token] / comment</td></tr>
    /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr>
    /// <tr><td style="border-bottom:none">comment = </td><td style="border-bottom:none">&lt; *(ctext / quoted-pair / comment) &gt;</td></tr>
    /// <tr><td style="border-bottom:none">ctext = </td><td style="border-bottom:none">%x21-27 / %x2A-5B / %x5D-7E / UTF8-NONASCII / LWS</td></tr>
    /// <tr><td style="border-bottom:none">quoted-string = </td><td style="border-bottom:none">SWS DOUBLE_QUOTE *(qdtext / quoted-pair ) DOUBLE_QUOTE</td></tr>
    /// <tr><td style="border-bottom:none">qdtext = </td><td style="border-bottom:none">LWS / %x21 / %x23-5B / %x5D-7E / UTF8-NONASCII</td></tr>
    /// <tr><td style="border-bottom:none">LWS = </td><td style="border-bottom:none">[*WSP CRLF] 1*WSP ; linear whitespace</td></tr>
    /// <tr><td style="border-bottom:none">SWS = </td><td style="border-bottom:none">[LWS] ; sep whitespace</td></tr>
    /// </table>  
    /// <para/>
    /// <note type="implementnotes">This HeaderField allows HeaderField grouping (<see cref="T:Konnetic.Sip.Headers.HeaderFieldGroup{T}"/>).</note>
    /// <example>
    /// <list type="bullet">
    /// <item>User-Agent: Softphone Beta1.5</item>  
    /// </list> 
    /// </example>
    /// </remarks> 
    /// <seealso cref="T:Konnetic.Sip.Headers.MediaTypeHeaderFieldBase"/>
    public sealed class UserAgentHeaderField : ServerValueHeaderFieldBase
    {
        #region Fields

        internal const string CompareName = "USER-AGENT";
        internal const string CompareShortName = CompareName;
        internal const string LongName = "User-Agent";

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAgentHeaderField"/> class.
        /// </summary>
        /// <param name="comment">The comment.</param>
        public UserAgentHeaderField(string comment)
            : base(comment)
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAgentHeaderField"/> class.
        /// </summary>
        /// <param name="productName">Name of the product.</param>
        /// <param name="productVersion">The product version.</param>
        public UserAgentHeaderField(string productName, string productVersion)
            : base(productName, productVersion)
        {
            Init();
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="UserAgentHeaderField"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <overloads>
		/// <summary>The method has two overloads.</summary>
		/// </overloads>
        public UserAgentHeaderField()
            : base(string.Empty,string.Empty,string.Empty)
        {
            Init();
        }

        #endregion Constructors

        #region Methods
		/// <summary>
		/// Performs an implicit conversion from <see cref="System.String"/> to <see cref="Konnetic.Sip.Headers.UserAgentHeaderField"/>.
		/// </summary>
		/// <param name="value">The string value representing the HeaderField.</param>
        /// <returns>A new <see cref="Konnetic.Sip.Headers.UserAgentHeaderField"/> populated from the <paramref name="value"/> parameter.</returns>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="value"/>.</exception>
        public static implicit operator UserAgentHeaderField(String value)
        {
            PropertyVerifier.ThrowOnNullArgument(value,"value");

            UserAgentHeaderField hf = new UserAgentHeaderField();
            hf.Parse(value);
            return hf;
        }
		/// <summary>
		/// Performs an explicit conversion from <see cref="Konnetic.Sip.Headers.UserAgentHeaderField"/> to <see cref="System.String"/>.
		/// </summary>
		/// <param name="headerField">The HeaderField to convert.</param>
		/// <returns>A string representation of the HeaderField.</returns>
		/// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="headerField"/>.</exception>
        public static explicit operator string(UserAgentHeaderField headerField)
        {
            PropertyVerifier.ThrowOnNullArgument(headerField, "headerField");
            return headerField.ToString();
        }
        /// <summary>
        /// Creates a deep copy of this instance.
        /// </summary> 
        /// <remarks>Creates and returns a deep copy of the Header. This method ensures a deep copy of the HeaderField, when a message is cloned the HeaderField can be modified without effecting the original HeaderField in the message.</remarks>
        /// <returns>A deep copy of <see cref="T:Konnetic.Sip.Headers.UserAgentHeaderField"/>.</returns>
        /// <threadsafety static="true" instance="false" />
public override HeaderFieldBase Clone()
        {
            UserAgentHeaderField newObj = new UserAgentHeaderField(ProductName,ProductVersion);
            newObj.Comment = Comment;
            return newObj;
        }
/// <summary>Compare this SIP Header for equality with the base <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>.
/// </summary>
/// <remarks>This method overrides the <c>equals</c> method in <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>. 
/// All optional headers are compared using object equality that is each field in the header is used for comparision. When comparing HeaderFields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular HeaderField, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are case-sensitive. 
/// </remarks>
/// <param name="other">The <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/> to compare to this instance.</param>
/// <returns><c>true</c> if <paramref name="other" /> is an instance of this class representing the same SIP HeaderField as this, <c>false</c> otherwise.</returns>
/// <overloads>
/// <summary>This method is overloaded.</summary>
/// <remarks>Overloads allow for equality against <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>, <see cref="T:System.Object"/> and another instance of this HeaderField.</remarks>
/// </overloads>  
/// <threadsafety static="true" instance="false" /> 
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public override bool Equals(HeaderFieldBase other)
        {
            return Equals((object)other);
        }
        /// <summary>Compare this SIP Header for equality with an instance of <see cref="T:System.Object"/>.
        /// </summary>
        /// <remarks>This method overrides the <c>equals</c> method in <see cref="System.Object"/>. 
        /// All optional headers are compared using object equality that is each field in the header is used for comparision. When comparing HeaderFields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular HeaderField, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are case-sensitive. 
        /// </remarks>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare to this instance.</param>
        /// <returns><c>true</c> if <paramref name="obj" /> is an instance of this class representing the same SIP HeaderField as this, <c>false</c> otherwise. </returns>        
        /// <overloads>
        /// <summary>This method is overloaded.</summary>
        /// <remarks>Overloads allow for equality against <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>, <see cref="T:System.Object"/> and another instance of this HeaderField.</remarks>
        /// </overloads> 
        /// <threadsafety static="true" instance="false" />
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public override bool Equals(object obj)
        {
            if(obj == null)
                {
                return false;
                }

            UserAgentHeaderField p1 = obj as UserAgentHeaderField;
            if((object)p1 == null)
                {
                HeaderFieldGroup<UserAgentHeaderField> p = obj as HeaderFieldGroup<UserAgentHeaderField>;
                if((object)p == null)
                    {
                    return false;
                    }
                else
                    {
                    return p.Equals(this);
                    }
                }
            else
                {
                return this.Equals(p1);
                }
        }
        /// <summary>
        /// Parses string representation of the HeaderField.
        /// </summary>
        /// <remarks> 
        /// <b>RFC 3261 Syntax:</b> 
        /// <table >
        /// <tr><td colspan="2" style="border-bottom:none">"User-Agent" ":" server-val *(LWS server-val)</td></tr> 
        /// <tr><td style="border-bottom:none">server-val = </td><td style="border-bottom:none">token [SLASH token] / comment</td></tr>
        /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr>
        /// <tr><td style="border-bottom:none">comment = </td><td style="border-bottom:none">&lt; *(ctext / quoted-pair / comment) &gt;</td></tr>
        /// <tr><td style="border-bottom:none">ctext = </td><td style="border-bottom:none">%x21-27 / %x2A-5B / %x5D-7E / UTF8-NONASCII / LWS</td></tr>
        /// <tr><td style="border-bottom:none">quoted-string = </td><td style="border-bottom:none">SWS DOUBLE_QUOTE *(qdtext / quoted-pair ) DOUBLE_QUOTE</td></tr>
        /// <tr><td style="border-bottom:none">qdtext = </td><td style="border-bottom:none">LWS / %x21 / %x23-5B / %x5D-7E / UTF8-NONASCII</td></tr>
        /// <tr><td style="border-bottom:none">LWS = </td><td style="border-bottom:none">[*WSP CRLF] 1*WSP ; linear whitespace</td></tr>
        /// <tr><td style="border-bottom:none">SWS = </td><td style="border-bottom:none">[LWS] ; sep whitespace</td></tr>
        /// </table>  
        /// <para/>
        /// <note type="implementnotes">This HeaderField allows HeaderField grouping (<see cref="T:Konnetic.Sip.Headers.HeaderFieldGroup{T}"/>).</note>
        /// <example>
        /// <list type="bullet">
        /// <item>User-Agent: Softphone Beta1.5</item>  
        /// </list> 
        /// </example>
        /// </remarks>
        /// <param name="value">The HeaderField string to parse.</param>
        /// <exception cref="T:Konnetic.Sip.SipParseException">Thrown when an invalid (non-standard) value is encountered.</exception>
        /// <exception cref="T:Konnetic.Sip.SipException">Thrown when a processing exception is encountered.</exception>
        /// <threadsafety static="true" instance="false" />
        public override void Parse(string value)
        {
            if(value != null)
                {
                RemoveFieldName(ref value, FieldName, CompactName);
                    base.Parse(value);
                }
        }

        private void Init()
        {
            FieldName = UserAgentHeaderField.LongName;
            CompactName = UserAgentHeaderField.LongName;
        }

        #endregion Methods
    }
}