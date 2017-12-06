/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Globalization;
using System.Text.RegularExpressions; 

namespace Konnetic.Sip.Headers
{
    /// <summary>This type represents an Accept HeaderField. The Accept HeaderField field can be used to specify certain media types which are acceptable for the <see cref="T:Konnetic.Sip.Messages.Response"/>. Accept headers can be used to indicate that the <see cref="T:Konnetic.Sip.Messages.Request"/> is specifically limited to a small set of desired types, as in the case of a <see cref="T:Konnetic.Sip.Messages.Request"/> for an in-line image.</summary>
    /// <remarks>  
    /// <b>Standards: RFC3261, RFC2616</b> 
    /// <para/>
    /// The Content-Type HeaderField must be present if the body is not empty. If the body is empty, and a Content-Type HeaderField is present, it indicates that the body of the specific type has zero length (for example, an empty audio file). 
    /// <para/>
    /// The type, subtype, and parameter attribute names are case-insensitive. Parameter values might or might not be casesensitive, depending on the semantics of the parameter name. Linear white space (LWS) must not be used between the type and subtype, nor between an attribute and its value. The presence or absence of a parameter might be significant to the processing of a media-type, depending on its definition within the media type registry.
    /// The asterisk "*" character is used to group media types into ranges, with "*/*" indicating all media types and "type/*" indicating all subtypes of that type. The media-range may include media type parameters that are applicable to that range.
    /// <para/>
    /// Each media-range may be followed by one or more accept-params, beginning with the "q" parameter for indicating a relative quality factor. The first "q" parameter (if any) separates the media-range parameter(s) from the accept-params. Quality factors allow the user or user agent to indicate the relative degree of preference for that media-range, using the qvalue scale from 0 to 1 (<see cref="T:Konnetic.Sip.Headers.QValueHeaderField"/>). The default value is q=1.
/// <para/>
    /// If no AcceptHeader is present, the server should assume a media of type "application" and subType "sdp". If an Accept HeaderField is present, and if the server cannot send a response which is acceptable according to the combined Accept field value, then the server should send a 406 (not acceptable) response.
    /// <span id="Example 1"> 
    /// <para/>
	/// <b>Precedence</b><para/>
	/// Media ranges can be overridden by more specific media ranges or specific media types. If more than one media range applies to a given type, the most specific reference has precedence. The media type quality factor associated with a given type is determined by finding the media range with the highest precedence which matches that type.
    /// <para/>
    /// <b>RFC 3261 Syntax:</b> 
    /// <table >
    /// <tr><td colspan="2" style="border-bottom:none">"Accept" ":" [ accept-range *("," accept-range) ]</td></tr>
    /// <tr><td style="border-bottom:none">accept-range = </td><td style="border-bottom:none">media-range *(SEMI accept-param) </td></tr>
    /// <tr><td style="border-bottom:none">media-range = </td><td style="border-bottom:none">( "*/*" / ( m-type SLASH "*" ) / ( m-type SLASH m-subtype ) ) *( SEMI m-parameter ) </td></tr>
    /// <tr><td style="border-bottom:none">accept-param = </td><td style="border-bottom:none">("q" EQUAL qvalue) / generic-param </td></tr>
    /// <tr><td style="border-bottom:none">qvalue = </td><td style="border-bottom:none">( "0" [ "." 0*3DIGIT ] ) / ( "1" [ "." 0*3("0") ] ) </td></tr>
    /// <tr><td style="border-bottom:none">generic-param = </td><td style="border-bottom:none">token [ EQUAL gen-value ] </td></tr>
    /// <tr><td style="border-bottom:none">gen-value = </td><td style="border-bottom:none">token / host / quoted-string </td></tr>
    /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" ) </td></tr>
    /// <tr><td style="border-bottom:none">quoted-string = </td><td style="border-bottom:none">SWS DOUBLE_QUOTE *(qdtext / quoted-pair ) DOUBLE_QUOTE </td></tr>
    /// <tr><td style="border-bottom:none">qdtext = </td><td style="border-bottom:none">LWS / %x21 / %x23-5B / %x5D-7E / UTF8-NONASCII </td></tr>
    /// <tr><td style="border-bottom:none">LWS = </td><td style="border-bottom:none">[*WSP CRLF] 1*WSP ; linear whitespace </td></tr>
    /// <tr><td style="border-bottom:none">SWS = </td><td style="border-bottom:none">[LWS] ; sep whitespace </td></tr>
    /// <tr><td style="border-bottom:none">host = </td><td style="border-bottom:none">hostname / IPv4address / IPv6reference </td></tr>
    /// </table>
    /// </span>  
    /// <para/>  
    /// <note type="implementnotes">This HeaderField allows HeaderField grouping (<see cref="T:Konnetic.Sip.Headers.HeaderFieldGroup{T}"/>).</note> 
    /// <example>
    /// <span id="Example 2"><i>Accept: text/*, text/html, text/html;level=1, */*</i>
    /// <para/>
    /// Has the following precedence: 
    /// <list type="number">
    /// <item>text/html;level=1</item>
    /// <item>text/html</item>
    /// <item>text/*</item>
    /// <item>*/*</item> 
    /// </list>
    /// </span>	
    /// <para/>
    /// <span id="Example 3">
    /// <i>Accept: text/*;q=0.3, text/html;q=0.7, text/html;level=1, text/html;level=2;q=0.4, */*;q=0.5</i>
    /// <p/>
    /// Would cause the following values to be associated:
    /// <list type="number">
    /// <item>text/html;level=1 = 1</item>
    /// <item>text/html = 0.7</item>
    /// <item>text/plain = 0.3</item>
    /// <item>image/jpeg = 0.5</item> 
    /// <item>text/html;level=2 = 0.4</item> 
    /// <item>text/html;level=3 = 0.7</item> 
    /// </list>
    /// <para/>
    /// <note type="caution">A user agent might be provided with a default set of quality values for certain media ranges. However, unless the user agent is a closed system which cannot interact with other rendering agents, this default set ought to be configurable by the user.</note>
    /// <para/>
    /// </span>	
    /// <para/>
    /// <span id="Example 4"> 
    /// <i>Accept: audio/*; q=0.2, audio/basic</i>
    /// <para/>
    /// Should be interpreted as "I prefer audio/basic, but send me any audio type if it is the best available after an 80% mark-down in quality." 
    /// <para/>
    /// <i>Accept: text/plain; q=0.5, text/html, text/x-dvi; q=0.8, text/x-c</i>
    /// Verbally, this would be interpreted as "text/html and text/x-c are the preferred media types, but if they do not exist, then send the text/x-dvi entity, and if that does not exist, send the text/plain entity." 
    /// </span>	
    /// </example>
    /// </remarks>
    /// <seealso cref="Konnetic.Sip.Headers.QValueHeaderFieldBase"/>
    /// <seealso cref="Konnetic.Sip.Headers.ContentEncodingHeaderField"/>
    /// <seealso cref="Konnetic.Sip.Headers.AcceptEncodingHeaderField"/> 
    public sealed class AcceptHeaderField : MediaTypeHeaderFieldBase
    {
        #region Fields

