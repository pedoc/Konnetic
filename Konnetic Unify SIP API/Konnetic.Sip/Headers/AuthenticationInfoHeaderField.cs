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
using System.Collections.ObjectModel;

namespace Konnetic.Sip.Headers
{
    /// <summary>
    /// The Authentication-Info HeaderField provides for mutual authentication with HTTP Digest. A server may include this HeaderField in a 2xx response to a request that was successfully authenticated using digest based on the <see cref="T:Konnetic.Sip.Headers.AuthorizationHeaderField"/> HeaderField.
    /// </summary>
    /// <remarks>
    /// <b>Standards: RFC3261, RFC2616</b> 
    /// <para/>
    /// The <seealso cref="T:Konnetic.Sip.Headers.AuthenticationInfoHeaderField"/> header is used by the server to communicate some information regarding the successful authentication in the response.
    /// <para/>
    /// <b>Syntax</b>
    /// The value of the nextnonce directive is the nonce the server wishes the client to use for a future authentication response. The server may send the Authentication-Info header with a nextnonce field as a means of implementing one-time or otherwise changing nonces. If the nextnonce field is present the client should use it when constructing the Authorization header for its next request. Failure of the client to do so may result in a request to re-authenticate from the server with the "stale=TRUE".
    /// <para/>
    /// <note type="implementnotes">Server implementations should carefully consider the performance implications of the use of this mechanism; pipelined requests will not be possible if every response includes a nextnonce directive that must be used on the next request received by the server. Consideration should be given to the performance vs. security tradeoffs of allowing an old nonce value to be used for a limited time to permit request pipelining. Use of the nonce-count can retain most of the security advantages of a new server nonce without the deleterious affects on pipelining.</note> 
    /// The Message-Qop parameter indicates the "quality of protection" options applied to the response by the server. The value "auth" indicates authentication; the value "auth-int" indicates authentication with integrity protection. The server should use the same value for the messageqop directive in the response as was sent by the client in the corresponding request.
    /// <para/>
    /// The optional response digest in the "response-auth" directive supports mutual authentication -- the server proves that it knows the user's secret, and with qop=auth-int also provides limited integrity protection of the response. The "response-digest" value is calculated as for the "request-digest" in the Authorization header, except that if "qop=auth" or is not specified in the Authorization header for the request, <i>A2 = ":" digest-uri-value</i> and if "qop=auth-int", then <i>A2 = ":" digest-uri-value ":" H(entity-body)</i>. Where "digest-uri-value" is the value of the "uri" directive on the Authorization header in the request. The "cnonce-value" and "ncvalue" must be the ones for the client request to which this message is the response. The "response-auth", "cnonce", and "nonce-count" directives must be present if "qop=auth" or "qop=auth-int" is specified.
    /// <para/>
    /// <b>RFC 3261 Syntax:</b> 
    /// <table >
    /// <tr><td colspan="2" style="border-bottom:none">"Authentication-Info" ":" ainfo *("," ainfo)</td></tr>
    /// <tr><td style="border-bottom:none">ainfo = </td><td style="border-bottom:none">nextnonce / message-qop / response-auth / cnonce / nonce-count</td></tr>
    /// <tr><td style="border-bottom:none">nextnonce = </td><td style="border-bottom:none">"nextnonce" EQUAL quoted-string</td></tr>
    /// <tr><td style="border-bottom:none">response-auth = </td><td style="border-bottom:none">"rspauth" EQUAL response-digest</td></tr>
    /// <tr><td style="border-bottom:none">response-digest = </td><td style="border-bottom:none">DOUBLE_QUOTE *HEX DOUBLE_QUOTE</td></tr>
    /// <tr><td style="border-bottom:none">quoted-string = </td><td style="border-bottom:none">DOUBLE_QUOTE *(qdtext / quoted-pair ) DOUBLE_QUOTE</td></tr>
    /// <tr><td style="border-bottom:none">qdtext = </td><td style="border-bottom:none">LWS / %x21 / %x23-5B / %x5D-7E / UTF8-NONASCII</td></tr>
    /// <tr><td style="border-bottom:none">quoted-pair = </td><td style="border-bottom:none">"\\" (%x00-09 / %x0B-0C / %x0E-7F)</td></tr>
    /// </table>
    /// <para/>
    /// <note type="implementnotes">This HeaderField does not allow HeaderField grouping.</note> 
    /// <example>
    /// <list type="bullet">
    /// <item>Authentication-Info: nextnonce="47364c23432d2e131a5fb210812c"</item>
    /// </list> 
    /// </example>
    /// </remarks>
    /// <seealso cref="T:Konnetic.Sip.Headers.SecurityHeaderFieldBase"/>
    /// <seealso cref="T:Konnetic.Sip.Headers.AuthorizationHeaderField"/> 
    public sealed class AuthenticationInfoHeaderField : SecurityHeaderFieldBase
    {
        #region Fields

