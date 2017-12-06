/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Globalization;
using System.Text.RegularExpressions;

using Konnetic.Sip.Messages;

namespace Konnetic.Sip.Headers
    {
    /// <summary>The Retry-After HeaderField indicates how long a service is unavailable.
    /// </summary>
    /// <remarks> 
    /// <b>Standards: RFC3261, RFC2616</b>
    /// <para/>
    /// The Retry-After HeaderField can be used with a 500 (Server Internal Error) or 503 (Service Unavailable) response to indicate how long the service is expected to be unavailable to the requesting client and with a 404 (Not Found), 413 (Request Entity Too Large), 480 (Temporarily Unavailable), 486 (Busy Here), 600 (Busy), or 603 (Decline) response to indicate when the called party anticipates being available again. The value of this field is a positive integer number of seconds (in decimal) after the time of the response.
    /// <para/>
    /// An optional comment can be used to indicate additional information about the time of callback. An optional duration parameter indicates how long the called party will be reachable starting at the initial time of availability. If no duration parameter is given, the service is assumed to be available indefinitely.
    /// <para/>
    /// <b>RFC 3261 Syntax:</b> 
    /// <table>
    /// <tr><td colspan="2" style="border-bottom:none">"Retry-After" ":" 1*DIGIT [ comment ] *( SEMI retry-param )</td></tr>
    /// <tr><td style="border-bottom:none">retry-param = </td><td style="border-bottom:none">("duration" EQUAL 1*DIGIT) / generic-param</td></tr>
    /// <tr><td style="border-bottom:none">generic-param = </td><td style="border-bottom:none">token [ EQUAL token / host / quoted-string ]</td></tr> 
    /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr>
    /// <tr><td style="border-bottom:none">comment = </td><td style="border-bottom:none">&lt; *(ctext / quoted-pair / comment) &gt;</td></tr>
    /// <tr><td style="border-bottom:none">ctext = </td><td style="border-bottom:none">%x21-27 / %x2A-5B / %x5D-7E / UTF8-NONASCII / LWS</td></tr>
    /// <tr><td style="border-bottom:none">quoted-string = </td><td style="border-bottom:none">SWS DOUBLE_QUOTE *(qdtext / quoted-pair ) DOUBLE_QUOTE</td></tr>
    /// <tr><td style="border-bottom:none">qdtext = </td><td style="border-bottom:none">LWS / %x21 / %x23-5B / %x5D-7E / UTF8-NONASCII</td></tr>
    /// <tr><td style="border-bottom:none">LWS = </td><td style="border-bottom:none">[*WSP CRLF] 1*WSP ; linear whitespace</td></tr>
    /// <tr><td style="border-bottom:none">SWS = </td><td style="border-bottom:none">[LWS] ; sep whitespace</td></tr>
    /// <tr><td style="border-bottom:none">host = </td><td style="border-bottom:none">hostname / IPv4address / IPv6reference</td></tr>
    /// </table>  
    /// <para/>
    /// <note type="implementnotes">This HeaderField does not allow HeaderField grouping.</note> 
    /// <example>
    /// <list type="bullet">
    /// <item>Retry-After: 18000;duration=3600</item> 
    /// <item>Retry-After: 120 (I'm in a meeting)</item> 
    /// </list> 
    /// </example>
    /// </remarks>  
    public sealed class RetryAfterHeaderField : ParamatizedHeaderFieldBase
    {
        #region Fields

        internal const string CompareName = "RETRY-AFTER";
        internal const string CompareShortName = CompareName;
        internal const string LongName = "Retry-After";
         
        private int? _seconds;
        private string _comment;

        #endregion Fields

        #region Properties 
        /// <summary>
        /// Gets the generic parameters.
        /// </summary>
        /// <value>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection<SipParameter>"/> field parameter.</value>
        public System.Collections.ObjectModel.ReadOnlyCollection<SipParameter> GenericParameters
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
        /// Gets or sets the comment.
        /// </summary>
        /// <value>The comment.</value>
        public string Comment
        {
            get { return Syntax.UnCommentString(_comment); }
            set
                {
				PropertyVerifier.ThrowOnNullArgument(value, "Comment");

                _comment = Syntax.ConvertToComment(value);
                }
        }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <remarks>Set value to null to remove from header.</remarks>
        /// <value>The duration.</value>
        public int? Duration
        {
            get
                {
                SipParameter sp = HeaderParameters["duration"];

                if((object)sp == null)
                    {
                    return null;
                    }
                try
                    {
                    return Int32.Parse(sp.Value, CultureInfo.InvariantCulture);
                    }
                catch(FormatException ex)
                    {
                    throw new SipFormatException("Cannot convert Duration to an integer.", ex);
                    }
                catch(OverflowException ex)
                    {
                    throw new SipOutOfRangeException("Duration", SR.GetString(SR.OutOfRangeException, sp.Value, "Duration", 0, int.MaxValue), ex);   
                    }
                }
            set
			{ 
                if(value < 0 || value ==null)
                    {
                    RemoveParameter("duration");
                    }
                else
                    {
                    HeaderParameters.Set("duration", value.ToString());
                    }
                }
        }

        public static Int64 MaxSeconds
            {
            get { return 4294967295; }
            }

        public static Int64 MinSeconds
            {
            get { return 0; }
            }
        /// <summary>
        /// Gets or sets the seconds.
        /// </summary>
        /// <remarks>Set value to null to remove from header.</remarks>
        /// <value>The seconds.</value>
        public int? Seconds
        {
            get { return _seconds; }
            set
			{ 
 
            if(value.HasValue)
                {
                PropertyVerifier.ThrowIfIntOutOfRange(value, MinSeconds, MaxSeconds, "Seconds");
                } 
                    _seconds = value; 
                }
        }

        #endregion Properties

        #region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="RetryAfterHeaderField"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <overloads>
		/// <summary>The method has two overloads.</summary>
		/// </overloads>
        public RetryAfterHeaderField()
            : this(null)
        { 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RetryAfterHeaderField"/> class.
        /// </summary>
        /// <param name="seconds">The seconds.</param>
        public RetryAfterHeaderField(int? seconds)
            : this(seconds,string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RetryAfterHeaderField"/> class.
        /// </summary>
        /// <param name="seconds">The seconds.</param>
        /// <param name="comment">The comment.</param>
        public RetryAfterHeaderField(int? seconds, string comment)
            : base()
        {
        PropertyVerifier.ThrowOnNullArgument(comment, "comment");
        RegisterKnownParameter("duration");
            Seconds = seconds;
            Comment = comment;
            AllowMultiple = false;
            FieldName = RetryAfterHeaderField.LongName;
            CompactName = RetryAfterHeaderField.LongName;
        }

        #endregion Constructors

        #region Methods
		/// <summary>
		/// Performs an implicit conversion from <see cref="System.String"/> to <see cref="Konnetic.Sip.Headers.RetryAfterHeaderField"/>.
		/// </summary>
		/// <param name="value">The string value representing the HeaderField.</param>
        /// <returns>A new <see cref="Konnetic.Sip.Headers.RetryAfterHeaderField"/> populated from the <paramref name="value"/> parameter.</returns>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="value"/>.</exception>
        public static implicit operator RetryAfterHeaderField(String value)
        {
            PropertyVerifier.ThrowOnNullArgument(value,"value");

            RetryAfterHeaderField hf = new RetryAfterHeaderField();
            hf.Parse(value);
            return hf;
        }
		/// <summary>
		/// Performs an explicit conversion from <see cref="Konnetic.Sip.Headers.RetryAfterHeaderField"/> to <see cref="System.String"/>.
		/// </summary>
		/// <param name="headerField">The HeaderField to convert.</param>
		/// <returns>A string representation of the HeaderField.</returns>
		/// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="headerField"/>.</exception>
        public static explicit operator string(RetryAfterHeaderField headerField)
        {
            PropertyVerifier.ThrowOnNullArgument(headerField, "headerField");
            return headerField.ToString();
        }

/// <threadsafety static="true" instance="false" />
public override HeaderFieldBase Clone()
        {
            RetryAfterHeaderField newObj = new RetryAfterHeaderField(Seconds);
            newObj._comment = _comment;
            CopyParametersTo(newObj); 
             return newObj;
        }

///<summary>
/// Compare this SIP Header for equality with another <see cref="T:Konnetic.Sip.Headers.RetryAfterHeaderField"/> object.</summary>
/// <remarks>
/// All optional headers are compared using object equality that is each field in the header is used for comparision. When comparing HeaderFields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular HeaderField, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are case-sensitive. 
/// </remarks>
/// <param name="other">The <see cref="T:Konnetic.Sip.Headers.RetryAfterHeaderField"/> to compare to this instance.</param>
/// <returns><c>true</c> if <paramref name="other" /> is an instance of this class representing the same SIP HeaderField as this, <c>false</c> otherwise.</returns>    
/// <overloads>
/// <summary>This method is overloaded.</summary>
/// <remarks>Overloads allow for equality against <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>, <see cref="T:System.Object"/> and another instance of this HeaderField.</remarks>
/// </overloads>  
/// <threadsafety static="true" instance="false" />
[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
public bool Equals(RetryAfterHeaderField other)
    {
    if((object)other == null)
        {
        return false;
        }

    return base.Equals((ParamatizedHeaderFieldBase)other) && _seconds.Equals(other._seconds) && _comment.Equals(other._comment);
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

            RetryAfterHeaderField p = obj as RetryAfterHeaderField;
            if((object)p == null)
                {
                HeaderFieldGroup<RetryAfterHeaderField> p1 = obj as HeaderFieldGroup<RetryAfterHeaderField>;
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
            return base.IsValid() && Seconds > 0;
        }
        /// <summary>
        /// Parses string representation of the HeaderField.
        /// </summary>
        /// <remarks> 
        /// <b>RFC 3261 Syntax:</b> 
        /// <table>
        /// <tr><td colspan="2" style="border-bottom:none">"Retry-After" ":" 1*DIGIT [ comment ] *( SEMI retry-param )</td></tr>
        /// <tr><td style="border-bottom:none">retry-param = </td><td style="border-bottom:none">("duration" EQUAL 1*DIGIT) / generic-param</td></tr>
        /// <tr><td style="border-bottom:none">generic-param = </td><td style="border-bottom:none">token [ EQUAL token / host / quoted-string ]</td></tr> 
        /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr>
        /// <tr><td style="border-bottom:none">comment = </td><td style="border-bottom:none">&lt; *(ctext / quoted-pair / comment) &gt;</td></tr>
        /// <tr><td style="border-bottom:none">ctext = </td><td style="border-bottom:none">%x21-27 / %x2A-5B / %x5D-7E / UTF8-NONASCII / LWS</td></tr>
        /// <tr><td style="border-bottom:none">quoted-string = </td><td style="border-bottom:none">SWS DOUBLE_QUOTE *(qdtext / quoted-pair ) DOUBLE_QUOTE</td></tr>
        /// <tr><td style="border-bottom:none">qdtext = </td><td style="border-bottom:none">LWS / %x21 / %x23-5B / %x5D-7E / UTF8-NONASCII</td></tr>
        /// <tr><td style="border-bottom:none">LWS = </td><td style="border-bottom:none">[*WSP CRLF] 1*WSP ; linear whitespace</td></tr>
        /// <tr><td style="border-bottom:none">SWS = </td><td style="border-bottom:none">[LWS] ; sep whitespace</td></tr>
        /// <tr><td style="border-bottom:none">host = </td><td style="border-bottom:none">hostname / IPv4address / IPv6reference</td></tr>
        /// </table>  
        /// <para/>
        /// <note type="implementnotes">This HeaderField does not allow HeaderField grouping.</note> 
        /// <example>
        /// <list type="bullet">
        /// <item>Retry-After: 18000;duration=3600</item> 
        /// <item>Retry-After: 120 (I'm in a meeting)</item> 
        /// </list> 
        /// </example>
        /// </remarks>
        /// <param name="value">The HeaderField string to parse.</param>
        /// <exception cref="T:Konnetic.Sip.SipParseException">Thrown when an invalid (non-standard) value is encountered.</exception>
        /// <exception cref="T:Konnetic.Sip.SipException">Thrown when a processing exception is encountered.</exception>
        /// <threadsafety static="true" instance="false" />
        public override void Parse(string value)
        {
            RemoveFieldName(ref value, FieldName, CompactName);
            Duration = null;
            Seconds = null; 
            Comment = string.Empty;
            base.Parse(value);
            if(!string.IsNullOrEmpty(value))
                {
                Regex _secondsRegex = new Regex(@"(?<=^\s*)[0-9]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                Regex _durationRegEx = new Regex(@"(?<=(.|\n)*duration\s*=\s*)[0-9]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                Regex _commentRegex = new Regex(@"(?<=^\s*[0-9]+\s*\()(.|\n)*?(?=\))", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                Match m = _secondsRegex.Match(value);
                if(m != null)
                    {
                    try
                        {
                        if(!string.IsNullOrEmpty(m.Value))
                            {
                            Int32 nVal = Int32.Parse(m.Value, CultureInfo.InvariantCulture);
                            Seconds = nVal;
                            }
                        }
						catch(SipException ex)
							{
							throw new SipParseException("Seconds", SR.ParseExceptionMessage(value), ex);
							}
					catch(FormatException ex)
						{
						throw new SipParseException("Not a valid Seconds.", ex);
						}
					catch(OverflowException ex)
                        {
                        throw new SipParseException(SR.GetString(SR.OverflowException, m.Value, "Seconds"), ex);
						}
						catch(Exception ex)
                        {
                        throw new SipParseException(SR.GetString(SR.GeneralParseException, m.Value, "Seconds"), ex);   
							}
                    }
                m = _durationRegEx.Match(value);
                if(m != null)
                    {
                    try
                        {
                        if(!string.IsNullOrEmpty(m.Value))
                            {
                            Int32 nVal = Int32.Parse(m.Value, CultureInfo.InvariantCulture);
                            Duration = nVal;
                            }
                        }
                    catch(SipException ex)
                        {
                        throw new SipParseException("Duration", SR.ParseExceptionMessage(value), ex);
                        }
                    catch(FormatException ex)
                        {
                        throw new SipParseException("Not a valid Duration.", ex);
                        }
                    catch(OverflowException ex)
                        {
                        throw new SipParseException(SR.GetString(SR.OverflowException, m.Value, "Duration"), ex);
                        }
                    catch(Exception ex)
                        {
                        throw new SipException(SR.GetString(SR.GeneralParseException, m.Value, "Duration"), ex);  
                        }
                    }
                m = _commentRegex.Match(value);
                if(m != null)
                    {
                    if(!string.IsNullOrEmpty(m.Value))
                        {try{
                        Comment = m.Value;
						}
						catch(SipException ex)
							{
							throw new SipParseException("Comment", SR.ParseExceptionMessage(value), ex);
							} 
						catch(Exception ex)
							{
							throw new SipException("Invalid HeaderField: Comment.", ex);
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
            string cmt = _comment.Length > 0 ? SR.SingleWhiteSpace + _comment  : string.Empty;
            if(_seconds.HasValue)
                {
                return ((int)Seconds).ToString(System.Globalization.CultureInfo.InvariantCulture) + cmt;
                }
            else
                {
                return cmt;
                }
        }

        #endregion Methods
    }
}