        internal const string CompareName = "ACCEPT";
        internal const string CompareShortName = CompareName;         
        internal const string LongName = "Accept";

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
        /// Gets or sets the "Quality" value for the HeaderField.
        /// </summary>
        /// <remarks>SIP content negotiation uses short "floating point" numbers to indicate the relative importance ("weight") of various negotiable parameters. A weight is normalized to a real number in the range 0 through 1, where 0 is the minimum and 1 the maximum value. If a parameter has a quality value of 0, then content with this parameter is ‘not acceptable' for the client. SIP applications must not generate more than three digits after the decimal point. User configuration of these values should also be limited in this fashion. 
        /// <para/>
        /// <b>RFC 3261 Syntax:</b>
        /// <table > 
        /// <tr><td style="border-bottom:none">qvalue = </td><td style="border-bottom:none">( "0" [ "." 0*3DIGIT ] ) </td></tr>
        /// <tr><td style="border-bottom:none"> </td><td style="border-bottom:none">| ( "1" [ "." 0*3("0") ] ) </td></tr> 
        /// </table>
        /// <para/>
        /// <remarks>Set value to null to remove from Header. 
        /// <note type="caution">"Quality values" is a misnomer, since these values merely represent relative degradation in desired quality.</note> 
        /// </remarks>
        /// <value>The "Quality" value.</value>
		/// <exception cref="T:Sip.Konnetic.SipFormatException">Thrown on invalid <paramref name="QValue"/> format.</exception>
        /// <exception cref="T:Sip.Konnetic.SipOutOfRangeException">Thrown on <paramref name="QValue"/> not between 0 (zero) and 1 (one).</exception>
        public float? QValue
        {
            get
                {
                SipParameter sp = HeaderParameters["q"];

                if((object)sp == null)
                    {
                    return null;
                    }
                try
                    {
                    float val = float.Parse(sp.Value, System.Globalization.NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);

                    PropertyVerifier.ThrowIfFloatOutOfRange(val, 0f, 1f, "'Q'");

                    return val;
                    }
                catch(FormatException ex)
                    {
                    throw new SipException(SR.GetString(SR.FloatConvertException, sp.Value, "QValue"), ex); 
                    }
                catch(ArgumentException ex)
                    {
                    throw new SipException(SR.GetString(SR.FloatConvertException, sp.Value, "QValue"), ex); 
                    } 
                }
            set
            {
                //q is seperator between header and generic parameters
                if(value == null)
                    {
                    RemoveParameter("q");
                    }
                else
                    {
                    PropertyVerifier.ThrowIfFloatOutOfRange(value, 0f, 1f, "'Q'");
                    HeaderParameters.Set("q", ((float)value).ToString("0.###", CultureInfo.InvariantCulture));
                    }

                }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Sip.Konnetic.Headers.AcceptHeaderField"/> class.
        /// </summary>
        /// <remarks>The default constructor. 
        /// <para/>
        /// <b>Default Initializations:</b>
		/// <list type="bullet">
        /// <item><see cref="P:Sip.Konnetic.Headers.AcceptHeaderField.MediaType"/> is set to "application".</item> 
        /// <item><see cref="P:Sip.Konnetic.Headers.AcceptHeaderField.MediaSubType"/> is set to "sdp".</item> 
		/// </list> </remarks>
		/// <overloads>
		/// <summary>The method has two overloads.</summary>
        /// <remarks>The overloads allow for initialising the mediaType and mediaSubType.</remarks>
		/// </overloads>
        public AcceptHeaderField()
            : base()
            {
            Init();
            MediaType = "application";
            MediaSubType = "sdp";
        }

 

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Sip.Konnetic.Headers.AcceptHeaderField"/> class.
        /// </summary>
        /// <param name="mediaType">A media <see cref="T:System.String"/>.</param>
        /// <param name="mediaSubType">A media subtype <see cref="T:System.String"/>.</param>
        /// <inheritdoc select="overloads/*" />
 
        public AcceptHeaderField(string mediaType, string mediaSubType)
            : base(mediaType, mediaSubType)
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AcceptHeaderField"/> class.
		/// </summary>
        /// <param name="mediaType">A media <see cref="T:Sip.Konnetic.Headers.MediaType"/>.</param>
        /// <param name="mediaSubType">A media subtype <see cref="T:System.String"/>.</param>
        /// <inheritdoc select="overloads/*" />
 
        public AcceptHeaderField(MediaType mediaType, string mediaSubType)
            : base(mediaType, mediaSubType)
        {
            Init();
        }

        #endregion Constructors

        #region Methods

        /// <summary>Performs an implicit conversion from a <see cref="T:System.String"/> to a<see cref="T:Konnetic.Sip.Headers.AcceptHeaderField"/>.
		/// </summary>
		/// <param name="value">The string value representing the HeaderField.</param>
        /// <returns>A new <see cref="T:Konnetic.Sip.Headers.AcceptHeaderField"/> populated from the <paramref name="value"/> parameter.</returns>
        /// <threadsafety static="true" instance="false" /> 
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="value"/>.</exception>
        public static implicit operator AcceptHeaderField(String value)
        {
            if((object)value == null)
                {
                return null;
                }

            AcceptHeaderField hf = new AcceptHeaderField();
            hf.Parse(value);
            return hf;
        }

		/// <summary>
		/// Performs an explicit conversion from <see cref="Konnetic.Sip.Headers.AcceptHeaderField"/> to <see cref="System.String"/>.
		/// </summary>
		/// <param name="headerField">The HeaderField to convert.</param>
		/// <returns>A string representation of the HeaderField.</returns>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="headerField"/>.</exception>
        /// <threadsafety static="true" instance="false" /> 
        public static explicit operator string(AcceptHeaderField headerField)
        {
            PropertyVerifier.ThrowOnNullArgument(headerField, "headerField");
            return headerField.ToString();
        }


        /// <summary>
        /// Creates a deap  copy of this instance.
        /// </summary> 
        /// <remarks>Creates and returns a deep copy of the Header. This method ensures a deep copy of the HeaderField, when a message is cloned the HeaderField can be modified without effecting the original HeaderField in the message.</remarks>
        /// <returns>A deep copy of <see cref="T:Konnetic.Sip.Headers.AcceptHeaderField"/>.</returns>
        /// <threadsafety static="true" instance="false" /> 
public override HeaderFieldBase Clone()
        {
        AcceptHeaderField newObj = new AcceptHeaderField(MediaType, MediaSubType);
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

            AcceptHeaderField p1 = obj as AcceptHeaderField;
            if((object)p1 == null)
                {
                HeaderFieldGroup<AcceptHeaderField> p = obj as HeaderFieldGroup<AcceptHeaderField>;
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
        /// <tr><td colspan="2" style="border-bottom:none">"Accept" ":" [ accept-range *("," accept-range) ]</td></tr>
        /// <tr><td style="border-bottom:none">accept-range = </td><td style="border-bottom:none">media-range *(SEMI accept-param) </td></tr>
        /// <tr><td style="border-bottom:none">media-range = </td><td style="border-bottom:none">( "*/*" / ( m-type SLASH "*" ) / ( m-type SLASH m-subtype ) ) *( SEMI m-parameter ) </td></tr>
        /// <tr><td style="border-bottom:none">accept-param = </td><td style="border-bottom:none">("q" EQUAL qvalue) / generic-param </td></tr>
        /// <tr><td style="border-bottom:none">qvalue = </td><td style="border-bottom:none">( "0" [ "." 0*3DIGIT ] ) / ( "1" [ "." 0*3("0") ] ) </td></tr>
        /// <tr><td style="border-bottom:none">generic-param = </td><td style="border-bottom:none">token [ EQUAL gen-value ] </td></tr>
        /// <tr><td style="border-bottom:none">gen-value = </td><td style="border-bottom:none">token / host / quoted-string </td></tr>
        /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" ) </td></tr>
        /// <tr><td style="border-bottom:none">quoted-string = </td><td style="border-bottom:none">SWS DOUBLE_QUOTE *(qdtext / quoted-pair ) DOUBLE_QUOTE </td></tr>
        /// <tr><td style="border-bottom:none">qdtext = </td><td style="border-bottom:none">LWS / %x21 / %x23-5B / %x5D-7E / UTF8-NONASCII </td></tr>
        /// <tr><td style="border-bottom:none">LWS = </td><td style="border-bottom:none">[*WSP CRLF] 1*WSP ; linear whitespace </td></tr>
        /// <tr><td style="border-bottom:none">SWS = </td><td style="border-bottom:none">[LWS] ; sep whitespace </td></tr>
        /// <tr><td style="border-bottom:none">host = </td><td style="border-bottom:none">hostname / IPv4address / IPv6reference </td></tr>
        /// </table>    
        /// <para/>
        /// <note type="implementnotes">This HeaderField allows HeaderField grouping (<see cref="T:Konnetic.Sip.Headers.HeaderFieldGroup{T}"/>).</note> 
        /// <example>
        /// <list type="bullet">
        /// <item>Accept: text/*, text/html, text/html;level=1, */*</item>
        /// <item>Accept: text/*;q=0.3, text/html;q=0.7, text/html;level=1, text/html;level=2;q=0.4, */*;q=0.5</item>
        /// <item>Accept: audio/*; q=0.2, audio/basic</item>
        /// <item>Accept: text/plain; q=0.5, text/html, text/x-dvi; q=0.8, text/x-c</item>
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
            QValue = null;

            Regex _mediaParamsFindRegex = new Regex(@"(?<=^.*\s*[\w-.!%_*+`'~]+/?[\w-.!%_*+`'~]*;(\n|.)+)(;q\s*=\s*(0|1)\.?[0-9]*(\n|.)+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex _mediaParamsWithQRegex = new Regex(@"(?<=^.*\s*[\w-.!%_*+`'~]+/?[\w-.!%_*+`'~]*;)(\n|.)+(?=;q\s*=\s*(0|1)\.?[0-9]*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex _mediaParamsWithoutQRegex = new Regex(@"(?<=^.*\s*[\w-.!%_*+`'~]+/?[\w-.!%_*+`'~]*;)(\n|.)+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            Match m = _mediaParamsFindRegex.Match(value);
            //Search for Q value. If found use 'WithQRegex', otherwise use 'WithoutQRegex'
            if(m != null)
                {
                if(!string.IsNullOrEmpty(m.Value))
                    {
                    m = _mediaParamsWithQRegex.Match(value);
                    }
                else
                    {
                    m = _mediaParamsWithoutQRegex.Match(value);
                    }
                }
            else
                {
                m = _mediaParamsWithoutQRegex.Match(value);
                }

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
            if(!string.IsNullOrEmpty(value))
                {
                float? f = QValueHeaderFieldBase.ParseQValue(value);
                if(f >= 0)
                    {
					try{
                    QValue = f;
							}
							catch(SipException ex)
								{
								throw new SipParseException("QValue", SR.ParseExceptionMessage(value), ex);
								}
							catch(Exception ex)
                        {
                        throw new SipParseException(SR.GetString(SR.GeneralParseException, f.ToString(), "QValue"), ex);   
								}
                    }
                }
            }
        }

        private void Init()
            {
            RegisterKnownParameter("q");
            AllowMultiple = true;
            FieldName = AcceptHeaderField.LongName;
            CompactName = AcceptHeaderField.LongName;
        }

        #endregion Methods
    }
}