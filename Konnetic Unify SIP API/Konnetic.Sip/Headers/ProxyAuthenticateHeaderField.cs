﻿/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;

namespace Konnetic.Sip.Headers
{
    /// <summary>A Proxy-Authenticate HeaderField value contains an authentication challenge.
    /// </summary>
    /// <remarks> 
    /// <b>Standards: RFC3261, RFC2616</b>
    /// <para/>
    /// The Proxy-Authenticate response-HeaderField must be included as part of a 407 (Proxy Authentication Required) response. The field value consists of a challenge that indicates the authentication scheme and parameters applicable to the proxy for this Request-URI.
    /// <para/>
    /// Unlike WWW-Authenticate, the Proxy-Authenticate HeaderField applies only to the current connection and should not be passed on to downstream clients. However, an intermediate proxy might need to obtain its own credentials by requesting them from the downstream client, which in some circumstances will appear as if the proxy is forwarding the Proxy-Authenticate HeaderField.
    /// <para/>
    /// <b>RFC 3261 Syntax:</b> 
    /// <para/>
    /// <table >
    /// <tr><td colspan="2" style="border-bottom:none">"Proxy-Authenticate" ":" ("Digest" WHITESPACE digest-cln *("," digest-cln)) / other-challenge</td></tr> 
    /// <tr><td style="border-bottom:none">other-challenge = </td><td style="border-bottom:none">token WHITESPACE auth-param *("," auth-param)</td></tr>
    /// <tr><td style="border-bottom:none">auth-param = </td><td style="border-bottom:none">token EQUAL ( token / quoted-string )</td></tr>
    /// <tr><td style="border-bottom:none">digest-cln = </td><td style="border-bottom:none">realm / domain / nonce / opaque / stale / algorithm / qop-options / auth-param</td></tr>
    /// <tr><td style="border-bottom:none">realm = </td><td style="border-bottom:none">"realm" EQUAL quoted-string</td></tr>
    /// <tr><td style="border-bottom:none">domain = </td><td style="border-bottom:none">"domain" EQUAL " URI *( 1*SP URI ) "</td></tr>
    /// <tr><td style="border-bottom:none">nonce = </td><td style="border-bottom:none">"nonce" EQUAL quoted-string</td></tr>
    /// <tr><td style="border-bottom:none">opaque = </td><td style="border-bottom:none">"opaque" EQUAL quoted-string</td></tr>
    /// <tr><td style="border-bottom:none">stale = </td><td style="border-bottom:none">"stale" EQUAL ( "true" / "false" )</td></tr>
    /// <tr><td style="border-bottom:none">algorithm = </td><td style="border-bottom:none">"algorithm" EQUAL ( "MD5" / "MD5-sess" / token )</td></tr>
    /// <tr><td style="border-bottom:none">qop-options = </td><td style="border-bottom:none">"qop" EQUAL ""auth" / "auth-int" / token *("," "auth" / "auth-int" / token) "</td></tr>
    /// 
    /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr>
    /// <tr><td style="border-bottom:none">quoted-string = </td><td style="border-bottom:none">SWS DOUBLE_QUOTE *(qdtext / quoted-pair ) DOUBLE_QUOTE</td></tr>
    /// <tr><td style="border-bottom:none">qdtext = </td><td style="border-bottom:none">LWS / %x21 / %x23-5B / %x5D-7E / UTF8-NONASCII</td></tr>
    /// <tr><td style="border-bottom:none">LWS = </td><td style="border-bottom:none">[*WSP CRLF] 1*WSP ; linear whitespace</td></tr>
    /// <tr><td style="border-bottom:none">SWS = </td><td style="border-bottom:none">[LWS] ; sep whitespace</td></tr>
    /// <tr><td style="border-bottom:none">host = </td><td style="border-bottom:none">hostname / IPv4address / IPv6reference</td></tr>
    /// </table>   
    /// <para/>
    /// <note type="implementnotes">This HeaderField, along with <see cref="T:Konnetic.Sip.Headers.ProxyAuthorizationHeaderField"/>, breaks the general rules about multiple HeaderField values. Although not a comma-separated list, this HeaderField name may be present multiple times, and must not be combined into a single header line using the usual rules. See <see cref="T:Konnetic.Sip.Headers.AuthHeaderFieldGroup{T}"/> for grouping of this HeaderField..</note> 
    /// <example>
    /// <list type="bullet">
    /// <item>Proxy-Authenticate: Digest realm="atlanta.com", domain="sip:ss1.carrier.com", qop="auth", nonce="f84f1cec41e6cbe5aea9c8e88d359", opaque="", stale=FALSE, algorithm=MD5</item>  
    /// </list> 
    /// </example>
    /// </remarks> 
    /// <seealso cref="T:Konnetic.Sip.Headers.ChallengeHeaderFieldBase"/>
    /// <seealso cref="T:Konnetic.Sip.Headers.AuthorizationHeaderField"/>
    /// <seealso cref="T:Konnetic.Sip.Headers.AuthenticationInfoHeaderField"/> 
    /// <seealso cref="T:Konnetic.Sip.Headers.ProxyAuthorizationHeaderField"/>
    /// <seealso cref="T:Konnetic.Sip.Headers.WwwAuthenticateHeaderField"/>
    public sealed class ProxyAuthenticateHeaderField : ChallengeHeaderFieldBase
    {
        #region Fields

