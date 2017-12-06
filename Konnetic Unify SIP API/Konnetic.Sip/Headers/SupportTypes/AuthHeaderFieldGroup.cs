/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Konnetic.Sip.Headers
{
    /// <summary>
    ///  Combines multiple Authoraization/Authentication HeaderFields with the same name, as a CRLF-seperated HeaderField list.
    /// </summary>        
    /// <remarks>
    /// Groups contain only one type of HeaderField. The <see cref="T:Konnetic.Sip.Headers.AuthHeaderFieldGroup"/> behaves must like a singular HeaderField. Multiple <see cref="T:Konnetic.Sip.Headers.AuthorizationHeaderField"/>, <see cref="T:Konnetic.Sip.Headers.ProxyAuthenticateHeaderField"/>, <see cref="T:Konnetic.Sip.Headers.ProxyAuthorizationHeaderField"/> or <see cref="T:Konnetic.Sip.Headers.WwwAuthenticateHeaderField"/> fields with the same field-name may be present in a message if and only if the entire field-value for that header field is defined as a separated list [i.e.CRLF #(values)]. It must be possible to combine the multiple header fields into one "field-name: field-value" pair, without changing the semantics of the message, by appending each subsequent field-value to the first, each separated by a CRLF. The order in which header fields with the same field-name are received is therefore significant to the interpretation of the combined field value, and thus a proxy must not change the order of these field values when a message is forwarded.
    /// <para/>
    /// <note type="implementnotes"><see cref="T:Konnetic.Sip.Headers.AuthHeaderFieldGroup"/> derives from <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/> and can be used to represent one HeaderField. This is useful for use within the <see cref="T:Konnetic.Sip.Headers.HeaderFieldCollection"/> which does not allow duplicate HeaderFields.</note>
    /// <para/>
    /// <b>RFC 3261 Syntax:</b> 
    /// <table >
    /// <tr><td colspan="2" style="border-bottom:none">header = header-name HCOLON header-value *(CRLF header-value)</td></tr>
    /// <tr><td style="border-bottom:none">header-name = </td><td style="border-bottom:none">Authorization / Proxy-Authenticate / Proxy-Authorization / WWW-Authenticate</td></tr>
    /// </table>
    /// <para/>
    /// The seperator is always CRLF for security HeaderFields. 
    /// </remarks>
    /// <example>
    /// <list type="bullet">
    /// <item>WWW-Authenticate: Digest realm="atlanta.com", domain="sip:boxesbybob.com", qop="auth", nonce="f84f1cec41e6cbe5aea9c8e88d359", opaque="", stale=FALSE, algorithm=MD5
    /// <para>CRLF</para>
    /// WWW-Authenticate: Digest realm="boston.com", domain="sip:boxesbybob.com", qop="auth", nonce="c60f3082ee1212b402a21831ae", opaque="", stale=FALSE, algorithm=MD5</item> 
    /// </list> 
    /// </example>
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    public class AuthHeaderFieldGroup<T> : HeaderFieldGroup<T>
        where T : SecurityHeaderFieldBase, new()
    {
        #region Constructors

	/// <summary>
    /// Initializes a new instance of the <see cref="AuthHeaderFieldGroup"/> class.
	/// </summary>
	/// <remarks>The default constructor.</remarks>
 
        public AuthHeaderFieldGroup()
            : base("\r\n")
        {
        }

        #endregion Constructors

        #region Methods


        /// <summary>
        /// Creates a deep copy of this instance.
        /// </summary> 
        /// <remarks>Creates and returns a deep copy of the HeaderField group. This method ensures a deep copy of the group, when a message is cloned the group can be modified without effecting the original group or HeaderField in the message.</remarks>
        /// <returns>A deep copy of <see cref="T:Konnetic.Sip.Headers.AuthHeaderFieldGroup"/>.</returns>
        /// <threadsafety static="true" instance="false" />
        public override HeaderFieldBase Clone()
        {
            AuthHeaderFieldGroup<T> group = new AuthHeaderFieldGroup<T>();
            if(Count > 0)
                {
                int i = 0;
                do
                    {
                    T b = (T)this[i].Clone();
                    group.Add(b);
                    i++;
                    } while(i < Count);
                }
            return group;
        }

        /// <summary>
        /// Parses string representation of the HeaderField.
        /// </summary>
        /// <remarks>  
        /// <b>RFC 3261 Syntax:</b> 
        /// <table >
        /// <tr><td colspan="2" style="border-bottom:none">header = header-name HCOLON header-value *(CRLF header-value)</td></tr> 
        /// </table>
        /// <para/>
        /// The seperator is always CRLF for security HeaderFields. 
        /// </remarks>
        /// <example>
        /// <list type="bullet">
        /// <item>WWW-Authenticate: Digest realm="atlanta.com", domain="sip:boxesbybob.com", qop="auth", nonce="f84f1cec41e6cbe5aea9c8e88d359", opaque="", stale=FALSE, algorithm=MD5
        /// <para>CRLF</para>
        /// WWW-Authenticate: Digest realm="boston.com", domain="sip:boxesbybob.com", qop="auth", nonce="c60f3082ee1212b402a21831ae", opaque="", stale=FALSE, algorithm=MD5</item>
        /// <item>Server: HomeServer v2</item>
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
                Clear();
                T b = new T();
                if(!value.Contains("\r\n"))
                    {
                        b.Parse(value);
                        Add(b);
                    }
                else{
                    value = Syntax.ReplaceFolding(value);
                    if(!string.IsNullOrEmpty(value))
                        {
                        Regex _uri = new Regex(@"\s*" + b.FieldName + @"\s*:\s*", RegexOptions.Compiled | RegexOptions.IgnoreCase );
                        string[] lines = _uri.Split(value);
                        if(lines.Length > 0)
                            {
                            foreach(string s in lines)
                                {
                                if(!string.IsNullOrEmpty(s))
                                    {
                                    b.Parse(s);
                                    Add(b);
                                    b = new T();
                                    }
                                }
                            }
                        }
                    }
                }
        }

        /// <summary>
        /// Throws if the user attempts to instantiate the generic class with a security HeaderField.
        /// </summary>
        /// <exception cref="SipException">Is raised when the user attempts to instantiate generic class with a security HeaderField.</exception>
        /// <threadsafety static="true" instance="false"/>
        protected override void ThrowOnSecurityGroup()
        {
            //Do Nothing. We don't want to throw on security
        }

        #endregion Methods

        #region Other

        ///// <summary>
        ///// Toes the string value.
        ///// </summary>
        ///// <returns></returns>
        //internal override string ToStringValue()
        //    {
        //    StringBuilder sb = new StringBuilder();
        //    bool first = true;
        //    for(int i = 0; i < Count; i++)
        //        {
        //        string s = this[i].ToStringValue();
        //        if(!string.IsNullOrEmpty(s))
        //            {
        //            if(!first) { sb.Append(Seperator); sb.Append(SR.SingleWhiteSpace); } else { first = false; }
        //            sb.Append(s);
        //            }
        //        }
        //    return sb.ToString();
        //}
        ///// <summary>
        ///// Gets the string.
        ///// </summary>
        ///// <param name="useCompactForm">if set to <c>true</c> [use compact form].</param>
        ///// <returns></returns>
        //protected override string GetString(bool useCompactForm)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    int c = Count;
        //    if(c > 0)
        //        {
        //        sb.Append(useCompactForm ? ShortName : FieldName);
        //        sb.Append(SR.GetString(SR.Seperator));
        //        for(int i = 0; i < c; i++)
        //            {
        //            sb.Append(this[i].ToStringValue());
        //            if(i < c - 1)
        //                {
        //                sb.Append("\r\n");
        //                }
        //            }
        //        }
        //    return sb.ToString();
        //}

        #endregion Other
    }
}