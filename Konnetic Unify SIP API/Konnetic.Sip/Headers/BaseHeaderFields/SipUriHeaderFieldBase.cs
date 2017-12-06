/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;

namespace Konnetic.Sip.Headers
{
    /// <summary> 
    /// The <see cref="T:Konnetic.Sip.Headers.SipUriHeaderFieldBase"/> provides SIP-URI information for HeaderFields.
    /// </summary>
    /// <remarks>
    /// <b>Standards: RFC3261</b>
    /// <para>The <see cref="T:Konnetic.Sip.Headers.AbsoluteUriHeaderFieldBase"/> header field is not consigned to SIP-URIs.</para>
    /// <para/>The abstract base class is used by <see cref="T:Konnetic.Sip.Headers.CallInfoHeaderField"/> and <seealso cref="T:Konnetic.Sip.Headers.AlertInfoHeaderField"/> headers.  
    /// <para/>
    /// Use of the URI in header fields can pose a security risk. If a callee fetches the URIs provided by a malicious caller, the callee may be at risk for displaying inappropriate or offensive content, dangerous or illegal content, and so on.  
    /// <para/> 
    /// <para>When comparing header fields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular header field, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are casesensitive.</para></para>
    /// <b>RFC 3261 Syntax:</b> 
    /// <table > 
    /// <tr><td style="border-bottom:none">SIP URI = </td><td style="border-bottom:none">&lt; absoluteURI &gt;</td></tr>
    /// <tr><td style="border-bottom:none">absoluteURI = </td><td style="border-bottom:none">scheme ":" ( ( net-path / abs-path ) [ "?" query ] / opaque-part )</td></tr>
    /// </table>
    /// <example>
    /// <list type="bullet">
    /// <item>Error-Info: &lt;sip:not-in-service-recording@atlanta.com&gt;</item>
    /// <item>From: "A. G. Bell" &lt;sip:agb@bell-telephone.com&gt;;tag=a48s</item> 
    /// </list> 
    /// </example>
    /// </remarks>
    /// <seealso cref="T:Konnetic.Sip.Headers.AbsoluteUriHeaderFieldBase"/> 
    /// <seealso cref="T:Konnetic.Sip.Headers.AddressedHeaderFieldBase"/> 
    /// <seealso cref="T:Konnetic.Sip.Headers.ErrorInfoHeaderField"/> 
    public abstract class SipUriHeaderFieldBase : ParamatizedHeaderFieldBase
    {
        #region Fields

        internal const string DEFAULTURI = "sip:localhost:5060";

        /// <summary>
        /// 
        /// </summary>
        private SipUri _uri;
        private bool _uriSet;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the Sip-URI.
        /// </summary>
        /// <value>The URI.</value>
        public SipUri Uri
        {
            get { return _uri; }
            set
            {

            PropertyVerifier.ThrowOnNullArgument(value, "Uri"); 
            _uriSet = true;
            _uri = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the URI has been set.
        /// </summary>
        /// <value><c>true</c> if the URI has been set; otherwise, <c>false</c>.</value>
        protected bool UriSet
        {
            get { return _uriSet; }
            set { _uriSet = value; }
        }

        #endregion Properties

        #region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="SipUriHeaderFieldBase"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <overloads>
		/// <summary>The method has three overloads.</summary>
		/// </overloads>
		protected SipUriHeaderFieldBase()
			: base()
			{
			Init(new SipUri());
			_uriSet = false;
			}
        /// <summary>
        /// Initializes a new instance of the <see cref="SipUriHeaderFieldBase"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        protected SipUriHeaderFieldBase(string uri)
            : this(new SipUri(uri))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SipUriHeaderFieldBase"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        protected SipUriHeaderFieldBase(SipUri uri)
            : base()
        {
            PropertyVerifier.ThrowOnNullArgument(uri, "uri");
            Init(uri);
        }

 
        #endregion Constructors

        #region Methods

      
        ///<summary>
        /// Compare this SIP Header for equality with another <see cref="T:Konnetic.Sip.Headers.SipUriHeaderFieldBase"/> object.</summary>
        /// <remarks>
        /// All optional headers are compared using object equality that is each field in the header is used for comparision. When comparing HeaderFields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular HeaderField, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are case-sensitive. 
        /// </remarks>
        /// <param name="other">The <see cref="T:Konnetic.Sip.Headers.SipUriHeaderFieldBase"/> to compare to this instance.</param>
        /// <returns><c>true</c> if <paramref name="other" /> is an instance of this class representing the same SIP HeaderField as this, <c>false</c> otherwise.</returns>    
        /// <overloads>
        /// <summary>This method is overloaded.</summary>
        /// <remarks>Overloads allow for equality against <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>, <see cref="T:System.Object"/> and another instance of this HeaderField.</remarks>
        /// </overloads>  
        /// <threadsafety static="true" instance="false" />
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public bool Equals(SipUriHeaderFieldBase other)
        {
            if((object)other == null)
                {
                return false;
                }
            return base.Equals((ParamatizedHeaderFieldBase)other) && this._uri.Equals(other._uri);
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
            SipUriHeaderFieldBase p = obj as SipUriHeaderFieldBase;
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
            bool retVal = base.IsValid();
            return (Uri.IsUriSet == true) && retVal;
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
                Uri = new SipUri();
                _uriSet = false;
                if(!string.IsNullOrEmpty(value))
                    {
                    //TODO This isn't standard! It can be any Absolute URI.
                    Regex _uriRegEx = new Regex(@"(?<=([^<]|\n)*)(?<=<?)(sips?:)((.|\n)+@)?([^>\s\?]+)?([^>\s;]+)?", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    Match m = _uriRegEx.Match(value);
                    string tValue = m.Value.TrimStart().TrimStart(new char[] { '<'});
                    if(m != null)
                        {
                        if(!string.IsNullOrEmpty(tValue))
                            { 
                            if(!tValue.TrimStart().ToUpperInvariant().StartsWith("SIP", StringComparison.OrdinalIgnoreCase))
                                {
                                tValue = "sip:" + tValue.TrimStart();
                                }
							try{
                            Uri = new SipUri(tValue);
							}
							catch(SipException ex)
								{
								throw new SipParseException("Uri", SR.ParseExceptionMessage(value), ex);
								}
							catch(Exception ex)
								{
                                throw new SipParseException("Invalid HeaderField: Uri.", ex);
								}
                            }
                        }
                    //Remove the Uri. It can contain ';' and '=', so it is possible to have <http://host.com/looks;like=aparameter>
                    Regex _uriRemove = new Regex(@"(?<=([^<]|\n)*)<?(sips?:)((.|\n)+@)?([^>\s\?]+)?([^>\s;]+)?>?", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    value = _uriRemove.Replace(value, "");
                    }

                    base.Parse(value);
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
            StringBuilder sb = new StringBuilder(50);

            if(_uriSet == true)
                {
                sb.Append(SR.UriStart);
                sb.Append(Uri.ToString());
                sb.Append(SR.UriEnd);

                } 
            return sb.ToString();
        }

        /// <summary>
        /// Initializes the specified type.
        /// </summary>
        /// <param name="uri">The Sip-URI.</param>
        private void Init(SipUri uri)
        {
            PropertyVerifier.ThrowOnNullArgument(uri, "uri");
            Uri = uri;
            _uriSet = uri.IsUriSet;
        }

        #endregion Methods
    }
}