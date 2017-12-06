/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Text.RegularExpressions;

using Konnetic.Sip.Messages;

namespace Konnetic.Sip.Headers
{
    /// <summary>
    /// The Allow HeaderField lists the set of methods supported by the client generating the message.
    /// </summary>
    /// <remarks>
    /// <b>Standards: RFC3261, RFC2616</b> 
    /// <para/>
    /// All methods, including ACK and CANCEL, understood by the client must be included in the list of methods in the Allow HeaderField, when present. The absence of an Allow HeaderField msut not be interpreted to mean that the client sending the message supports no methods. Rather, it implies that the client is not providing any information on what methods it supports. 
    /// <para/>Supplying an Allow HeaderField in responses to methods other than OPTIONS reduces the number of messages needed.
    /// <para/>
    /// <b>RFC 3261 Syntax:</b> 
    /// <table >
    /// <tr><td colspan="2" style="border-bottom:none">"Allow" ":" [Method *("," Method)]</td></tr>
    /// <tr><td style="border-bottom:none">Method = </td><td style="border-bottom:none">INVITE / ACK / OPTIONS / BYE / CANCELm / REGISTER / extension-method</td></tr>
    /// </table>
    /// <para/>
    /// <note type="implementnotes">This HeaderField allows HeaderField grouping (<see cref="T:Konnetic.Sip.Headers.HeaderFieldGroup{T}"/>).</note> 
    /// <example>
    /// <list type="bullet">
    /// <item>Allow: INVITE, ACK, OPTIONS, CANCEL, BYE</item>
    /// </list> 
    /// </example>
    /// </remarks>
    public sealed class AllowHeaderField : HeaderFieldBase
    {
        #region Fields

        internal const string CompareName = "ALLOW";
        internal const string CompareShortName = CompareName;
        internal const string LongName = "Allow";

        /// <summary>
        /// 
        /// </summary>
        private SipMethod _method;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        /// <value>The method.</value>
        public SipMethod Method
        {
            get { return _method; }
            set
            {
            _method = value;
            }
        }

        #endregion Properties

        #region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="AllowHeaderField"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <overloads>
		/// <summary>The method has one overload.</summary>
		/// </overloads>
        public AllowHeaderField()
            : base()
        {
            AllowMultiple = true;
            Method = new SipMethod("");
            FieldName = AllowHeaderField.LongName;
            CompactName = AllowHeaderField.LongName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CSeqHeaderField"/> class.
        /// </summary>
        /// <param name="method">The method.</param>
        public AllowHeaderField(SipMethod method)
            : this()
        {
            PropertyVerifier.ThrowOnNullArgument(method, "method");
            Method = method;
        }

        #endregion Constructors

        #region Methods

		/// <summary>
		/// Performs an implicit conversion from <see cref="System.String"/> to <see cref="Konnetic.Sip.Headers.AllowHeaderField"/>.
		/// </summary>
		/// <param name="value">The string value representing the HeaderField.</param>
        /// <returns>A new <see cref="Konnetic.Sip.Headers.AllowHeaderField"/> populated from the <paramref name="value"/> parameter.</returns>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="value"/>.</exception>
        public static implicit operator AllowHeaderField(String value)
        {
            PropertyVerifier.ThrowOnNullArgument(value,"value");

            AllowHeaderField hf = new AllowHeaderField();
            hf.Parse(value);
            return hf;
        }

		/// <summary>
		/// Performs an explicit conversion from <see cref="Konnetic.Sip.Headers.AllowHeaderField"/> to <see cref="System.String"/>.
		/// </summary>
		/// <param name="headerField">The HeaderField to convert.</param>
		/// <returns>A string representation of the HeaderField.</returns>
		/// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="headerField"/>.</exception>
        public static explicit operator string(AllowHeaderField headerField)
        {
            PropertyVerifier.ThrowOnNullArgument(headerField, "headerField");
            return headerField.ToString();
        }
        /// <summary>
        /// Creates a deap  copy of this instance.
        /// </summary> 
        /// <remarks>Creates and returns a deep copy of the Header. This method ensures a deep copy of the HeaderField, when a message is cloned the HeaderField can be modified without effecting the original HeaderField in the message.</remarks>
        /// <returns>A deep copy of <see cref="T:Konnetic.Sip.Headers.AllowHeaderField"/>.</returns>
        /// <threadsafety static="true" instance="false" /> 
public override HeaderFieldBase Clone()
        {
            AllowHeaderField newAllow = new AllowHeaderField(Method);
            return newAllow;
        }
/// <summary>
/// Compare this SIP Header for equality with another <see cref="T:Konnetic.Sip.Headers.AllowHeaderField"/> object.</summary>
/// <remarks>
/// All optional headers are compared using object equality that is each field in the header is used for comparision. When comparing HeaderFields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular HeaderField, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are case-sensitive. 
/// </remarks>
/// <param name="other">The <see cref="T:Konnetic.Sip.Headers.AllowHeaderField"/> to compare to this instance.</param>
/// <returns><c>true</c> if <paramref name="other" /> is an instance of this class representing the same SIP HeaderField as this, <c>false</c> otherwise.</returns>    
/// <overloads>
/// <summary>This method is overloaded.</summary>
/// <remarks>Overloads allow for equality against <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>, <see cref="T:System.Object"/> and another instance of this HeaderField.</remarks>
/// </overloads>  
/// <threadsafety static="true" instance="false" />
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public bool Equals(AllowHeaderField other)
        {
            if((object)other == null)
                {
                return false;
                }

            return base.Equals((HeaderFieldBase)other) && _method.Equals(other.Method);
        }

        /// <summary>Compare this SIP Header for equality with the base <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>.
        /// </summary>
        /// <remarks>This method overrides the <c>equals</c> method in <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>.
        /// All optional headers are compared using object equality that is each field in the header is used for comparision. When comparing HeaderFields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular HeaderField, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are case-sensitive.
        /// </remarks>
        /// <param name="other">The <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/> to compare to this instance.</param>
        /// <returns><c>true</c> if <paramref name="other" /> is an instance of this class representing the same SIP HeaderField as this, <c>false</c> otherwise.</returns> 
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
        /// <threadsafety static="true" instance="false" />
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public override bool Equals(object obj)
        {
            if(obj == null)
                {
                return false;
                }

            AllowHeaderField p1 = obj as AllowHeaderField;
            if((object)p1 == null)
                {
                HeaderFieldGroup<AllowHeaderField> p = obj as HeaderFieldGroup<AllowHeaderField>;
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
            return base.IsValid()  && !string.IsNullOrEmpty(Method);
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
            Method = SipMethod.Empty;
            if(!string.IsNullOrEmpty(value))
                {
                RemoveFieldName(ref value, FieldName, CompactName);

                if(!string.IsNullOrEmpty(value))
                    {
                    Regex _methodRegex = new Regex(@"(?<=^\s*)[\w\-.!%*_+`'~]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    Match m = _methodRegex.Match(value);
                    if(m != null)
                        {
                        if(!string.IsNullOrEmpty(m.Value))
                            {
							try{
                            Method = new SipMethod(m.Value);
							}
							catch(SipException ex)
								{
                                throw new SipParseException("Method", SR.ParseExceptionMessage(value), ex);
								}
							catch(Exception ex)
                                {
                                throw new SipParseException(SR.GetString(SR.GeneralParseException, m.Value, "Method"), ex); 
								}
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
            return  Method;
        }

        #endregion Methods
    }
}