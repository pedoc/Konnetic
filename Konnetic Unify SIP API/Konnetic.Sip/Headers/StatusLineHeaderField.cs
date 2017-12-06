/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;

using Konnetic.Sip.Messages;

namespace Konnetic.Sip.Headers
{
    /// <summary>SIP responses are distinguished from requests by having a Status-Line as their start-line. 
    /// </summary>
    /// <remarks> 
    /// <b>Standards: RFC3261, RFC2616</b>
    /// <para/>
    /// A Status-Line consists of the protocol version followed by a numeric Status-Code and its associated textual phrase, with each element separated by a single SPACE character. 
    /// <para/>
    /// No CR or LF is allowed except in the final CRLF sequence.
    /// <para/>
    /// The Status-Code is a 3-digit integer result code that indicates the outcome of an attempt to understand and satisfy a request. The Reason-Phrase is intended to give a short textual description of the Status-Code. The Status-Code is intended for use by automata, whereas the Reason-Phrase is intended for the human user. A client is not required to examine or display the Reason-Phrase.
    /// <para/>
    /// While the SIP specification suggests specific wording for the reason phrase, implementations may choose other text, for example, in the language indicated in the Accept-Language HeaderField of the request. The first digit of the Status-Code defines the class of response. The last two digits do not have any categorization role. For this reason, any response with a status code between 100 and 199 is referred to as a "1xx response", any response with a status code between 200 and 299 as a "2xx response", and so on. SIP/2.0 allows six values for the first digit:
    /// <para/>
    /// <list type="bullet">
    /// <item><i>1xx:</i> Provisional – request received, continuing to process the request.</item>
    /// <item><i>2xx:</i> Success – the action was successfully received, understood, and accepted;</item>
    /// <item><i>3xx:</i> Redirection – further action needs to be taken in order to complete the request;</item>
    /// <item><i>4xx:</i> Client Error – the request contains bad syntax or cannot be fulfilled at this server;</item>
    /// <item><i>5xx:</i> Server Error – the server failed to fulfill an apparently valid request;</item>
    /// <item><i>6xx:</i> Global Failure – the request cannot be fulfilled at any server.</item>
    /// </list>
    /// <b>RFC 3261 Syntax:</b> 
    /// <table >
    /// <tr><td colspan="2" style="border-bottom:none">SIP-Version SP Status-Code SP Reason-Phrase CRLF</td></tr> 
    /// <tr><td style="border-bottom:none">SIP-Version = </td><td style="border-bottom:none">"SIP" "/" 1*DIGIT "." 1*DIGIT</td></tr>
    /// <tr><td style="border-bottom:none">Status-Code = </td><td style="border-bottom:none">Informational / Redirection / Success / Client-Error / Server-Error / Global-Failure / extension-code</td></tr>
    /// <tr><td style="border-bottom:none">extension-code = </td><td style="border-bottom:none">3DIGIT</td></tr>
    /// <tr><td style="border-bottom:none">Reason-Phrase = </td><td style="border-bottom:none">*(ASCII / escaped / UTF8 / SPACE / TAB) ; all characters</td></tr>
    /// <tr><td style="border-bottom:none">escaped = </td><td style="border-bottom:none">%HEXDIG HEXDIG</td></tr> 
    /// <tr><td style="border-bottom:none">HEXDIG = </td><td style="border-bottom:none">DIGIT / "a" / "b" / "c" / "d" / "e" / "f"</td></tr> 
    /// </table>  
    /// <para/>
    /// <note type="implementnotes">This HeaderField does not allow HeaderField grouping.</note> 
    /// <example>
    /// <list type="bullet">
    /// <item>SIP/2.0 200 OK</item> 
    /// <item>SIP/2.0 100 Trying</item> 
    /// </list> 
    /// </example>
    /// </remarks> 
    /// <seealso cref="T:Konnetic.Sip.Headers.MediaTypeHeaderFieldBase"/>
    public sealed class StatusLineHeaderField
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Int16? _statusCode;

        /// <summary>
        /// 
        /// </summary>
        private string _reasonPhrase;

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
        /// Gets or sets the reason phrase.
        /// </summary>
        /// <value>The reason phrase.</value>
        public string ReasonPhrase
        {
            get { return _reasonPhrase; }
            set
            {
            PropertyVerifier.ThrowOnNullArgument(value, "ReasonPhrase");
            _reasonPhrase = value;
            }
        }

        /// <summary>
        /// Gets or sets the scheme.
        /// </summary>
        /// <value>The scheme.</value>
        public string Scheme
        {
            get { return _scheme; }
            private set
            {
            PropertyVerifier.ThrowOnNullArgument(value, "Scheme");
            _scheme = value;
            }
        }

        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>The status code.</value>
        public Int16? StatusCode
        {
            get { return _statusCode; }
            set 
            {
                if(value.HasValue)
                    {
                    if(value < 100 || value > 699)
                        {
                        throw new SipException("Unknown Response class. Must be between 100 and 699.");
                        }
                    } 
                    _statusCode = value; 
            }
        }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public string Version
        {
            get { return _version; }
            private set
            {
            PropertyVerifier.ThrowOnNullArgument(value, "Version");
            _version = value;
            }
        }

        #endregion Properties

