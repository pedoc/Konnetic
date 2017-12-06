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
    /// <summary> The Date HeaderField contains the date and time. The Date HeaderField reflects the time when the request or response is first sent.
    /// </summary>
    /// <remarks> 
    /// <b>Standards: RFC3261, RFC1123</b>
    /// <para/>
    /// SIP restricts the time zone in SIP-date to "GMT". Format: ddd, dd MMM yyyy HH':'mm':'ss 'GMT'. The date is case-sensitive.
    /// <para/>
    /// <note type="implementnotes">The Date HeaderField can be used by simple end systems without a battery-backed clock to acquire a notion of current time. However, in its GMT form, it requires clients to know their offset from GMT.</note> 
    /// <b>RFC 3261 Syntax:</b> 
    /// <table >
    /// <tr><td colspan="2" style="border-bottom:none">"Date" ":" wkday "," SPACE date1 SPACE time SPACE "GMT"</td></tr>
    /// <tr><td style="border-bottom:none">date1 = </td><td style="border-bottom:none">2DIGIT SPACE month SPACE 4DIGIT; day month year (e.g., 02 Jun 1982)</td></tr>
    /// <tr><td style="border-bottom:none">time = </td><td style="border-bottom:none">2DIGIT ":" 2DIGIT ":" 2DIGIT; 00:00:00 - 23:59:59</td></tr> 
    /// <tr><td style="border-bottom:none">wkday = </td><td style="border-bottom:none">"Mon" / "Tue" / "Wed" / "Thu" / "Fri" / "Sat" / "Sun"</td></tr> 
    /// <tr><td style="border-bottom:none">month = </td><td style="border-bottom:none">"Jan" / "Feb" / "Mar" / "Apr" / "May" / "Jun" / "Jul" / "Aug" / "Sep" / "Oct" / "Nov" / "Dec"</td></tr>  
    /// </table>   
    /// <para/>
    /// <note type="implementnotes">This HeaderField does not allow HeaderField grouping.</note> 
    /// <example>
    /// <list type="bullet">
    /// <item>Date: Sat, 13 Nov 2010 23:29:00 GMT</item>  
    /// </list> 
    /// </example>
    /// </remarks>  
    public sealed class DateHeaderField : HeaderFieldBase
    {
        #region Fields

        internal const string CompareName = "DATE";
        internal const string CompareShortName = CompareName;
        internal const string LongName = "Date";

        private string _date;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>The date.</value>
        public string Date
        {
            get { return _date; }
             private set
            {
            PropertyVerifier.ThrowOnNullArgument(value, "Date");
            _date = value;
            }
        }

        #endregion Properties

        #region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="DateHeaderField"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <overloads>
		/// <summary>The method has one overload.</summary>
		/// </overloads>
        public DateHeaderField()
            : base()
        {
            _date = string.Empty;
            AllowMultiple = false;
            FieldName = DateHeaderField.LongName;
            CompactName = DateHeaderField.LongName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateHeaderField"/> class.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        public DateHeaderField(DateTime dateTime)
            : this()
        {
            PropertyVerifier.ThrowOnNullArgument(dateTime, "dateTime");
            SetDate(dateTime);
        }

        #endregion Constructors

        #region Methods
		/// <summary>
		/// Performs an implicit conversion from <see cref="System.String"/> to <see cref="Konnetic.Sip.Headers.DateHeaderField"/>.
		/// </summary>
		/// <param name="value">The string value representing the HeaderField.</param>
        /// <returns>A new <see cref="Konnetic.Sip.Headers.DateHeaderField"/> populated from the <paramref name="value"/> parameter.</returns>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="value"/>.</exception>
        public static implicit operator DateHeaderField(String value)
        {
            PropertyVerifier.ThrowOnNullArgument(value,"value");

            DateHeaderField hf = new DateHeaderField();
            hf.Parse(value);
            return hf;
        }
		/// <summary>
		/// Performs an explicit conversion from <see cref="Konnetic.Sip.Headers.DateHeaderField"/> to <see cref="System.String"/>.
		/// </summary>
		/// <param name="headerField">The HeaderField to convert.</param>
		/// <returns>A string representation of the HeaderField.</returns>
		/// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="headerField"/>.</exception>
        public static explicit operator string(DateHeaderField headerField)
        {
            PropertyVerifier.ThrowOnNullArgument(headerField, "headerField");
            return headerField.ToString();
        }

        /// <summary>
        /// Creates a deap  copy of this instance.
        /// </summary> 
        /// <remarks>Creates and returns a deep copy of the Header. This method ensures a deep copy of the HeaderField, when a message is cloned the HeaderField can be modified without effecting the original HeaderField in the message.</remarks>
        /// <returns>A deep copy of <see cref="T:Konnetic.Sip.Headers.DateHeaderField"/>.</returns>
        /// <threadsafety static="true" instance="false" /> 
public override HeaderFieldBase Clone()
        {
            DateHeaderField newObj = new DateHeaderField();
            if(string.IsNullOrEmpty(Date))
                {
                return newObj;
                }
            try
                {
                string d = Date.Replace(" GMT", "");
                DateTime dt = DateTime.Parse(d, CultureInfo.InvariantCulture);
                newObj.SetDate(dt);
                }
            catch(FormatException ex)
                {
                throw new SipFormatException("Unrecognised date format.",ex);
                }
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
        /// <summary>
        /// Compare this SIP Header for equality with another <see cref="T:Konnetic.Sip.Headers.DateHeaderField"/> object.</summary>
        /// <remarks>
        /// All optional headers are compared using object equality that is each field in the header is used for comparision. When comparing HeaderFields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular HeaderField, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are case-sensitive. 
        /// </remarks>
        /// <param name="other">The <see cref="T:Konnetic.Sip.Headers.DateHeaderField"/> to compare to this instance.</param>
        /// <returns><c>true</c> if <paramref name="other" /> is an instance of this class representing the same SIP HeaderField as this, <c>false</c> otherwise.</returns>    
        /// <overloads>
        /// <summary>This method is overloaded.</summary>
        /// <remarks>Overloads allow for equality against <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>, <see cref="T:System.Object"/> and another instance of this HeaderField.</remarks>
        /// </overloads>  
        /// <threadsafety static="true" instance="false" />
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public bool Equals(DateHeaderField other)
        {
            if((object)other == null)
                {
                return false;
                }

            return base.Equals((HeaderFieldBase)other) && Date.Equals(other.Date, StringComparison.OrdinalIgnoreCase);
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

            DateHeaderField p1 = obj as DateHeaderField;
            if((object)p1 == null)
                {
                HeaderFieldGroup<DateHeaderField> p = obj as HeaderFieldGroup<DateHeaderField>;
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
            return base.IsValid() && !string.IsNullOrEmpty(Date);
        }
        /// <summary>
        /// Parses string representation of the HeaderField.
        /// </summary>
        /// <remarks> 
        /// <b>RFC 3261 Syntax:</b> 
        /// <table >
        /// <tr><td colspan="2" style="border-bottom:none">"Date" ":" wkday "," SPACE date1 SPACE time SPACE "GMT"</td></tr>
        /// <tr><td style="border-bottom:none">date1 = </td><td style="border-bottom:none">2DIGIT SPACE month SPACE 4DIGIT; day month year (e.g., 02 Jun 1982)</td></tr>
        /// <tr><td style="border-bottom:none">time = </td><td style="border-bottom:none">2DIGIT ":" 2DIGIT ":" 2DIGIT; 00:00:00 - 23:59:59</td></tr> 
        /// <tr><td style="border-bottom:none">wkday = </td><td style="border-bottom:none">"Mon" / "Tue" / "Wed" / "Thu" / "Fri" / "Sat" / "Sun"</td></tr> 
        /// <tr><td style="border-bottom:none">month = </td><td style="border-bottom:none">"Jan" / "Feb" / "Mar" / "Apr" / "May" / "Jun" / "Jul" / "Aug" / "Sep" / "Oct" / "Nov" / "Dec"</td></tr>  
        /// </table>   
        /// <para/>
        /// <note type="implementnotes">This HeaderField does not allow HeaderField grouping.</note> 
        /// <example>
        /// <list type="bullet">
        /// <item>Date: Sat, 13 Nov 2010 23:29:00 GMT</item>  
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
                Date = string.Empty;
                if(!string.IsNullOrEmpty(value))
                    {
                    Regex _dateRegex = new Regex(@"(?<=^\s*)[a-zA-Z]{3},\s+[0-9]{1,2}\s+[a-zA-Z]{3}\s+[0-9]{4}\s+[0-9:]{5,8}", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    Match m = _dateRegex.Match(value);
                    if(m != null)
                        {
                        try
                            {
                            if(!string.IsNullOrEmpty(m.Value))
                                {
                                DateTime nVal = DateTime.Parse(m.Value, CultureInfo.InvariantCulture);
                                SetDate(nVal);
                                }
                            }
							catch(SipException ex)
								{
								throw new SipParseException("Date", SR.ParseExceptionMessage(value), ex);
								}
						catch(FormatException ex)
							{
							throw new SipParseException("Date", SR.ParseExceptionMessage(value), ex);
							}
							catch(Exception ex)
                            {
                            throw new SipParseException(SR.GetString(SR.GeneralParseException, m.Value, "Date"), ex);  
								} 
                        }
                    }
                }
        }

        /// <summary>
        /// Sets the date.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        public void SetDate(DateTime dateTime)
        {
             PropertyVerifier.ThrowOnNullArgument(dateTime, "dateTime");
            DateTimeFormatInfo i = new DateTimeFormatInfo();

            Date = dateTime.ToString(i.RFC1123Pattern, CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Gets a string representation of the HeaderField value. The HeaderField name is excluded.
        /// </summary>
        /// <remarks>This method overrides the <c>GetStringValue</c> method in <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>. </remarks>
        /// <returns>String representation of the HeaderField value</returns>
        /// <threadsafety static="true" instance="false" />
        public override string GetStringValue()
        {
            return Date;
        }

        #endregion Methods
    }
}