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
    /// The <see cref="T:Konnetic.Sip.Headers.AuthorizationHeaderFieldBase"/> provides Authorization credential information for HeaderFields. 
    /// </summary>
    /// <remarks>
    /// <b>Standards: RFC3261, RFC2616, RFC2617</b>
    /// <para/>
    /// The abstract base class is used by <see cref="T:Konnetic.Sip.Headers.AuthorizationHeaderField"/> and <seealso cref="T:Konnetic.Sip.Headers.ProxyAuthorizationHeaderField"/> headers.  
    /// <para/>
    /// The credentials contain the authentication information of the client for the realm of the resource being requested as well as parameters required in support of authentication and replay protection.
    /// <para/>
    /// <b>RFC 3261 Syntax:</b> 
    /// <table > 
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
    /// <example>
    /// <list type="bullet">
    /// <item>Authorization: Digest username="Alice", realm="atlanta.com", nonce="84a4cc6f3082121f32b42a2187831a9e", response="7587245234b3434cc3412213e5f113a5432"</item>
    /// <item>Proxy-Authorization: Digest username="Alice", realm="atlanta.com", nonce="c60f3082ee1212b402a21831ae", response="245f23415f11432b3434341c022"</item>  
    /// </list> 
    /// </example>
    /// </remarks>
    /// <seealso cref="T:Konnetic.Sip.Headers.SchemeHeaderFieldBase"/>
    /// <seealso cref="T:Konnetic.Sip.Headers.AuthorizationHeaderField"/>
    /// <seealso cref="T:Konnetic.Sip.Headers.ProxyAuthorizationHeaderField"/> 
    public abstract class AuthorizationHeaderFieldBase : SchemeAuthHeaderFieldBase
    {
        #region Fields

        private SipUri _uri; 

        #endregion Fields

        #region Properties
        /// <summary>
        /// Gets the generic parameters.
        /// </summary>
        /// <value>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection<SipParameter>"/> field parameter.</value>
        public System.Collections.ObjectModel.ReadOnlyCollection<SipParameter> AuthParameters
            {
            get { return InternalGenericParameters; }
            }
        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public void AddParameter(string name, string value)
            {
            InternalAddGenericParameter(name, value);
            }
        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void AddParameter(SipParameter parameter)
            {
            InternalAddGenericParameter(parameter);
            }
        /// <summary>
        /// Gets or sets the C nonce.
        /// </summary>
        /// <remarks>This must be specified if a qop directive is sent, and must not be specified if the server did not send a qop directive in the WWW-Authenticate header field. The cnonce-value is an opaque quoted string value provided by the client and used by both client and server to avoid chosen plaintext attacks, to provide mutual authentication, and to provide some message integrity protection. See the descriptions below of the calculation of the responsedigest and request-digest values.
        /// <para/>
        /// The CNonce value is always converted to a quoted-string for the HeaderFields.
        /// </remarks>
        /// <value>The CNonce.</value> 
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
        /// Gets or sets the nonce count. The nc-value is the hexadecimal count of the number of requests (including the current request) that the client has sent with the nonce value in this request.
        /// </summary>
        /// <remarks>This must be specified if a qop directive is sent (see above), and must not be specified if the server did not send a qop directive in the WWW-Authenticate header field. 
        /// <example>In the first request sent in response to a given nonce value, the client sends "nc=00000001". </example><para/>The purpose of this directive is to allow the server to detect request replays by maintaining its own copy of this count - if the same nc-value is seen twice, then the request is a replay. See the description below of the construction of the request-digest value.</remarks>
        /// <value>The nonce count.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="NonceCount"/>.</exception> 
        /// <exception cref="SipFormatException">Thrown when a user attempts to add a CNonce greater than 8 Hex chars <paramref name="NonceCount"/>.</exception> 
        /// <exception cref="SipFormatException">Thrown when a user attempts to add non-Hex or numeric characters.<paramref name="NonceCount"/>.</exception>  
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
        /// Gets or sets the message qop.
        /// </summary>
        /// <value>The message qop.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="MessageQop"/>.</exception> 
        /// <exception cref="SipFormatException">Thrown when a user attempts to add non-token characters.</exception>
        public virtual string MessageQop
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
        /// Gets or sets the response.A string of 32 hex digits, which proves that the user knows a password
        /// </summary> 
        /// <remarks>The Response is always converted to a quoted-string in the HeaderField.</remarks>
        /// <value>The response.</value>
        /// <exception cref="SipException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="Response"/>.</exception> 
        /// <exception cref="SipFormatException">Thrown when a user attempts to add a CNonce greater than 8 Hex chars <paramref name="NonceCount"/>.</exception> 
        /// <exception cref="SipFormatException">Thrown when a user attempts to add a quoted non-Hex or non-numeric character.<paramref name="NonceCount"/>.</exception>  
        public string Response
        {
            get
                {
                SipParameter sp = HeaderParameters["response"];

                if((object)sp == null)
                    {
                    return string.Empty;
                    }

                return Syntax.UnQuotedString(sp.Value);
                }
            set
            {
            PropertyVerifier.ThrowOnNullArgument(value, "Response");
                value = value.Trim().ToLowerInvariant();
                if(string.IsNullOrEmpty(value))
                    {
                    RemoveParameter("response");
                    }
                else
                    {
                    if(!Syntax.IsLHexWithQuotes(value))
                        {
                        throw new SipFormatException("Illegal characters found in Response");
                        }
                    SipParameter sp = new SipParameter("response", Syntax.ConvertToQuotedString(value), true);
                    sp.CaseSensitiveComparison = true;
                    HeaderParameters.Set(sp);
                    }
                }
        }

        /// <summary>
        /// Gets or sets the URI. The URI from Request-URI of the Request-Line; duplicated here because proxies are allowed to change the Request-Line in transit.
        /// </summary>
        /// <value>The URI.</value>
        public SipUri Uri
        {
            get { return _uri; }
            set
            {
            PropertyVerifier.ThrowOnNullArgument(value, "Uri"); 
            _uri = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the user. The user's name in the specified realm.
        /// </summary>
        /// <remarks>The username is always converted to a quoted-string in the HeaderField.</remarks>
        /// <value>The name of the user.</value>
        /// <exception cref="SipException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="UserName"/>.</exception>  
        public string Username
        {
            get
                {
                SipParameter sp = HeaderParameters["username"];

                if((object)sp == null)
                    {
                    return string.Empty;
                    }
                return Syntax.UnQuotedString(sp.Value);
                }
            set
                {
                PropertyVerifier.ThrowOnNullArgument(value, "UserName");
                if(string.IsNullOrEmpty(value))
                    {
                    RemoveParameter("username");
                    }
                else
                    {
                    SipParameter sp = new SipParameter("username", Syntax.ConvertToQuotedString(value), true);
                    sp.CaseSensitiveComparison = true;
                    HeaderParameters.Set(sp);
                    }
                }
        }
 

        #endregion Properties

        #region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="AuthorizationHeaderFieldBase"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <overloads>
		/// <summary>The method has two overloads.</summary>
		/// </overloads>
        protected AuthorizationHeaderFieldBase()
            : this("Digest")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationHeaderFieldBase"/> class.
        /// </summary>
        /// <param name="scheme">The scheme.</param>
        protected AuthorizationHeaderFieldBase(string scheme)
            : base(scheme)
        {
        RegisterKnownParameter("username");
        RegisterKnownParameter("response");
        RegisterKnownParameter("nc");
        RegisterKnownParameter("cnonce");
        RegisterKnownParameter("qop");
        _uri = new SipUri();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Compare this SIP Header for equality with another <see cref="T:Konnetic.Sip.Headers.AuthorizationHeaderFieldBase"/> object.</summary>
        /// <remarks>
        /// All optional headers are compared using object equality that is each field in the header is used for comparision. When comparing HeaderFields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular HeaderField, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are case-sensitive. 
        /// </remarks>
        /// <param name="other">The <see cref="T:Konnetic.Sip.Headers.AuthorizationHeaderFieldBase"/> to compare to this instance.</param>
        /// <returns><c>true</c> if <paramref name="other" /> is an instance of this class representing the same SIP HeaderField as this, <c>false</c> otherwise.</returns>    
        /// <overloads>
        /// <summary>This method is overloaded.</summary>
        /// <remarks>Overloads allow for equality against <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>, <see cref="T:System.Object"/> and another instance of this HeaderField.</remarks>
        /// </overloads>  
        /// <threadsafety static="true" instance="false" />
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public bool Equals(AuthorizationHeaderFieldBase other)
            {
            if((object)other == null)
                {
                return false;
                }
            bool baseVal = base.Equals((SchemeAuthHeaderFieldBase)other) && Scheme.Equals(other.Scheme, StringComparison.OrdinalIgnoreCase);
            if(baseVal == true)
                {
                return baseVal && Uri.Equals(other.Uri);
                }
            else
                {
                return baseVal;
                }
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

            AuthorizationHeaderFieldBase p1 = obj as AuthorizationHeaderFieldBase;
            if((object)p1 == null)
                {
                    return false;
                }
            else
                {
                return this.Equals(p1);
                }
        }
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
                return base.IsValid() && !string.IsNullOrEmpty(Scheme) && HeaderParameters.Count > 0;
        }
        /// <summary>
        /// Parses string representation of the HeaderField.
        /// </summary>
        /// <remarks>  
        /// <b>RFC 3261 Syntax:</b> 
        /// <table > 
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
        /// <example>
        /// <list type="bullet">
        /// <item>Authorization: Digest username="Alice", realm="atlanta.com", nonce="84a4cc6f3082121f32b42a2187831a9e", response="7587245234b3434cc3412213e5f113a5432"</item>
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
                this._uri = new SipUri();
                Uri.IsUriSet = false;
                //Uri can contain "," which is wrongly interprested by the paramatized HeaderField.
                Regex _uriRegex = new Regex(@"(?<=(.|\n)*" + Scheme + @"(.|\n)*uri\s*=\s*"")(.|\n)*?([^\\](?="")|\\\\(?=""))", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                Match mUri = _uriRegex.Match(value);
                Regex _uriRegexReplace = new Regex(@"(?<=(.|\n)*" + Scheme + @"(.|\n)*""),?\s*uri\s*=\s*(.|\n)*?([^\\](?="")|\\\\(?=""))", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                value = _uriRegexReplace.Replace(value, string.Empty);

                base.Parse(value);

                Username = string.Empty;
                Response = string.Empty;
                NonceCount = string.Empty;
                CNonce = string.Empty;
                if(mUri != null)
                    {
                    if(!string.IsNullOrEmpty(mUri.Value))
                        {
						try{
                        Uri = new SipUri(mUri.Value);
						}
						catch(SipException ex)
							{
							throw new SipParseException("Uri", SR.ParseExceptionMessage(value), ex);
							}
						catch(Exception ex)
                            {
                            throw new SipParseException(SR.GetString(SR.GeneralParseException, mUri.Value, "Uri"), ex);   
							}
                        }
                    }

                if(!string.IsNullOrEmpty(value))
                    {
                    Regex _username = new Regex(@"(?<=(.|\n)*" + Scheme + @"(.|\n)*username\s*=\s*"")(.|\n)*?([^\\](?="")|\\\\(?=""))", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    Regex _dResponse = new Regex(@"(?<=(.|\n)*" + Scheme + @"(.|\n)*response\s*=\s*<\s*)([0-9]|%[0-9a-f]{2}){32}", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    Regex _cNonce = new Regex(@"(?<=(.|\n)*" + Scheme + @"(.|\n)*cnonce\s*=\s*"")(.|\n)*?([^\\](?="")|\\\\(?=""))", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    Regex _nonceCount = new Regex(@"(?<=(.|\n)*" + Scheme + @"(.|\n)*nc\s*=\s*)([0-9]|%[0-9a-f]{2}){8}(?=(,|\s|$)+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    Regex _qopRegex = new Regex(@"(?<=(.|\n)*qop\s*=\s*)[\w\-.!%*_+`'~]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    Match m = _username.Match(value);
                    if(m != null)
                        {
                        if(!string.IsNullOrEmpty(m.Value))
                            {
							try{
							Username = Syntax.UnescapeString(m.Value.Trim());
							}
							catch(SipException ex)
								{
								throw new SipParseException("UserName", SR.ParseExceptionMessage(value), ex);
								}
							catch(Exception ex)
                                {
                                throw new SipException(SR.GetString(SR.GeneralParseException, m.Value, "UserName"), ex);  
								}
                            }
                        }

                    m = _dResponse.Match(value);
                    if(m != null)
                        {
                        if(!string.IsNullOrEmpty(m.Value))
                            {
							try{
							Response = Syntax.UnescapeString(m.Value.Trim());
							}
							catch(SipException ex)
								{
								throw new SipParseException("Response", SR.ParseExceptionMessage(value), ex);
								}
							catch(Exception ex)
                                {
                                throw new SipException(SR.GetString(SR.GeneralParseException, m.Value, "Response"), ex);  
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
                            CNonce = Syntax.UnescapeString(m.Value.Trim());
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
                    m = _qopRegex.Match(value);
                    if(m != null)
                        {
                        if(!string.IsNullOrEmpty(m.Value))
                            {
                            try
                                {
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
                    }
                }
        }
        /// <summary>
        /// Gets a string representation of the HeaderField value. The HeaderField name is excluded.
        /// </summary>
        /// <remarks>This method overrides the <c>GetStringValue</c> method in <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>. </remarks>
        /// <returns>String representation of the HeaderField value</returns>
        /// <threadsafety static="true" instance="false" />
        protected override string GetStringValueNoParams()
        {
            StringBuilder sb = new StringBuilder(base.GetStringValueNoParams());
            if(Uri.IsUriSet)
                {
                sb.Append("uri=");
                sb.Append(SR.SecurityUriStart);
                sb.Append(System.Uri.EscapeUriString(Uri.ToString()));
                sb.Append(SR.SecurityUriEnd);
                if(HasParameters)
                    {
                    sb.Append(HeaderParameters.Seperator);
                    }
                }
            return sb.ToString();
        }

        #endregion Methods
    }
}