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

namespace Konnetic.Sip.Headers
{
    /// <summary>The Max-Forwards HeaderField must be used with any SIP method to limit the number of proxies or gateways that can forward the request to the next downstream server.</summary>
    /// <remarks>  
    /// <b>Standards: RFC3261, RFC2616</b>
    /// <para/>
    /// The Max-Forwards HeaderField can also be useful when the client is attempting to trace a request chain that appears to be failing or looping in mid-chain.
    /// <para/>
    /// The Max-Forwards value is an integer in the range 0-255 indicating the remaining number of times this request message is allowed to be forwarded. This count is decremented by each server that forwards the request. The recommended initial value is 70.
    /// <para/>
    /// This HeaderField should be inserted by elements that can not otherwise guarantee loop detection. For example, a server/client should insert a Max-Forwards HeaderField.
    /// <para/>
    /// <b>Validation</b>
    /// <para/>
    /// The Max-Forwards HeaderField is used to limit the number of elements a SIP request can traverse according to the following rules.
    /// <list type="number">
    /// <item>If the request does not contain a Max-Forwards HeaderField, this check is passed.</item>
    /// <item>If the request contains a Max-Forwards HeaderField with a field value greater than zero, the check is passed.</item>
    /// <item>If the request contains a Max-Forwards HeaderField with a field value of zero (0), the element must not forward the request. If the request was for OPTIONS, the element may act as the final recipient and respond (see next secion). Otherwise, the element must return a 483 (Too many hops) response.</item>
    /// </list>
    /// <para/>
    /// <b>OPTIONS</b>
    /// <para/>
    /// The SIP method OPTIONS allows a client to query another client or a proxy server as to its capabilities. The Max-Forwards request-HeaderField provides a mechanism with the OPTIONS methods to limit the number of proxies or gateways that can forward the request to the next inbound server. This can be useful when the client is attempting to trace a request chain which appears to be failing or looping in mid-chain. A server receiving an OPTIONS request with a Max-Forwards HeaderField value of 0 may respond to the request regardless of the Request-URI.
    /// <para/>
    /// As is the case for general client behavior, the transaction layer can return a timeout error if the OPTIONS yields no response. This may indicate that the target is unreachable and hence unavailable. An OPTIONS request may be sent as part of an established dialog to query the peer on capabilities that may be utilized later in the dialog.
    /// <para/>
    /// <note type="implementnotes">This behavior is common with HTTP/1.1. This behavior can be used as a "traceroute" functionality to check the capabilities of individual hop servers by sending a series of OPTIONS requests with incremented Max-Forwards values.</note> 
    /// <para/>
    /// <b>RFC 3261 Syntax:</b> 
    /// <table >
    /// <tr><td colspan="2" style="border-bottom:none">"Max-Forwards" ":" 1*DIGIT</td></tr>  
    /// </table>   
    /// <para/>
    /// <note type="implementnotes">This HeaderField does not allow HeaderField grouping.</note> 
    /// <example>
    /// <list type="bullet">
    /// <item>Max-Forwards: 6</item>  
    /// </list> 
    /// </example>
    /// </remarks>  
    public sealed class MaxForwardsHeaderField : HeaderFieldBase
    {
        #region Fields

        internal const string CompareName = "MAX-FORWARDS";
        internal const string CompareShortName = CompareName;

        /// <summary>
        /// The long form of the name.
        /// </summary>
        internal const string LongName = "Max-Forwards";

        /// <summary>
        /// 
        /// </summary>
        private byte? _maxForwards;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the max forwards.
        /// </summary>
        /// <value>The max forwards.</value>
        public byte? MaxForwards
        {
            get { return _maxForwards; }
            set
			{  
                _maxForwards = value;  }
        }

        #endregion Properties

        #region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="MaxForwardsHeaderField"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <remarks>Defaults: 
		/// <list type="bullet">
		/// <item><c>MaxForwards</c> is set to 70.</item> 
		/// </list> </remarks>
		/// <overloads>
		/// <summary>The method has one overload.</summary>
		/// </overloads>
        public MaxForwardsHeaderField()
            : this(70)
        {
        }

 

        /// <summary>
        /// Initializes a new instance of the <see cref="MaxForwardsHeaderField"/> class.
        /// </summary>
        /// <param name="forwards">The forwards.</param>
        public MaxForwardsHeaderField(byte? forwards)
            : base()
        {
            MaxForwards = forwards;
            AllowMultiple = false;
            CompactName = MaxForwardsHeaderField.LongName;
            FieldName = MaxForwardsHeaderField.LongName;
        }

        #endregion Constructors

        #region Methods