        #region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="StatusLineHeaderField"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <overloads>
		/// <summary>The method has one overload.</summary>
		/// </overloads>
        public StatusLineHeaderField()
        {
            Version = SR.SIPVersionNumber;
            Scheme = SR.DefaultSipScheme;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusLineHeaderField"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public StatusLineHeaderField(string value)
        {
            PropertyVerifier.ThrowOnNullArgument(value, "value");
            Parse(value);
        }

        #endregion Constructors

        #region Methods

		/// <summary>
		/// Performs an implicit conversion from <see cref="System.String"/> to <see cref="Konnetic.Sip.Headers.StatusLineHeaderField"/>.
		/// </summary>
		/// <param name="value">The string value representing the HeaderField.</param>
        /// <returns>A new <see cref="Konnetic.Sip.Headers.StatusLineHeaderField"/> populated from the passed in string</returns>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="value"/>.</exception>
        public static implicit operator StatusLineHeaderField(String value)
        {
            PropertyVerifier.ThrowOnNullArgument(value,"value");

            StatusLineHeaderField hf = new StatusLineHeaderField();
            hf.Parse(value);
            return hf;
        }

		/// <summary>
		/// Performs an explicit conversion from <see cref="Konnetic.Sip.Headers.StatusLineHeaderField"/> to <see cref="System.String"/>.
		/// </summary>
		/// <param name="headerField">The HeaderField to convert.</param>
		/// <returns>A string representation of the HeaderField.</returns>
		/// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="headerField"/>.</exception>
        public static explicit operator string(StatusLineHeaderField headerField)
        {
            PropertyVerifier.ThrowOnNullArgument(headerField, "headerField");
            return headerField.ToString();
        }

        /// <summary>
        /// Creates a deep copy of this instance.
        /// </summary> 
        /// <remarks>Creates and returns a deep copy of the Header. This method ensures a deep copy of the HeaderField, when a message is cloned the HeaderField can be modified without effecting the original HeaderField in the message.</remarks>
        /// <returns>A deep copy of <see cref="T:Konnetic.Sip.Headers.StatusLineHeaderField"/>.</returns>
        /// <threadsafety static="true" instance="false" />
        public StatusLineHeaderField Clone()
        {
            return new StatusLineHeaderField(ToString());
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
        public void Parse(string value)
        {
            value=Syntax.ReplaceFolding(value);
            Regex _schemeRegex = new Regex(@"(?<=^\s*)SIP", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex _versionRegex = new Regex(@"(?<=^\s*SIP/)[0-9]{1}.[0.9]{1}", RegexOptions.Compiled | RegexOptions.IgnoreCase );
            Regex _statusCodeRegex = new Regex(@"(?<=^\s*SIP/[0-9]{1}.[0.9]{1}\s+)[0-9]{3}", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex _reasonPhraseRegex = new Regex(@"(?<=^\s*SIP/[0-9]{1}.[0.9]{1}\s+[0-9]{3}\s+)(.|\n)+(?=\s*$)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            Match m = _reasonPhraseRegex.Match(value);
            string tValue = m.Value;
            if((object)m == null || string.IsNullOrEmpty(tValue))
            {
            throw new SipParseException("ReasonPhrase",new SipUriFormatException("Reason Phrase is required for Status Line Field"));
            }
            ReasonPhrase = tValue;

            m = _statusCodeRegex.Match(value);
            tValue = m.Value;
            if((object)m == null || string.IsNullOrEmpty(tValue))
            {
            throw new SipParseException("StatusCode", new SipFormatException("Status Code is required for Status Line Field"));
            }

            try
            {
            StatusCode = Int16.Parse(tValue, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch(FormatException)
            {
            throw new SipParseException("StatusCode", new SipFormatException("Status Code is invalid."));
            }
            catch(OverflowException ex)
                {
                throw new SipParseException(SR.GetString(SR.OverflowException, tValue, "Status Code"), ex); 
            }

            m = _schemeRegex.Match(value);
            tValue = m.Value;
            if((object)m == null || string.IsNullOrEmpty(tValue))
            {
            throw new SipParseException("Scheme", new SipFormatException("Scheme is required for Request Line Field. It must be set to 'SIP'."));
            }
            Scheme = m.Value;

            m = _versionRegex.Match(value);
            tValue = m.Value;
            if((object)m == null || string.IsNullOrEmpty(tValue))
            {
            throw new SipParseException("Version", new SipFormatException("Scheme Version is required for Request Line Field"));
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
            if(!IsValid())
            {
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder(40);
            sb.Append(Scheme);
            sb.Append(SR.SipVersionSeperator);
            sb.Append(Version);
            sb.Append(SR.SingleWhiteSpace);
            sb.Append(((short)StatusCode).ToString(System.Globalization.CultureInfo.InvariantCulture));
            sb.Append(SR.SingleWhiteSpace);
            sb.Append(ReasonPhrase);
            return sb.ToString();
        }

        /// <summary>
        /// Determines whether this instance is valid.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        internal bool IsValid()
        {
            return !string.IsNullOrEmpty(Scheme) && !string.IsNullOrEmpty(Version) && (StatusCode >= 100 || StatusCode<=699) && !string.IsNullOrEmpty(ReasonPhrase);
        }

        #endregion Methods
    }
}