        internal const string CompareName = "PROXY-AUTHENTICATE";
        internal const string CompareShortName = CompareName;

        /// <summary>
        /// 
        /// </summary>
        internal const string LongName = "Proxy-Authenticate";

        #endregion Fields

        #region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ProxyAuthenticateHeaderField"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <overloads>
		/// <summary>The method has one overload.</summary>
		/// </overloads>
        public ProxyAuthenticateHeaderField()
            : this("Digest")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyAuthenticateHeaderField"/> class.
        /// </summary>
        /// <param name="scheme">The scheme.</param>
        public ProxyAuthenticateHeaderField(string scheme)
            : base(scheme)
        {
            FieldName = ProxyAuthenticateHeaderField.LongName;
            CompactName = ProxyAuthenticateHeaderField.LongName;
            AllowMultiple = true;
        }

        #endregion Constructors

        #region Methods
		/// <summary>
		/// Performs an implicit conversion from <see cref="System.String"/> to <see cref="Konnetic.Sip.Headers.ProxyAuthenticateHeaderField"/>.
		/// </summary>
		/// <param name="value">The string value representing the HeaderField.</param>
        /// <returns>A new <see cref="Konnetic.Sip.Headers.ProxyAuthenticateHeaderField"/> populated from the <paramref name="value"/> parameter.</returns>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="value"/>.</exception>
        public static implicit operator ProxyAuthenticateHeaderField(String value)
        {
            PropertyVerifier.ThrowOnNullArgument(value,"value");

            ProxyAuthenticateHeaderField hf = new ProxyAuthenticateHeaderField();
            hf.Parse(value);
            return hf;
        }
		/// <summary>
		/// Performs an explicit conversion from <see cref="Konnetic.Sip.Headers.ProxyAuthenticateHeaderField"/> to <see cref="System.String"/>.
		/// </summary>
		/// <param name="headerField">The HeaderField to convert.</param>
		/// <returns>A string representation of the HeaderField.</returns>
		/// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="headerField"/>.</exception>
        public static explicit operator string(ProxyAuthenticateHeaderField headerField)
        {
            PropertyVerifier.ThrowOnNullArgument(headerField, "headerField");
            return headerField.ToString();
        }

