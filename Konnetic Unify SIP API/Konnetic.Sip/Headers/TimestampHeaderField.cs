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
    /// <summary>The Timestamp HeaderField describes when the client sent the request to the server.
    /// </summary>
    /// <remarks> 
    /// <b>Standards: RFC3261</b>
    /// <para/>
    /// <b>Sending a Provisional Response</b>
    /// <para/>
    /// When a 100 (Trying) response is generated, any Timestamp HeaderField present in the request must be copied into this 100 (Trying) response. If there is a delay in generating the response, the server should add a delay value into the Timestamp value in the response. This value must contain the difference between the time of sending of the response and receipt of the request, measured in seconds.
    /// <para/>
    /// Although there is no normative behavior that makes use of the header, it allows for extensions or SIP applications to obtain Round-Trip-Time estimates. 
    /// <para/>
    /// <b>RFC 3261 Syntax:</b> 
    /// <table >
    /// <tr><td colspan="2" style="border-bottom:none">"Timestamp" ":" 1*(DIGIT) [ "." *(DIGIT) ] [ WHITESPACE delay ]</td></tr>
    /// <tr><td style="border-bottom:none">delay = </td><td style="border-bottom:none">*(DIGIT) [ "." *(DIGIT) ]</td></tr>  
    /// </table>  
    /// <para/> 
    /// <note type="implementnotes">This HeaderField does not allow HeaderField grouping.</note> 
    /// <para/>
    /// <note type="implementnotes">It is not particularly useful to encrypt this field.</note> 
    /// <example>
    /// <list type="bullet">
    /// <item>Timestamp: 54</item>  
    /// </list> 
    /// </example>
    /// </remarks> 
    /// <seealso cref="T:Konnetic.Sip.Headers.MediaTypeHeaderFieldBase"/>
    public sealed class TimestampHeaderField : HeaderFieldBase
    {
        #region Fields

        internal const string CompareName = "TIMESTAMP";
        internal const string CompareShortName = CompareName;
        internal const string LongName = "Timestamp";
         
        private float? _delay;
        private float? _time;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the delay.
        /// </summary>
        /// <value>The delay.</value>
        public float? Delay
        {
            get { return _delay; }
            set
            {
            if(value.HasValue)
                {
                PropertyVerifier.ThrowIfFloatOutOfRange(value, 0f, float.MaxValue, "Delay");
                } 
                _delay = value;  
            }
        }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>The time.</value>
        public float? Time
        {
            get { return _time; }
            set
            {
            if(value.HasValue)
                {
                PropertyVerifier.ThrowIfFloatOutOfRange(value, 0f, float.MaxValue, "Time");
                } 
                _time = value; 
            }
        }

        #endregion Properties

        #region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="TimestampHeaderField"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <overloads>
		/// <summary>The method has one overload.</summary>
		/// </overloads>
        public TimestampHeaderField()
            : this(null,null)
        { 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimestampHeaderField"/> class.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <param name="delay">The delay.</param>
        public TimestampHeaderField(float? time, float? delay)
            : base()
        {
            Time = time;
            Delay = delay;
            AllowMultiple = false;
            FieldName = TimestampHeaderField.LongName;
            CompactName = TimestampHeaderField.LongName;
        }

        #endregion Constructors

        #region Methods
		/// <summary>
		/// Performs an implicit conversion from <see cref="System.String"/> to <see cref="Konnetic.Sip.Headers.TimestampHeaderField"/>.
		/// </summary>
		/// <param name="value">The string value representing the HeaderField.</param>
        /// <returns>A new <see cref="Konnetic.Sip.Headers.TimestampHeaderField"/> populated from the <paramref name="value"/> parameter.</returns>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="value"/>.</exception>
        public static implicit operator TimestampHeaderField(String value)
        {
            PropertyVerifier.ThrowOnNullArgument(value,"value");

            TimestampHeaderField hf = new TimestampHeaderField();
            hf.Parse(value);
            return hf;
        }
		/// <summary>
		/// Performs an explicit conversion from <see cref="Konnetic.Sip.Headers.TimestampHeaderField"/> to <see cref="System.String"/>.
		/// </summary>
		/// <param name="headerField">The HeaderField to convert.</param>
		/// <returns>A string representation of the HeaderField.</returns>
		/// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="headerField"/>.</exception>
        public static explicit operator string(TimestampHeaderField headerField)
        {
            PropertyVerifier.ThrowOnNullArgument(headerField, "headerField");
            return headerField.ToString();
        }

        /// <summary>
        /// Creates a deep copy of this instance.
        /// </summary> 
        /// <remarks>Creates and returns a deep copy of the Header. This method ensures a deep copy of the HeaderField, when a message is cloned the HeaderField can be modified without effecting the original HeaderField in the message.</remarks>
        /// <returns>A deep copy of <see cref="T:Konnetic.Sip.Headers.TimestampHeaderField"/>.</returns>
        /// <threadsafety static="true" instance="false" />
public override HeaderFieldBase Clone()
        {
            TimestampHeaderField newObj = new TimestampHeaderField();
            newObj._time = _time;
            newObj._delay=_delay; 
            return newObj;
        }

///<summary>
/// Compare this SIP Header for equality with another <see cref="T:Konnetic.Sip.Headers.TimestampHeaderField"/> object.</summary>
/// <remarks>
/// All optional headers are compared using object equality that is each field in the header is used for comparision. When comparing HeaderFields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular HeaderField, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are case-sensitive. 
/// </remarks>
/// <param name="other">The <see cref="T:Konnetic.Sip.Headers.TimestampHeaderField"/> to compare to this instance.</param>
/// <returns><c>true</c> if <paramref name="other" /> is an instance of this class representing the same SIP HeaderField as this, <c>false</c> otherwise.</returns>    
/// <overloads>
/// <summary>This method is overloaded.</summary>
/// <remarks>Overloads allow for equality against <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>, <see cref="T:System.Object"/> and another instance of this HeaderField.</remarks>
/// </overloads>  
/// <threadsafety static="true" instance="false" />
[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
public bool Equals(TimestampHeaderField other)
    {
    if((object)other == null)
        {
        return false;
        }

    bool retVal = base.Equals((HeaderFieldBase)other);
    if(!retVal)
        {
        return false;
        }

    if(Time.HasValue && other.Time.HasValue)
        {
        retVal = retVal && Math.Abs((float)Time - (float)other.Time) < 0.001;
        }
    else
        {
        retVal = retVal && Time.Equals(other.Time); //in case of two nulls
        }
    if(!retVal)
        {
        return false;
        }

    if(Delay.HasValue && other.Delay.HasValue)
        {
        retVal = retVal && Math.Abs((float)Delay - (float)other.Delay) < 0.001;
        }
    else
        {
        retVal = retVal && Delay.Equals(other.Delay); //in case of two nulls
        }

    return retVal;
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

            TimestampHeaderField p = obj as TimestampHeaderField;
            if((object)p == null)
                {
                HeaderFieldGroup<TimestampHeaderField> p1 = obj as HeaderFieldGroup<TimestampHeaderField>;
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
            return base.IsValid() && Time>0;
        }
        /// <summary>
        /// Parses string representation of the HeaderField.
        /// </summary>
        /// <remarks>  
        /// <b>RFC 3261 Syntax:</b> 
        /// <table >
        /// <tr><td colspan="2" style="border-bottom:none">"Timestamp" ":" 1*(DIGIT) [ "." *(DIGIT) ] [ WHITESPACE delay ]</td></tr>
        /// <tr><td style="border-bottom:none">delay = </td><td style="border-bottom:none">*(DIGIT) [ "." *(DIGIT) ]</td></tr>
        /// </table>  
        /// <para/> 
        /// <note type="implementnotes">This HeaderField does not allow HeaderField grouping.</note> 
        /// <para/>
        /// <note type="implementnotes">It is not particularly useful to encrypt this field.</note> 
        /// <example>
        /// <list type="bullet">
        /// <item>Timestamp: 54</item>  
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
                Time = null;
                Delay = null;  
                if(!string.IsNullOrEmpty(value))
                    {
                    Regex _timeRegex = new Regex(@"(?<=^\s*)[0-9]+(.[0-9]+)?", RegexOptions.Compiled | RegexOptions.IgnoreCase );
                    Regex _delayRegex = new Regex(@"(?<=^\s*[0-9]+(.[0-9]+)?\s+)[0-9]+(.[0-9]+)?\s*", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    Match m = _timeRegex.Match(value);
                    if(m != null)
                        {
                        if(!string.IsNullOrEmpty(m.Value))
                            {
                            try
                                {
                                Time = float.Parse(m.Value.Trim(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
								}
							catch(SipException ex)
								{
								throw new SipParseException("Time", SR.ParseExceptionMessage(value), ex);
								}
							catch(FormatException ex)
                                {
                                throw new SipParseException(SR.GetString(SR.FloatConvertException, m.Value, "Time"), ex);
								}
							catch(OverflowException ex)
                                {
                                throw new SipParseException(SR.GetString(SR.OverflowException, m.Value, "Delay"), ex);
								}
							catch(Exception ex)
                                {
                                throw new SipParseException(SR.GetString(SR.GeneralParseException, m.Value, "Time"), ex);   
								}
                            }
                        }
                    m = _delayRegex.Match(value);
                    if(m != null)
                        {
                        if(!string.IsNullOrEmpty(m.Value))
                            {
                            try
                                {
                                Delay = float.Parse(m.Value.Trim(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
								}
							catch(SipException ex)
								{
								throw new SipParseException("Delay", SR.ParseExceptionMessage(value), ex);
								}
                            catch(FormatException ex)
                                {
                                throw new SipParseException(SR.GetString(SR.FloatConvertException, m.Value, "Delay"), ex);
                                }
                            catch(OverflowException ex)
                                {
                                throw new SipParseException(SR.GetString(SR.OverflowException, m.Value, "Delay"), ex);
								}
							catch(Exception ex)
                                {
                                throw new SipParseException(SR.GetString(SR.GeneralParseException, m.Value, "Delay"), ex);   
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
            string retVal = string.Empty;
            string s = string.Empty;
            int dPoint;
            if(_time.HasValue)
            {
                 s = ((float)Time).ToString("0.00000000", CultureInfo.InvariantCulture);
                dPoint = s.IndexOf('.');
                if(dPoint > 0)
                    {
                    int remove = s.Length - dPoint;
                    if(remove > 3)
                        {
                        s = s.Substring(0, s.Length-(remove - 4));
                        }
                    }
                s = s.TrimEnd('0');
                retVal = s.TrimEnd('.');

                if(_delay.HasValue)
                {
                    retVal += SR.SingleWhiteSpace;

                    s = ((float)Delay).ToString("0.00000000", CultureInfo.InvariantCulture);
                    dPoint = s.IndexOf('.');
                    if(dPoint > 0)
                        {
                        int remove = s.Length - dPoint;
                        if(remove > 3)
                            {
                            s = s.Substring(0, s.Length - (remove - 4));
                            }
                        }
                    s = s.TrimEnd('0');
                    retVal += s.TrimEnd('.');
                }
            }
            return retVal;
        }

        #endregion Methods
    }
}