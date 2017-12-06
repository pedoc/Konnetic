/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;

namespace Konnetic.Sip.Headers
{
    /// <summary>
    /// The <see cref="T:Konnetic.Sip.Headers.AddressedHeaderFieldBase"/> provides Display Name information for HeaderFields. 
    /// </summary>
    /// <remarks>
    /// <b>Standards: RFC3261</b>
    /// <para/>The abstract base class is used by  <see cref="T:Konnetic.Sip.Headers.ContactHeaderField"/>, <see cref="T:Konnetic.Sip.Headers.RecordRouteHeaderField"/>, <seealso cref="T:Konnetic.Sip.Headers.ReplyToHeaderField"/>, <see cref="T:Konnetic.Sip.Headers.RouteHeaderField"/> HeaderFields and one base abstract class <seealso cref="T:Konnetic.Sip.Headers.TagAddressedHeaderFieldBase"/>.
    /// <para/>
    /// The display name can be tokens, or a quoted string, if a larger character set is desired.
    /// <para/> 
    /// <b>RFC 3261 Syntax:</b> 
    /// <table >
    /// <tr><td style="border-bottom:none">display-name = </td><td style="border-bottom:none">*(token LWS)/ quoted-string</td></tr> 
    /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr>
    /// <tr><td style="border-bottom:none">quoted-string = </td><td style="border-bottom:none">SWS DOUBLE_QUOTE *(qdtext / quoted-pair ) DOUBLE_QUOTE</td></tr>
    /// <tr><td style="border-bottom:none">qdtext = </td><td style="border-bottom:none">LWS / %x21 / %x23-5B / %x5D-7E / UTF8-NONASCII</td></tr>
    /// <tr><td style="border-bottom:none">LWS = </td><td style="border-bottom:none">[*WSP CRLF] 1*WSP ; linear whitespace</td></tr>
    /// <tr><td style="border-bottom:none">SWS = </td><td style="border-bottom:none">[LWS] ; sep whitespace</td></tr>
    /// </table >
    ///  <example>
    /// <list type="bullet">
    /// <item>Contact: "Mr. Watson" &lt;sip:watson@worcester.bell-telephone.co&gt; ;q=0.7; expires=3600, "Mr. Watson" &lt;mailto:watson@bell-telephone.com&gt; ;q=0.1</item>  
    /// <item>Reply-To: Bob &lt;sip:bob@biloxi.com&gt;</item>  
    /// <item>To: The Operator <sip:operator@cs.columbia.edu>;tag=287447</item> 
    /// </list> 
    /// </example>
    /// <seealso cref="T:Konnetic.Sip.Headers.SipUriHeaderFieldBase"/> 
    /// <seealso cref="T:Konnetic.Sip.Headers.ContactHeaderField"/> 
    /// <seealso cref="T:Konnetic.Sip.Headers.RouteHeaderField"/>
    /// <seealso cref="T:Konnetic.Sip.Headers.RecordRouteHeaderField"/>
    /// <seealso cref="T:Konnetic.Sip.Headers.ReplyToHeaderField"/> 
    /// <seealso cref="T:Konnetic.Sip.Headers.TagAddressedHeaderFieldBase"/> 
    /// </remarks> 
    public abstract class AddressedHeaderFieldBase : SipUriHeaderFieldBase
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private string _displayName;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <remarks>The Display Name is always converted to a quoted string.</remarks>
        /// <value>The display name.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="DisplayName"/>.</exception> 
        [DefaultValue("Display Name")]
        public virtual string DisplayName
        {
            get { return Syntax.UnQuotedString(_displayName); }
            set
            {
                PropertyVerifier.ThrowOnNullArgument(DisplayName, "DisplayName");
                //value = value.Trim();
                _displayName = Syntax.ConvertToQuotedString(Syntax.ReplaceFolding(value));
                }
        }

        #endregion Properties

        #region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="AddressedHeaderFieldBase"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <overloads>
		/// <summary>The method has five overloads.</summary>
		/// </overloads>
		protected AddressedHeaderFieldBase()
			: base()
			{
			Init();
			}
        /// <summary>
        /// Initializes a new instance of the <see cref="AddressedHeaderFieldBase"/> class.
        /// </summary>
        /// <param name="uri">The initiator's URI string.</param> 
        protected AddressedHeaderFieldBase(string uri)
            : this(new SipUri(uri))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressedHeaderFieldBase"/> class.
        /// </summary>
        /// <param name="uri">The recipient's URI.</param>
        protected AddressedHeaderFieldBase(SipUri uri)
            : this(uri, string.Empty)
        {
        }
 
