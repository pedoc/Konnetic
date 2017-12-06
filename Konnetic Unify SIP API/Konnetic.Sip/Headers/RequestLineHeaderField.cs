/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

using Konnetic.Sip.Messages;

namespace Konnetic.Sip.Headers
{
    /// <summary>SIP requests are distinguished by having a Request-Line for a start-line. 
    /// </summary>
    /// <remarks> 
    /// <b>Standards: RFC3261, RFC2616, RFC2806</b>
    /// <para/>
    /// A Request-Line contains a method name, a Request-URI, and the protocol version separated by a single space (SP) character. The Request-Line ends with CRLF. No CR or LF are allowed except in the end-of-line CRLF sequence. No linear whitespace (LWS) is allowed in any of the elements.
    /// <para/>
    /// <list type="bullet">
    /// <item><i>Method:</i> SIP defines six methods: REGISTER for registering contact information, INVITE, ACK, and CANCEL for setting up sessions, BYE for terminating sessions, and OPTIONS for querying servers about their capabilities. SIP extensions, documented in standards track RFCs, may define additional methods.</item>
    /// <item><i>Request-URI:</i> The Request-URI is a SIP or SIPS URI as described in Section 19.1 or a general URI (RFC 2396 [5]). It indicates the user or service to which this request is being addressed. The Request-URI must not contain unescaped spaces or control characters and must not be enclosed in "<>". SIP elements may support Request-URIs with schemes other than "sip" and "sips", for example the "tel" URI scheme of RFC 2806. SIP elements may translate non-SIP URIs using any mechanism at their disposal, resulting in SIP URI, SIPS URI, or some other scheme.</item>
    /// <item><i>SIP-Version:</i> Both request and response messages include the version of SIP in use. To be compliant with the SIP specification, applications sending SIP messages must include a SIP-Version of "SIP/2.0". The SIP-Version string is caseinsensitive, but implementations must send upper-case.</item>
    /// </list>
    /// <para/>
    /// <b>RFC 3261 Syntax:</b> 
    /// <table >
    /// <tr><td colspan="2" style="border-bottom:none">Method SPACE Request-URI SPACE SIP-Version CRLF</td></tr> 
    /// <tr><td style="border-bottom:none">Request-URI = </td><td style="border-bottom:none">SIP-URI / SIPS-URI / absoluteURI</td></tr>
    /// <tr><td style="border-bottom:none">absoluteURI = </td><td style="border-bottom:none">scheme ":" ( ( "//" authority [ abs-path ] / "/" path-segments ) [ "?" query ] / opaque-part )</td></tr> 
    /// </table>  
    /// <para/>
    /// <note type="implementnotes">This HeaderField does not allow HeaderField grouping.</note> 
    /// <example>
    /// <list type="bullet">
    /// <item>INVITE sip:bob@192.0.2.4 SIP/2.0</item> 
    /// <item>REGISTER sip:registrar.biloxi.com SIP/2.0</item> 
    /// </list> 
    /// </example>
    /// </remarks> 
    /// <seealso cref="T:Konnetic.Sip.Headers.MediaTypeHeaderFieldBase"/>
    public sealed class RequestLineHeaderField
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private SipMethod _method;

        /// <summary>
        /// 
        /// </summary>
        private SipUri _uri;

        /// <summary>
        /// 
        /// </summary>
        private string _scheme;

        /// <summary>
        /// 
        /// </summary>
        private string _version;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        /// <value>The method.</value>
        public SipMethod Method
        {
            get { return _method; }
              set { _method = value; }
        }

        /// <summary>
        /// Gets or sets the request URI.
        /// </summary>
        /// <value>The request URI.</value>
        public SipUri RequestUri
        {
            get { return _uri; }
              set { _uri = value; }
        }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        /// <summary>
        /// Gets or sets the scheme.
        /// </summary>
        /// <value>The scheme.</value>
        internal string Scheme
        {
            get { return _scheme; }
            set {
            if(value.ToUpperInvariant() != "SIP")
                {
                throw new SipException(string.Format(CultureInfo.InvariantCulture, "Specified scheme is not supported ({0}).", value));
                }
                _scheme = value; }
        }

        #endregion Properties

