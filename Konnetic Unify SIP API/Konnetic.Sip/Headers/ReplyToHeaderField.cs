/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.ComponentModel;

namespace Konnetic.Sip.Headers
{
    /// <summary>The Reply-To HeaderField contains a logical return URI that may be different from the From HeaderField. 
    /// </summary>
    /// <remarks> 
    /// <b>Standards: RFC3261</b>
    /// <para/>
    /// the URI may be used to return missed calls or unestablished sessions. If the user wished to remain anonymous, the HeaderField should either be omitted from the request or populated in such a way that does not reveal any private information.
    /// <para/>
    /// Even if the display-name is empty, the name-addr form must be used if the addr-spec contains a comma, question mark, or semicolon.
    /// <para/>
    /// <b>RFC 3261 Syntax:</b> 
    /// <table >
    /// <tr><td colspan="2" style="border-bottom:none">"Reply-To" ":" ( name-addr / addr-spec ) *( SEMI generic-param )</td></tr> 
    /// <tr><td style="border-bottom:none">name-addr = </td><td style="border-bottom:none">[ display-name ] LAQUOT addr-spec RAQUOT</td></tr>
    /// <tr><td style="border-bottom:none">addr-spec = </td><td style="border-bottom:none">SIP-URI / SIPS-URI / absoluteURI</td></tr>
    /// <tr><td style="border-bottom:none">display-name = </td><td style="border-bottom:none">*(token LWS)/ quoted-string</td></tr>
    /// <tr><td style="border-bottom:none">generic-param = </td><td style="border-bottom:none">token [ EQUAL token / host / quoted-string ]</td></tr> 
    /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr>
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
    /// <item>Reply-To: Bob &lt;sip:bob@biloxi.com&gt;</item>  
    /// </list> 
    /// </example>
    /// </remarks> 
    /// <seealso cref="T:Konnetic.Sip.Headers.MediaTypeHeaderFieldBase"/>
    public sealed class ReplyToHeaderField : AddressedHeaderFieldBase
    {
        #region Fields

        internal const string CompareName = "REPLY-TO";
        internal const string CompareShortName = CompareName;

        /// <summary>
        /// The long form of the name.
        /// </summary>
        internal const string LongName = "Reply-To";

        #endregion Fields
        #region Properties
        /// <summary>
        /// Gets the generic parameters.
        /// </summary>
        /// <value>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection&lt;SipParameter&gt;"/> field parameter.</value>
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
        #endregion Properties
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteHeaderField"/> class.
        /// </summary>
        /// <param name="uri"></param>
        public ReplyToHeaderField(string uri)
            : this(new SipUri(uri))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteHeaderField"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        public ReplyToHeaderField(SipUri uri)
            : base(uri)
        {
            Init();
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="ReplyToHeaderField"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <overloads>
		/// <summary>The method has two overloads.</summary>
		/// </overloads>
        public ReplyToHeaderField()
            : base()
        {
            Init();
        }

        #endregion Constructors

        #region Methods

		/// <summary>
		/// Performs an implicit conversion from <see cref="System.String"/> to <see cref="Konnetic.Sip.Headers.ReplyToHeaderField"/>.
		/// </summary>
		/// <param name="value">The string value representing the HeaderField.</param>
        /// <returns>A new <see cref="Konnetic.Sip.Headers.ReplyToHeaderField"/> populated from the <paramref name="value"/> parameter.</returns>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="value"/>.</exception>
        public static implicit operator ReplyToHeaderField(String value)
        {
            PropertyVerifier.ThrowOnNullArgument(value,"value");

            ReplyToHeaderField hf = new ReplyToHeaderField();
            hf.Parse(value);
            return hf;
        }

		/// <summary>
		/// Performs an explicit conversion from <see cref="Konnetic.Sip.Headers.ReplyToHeaderField"/> to <see cref="System.String"/>.
		/// </summary>
		/// <param name="headerField">The HeaderField to convert.</param>
		/// <returns>A string representation of the HeaderField.</returns>
		/// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="headerField"/>.</exception>
        public static explicit operator string(ReplyToHeaderField headerField)
        {
            PropertyVerifier.ThrowOnNullArgument(headerField, "headerField");
            return headerField.ToString();
        }

        /// <summary>
        /// Creates a deap  copy of this instance.
        /// </summary> 
        /// <remarks>Creates and returns a deep copy of the Header. This method ensures a deep copy of the HeaderField, when a message is cloned the HeaderField can be modified without effecting the original HeaderField in the message.</remarks>
        /// <returns>A deep copy of <see cref="T:Konnetic.Sip.Headers.ReplyToHeaderField"/>.</returns>
        /// <threadsafety static="true" instance="false" /> 
public override HeaderFieldBase Clone()
        {
        ReplyToHeaderField newObj = new ReplyToHeaderField(Uri);
        CopyParametersTo(newObj);
            newObj.DisplayName = DisplayName;
            newObj.UriSet = UriSet;
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

            ReplyToHeaderField p1 = obj as ReplyToHeaderField;
            if((object)p1 == null)
                {
                HeaderFieldGroup<ReplyToHeaderField> p = obj as HeaderFieldGroup<ReplyToHeaderField>;
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
        /// <tr><td colspan="2" style="border-bottom:none">"Reply-To" ":" ( name-addr / addr-spec ) *( SEMI generic-param )</td></tr> 
        /// <tr><td style="border-bottom:none">name-addr = </td><td style="border-bottom:none">[ display-name ] LAQUOT addr-spec RAQUOT</td></tr>
        /// <tr><td style="border-bottom:none">addr-spec = </td><td style="border-bottom:none">SIP-URI / SIPS-URI / absoluteURI</td></tr>
        /// <tr><td style="border-bottom:none">display-name = </td><td style="border-bottom:none">*(token LWS)/ quoted-string</td></tr>
        /// <tr><td style="border-bottom:none">generic-param = </td><td style="border-bottom:none">token [ EQUAL token / host / quoted-string ]</td></tr> 
        /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr>
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
        /// <item>Reply-To: Bob &lt;sip:bob@biloxi.com&gt;</item>  
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

        /// <summary>
        /// Initialises this instance.
        /// </summary>
        private void Init()
        {
            AllowMultiple = false;
            FieldName = ReplyToHeaderField.LongName;
            CompactName = ReplyToHeaderField.LongName;
        }

        #endregion Methods
    }
}