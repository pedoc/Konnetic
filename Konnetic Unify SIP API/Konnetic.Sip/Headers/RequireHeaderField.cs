/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;

namespace Konnetic.Sip.Headers
{
    /// <summary>The Require HeaderField is used by clients to tell servers about options that the client expects the server to support in order to process the request. 
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
    /// <tr><td colspan="2" style="border-bottom:none">"Require" ":" option-tag *("," option-tag)</td></tr> 
    /// <tr><td style="border-bottom:none">option-tag = </td><td style="border-bottom:none">token</td></tr>
    /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr> 
    /// </table>  
    /// <para/>
    /// <note type="implementnotes">This HeaderField allows HeaderField grouping (<see cref="T:Konnetic.Sip.Headers.HeaderFieldGroup{T}"/>).</note>  
    /// <example>
    /// <list type="bullet">
    /// <item>Require: 100rel</item> 
    /// </list> 
    /// </example>
    /// </remarks> 
    /// <seealso cref="T:Konnetic.Sip.Headers.ProxyRequireHeaderFieldBase"/>
    /// <seealso cref="T:Konnetic.Sip.Headers.SupportedHeaderFieldBase"/>
    /// <seealso cref="T:Konnetic.Sip.Headers.UnsupportedHeaderFieldBase"/>
    public sealed class RequireHeaderField : OptionHeaderFieldBase
    {
        #region Fields

        internal const string CompareName = "REQUIRE";
        internal const string CompareShortName = CompareName;

        /// <summary>
        /// 
        /// </summary>
        internal const string LongName = "Require";

        #endregion Fields

        #region Constructors


		/// <summary>
		/// Initializes a new instance of the <see cref="RequireHeaderField"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <overloads>
		/// <summary>The method has one overload.</summary>
		/// </overloads>
        public RequireHeaderField()
            : this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequireHeaderField"/> class.
        /// </summary>
        /// <param name="option">The option.</param>
        public RequireHeaderField(string option)
            : base(option)
        {
            AllowMultiple = true;
            FieldName = RequireHeaderField.LongName;
            CompactName = RequireHeaderField.LongName;
        }

        #endregion Constructors

        #region Methods

		/// <summary>
		/// Performs an implicit conversion from <see cref="System.String"/> to <see cref="Konnetic.Sip.Headers.RequireHeaderField"/>.
		/// </summary>
		/// <param name="value">The string value representing the HeaderField.</param>
        /// <returns>A new <see cref="Konnetic.Sip.Headers.RequireHeaderField"/> populated from the passed in string</returns>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="value"/>.</exception>
        public static implicit operator RequireHeaderField(String value)
        {
            PropertyVerifier.ThrowOnNullArgument(value,"value");

            RequireHeaderField hf = new RequireHeaderField();
            hf.Parse(value);
            return hf;
        }

		/// <summary>
		/// Performs an explicit conversion from <see cref="Konnetic.Sip.Headers.RequireHeaderField"/> to <see cref="System.String"/>.
		/// </summary>
		/// <param name="headerField">The HeaderField to convert.</param>
		/// <returns>A string representation of the HeaderField.</returns>
		/// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="headerField"/>.</exception>
        public static explicit operator string(RequireHeaderField headerField)
        {
            PropertyVerifier.ThrowOnNullArgument(headerField, "headerField");
            return headerField.ToString();
        }


        /// <summary>
        /// Creates a deap  copy of this instance.
        /// </summary> 
        /// <remarks>Creates and returns a deep copy of the Header. This method ensures a deep copy of the HeaderField, when a message is cloned the HeaderField can be modified without effecting the original HeaderField in the message.</remarks>
        /// <returns>A deep copy of <see cref="T:Konnetic.Sip.Headers.RequireHeaderField"/>.</returns>
        /// <threadsafety static="true" instance="false" /> 
        public override HeaderFieldBase Clone()
        {
            return new RequireHeaderField(Option);
        }

        /// <summary>
        /// Parses string representation of the HeaderField.
        /// </summary>
        /// <remarks>  
        /// <b>RFC 3261 Syntax:</b> 
        /// <table >
        /// <tr><td colspan="2" style="border-bottom:none">"Require" ":" option-tag *("," option-tag)</td></tr> 
        /// <tr><td style="border-bottom:none">option-tag = </td><td style="border-bottom:none">token</td></tr>
        /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr> 
        /// </table>  
        /// <para/>
        /// <note type="implementnotes">This HeaderField allows HeaderField grouping (<see cref="T:Konnetic.Sip.Headers.HeaderFieldGroup{T}"/>).</note>  
        /// <example>
        /// <list type="bullet">
        /// <item>Require: 100rel</item> 
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