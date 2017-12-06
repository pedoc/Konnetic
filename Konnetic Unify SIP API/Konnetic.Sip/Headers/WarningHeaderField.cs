/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

using Konnetic.Sip.Messages;

namespace Konnetic.Sip.Headers
    {
    /// <summary>The Warning HeaderField is used to carry additional information about the status of a response. Warning HeaderField values are sent with responses and contain a three-digit warning code, host name, and warning text. 
    /// </summary>
    /// <remarks> 
    /// <b>Standards: RFC3261, RFC2616</b>
    /// <para/>
    /// The "warn-text" should be in a natural language that is most likely to be intelligible to the human user receiving the response. This decision can be based on any available knowledge, such as the location of the user, the Accept-Language field in a request, or the Content-Language field in a response.
    /// <para/>
    /// The currently-defined "warn-code"s are listed below, with a recommended warn-text in English and a description of their meaning. These warnings describe failures induced by the session description. The first digit of warning codes beginning with "3" indicates warnings specific to SIP.Warnings 300 through 329 are reserved for indicating problems with keywords in the session description, 330 through 339 are warnings related to basic network services requested in the session description, 370 through 379 are warnings related to quantitative QoS parameters requested in the session description, and 390 through 399 are miscellaneous warnings that do not fall into one of the above categories.
    /// <para/>
    /// <note type="caution">1xx and 2xx have been taken by HTTP/1.1.</note>
    /// <para/>
    /// <list type="bullet">
    /// <item><b>300 Incompatible network protocol:</b> One or more network protocols contained in the session description are not available.</item>
    /// <item><b>301 Incompatible network address formats:</b> One or more network address formats contained in the session description are not available.</item>
    /// <item><b>302 Incompatible transport protocol:</b> One or more transport protocols described in the session description are not available.</item>
    /// <item><b>303 Incompatible bandwidth units:</b> One or more bandwidth measurement units contained in the session description were not understood.</item>
    /// <item><b>304 Media type not available:</b> One or more media types contained in the session description are not available. 305 Incompatible media format: One or more media formats contained in the session description are not available.</item>
    /// <item><b>305 Incompatible media format:</b> One or more media formats contained in the session description are not available.</item>
    /// <item><b>306 Attribute not understood:</b> One or more of the media attributes in the session description are not supported. 307 Session description parameter not understood: A parameter other than those listed above was not understood.</item>
    /// <item><b>330 Multicast not available:</b> The site where the user is located does not support multicast.</item>
    /// <item><b>331 Unicast not available:</b> The site where the user is located does not support unicast communication (usually due to the presence of a firewall).</item>
    /// <item><b>370 Insufficient bandwidth:</b> The bandwidth specified in the session description or defined by the media exceeds that known to be available.</item>
    /// <item><b>399 Miscellaneous warning:</b> The warning text can include arbitrary information to be presented to a human user or logged. A system receiving this warning must not take any automated action.</item>
    /// </list>
    /// <b>RFC 3261 Syntax:</b> 
    /// <table >
    /// <tr><td colspan="2" style="border-bottom:none">"Warning" ":" warning-value *("," warning-value)</td></tr> 
    /// <tr><td style="border-bottom:none">warning-value = </td><td style="border-bottom:none">3DIGIT SPACE warn-agent SPACE quoted-string</td></tr> 
    /// <tr><td style="border-bottom:none">warn-agent = </td><td style="border-bottom:none">hostport / pseudonym ; the name or pseudonym of the server adding ; the Warning header, for use in debugging</td></tr>
    /// <tr><td style="border-bottom:none">pseudonym = </td><td style="border-bottom:none">token</td></tr> 
    /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr>
    /// <tr><td style="border-bottom:none">quoted-string = </td><td style="border-bottom:none">SWS DOUBLE_QUOTE *(qdtext / quoted-pair ) DOUBLE_QUOTE</td></tr>
    /// <tr><td style="border-bottom:none">qdtext = </td><td style="border-bottom:none">LWS / %x21 / %x23-5B / %x5D-7E / UTF8-NONASCII</td></tr>
    /// <tr><td style="border-bottom:none">LWS = </td><td style="border-bottom:none">[*WSP CRLF] 1*WSP ; linear whitespace</td></tr>
    /// <tr><td style="border-bottom:none">SWS = </td><td style="border-bottom:none">[LWS] ; sep whitespace</td></tr>
    /// <tr><td style="border-bottom:none">host = </td><td style="border-bottom:none">hostname / IPv4address / IPv6reference</td></tr>
    /// <tr><td style="border-bottom:none">port = </td><td style="border-bottom:none">1*DIGIT</td></tr>
    /// </table>  
    /// <para/>
    /// <note type="implementnotes">This HeaderField allows HeaderField grouping (<see cref="T:Konnetic.Sip.Headers.HeaderFieldGroup{T}"/>).</note>
    /// <example>
    /// <list type="bullet">
    /// <item>Warning: 307 isi.edu "Session parameter 'foo' not understood"</item>
    /// <item>Warning: 301 isi.edu "Incompatible network address type 'E.164'"</item>
    /// </list>
    /// </example>
    /// </remarks>
    /// <seealso cref="T:Konnetic.Sip.Headers.MediaTypeHeaderFieldBase"/>
    public sealed class WarningHeaderField : HeaderFieldBase
    {
        #region Fields

        internal const string CompareName = "WARNING";
        internal const string CompareShortName = CompareName;
        internal const string LongName = "Warning";

        private Int16? _code; 
        private string _agent;
        private string _warnText;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the agent.
        /// </summary>
        /// <value>The agent.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="Agent"/>.</exception> 
        /// <exception cref="SipFormatException">Thrown when a user attempts to add non-token characters.</exception>
        public string Agent
        {
            get { return _agent; }
            set
            {
            PropertyVerifier.ThrowOnNullArgument(value, "Agent");
            if(!(Syntax.IsUnReservedHost(value) || Syntax.IsToken(value)))
                {
                PropertyVerifier.ThrowOnInvalidToken(value, "Agent");
                }
            _agent = value;
            }
        }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
        public Int16? Code
        {
            get { return _code; }
            set
            {
            if(value.HasValue)
                {
                if(value < 100 || value > 999)
                    {
                    throw new SipException("Code must be three digits.");
                    }  
                } 
            _code = value;
            }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="Text"/>.</exception> 
        public string Text
        {
            get { return Syntax.UnQuotedString(_warnText); }
            set
            {
            PropertyVerifier.ThrowOnNullArgument(value, "Text");
            _warnText = Syntax.ConvertToQuotedString(value);
            }
        }

        #endregion Properties

        #region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="WarningHeaderField"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <overloads>
		/// <summary>The method has two overloads.</summary>
		/// </overloads>
        public WarningHeaderField()
            : base()
        {
            Agent = string.Empty;
            Text = string.Empty;
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WarningHeaderField"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="agent">The agent.</param>
        /// <param name="text">The text.</param>
        public WarningHeaderField(SipWarningCode code, string agent, string text)
            : this(((Int16)code), agent, text)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WarningHeaderField"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="agent">The agent.</param>
        /// <param name="text">The text.</param>
        public WarningHeaderField(Int16? code, string agent, string text)
        {
            PropertyVerifier.ThrowOnNullArgument(agent, "agent");
            PropertyVerifier.ThrowOnNullArgument(text, "text");
            Code = code;
            Agent = agent;
            Text = text; 
            Init();
        }

        #endregion Constructors

        #region Methods
		/// <summary>
		/// Performs an implicit conversion from <see cref="System.String"/> to <see cref="Konnetic.Sip.Headers.WarningHeaderField"/>.
		/// </summary>
		/// <param name="value">The string value representing the HeaderField.</param>
        /// <returns>A new <see cref="Konnetic.Sip.Headers.WarningHeaderField"/> populated from the <paramref name="value"/> parameter.</returns>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="value"/>.</exception>
        public static implicit operator WarningHeaderField(String value)
        {
            PropertyVerifier.ThrowOnNullArgument(value,"value");

            WarningHeaderField hf = new WarningHeaderField();
            hf.Parse(value);
            return hf;
        }
		/// <summary>
		/// Performs an explicit conversion from <see cref="Konnetic.Sip.Headers.WarningHeaderField"/> to <see cref="System.String"/>.
		/// </summary>
		/// <param name="headerField">The HeaderField to convert.</param>
		/// <returns>A string representation of the HeaderField.</returns>
		/// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="headerField"/>.</exception>
        public static explicit operator string(WarningHeaderField headerField)
        {
            PropertyVerifier.ThrowOnNullArgument(headerField, "headerField");
            return headerField.ToString();
        }

        /// <summary>
        /// Creates a deep copy of this instance.
        /// </summary> 
        /// <remarks>Creates and returns a deep copy of the Header. This method ensures a deep copy of the HeaderField, when a message is cloned the HeaderField can be modified without effecting the original HeaderField in the message.</remarks>
        /// <returns>A deep copy of <see cref="T:Konnetic.Sip.Headers.WarningHeaderField"/>.</returns>
        /// <threadsafety static="true" instance="false" />
public override HeaderFieldBase Clone()
        {
            WarningHeaderField newObj = new WarningHeaderField();
            newObj._code=_code; 
            newObj._warnText=_warnText;
            newObj._agent = _agent;
            return newObj;
        }
///<summary>
/// Compare this SIP Header for equality with another <see cref="T:Konnetic.Sip.Headers.WarningHeaderField"/> object.</summary>
/// <remarks>
/// All optional headers are compared using object equality that is each field in the header is used for comparision. When comparing HeaderFields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular HeaderField, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are case-sensitive. 
/// </remarks>
/// <param name="other">The <see cref="T:Konnetic.Sip.Headers.WarningHeaderField"/> to compare to this instance.</param>
/// <returns><c>true</c> if <paramref name="other" /> is an instance of this class representing the same SIP HeaderField as this, <c>false</c> otherwise.</returns>    
/// <overloads>
/// <summary>This method is overloaded.</summary>
/// <remarks>Overloads allow for equality against <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>, <see cref="T:System.Object"/> and another instance of this HeaderField.</remarks>
/// </overloads>  
/// <threadsafety static="true" instance="false" />
[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
public bool Equals(WarningHeaderField other)
    {
    if((object)other == null)
        {
        return false;
        }

    return base.Equals((HeaderFieldBase)other) && other.Text.Equals(Text, StringComparison.Ordinal) && other.Agent.Equals(Agent, StringComparison.OrdinalIgnoreCase) && other.Code.Equals(Code);
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

            WarningHeaderField p = obj as WarningHeaderField;
            if((object)p == null)
                {
                HeaderFieldGroup<WarningHeaderField> p1 = obj as HeaderFieldGroup<WarningHeaderField>;
                if((object)p1 == null)
                    {
                    return false;
                    }
                else
                    {
                    return p1.Equals(this);
                    }
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
            return base.IsValid() && Code >= 100 && Code <= 999 && !string.IsNullOrEmpty(Agent) && !string.IsNullOrEmpty(Text);
        }
        /// <summary>
        /// Parses string representation of the HeaderField.
        /// </summary>
        /// <remarks>  
        /// <b>RFC 3261 Syntax:</b> 
        /// <table >
        /// <tr><td colspan="2" style="border-bottom:none">"Warning" ":" warning-value *("," warning-value)</td></tr> 
        /// <tr><td style="border-bottom:none">warning-value = </td><td style="border-bottom:none">3DIGIT SPACE warn-agent SPACE quoted-string</td></tr> 
        /// <tr><td style="border-bottom:none">warn-agent = </td><td style="border-bottom:none">hostport / pseudonym ; the name or pseudonym of the server adding ; the Warning header, for use in debugging</td></tr>
        /// <tr><td style="border-bottom:none">pseudonym = </td><td style="border-bottom:none">token</td></tr> 
        /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr>
        /// <tr><td style="border-bottom:none">quoted-string = </td><td style="border-bottom:none">SWS DOUBLE_QUOTE *(qdtext / quoted-pair ) DOUBLE_QUOTE</td></tr>
        /// <tr><td style="border-bottom:none">qdtext = </td><td style="border-bottom:none">LWS / %x21 / %x23-5B / %x5D-7E / UTF8-NONASCII</td></tr>
        /// <tr><td style="border-bottom:none">LWS = </td><td style="border-bottom:none">[*WSP CRLF] 1*WSP ; linear whitespace</td></tr>
        /// <tr><td style="border-bottom:none">SWS = </td><td style="border-bottom:none">[LWS] ; sep whitespace</td></tr>
        /// <tr><td style="border-bottom:none">host = </td><td style="border-bottom:none">hostname / IPv4address / IPv6reference</td></tr>
        /// <tr><td style="border-bottom:none">port = </td><td style="border-bottom:none">1*DIGIT</td></tr>
        /// </table>  
        /// <para/>
        /// <note type="implementnotes">This HeaderField allows HeaderField grouping (<see cref="T:Konnetic.Sip.Headers.HeaderFieldGroup{T}"/>).</note>
        /// <example>
        /// <list type="bullet">
        /// <item>Warning: 307 isi.edu "Session parameter 'foo' not understood"</item>
        /// <item>Warning: 301 isi.edu "Incompatible network address type 'E.164'"</item>
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
                _code = null; 
                Agent = string.Empty;
                Text = string.Empty;

                if(!string.IsNullOrEmpty(value))
                    {

                    Regex _codeRegex = new Regex(@"(?<=^\s*)[0-9]{3}", RegexOptions.Compiled | RegexOptions.IgnoreCase );
                    Regex _agentRegEx = new Regex(@"(?<=^\s*[0-9]{3}\s*)[\w:\-.\[\]%!_*+`'~]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    Regex _textRegEx = new Regex(@"(?<=^\s*[0-9]{3}\s*[\w:\-.\[\]%!_*+`'~""]+\s*"")(.|\n)*?([^\\](?="")|\\\\(?=""))", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    Match m = _codeRegex.Match(value);
                    if(m != null)
                        {
                        if(!string.IsNullOrEmpty(m.Value))
                            {
                            try
                                {
                                Int16 c = Int16.Parse(m.Value.Trim(),CultureInfo.InvariantCulture);
                                Code = c;
								}
							catch(SipException ex)
								{
								throw new SipParseException("Code", SR.ParseExceptionMessage(value), ex);
								}
							catch(FormatException ex)
								{
                                throw new SipParseException(SR.GetString(SR.IntegerConvertException, m.Value,"Code"), ex);
								}
							catch(OverflowException ex)
								{
                                throw new SipParseException(SR.GetString(SR.OverflowException, m.Value, "Code"), ex);
								}
							catch(Exception ex)
								{
                                throw new SipParseException(SR.GetString(SR.GeneralParseException, m.Value, "Code"), ex);
								}
                            }
                        }
                    m = _agentRegEx.Match(value);
                    if(m != null)
                        {
                        if(!string.IsNullOrEmpty(m.Value))
                            {
							try
								{
								Agent = m.Value.Trim();
								}
							catch(SipException ex)
								{
								throw new SipParseException("Agent", SR.ParseExceptionMessage(value), ex);
								}
							catch(Exception ex)
                                {
                                throw new SipParseException(SR.GetString(SR.GeneralParseException, m.Value, "Agent"), ex);
								}
                            }
                        }
                    m = _textRegEx.Match(value);
                    if(m != null)
                        {
                        if(!string.IsNullOrEmpty(m.Value))
                            {
							try{
                            Text = Syntax.UnQuotedString(m.Value);
							}
							catch(SipException ex)
								{
								throw new SipParseException("Text", SR.ParseExceptionMessage(value), ex);
								}
							catch(Exception ex)
                                {
                                throw new SipParseException(SR.GetString(SR.GeneralParseException, m.Value, "Text"), ex);
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
        public override string GetStringValue()
        {
            StringBuilder sb = new StringBuilder(200);
            sb.Append(_code.HasValue ? ((Int16)Code).ToString(CultureInfo.InvariantCulture) : string.Empty);
            sb.Append(_code.HasValue ? new string(SR.SingleWhiteSpace,1) : string.Empty);
            sb.Append(Agent);
            sb.Append(string.IsNullOrEmpty(Agent) ? string.Empty : new string(SR.SingleWhiteSpace,1));
            sb.Append(_warnText);
            return sb.ToString();
        }

        private void Init()
        {
            AllowMultiple = true;
            FieldName = WarningHeaderField.LongName;
            CompactName = WarningHeaderField.LongName;
        }

        #endregion Methods
    }
}