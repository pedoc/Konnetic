/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Konnetic.Sip.Headers
{
    /// <summary>
    /// The <see cref="T:Konnetic.Sip.Headers.OptionHeaderFieldBase"/> provides the Options tag for HeaderFields.
    /// </summary>
    /// <remarks>
    /// <b>Standards: RFC3261</b>
    /// <para/>The abstract base class is used by <see cref="T:Konnetic.Sip.Headers.UnsupportedHeaderField"/>,<see cref="T:Konnetic.Sip.Headers.SupportedHeaderField"/>,<see cref="T:Konnetic.Sip.Headers.RequireHeaderField"/> and <seealso cref="T:Konnetic.Sip.Headers.ProxyRequireHeaderField"/> headers.  
    /// <para/>
    /// Option tags are unique identifiers used to designate new options (extensions) in SIP. These tags are used in <see cref="T:Konnetic.Sip.Headers.RequestHeaderField"/>, <see cref="T:Konnetic.Sip.Headers.ProxyRequireHeaderField"/>, <see cref="T:Konnetic.Sip.Headers.SupportedHeaderField"/> and <see cref="T:Konnetic.Sip.Headers.UnsupportedHeaderField"/> header fields. Note that these options appear as parameters in those header fields in an option-tag = token form. Option tags are defined in standards track RFCs. This is a change from past practice, and is instituted to ensure continuing multi-vendor interoperability. An IANA registry of option tags is used to ensure easy reference.
    /// <b>RFC 3261 Syntax:</b> 
    /// <table > 
    /// <tr><td style="border-bottom:none">option-tag = </td><td style="border-bottom:none">token</td></tr>
    /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr>
    /// </table>
    /// <example>
    /// <list type="bullet">
    /// <item>Unsupported: 100rel;option=rel</item> 
    /// </list> 
    /// </example>
    /// </remarks>
    /// <seealso cref="T:Konnetic.Sip.Headers.ProxyRequireHeaderField"/>
    /// <seealso cref="T:Konnetic.Sip.Headers.RequireHeaderField"/>
    /// <seealso cref="T:Konnetic.Sip.Headers.SupportedHeaderField"/>  
    /// <seealso cref="T:Konnetic.Sip.Headers.UnsupportedHeaderField"/>  
    public abstract class OptionHeaderFieldBase : HeaderFieldBase
    {
        #region Fields

        /// <summary>
        /// Represents the field's parameter.
        /// </summary>
        private string _option;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the option.
        /// </summary>
        /// <value>The option.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="Option"/>.</exception> 
        /// <exception cref="SipFormatException">Thrown when a user attempts to add non-token characters.</exception> 
        public string Option
        {
            get { return _option; }
            set
            {
            PropertyVerifier.ThrowOnNullArgument(value, "Option");
            value = value.Trim();
            PropertyVerifier.ThrowOnInvalidToken(value, "Option");
            _option = value;
            }
        }

        #endregion Properties

        #region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="OptionHeaderFieldBase"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <overloads>
		/// <summary>The method has one overload.</summary>
		/// </overloads>
        protected OptionHeaderFieldBase()
            : this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionHeaderFieldBase"/> class.
        /// </summary>
        /// <param name="option">The option.</param>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="option"/>.</exception>
        protected OptionHeaderFieldBase(String option)
            : base()
        {
            PropertyVerifier.ThrowOnNullArgument(option, "option");
            AllowMultiple = true;
            Option = option;
        }

        #endregion Constructors

        #region Methods
        
        ///<summary>
        /// Compare this SIP Header for equality with another <see cref="T:Konnetic.Sip.Headers.OptionHeaderFieldBase"/> object.</summary>
        /// <remarks>
        /// All optional headers are compared using object equality that is each field in the header is used for comparision. When comparing HeaderFields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular HeaderField, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are case-sensitive. 
        /// </remarks>
        /// <param name="other">The <see cref="T:Konnetic.Sip.Headers.OptionHeaderFieldBase"/> to compare to this instance.</param>
        /// <returns><c>true</c> if <paramref name="other" /> is an instance of this class representing the same SIP HeaderField as this, <c>false</c> otherwise.</returns>    
        /// <overloads>
        /// <summary>This method is overloaded.</summary>
        /// <remarks>Overloads allow for equality against <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>, <see cref="T:System.Object"/> and another instance of this HeaderField.</remarks>
        /// </overloads>  
        /// <threadsafety static="true" instance="false" />
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public bool Equals(OptionHeaderFieldBase other)
        {
            if((object)other == null)
                {
                return false;
                }
            return base.Equals((HeaderFieldBase)other) && other.Option.Equals(this.Option, StringComparison.OrdinalIgnoreCase);
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
            OptionHeaderFieldBase p = obj as OptionHeaderFieldBase;
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
            return !string.IsNullOrEmpty(Option);
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
                Option = string.Empty;
                if(!string.IsNullOrEmpty(value))
                    {
                    Regex _optionRegex = new Regex(@"[\w\-.!%*_+`'~]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    Match m = _optionRegex.Match(value);
                    if(m != null)
                        {
						if(!string.IsNullOrEmpty(m.Value))
							{
							try
								{
								Option = m.Value;
								}
							catch(SipException ex)
								{
								throw new SipParseException("Option", SR.ParseExceptionMessage(value), ex);
								}
							catch(Exception ex)
                                {
                                throw new SipParseException(SR.GetString(SR.GeneralParseException, m.Value, "Option"), ex); 
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
            return Option.ToString();
        }

        #endregion Methods
    }
}