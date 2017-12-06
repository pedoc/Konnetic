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
    /// <summary> 
    /// The Content-Type HeaderField indicates the media type of the message-body sent to the recipient. The "media-type" element is defined in. SIP uses Internet Media Types in the Content-Type and Accept HeaderFields in order to provide open and extensible data typing and type negotiation.
    /// </summary>
    /// <remarks> 
    /// <b>Standards: RFC3261, RFC2616</b>
    ///  <para/>
    /// The Content-Type HeaderField must be present if the body is not empty. If the body is empty, and a Content-Type HeaderField is present, it indicates that the body of the specific type has zero length (for example, an empty audio file). 
    /// <para/>
    /// The type, subtype, and parameter attribute names are case-insensitive. Parameter values might or might not be casesensitive, depending on the semantics of the parameter name. Linear white space (LWS) must not be used between the type and subtype, nor between an attribute and its value. The presence or absence of a parameter might be significant to the processing of a media-type, depending on its definition within the media type registry.
    /// <para/>
    /// The asterisk "*" character is used to group media types into ranges, with "*/*" indicating all media types and "type/*" indicating all subtypes of that type. The media-range may include media type parameters that are applicable to that range.
    /// <para/>
    /// <b>RFC 3261 Syntax:</b> 
    /// <table >
    /// <tr><td colspan="2" style="border-bottom:none">( "Content-Type" / "c" ) ":" media-type</td></tr>  
    /// <tr><td style="border-bottom:none">media-type = </td><td style="border-bottom:none">m-type SLASH m-subtype *(SEMI m-parameter)</td></tr>
    /// <tr><td style="border-bottom:none">m-type = </td><td style="border-bottom:none">"text" / "image" / "audio" / "video" / "application" / token / composite-type</td></tr>
    /// <tr><td style="border-bottom:none">composite-type = </td><td style="border-bottom:none">"message" / "multipart" / token</td></tr>
    /// <tr><td style="border-bottom:none">m-parameter = </td><td style="border-bottom:none">token EQUAL token / quoted-string</td></tr>  
    /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr>
    /// <tr><td style="border-bottom:none">quoted-string = </td><td style="border-bottom:none">SWS DOUBLE_QUOTE *(qdtext / quoted-pair ) DOUBLE_QUOTE</td></tr>
    /// <tr><td style="border-bottom:none">qdtext = </td><td style="border-bottom:none">LWS / %x21 / %x23-5B / %x5D-7E / UTF8-NONASCII</td></tr>
    /// <tr><td style="border-bottom:none">LWS = </td><td style="border-bottom:none">[*WSP CRLF] 1*WSP ; linear whitespace</td></tr>
    /// <tr><td style="border-bottom:none">SWS = </td><td style="border-bottom:none">[LWS] ; sep whitespace</td></tr> 
    /// </table>  
    /// <para/>
    /// <note type="implementnotes">The compact form of the Content-Type HeaderField is "c".</note> 
    /// <para/>
    /// <note type="implementnotes">This HeaderField does not allow HeaderField grouping.</note> 
    /// <example>
    /// <list type="bullet">
    /// <item>Content-Type: application/sdp</item> 
    /// <item>c: text/html; charset=ISO-8859-4</item> 
    /// </list> 
    /// </example>
    /// </remarks> 
    /// <seealso cref="T:Konnetic.Sip.Headers.AllowHeaderFieldBase"/>
    /// <seealso cref="T:Konnetic.Sip.Headers.MediaTypeHeaderFieldBase"/>
    public sealed class ContentTypeHeaderField : MediaTypeHeaderFieldBase
    {
        #region Fields

        internal const string CompareName = "CONTENT-TYPE";
        internal const string CompareShortName = "C";
        internal const string LongName = "Content-Type";
        internal const string ShortName = "c";

        #endregion Fields

        #region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ContentTypeHeaderField"/> class.
		/// </summary>
        /// <remarks>The default constructor.</remarks>
        /// <para/>
        /// <b>Default Initializations:</b>
        /// <list type="bullet">
        /// <item><see cref="P:Sip.Konnetic.Headers.AcceptHeaderField.MediaType"/> is set to "application".</item> 
        /// <item><see cref="P:Sip.Konnetic.Headers.AcceptHeaderField.MediaSubType"/> is set to "sdp".</item> 
        /// </list> </remarks>
		/// <overloads>
		/// <summary>The method has two overloads.</summary>
		/// </overloads>
        public ContentTypeHeaderField()
            : base()
            {
            MediaType = "application";
            MediaSubType = "sdp";
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentTypeHeaderField"/> class.
        /// </summary>
        /// <param name="mediaType">Type of the media.</param>
        /// <param name="mediaSubType">Type of the media sub.</param>
        public ContentTypeHeaderField(string mediaType, string mediaSubType)
            : base(mediaType, mediaSubType)
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentTypeHeaderField"/> class.
        /// </summary>
        /// <param name="mediaType">Type of the media.</param>
        /// <param name="mediaSubType">Type of the media sub.</param>
        public ContentTypeHeaderField(MediaType mediaType, string mediaSubType)
            : base(mediaType, mediaSubType)
        {
            Init();
        }

        #endregion Constructors

        #region Methods
		/// <summary>
		/// Performs an implicit conversion from <see cref="System.String"/> to <see cref="Konnetic.Sip.Headers.ContentTypeHeaderField"/>.
		/// </summary>
		/// <param name="value">The string value representing the HeaderField.</param>
        /// <returns>A new <see cref="Konnetic.Sip.Headers.ContentTypeHeaderField"/> populated from the <paramref name="value"/> parameter.</returns>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="value"/>.</exception>
        public static implicit operator ContentTypeHeaderField(String value)
        {
            PropertyVerifier.ThrowOnNullArgument(value,"value");

            ContentTypeHeaderField hf = new ContentTypeHeaderField();
            hf.Parse(value);
            return hf;
        }
		/// <summary>
		/// Performs an explicit conversion from <see cref="Konnetic.Sip.Headers.ContentTypeHeaderField"/> to <see cref="System.String"/>.
		/// </summary>
		/// <param name="headerField">The HeaderField to convert.</param>
		/// <returns>A string representation of the HeaderField.</returns>
		/// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="headerField"/>.</exception>
        public static explicit operator string(ContentTypeHeaderField headerField)
        {
            PropertyVerifier.ThrowOnNullArgument(headerField, "headerField");
            return headerField.ToString();
        }

        /// <summary>
        /// Creates a deap  copy of this instance.
        /// </summary> 
        /// <remarks>Creates and returns a deep copy of the Header. This method ensures a deep copy of the HeaderField, when a message is cloned the HeaderField can be modified without effecting the original HeaderField in the message.</remarks>
        /// <returns>A deep copy of <see cref="T:Konnetic.Sip.Headers.ContentTypeHeaderField"/>.</returns>
        /// <threadsafety static="true" instance="false" /> 
public override HeaderFieldBase Clone()
        {
        ContentTypeHeaderField newObj = new ContentTypeHeaderField(MediaType, MediaSubType);
        CopyParametersTo(newObj); 
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

            ContentTypeHeaderField p1 = obj as ContentTypeHeaderField;
            if((object)p1 == null)
                {
                HeaderFieldGroup<ContentTypeHeaderField> p = obj as HeaderFieldGroup<ContentTypeHeaderField>;
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
        /// <tr><td colspan="2" style="border-bottom:none">( "Content-Type" / "c" ) ":" media-type</td></tr>  
        /// <tr><td style="border-bottom:none">media-type = </td><td style="border-bottom:none">m-type SLASH m-subtype *(SEMI m-parameter)</td></tr>
        /// <tr><td style="border-bottom:none">m-type = </td><td style="border-bottom:none">"text" / "image" / "audio" / "video" / "application" / token / composite-type</td></tr>
        /// <tr><td style="border-bottom:none">composite-type = </td><td style="border-bottom:none">"message" / "multipart" / token</td></tr>
        /// <tr><td style="border-bottom:none">m-parameter = </td><td style="border-bottom:none">token EQUAL token / quoted-string</td></tr>  
        /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr>
        /// <tr><td style="border-bottom:none">quoted-string = </td><td style="border-bottom:none">SWS DOUBLE_QUOTE *(qdtext / quoted-pair ) DOUBLE_QUOTE</td></tr>
        /// <tr><td style="border-bottom:none">qdtext = </td><td style="border-bottom:none">LWS / %x21 / %x23-5B / %x5D-7E / UTF8-NONASCII</td></tr>
        /// <tr><td style="border-bottom:none">LWS = </td><td style="border-bottom:none">[*WSP CRLF] 1*WSP ; linear whitespace</td></tr>
        /// <tr><td style="border-bottom:none">SWS = </td><td style="border-bottom:none">[LWS] ; sep whitespace</td></tr> 
        /// </table>  
        /// <para/>
        /// <note type="implementnotes">The compact form of the Content-Type HeaderField is "c".</note> 
        /// <para/>
        /// <note type="implementnotes">This HeaderField does not allow HeaderField grouping.</note> 
        /// <example>
        /// <list type="bullet">
        /// <item>Content-Type: application/sdp</item> 
        /// <item>c: text/html; charset=ISO-8859-4</item> 
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

                Regex _mediaParamsRegex = new Regex(@"(?<=^.*\s*[\w-.!%_*+`'~]+/?[\w-.!%_*+`'~]*;)(\n|.)+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                Match m = _mediaParamsRegex.Match(value); 

                //Register Media Params
                if(m != null)
                    {

                    if(!string.IsNullOrEmpty(m.Value))
                        {
                        string MediaParams = m.Value;
                        string[] lines = MediaParams.Split(';');
                        if(lines.Length > 0)
                            {
                            for(int i = 0; i < lines.Length; i++)
                                {

                                string s = lines[i].Trim();
                                if(!string.IsNullOrEmpty(s))
                                    {
                                    SipParameter sp = new SipParameter(s);
                                    RegisterKnownParameter(sp.Name);
                                    }
                                }
                            }
                        }

                    }

                base.Parse(value);

                //Add Media Params
                if(m != null)
                    {

                    if(!string.IsNullOrEmpty(m.Value))
                        {
                        string MediaParams = m.Value;
                        string[] lines = MediaParams.Split(';');
                        if(lines.Length > 0)
                            {
                            for(int i = 0; i < lines.Length; i++)
                                {

                                string s = lines[i].Trim();
                                if(!string.IsNullOrEmpty(s))
                                    {
                                    SipParameter sp = new SipParameter(s);
                                    AddMediaParameter(sp);
                                    }
                                }
                            }
                        }

                    }
                }
        }

        private void Init()
            {
            FieldName = ContentTypeHeaderField.LongName;
            CompactName = ContentTypeHeaderField.ShortName;
            AllowMultiple = false;
            AllowGenericParameters = false;
        }

        #endregion Methods
    }
}