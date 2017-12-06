/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Globalization;
using System.Text.RegularExpressions;

using Konnetic.Sip.Messages;

namespace Konnetic.Sip.Headers
{
    /// <summary>
    /// The Content-Language entity-HeaderField describes the natural language(s) of the intended audience for the enclosed entity. This might not be equivalent to all the languages used within the entity-body. The primary purpose of Content-Language is to allow a user to identify and differentiate entities according to the user's own preferred language.
    /// </summary>
    /// <remarks>   
    /// <b>Standards: RFC3261, RFC2616</b>
    /// <para/>
    /// <b>Language Tags</b>
    /// <para/>
    /// A language tag identifies a natural language spoken, written, or otherwise conveyed by human beings for communication of information to other human beings. Computer languages are explicitly excluded. SIP uses language tags within the Accept-Language and Content-Language fields.
    /// <para/>
    /// The syntax and registry of SIP language tags is the same as that defined by RFC 1766. In summary, a language tag is composed of 1 or more parts: A primary language tag and a possibly empty series of subtag. White space is not allowed within the tag and all tags are case-insensitive. The name space of language tags is administered by the IANA (Internet Assigned Numbers Authority).
    /// <para/>
    /// The special "*" symbol in an Content-Language field matches any available content-coding not explicitly listed in the HeaderField.
    /// If no Content-Language is specified, the default is that the content is intended for all language audiences. This might mean that the sender does not consider it to be specific to any natural language, or that the sender does not know for which language it is intended. 
    /// <para/>
    /// Multiple languages may be listed for content that is intended for multiple audiences. For example, a rendition of the "Treaty of Waitangi," presented simultaneously in the original Maori and English versions, would call for <i>Content-Language: mi, en</i>
    /// <para/>
    /// <note type="caution">However, just because multiple languages are present within an entity does not mean that it is intended for multiple linguistic audiences. An example would be a beginner's language primer, such as "A First Lesson in Latin," which is clearly intended to be used by an English-literate audience. In this case, the Content-Language would properly only include "en".</note>
    /// <para/>
    /// <note type="implementnotes">Content-Language may be applied to any media type -- it is not limited to textual documents.</note>
    /// <para/>
    /// <b>RFC 3261 Syntax:</b> 
    /// <table >
    /// <tr><td colspan="2" style="border-bottom:none">"Content-Language" ":" language-tag *("," language-tag)</td></tr>
    /// <tr><td style="border-bottom:none">language-tag = </td><td style="border-bottom:none">1*8ALPHA *( "-" 1*8ALPHA )</td></tr> 
    /// </table>  
    /// <para/>
    /// <note type="implementnotes">This HeaderField allows HeaderField grouping (<see cref="T:Konnetic.Sip.Headers.HeaderFieldGroup{T}"/>).</note> 
    /// <example>
    /// <list type="bullet">
    /// <item>Content-Language: da</item> 
    /// <item>Content-Language: mi, en</item> 
    /// </list> 
    /// </example>
    /// </remarks>
    /// <seealso cref="T:Konnetic.Sip.Headers.AcceptLanguageTypeHeaderFieldBase"/> 
    public sealed class ContentLanguageHeaderField : HeaderFieldBase
    {
        #region Fields

        internal const string CompareName = "CONTENT-LANGUAGE";
        internal const string CompareShortName = CompareName;
        internal const string LongName = "Content-Language";

        private string _languageTag;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the language tag.
        /// </summary>
        /// <value>The language tag.</value>
        public string LanguageTag
        {
            get { return _languageTag; }
            set {
            PropertyVerifier.ThrowOnNullArgument(value, "LanguageTag");
            value = value.Trim();
            if(!string.IsNullOrEmpty(value) && !Syntax.IsAlphaWithDash(value))
                {
                throw new SipFormatException("Illegal characters found in LanguageTag.");
                }
            _languageTag = value;
            }
        }

        #endregion Properties

