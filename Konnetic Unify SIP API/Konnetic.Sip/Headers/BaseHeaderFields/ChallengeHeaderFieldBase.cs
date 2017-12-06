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
    /// The <see cref="T:Konnetic.Sip.Headers.ChallengeHeaderFieldBase"/> provides Authorization credential information for HeaderFields. 
    /// </summary>
    /// <remarks> 
    /// <b>Standards: RFC3261, RFC2616, RFC2617</b>
    /// <para/>
    /// The abstract base class is used by <see cref="T:Konnetic.Sip.Headers.ProxyAuthenticateHeaderField"/> and <seealso cref="T:Konnetic.Sip.Headers.WwwAuthenticateHeaderField"/> headers.  
    /// <para/>
    /// SIP provides a simple challenge-response authentication mechanism that may be used by a server to challenge a client request and by a client to provide authentication information. 
    /// <para/>
    /// <b>RFC 3261 Syntax:</b> 
    /// <table >
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
    /// <example>
    /// <list type="bullet">
    /// <item>WWW-Authenticate: Digest realm="atlanta.com", domain="sip:boxesbybob.com", qop="auth", nonce="f84f1cec41e6cbe5aea9c8e88d359", opaque="", algorithm=MD5</item>  
    /// <item>Proxy-Authenticate: Digest realm="atlanta.com" nonce="f84f1cec41e6cbe5aea9c8e88d359", opaque="", stale=FALSE, algorithm=MD5</item>  
    /// </list> 
    /// </example>
    /// </remarks> 
    /// <seealso cref="T:Konnetic.Sip.Headers.SchemeHeaderFieldBase"/> 
    /// <seealso cref="T:Konnetic.Sip.Headers.ProxyAuthenticateHeaderField"/> 
    /// <seealso cref="T:Konnetic.Sip.Headers.WwwAuthenticateHeaderField"/> 
    public abstract class ChallengeHeaderFieldBase : SchemeAuthHeaderFieldBase
    {
        #region Fields

        private Uri _uri;
        private bool _uriSet;

        #endregion Fields

        #region Properties
        /// <summary>
        /// Gets the auth parameters.
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
        /// Gets or sets the domain.
        /// </summary>
        /// <remarks>A quoted, space-separated list of URIs, as specified in RFC XURI, that define the protection space. If a URI is an abs_path, it is relative to the canonical root URL of the server being accessed. An absoluteURI in this list may refer to a different server than the one being accessed. The client can use this list to determine the set of URIs for which the same authentication information may be sent: any URI that has a URI in this list as a prefix (after both have been made absolute) may be assumed to be in the same protection space. If this directive is omitted or its value is empty, the client should assume that the protection space consists of all URIs on the responding server.</remarks>
        /// <value>The domain.</value>
        public Uri Domain
        {
            get { return _uri; }
            set
                {
                _uriSet = !((object)value == null);
                _uri = value;
                }
        }

        /// <summary>
        /// Gets or sets the message qop. Indicates what "quality of protection" the client has applied to the message.
        /// </summary>
        /// <remarks>Indicates what "quality of protection" the client has applied to the message. If present, its value must be one of the alternatives the server indicated it supports in the WWW-Authenticate header. These values affect the computation of the request-digest. Note that this is a single token, not a quoted list of alternatives as in WWW- Authenticate. This directive is optional in order to preserve backward compatibility with a minimal implementation of RFC 2069, but SHOULD be used if the server indicated that qop is supported by providing a qop directive in the WWW-Authenticate header field.</remarks>
        /// <value>The message qop.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="MessageQop"/>.</exception> 
        /// <exception cref="SipFormatException">Thrown when a user attempts to add non-token characters.<paramref name="MessageQop"/>.</exception> 
        public string MessageQop
        {
            get
                {
                SipParameter sp = HeaderParameters["qop"];

                if((object)sp == null)
                    {
                    return string.Empty;
                    }

                return Syntax.UnQuotedString(sp.Value);
                }
            set
                { 
                PropertyVerifier.ThrowOnNullArgument(value, "MessageQop");
                value = value.Trim();
                //Challenge Qop allows comma delimited values that are then quoted.
                PropertyVerifier.ThrowOnInvalidQuotedTokenWithComma(value, "MessageQop");
                if(string.IsNullOrEmpty(value.Trim()))
                    {
                    RemoveParameter("qop");
                    }
                else
                    {
                    //Different to other Security headerfields the Qop is quoted.
                    SipParameter sp = new SipParameter("qop", Syntax.QuoteString(value), true);
                    sp.CaseSensitiveComparison = true;
                    HeaderParameters.Set(sp);
                    }
                }
        }

        /// <summary>
        /// Gets or sets a value indicating the stale value. It is a flag, indicating that the previous request from the client was rejected because the nonce value was stale.
        /// </summary>
        /// <remarks>A flag, indicating that the previous request from the client was rejected because the nonce value was stale. If stale is TRUE (case-insensitive), the client may wish to simply retry the request with a new encrypted response, without reprompting the user for a new username and password. The server should only set stale to TRUE if it receives a request for which the nonce is invalid but with a valid digest for that nonce (indicating that the client knows the correct username/password). If stale is FALSE, or anything other than TRUE, or the stale directive is not present, the username and/or password are invalid, and new values must be obtained.</remarks>
        /// <value><c>true</c> if stale; otherwise, <c>false</c>.</value>
        /// <exception cref="SipFormatException">Thrown when the underlying parameter string cannot be converted to a bool.</exception> 
        public bool Stale
            {
            [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
            get
                {
                SipParameter sp = HeaderParameters["stale"];

                if((object)sp == null)
                    {
                    return false;
                    }
                try
                    {
                    bool retVal = bool.Parse(sp.Value);
                    return retVal;
                    }
                catch(FormatException ex)
                    {
                    throw new SipFormatException("Cannot convert Stale to bool.", ex);
                    }
                }
            set
            {
            HeaderParameters.Set("stale", value.ToString().ToUpperInvariant());
                }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the domain has been set.
        /// </summary>
        /// <value><c>true</c> if the domain has been set; otherwise, <c>false</c>.</value>
        protected bool DomainSet
        {
            get { return _uriSet; }
            set { _uriSet = value; }
        }

        #endregion Properties

        #region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ChallengeHeaderFieldBase"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <overloads>
		/// <summary>The method has two overloads.</summary>
		/// </overloads>
        protected ChallengeHeaderFieldBase()
            : this("Digest")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChallengeHeaderFieldBase"/> class.
        /// </summary>
        /// <param name="scheme">The scheme.</param>
        protected ChallengeHeaderFieldBase(string scheme)
            : base(scheme)
            {
            RegisterKnownParameter("stale");
            RegisterKnownParameter("qop");
            AllowMultiple = true;
            _uri = new SipUri();
            _uriSet = false;
        }

        #endregion Constructors

        #region Methods
        /// <summary>
        /// Compare this SIP Header for equality with another <see cref="T:Konnetic.Sip.Headers.ChallengeHeaderFieldBase"/> object.</summary>
        /// <remarks>
        /// All optional headers are compared using object equality that is each field in the header is used for comparision. When comparing HeaderFields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular HeaderField, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are case-sensitive. 
        /// </remarks>
        /// <param name="other">The <see cref="T:Konnetic.Sip.Headers.ChallengeHeaderFieldBase"/> to compare to this instance.</param>
        /// <returns><c>true</c> if <paramref name="other" /> is an instance of this class representing the same SIP HeaderField as this, <c>false</c> otherwise.</returns>    
        /// <overloads>
        /// <summary>This method is overloaded.</summary>
        /// <remarks>Overloads allow for equality against <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>, <see cref="T:System.Object"/> and another instance of this HeaderField.</remarks>
        /// </overloads>  
        /// <threadsafety static="true" instance="false" />
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public bool Equals(ChallengeHeaderFieldBase other)
            {
            if((object)other == null)
                {
                return false;
                }
            bool baseVal = base.Equals((SchemeAuthHeaderFieldBase)other);
            if(baseVal == true)
                {
                return baseVal && Domain.Equals(other.Domain);
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

            ChallengeHeaderFieldBase p = obj as ChallengeHeaderFieldBase;
            if((object)p == null)
                {
                return false;
                }

            return this.Equals(p);
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
            if(_uriSet)
            { 
            return base.IsValid() && !string.IsNullOrEmpty(Domain.AbsoluteUri) && !string.IsNullOrEmpty(Scheme) && HeaderParameters.Count > 0;
            }
            else
            {
            return base.IsValid() && !string.IsNullOrEmpty(Scheme) && HeaderParameters.Count > 0;
            }
        }
        /// <summary>
        /// Parses string representation of the HeaderField.
        /// </summary>
        /// <remarks> 
        /// <b>RFC 3261 Syntax:</b> 
        /// <table >
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
        /// <example>
        /// <list type="bullet">
        /// <item>WWW-Authenticate: Digest realm="atlanta.com", domain="sip:boxesbybob.com", qop="auth", nonce="f84f1cec41e6cbe5aea9c8e88d359", opaque="", stale=FALSE, algorithm=MD5</item>  
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

                //Quoted and comma delimited
                //Commas can be interpreted by the ParamitizedHeaderField as a new field, so we need to deal with it here.
                Regex _qop = new Regex(@"(?<=(.|\n)*qop\s*=\s*"")[,\w\-.!%*_+`'~]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                Match mQop = _qop.Match(value);
                Regex _qopReplace = new Regex(@"(?<=(.|\n)*),?qop\s*=\s*""[,\w\-.!%*_+`'~,]+""", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                value = _qopReplace.Replace(value, "");

                Regex _uriRegex = new Regex(@"(?<=(.|\n)*domain\s*=\s*"")[^""]*", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                Match mDomain = _uriRegex.Match(value);
                Regex _uriReplace = new Regex(@"(?<=(.|\n)*)\s*domain\s*=\s*""[^""]*"",?", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                value = _uriReplace.Replace(value, "");

                base.Parse(value);
                MessageQop = String.Empty;
                Domain = new SipUri();
                _uriSet = false;
                ClearStale();
                if(mQop != null)
                    {
                    if(!string.IsNullOrEmpty(mQop.Value))
                        {
						try{
                        MessageQop = mQop.Value.Trim();

						}
						catch(SipException ex)
							{
							throw new SipParseException("MessageQop", SR.ParseExceptionMessage(value), ex);
							}
						catch(Exception ex)
                            {
                            throw new SipParseException(SR.GetString(SR.GeneralParseException, mQop.Value, "MessageQop"), ex);  
							}
                        }
                    }

                if(mDomain != null)
                    {
                    if(!string.IsNullOrEmpty(mDomain.Value))
                        {try{
						Domain = new Uri(mDomain.Value);
						}
						catch(SipException ex)
							{
							throw new SipParseException("Domain", SR.ParseExceptionMessage(value), ex);
							}
						catch(Exception ex)
                            {
                            throw new SipParseException(SR.GetString(SR.GeneralParseException, mDomain.Value, "Domain"), ex);   
							}
                        }
                    }

                if(!string.IsNullOrEmpty(value))
                    {

                    Regex _stale = new Regex(@"(?<=(.|\n)*stale\s*=\s*)(true|false)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        Match m = _stale.Match(value);
                        if(m != null)
                            {
                            if(!string.IsNullOrEmpty(m.Value))
                                {
                                try
                                    {
                                    Stale = bool.Parse(m.Value);
									}
								catch(SipException ex)
									{
                                    throw new SipParseException("Stale", SR.ParseExceptionMessage(value), ex);
									}
                                catch(FormatException ex)
                                    {
                                    throw new SipParseException(SR.GetString(SR.GeneralParseException, m.Value, "Stale"), ex); 
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
            if(_uriSet)
                {
                sb.Append("domain=");
                sb.Append(SR.SecurityUriStart);
                sb.Append(System.Uri.EscapeUriString(Domain.ToString()));
                sb.Append(SR.SecurityUriEnd);
                if(HasParameters)
                    {
                    sb.Append(HeaderParameters.Seperator);
                    }
                }
            return sb.ToString();
        }

        private void ClearStale()
        {
            RemoveParameter("stale");
        }

        #endregion Methods
    }
}