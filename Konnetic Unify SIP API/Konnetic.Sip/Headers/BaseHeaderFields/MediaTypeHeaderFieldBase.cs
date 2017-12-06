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
        /// The <see cref="T:Konnetic.Sip.Headers.MediaTypeHeaderFieldBase"/> provides Media and Media Subtype information for HeaderFields.
        /// </summary>
        /// <remarks>
        /// 	<b>Standards: RFC3261</b>
        /// 	<para/>The abstract base class is used by <see cref="T:Konnetic.Sip.Headers.AcceptHeaderField"/> and <see cref="T:Konnetic.Sip.Headers.ContentTypeHeaderField"/>.
        /// <para/>
        /// SIP uses Internet Media Types in order to provide open and extensible data typing and type negotiation. HeaderFields may follow the type/subtype in the form of attribute/value pairs. The type, subtype, and parameter attribute names are case-insensitive. Parameter values might or might not be casesensitive, depending on the semantics of the parameter name. Linear white space (LWS) must not be used between the type and subtype, nor between an attribute and its value. The presence or absence of a parameter might be significant to the processing of a media-type, depending on its definition within the media type registry. Note that some older SIP applications do not recognize media type parameters. When sending data to older SIP applications, implementations SHOULD only use media type parameters when they are required by that type/subtype definition. Media-type values are registered with the Internet Assigned Number Authority.
        /// <para/>
        /// 	<b>RFC 3261 Syntax:</b>
        /// 	<table>
        /// 		<tr><td style="border-bottom:none">media-type = </td><td style="border-bottom:none">m-type SLASH m-subtype *(SEMI m-parameter)</td></tr>
        /// 		<tr><td style="border-bottom:none">m-type = </td><td style="border-bottom:none">discrete-type / composite-type</td></tr>
        /// 		<tr><td style="border-bottom:none">discrete-type = </td><td style="border-bottom:none">"text" / "image" / "audio" / "video" / "application" / extension-token</td></tr>
        /// 		<tr><td style="border-bottom:none">composite-type = </td><td style="border-bottom:none">"message" / "multipart" / extension-token</td></tr>
        /// 		<tr><td style="border-bottom:none">extension-token = </td><td style="border-bottom:none">ietf-token / "x-" token</td></tr>
        /// 		<tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr>
        /// 	</table>
        /// 	<example>
        /// 		<list type="bullet">
        /// 			<item>Content-Type: application/sdp</item>
        /// 			<item>Accept: application/sdp;level=1, application/x-private, text/html</item>
        /// 		</list>
        /// 	</example>
        /// 	<seealso cref="T:Konnetic.Sip.Headers.ParamatizedHeaderFieldBase"/>
        /// 	<seealso cref="T:Konnetic.Sip.Headers.AcceptHeaderField"/>
        /// 	<seealso cref="T:Konnetic.Sip.Headers.ContentTypeHeaderField"/>
        /// </remarks>
    public abstract class MediaTypeHeaderFieldBase : ParamatizedHeaderFieldBase
    {
        #region Fields

    /// <summary>
    /// 
    /// </summary>
        private string _mediaSubType;
        /// <summary>
        /// 
        /// </summary>
        private string _mediaType;

        /// <summary>
        /// Adds the media parameter.
        /// </summary>
        /// <remarks>Media parameters are present on <see cref="T:Konnetic.Sip.Headers.ContentTypeHeaderField"/> and <see cref="T:Konnetic.Sip.Headers.AcceptHeaderField"/> header fields. They are seperated from Generic parameters (if any) by the 'q' parameter.
        /// <note type="caution"/>Use of the "q" parameter name to separate media type parameters from Accept extension parameters is due to historical practice. Although this prevents any media type parameter named "q" from being used with a media range, such an event is believed to be unlikely given the lack of any "q" parameters in the IANA media type registry and the rare usage of any media type parameters in Accept. Future media types are discouraged from registering any parameter named "q".</note>
        /// </remarks>
        /// <param name="parameter">The parameter to remove.</param>
        /// <threadsafety static="true" instance="false" /> 
        /// <exception cref="SipException">Thrown on attempted addition of the 'q' parameter.</exception>
        public void AddMediaParameter(SipParameter parameter)
            {
            if(parameter.Name.ToUpperInvariant() == "Q")
                {
                throw new SipException("'q'-value media parameters are not allowed.");
                }
            parameter.IsMediaParameter = true;
            RegisterKnownParameter(parameter.Name);
            HeaderParameters.Set(0, parameter);
            }

        /// <summary>
        /// Removes a media parameter.
        /// </summary>
        /// <remarks>Media parameters are present on <see cref="T:Konnetic.Sip.Headers.ContentTypeHeaderField"/> and <see cref="T:Konnetic.Sip.Headers.AcceptHeaderField"/> header fields. They are seperated from Generic parameters (if any) by the 'q' parameter.</remarks>
        /// <param name="parameter">The parameter to remove.</param>
        /// <threadsafety static="true" instance="false" /> 
        public void RemoveMediaParameter(SipParameter parameter)
            {
            RemoveParameter(parameter);
            UnRegisterKnownParameter(parameter.Name);
            }
        #endregion Fields

        #region Properties
        /// <summary>
        /// Gets the generic parameters.
        /// </summary>
        /// <remarks>Media parameters are present on <see cref="T:Konnetic.Sip.Headers.ContentTypeHeaderField"/> and <see cref="T:Konnetic.Sip.Headers.AcceptHeaderField"/> header fields. They are seperated from Generic parameters (if any) by the 'q' parameter.</remarks>
        /// <value>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection<SipParameter>"/> field parameter.</value>
        public System.Collections.ObjectModel.ReadOnlyCollection<SipParameter> MediaParameters
            {
            get { return new System.Collections.ObjectModel.ReadOnlyCollection<SipParameter>(HeaderParameters); }
            }
        /// <summary>
        /// Gets or sets the media subtype.
        /// </summary>
        /// <value>The type of the media subtype.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="MediaType"/>.</exception> 
        /// <exception cref="SipFormatException">Thrown when a user attempts to add non-token characters.</exception> 
        public string MediaSubType
        {
            get
                {
                return _mediaSubType;
                }
            set
                {
                PropertyVerifier.ThrowOnNullArgument(value, "MediaSubType"); 
                value = value.Trim();
                PropertyVerifier.ThrowOnInvalidToken(value, "MediaSubType");
                _mediaSubType = value;

                }
        }

        /// <summary>
        /// Gets or sets the type of the media.
        /// </summary>
        /// <value>The type of the media.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="MediaType"/>.</exception> 
        /// <exception cref="SipFormatException">Thrown when a user attempts to add non-token characters.</exception> 
        public string MediaType
        {
            get
                {
                return _mediaType;
                }
            set
            {
                PropertyVerifier.ThrowOnNullArgument(value, "MediaType"); 
                value = value.Trim();
                PropertyVerifier.ThrowOnInvalidToken(value, "MediaType");  
 
                _mediaType = value;

                }
        }

        #endregion Properties

        #region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="MediaTypeHeaderFieldBase"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <overloads>
		/// <summary>The method has two overloads.</summary>
		/// </overloads>
        protected MediaTypeHeaderFieldBase()
            : this(string.Empty,string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaTypeHeaderFieldBase"/> class.
        /// </summary>
        /// <param name="mediaType">The media.</param>
        /// <param name="mediaSubType">The media subtype.</param>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="mediaSubType"/>.</exception> 
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="mediaType"/>.</exception> 
        protected MediaTypeHeaderFieldBase(string mediaType, string mediaSubType)
            : base()
        {
            PropertyVerifier.ThrowOnNullArgument(mediaType, "mediaType");
            PropertyVerifier.ThrowOnNullArgument(mediaSubType, "mediaSubType"); 
            AllowMultiple = true;
            MediaType = mediaType;
            MediaSubType = mediaSubType; 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaTypeHeaderFieldBase"/> class.
        /// </summary>
        /// <param name="mediaType">The media.</param>
        /// <param name="mediaSubType">The media subtype.</param>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="mediaSubType"/>.</exception> 
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="mediaType"/>.</exception> 
        protected MediaTypeHeaderFieldBase(MediaType mediaType, string mediaSubType)
            : base()
            {
            PropertyVerifier.ThrowOnNullArgument(mediaType, "mediaType");
            PropertyVerifier.ThrowOnNullArgument(mediaSubType, "mediaSubType"); 

            if(mediaType == Konnetic.Sip.Headers.MediaType.All)
                {
                MediaType = new string(SR.MediaTypeDefault,1);
                }
            else
                {
                MediaType = Enum.GetName(typeof(MediaType), mediaType).ToLowerInvariant();
                }
                
            AllowMultiple = true;
            MediaSubType = mediaSubType; 
        }
 
        #endregion Constructors

        #region Methods
        ///<summary>
        /// Compare this SIP Header for equality with another <see cref="T:Konnetic.Sip.Headers.MediaTypeHeaderFieldBase"/> object.</summary>
        /// <remarks>
        /// All optional headers are compared using object equality that is each field in the header is used for comparision. When comparing HeaderFields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular HeaderField, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are case-sensitive. 
        /// </remarks>
        /// <param name="other">The <see cref="T:Konnetic.Sip.Headers.MediaTypeHeaderFieldBase"/> to compare to this instance.</param>
        /// <returns><c>true</c> if <paramref name="other" /> is an instance of this class representing the same SIP HeaderField as this, <c>false</c> otherwise.</returns>    
        /// <overloads>
        /// <summary>This method is overloaded.</summary>
        /// <remarks>Overloads allow for equality against <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>, <see cref="T:System.Object"/> and another instance of this HeaderField.</remarks>
        /// </overloads>  
        /// <threadsafety static="true" instance="false" />
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public bool Equals(MediaTypeHeaderFieldBase other)
            {
            if((object)other == null)
                {
                return false;
                }

            return base.Equals((ParamatizedHeaderFieldBase)other) && MediaType.Equals(other.MediaType, StringComparison.OrdinalIgnoreCase) && MediaSubType.Equals(other.MediaSubType, StringComparison.OrdinalIgnoreCase);
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

            MediaTypeHeaderFieldBase p1 = obj as MediaTypeHeaderFieldBase;
            if((object)p1 == null)
                {
                    return false;
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
            return base.IsValid() && !string.IsNullOrEmpty(MediaType) && !string.IsNullOrEmpty(MediaSubType);
        }
        /// <summary>
        /// Parses string representation of the HeaderField.
        /// </summary>
        /// <remarks>  
        /// <b>RFC 3261 Syntax:</b> 
        /// <table >
        /// <tr><td style="border-bottom:none">media-type = </td><td style="border-bottom:none">m-type SLASH m-subtype *(SEMI m-parameter)</td></tr> 
        /// <tr><td style="border-bottom:none">m-type = </td><td style="border-bottom:none">discrete-type / composite-type</td></tr> 
        /// <tr><td style="border-bottom:none">discrete-type = </td><td style="border-bottom:none">"text" / "image" / "audio" / "video" / "application" / extension-token</td></tr> 
        /// <tr><td style="border-bottom:none">composite-type = </td><td style="border-bottom:none">"message" / "multipart" / extension-token</td></tr> 
        /// <tr><td style="border-bottom:none">extension-token = </td><td style="border-bottom:none">ietf-token / "x-" token</td></tr> 
        /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr>
        /// </table >
        ///  <example>
        /// <list type="bullet">
        /// <item>Content-Type: application/sdp</item>  
        /// <item>Accept: application/sdp;level=1, application/x-private, text/html</item>   
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
                MediaType = string.Empty;
                MediaSubType = string.Empty;

                base.Parse(value);
                
                if(!string.IsNullOrEmpty(value))
                    {
                     

                    Regex _mediaTypeRegex = new Regex(@"(?<=^\s*)[\w-.!%_*+`'~]+(?=/)?", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    Regex _mediaSubTypeRegex = new Regex(@"(?<=^\s*[\w-.!%_*+`'~]+/)[\w-.!%_*+`'~]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    Match m = _mediaTypeRegex.Match(value);
                    if(m != null)
                        {
                        if(!string.IsNullOrEmpty(m.Value))
                            {
							try{
                            MediaType = m.Value;
							}
							catch(SipException ex)
								{
								throw new SipParseException("MediaType", SR.ParseExceptionMessage(value), ex);
								}
							catch(Exception ex)
                                {
                                throw new SipParseException(SR.GetString(SR.GeneralParseException, m.Value, "MediaType"), ex); 
								}
                            }
                        }
                    m = _mediaSubTypeRegex.Match(value);
                    if(m != null)
                        {
                        if(!string.IsNullOrEmpty(m.Value))
                            {
							try{
                            MediaSubType = m.Value;
							}
							catch(SipException ex)
								{
								throw new SipParseException("MediaSubType", SR.ParseExceptionMessage(value), ex);
								}
							catch(Exception ex)
                                {
                                throw new SipException(SR.GetString(SR.GeneralParseException, m.Value, "MediaSubType"), ex);  
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
            if(string.IsNullOrEmpty(MediaType) && string.IsNullOrEmpty(MediaSubType))
            {
            return string.Empty;
            }
            return (string.IsNullOrEmpty(MediaType) ? "*" : MediaType) + SR.MediaTypeSeperator + (MediaSubType == string.Empty ? "*" : MediaSubType);
        }

        #endregion Methods
    }
}