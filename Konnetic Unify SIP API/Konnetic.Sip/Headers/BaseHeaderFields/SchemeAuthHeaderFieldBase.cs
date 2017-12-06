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
    /// The <see cref="T:Konnetic.Sip.Headers.SchemeHeaderFieldBase"/> provides Scheme and Authorization information for HeaderFields.
    /// </summary>
    /// <remarks>
    /// <b>Standards: RFC3261, RFC2616</b>
    /// <para>SIP provides a simple challenge-response authentication mechanism that MAY be used by a server to challenge a client request and by a client to provide authentication information. It uses an extensible, case-insensitive token to identify the authentication scheme, followed by a comma-separated list of attribute-value pairs which carry the parameters necessary for achieving authentication via that scheme.</para>
    /// <para>The 401 (Unauthorized) response message is used by a server to challenge the authorization of a user. This response MUST include a <see cref="T:Konnetic.Sip.Headers.WwwAuthenticateHeaderField"/> header field containing at least one challenge applicable to the requested resource. The 407 (Proxy Authentication Required) response message is used by a proxy to challenge the authorization of a client and MUST include a <see cref="T:Konnetic.Sip.Headers.ProxyAuthenticateHeaderField"/> header field containing at least one challenge applicable to the proxy for the requested resource.</para>
    /// <para>The authentication parameter realm is defined for all authentication schemes</para>
    /// <para>When comparing header fields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular header field, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are casesensitive.</para>
    /// <note type="caution">"Quality values" is a misnomer, since these values merely represent relative degradation in desired quality.</note> 
    /// <para/>The abstract base class is used by <see cref="T:Konnetic.Sip.Headers.AuthorizationHeaderFieldBase"/> and <see cref="T:Konnetic.Sip.Headers.ChallengeHeaderFieldBase"/> headers.  
    /// <b>RFC 3261 Syntax:</b> 
    /// <table >  
    /// <tr><td style="border-bottom:none">auth-scheme = </td><td style="border-bottom:none">token</td></tr> 
    /// <tr><td style="border-bottom:none">auth-param = </td><td style="border-bottom:none">token "=" ( token | quoted-string )</td></tr> 
    /// <tr><td style="border-bottom:none">realm = </td><td style="border-bottom:none">"realm" "=" quoted-string</td></tr> 
    /// <tr><td style="border-bottom:none">algorithm = </td><td style="border-bottom:none">"algorithm" "=" ( "MD5" | "MD5-sess" | token )</td></tr> 
    /// <tr><td style="border-bottom:none">nonce = </td><td style="border-bottom:none">"nonce" "=" quoted-string</td></tr> 
    /// <tr><td style="border-bottom:none">opaque = </td><td style="border-bottom:none">"opaque"" "=" quoted-string</td></tr>
    /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr>
    /// <tr><td style="border-bottom:none">quoted-string = </td><td style="border-bottom:none">DOUBLE_QUOTE *(qdtext / quoted-pair ) DOUBLE_QUOTE</td></tr>
    /// <tr><td style="border-bottom:none">qdtext = </td><td style="border-bottom:none">LWS / %x21 / %x23-5B / %x5D-7E / UTF8-NONASCII</td></tr>
    /// <tr><td style="border-bottom:none">quoted-pair = </td><td style="border-bottom:none">"\\" (%x00-09 / %x0B-0C / %x0E-7F)</td></tr> 
    /// </table>
    /// <example>
    /// <list type="bullet">
    /// <item>WWW-Authenticate: Digest realm="atlanta.com", domain="sip:boxesbybob.com", qop="auth", nonce="f84f1cec41e6cbe5aea9c8e88d359", opaque="", algorithm=MD5</item>  
    /// <item>Proxy-Authenticate: Digest realm="atlanta.com" nonce="f84f1cec41e6cbe5aea9c8e88d359", opaque="", stale=FALSE, algorithm=MD5</item>   
    /// </list>  
    /// </example>
    /// </remarks> 
    /// <seealso cref="T:Konnetic.Sip.Headers.AuthorizationHeaderFieldBase"/> 
    /// <seealso cref="T:Konnetic.Sip.Headers.ChallengeHeaderFieldBase"/> 
    public abstract class SchemeAuthHeaderFieldBase : SecurityHeaderFieldBase
    {
        #region Fields

        private string _scheme;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the algorithm.
        /// </summary>
        /// <remaks>A string indicating a pair of algorithms used to produce the digest and a checksum. If this is not present it is assumed to be "MD5". If the algorithm is not understood, the challenge should be ignored (and a different one used, if there is more than one).</remaks>
        /// <value>The algorithm.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="Algorithm"/>.</exception> 
        /// <exception cref="SipFormatException">Thrown when a user attempts to add non-token characters.</exception>
        public string Algorithm
        {
            get
                {
                SipParameter sp = HeaderParameters["algorithm"];

                if((object)sp == null)
                    {
                    return string.Empty;
                    }

                return sp.Value;
                }
            set
            {
            PropertyVerifier.ThrowOnNullArgument(value, "Algorithm");
                value = value.Trim().ToUpperInvariant();
                PropertyVerifier.ThrowOnInvalidToken(value, "Algorithm");
                if(string.IsNullOrEmpty(value))
                    {
                    RemoveParameter("algorithm");
                    }
                else
                    {
                    HeaderParameters.Set("algorithm", value);
                    }
                }
        }

       

        /// <summary>
        /// Gets or sets the nonce.
        /// </summary>
        /// <remarks>A server-specified data string which should be uniquely generated each time a 401 response is made. It is recommended that this string be base64 or hexadecimal data. Specifically, since the string is passed in the header lines as a quoted string, the double-quote character is not allowed.</remarks>
        /// <value>The nonce.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="Nonce"/>.</exception> 
        public string Nonce
        {
            get
                {
                SipParameter sp = HeaderParameters["nonce"];

                if((object)sp == null)
                    {
                    return string.Empty;
                    }
                return Syntax.UnQuotedString(sp.Value);
                }
            set
            {
            PropertyVerifier.ThrowOnNullArgument(value, "Nonce");
                value = value.Trim().ToLowerInvariant();
                if(string.IsNullOrEmpty(value))
                    {
                    RemoveParameter("nonce");
                    }
                else
                    {
                    SipParameter sp = new SipParameter("nonce", Syntax.ConvertToQuotedString(value), true);
                    sp.CaseSensitiveComparison = true;
                    HeaderParameters.Set(sp);
                    }

                }
        }
         
        /// <summary>
        /// Gets or sets the opaque.
        /// </summary>
        /// <remarks>A string of data, specified by the server, which should be returned by the client unchanged in the Authorization header of subsequent requests with URIs in the same protection space. It is recommended that this string be base64 or hexadecimal data.</remarks>
        /// <value>The opaque.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="Opaque"/>.</exception> 
        public string Opaque
        {
            get
                {
                SipParameter sp = HeaderParameters["opaque"];

                if((object)sp == null)
                    {
                    return string.Empty;
                    }

                return Syntax.UnQuotedString(sp.Value);

                }
            set
            {
                PropertyVerifier.ThrowOnNullArgument(value, "Opaque");
                value = value.Trim().ToLowerInvariant();
                if(string.IsNullOrEmpty(value))
                    {
                    RemoveParameter("opaque");
                    }
                else
                    {
                    SipParameter sp = new SipParameter("opaque", Syntax.ConvertToQuotedString(value), true);
                    sp.CaseSensitiveComparison = true;
                    HeaderParameters.Set(sp);
                    }
                }
        }

        /// <summary>
        /// Gets or sets the realm.
        /// </summary>
        /// <remarks>A string to be displayed to users so they know which username and password to use. This string should contain at least the name of the host performing the authentication and might additionally indicate the collection of users who might have access. An example might be "registered_users@gotham.news.com".</remarks>
        /// <value>The realm.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="Opaque"/>.</exception>
        public string Realm
        {
            get
                {
                SipParameter sp = HeaderParameters["realm"];

                if((object)sp == null)
                    {
                    return string.Empty;
                    }
                return Syntax.UnQuotedString(sp.Value);
                }
            set
            {
            PropertyVerifier.ThrowOnNullArgument(value, "Realm");
                value = value.Trim();
                if(string.IsNullOrEmpty(value))
                    {
                    RemoveParameter("realm");
                    }
                else
                    {
                    SipParameter sp = new SipParameter("realm", Syntax.ConvertToQuotedString(value), true);
                    sp.CaseSensitiveComparison = true;
                    HeaderParameters.Set(sp);
                    }
                }
        }

        /// <summary>
        /// Gets or sets the scheme.
        /// </summary>
        /// <remarks>Note that due to its weak security, the usage of "Basic" authentication has been deprecated. Servers must not accept credentials using the "Basic" authorization scheme, and servers also must not challenge with "Basic".</remarks>
        /// <value>The scheme.</value>
        /// <exception cref="SipException">Thrown when Basic authentication is applied.</exception> 
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="Scheme"/>.</exception> 
        /// <exception cref="SipFormatException">Thrown when a user attempts to add non-token characters.</exception>
        public string Scheme
        {
            get
                {
                return _scheme;
                }
            protected set
            {
            PropertyVerifier.ThrowOnNullArgument(value, "Scheme");
            value = value.Trim();
            PropertyVerifier.ThrowOnInvalidToken(value, "Scheme");

            if(value.ToUpperInvariant() == "BASIC")
                {
                throw new SipException("The usage of \"Basic\" authentication has been deprecated from SIP.");
                }
                _scheme = value;
                }
        }

        #endregion Properties

        #region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="SchemeHeaderFieldBase"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <overloads>
		/// <summary>The method has one overload.</summary>
		/// </overloads>
        protected SchemeAuthHeaderFieldBase()
            : this("Digest")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SchemeHeaderFieldBase"/> class.
        /// </summary>
        /// <param name="scheme">The scheme.</param>
        protected SchemeAuthHeaderFieldBase(string scheme)
            : base()
            {
            PropertyVerifier.ThrowOnNullArgument(scheme, "scheme");
            RegisterKnownParameter("realm");
            RegisterKnownParameter("opaque");
            RegisterKnownParameter("nonce");
            RegisterKnownParameter("algorithm"); 
            Scheme = scheme;
        }

        #endregion Constructors

        #region Methods
        ///<summary>
        /// Compare this SIP Header for equality with another <see cref="T:Konnetic.Sip.Headers.SchemeHeaderFieldBase"/> object.</summary>
        /// <remarks>
        /// All optional headers are compared using object equality that is each field in the header is used for comparision. When comparing HeaderFields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular HeaderField, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are case-sensitive. 
        /// </remarks>
        /// <param name="other">The <see cref="T:Konnetic.Sip.Headers.SchemeHeaderFieldBase"/> to compare to this instance.</param>
        /// <returns><c>true</c> if <paramref name="other" /> is an instance of this class representing the same SIP HeaderField as this, <c>false</c> otherwise.</returns>    
        /// <overloads>
        /// <summary>This method is overloaded.</summary>
        /// <remarks>Overloads allow for equality against <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>, <see cref="T:System.Object"/> and another instance of this HeaderField.</remarks>
        /// </overloads>  
        /// <threadsafety static="true" instance="false" />
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public bool Equals(SchemeAuthHeaderFieldBase other)
            {
            if((object)other == null)
                {
                return false;
                }
            return base.Equals((SecurityHeaderFieldBase)other) && Scheme.Equals(other.Scheme, StringComparison.OrdinalIgnoreCase);
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

            SchemeAuthHeaderFieldBase p = obj as SchemeAuthHeaderFieldBase;
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
            return base.IsValid() && !string.IsNullOrEmpty(Scheme) && HeaderParameters.Count > 0;
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
                Scheme = string.Empty;

                Regex _schemeRegex = new Regex(@"(?<=^\s*)[\w-.!%_*+`'~]+(?=\s*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                Match m = _schemeRegex.Match(value);
                    if(m != null)
                        {
                        if(!string.IsNullOrEmpty(m.Value))
                            {try{
                            Scheme = m.Value;
							}
							catch(SipException ex)
								{
								throw new SipParseException("Scheme", SR.ParseExceptionMessage(value), ex);
								}
							catch(Exception ex)
                                {
                                throw new SipParseException(SR.GetString(SR.GeneralParseException, m.Value, "Scheme"), ex);  
								}
                            }
                        }
                    Regex _schemeReplace = new Regex(@"^\s*[\w-.!%_*+`'~]+\s*", RegexOptions.Compiled | RegexOptions.IgnoreCase );
                    value = _schemeReplace.Replace(value, "");

                    base.Parse(value);

                    if(!string.IsNullOrEmpty(value))
                        {
                        Regex _realmRegex = new Regex(@"(?<=(.|\n)*realm\s*=\s*"")(.|\n)*?([^\\](?="")|\\\\(?=""))", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        Regex _nonceRegex = new Regex(@"(?<=(.|\n)*nonce\s*=\s*"")(.|\n)*?([^\\](?="")|\\\\(?=""))", RegexOptions.Compiled | RegexOptions.IgnoreCase);
             			Regex _algorithmRegex = new Regex(@"(?<=(.|\n)*algorithm\s*=\s*)[\w\-.!%*_+`'~]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        Regex _opaqueRegex = new Regex(@"(?<=(.|\n)*opaque\s*=\s*"")(.|\n)*?([^\\](?="")|\\\\(?=""))", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                        m = _realmRegex.Match(value);
                        if(m != null)
                            {
                            if(!string.IsNullOrEmpty(m.Value))
                                {
								try{
                                Realm = Syntax.UnescapeString(m.Value.Trim());
								}
								catch(SipException ex)
									{
									throw new SipParseException("Realm", SR.ParseExceptionMessage(value), ex);
									}
								catch(Exception ex)
                                    {
                                    throw new SipParseException(SR.GetString(SR.GeneralParseException, m.Value, "Realm"), ex);  
									}
                                }
                            }

                        m = _nonceRegex.Match(value);
                        if(m != null)
                            {
                            if(!string.IsNullOrEmpty(m.Value))
                                {
								try{
                                Nonce = Syntax.UnescapeString(m.Value.Trim());
								}
								catch(SipException ex)
									{
									throw new SipParseException("Nonce", SR.ParseExceptionMessage(value), ex);
									}
								catch(Exception ex)
                                    {
                                    throw new SipException(SR.GetString(SR.GeneralParseException, m.Value, "Nonce"), ex);  
									}
                                }
                            }

                        m = _algorithmRegex.Match(value);
                        if(m != null)
                            {
                            if(!string.IsNullOrEmpty(m.Value))
                                {
								try{
                                Algorithm = m.Value.Trim();
								}
								catch(SipException ex)
									{
									throw new SipParseException("Algorithm", SR.ParseExceptionMessage(value), ex);
									}
								catch(Exception ex)
                                    {
                                    throw new SipException(SR.GetString(SR.GeneralParseException, m.Value, "Algorithm"), ex); 
									}
                                }
                            }

                        m = _opaqueRegex.Match(value);
                        if(m != null)
                            {
                            if(!string.IsNullOrEmpty(m.Value))
                                {
								try{
                                Opaque = Syntax.UnescapeString(m.Value.Trim());
								}
								catch(SipException ex)
									{
									throw new SipParseException("Opaque", SR.ParseExceptionMessage(value), ex);
									}
								catch(Exception ex)
                                    {
                                    throw new SipException(SR.GetString(SR.GeneralParseException, m.Value, "Opaque"), ex);  
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
            StringBuilder sb = new StringBuilder(Scheme); 
            
            if(Scheme.Length > 0)
                {
                sb.Append(SR.SingleWhiteSpace);
                } 

            return sb.ToString();
        }

        #endregion Methods
    }
}