        internal const string CompareName = "AUTHENTICATION-INFO";
        internal const string CompareShortName = CompareName;

        /// <summary>
        /// 
        /// </summary>
        internal const string LongName = "Authentication-Info";

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the CNonce.
        /// </summary>
        /// <value>The CNonce.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="CNonce"/>.</exception> 
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
        public string CNonce
        {
            get
                {
                SipParameter sp = HeaderParameters["cnonce"];

                if((object)sp == null)
                    {
                    return string.Empty;
                    }

                return Syntax.UnQuotedString(sp.Value);
                }
            set
            {
            PropertyVerifier.ThrowOnNullArgument(value, "CNonce");
                value = value.Trim().ToLowerInvariant();
                if(string.IsNullOrEmpty(value))
                    {
                    RemoveParameter("cnonce");
                    }
                else
                    {
                    SipParameter sp = new SipParameter("cnonce", Syntax.ConvertToQuotedString(value), true);
                    sp.CaseSensitiveComparison = true;
                    HeaderParameters.Set(sp);
                    }
                }
        }

        /// <summary>
        /// Gets or sets the message qop.
        /// </summary>
        /// <value>The message qop.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="MessageQop"/>.</exception> 
        /// <exception cref="SipFormatException">Thrown when a user attempts to add non-token characters.</exception> 
        public string MessageQop
        {
            get
                {
                SipParameter sp = HeaderParameters["qop"];

                if((object)sp == null)
                    {
                    return string.Empty;
                    }

                return sp.Value;
                }
            set
            {
                PropertyVerifier.ThrowOnNullArgument(value, "MessageQop");
                value = value.Trim();
                PropertyVerifier.ThrowOnInvalidToken(value, "MessageQop");
                if(string.IsNullOrEmpty(value))
                    {
                    RemoveParameter("qop");
                    }
                else
                    {
                    HeaderParameters.Set("qop", value);
                    }
                }
        }

        /// <summary>
        /// Gets or sets the next nonce.
        /// </summary>
        /// <value>The next nonce.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="NextNonce"/>.</exception>  
        public string NextNonce
        {
            get
                {
                SipParameter sp = HeaderParameters["nextnonce"];
                
                if((object)sp == null)
                    {
                    return string.Empty;
                    }

                return Syntax.UnQuotedString(sp.Value);
                }
            set
            {
            PropertyVerifier.ThrowOnNullArgument(value, "NextNonce");
                value = value.Trim().ToLowerInvariant();
                if(string.IsNullOrEmpty(value))
                    {
                    RemoveParameter("nextnonce");
                    }
                else
                    {
                    SipParameter sp = new SipParameter("nextnonce", Syntax.ConvertToQuotedString(value), true);
                    sp.CaseSensitiveComparison = true;
                    HeaderParameters.Set(sp);
                    }

                }
        }

        /// <summary>
        /// Gets or sets the nonce count.
        /// </summary>
        /// <value>The nonce count.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="NonceCount"/>.</exception>  
        public string NonceCount
        {
            get
                {
                SipParameter sp = HeaderParameters["nc"];

                if((object)sp == null)
                    {
                    return string.Empty;
                    }

                return sp.Value;
                }
            set
            {
            PropertyVerifier.ThrowOnNullArgument(value, "NonceCount");
            value = value.Trim().ToLowerInvariant();
            if(value.Length > 24)
                {
                throw new SipFormatException("NonceCount cannot be greater than 8 characters (LHex).");
                }
            if(!string.IsNullOrEmpty(value) && !Syntax.IsLHex(value))
                {
                throw new SipFormatException("Illegal characters found in NonceCount.");
                }
                if(string.IsNullOrEmpty(value))
                    {
                    RemoveParameter("nc");
                    }
                else
                    {
                    HeaderParameters.Set("nc", value.ToString());
                    }
                }
        }

