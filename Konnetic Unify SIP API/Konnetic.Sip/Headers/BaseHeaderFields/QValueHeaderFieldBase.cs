/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Konnetic.Sip.Headers
{

    /// <summary> 
    /// The <see cref="T:Konnetic.Sip.Headers.QValueHeaderFieldBase"/> provides the quality factor parameter for HeaderFields.
    /// </summary>
    /// <remarks>
    /// <b>Standards: RFC3261, RFC2616</b>
    /// <para>SIP content negotiation uses short "floating point" numbers to indicate the relative importance ("weight") of various negotiable parameters. A weight is normalized to a real number in the range 0 through 1, where 0 is the minimum and 1 the maximum value. If a parameter has a quality value of 0, then content with this parameter is ‘not acceptable’ for the client. SIP applications must not generate more than three digits after the decimal point. User configuration of these values SHOULD also be limited in this fashion.</para>
    /// <para>Quality factors allow the user or user agent to indicate the relative degree of preference for that headerfield value, using the qvalue scale from 0 to 1. The default value is q=1.</para>
    /// <para>When comparing header fields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular header field, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are casesensitive.</para>
    /// <note type="caution">"Quality values" is a misnomer, since these values merely represent relative degradation in desired quality.</note> 
    /// <para/>The abstract base class is used by <see cref="T:Konnetic.Sip.Headers.AcceptEncodingHeaderField"/> and <see cref="T:Konnetic.Sip.Headers.AcceptLanguageHeaderField"/> headers.  
    /// <b>RFC 3261 Syntax:</b> 
    /// <table >  
    /// <tr><td style="border-bottom:none">qvalue = </td><td style="border-bottom:none">( "0" [ "." 0*3DIGIT ] ) | ( "1" [ "." 0*3("0") ] )</td></tr> 
    /// </table>
    /// <example>
    /// <list type="bullet">
    /// <item>Accept: audio/*; q=0.2, audio/basic</item> 
    /// <item>Accept: text/*;q=0.3, text/html;q=0.7, text/html;level=1,text/html;level=2;q=0.4, */*;q=0.5</item>  
    /// </list> 
    /// The last example would cause the following values to be associated:
    /// <list type="number">
    /// <item>text/html;level=1 = 1</item> 
    /// <item>text/html = 0.7</item>  
    /// <item>text/plain = 0.3</item>  
    /// <item>image/jpeg = 0.5</item>  
    /// <item>text/html;level=2 = 0.4</item>  
    /// <item>text/html;level=3 = 0.7</item>  
    /// </list>
    /// </example>
    /// </remarks> 
    public abstract class QValueHeaderFieldBase : ParamatizedHeaderFieldBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Q value.
        /// </summary>
        /// <remarks>Set value to null to remove from Parameter list. 
        /// <note type="caution">"Quality values" is a misnomer, since these values merely represent relative degradation in desired quality.</note> 
        /// </remarks>
        /// <value>The Q value.</value>
        public float? QValue
        {
            get
                {
                SipParameter sp = HeaderParameters["q"];

                if((object)sp == null)
                    {
                    return null;
                    }
                try
                    {
                    float val = float.Parse(sp.Value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);

                    PropertyVerifier.ThrowIfFloatOutOfRange(val, 0f, 1f, "'Q'");
                    return val;
                    }
                catch(FormatException ex)
                    {
                    throw new SipException(SR.GetString(SR.FloatConvertException, sp.Value, "QValue"), ex);
                    }
                catch(ArgumentException ex)
                    {
                    throw new SipException(SR.GetString(SR.FloatConvertException, sp.Value, "QValue"), ex);
                    }
                }
            set
            {
                if(value == null)
                    {
                    RemoveParameter("q");
                    }
                else
                    {
                    PropertyVerifier.ThrowIfFloatOutOfRange(value, 0f, 1f, "'Q'");
                    HeaderParameters.Set(0, "q", ((float)value).ToString("0.###", CultureInfo.InvariantCulture));
                    }

                }
        }

        #endregion Properties

        #region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="QValueHeaderFieldBase"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <overloads>
		/// <summary>The method has one overload.</summary>
		/// </overloads>
        protected QValueHeaderFieldBase()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QValueHeaderField"/> class.
        /// </summary>
        /// <param name="qValue">The q value.</param>
        protected QValueHeaderFieldBase(float? qValue)
            : base()
        {
            RegisterKnownParameter("q");
            QValue = qValue;
        }

        #endregion Constructors

        #region Methods
        ///<summary>
        /// Compare this SIP Header for equality with another <see cref="T:Konnetic.Sip.Headers.QValueHeaderFieldBase"/> object.</summary>
        /// <remarks>
        /// All optional headers are compared using object equality that is each field in the header is used for comparision. When comparing HeaderFields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular HeaderField, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are case-sensitive. 
        /// </remarks>
        /// <param name="other">The <see cref="T:Konnetic.Sip.Headers.QValueHeaderFieldBase"/> to compare to this instance.</param>
        /// <returns><c>true</c> if <paramref name="other" /> is an instance of this class representing the same SIP HeaderField as this, <c>false</c> otherwise.</returns>    
        /// <overloads>
        /// <summary>This method is overloaded.</summary>
        /// <remarks>Overloads allow for equality against <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>, <see cref="T:System.Object"/> and another instance of this HeaderField.</remarks>
        /// </overloads>  
        /// <threadsafety static="true" instance="false" />
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public bool Equals(QValueHeaderFieldBase other)
            {
            if((object)other == null)
                {
                return false;
                }

            if(QValue.HasValue && other.QValue.HasValue)
                {
                return base.Equals((ParamatizedHeaderFieldBase)other) && Math.Abs((float)QValue - (float)other.QValue) < 0.001;
                }
            else
                {
                return base.Equals((ParamatizedHeaderFieldBase)other) && QValue.Equals(other.QValue);
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

            QValueHeaderFieldBase p = obj as QValueHeaderFieldBase;
            if((object)p == null)
                {
                return false;
                }

            return this.Equals(p);
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
				base.Parse(value);
				QValue = null;
                if(!string.IsNullOrEmpty(value))
                    {
                    float? f = ParseQValue(value);
                    if(f >= 0)
                        {
						try{
                        QValue = f;
						}
						catch(SipException ex)
							{
							throw new SipParseException("QValue", SR.ParseExceptionMessage(value), ex);
							}
						catch(Exception ex)
                            {
                            throw new SipParseException(SR.GetString(SR.GeneralParseException, f.ToString(), "QValue"), ex);  
							}
                        }
                    }
                }
        }

        internal static float? ParseQValue(string value)
        {
            Regex _qValue = new Regex(@"(?<=(.|\n)*;?q\s*=\s*)(0|1)\.?[0-9]*", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            Match m = _qValue.Match(value);
            if(m != null)
                {
                if(!string.IsNullOrEmpty(m.Value))
                    {
                    try
                        {
                        float nVal = float.Parse(m.Value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
                        PropertyVerifier.ThrowIfFloatOutOfRange(nVal, 0f, 1f, "'Q'");
                        return nVal;
                        }
                    catch(FormatException ex)
                        {
                        throw new SipException(SR.GetString(SR.FloatConvertException, m.Value, "QValue"), ex); 
                        }
                    catch(ArgumentException ex)
                        {
                        throw new SipException(SR.GetString(SR.FloatConvertException, m.Value, "QValue"), ex); 
                        }
                    }
                }
            return null;
        }

        #endregion Methods
    }
}