        /// <summary>
        /// Initializes a new instance of the <see cref="AddressedHeaderFieldBase"/> class.
        /// </summary>
        /// <param name="uri">The recipient's URI.</param>
        /// <param name="displayName">The recipient's display name.</param>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="displayName"/>.</exception> 
        protected AddressedHeaderFieldBase(SipUri uri, string displayName)
            : base(uri)
        {
            Init();
            PropertyVerifier.ThrowOnNullArgument(displayName, "displayName");
            DisplayName = displayName;
        }



        #endregion Constructors

        #region Methods

        /// <summary>
        /// Parses string representation of the HeaderField.
        /// </summary>
        /// <remarks> 
        /// <b>RFC 3261 Syntax:</b> 
        /// <table >
        /// <tr><td style="border-bottom:none">display-name = </td><td style="border-bottom:none">*(token LWS)/ quoted-string</td></tr> 
        /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr>
        /// <tr><td style="border-bottom:none">quoted-string = </td><td style="border-bottom:none">SWS DOUBLE_QUOTE *(qdtext / quoted-pair ) DOUBLE_QUOTE</td></tr>
        /// <tr><td style="border-bottom:none">qdtext = </td><td style="border-bottom:none">LWS / %x21 / %x23-5B / %x5D-7E / UTF8-NONASCII</td></tr>
        /// <tr><td style="border-bottom:none">LWS = </td><td style="border-bottom:none">[*WSP CRLF] 1*WSP ; linear whitespace</td></tr>
        /// <tr><td style="border-bottom:none">SWS = </td><td style="border-bottom:none">[LWS] ; sep whitespace</td></tr>
        /// </table >
        ///  <example>
        /// <list type="bullet">
        /// <item>Contact: "Mr. Watson" &lt;sip:watson@worcester.bell-telephone.co&gt; ;q=0.7; expires=3600, "Mr. Watson" &lt;mailto:watson@bell-telephone.com&gt; ;q=0.1</item>  
        /// <item>Reply-To: Bob &lt;sip:bob@biloxi.com&gt;</item>  
        /// <item>To: The Operator <sip:operator@cs.columbia.edu>;tag=287447</item> 
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
                DisplayName = string.Empty;
                if(!string.IsNullOrEmpty(value))
                    {

                    Regex _display = new Regex(@"(?<=^\s*)(.|\n)+?(?=\s*(<{1}|[^<]sips?:))", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    Match m = _display.Match(value);
                    if(m != null)
                        {
                        if(!string.IsNullOrEmpty(m.Value))
                            {
							try{
                            DisplayName = Syntax.UnQuotedString(m.Value.Trim());
							}
							catch(SipException ex)
								{
								throw new SipParseException("DisplayName", SR.ParseExceptionMessage(value), ex);
								}
							catch(Exception ex)
                                {
                                throw new SipParseException(SR.GetString(SR.GeneralParseException, m.Value, "DisplayName"), ex);  
								}
                            }
                        }
                    }

                //Remove the display name as it may get confused with the underlying parses
                Regex _displayRemove = new Regex(@"^\s*(.|\n)+?\s*(?=<{1})", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                value = _displayRemove.Replace(value, "");
                value = System.Uri.UnescapeDataString(value);
                }
            base.Parse(value);
        }

        /// <summary>
        /// Gets a string representation of the HeaderField value. The HeaderField name is excluded.
        /// </summary>
        /// <remarks>This method overrides the <c>GetStringValue</c> method in <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>. </remarks>
        /// <returns>String representation of the HeaderField value</returns>
        /// <threadsafety static="true" instance="false" /> 
        protected override string GetStringValueNoParams()
        {
            StringBuilder sb = new StringBuilder(100);
            if(!(string.IsNullOrEmpty(_displayName) || _displayName == "\"\""))
                {
                sb.Append(_displayName);
                }
            string sBase = base.GetStringValueNoParams();
            if(!string.IsNullOrEmpty(sBase))
                {
                if(!(string.IsNullOrEmpty(_displayName)||_displayName=="\"\""))
                    {
                    sb.Append(SR.SingleWhiteSpace);
                    }
                sb.Append(sBase);
                }
            return sb.ToString();
        }

        /// <summary>
        /// Initialises this instance.
        /// </summary>
        private void Init()
        {
            _displayName = string.Empty;
        }

        #endregion Methods
    }
}