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
    /// <summary> 
    /// The <see cref="T:Konnetic.Sip.Headers.SecondsHeaderFieldBase"/> provides time dependent information for HeaderFields.
    /// </summary>
    /// <remarks>
    /// <b>Standards: RFC3261, RFC2616</b>
    /// <para>The meaning of the seconds value is header field depenedent. The <see cref="T:Konnetic.Sip.Headers.ExpiresHeaderField"/> HeaderField uses the value to give the relative time after which the message (or content) expires. Whereas the <see cref="T:Konnetic.Sip.Headers.MinExpiresHeaderField"/> HeaderField conveys the minimum refresh interval supported for soft-state elements managed by that server.</para> 
    /// <para/>The abstract base class is used by <see cref="T:Konnetic.Sip.Headers.ExpiresHeaderField"/> and <see cref="T:Konnetic.Sip.Headers.MinExpiresHeaderField"/> headers.  
    /// <b>RFC 3261 Syntax:</b> 
    /// <table >  
    /// <tr><td style="border-bottom:none">delta-seconds = </td><td style="border-bottom:none">1*DIGIT</td></tr> 
    /// <example>
    /// <list type="bullet">
    /// <item>Min-Expires: 60</item> 
    /// <item>Expires: 5</item>  
    /// </list>  
    /// </example>
    /// </remarks> 
    /// <seealso cref="T:Konnetic.Sip.Headers.ExpiresHeaderField"/> 
    /// <seealso cref="T:Konnetic.Sip.Headers.MinExpiresHeaderField"/> 
    public abstract class SecondsHeaderFieldBase : HeaderFieldBase
    {
        #region Fields

        private Int64? _seconds;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the max seconds allowed.
        /// </summary>
        /// <value>The max seconds.</value>
        public static Int64 MaxSeconds
        {
            get{return 4294967295;}
        }

        /// <summary>
        /// Gets the min seconds allowed.
        /// </summary>
        /// <value>The min seconds.</value>
        public static Int64 MinSeconds
        {
            get{return 0;}
        }

        /// <summary>
        /// Gets or sets the seconds value. A value between 0 and (2^32)-1.
        /// </summary>
        /// <value>The seconds.</value>
        public Int64? Seconds
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
		/// Initializes a new instance of the <see cref="SecondsHeaderFieldBase"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <overloads>
		/// <summary>The method has one overload.</summary>
		/// </overloads>
        protected SecondsHeaderFieldBase()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecondsHeaderFieldBase"/> class.
        /// </summary>
        /// <param name="seconds">The seconds.</param>
        protected SecondsHeaderFieldBase(Int64? seconds)
            : base()
        {
            Seconds = seconds;
            AllowMultiple = false;
        }

        #endregion Constructors

        #region Methods
        ///<summary>
        /// Compare this SIP Header for equality with another <see cref="T:Konnetic.Sip.Headers.SecondsHeaderFieldBase"/> object.</summary>
        /// <remarks>
        /// All optional headers are compared using object equality that is each field in the header is used for comparision. When comparing HeaderFields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular HeaderField, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are case-sensitive. 
        /// </remarks>
        /// <param name="other">The <see cref="T:Konnetic.Sip.Headers.SecondsHeaderFieldBase"/> to compare to this instance.</param>
        /// <returns><c>true</c> if <paramref name="other" /> is an instance of this class representing the same SIP HeaderField as this, <c>false</c> otherwise.</returns>    
        /// <overloads>
        /// <summary>This method is overloaded.</summary>
        /// <remarks>Overloads allow for equality against <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>, <see cref="T:System.Object"/> and another instance of this HeaderField.</remarks>
        /// </overloads>  
        /// <threadsafety static="true" instance="false" />
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public bool Equals(SecondsHeaderFieldBase other)
            {
            if((object)other == null)
                {
                return false;
                }

            return base.Equals((HeaderFieldBase)other) && Seconds.Equals(other.Seconds);
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

            SecondsHeaderFieldBase p = obj as SecondsHeaderFieldBase;
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
            return base.IsValid() && Seconds>=0;
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
                Seconds = null;
                if(!string.IsNullOrEmpty(value))
                    {
                    Regex _secondsRegex = new Regex(@"(?<=^\s*)[0-9]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    Match m = _secondsRegex.Match(value);
                    if(m != null)
                        {
                        try
                            {
                            if(!string.IsNullOrEmpty(m.Value))
                                {
                                long nVal = long.Parse(m.Value,CultureInfo.InvariantCulture);
                                Seconds = nVal;
                                }
                            }
								catch(SipException ex)
									{
									throw new SipParseException("Seconds", SR.ParseExceptionMessage(value), ex);
									}
						catch(FormatException ex)
							{
							throw new SipParseException("Not a valid Seconds value.", ex);
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
            if(_seconds.HasValue)
                {
                return ((long)_seconds).ToString(System.Globalization.CultureInfo.InvariantCulture);
                } 
                return string.Empty;  
        }

        #endregion Methods
    }
}