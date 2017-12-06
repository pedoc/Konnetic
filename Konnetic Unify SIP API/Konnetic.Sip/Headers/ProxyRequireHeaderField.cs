/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;

namespace Konnetic.Sip.Headers
{
    /// <summary>The Proxy-Require HeaderField is used to indicate proxy-sensitive features that must be supported by the proxy. 
    /// </summary>
    /// <remarks> 
    /// <b>Standards: RFC3261, RFC2616</b>
    /// <para/>
    /// The Proxy-Require HeaderField is used by client to tell servers about options that the client expects the server to support in order to process the request. Although an optional HeaderField, the Proxy-Require must not be ignored if it is present.
    /// <para/>
    /// The Require HeaderField contains a list of option tags (see below). Each option tag defines a SIP extension that must be understood to process the request. Frequently, this is used to indicate that a specific set of extension HeaderFields need to be understood. A client compliant to the SIP specification must only include option tags corresponding to standards-track RFCs.
    /// <b>Option Tag</b>
    /// Option tags are unique identifiers used to designate new options (extensions) in SIP. These tags are used in <see cref="T:Konnetic.Sip.Headers.RequireHeaderField"/>, <see cref="T:Konnetic.Sip.Headers.ProxyRequireHeaderField"/>, <see cref="T:Konnetic.Sip.Headers.SupportedHeaderField"/> and <see cref="T:Konnetic.Sip.Headers.UnsupportedHeaderFieldBase"/> HeaderFields. Note that these options appear as parameters in those HeaderFields in an option-tag = token form.
    /// <para/> 
    /// <b>RFC 3261 Syntax:</b> 
    /// <table >
    /// <tr><td colspan="2" style="border-bottom:none">"Proxy-Require" ":" option-tag *("," option-tag)</td></tr>  
    /// <tr><td style="border-bottom:none">option-tag = </td><td style="border-bottom:none">token</td></tr>
    /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr> 
    /// </table>  
    /// <para/>
    /// <note type="implementnotes">This HeaderField allows HeaderField grouping (<see cref="T:Konnetic.Sip.Headers.HeaderFieldGroup{T}"/>).</note> 
    /// <example>
    /// <list type="bullet">
    /// <item>Proxy-Require: foo</item>  
    /// </list> 
    /// </example>
    /// </remarks> 
    /// <seealso cref="T:Konnetic.Sip.Headers.RequireHeaderFieldBase"/>
    /// <seealso cref="T:Konnetic.Sip.Headers.SupportedHeaderFieldBase"/>
    /// <seealso cref="T:Konnetic.Sip.Headers.UnsupportedHeaderFieldBase"/>
    public sealed class ProxyRequireHeaderField : OptionHeaderFieldBase
    {
        #region Fields

        internal const string CompareName = "PROXY-REQUIRE";
        internal const string CompareShortName = CompareName;

        /// <summary>
        /// 
        /// </summary>
        internal const string LongName = "Proxy-Require";

        #endregion Fields

        #region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ProxyRequireHeaderField"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <overloads>
		/// <summary>The method has one overload.</summary>
		/// </overloads>
        public ProxyRequireHeaderField()
            : this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyRequireHeaderField"/> class.
        /// </summary>
        /// <param name="option">The option.</param>
        public ProxyRequireHeaderField(string option)
            : base(option)
        {
            FieldName = ProxyRequireHeaderField.LongName;
            CompactName = ProxyRequireHeaderField.LongName;
        }

        #endregion Constructors

        #region Methods

		/// <summary>
		/// Performs an implicit conversion from <see cref="System.String"/> to <see cref="Konnetic.Sip.Headers.ProxyRequireHeaderField"/>.
		/// </summary>
		/// <param name="value">The string value representing the HeaderField.</param>
        /// <returns>A new <see cref="Konnetic.Sip.Headers.ProxyRequireHeaderField"/> populated from the <paramref name="value"/> parameter.</returns>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="value"/>.</exception>
        public static implicit operator ProxyRequireHeaderField(String value)
        {
            PropertyVerifier.ThrowOnNullArgument(value,"value");

            ProxyRequireHeaderField hf = new ProxyRequireHeaderField();
            hf.Parse(value);
            return hf;
        }

		/// <summary>
		/// Performs an explicit conversion from <see cref="Konnetic.Sip.Headers.ProxyRequireHeaderField"/> to <see cref="System.String"/>.
		/// </summary>
		/// <param name="headerField">The HeaderField to convert.</param>
		/// <returns>A string representation of the HeaderField.</returns>
		/// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="headerField"/>.</exception>
        public static explicit operator string(ProxyRequireHeaderField headerField)
        {
            PropertyVerifier.ThrowOnNullArgument(headerField, "headerField");
            return headerField.ToString();
        }

        /// <summary>
        /// Creates a deap  copy of this instance.
        /// </summary> 
        /// <remarks>Creates and returns a deep copy of the Header. This method ensures a deep copy of the HeaderField, when a message is cloned the HeaderField can be modified without effecting the original HeaderField in the message.</remarks>
        /// <returns>A deep copy of <see cref="T:Konnetic.Sip.Headers.ProxyRequireHeaderField"/>.</returns>
        /// <threadsafety static="true" instance="false" /> 
public override HeaderFieldBase Clone()
        {
            return new ProxyRequireHeaderField(Option);
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

            ProxyRequireHeaderField p1 = obj as ProxyRequireHeaderField;
            if((object)p1 == null)
                {
                HeaderFieldGroup<ProxyRequireHeaderField> p = obj as HeaderFieldGroup<ProxyRequireHeaderField>;
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
        /// <tr><td colspan="2" style="border-bottom:none">"Proxy-Require" ":" option-tag *("," option-tag)</td></tr>  
        /// <tr><td style="border-bottom:none">option-tag = </td><td style="border-bottom:none">token</td></tr>
        /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr> 
        /// </table>  
        /// <para/>
        /// <note type="implementnotes">This HeaderField allows HeaderField grouping (<see cref="T:Konnetic.Sip.Headers.HeaderFieldGroup{T}"/>).</note> 
        /// <example>
        /// <list type="bullet">
        /// <item>Proxy-Require: foo</item>  
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