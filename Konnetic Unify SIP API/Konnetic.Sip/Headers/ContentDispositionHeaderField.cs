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
    /// The Content-Disposition HeaderField describes how the message body or, for multipart messages, a message body part is to be interpreted by the client or server. 
    /// </summary>
    /// <remarks>
    /// <b>Standards: RFC3261, RFC2616</b>
    /// <para/>
    /// A mechanism is needed to allow the sender to transmit presentational information to the recipient; the Content-Disposition header provides this mechanism, allowing each component of a message to be tagged with an indication of its desired presentation semantics. 
    /// <para/>
    /// This SIP HeaderField extends the MIME Content-Type. Several new disposition-types of the Content-Disposition header are defined by SIP. The value session indicates that the body part describes a session, for either calls or early (pre-call) media. The value render indicates that the body part should be displayed or otherwise rendered to the user. Note that the value render is used rather than inline to avoid the connotation that the MIME body is displayed as a part of the rendering of the entire message (since the MIME bodies of SIP messages oftentimes are not displayed to users). For backward-compatibility, if the Content-Disposition HeaderField is missing, the server should assume bodies of Content-Type "application/sdp" are the disposition session, while other content types are render. 
    /// <para/>
    /// The disposition type icon indicates that the body part contains an image suitable as an iconic representation of the caller or callee that could be rendered informationally by a user agent when a message has been received, or persistently while a dialog takes place. The value alert indicates that the body part contains information, such as an audio clip, that should be rendered by the user agent in an attempt to alert the user to the receipt of a request, generally a request that initiates a dialog; this alerting body could for example be rendered as a ring tone for a phone call after a 180 Ringing provisional response has been sent.
    /// <para/>
    /// Any MIME body with a disposition-type that renders content to the user should only be processed when a message has been properly authenticated. 
    /// <para/>
    /// The handling parameter describes how the server should react if it receives a message body whose content type or disposition type it does not understand. The parameter has defined values of optional and required. If the handling parameter is missing, the value required should be assumed.    
    /// <para/>
    /// If this HeaderField is missing, the MIME type determines the default content disposition. If there is none, "render" is assumed.
     ///  <para/>
    /// <b>RFC 3261 Syntax:</b> 
    /// <table >
    /// <tr><td colspan="2" style="border-bottom:none">"Content-Disposition" ":" disp-type *( SEMI disp-param )</td></tr>
    /// <tr><td style="border-bottom:none">disp-type = </td><td style="border-bottom:none">"render" / "session" / "icon" / "alert" / token</td></tr>
    /// <tr><td style="border-bottom:none">disp-param = </td><td style="border-bottom:none">handling-param / generic-param</td></tr>
    /// <tr><td style="border-bottom:none">handling-param = </td><td style="border-bottom:none">"handling" EQUAL ( "optional" / "required" / token )</td></tr>
    /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr>
    /// </table> 
    /// <para/>
    /// <note type="implementnotes">This HeaderField does not allow HeaderField grouping.</note> 
    /// <example>
    /// <list type="bullet">
    /// <item>Content-Disposition: session</item> 
    /// </list> 
    /// </example>
    /// </remarks>
    public sealed class ContentDispositionHeaderField : ParamatizedHeaderFieldBase
    {
        #region Fields

        internal const string CompareName = "CONTENT-DISPOSITION";
        internal const string CompareShortName = CompareName;
        internal const string LongName = "Content-Disposition";

        private string _dispType;

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
        /// Gets or sets the type of the disposition.
        /// </summary>
        /// <value>The type of the disposition.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="DispositionType"/>.</exception> 
        /// <exception cref="SipFormatException">Thrown when a user attempts to add non-token characters.<paramref name="DispositionType"/>.</exception>  
        public string DispositionType
        {
            get { return _dispType; }
            set
            {
            PropertyVerifier.ThrowOnNullArgument(value, "DispositionType"); 
            value = value.Trim().ToLowerInvariant();
            PropertyVerifier.ThrowOnInvalidToken(value, "DispositionType");
            value = value == "none" ? string.Empty : value;
            _dispType = value;

            }
        }

        /// <summary>
        /// Gets or sets the handling.
        /// </summary>
        /// <value>The handling.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="Handling"/>.</exception> 
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
        public string Handling
        {
            get
                {
                SipParameter sp = HeaderParameters["handling"];

                if((object)sp == null)
                    {
                    return string.Empty;
                    }

                return sp.Value;
                }
            set
            {
            PropertyVerifier.ThrowOnNullArgument(value, "Handling"); 
                value = value.Trim();
                if(string.IsNullOrEmpty(value))
                    {
                    RemoveParameter("handling");
                    }
                else
                    {
                    HeaderParameters.Set("handling", value.ToLowerInvariant());
                    }
                }
        }

        #endregion Properties

        #region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ContentDispositionHeaderField"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <overloads>
		/// <summary>The method has three overloads.</summary>
		/// </overloads>
        public ContentDispositionHeaderField()
            : this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentDispositionHeaderField"/> class.
        /// </summary>
        /// <param name="dispositionType">Type of the disposition.</param>
        public ContentDispositionHeaderField(string dispositionType)
            : base()
        {
        PropertyVerifier.ThrowOnNullArgument(dispositionType, "dispositionType");
        RegisterKnownParameter("handling");
            DispositionType = dispositionType;
            AllowMultiple = false;
            FieldName = ContentDispositionHeaderField.LongName;
            CompactName = ContentDispositionHeaderField.LongName; 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentDispositionHeaderField"/> class.
        /// </summary>
        /// <param name="dispositionType">Type of the disposition.</param>
        public ContentDispositionHeaderField(DispositionType dispositionType) 
            :this()
            {
            if(dispositionType == Konnetic.Sip.Headers.DispositionType.None)
                {
                DispositionType = string.Empty;
                }
            else
                {
                DispositionType = Enum.GetName(typeof(DispositionType), dispositionType);
                } 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentDispositionHeaderField"/> class.
        /// </summary>
        /// <param name="dispositionType">Type of the disposition.</param>
        /// <param name="handling">The handling.</param>
        public ContentDispositionHeaderField(DispositionType dispositionType, ContentDispositionHandling handling)
            : this(dispositionType)
            {
            if(handling == Konnetic.Sip.Headers.ContentDispositionHandling.None)
                {
                Handling = string.Empty;
                }
            else
                {
                Handling = Enum.GetName(typeof(ContentDispositionHandling), handling);
                } 
        }

        #endregion Constructors

        #region Methods
		/// <summary>
		/// Performs an implicit conversion from <see cref="System.String"/> to <see cref="Konnetic.Sip.Headers.ContentDispositionHeaderField"/>.
		/// </summary>
		/// <param name="value">The string value representing the HeaderField.</param>
        /// <returns>A new <see cref="Konnetic.Sip.Headers.ContentDispositionHeaderField"/> populated from the <paramref name="value"/> parameter.</returns>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="value"/>.</exception>
        public static implicit operator ContentDispositionHeaderField(String value)
        {
            PropertyVerifier.ThrowOnNullArgument(value,"value");

            ContentDispositionHeaderField hf = new ContentDispositionHeaderField();
            hf.Parse(value);
            return hf;
        }
		/// <summary>
		/// Performs an explicit conversion from <see cref="Konnetic.Sip.Headers.ContentDispositionHeaderField"/> to <see cref="System.String"/>.
		/// </summary>
		/// <param name="headerField">The HeaderField to convert.</param>
		/// <returns>A string representation of the HeaderField.</returns>
		/// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="headerField"/>.</exception>
        public static explicit operator string(ContentDispositionHeaderField headerField)
        {
            PropertyVerifier.ThrowOnNullArgument(headerField, "headerField");
            return headerField.ToString();
        }

        /// <summary>
        /// Creates a deap  copy of this instance.
        /// </summary> 
        /// <remarks>Creates and returns a deep copy of the Header. This method ensures a deep copy of the HeaderField, when a message is cloned the HeaderField can be modified without effecting the original HeaderField in the message.</remarks>
        /// <returns>A deep copy of <see cref="T:Konnetic.Sip.Headers.ContentDispositionHeaderField"/>.</returns>
        /// <threadsafety static="true" instance="false" /> 
public override HeaderFieldBase Clone()
        {
        ContentDispositionHeaderField newObj = new ContentDispositionHeaderField(DispositionType);
        CopyParametersTo(newObj); 
            return newObj;
        }

        /// <summary>
        /// Compare this SIP Header for equality with another <see cref="T:Konnetic.Sip.Headers.ContentDispositionHeaderField"/> object.</summary>
        /// <remarks>
        /// All optional headers are compared using object equality that is each field in the header is used for comparision. When comparing HeaderFields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular HeaderField, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are case-sensitive. 
        /// </remarks>
        /// <param name="other">The <see cref="T:Konnetic.Sip.Headers.ContentDispositionHeaderField"/> to compare to this instance.</param>
        /// <returns><c>true</c> if <paramref name="other" /> is an instance of this class representing the same SIP HeaderField as this, <c>false</c> otherwise.</returns>    
        /// <overloads>
        /// <summary>This method is overloaded.</summary>
        /// <remarks>Overloads allow for equality against <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>, <see cref="T:System.Object"/> and another instance of this HeaderField.</remarks>
        /// </overloads>  
        /// <threadsafety static="true" instance="false" />
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public bool Equals(ContentDispositionHeaderField other)
        {
            if((object)other == null)
                {
                return false;
                }

            return base.Equals((ParamatizedHeaderFieldBase)other) && DispositionType.Equals(other.DispositionType, StringComparison.OrdinalIgnoreCase);
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

            ContentDispositionHeaderField p1 = obj as ContentDispositionHeaderField;
            if((object)p1 == null)
                {
                HeaderFieldGroup<ContentDispositionHeaderField> p = obj as HeaderFieldGroup<ContentDispositionHeaderField>;
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
            return base.IsValid()  && !string.IsNullOrEmpty(DispositionType);
        }
        /// <summary>
        /// Parses string representation of the HeaderField.
        /// </summary>
        /// <remarks>  
        /// <b>RFC 3261 Syntax:</b> 
        /// <table >
        /// <tr><td colspan="2" style="border-bottom:none">"Content-Disposition" ":" disp-type *( SEMI disp-param )</td></tr>
        /// <tr><td style="border-bottom:none">disp-type = </td><td style="border-bottom:none">"render" / "session" / "icon" / "alert" / token</td></tr>
        /// <tr><td style="border-bottom:none">disp-param = </td><td style="border-bottom:none">handling-param / generic-param</td></tr>
        /// <tr><td style="border-bottom:none">handling-param = </td><td style="border-bottom:none">"handling" EQUAL ( "optional" / "required" / token )</td></tr>
        /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr>
        /// </table>
        /// <para/> 
        /// <note type="implementnotes">This HeaderField does not allow HeaderField grouping.</note> 
        /// <example>
        /// <list type="bullet">
        /// <item>Content-Disposition: session</item> 
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
                DispositionType = string.Empty;
                Handling = string.Empty;
                    base.Parse(value);
                if(!string.IsNullOrEmpty(value))
                    {
                    Regex _dispTypeRegEx = new Regex(@"(?<=^\s*)[\w\-.!%*_+`'~]+", RegexOptions.Compiled | RegexOptions.IgnoreCase );

                    Regex _handlingRegex = new Regex(@"(?<=(.|\n)*handling\s*=\s*)[\w-.!%_*+`'~]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    Match m = _dispTypeRegEx.Match(value);
                    if(m != null)
                        {
                        if(!string.IsNullOrEmpty(m.Value))
                            {
                            try
                                {
							    DispositionType = m.Value.Trim();
							    }
							catch(SipException ex)
								{
                                throw new SipParseException("DispositionType", SR.ParseExceptionMessage(value), ex);
								}
							catch(Exception ex)
                                {
                                throw new SipParseException(SR.GetString(SR.GeneralParseException, m.Value, "DispositionType"), ex);  
								}
                            }
                        }
                    m = _handlingRegex.Match(value);
                    if(m != null)
                        {
                        if(!string.IsNullOrEmpty(m.Value))
                            {
                            try
                                {
                                Handling = m.Value.Trim();
                                }
                            catch(SipException ex)
                                {
                                throw new SipParseException("Handling", SR.ParseExceptionMessage(value), ex);
                                }
                            catch(Exception ex)
                                {
                                throw new SipException(SR.GetString(SR.GeneralParseException, m.Value, "Handling"), ex);  
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
            return DispositionType;
        }

        #endregion Methods
    }
}