		/// <summary>
		/// Performs an implicit conversion from <see cref="System.String"/> to <see cref="Konnetic.Sip.Headers.MaxForwardsHeaderField"/>.
		/// </summary>
		/// <param name="value">The string value representing the HeaderField.</param>
        /// <returns>A new <see cref="Konnetic.Sip.Headers.MaxForwardsHeaderField"/> populated from the <paramref name="value"/> parameter.</returns>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="value"/>.</exception>
        public static implicit operator MaxForwardsHeaderField(String value)
        {
            PropertyVerifier.ThrowOnNullArgument(value,"value");

            MaxForwardsHeaderField hf = new MaxForwardsHeaderField();
            hf.Parse(value);
            return hf;
        }

		/// <summary>
		/// Performs an explicit conversion from <see cref="Konnetic.Sip.Headers.MaxForwardsHeaderField"/> to <see cref="System.String"/>.
		/// </summary>
		/// <param name="headerField">The HeaderField to convert.</param>
		/// <returns>A string representation of the HeaderField.</returns>
		/// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="headerField"/>.</exception>
        public static explicit operator string(MaxForwardsHeaderField headerField)
        {
            PropertyVerifier.ThrowOnNullArgument(headerField, "headerField");
            return headerField.ToString();
        }

        /// <summary>
        /// Creates a deap  copy of this instance.
        /// </summary> 
        /// <remarks>Creates and returns a deep copy of the Header. This method ensures a deep copy of the HeaderField, when a message is cloned the HeaderField can be modified without effecting the original HeaderField in the message.</remarks>
        /// <returns>A deep copy of <see cref="T:Konnetic.Sip.Headers.MaxForwardsHeaderField"/>.</returns>
        /// <threadsafety static="true" instance="false" /> 
public override HeaderFieldBase Clone()
        {
            return new MaxForwardsHeaderField(MaxForwards);
        }       
        /// <summary>
/// Compare this SIP Header for equality with another <see cref="T:Konnetic.Sip.Headers.MaxForwardsHeaderField"/> object.</summary>
/// <remarks>
/// All optional headers are compared using object equality that is each field in the header is used for comparision. When comparing HeaderFields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular HeaderField, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are case-sensitive. 
/// </remarks>
/// <param name="other">The <see cref="T:Konnetic.Sip.Headers.MaxForwardsHeaderField"/> to compare to this instance.</param>
/// <returns><c>true</c> if <paramref name="other" /> is an instance of this class representing the same SIP HeaderField as this, <c>false</c> otherwise.</returns>    
/// <overloads>
/// <summary>This method is overloaded.</summary>
/// <remarks>Overloads allow for equality against <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>, <see cref="T:System.Object"/> and another instance of this HeaderField.</remarks>
/// </overloads>  
/// <threadsafety static="true" instance="false" />
[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
public bool Equals(MaxForwardsHeaderField other)
    {
    if((object)other == null)
        {
        return false;
        }

    return base.Equals((HeaderFieldBase)other) && MaxForwards.Equals(other.MaxForwards);
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

            MaxForwardsHeaderField p1 = obj as MaxForwardsHeaderField;
            if((object)p1 == null)
                {
                HeaderFieldGroup<MaxForwardsHeaderField> p = obj as HeaderFieldGroup<MaxForwardsHeaderField>;
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
        /// Parses string representation of the HeaderField.
        /// </summary>
        /// <remarks> 
        /// <b>RFC 3261 Syntax:</b> 
        /// <table >
        /// <tr><td colspan="2" style="border-bottom:none">"Max-Forwards" ":" 1*DIGIT</td></tr>  
        /// </table>   
        /// <para/>
        /// <note type="implementnotes">This HeaderField does not allow HeaderField grouping.</note> 
        /// <example>
        /// <list type="bullet">
        /// <item>Max-Forwards: 6</item>  
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
            MaxForwards = null;
            if(!string.IsNullOrEmpty(value))
                {
                Regex _maxForwardsRegex = new Regex(@"(?<=^\s*)[0-9]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                Match m = _maxForwardsRegex.Match(value);
                if(m != null)
                    {
                    try
                        {
                        if(!string.IsNullOrEmpty(m.Value))
                            {
                            byte nVal = byte.Parse(m.Value, CultureInfo.InvariantCulture);
                            MaxForwards = nVal;
                            }
                        }
							catch(SipException ex)
								{
								throw new SipParseException("MaxForwards", SR.ParseExceptionMessage(value), ex);
								}
					catch(FormatException ex)
                        {
                        throw new SipParseException(SR.GetString(SR.ByteConvertException, m.Value, "MaxForwards"), ex);
						}
					catch(OverflowException ex)
                        {
                        throw new SipParseException(SR.GetString(SR.OverflowException, m.Value, "MaxForwards"), ex);
						}
							catch(Exception ex)
                        {
                        throw new SipParseException(SR.GetString(SR.GeneralParseException, m.Value, "MaxForwards"), ex); 
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
        if(MaxForwards.HasValue)
            {
            return ((byte)MaxForwards).ToString(System.Globalization.CultureInfo.InvariantCulture);
            }
        else
            {
            return string.Empty;
            }
        }

        #endregion Methods
    }
}