        /// <summary>
        /// Gets or sets the response auth.
        /// </summary>
        /// <value>The response auth.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="ResponseAuth"/>.</exception>
        public string ResponseAuth
        {
            get
                {
                SipParameter sp = HeaderParameters["rspauth"];

                if((object)sp == null)
                    {
                    return string.Empty;
                    }

                return Syntax.UnQuotedString(sp.Value);
                }
            set
            {
            PropertyVerifier.ThrowOnNullArgument(value, "ResponseAuth");
                value = value.Trim().ToLowerInvariant();
                if(!string.IsNullOrEmpty(value) && !Syntax.IsLHexWithQuotes(value))
                    {
                    throw new SipFormatException("Illegal characters found in ResponseAuth");
                    }
                if(string.IsNullOrEmpty(value))
                    {
                    RemoveParameter("rspauth");
                    }
                else
                    {
                    SipParameter sp = new SipParameter("rspauth", Syntax.ConvertToQuotedString(value), true);
                    sp.CaseSensitiveComparison = true;
                    HeaderParameters.Set(sp);
                    }
                }
        }
        
        #endregion Properties

        #region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="AuthenticationInfoHeaderField"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks> 
        public AuthenticationInfoHeaderField()
            : base()
            {
            RegisterKnownParameter("cnonce");
            RegisterKnownParameter("qop");
            RegisterKnownParameter("nextnonce");
            RegisterKnownParameter("nc");
            RegisterKnownParameter("rspauth");
            FieldName = AuthenticationInfoHeaderField.LongName;
            CompactName = AuthenticationInfoHeaderField.LongName;
            AllowMultiple = false;
            AllowGenericParameters = false;
        }

        #endregion Constructors

        #region Methods
		/// <summary>
		/// Performs an implicit conversion from <see cref="System.String"/> to <see cref="Konnetic.Sip.Headers.AuthenticationInfoHeaderField"/>.
		/// </summary>
		/// <param name="value">The string value representing the HeaderField.</param>
        /// <returns>A new <see cref="Konnetic.Sip.Headers.AuthenticationInfoHeaderField"/> populated from the <paramref name="value"/> parameter.</returns>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="value"/>.</exception>
        public static implicit operator AuthenticationInfoHeaderField(String value)
        {
            PropertyVerifier.ThrowOnNullArgument(value,"value");

            AuthenticationInfoHeaderField hf = new AuthenticationInfoHeaderField();
            hf.Parse(value);
            return hf;
        }
		/// <summary>
		/// Performs an explicit conversion from <see cref="Konnetic.Sip.Headers.AuthenticationInfoHeaderField"/> to <see cref="System.String"/>.
		/// </summary>
		/// <param name="headerField">The HeaderField to convert.</param>
		/// <returns>A string representation of the HeaderField.</returns>
		/// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="headerField"/>.</exception>
        public static explicit operator string(AuthenticationInfoHeaderField headerField)
        {
            PropertyVerifier.ThrowOnNullArgument(headerField, "headerField");
            return headerField.ToString();
        }