        #region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="RequestLineHeaderField"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <overloads>
		/// <summary>The method has one overload.</summary>
		/// </overloads>
        public RequestLineHeaderField()
        {
            Version = SR.SIPVersionNumber;
            Scheme = SR.DefaultSipScheme;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestLineHeaderField"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public RequestLineHeaderField(string value)
        {
            PropertyVerifier.ThrowOnNullArgument(value, "value");
            Parse(value);
        }

        #endregion Constructors

        #region Methods

		/// <summary>
		/// Performs an implicit conversion from <see cref="System.String"/> to <see cref="Konnetic.Sip.Headers.RequestLineHeaderField"/>.
		/// </summary>
		/// <param name="value">The string value representing the HeaderField.</param>
        /// <returns>A new <see cref="Konnetic.Sip.Headers.RequestLineHeaderField"/> populated from the passed in string</returns>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="value"/>.</exception>
        public static implicit operator RequestLineHeaderField(String value)
        {
            PropertyVerifier.ThrowOnNullArgument(value,"value");
            RequestLineHeaderField hf = new RequestLineHeaderField();
            hf.Parse(value);
            return hf;
        }

		/// <summary>
		/// Performs an explicit conversion from <see cref="Konnetic.Sip.Headers.RequestLineHeaderField"/> to <see cref="System.String"/>.
		/// </summary>
		/// <param name="headerField">The HeaderField to convert.</param>
		/// <returns>A string representation of the HeaderField.</returns>
		/// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="headerField"/>.</exception>
        public static explicit operator string(RequestLineHeaderField headerField)
        {
            PropertyVerifier.ThrowOnNullArgument(headerField, "headerField");
            return headerField.ToString();
        }
        /// <summary>
        /// Creates a deap  copy of this instance.
        /// </summary> 
        /// <remarks>Creates and returns a deep copy of the Header. This method ensures a deep copy of the HeaderField, when a message is cloned the HeaderField can be modified without effecting the original HeaderField in the message.</remarks>
        /// <returns>A deep copy of <see cref="T:Konnetic.Sip.Headers.RequestLineHeaderField"/>.</returns>
        /// <threadsafety static="true" instance="false" /> 
        public RequestLineHeaderField Clone()
        {
            return new RequestLineHeaderField(ToString());
        }

        /// <summary>
        /// Parses string representation of the HeaderField.
        /// </summary>
        /// <remarks>  
        /// <b>RFC 3261 Syntax:</b> 
        /// <table >
        /// <tr><td colspan="2" style="border-bottom:none">Method SPACE Request-URI SPACE SIP-Version CRLF</td></tr> 
        /// <tr><td style="border-bottom:none">Request-URI = </td><td style="border-bottom:none">SIP-URI / SIPS-URI / absoluteURI</td></tr>
        /// <tr><td style="border-bottom:none">absoluteURI = </td><td style="border-bottom:none">scheme ":" ( ( "//" authority [ abs-path ] / "/" path-segments ) [ "?" query ] / opaque-part )</td></tr> 
        /// </table>  
        /// <para/>
        /// <note type="implementnotes">This HeaderField does not allow HeaderField grouping.</note> 
        /// <example>
        /// <list type="bullet">
        /// <item>INVITE sip:bob@192.0.2.4 SIP/2.0</item> 
        /// <item>REGISTER sip:registrar.biloxi.com SIP/2.0</item> 
        /// </list> 
        /// </example>
        /// </remarks>
        /// <param name="value">The HeaderField string to parse.</param>
        /// <exception cref="T:Konnetic.Sip.SipParseException">Thrown when an invalid (non-standard) value is encountered.</exception>
        /// <exception cref="T:Konnetic.Sip.SipException">Thrown when a processing exception is encountered.</exception>
        /// <threadsafety static="true" instance="false" />
        public void Parse(string value)
        {
            value=Syntax.ReplaceFolding(value);
            Regex _uriRegex = new Regex(@"(?<=^\s*\w+\s)[^\s]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex _methodRegex = new Regex(@"(?<=^\s*)\w+(?=\s)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex _schemeRegex = new Regex(@"(?<=^\s*\w+\s+[^\s]+\s+)SIP(?=/)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex _versionRegex = new Regex(@"(?<=^\s*\w+\s+[^\s]+\s+SIP/)[0-9.]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            Match m = _uriRegex.Match(value);
            string tValue = m.Value;
            if((object)m == null || string.IsNullOrEmpty(tValue))
            {
            throw new SipUriFormatException("Uri is required for Request Line Field");
            }
            if(!tValue.TrimStart().StartsWith("sip",StringComparison.OrdinalIgnoreCase))
            {
            tValue = "sip:" + tValue.TrimStart();
            }
            RequestUri = new SipUri(tValue);

            m = _methodRegex.Match(value);
            tValue = m.Value;
            if((object)m == null || string.IsNullOrEmpty(tValue))
            {
            throw new SipFormatException("Method is required for Request Line Field");
            }
            Method = new SipMethod(m.Value);

            m = _schemeRegex.Match(value);
            tValue = m.Value;
            if((object)m == null || string.IsNullOrEmpty(tValue))
            {
            throw new SipFormatException("Scheme is required for Request Line Field");
            }
            Scheme = m.Value;

            m = _versionRegex.Match(value);
            tValue = m.Value;
            if((object)m == null || string.IsNullOrEmpty(tValue))
            {
            throw new SipFormatException("Scheme Version is required for Request Line Field");
            }
            Version = m.Value;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if(Method == SipMethod.Register)
            {
            //TODO Remove UserInfo for Register
            //Uri.UserName = "";
            //Uri.Password = "";
            }
            StringBuilder sb = new StringBuilder(50);
            sb.Append(Method);
            sb.Append(SR.SingleWhiteSpace);
            sb.Append(RequestUri.ToString());
            sb.Append(SR.SingleWhiteSpace);
            sb.Append(Scheme);
            sb.Append(SR.SipVersionSeperator);
            sb.Append(Version);
            return sb.ToString();
        }

        /// <summary>
        /// Validates this instance against the standard. Indicated whether it reaches minimum compliance.
        /// </summary> 
        /// <returns>
        /// 	<c>true</c> if instance represents a valid RequestLine; otherwise, <c>false</c>.
        /// </returns>
        /// <threadsafety static="true" instance="false" />
        internal bool IsValid()
        {
            return RequestUri != null && !string.IsNullOrEmpty(RequestUri.Host) && !string.IsNullOrEmpty(Method);
        }

        #endregion Methods
    }
}