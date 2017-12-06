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
    /// The <see cref="T:Konnetic.Sip.Headers.AbsoluteUriHeaderFieldBase"/> provides URI information for HeaderFields.
    /// </summary>
    /// <remarks>
    /// <b>Standards: RFC3261</b>
    /// <para>The <see cref="T:Konnetic.Sip.Headers.AbsoluteUriHeaderFieldBase"/> header field is not consigned to SIP-URIs.</para>
    /// <para/>The abstract base class is used by <see cref="T:Konnetic.Sip.Headers.CallInfoHeaderField"/> and <seealso cref="T:Konnetic.Sip.Headers.AlertInfoHeaderField"/> headers.  
    /// <para/>
    /// Use of the URI in header fields can pose a security risk. If a callee fetches the URIs provided by a malicious caller, the callee may be at risk for displaying inappropriate or offensive content, dangerous or illegal content, and so on. Therefore, it is recommended that a client only render the information in the Call-Info and Alert-Info header fields if it can verify the authenticity of the element that originated the header field and trusts that element. This need not be the peer client; a proxy can insert this header field into requests.
    /// <para/>
    /// <b>RFC 3261 Syntax:</b> 
    /// <table > 
    /// <tr><td style="border-bottom:none">absoluteURI = </td><td style="border-bottom:none">scheme ":" ( ( net-path / abs-path ) [ "?" query ] / opaque-part )</td></tr>
    /// </table>
    /// <example>
    /// <list type="bullet">
    /// <item>Alert-Info: &lt;http://www.example.com/sounds/moo.wav&gt;</item>
    /// <item>Call-Info: &lt;http://wwww.example.com/alice/photo.jpg&gt;;purpose=icon,&lt;http://www.example.com/alice/&gt; ;purpose=info</item> 
    /// </list> 
    /// </example>
    /// </remarks>
    /// <seealso cref="T:Konnetic.Sip.Headers.SipUriHeaderFieldBase"/> 
    /// <seealso cref="T:Konnetic.Sip.Headers.ParamatizedHeaderFieldBase"/>
    /// <seealso cref="T:Konnetic.Sip.Headers.AlertInfoHeaderField"/>
    /// <seealso cref="T:Konnetic.Sip.Headers.CallInfoHeaderField"/> 
    public abstract class AbsoluteUriHeaderFieldBase : ParamatizedHeaderFieldBase
    {
        #region Fields

    /// <summary>
    /// The default URI used for non-SIP URIs.
    /// </summary>
        public const string DEFAULTURI = "http://localhost/";

        /// <summary>
        /// 
        /// </summary>
        private Uri _uri;
        /// <summary>
        /// 
        /// </summary>
        private bool _uriSet;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the URI.
        /// </summary>
        /// <value>The URI.</value>
        public Uri AbsoluteUri
        {
            get { return _uri; }
            set
            {
            PropertyVerifier.ThrowOnNullArgument(value, "AbsoluteUri");
            if(value.IsAbsoluteUri == false)
                {
                throw new SipUriFormatException("Not an AbsoluteUri.");
                }
            _uriSet = true;
            _uri = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the absolute URI has been set.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if the absolute URI has been set; otherwise, <c>false</c>.
        /// </value>
        protected bool AbsoluteUriSet
            {
            [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
            get { return _uriSet; }
            set { _uriSet = value; }
        }

        #endregion Properties

        #region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="AbsoluteUriHeaderFieldBase"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <overloads>
		/// <summary>The method has three overloads.</summary>
		/// </overloads>
		protected AbsoluteUriHeaderFieldBase()
			: base()
			{
			AbsoluteUri = new Uri(DEFAULTURI);
			_uriSet = false;
			}
        /// <summary>
		/// Initializes a new instance of the <see cref="AbsoluteUriHeaderFieldBase"/> class.
        /// </summary>
        /// <param name="uri">The string URI.</param>
        /// <exception cref="T:System.ArgumentNullException">Thrown when the <paramref name="obj"/>  parameter is null (<b>Nothing</b> in Visual Basic).</exception>
        protected AbsoluteUriHeaderFieldBase(string uri)
            : base()
            {
            PropertyVerifier.ThrowOnNullArgument(uri, "uri");
            AbsoluteUri = new Uri(Uri.UnescapeDataString(uri));
        }

        /// <summary>
		/// Initializes a new instance of the <see cref="AbsoluteUriHeaderFieldBase"/> class.
        /// </summary>
		/// <param name="uri">The URI.</param>		
		/// <exception cref="T:System.ArgumentNullException">Thrown when the <paramref name="obj"/>  parameter is null (<b>Nothing</b> in Visual Basic).</exception>
        protected AbsoluteUriHeaderFieldBase(Uri uri)
            : base()
        {
            PropertyVerifier.ThrowOnNullArgument(uri, "uri");
            AbsoluteUri = uri;
        }

        ///// <summary>
        ///// Initializes a new instance of the <see cref="ToHeaderField"/> class.
        ///// </summary>
        ///// <param name="uri">The URI.</param>
        ///// <param name="param">A SipParameter to initialize the  param.</param>
        ///// <exception cref="T:System.ArgumentNullException">Thrown when the <paramref name="obj"/>  parameter is null (<b>Nothing</b> in Visual Basic).</exception>
        //protected AbsoluteUriHeaderFieldBase(Uri uri, SipParameter param)
        //    : base(param)
        //{
        //    ArgumentVerification.ThrowOnNullArgument(uri, "uri");
        //    AbsoluteUri = uri;
        //}


        #endregion Constructors

        #region Methods



        /// <summary>
        /// Compare this SIP Header for equality with another <see cref="T:Konnetic.Sip.Headers.AbsoluteUriHeaderFieldBase"/> object.</summary>
        /// <remarks>
        /// All optional headers are compared using object equality that is each field in the header is used for comparision. When comparing HeaderFields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular HeaderField, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are case-sensitive. 
        /// </remarks>
        /// <param name="other">The <see cref="T:Konnetic.Sip.Headers.AbsoluteUriHeaderFieldBase"/> to compare to this instance.</param>
        /// <returns><c>true</c> if <paramref name="other" /> is an instance of this class representing the same SIP HeaderField as this, <c>false</c> otherwise.</returns>    
        /// <overloads>
        /// <summary>This method is overloaded.</summary>
        /// <remarks>Overloads allow for equality against <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>, <see cref="T:System.Object"/> and another instance of this HeaderField.</remarks>
        /// </overloads>  
        /// <threadsafety static="true" instance="false" />
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public bool Equals( AbsoluteUriHeaderFieldBase other)
        {
            if((object)other == null)
                {
                return false;
                }
            return base.Equals((ParamatizedHeaderFieldBase)other) && this.AbsoluteUri.Equals(other.AbsoluteUri);
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

            AbsoluteUriHeaderFieldBase p1 = obj as AbsoluteUriHeaderFieldBase;
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
            bool retVal = base.IsValid();
            return (!string.IsNullOrEmpty(AbsoluteUri.Authority)) && retVal;
            }

        /// <summary>
        /// Parses string representation of the HeaderField.
        /// </summary>
        /// <remarks> 
        /// <b>RFC 3261 Syntax:</b> 
        /// <table > 
        /// <tr><td style="border-bottom:none">absoluteURI = </td><td style="border-bottom:none">scheme ":" ( ( net-path / abs-path ) [ "?" query ] / opaque-part )</td></tr>
        /// </table>
        /// <example>
        /// <list type="bullet">
        /// <item>Alert-Info: &lt;http://www.example.com/sounds/moo.wav&gt;</item>
        /// <item>Call-Info: &lt;http://wwww.example.com/alice/photo.jpg&gt;;purpose=icon,&lt;http://www.example.com/alice/&gt; ;purpose=info</item> 
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
                AbsoluteUri = new Uri(DEFAULTURI);
                _uriSet = false;
                if(!string.IsNullOrEmpty(value))
                    {
                    Regex _uriRegex = new Regex(@"(?<=(.|\n)*\s?<)[\w+-.]+:[^>]+|^\s*[\w+-.]+:[^<>\s]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
						Match m = _uriRegex.Match(value);
						string tValue = m.Value;
						if(m != null)
							{
							if(!string.IsNullOrEmpty(tValue))
								{
					try
						{
								AbsoluteUri = new Uri(Uri.UnescapeDataString(tValue));
								}
					catch(SipException ex)
						{
                        throw new SipParseException("AbsoluteUri", SR.ParseExceptionMessage(value), ex);
						}
					catch(Exception ex)
                        {
                        throw new SipParseException(SR.GetString(SR.GeneralParseException, m.Value, "AbsoluteUri"), ex);   
						}
								}
							}
                    //Remove the Uri. It can contain ';' and '=', so it is possible to have http://host.com/looks;likes=a_parameter
                    Regex _uriRemove = new Regex(@"\s*<[\w+-.]+:[^>]+>|^\s*[\w+-.]+:[^<>\s]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
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
                sb.Append(AbsoluteUri.AbsoluteUri);
                sb.Append(SR.UriEnd);

                } 
            return sb.ToString();
        }

        #endregion Methods

 
	}
}