        /// <summary>
        /// Creates a deap  copy of this instance.
        /// </summary> 
        /// <remarks>Creates and returns a deep copy of the Header. This method ensures a deep copy of the HeaderField, when a message is cloned the HeaderField can be modified without effecting the original HeaderField in the message.</remarks>
        /// <returns>A deep copy of <see cref="T:Konnetic.Sip.Headers.AuthenticationInfoHeaderField"/>.</returns>
        /// <threadsafety static="true" instance="false" /> 
public override HeaderFieldBase Clone()
        {
        AuthenticationInfoHeaderField newObj = new AuthenticationInfoHeaderField();
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

            AuthenticationInfoHeaderField p1 = obj as AuthenticationInfoHeaderField;
            if((object)p1 == null)
                {
                AuthHeaderFieldGroup<AuthenticationInfoHeaderField> p = obj as AuthHeaderFieldGroup<AuthenticationInfoHeaderField>;
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
        /// <tr><td colspan="2" style="border-bottom:none">Authentication-Info = "Authentication-Info" ":" ainfo *("," ainfo)</td></tr>
        /// <tr><td style="border-bottom:none">ainfo = </td><td style="border-bottom:none">nextnonce / message-qop / response-auth / cnonce / nonce-count</td></tr>
        /// <tr><td style="border-bottom:none">nextnonce = </td><td style="border-bottom:none">"nextnonce" EQUAL quoted-string</td></tr>
        /// <tr><td style="border-bottom:none">response-auth = </td><td style="border-bottom:none">"rspauth" EQUAL response-digest</td></tr>
        /// <tr><td style="border-bottom:none">response-digest = </td><td style="border-bottom:none">DOUBLE_QUOTE *HEX DOUBLE_QUOTE</td></tr>
        /// <tr><td style="border-bottom:none">quoted-string = </td><td style="border-bottom:none">DOUBLE_QUOTE *(qdtext / quoted-pair ) DOUBLE_QUOTE</td></tr>
        /// <tr><td style="border-bottom:none">qdtext = </td><td style="border-bottom:none">LWS / %x21 / %x23-5B / %x5D-7E / UTF8-NONASCII</td></tr>
        /// <tr><td style="border-bottom:none">quoted-pair = </td><td style="border-bottom:none">"\\" (%x00-09 / %x0B-0C / %x0E-7F)</td></tr>
        /// </table>
        /// <para/>
        /// <note type="implementnotes">This HeaderField does not allow HeaderField grouping.</note> 
        /// <example>
        /// <list type="bullet">
        /// <item>Authentication-Info: nextnonce="47364c23432d2e131a5fb210812c"</item>
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
                NextNonce = string.Empty;
                MessageQop = string.Empty;
                ResponseAuth = string.Empty;
                CNonce = string.Empty;
                NonceCount = string.Empty;
            if(!string.IsNullOrEmpty(value))
                {

                Regex _nextnonce = new Regex(@"(?<=(.|\n)*nextnonce\s*=\s*"")(.|\n)*?([^\\](?="")|\\\\(?=""))", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                Regex _qop = new Regex(@"(?<=(.|\n)*qop\s*=\s*)[\w\-.!%*_+`'~]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                Regex _responseAuth = new Regex(@"(?<=(.|\n)*rspauth\s*=\s*"")([0-9]|%[0-9a-f]{2})*((?=[^\\])(?="")|(?=\\\\""))", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                Regex _cNonce = new Regex(@"(?<=(.|\n)*cnonce\s*=\s*"")(.|\n)*?([^\\](?="")|\\\\(?=""))", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                Regex _nonceCount = new Regex(@"(?<=(.|\n)*nc\s*=\s*)([0-9]|%[0-9a-f]{2}){8}(?=(,|\s|$)+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                Match m = _nextnonce.Match(value);
                if(m != null)
                    {
                    if(!string.IsNullOrEmpty(m.Value))
                        {
						try{
                        NextNonce = Syntax.UnQuotedString(m.Value.Trim());

						}
						catch(SipException ex)
							{
                            throw new SipParseException("NextNonce", SR.ParseExceptionMessage(value), ex);
							}
						catch(Exception ex)
                            {
                            throw new SipParseException(SR.GetString(SR.GeneralParseException, m.Value, "NextNonce"), ex);  
							}
                        }
                    }

                m = _qop.Match(value);
                if(m != null)
                    {
                    if(!string.IsNullOrEmpty(m.Value))
                        {
						try{
                        MessageQop = m.Value.Trim();

						}
						catch(SipException ex)
							{
							throw new SipParseException("MessageQop", SR.ParseExceptionMessage(value), ex);
							}
						catch(Exception ex)
                            {
                            throw new SipException(SR.GetString(SR.GeneralParseException, m.Value, "MessageQop"), ex);   
							}
                        }
                    }

                m = _responseAuth.Match(value);
                if(m != null)
                    {
                    if(!string.IsNullOrEmpty(m.Value))
                        {
						try{
                        ResponseAuth = Syntax.UnQuotedString(m.Value.Trim());
						}
						catch(SipException ex)
							{
							throw new SipParseException("ResponseAuth", SR.ParseExceptionMessage(value), ex);
							}
						catch(Exception ex)
                            {
                            throw new SipException(SR.GetString(SR.GeneralParseException, m.Value, "ResponseAuth"), ex); 
							}
                        }
                    }

                m = _nonceCount.Match(value);
                if(m != null)
                    {
                    if(!string.IsNullOrEmpty(m.Value))
                        {
						try{
                        NonceCount = m.Value;
						}
						catch(SipException ex)
							{
							throw new SipParseException("NonceCount", SR.ParseExceptionMessage(value), ex);
							}
						catch(Exception ex)
                            {
                            throw new SipException(SR.GetString(SR.GeneralParseException, m.Value, "NonceCount"), ex);  
							}
                        }
                    }

                m = _cNonce.Match(value);
                if(m != null)
                    {
                    if(!string.IsNullOrEmpty(m.Value))
                        {
						try{
                        CNonce = Syntax.UnQuotedString(m.Value.Trim());
						}
						catch(SipException ex)
							{
							throw new SipParseException("CNonce", SR.ParseExceptionMessage(value), ex);
							}
						catch(Exception ex)
                            {
                            throw new SipException(SR.GetString(SR.GeneralParseException, m.Value, "CNonce"), ex); 
							}
                        
                        }
                    }
                }
            }
        } 
        #endregion Methods
    }
}