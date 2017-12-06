/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Text.RegularExpressions;

namespace Konnetic.Sip.Headers
{
    /// <summary>
    /// The Call-ID HeaderField uniquely identifies a particular invitation or all registrations of a particular client.
    /// </summary>
    /// <remarks>  
    /// <b>Standards: RFC3261</b>
    /// <para/>
    ///  A single multimedia conference can give rise to several calls with different Call-IDs, for example, if a user invites a single individual several times to the same (long-running) conference. Call-IDs are case-sensitive and are simply compared byte-by-byte.
    ///  <para/>
    /// <b>RFC 3261 Syntax:</b> 
    /// <table >
    /// <tr><td colspan="2" style="border-bottom:none">( "Call-ID" / "i" ) ":" word [ "@" word ]</td></tr>
    /// <tr><td style="border-bottom:none">word = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" / "(" / ")" / "&lt;" / "&gt;" / ":" / "\\" / DOUBLE_QUOTE / "/" / "[" / "]" / "?" / "{" / "}" )</td></tr>
    /// </table>
    /// <para/>
    /// <note type="implementnotes">The compact form of the Call-ID HeaderField is "i".</note> 
    /// <para/>
    /// <note type="implementnotes">This HeaderField does not allow HeaderField grouping.</note> 
    /// <example>
    /// <list type="bullet">
    /// <item>Call-ID: f81d4fae-7dec-11d0-a765-00a0c91e6bf6@biloxi.com</item>
    /// <item>i:f81d4fae-7dec-11d0-a765-00a0c91e6bf6@192.0.2.4</item>
    /// </list> 
    /// </example>
    /// </remarks>  
    public sealed class CallIdHeaderField : CallIdHeaderFieldBase
    {
        #region Fields

        internal const string CompareName = "CALL-ID";
        internal const string CompareShortName = "I"; 
        internal const string LongName = "Call-ID"; 
        internal const string ShortName = "i";

        #endregion Fields

        #region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CallIdHeaderField"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <remarks>Defaults: 
		/// <list type="bullet">
		/// <item><c>CallId</c> is set to a new valid call id.</item> 
		/// </list> </remarks>
		/// <overloads>
		/// <summary>The method has one overload.</summary>
		/// </overloads>
        public CallIdHeaderField()
            : this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CallIdHeaderField"/> class.
        /// </summary>
        /// <param name="callId">A CallId to initalise the value to.</param>
        public CallIdHeaderField(string callId)
            : base(callId)
			{
			AllowMultiple = false;
			FieldName = CallIdHeaderField.LongName;
			CompactName = CallIdHeaderField.ShortName;
        }
 

        #endregion Constructors

        #region Methods

		/// <summary>
		/// Performs an implicit conversion from <see cref="System.String"/> to <see cref="Konnetic.Sip.Headers.CallIdHeaderField"/>.
		/// </summary>
		/// <param name="value">The string value representing the HeaderField.</param>
        /// <returns>A new <see cref="Konnetic.Sip.Headers.CallIdHeaderField"/> populated from the <paramref name="value"/> parameter.</returns>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="value"/>.</exception>
        public static implicit operator CallIdHeaderField(String value)
        {
            PropertyVerifier.ThrowOnNullArgument(value,"value");

            CallIdHeaderField hf = new CallIdHeaderField();
            hf.Parse(value);
            return hf;
        }

		/// <summary>
		/// Performs an explicit conversion from <see cref="Konnetic.Sip.Headers.CallIdHeaderField"/> to <see cref="System.String"/>.
		/// </summary>
		/// <param name="headerField">The HeaderField to convert.</param>
		/// <returns>A string representation of the HeaderField.</returns>
		/// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="headerField"/>.</exception>
        public static explicit operator string(CallIdHeaderField headerField)
        {
            PropertyVerifier.ThrowOnNullArgument(headerField, "headerField");
            return headerField.ToString();
        }

        /// <summary>
        /// Creates a deap  copy of this instance.
        /// </summary> 
        /// <remarks>Creates and returns a deep copy of the Header. This method ensures a deep copy of the HeaderField, when a message is cloned the HeaderField can be modified without effecting the original HeaderField in the message.</remarks>
        /// <returns>A deep copy of <see cref="T:Konnetic.Sip.Headers.CallIdHeaderField"/>.</returns>
        /// <threadsafety static="true" instance="false" /> 
public override HeaderFieldBase Clone()
        {
            return new CallIdHeaderField(CallId);
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

            CallIdHeaderField p1 = obj as CallIdHeaderField;
            if((object)p1 == null)
                {
                HeaderFieldGroup<CallIdHeaderField> p = obj as HeaderFieldGroup<CallIdHeaderField>;
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
        /// <tr><td colspan="2" style="border-bottom:none">( "Call-ID" / "i" ) ":" word [ "@" word ]</td></tr>
        /// <tr><td style="border-bottom:none">word = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" / "(" / ")" / "&lt;" / "&gt;" / ":" / "\\" / DOUBLE_QUOTE / "/" / "[" / "]" / "?" / "{" / "}" )</td></tr>
        /// </table>
        /// <para/>
        /// <note type="implementnotes">The compact form of the Call-ID HeaderField is "i".</note> 
        /// <para/>
        /// <note type="implementnotes">This HeaderField does not allow HeaderField grouping.</note> 
        /// <example>
        /// <list type="bullet">
        /// <item>Call-ID: f81d4fae-7dec-11d0-a765-00a0c91e6bf6@biloxi.com</item>
        /// <item>i:f81d4fae-7dec-11d0-a765-00a0c91e6bf6@192.0.2.4</item>
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
            base.Parse(value);
            }
        }

        #endregion Methods
    }
}