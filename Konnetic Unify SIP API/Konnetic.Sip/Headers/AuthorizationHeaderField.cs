/* 
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
    /// <summary>
    /// The Authorization HeaderField contains authentication credentials of a client.  
    /// </summary>
    /// <remarks>
    /// <b>Standards: RFC3261, RFC2616</b> 
    /// <para/>
    /// <b>Syntax</b> 
    /// <para/>
    /// The values of the opaque and algorithm fields must be those supplied in the WWW-Authenticate response header for the entity being requested. 
    /// <list type="bullet">
    /// <item><i>response</i>: A string of 32 hex digits computed as defined below, which proves that the user knows a password.</item>
    /// <item><i>username</i>: The user's name in the specified realm.</item>
    /// <item><i>digest-uri</i>: The URI from Request-URI of the Request-Line; duplicated here because proxies are allowed to change the Request-Line in transit.</item>
    /// <item><i>qop</i>: Indicates what "quality of protection" the client has applied to the message. If present, its value must be one of the alternatives the server indicated it supports in the <see cref="T:Konnetic.Sip.Headers.WwwAuthenticateHeaderField"/> header. These values affect the computation of the request-digest. Note that this is a single token, not a quoted list of alternatives as in <see cref="T:Konnetic.Sip.Headers.WwwAuthenticateHeaderField"/>. This directive is optional in order to preserve backward compatibility with a minimal implementation of RFC 2069, but should be used if the server indicated that qop is supported by providing a qop directive in the <see cref="T:Konnetic.Sip.Headers.WwwAuthenticateHeaderField"/> HeaderField.</item>
    /// <item><i>cnonce</i>: This must be specified if a qop directive is sent (see above), and must not be specified if the server did not send a qop directive in the <see cref="T:Konnetic.Sip.Headers.WwwAuthenticateHeaderField"/> HeaderField. The cnonce-value is an opaque quoted string value provided by the client and used by both client and server to avoid chosen plaintext attacks, to provide mutual authentication, and to provide some message integrity protection. See the descriptions below of the calculation of the responsedigest and request-digest values.</item>
    /// <item><i>nonce-count</i>: This must be specified if a qop directive is sent (see above), and must not be specified if the server did not send a qop directive in the <see cref="T:Konnetic.Sip.Headers.WwwAuthenticateHeaderField"/> HeaderField. The nc-value is the hexadecimal count of the number of requests (including the current request) that the client has sent with the nonce value in this request. For example, in the first request sent in response to a given nonce value, the client sends "nc=00000001". The purpose of this directive is to allow the server to detect request replays by maintaining its own copy of this count - if the same nc-value is seen  twice, then the request is a replay. See the description below of the construction of the request-digest value.</item>
    /// <item><i>auth-param</i>: This directive allows for future extensions. Any unrecognized directive must be ignored.</item> 
    /// </list>  
    /// <para/>
    /// <b>RFC 3261 Syntax:</b> 
    /// <table >
    /// <tr><td colspan="2" style="border-bottom:none">"Authorization" ":" credentials</td></tr>
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
    /// <para/>
    /// <note type="implementnotes">It is not particularly useful to encrypt this field.</note> 
    /// <example>
    /// <list type="bullet">
    /// <item>Authorization: Digest username="Alice", realm="atlanta.com", nonce="84a4cc6f3082121f32b42a2187831a9e", response="7587245234b3434cc3412213e5f113a5432"</item>
    /// </list> 
    /// </example>
    /// </remarks>
    /// <seealso cref="T:Konnetic.Sip.Headers.AuthorizationHeaderFieldBase"/>
    /// <seealso cref="T:Konnetic.Sip.Headers.AuthenticationInfoHeaderField"/> 
    /// <seealso cref="T:Konnetic.Sip.Headers.ProxyAuthorizationHeaderField"/>
    /// <seealso cref="T:Konnetic.Sip.Headers.WwwAuthenticateHeaderField"/>
    public sealed class AuthorizationHeaderField : AuthorizationHeaderFieldBase
    {
        #region Fields

        internal const string CompareName = "AUTHORIZATION";
        internal const string CompareShortName = CompareName;

        /// <summary>
        /// 
        /// </summary>
        internal const string LongName = "Authorization";

        #endregion Fields

        #region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="AuthorizationHeaderField"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <overloads>
		/// <summary>The method has one overload.</summary>
		/// </overloads>
        public AuthorizationHeaderField()
            : this("Digest")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationHeaderField"/> class.
        /// </summary>
        /// <param name="scheme">The scheme.</param>
        public AuthorizationHeaderField(string scheme)
            : base(scheme)
        {
            FieldName = AuthorizationHeaderField.LongName;
            CompactName = AuthorizationHeaderField.LongName;
            AllowMultiple = true;
        }

        #endregion Constructors

        #region Methods
		/// <summary>
		/// Performs an implicit conversion from <see cref="System.String"/> to <see cref="Konnetic.Sip.Headers.AuthorizationHeaderField"/>.
		/// </summary>
		/// <param name="value">The string value representing the HeaderField.</param>
        /// <returns>A new <see cref="Konnetic.Sip.Headers.AuthorizationHeaderField"/> populated from the <paramref name="value"/> parameter.</returns>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="value"/>.</exception>
        public static implicit operator AuthorizationHeaderField(String value)
        {
            PropertyVerifier.ThrowOnNullArgument(value, "value");

            AuthorizationHeaderField hf = new AuthorizationHeaderField();
            hf.Parse(value);
            return hf;
        }
		/// <summary>
		/// Performs an explicit conversion from <see cref="Konnetic.Sip.Headers.AuthorizationHeaderField"/> to <see cref="System.String"/>.
		/// </summary>
		/// <param name="headerField">The HeaderField to convert.</param>
		/// <returns>A string representation of the HeaderField.</returns>
		/// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="headerField"/>.</exception>
        public static explicit operator string(AuthorizationHeaderField headerField)
        {
            PropertyVerifier.ThrowOnNullArgument(headerField, "headerField");
            return headerField.ToString();
        }


        /// <summary>
        /// Creates a deap  copy of this instance.
        /// </summary> 
        /// <remarks>Creates and returns a deep copy of the Header. This method ensures a deep copy of the HeaderField, when a message is cloned the HeaderField can be modified without effecting the original HeaderField in the message.</remarks>
        /// <returns>A deep copy of <see cref="T:Konnetic.Sip.Headers.AuthorizationHeaderField"/>.</returns>
        /// <threadsafety static="true" instance="false" /> 
public override HeaderFieldBase Clone()
        {
            AuthorizationHeaderField newObj = new AuthorizationHeaderField(Scheme);
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

            AuthorizationHeaderField p1 = obj as AuthorizationHeaderField;
            if((object)p1 == null)
                {
                AuthHeaderFieldGroup<AuthorizationHeaderField> p = obj as AuthHeaderFieldGroup<AuthorizationHeaderField>;
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
        /// <tr><td colspan="2" style="border-bottom:none">"Authorization" ":" credentials</td></tr>
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
        /// <item>Authorization: Digest username="Alice", realm="atlanta.com", nonce="84a4cc6f3082121f32b42a2187831a9e", response="7587245234b3434cc3412213e5f113a5432"</item>
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