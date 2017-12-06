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
    /// The Accept-Language request-HeaderField is similar to Accept, but restricts the set of natural languages that are preferred as a response to the request. The Accept-Language HeaderField is used in requests to indicate the preferred languages for reason phrases, session descriptions, or status responses carried as message bodies in the <see cref="T:Konnetic.Sip.Messages.Response"/>. 
    /// </summary>
    /// <remarks>
    /// <b>Standards: RFC3261, RFC2616, RFC1766</b> 
    /// <para/>
    /// The syntax and registry of SIP language tags is the same as that defined by RFC 1766. In summary, a language tag is composed of 1 or more parts: A primary language tag and a possibly empty series of subtag. White space is not allowed within the tag and all tags are case-insensitive. The name space of language tags is administered by the IANA (Internet Assigned Numbers Authority).
    /// <para/>
    /// The special "*" symbol in an Accept-Language field matches any available content-coding not explicitly listed in the HeaderField. If no Accept-Language HeaderField is present, the server should assume all languages are acceptable to the client.
    /// <para/>
    /// <b>Language Tags</b>
    /// <para/>
    /// A language tag identifies a natural language spoken, written, or otherwise conveyed by human beings for communication of information to other human beings. Computer languages are explicitly excluded. SIP uses language tags within the Accept-Language and Content-Language fields.
    /// <para/>
    /// <b>RFC 3261 Syntax:</b> 
    /// <table > 
    /// <tr><td style="border-bottom:none">language-tag = </td><td style="border-bottom:none">primary-tag *( "-" subtag )</td></tr>    
    /// <tr><td style="border-bottom:none">primary-tag = </td><td style="border-bottom:none">1*8ALPHA</td></tr>
    /// <tr><td style="border-bottom:none">subtag = </td><td style="border-bottom:none">1*8ALPHA</td></tr>
    /// </table>
    /// <example>
    /// <i>en, en-US, en-cockney, i-cherokee, x-pig-latin</i>
    /// <para/>
    /// Where any two-letter primary-tag is an ISO-639 language abbreviation and any two-letter initial subtag is an ISO-3166 country code. (The last three tags above are not registered tags; all but the last are examples of tags which could be registered in future.)
    /// </example>
    /// <para/>
    /// <b>Quality</b>
    /// <para/>
    /// Each language-range may be given an associated quality value which represents an estimate of the user's preference for the languages specified by that range. The quality value defaults to "q=1".
    /// <para/> 
    /// <note type="caution">Each language-range may be given an associated quality value which represents an estimate of the user's preference for the languages specified by that range. The quality value defaults to "q=1".</note>
    /// The language quality factor assigned to a language-tag by the Accept-Language field is the quality value of the longest language-range in the field that matches the language-tag. If no language-range in the field matches the tag, the language quality factor assigned is 0. If no Accept-Language header is present in the request, the server should assume that all languages are equally acceptable. If an Accept-Language header is present, then all languages which are assigned a quality factor greater than 0 are acceptable.
    /// <para/>
    /// <b>Other</b>
    /// <para/>
    /// It might be contrary to the privacy expectations of the user to send an Accept-Language header with the complete linguistic preferences of the user in every request.
    /// <para/>
    /// As intelligibility is highly dependent on the individual user, it is recommended that client applications make the choice of linguistic preference available to the user. If the choice is not made available, then the Accept-Language HeaderField must not be given in the request.
    /// <para/>
    /// <note type="caution">When making the choice of linguistic preference available to the user, we remind implementors of the fact that users are not familiar with the details of language matching as described above, and should provide appropriate guidance. As an example, users might assume that on selecting "en-gb", they will be served any kind of English document if British English is not available. A user agent might suggest in such a case to add "en" to get the best matching behavior.</note>
    /// <para/>
    /// <b>RFC 3261 Syntax:</b> 
    /// <table >
    /// <tr><td colspan="2" style="border-bottom:none">"Accept-Language" ":" [ language *("," language) ]</td></tr>
    /// <tr><td style="border-bottom:none">language = </td><td style="border-bottom:none">language-range *(SEMI accept-param)</td></tr>
    /// <tr><td style="border-bottom:none">language-range = </td><td style="border-bottom:none">( ( 1*8ALPHA *( "-" 1*8ALPHA ) ) / "*" )</td></tr>
    /// </table >
    /// <para/>
    /// <note type="implementnotes">This HeaderField allows HeaderField grouping (<see cref="T:Konnetic.Sip.Headers.HeaderFieldGroup{T}"/>).</note> 
    /// <example>
    /// <list type="bullet">
    /// <item>Accept-Language: da, en-gb;q=0.8, en;q=0.7 </item>
    /// </list>
    /// </example>
    /// </remarks>
    /// <seealso cref="T:Konnetic.Sip.Headers.QValueHeaderFieldBase"/>
    /// <seealso cref="T:Konnetic.Sip.Headers.ContentLanguageHeaderField"/> 
    public sealed class AcceptLanguageHeaderField : QValueHeaderFieldBase
    {
        #region Fields

        internal const string CompareName = "ACCEPT-LANGUAGE";
        internal const string CompareShortName = CompareName;

        /// <summary>
        /// The long form of the name.
        /// </summary>
        internal const string LongName = "Accept-Language";

        /// <summary>
        /// 
        /// </summary>
        private string _languageRange;

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
        /// <summary>
        /// Gets or sets the language range.
        /// </summary>
        /// <value>The language range.</value>
        public string LanguageRange
        {
            get
                {
             		return _languageRange;
                }
            set
            {

            PropertyVerifier.ThrowOnNullArgument(value, "LanguageRange");
                value = value.Trim();
                if(!string.IsNullOrEmpty(value) && !Syntax.IsUnReservedLanguageRange(value))
                    {
                    throw new SipFormatException("Invalid characters in Language Range");
                    }
                _languageRange = value;

            }
        }

        #endregion Properties

        #region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="AcceptLanguageHeaderField"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <overloads>
		/// <summary>The method has one overload.</summary>
		/// </overloads>
        public AcceptLanguageHeaderField()
            : this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AcceptLanguageHeaderField"/> class.
        /// </summary>
        /// <param name="languageRange">The language range.</param>
        public AcceptLanguageHeaderField(string languageRange)
            : base()
        {
            PropertyVerifier.ThrowOnNullArgument(languageRange, "languageRange");
            LanguageRange = languageRange;
            AllowMultiple = true;
            FieldName = AcceptLanguageHeaderField.LongName;
            CompactName = AcceptLanguageHeaderField.LongName;
        }

        #endregion Constructors

        #region Methods


		/// <summary>
		/// Performs an implicit conversion from <see cref="System.String"/> to <see cref="Konnetic.Sip.Headers.AcceptLanguageHeaderField"/>.
		/// </summary>
		/// <param name="value">The string value representing the HeaderField.</param>
        /// <returns>A new <see cref="Konnetic.Sip.Headers.AcceptLanguageHeaderField"/> populated from the <paramref name="value"/> parameter.</returns>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="value"/>.</exception>
        public static implicit operator AcceptLanguageHeaderField(String value)
        {
            PropertyVerifier.ThrowOnNullArgument(value,"value");

            AcceptLanguageHeaderField hf = new AcceptLanguageHeaderField();
            hf.Parse(value);
            return hf;
        }


		/// <summary>
		/// Performs an explicit conversion from <see cref="Konnetic.Sip.Headers.AcceptLanguageHeaderField"/> to <see cref="System.String"/>.
		/// </summary>
		/// <param name="headerField">The HeaderField to convert.</param>
		/// <returns>A string representation of the HeaderField.</returns>
		/// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="headerField"/>.</exception>
        public static explicit operator string(AcceptLanguageHeaderField headerField)
        {
            PropertyVerifier.ThrowOnNullArgument(headerField, "headerField");
            return headerField.ToString();
        }

        /// <summary>
		/// Creates a deep copy of this instance.
		/// </summary> 
		/// <remarks>Creates and returns a deep copy of the Header. This method ensures a deep copy of the HeaderField, when a message is cloned the HeaderField can be modified without effecting the original HeaderField in the message.</remarks>
        /// <returns>A deep copy of <see cref="T:Konnetic.Sip.Headers.AcceptLanguageHeaderField"/>.</returns>
/// <threadsafety static="true" instance="false" />
public override HeaderFieldBase Clone()
        {
        AcceptLanguageHeaderField newObj = new AcceptLanguageHeaderField(LanguageRange);
        CopyParametersTo(newObj); 
            return newObj;
        }

/// <summary>
/// Compare this SIP Header for equality with another <see cref="T:Konnetic.Sip.Headers.AcceptLanguageHeaderField"/> object.</summary>
/// <remarks>
/// All optional headers are compared using object equality that is each field in the header is used for comparision. When comparing HeaderFields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular HeaderField, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are case-sensitive. 
/// </remarks>
/// <param name="other">The <see cref="T:Konnetic.Sip.Headers.AcceptLanguageHeaderField"/> to compare to this instance.</param>
/// <returns><c>true</c> if <paramref name="other" /> is an instance of this class representing the same SIP HeaderField as this, <c>false</c> otherwise.</returns>    
/// <overloads>
/// <summary>This method is overloaded.</summary>
/// <remarks>Overloads allow for equality against <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>, <see cref="T:System.Object"/> and another instance of this HeaderField.</remarks>
/// </overloads>  
/// <threadsafety static="true" instance="false" />
[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
public bool Equals(AcceptLanguageHeaderField other)
    {
    if((object)other == null)
        {
        return false;
        }

    return base.Equals((QValueHeaderFieldBase)other) && LanguageRange.Equals(other.LanguageRange, StringComparison.OrdinalIgnoreCase);
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

            AcceptLanguageHeaderField p1 = obj as AcceptLanguageHeaderField;
            if((object)p1 == null)
                {
                HeaderFieldGroup<AcceptLanguageHeaderField> p = obj as HeaderFieldGroup<AcceptLanguageHeaderField>;
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
            return base.IsValid() && !string.IsNullOrEmpty(LanguageRange);
        }

        /// <summary>
        /// Parses string representation of the HeaderField.
        /// </summary>
        /// <remarks> 
        /// <b>RFC 3261 Syntax:</b> 
        /// <table >
        /// <tr><td colspan="2" style="border-bottom:none">"Accept-Language" ":" [ language *("," language) ]</td></tr>
        /// <tr><td style="border-bottom:none">language = </td><td style="border-bottom:none">language-range *(SEMI accept-param)</td></tr>
        /// <tr><td style="border-bottom:none">language-range = </td><td style="border-bottom:none">( ( 1*8ALPHA *( "-" 1*8ALPHA ) ) / "*" )</td></tr>
        /// </table >
        /// <example>
        /// <list type="bullet">
        /// <item>Accept-Language: da, en-gb;q=0.8, en;q=0.7 </item>
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
            LanguageRange = string.Empty;
                base.Parse(value);
            if(!string.IsNullOrEmpty(value))
                {

                Regex _coding = new Regex(@"(?<=^\s*)[\w-.!%_*+`'~]+|^\s*[\w-.!%_*+`'~]+[;,]?$", RegexOptions.Compiled | RegexOptions.IgnoreCase );

                Match m = _coding.Match(value);
                if(m != null)
                    {
                    if(!string.IsNullOrEmpty(m.Value))
                        {
						try{
                        LanguageRange = m.Value;
						}
						catch(SipException ex)
							{
							throw new SipParseException("LanguageRange", SR.ParseExceptionMessage(value), ex);
							}
						catch(Exception ex)
                            {
                            throw new SipParseException(SR.GetString(SR.GeneralParseException, m.Value, "LanguageRange"), ex);  
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
        protected override string GetStringValueNoParams()
        {
            return LanguageRange;
        }

        #endregion Methods
    }
}