        /// <summary>
        /// Creates a deap  copy of this instance.
        /// </summary> 
        /// <remarks>Creates and returns a deep copy of the Header. This method ensures a deep copy of the HeaderField, when a message is cloned the HeaderField can be modified without effecting the original HeaderField in the message.</remarks>
        /// <returns>A deep copy of <see cref="T:Konnetic.Sip.Headers.ProxyAuthenticateHeaderField"/>.</returns>
        /// <threadsafety static="true" instance="false" /> 
public override HeaderFieldBase Clone()
        {
            ProxyAuthenticateHeaderField newObj = new ProxyAuthenticateHeaderField(Scheme);
            newObj.Domain = Domain;
            newObj.DomainSet = DomainSet;
            CopyParametersTo(newObj); 
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

            ProxyAuthenticateHeaderField p1 = obj as ProxyAuthenticateHeaderField;
            if((object)p1 == null)
                {
                AuthHeaderFieldGroup<ProxyAuthenticateHeaderField> p = obj as AuthHeaderFieldGroup<ProxyAuthenticateHeaderField>;
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
        /// <para/>
        /// <table >
        /// <tr><td colspan="2" style="border-bottom:none">"Proxy-Authenticate" ":" ("Digest" WHITESPACE digest-cln *("," digest-cln)) / other-challenge</td></tr> 
        /// <tr><td style="border-bottom:none">other-challenge = </td><td style="border-bottom:none">token WHITESPACE auth-param *("," auth-param)</td></tr>
        /// <tr><td style="border-bottom:none">auth-param = </td><td style="border-bottom:none">token EQUAL ( token / quoted-string )</td></tr>
        /// <tr><td style="border-bottom:none">digest-cln = </td><td style="border-bottom:none">realm / domain / nonce / opaque / stale / algorithm / qop-options / auth-param</td></tr>
        /// <tr><td style="border-bottom:none">realm = </td><td style="border-bottom:none">"realm" EQUAL quoted-string</td></tr>
        /// <tr><td style="border-bottom:none">domain = </td><td style="border-bottom:none">"domain" EQUAL " URI *( 1*SP URI ) "</td></tr>
        /// <tr><td style="border-bottom:none">nonce = </td><td style="border-bottom:none">"nonce" EQUAL quoted-string</td></tr>
        /// <tr><td style="border-bottom:none">opaque = </td><td style="border-bottom:none">"opaque" EQUAL quoted-string</td></tr>
        /// <tr><td style="border-bottom:none">stale = </td><td style="border-bottom:none">"stale" EQUAL ( "true" / "false" )</td></tr>
        /// <tr><td style="border-bottom:none">algorithm = </td><td style="border-bottom:none">"algorithm" EQUAL ( "MD5" / "MD5-sess" / token )</td></tr>
        /// <tr><td style="border-bottom:none">qop-options = </td><td style="border-bottom:none">"qop" EQUAL ""auth" / "auth-int" / token *("," "auth" / "auth-int" / token) "</td></tr>
        /// 
        /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr>
        /// <tr><td style="border-bottom:none">quoted-string = </td><td style="border-bottom:none">SWS DOUBLE_QUOTE *(qdtext / quoted-pair ) DOUBLE_QUOTE</td></tr>
        /// <tr><td style="border-bottom:none">qdtext = </td><td style="border-bottom:none">LWS / %x21 / %x23-5B / %x5D-7E / UTF8-NONASCII</td></tr>
        /// <tr><td style="border-bottom:none">LWS = </td><td style="border-bottom:none">[*WSP CRLF] 1*WSP ; linear whitespace</td></tr>
        /// <tr><td style="border-bottom:none">SWS = </td><td style="border-bottom:none">[LWS] ; sep whitespace</td></tr>
        /// <tr><td style="border-bottom:none">host = </td><td style="border-bottom:none">hostname / IPv4address / IPv6reference</td></tr>
        /// </table>   
        /// <para/>
        /// <note type="implementnotes">This HeaderField, along with <see cref="T:Konnetic.Sip.Headers.ProxyAuthorizationHeaderField"/>, breaks the general rules about multiple HeaderField values. Although not a comma-separated list, this HeaderField name may be present multiple times, and must not be combined into a single header line using the usual rules. See <see cref="T:Konnetic.Sip.Headers.AuthHeaderFieldGroup{T}"/> for grouping of this HeaderField..</note>  
        /// <example>
        /// <list type="bullet">
        /// <item>Proxy-Authenticate: Digest realm="atlanta.com", domain="sip:ss1.carrier.com", qop="auth", nonce="f84f1cec41e6cbe5aea9c8e88d359", opaque="", stale=FALSE, algorithm=MD5</item>  
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

        #endregion Methods
    }
}