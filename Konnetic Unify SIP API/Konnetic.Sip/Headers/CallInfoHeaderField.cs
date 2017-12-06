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
    /// The Call-Info HeaderField provides additional information about the caller or callee, depending on whether it is found in a <see cref="T:Konnetic.Sip.Messages.Request"/> or <see cref="T:Konnetic.Sip.Messages.Response"/>. 
    /// </summary>
    /// <remarks>
    /// <b>Standards: RFC3261</b>
    /// <para/>
    /// The purpose of the URI is described by the purpose parameter. The icon parameter designates an image suitable as an iconic representation of the caller or callee. The info parameter describes the caller or callee in general, for example, through a web page. The card parameter provides a business card, for example, in vCard [37] or LDIF [38] formats. Additional tokens can be registered using IANA (Internet Assigned Numbers Authority).
    /// <b>Security</b>
    /// <para/>
    /// Use of the Call-Info HeaderField can pose a security risk. If a callee fetches the URIs provided by a malicious caller, the callee may be at risk for displaying inappropriate or offensive content, dangerous or illegal content, and so on. Therefore, it is recommended by the SIP standard that a client only render the information in the Call-Info HeaderField if it can verify the authenticity of the element that originated the HeaderField and trusts that element. This need not be the peer client; a proxy can insert this HeaderField into requests.
    /// <para/>
    /// <b>RFC 3261 Syntax:</b> 
    /// <table >
    /// <tr><td colspan="2" style="border-bottom:none">"Call-Info" ":" info *("," info)</td></tr>
    /// <tr><td style="border-bottom:none">info = </td><td style="border-bottom:none">&lt; absoluteURI &gt; *( SEMI info-param)</td></tr>
    /// <tr><td style="border-bottom:none">absoluteURI = </td><td style="border-bottom:none">scheme ":" ( ( net-path / abs-path ) [ "?" query ] / opaque-part )</td></tr>
    /// <tr><td style="border-bottom:none">info-param = </td><td style="border-bottom:none">( "purpose" EQUAL ( "icon" / "info" / "card" / token ) ) / generic-param</td></tr>
    /// </table>
    /// <para/>
    /// <note type="implementnotes">This HeaderField allows HeaderField grouping (<see cref="T:Konnetic.Sip.Headers.HeaderFieldGroup{T}"/>).</note> 
    /// <example>
    /// <list type="bullet">
    /// <item>Call-Info: &lt;http://wwww.example.com/alice/photo.jpg&gt;;purpose=icon,&lt;http://www.example.com/alice/&gt; ;purpose=info</item> 
    /// </list> 
    /// </example>
    /// </remarks>
    public sealed class CallInfoHeaderField : AbsoluteUriHeaderFieldBase
    {
        #region Fields

        internal const string CompareName = "CALL-INFO";
        internal const string CompareShortName = CompareName;

        /// <summary>
        /// 
        /// </summary>
        internal const string LongName = "Call-Info";

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
        /// Gets or sets the purpose.
        /// </summary>
        /// <value>The purpose.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="Purpose"/>.</exception> 
        /// <exception cref="SipFormatException">Thrown when a user attempts to add non-token characters.<paramref name="Purpose"/>.</exception> 
        public string Purpose
        {
            get
                {
                SipParameter sp = HeaderParameters["purpose"];

                if((object)sp == null)
                    {
                    return string.Empty;
                    }

                return sp.Value;
                }
            set
            {
            PropertyVerifier.ThrowOnNullArgument(value, "Purpose");
                value = value.Trim();
                PropertyVerifier.ThrowOnInvalidToken(value, "Purpose");

 
                if(string.IsNullOrEmpty(value))
                    {
                    RemoveParameter("purpose");
                    }
                else
                    {
                    HeaderParameters.Set("purpose", value);
                    }

                }
        }

        #endregion Properties

        #region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="CallInfoHeaderField"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <overloads>
		/// <summary>The method has four overloads.</summary>
		/// </overloads>
        public CallInfoHeaderField()
            : base()
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CallInfoHeaderField"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        public CallInfoHeaderField(string uri)
            : this(new Uri(Uri.UnescapeDataString(uri)))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CallInfoHeaderField"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="purpose">The purpose.</param>
        public CallInfoHeaderField(string uri, CallInfoPurpose purpose)
            : this(new Uri(Uri.UnescapeDataString(uri)), purpose)
            {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CallInfoHeaderField"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        public CallInfoHeaderField(Uri uri)
            : base(uri)
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CallInfoHeaderField"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="purpose">The purpose.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
        public CallInfoHeaderField(Uri uri, CallInfoPurpose purpose)
            : base(uri)
        {
            Init();
            if(purpose != CallInfoPurpose.None)
                {
                Purpose = Enum.GetName(typeof(CallInfoPurpose), purpose).ToLowerInvariant();
                }
        }

        #endregion Constructors

        #region Methods
		/// <summary>
		/// Performs an implicit conversion from <see cref="System.String"/> to <see cref="Konnetic.Sip.Headers.CallInfoHeaderField"/>.
		/// </summary>
		/// <param name="value">The string value representing the HeaderField.</param>
        /// <returns>A new <see cref="Konnetic.Sip.Headers.CallInfoHeaderField"/> populated from the <paramref name="value"/> parameter.</returns>   
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="value"/>.</exception>
        public static implicit operator CallInfoHeaderField(String value)
        {
            PropertyVerifier.ThrowOnNullArgument(value,"value");

            CallInfoHeaderField hf = new CallInfoHeaderField();
            hf.Parse(value);
            return hf;
        }
		/// <summary>
		/// Performs an explicit conversion from <see cref="Konnetic.Sip.Headers.CallInfoHeaderField"/> to <see cref="System.String"/>.
		/// </summary>
		/// <param name="headerField">The HeaderField to convert.</param>
		/// <returns>A string representation of the HeaderField.</returns>
		/// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="headerField"/>.</exception>
        public static explicit operator string(CallInfoHeaderField headerField)
        {
            PropertyVerifier.ThrowOnNullArgument(headerField, "headerField");
            return headerField.ToString();
        }

        /// <summary>
        /// Creates a deap  copy of this instance.
        /// </summary> 
        /// <remarks>Creates and returns a deep copy of the Header. This method ensures a deep copy of the HeaderField, when a message is cloned the HeaderField can be modified without effecting the original HeaderField in the message.</remarks>
        /// <returns>A deep copy of <see cref="T:Konnetic.Sip.Headers.CallInfoHeaderField"/>.</returns>
        /// <threadsafety static="true" instance="false" /> 
public override HeaderFieldBase Clone()
        {
        CallInfoHeaderField newObj = new CallInfoHeaderField(AbsoluteUri);
        CopyParametersTo(newObj); 
            newObj.AbsoluteUriSet = AbsoluteUriSet;
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

            CallInfoHeaderField p1 = obj as CallInfoHeaderField;
            if((object)p1 == null)
                {
                HeaderFieldGroup<CallInfoHeaderField> p = obj as HeaderFieldGroup<CallInfoHeaderField>;
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
        /// <tr><td colspan="2" style="border-bottom:none">"Call-Info" ":" info *("," info)</td></tr>
        /// <tr><td style="border-bottom:none">info = </td><td style="border-bottom:none">&lt; absoluteURI &gt; *( SEMI info-param)</td></tr>
        /// <tr><td style="border-bottom:none">absoluteURI = </td><td style="border-bottom:none">scheme ":" ( ( net-path / abs-path ) [ "?" query ] / opaque-part )</td></tr>
        /// <tr><td style="border-bottom:none">info-param = </td><td style="border-bottom:none">( "purpose" EQUAL ( "icon" / "info" / "card" / token ) ) / generic-param</td></tr>
        /// </table>
        /// <para/>
        /// <note type="implementnotes">This HeaderField allows HeaderField grouping (<see cref="T:Konnetic.Sip.Headers.HeaderFieldGroup{T}"/>).</note> 
        /// <example>
        /// <list type="bullet">
        /// <item>Call-Info: <http://wwww.example.com/alice/photo.jpg>;purpose=icon,<http://www.example.com/alice/> ;purpose=info</item> 
        /// </list> 
        /// </example>
        /// </remarks>
        /// <param name="value">The HeaderField string to parse.</param>
        /// <exception cref="T:Konnetic.Sip.SipParseException">Thrown when an invalid (non-standard) value is encountered.</exception>
        /// <exception cref="T:Konnetic.Sip.SipException">Thrown when a processing exception is encountered.</exception>
        /// <threadsafety static="true" instance="false" />
        /// <seealso cref="T:Konnetic.Sip.Headers.AlertInfoHeaderField"/> 
        public override void Parse(string value)
        {
            if(value != null)
                {
                RemoveFieldName(ref value, FieldName, CompactName);
                Purpose = string.Empty;
                base.Parse(value);
                if(!string.IsNullOrEmpty(value))
                    {
                    Regex _purposeRegex = new Regex(@"(?<=(.|\n)*purpose\s*=\s*)[\w-.!%_*+`'~]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    Match m = _purposeRegex.Match(value);
                    if(m != null)
                        {
                        if(!string.IsNullOrEmpty(m.Value))
                            {
                            try
                                {
                                Purpose = m.Value.Trim();
                                }
                            catch(SipException ex)
                                {
                                throw new SipParseException("Purpose", SR.ParseExceptionMessage(value), ex);
                                }
                            catch(Exception ex)
                                {
                                throw new SipParseException(SR.GetString(SR.GeneralParseException, m.Value, "Purpose"), ex); 
                                }
                            }
                        }

                    }

                }
        }

        private void Init()
            {
            RegisterKnownParameter("purpose");
            AllowMultiple = true;
            FieldName = CallInfoHeaderField.LongName;
            CompactName = CallInfoHeaderField.LongName;
        }

        #endregion Methods
    }
}