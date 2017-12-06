/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Text.RegularExpressions;

using Konnetic.Sip.Messages;

namespace Konnetic.Sip.Headers
    {   
    /// <summary>The Proxy-Authorization HeaderField allows the client to identify itself (or its user) to a proxy that requires authentication. 
    /// </summary>
    /// <remarks> 
    /// <b>Standards: RFC3261, RFC2616</b>
    /// <para/>
    /// When a client sends a request to a proxy server, the proxy server may authenticate the originator before the request is processed. If no credentials (in the Proxy-Authorization HeaderField) are provided in the request, the proxy can challenge the originator to provide credentials by rejecting the request with a 407 (Proxy Authentication Required) status code.
    /// <para/>
    /// A Proxy-Authorization field value consists of credentials containing the authentication information of the user agent for the proxy and/or realm of the resource being requested. Unlike Authorization, the Proxy-Authorization HeaderField applies only to the next outbound proxy that demanded authentication using the Proxy-Authenticate field. When multiple proxies are used in a chain, the Proxy-Authorization HeaderField is consumed by the first outbound proxy that was expecting to receive credentials. A proxy may relay the credentials from the client request to the next proxy if that is the mechanism by which the proxies cooperatively authenticate a given request.
    /// <para/>
    /// All 407 (Proxy Authentication Required) responses must be forwarded upstream toward the client following the procedures for any other response. It is the client's responsibility to add the Proxy-Authorization HeaderField value containing credentials for the realm of the proxy that has asked for authentication.
    /// <para/>
    /// <note type="implementnotes">Proxies must not add values to the Proxy-Authorization HeaderField.</note>
    /// <para/>
    /// <b>RFC 3261 Syntax:</b> 
    /// <table >
    /// <tr><td colspan="2" style="border-bottom:none">"Proxy-Authorization" ":" credentials</td></tr> 
    /// <tr><td style="border-bottom:none">credentials = </td><td style="border-bottom:none">("Digest" WHITESPACE digest-response) / other-response</td></tr>
    /// <tr><td style="border-bottom:none">digest-response = </td><td style="border-bottom:none">dig-resp *("," dig-resp)</td></tr>
    /// <tr><td style="border-bottom:none">dig-resp = </td><td style="border-bottom:none">username / realm / nonce / digest-uri / dresponse / algorithm / cnonce / opaque / message-qop / nonce-count / auth-param</td></tr>
    /// <tr><td style="border-bottom:none">username = </td><td style="border-bottom:none">"username" EQUAL quoted-string</td></tr>
    /// <tr><td style="border-bottom:none">digest-uri = </td><td style="border-bottom:none">"uri" EQUAL &lt; request-uri &gt;</td></tr>
    /// <tr><td style="border-bottom:none">message-qop = </td><td style="border-bottom:none">"qop" EQUAL "auth" / "auth-info" / token</td></tr>
    /// <tr><td style="border-bottom:none">cnonce = </td><td style="border-bottom:none">"cnonce" EQUAL quoted-string</td></tr>
    /// <tr><td style="border-bottom:none">nonce-count = </td><td style="border-bottom:none">"nc" EQUAL 8HEX</td></tr>
    /// <tr><td style="border-bottom:none">dresponse = </td><td style="border-bottom:none">"response" EQUAL DOUBLE_QUOTE 32HEX DOUBLE_QUOTE</td></tr>
    /// <tr><td style="border-bottom:none">auth-param = </td><td style="border-bottom:none">token EQUAL ( token / quoted-string )</td></tr>
    /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr>
    /// <tr><td style="border-bottom:none">quoted-string = </td><td style="border-bottom:none">DOUBLE_QUOTE *(qdtext / quoted-pair ) DOUBLE_QUOTE</td></tr>
    /// <tr><td style="border-bottom:none">qdtext = </td><td style="border-bottom:none">LWS / %x21 / %x23-5B / %x5D-7E / UTF8-NONASCII</td></tr>
    /// <tr><td style="border-bottom:none">quoted-pair = </td><td style="border-bottom:none">"\\" (%x00-09 / %x0B-0C / %x0E-7F)</td></tr>
    /// </table> 
    /// <para/>
    /// <note type="implementnotes">This HeaderField, along with <see cref="T:Konnetic.Sip.Headers.ProxyAuthorizationHeaderField"/>, breaks the general rules about multiple HeaderField values. Although not a comma-separated list, this HeaderField name may be present multiple times, and must not be combined into a single header line using the usual rules. See <see cref="T:Konnetic.Sip.Headers.AuthHeaderFieldGroup{T}"/> for grouping of this HeaderField..</note> 
    /// <example>
    /// <list type="bullet">
    /// <item>Proxy-Authorization: Digest username="Alice", realm="atlanta.com", nonce="c60f3082ee1212b402a21831ae", response="245f23415f11432b3434341c022"</item>  
    /// </list> 
    /// </example>
    /// </remarks> 
    /// <seealso cref="T:Konnetic.Sip.Headers.AuthorizationHeaderFieldBase"/>
    /// <seealso cref="T:Konnetic.Sip.Headers.AuthorizationHeaderField"/>
    /// <seealso cref="T:Konnetic.Sip.Headers.AuthenticationInfoHeaderField"/> 
    /// <seealso cref="T:Konnetic.Sip.Headers.ProxyAuthenticateHeaderField"/>
    /// <seealso cref="T:Konnetic.Sip.Headers.WwwAuthenticateHeaderField"/>
    public sealed class ProxyAuthorizationHeaderField : AuthorizationHeaderFieldBase
    {
        #region Fields

        internal const string CompareName = "PROXY-AUTHORIZATION";
        internal const string CompareShortName = CompareName;
        internal const string LongName = "Proxy-Authorization";

        #endregion Fields

        #region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ProxyAuthorizationHeaderField"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks> 
        public ProxyAuthorizationHeaderField()
            : base()
        {
            FieldName = ProxyAuthorizationHeaderField.LongName;
            CompactName = ProxyAuthorizationHeaderField.LongName;
            AllowMultiple = true;
        }

        #endregion Constructors

        #region Methods
		/// <summary>
		/// Performs an implicit conversion from <see cref="System.String"/> to <see cref="Konnetic.Sip.Headers.ProxyAuthorizationHeaderField"/>.
		/// </summary>
		/// <param name="value">The string value representing the HeaderField.</param>
        /// <returns>A new <see cref="Konnetic.Sip.Headers.ProxyAuthorizationHeaderField"/> populated from the <paramref name="value"/> parameter.</returns>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="value"/>.</exception>
        public static implicit operator ProxyAuthorizationHeaderField(String value)
        {
            PropertyVerifier.ThrowOnNullArgument(value,"value");

            ProxyAuthorizationHeaderField hf = new ProxyAuthorizationHeaderField();
            hf.Parse(value);
            return hf;
        }
		/// <summary>
		/// Performs an explicit conversion from <see cref="Konnetic.Sip.Headers.ProxyAuthorizationHeaderField"/> to <see cref="System.String"/>.
		/// </summary>
		/// <param name="headerField">The HeaderField to convert.</param>
		/// <returns>A string representation of the HeaderField.</returns>
		/// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="headerField"/>.</exception>
        public static explicit operator string(ProxyAuthorizationHeaderField headerField)
        {
            PropertyVerifier.ThrowOnNullArgument(headerField, "headerField");
            return headerField.ToString();
        }

        /// <summary>
        /// Creates a deap  copy of this instance.
        /// </summary> 
        /// <remarks>Creates and returns a deep copy of the Header. This method ensures a deep copy of the HeaderField, when a message is cloned the HeaderField can be modified without effecting the original HeaderField in the message.</remarks>
        /// <returns>A deep copy of <see cref="T:Konnetic.Sip.Headers.ProxyAuthorizationHeaderField"/>.</returns>
        /// <threadsafety static="true" instance="false" /> 
public override HeaderFieldBase Clone()
        {
            ProxyAuthorizationHeaderField newObj = new ProxyAuthorizationHeaderField();
            newObj.Uri = Uri;
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

            ProxyAuthorizationHeaderField p1 = obj as ProxyAuthorizationHeaderField;
            if((object)p1 == null)
                {
                AuthHeaderFieldGroup<ProxyAuthorizationHeaderField> p = obj as AuthHeaderFieldGroup<ProxyAuthorizationHeaderField>;
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
        /// <tr><td colspan="2" style="border-bottom:none">"Proxy-Authorization" ":" credentials</td></tr> 
        /// <tr><td style="border-bottom:none">credentials = </td><td style="border-bottom:none">("Digest" WHITESPACE digest-response) / other-response</td></tr>
        /// <tr><td style="border-bottom:none">digest-response = </td><td style="border-bottom:none">dig-resp *("," dig-resp)</td></tr>
        /// <tr><td style="border-bottom:none">dig-resp = </td><td style="border-bottom:none">username / realm / nonce / digest-uri / dresponse / algorithm / cnonce / opaque / message-qop / nonce-count / auth-param</td></tr>
        /// <tr><td style="border-bottom:none">username = </td><td style="border-bottom:none">"username" EQUAL quoted-string</td></tr>
        /// <tr><td style="border-bottom:none">digest-uri = </td><td style="border-bottom:none">"uri" EQUAL &lt; request-uri &gt;</td></tr>
        /// <tr><td style="border-bottom:none">message-qop = </td><td style="border-bottom:none">"qop" EQUAL "auth" / "auth-info" / token</td></tr>
        /// <tr><td style="border-bottom:none">cnonce = </td><td style="border-bottom:none">"cnonce" EQUAL quoted-string</td></tr>
        /// <tr><td style="border-bottom:none">nonce-count = </td><td style="border-bottom:none">"nc" EQUAL 8HEX</td></tr>
        /// <tr><td style="border-bottom:none">dresponse = </td><td style="border-bottom:none">"response" EQUAL DOUBLE_QUOTE 32HEX DOUBLE_QUOTE</td></tr>
        /// <tr><td style="border-bottom:none">auth-param = </td><td style="border-bottom:none">token EQUAL ( token / quoted-string )</td></tr>
        /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr>
        /// <tr><td style="border-bottom:none">quoted-string = </td><td style="border-bottom:none">DOUBLE_QUOTE *(qdtext / quoted-pair ) DOUBLE_QUOTE</td></tr>
        /// <tr><td style="border-bottom:none">qdtext = </td><td style="border-bottom:none">LWS / %x21 / %x23-5B / %x5D-7E / UTF8-NONASCII</td></tr>
        /// <tr><td style="border-bottom:none">quoted-pair = </td><td style="border-bottom:none">"\\" (%x00-09 / %x0B-0C / %x0E-7F)</td></tr>
        /// </table> 
        /// <para/>
        /// <note type="implementnotes">This HeaderField, along with <see cref="T:Konnetic.Sip.Headers.ProxyAuthorizationHeaderField"/>, breaks the general rules about multiple HeaderField values. Although not a comma-separated list, this HeaderField name may be present multiple times, and must not be combined into a single header line using the usual rules. See <see cref="T:Konnetic.Sip.Headers.AuthHeaderFieldGroup{T}"/> for grouping of this HeaderField..</note> 
        /// <example>
        /// <list type="bullet">
        /// <item>Proxy-Authorization: Digest username="Alice", realm="atlanta.com", nonce="c60f3082ee1212b402a21831ae", response="245f23415f11432b3434341c022"</item>  
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