﻿/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace Konnetic.Sip.Headers
{

    /// <summary>The To HeaderField specifies the logical recipient of the request. 
    /// </summary>
    /// <remarks> 
    /// <b>Standards: RFC3261</b>
    /// <para/>
    /// The optional display-name is meant to be rendered by a human user interface. Even if the display-name is empty, the name-addr form must be used if the addr-spec contains a comma, question mark, or semicolon.
    /// <para/>
    /// Two To HeaderFields are equivalent if their URIs match, and their parameters match. Extension parameters in one HeaderField, not present in the other are ignored for the purposes of comparison. This means that the display name and presence or absence of angle brackets do not affect matching.
    /// <para/>
    /// <b>URI</b>
    /// The Contact, From, and To HeaderFields contain a URI. If the URI contains a comma, question mark or semicolon, the URI must be enclosed in angle brackets (&lt; and &gt;). Any URI parameters are contained within these brackets. If the URI is not enclosed in angle brackets, any semicolon-delimited parameters are header-parameters, not URI parameters.
    /// <para/>
    /// <b>Tag Parameter</b>
    /// <para/>
    /// The tag parameter is used in the To and From HeaderFields of SIP messages. It serves as a general mechanism to identify a dialog, which is the combination of the Call-ID along with two tags, one from each participant in the dialog. When a client sends a request outside of a dialog, it contains a From tag only, providing "half" of the dialog ID. The dialog is completed from the response(s), each of which contributes the second half in the To HeaderField. The forking of SIP requests means that multiple dialogs can be established from a single request. This also explains the need for the two-sided dialog identifier; without a contribution from the recipients, the originator could not disambiguate the multiple dialogs established from a single request.
    /// <para/>
    /// When a tag is generated by a client for insertion into a request or response, it must be globally unique and cryptographically random with at least 32 bits of randomness. A property of this selection requirement is that a client will place a different tag into the From header of an INVITE than it would place into the To header of the response to the same INVITE. This is needed in order for a client to invite itself to a session, a common case for "hairpinning" of calls in PSTN gateways. Similarly, two INVITEs for different calls will have different From tags, and two responses for different calls will have different To tags. 
    /// <para/>
    /// Besides the requirement for global uniqueness, the algorithm for generating a tag is implementation-specific. Tags are helpful in fault tolerant systems, where a dialog is to be recovered on an alternate server after a failure. A server can select the tag in such a way that a backup can recognize a request as part of a dialog on the failed server, and therefore determine that it should attempt to recover the dialog and any other state associated with it.
    /// <para/>
    /// <b>RFC 3261 Syntax:</b> 
    /// <table >
    /// <tr><td colspan="2" style="border-bottom:none">( "To" / "t" ) ":" ( name-addr / addr-spec ) *( SEMI to-param )</td></tr> 
    /// <tr><td style="border-bottom:none">to-param = </td><td style="border-bottom:none">"tag" EQUAL token / generic-param</td></tr>
    /// <tr><td style="border-bottom:none">name-addr = </td><td style="border-bottom:none">[ display-name ] &lt; addr-spec &gt;</td></tr>
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
    /// <note type="implementnotes">The compact form of the To HeaderField is "t".</note> 
    /// <para/>
    /// <note type="implementnotes">This HeaderField does not allow HeaderField grouping.</note> 
    /// <example>
    /// <list type="bullet">
    /// <item>To: The Operator <sip:operator@cs.columbia.edu>;tag=287447</item> 
    /// <item>t: sip:+12125551212@server.phone2net.com</item> 
    /// </list> 
    /// </example>
    /// </remarks> 
    /// <seealso cref="T:Konnetic.Sip.Headers.MediaTypeHeaderFieldBase"/>
    public sealed class ToHeaderField : TagAddressedHeaderFieldBase
    {
        #region Fields

        internal const string CompareName = "TO";
        internal const string CompareShortName = "T";

        /// <summary>
        /// The long form of the name.
        /// </summary>
        internal const string LongName = "To";

        /// <summary>
        /// The short form of the name.
        /// </summary>
        internal const string ShortName = "t";

        #endregion Fields
        #region Properties
        /// <summary>
        /// Gets the generic parameters.
        /// </summary>
        /// <value>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection<SipParameter>"/> field parameter.</value>
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
		/// Initializes a new instance of the <see cref="ToHeaderField"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <overloads>
		/// <summary>The method has four overloads.</summary>
		/// </overloads>
		public ToHeaderField()
			: base()
			{
			Init();
			}
        /// <summary>
        /// Initializes a new instance of the <see cref="ToHeaderField"/> class.
        /// </summary>
        /// <param name="uri">The initiator's URI string.</param>
        public ToHeaderField(string uri)
            : this(new SipUri(uri))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ToHeaderField"/> class.
        /// </summary>
        /// <param name="uri">The initiator's URI.</param>
        public ToHeaderField(SipUri uri)
            : this(uri, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ToHeaderField"/> class.
        /// </summary>
        /// <param name="uri">The initiator's URI.</param>
        /// <param name="displayName">The initiator's display name.</param>
        public ToHeaderField(SipUri uri, string displayName)
            : base(uri, displayName)
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ToHeaderField"/> class.
        /// </summary>
        /// <param name="uri">The initiator's URI.</param>
        /// <param name="displayName">The initiator's display name.</param>
        /// <param name="tag">The tag.</param>
        public ToHeaderField(SipUri uri, string displayName, string tag)
            : base(uri, displayName, tag)
        {
            Init();
        }



        #endregion Constructors

        #region Methods

		/// <summary>
		/// Performs an implicit conversion from <see cref="System.String"/> to <see cref="Konnetic.Sip.Headers.ToHeaderField"/>.
		/// </summary>
		/// <param name="value">The string value representing the HeaderField.</param>
        /// <returns>A new <see cref="Konnetic.Sip.Headers.ToHeaderField"/> populated from the <paramref name="value"/> parameter.</returns>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="value"/>.</exception>
        public static implicit operator ToHeaderField(String value)
        {
            PropertyVerifier.ThrowOnNullArgument(value,"value");

            ToHeaderField hf = new ToHeaderField();
            hf.Parse(value);
            return hf;
        }

		/// <summary>
		/// Performs an explicit conversion from <see cref="Konnetic.Sip.Headers.ToHeaderField"/> to <see cref="System.String"/>.
		/// </summary>
		/// <param name="headerField">The HeaderField to convert.</param>
		/// <returns>A string representation of the HeaderField.</returns>
		/// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="headerField"/>.</exception>
        public static explicit operator string(ToHeaderField headerField)
        {
            PropertyVerifier.ThrowOnNullArgument(headerField, "headerField");
            return headerField.ToString();
        }

        /// <summary>
        /// Creates a deep copy of this instance.
        /// </summary> 
        /// <remarks>Creates and returns a deep copy of the Header. This method ensures a deep copy of the HeaderField, when a message is cloned the HeaderField can be modified without effecting the original HeaderField in the message.</remarks>
        /// <returns>A deep copy of <see cref="T:Konnetic.Sip.Headers.ToHeaderField"/>.</returns>
        /// <threadsafety static="true" instance="false" />
public override HeaderFieldBase Clone()
        {
            ToHeaderField newObj = new ToHeaderField(Uri, DisplayName);
            CopyParametersTo(newObj); 
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

            ToHeaderField p1 = obj as ToHeaderField;
            if((object)p1 == null)
                {
                HeaderFieldGroup<ToHeaderField> p = obj as HeaderFieldGroup<ToHeaderField>;
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
        /// <tr><td colspan="2" style="border-bottom:none">( "To" / "t" ) ":" ( name-addr / addr-spec ) *( SEMI to-param )</td></tr> 
        /// <tr><td style="border-bottom:none">to-param = </td><td style="border-bottom:none">"tag" EQUAL token / generic-param</td></tr>
        /// <tr><td style="border-bottom:none">name-addr = </td><td style="border-bottom:none">[ display-name ] &lt; addr-spec &gt;</td></tr>
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
        /// <note type="implementnotes">The compact form of the To HeaderField is "t".</note> 
        /// <para/>
        /// <note type="implementnotes">This HeaderField does not allow HeaderField grouping.</note> 
        /// <example>
        /// <list type="bullet">
        /// <item>To: The Operator &#060;sip:operator&#064;cs.columbia.edu&#062;;tag=287447</item> 
        /// <item>t: sip:+12125551212@server.phone2net.com</item> 
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
            FieldName = ToHeaderField.LongName;
            CompactName = ToHeaderField.ShortName;
        }

        #endregion Methods
    }
}