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
    /// <summary>The Organization HeaderField conveys the name of the organization to which the SIP element issuing the request or response belongs. 
    /// </summary>
    /// <remarks> 
    /// <b>Standards: RFC3261, RFC2616</b>
    /// <para/>
    /// The field may be used by client software to filter calls.
    /// <para/>
    /// <b>RFC 3261 Syntax:</b> 
    /// <table >
    /// <tr><td colspan="2" style="border-bottom:none">"Organization" ":" [TRIMMED_UTF8_TEXT]; The TRIMMED_UTF8_TEXT rule is used for descriptive field contents that are not quoted strings, where leading and trailing whitespace is not meaningful.</td></tr> 
    /// </table>  
    /// <para/>
    /// <note type="implementnotes">This HeaderField does not allow HeaderField grouping.</note> 
    /// <example>
    /// <list type="bullet">
    /// <item>Organization: Boxes by Bob</item>  
    /// </list> 
    /// </example>
    /// </remarks> 
    /// <seealso cref="T:Konnetic.Sip.Headers.MediaTypeHeaderFieldBase"/>
    public sealed class OrganizationHeaderField : HeaderFieldBase
    {
        #region Fields

        internal const string CompareName = "ORGANIZATION";
        internal const string CompareShortName = CompareName;
        internal const string LongName = "Organization";

        private string _organization;

        #endregion Fields

        #region Properties

        public string Organization
        {
            get { return _organization; }
              set {
              PropertyVerifier.ThrowOnNullArgument(value,"Organization");
             _organization = Syntax.ConvertToTextUtf8Trim(value);
              }
        }

        #endregion Properties

        #region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="OrganizationHeaderField"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <overloads>
		/// <summary>The method has one overload.</summary>
		/// </overloads>
        public OrganizationHeaderField()
            : this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationHeaderField"/> class.
        /// </summary>
        /// <param name="organization">The organization.</param>
        public OrganizationHeaderField(string organization)
            : base()
        {
            PropertyVerifier.ThrowOnNullArgument(organization, "organization");
            Organization = organization;
            AllowMultiple = false;
            FieldName = OrganizationHeaderField.LongName;
            CompactName = OrganizationHeaderField.LongName;
        }

        #endregion Constructors

        #region Methods
		/// <summary>
		/// Performs an implicit conversion from <see cref="System.String"/> to <see cref="Konnetic.Sip.Headers.OrganizationHeaderField"/>.
		/// </summary>
		/// <param name="value">The string value representing the HeaderField.</param>
        /// <returns>A new <see cref="Konnetic.Sip.Headers.OrganizationHeaderField"/> populated from the <paramref name="value"/> parameter.</returns>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="value"/>.</exception>
        public static implicit operator OrganizationHeaderField(String value)
        {
            PropertyVerifier.ThrowOnNullArgument(value,"value");

            OrganizationHeaderField hf = new OrganizationHeaderField();
            hf.Parse(value);
            return hf;
        }
		/// <summary>
		/// Performs an explicit conversion from <see cref="Konnetic.Sip.Headers.OrganizationHeaderField"/> to <see cref="System.String"/>.
		/// </summary>
		/// <param name="headerField">The HeaderField to convert.</param>
		/// <returns>A string representation of the HeaderField.</returns>
		/// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="headerField"/>.</exception>
        public static explicit operator string(OrganizationHeaderField headerField)
        {
            PropertyVerifier.ThrowOnNullArgument(headerField, "headerField");
            return headerField.ToString();
        }

        /// <summary>
        /// Creates a deap  copy of this instance.
        /// </summary> 
        /// <remarks>Creates and returns a deep copy of the Header. This method ensures a deep copy of the HeaderField, when a message is cloned the HeaderField can be modified without effecting the original HeaderField in the message.</remarks>
        /// <returns>A deep copy of <see cref="T:Konnetic.Sip.Headers.OrganizationHeaderField"/>.</returns>
        /// <threadsafety static="true" instance="false" /> 
public override HeaderFieldBase Clone()
        {
            OrganizationHeaderField newObj = new OrganizationHeaderField(Organization);
            return newObj;
        }
///<summary>
/// Compare this SIP Header for equality with another <see cref="T:Konnetic.Sip.Headers.OrganizationHeaderField"/> object.</summary>
/// <remarks>
/// All optional headers are compared using object equality that is each field in the header is used for comparision. When comparing HeaderFields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular HeaderField, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are case-sensitive. 
/// </remarks>
/// <param name="other">The <see cref="T:Konnetic.Sip.Headers.OrganizationHeaderField"/> to compare to this instance.</param>
/// <returns><c>true</c> if <paramref name="other" /> is an instance of this class representing the same SIP HeaderField as this, <c>false</c> otherwise.</returns>    
/// <overloads>
/// <summary>This method is overloaded.</summary>
/// <remarks>Overloads allow for equality against <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>, <see cref="T:System.Object"/> and another instance of this HeaderField.</remarks>
/// </overloads>  
/// <threadsafety static="true" instance="false" />
[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
public bool Equals(OrganizationHeaderField other)
    {
    if((object)other == null)
        {
        return false;
        }

    return base.Equals((HeaderFieldBase)other) && Organization.Equals(other.Organization, StringComparison.Ordinal);
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

            OrganizationHeaderField p = obj as OrganizationHeaderField;
            if((object)p == null)
                {
                HeaderFieldGroup<OrganizationHeaderField> p1 = obj as HeaderFieldGroup<OrganizationHeaderField>;
                if((object)p1 == null)
                    {
                    return false;
                    }
                else
                    {
                    return p1.Equals(this);
                    }
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
            return base.IsValid() && !String.IsNullOrEmpty(Organization);
        }
        /// <summary>
        /// Parses string representation of the HeaderField.
        /// </summary>
        /// <remarks> 
        /// <b>RFC 3261 Syntax:</b> 
        /// <table >
        /// <tr><td colspan="2" style="border-bottom:none">"Organization" ":" [TRIMMED_UTF8_TEXT]; The TRIMMED_UTF8_TEXT rule is used for descriptive field contents that are not quoted strings, where leading and trailing whitespace is not meaningful.</td></tr> 
        /// </table>  
        /// <para/>
        /// <note type="implementnotes">This HeaderField does not allow HeaderField grouping.</note> 
        /// <example>
        /// <list type="bullet">
        /// <item>Organization: Boxes by Bob</item>  
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
                Organization = string.Empty;
                if(!string.IsNullOrEmpty(value))
                    {

                    Regex _organizationRegex = new Regex(@"(.|\n)*", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    Match m = _organizationRegex.Match(value);
                    if(m != null)
                        {
						if(!string.IsNullOrEmpty(m.Value))
							{
							try{
							Organization = m.Value;
							}
							catch(SipException ex)
								{
								throw new SipParseException("Organization", SR.ParseExceptionMessage(value), ex);
								}
							catch(Exception ex)
								{
                                throw new SipParseException("Invalid HeaderField: Organization.", ex);
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
            return Organization;
        }

        #endregion Methods
    }
}