        #region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ContentLanguageHeaderField"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <overloads>
		/// <summary>The method has two overloads.</summary>
		/// </overloads>
        public ContentLanguageHeaderField()
            : this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentLanguageHeaderField"/> class.
        /// </summary>
        /// <param name="culture">The culture.</param>
        public ContentLanguageHeaderField(CultureInfo culture)
            : this(culture.Name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentLanguageHeaderField"/> class.
        /// </summary>
        /// <param name="languageTag">The language tag.</param>
        public ContentLanguageHeaderField(string languageTag)
            : base()
        {
            PropertyVerifier.ThrowOnNullArgument(languageTag, "languageTag");
            LanguageTag = languageTag;
            AllowMultiple = true;
            FieldName = ContentLanguageHeaderField.LongName;
            CompactName = ContentLanguageHeaderField.LongName;
        }

        #endregion Constructors

        #region Methods
		/// <summary>
		/// Performs an implicit conversion from <see cref="System.String"/> to <see cref="Konnetic.Sip.Headers.ContentLanguageHeaderField"/>.
		/// </summary>
		/// <param name="value">The string value representing the HeaderField.</param>
        /// <returns>A new <see cref="Konnetic.Sip.Headers.ContentLanguageHeaderField"/> populated from the <paramref name="value"/> parameter.</returns>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="value"/>.</exception>
        public static implicit operator ContentLanguageHeaderField(String value)
        {
            PropertyVerifier.ThrowOnNullArgument(value,"value");

            ContentLanguageHeaderField hf = new ContentLanguageHeaderField();
            hf.Parse(value);
            return hf;
        }
		/// <summary>
		/// Performs an explicit conversion from <see cref="Konnetic.Sip.Headers.ContentLanguageHeaderField"/> to <see cref="System.String"/>.
		/// </summary>
		/// <param name="headerField">The HeaderField to convert.</param>
		/// <returns>A string representation of the HeaderField.</returns>
		/// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="headerField"/>.</exception>
        public static explicit operator string(ContentLanguageHeaderField headerField)
        {
            PropertyVerifier.ThrowOnNullArgument(headerField, "headerField");
            return headerField.ToString();
        }

        /// <summary>
        /// Creates a deap  copy of this instance.
        /// </summary> 
        /// <remarks>Creates and returns a deep copy of the Header. This method ensures a deep copy of the HeaderField, when a message is cloned the HeaderField can be modified without effecting the original HeaderField in the message.</remarks>
        /// <returns>A deep copy of <see cref="T:Konnetic.Sip.Headers.ContentLanguageHeaderField"/>.</returns>
        /// <threadsafety static="true" instance="false" /> 
public override HeaderFieldBase Clone()
        {
            ContentLanguageHeaderField newObj = new ContentLanguageHeaderField(LanguageTag);
            return newObj;
        }
/// <summary>
/// Compare this SIP Header for equality with another <see cref="T:Konnetic.Sip.Headers.ContentLanguageHeaderField"/> object.</summary>
/// <remarks>
/// All optional headers are compared using object equality that is each field in the header is used for comparision. When comparing HeaderFields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular HeaderField, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are case-sensitive. 
/// </remarks>
/// <param name="other">The <see cref="T:Konnetic.Sip.Headers.ContentLanguageHeaderField"/> to compare to this instance.</param>
/// <returns><c>true</c> if <paramref name="other" /> is an instance of this class representing the same SIP HeaderField as this, <c>false</c> otherwise.</returns>    
/// <overloads>
/// <summary>This method is overloaded.</summary>
/// <remarks>Overloads allow for equality against <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>, <see cref="T:System.Object"/> and another instance of this HeaderField.</remarks>
/// </overloads>  
/// <threadsafety static="true" instance="false" />
[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
public bool Equals(ContentLanguageHeaderField other)
    {
    if((object)other == null)
        {
        return false;
        }

    return base.Equals((HeaderFieldBase)other) && LanguageTag.Equals(other.LanguageTag, StringComparison.OrdinalIgnoreCase);
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

            ContentLanguageHeaderField p1 = obj as ContentLanguageHeaderField;
            if((object)p1 == null)
                {
                HeaderFieldGroup<ContentLanguageHeaderField> p = obj as HeaderFieldGroup<ContentLanguageHeaderField>;
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
            return base.IsValid() && !string.IsNullOrEmpty(LanguageTag);
        }
        /// <summary>
        /// Parses string representation of the HeaderField.
        /// </summary>
        /// <remarks>  
        /// <b>RFC 3261 Syntax:</b> 
        /// <table >
        /// <tr><td colspan="2" style="border-bottom:none">"Content-Language" ":" language-tag *("," language-tag)</td></tr>
        /// <tr><td style="border-bottom:none">language-tag = </td><td style="border-bottom:none">1*8ALPHA *( "-" 1*8ALPHA )</td></tr> 
        /// </table>  
        /// <para/>
        /// <note type="implementnotes">This HeaderField allows HeaderField grouping (<see cref="T:Konnetic.Sip.Headers.HeaderFieldGroup{T}"/>).</note> 
        /// <example>
        /// <list type="bullet">
        /// <item>Content-Language: da</item> 
        /// <item>Content-Language: mi, en</item> 
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
                LanguageTag = string.Empty;
                if(!string.IsNullOrEmpty(value))
                    {
                    Regex _contentLanguage = new Regex(@"(?<=^\s*)[\w\-]+", RegexOptions.Compiled | RegexOptions.IgnoreCase );

                    Match m = _contentLanguage.Match(value);
                    if(m != null)
                        {
                        if(!string.IsNullOrEmpty(m.Value))
                            {try{
                            LanguageTag = m.Value.Trim();
							}
							catch(SipException ex)
								{
								throw new SipParseException("LanguageTag", SR.ParseExceptionMessage(value), ex);
								}
							catch(Exception ex)
                                {
                                throw new SipParseException(SR.GetString(SR.GeneralParseException, m.Value, "LanguageTag"), ex);  
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
            return LanguageTag;
        }

        #endregion Methods
    }
}