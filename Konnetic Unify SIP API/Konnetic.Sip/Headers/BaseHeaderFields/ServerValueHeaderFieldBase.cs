/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using Konnetic.Sip.Messages;

namespace Konnetic.Sip.Headers
    {   
    /// <summary> 
    /// The <see cref="T:Konnetic.Sip.Headers.ServerValueHeaderFieldBase"/> contains information about the software used by the server to handle the request.
    /// </summary>
    /// <remarks>
    /// <b>Standards: RFC3261, RFC2616</b>
    /// <para>Revealing the specific software version of the server might allow the server to become more vulnerable to attacks against software that is known to contain security holes. Implementers should make the Server header field a configurable option.</para> 
    /// <para/>The abstract base class is used by <see cref="T:Konnetic.Sip.Headers.ServerHeaderField"/> and <see cref="T:Konnetic.Sip.Headers.UserAgentHeaderField"/> headers.  
    /// <b>RFC 3261 Syntax:</b> 
    /// <table >  
    /// <tr><td style="border-bottom:none">server-val = </td><td style="border-bottom:none">product / comment</td></tr> 
    /// <tr><td style="border-bottom:none">product = </td><td style="border-bottom:none">token [SLASH product-version]</td></tr> 
    /// <tr><td style="border-bottom:none">product-version = </td><td style="border-bottom:none">token</td></tr> 
    /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr>
    /// <tr><td style="border-bottom:none">comment = </td><td style="border-bottom:none">&lt; *(ctext / quoted-pair / comment) &gt;</td></tr>
    /// <tr><td style="border-bottom:none">ctext = </td><td style="border-bottom:none">%x21-27 / %x2A-5B / %x5D-7E / UTF8-NONASCII / LWS</td></tr>
    /// <tr><td style="border-bottom:none">quoted-string = </td><td style="border-bottom:none">SWS DOUBLE_QUOTE *(qdtext / quoted-pair ) DOUBLE_QUOTE</td></tr>
    /// <tr><td style="border-bottom:none">qdtext = </td><td style="border-bottom:none">LWS / %x21 / %x23-5B / %x5D-7E / UTF8-NONASCII</td></tr>
    /// <tr><td style="border-bottom:none">LWS = </td><td style="border-bottom:none">[*WSP CRLF] 1*WSP ; linear whitespace</td></tr>
    /// <tr><td style="border-bottom:none">SWS = </td><td style="border-bottom:none">[LWS] ; sep whitespace</td></tr>
    /// </table>  
    /// <example>
    /// <list type="bullet">
    /// <item>User-Agent: Softphone Beta1.5</item>
    /// <item>Server: HomeServer v2</item>
    /// </list>  
    /// </example>
    /// </remarks> 
    /// <seealso cref="T:Konnetic.Sip.Headers.ServerHeaderField"/> 
    /// <seealso cref="T:Konnetic.Sip.Headers.UserAgentHeaderField"/> 
    public abstract class ServerValueHeaderFieldBase : HeaderFieldBase
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private string _comment;

        /// <summary>
        /// 
        /// </summary>
        private string _productName;

        /// <summary>
        /// 
        /// </summary>
        private string _productVersion;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>The comment.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="Comment"/>.</exception> 
        public string Comment
        {
            get { return Syntax.UnCommentString(_comment); }
            set
            {
            PropertyVerifier.ThrowOnNullArgument(value, "Comment");

            _comment = Syntax.ConvertToComment(value);
                }
        }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        /// <value>The name of the product.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="ProductName"/>.</exception> 
        /// <exception cref="SipFormatException">Thrown when a user attempts to add non-token characters.</exception>
        public string ProductName
        {
            get { return _productName; }
        set
            {
            PropertyVerifier.ThrowOnNullArgument(value, "ProductName");
            PropertyVerifier.ThrowOnInvalidToken(value, "ProductName");
                 _productName = value; }
        }

        /// <summary>
        /// Gets or sets the product version.
        /// </summary>
        /// <value>The product version.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="ProductVersion"/>.</exception> 
        /// <exception cref="SipFormatException">Thrown when a user attempts to add non-token characters.</exception>
        public string ProductVersion
        {
            get { return _productVersion; }
            set
            {
            PropertyVerifier.ThrowOnNullArgument(value, "ProductVersion");
            PropertyVerifier.ThrowOnInvalidToken(value, "ProductVersion");
            _productVersion = value;
            }
        }

        #endregion Properties

        #region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ServerValueHeaderFieldBase"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <overloads>
		/// <summary>The method has three overloads.</summary>
		/// </overloads>
		protected ServerValueHeaderFieldBase()
			: this(string.Empty, string.Empty, string.Empty)
			{
			}

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerValueHeaderFieldBase"/> class.
        /// </summary>
        /// <param name="productName">Name of the product.</param>
        /// <param name="productVersion">The product version.</param>
        protected ServerValueHeaderFieldBase(string productName, string productVersion)
            : this(productName, productVersion, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerValueHeaderFieldBase"/> class.
        /// </summary>
        /// <param name="productName">Name of the product.</param>
        /// <param name="productVersion">The product version.</param>
        /// <param name="comment">A comment.</param>
        protected ServerValueHeaderFieldBase(string productName, string productVersion, string comment)
            : base()
        {
            PropertyVerifier.ThrowOnNullArgument(productName, "productName");
            PropertyVerifier.ThrowOnNullArgument(productVersion, "productVersion");
            PropertyVerifier.ThrowOnNullArgument(comment, "comment");
            AllowMultiple = true;
            Comment = comment;
            ProductVersion = productVersion;
            ProductName = productName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerValueHeaderFieldBase"/> class.
        /// </summary>
        /// <param name="comment">A comment.</param>
        protected ServerValueHeaderFieldBase(string comment)
            : this(string.Empty, string.Empty, comment)
        {
        }
        #endregion Constructors

        #region Methods

        ///<summary>
        /// Compare this SIP Header for equality with another <see cref="T:Konnetic.Sip.Headers.ServerValueHeaderFieldBase"/> object.</summary>
        /// <remarks>
        /// All optional headers are compared using object equality that is each field in the header is used for comparision. When comparing HeaderFields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular HeaderField, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are case-sensitive. 
        /// </remarks>
        /// <param name="other">The <see cref="T:Konnetic.Sip.Headers.ServerValueHeaderFieldBase"/> to compare to this instance.</param>
        /// <returns><c>true</c> if <paramref name="other" /> is an instance of this class representing the same SIP HeaderField as this, <c>false</c> otherwise.</returns>    
        /// <overloads>
        /// <summary>This method is overloaded.</summary>
        /// <remarks>Overloads allow for equality against <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>, <see cref="T:System.Object"/> and another instance of this HeaderField.</remarks>
        /// </overloads>  
        /// <threadsafety static="true" instance="false" />
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public bool Equals(ServerValueHeaderFieldBase other)
            {
            if((object)other == null)
                {
                return false;
                }

            return base.Equals((HeaderFieldBase)other) && other.ProductVersion.Equals(ProductVersion, StringComparison.OrdinalIgnoreCase) && other.ProductName.Equals(ProductName, StringComparison.OrdinalIgnoreCase) && other.Comment.Equals(Comment, StringComparison.OrdinalIgnoreCase);
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

            ServerValueHeaderFieldBase p = obj as ServerValueHeaderFieldBase;
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
            if(!string.IsNullOrEmpty(Comment))
                {
                return base.IsValid();
                }
            if(!string.IsNullOrEmpty(ProductVersion))
                {
                return base.IsValid();
                }
            return false;
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
                //CheckAndReplaceFieldName(ref value, FieldName, CompactName);
                Comment = string.Empty;
                ProductName = string.Empty;
                ProductVersion = string.Empty;
                if(!string.IsNullOrEmpty(value))
                    {

                    Regex _commentRegex = new Regex(@"(?<=^\s*(.|\n)*\s*\()(.|\n)*(?=\))", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    Regex _productRegex = new Regex(@"(?<=^\s*)[\w-.!%_*+`'~]+(?=/?[\w-.!%_*+`'~]*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    Regex _versionRegex = new Regex(@"(?<=^\s*[\w-.!%_*+`'~]+/)[\w-.!%_*+`'~]*", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    Match m = _commentRegex.Match(value);
                    if(m != null)
                        {
                        if(!string.IsNullOrEmpty(m.Value))
                            {
							try{
                            Comment = m.Value;
							}
							catch(SipException ex)
								{
								throw new SipParseException("Comment", SR.ParseExceptionMessage(value), ex);
								} 
							catch(Exception ex)
                                {
                                throw new SipParseException(SR.GetString(SR.GeneralParseException, m.Value, "Comment"), ex);  
								}
                            }
                        }
                    m = _productRegex.Match(value);
                    if(m != null)
                        {
                        if(!string.IsNullOrEmpty(m.Value))
                            {
							try{
                             ProductName = m.Value;
							}
							catch(SipException ex)
								{
								throw new SipParseException("ProductName", SR.ParseExceptionMessage(value), ex);
								}
							catch(Exception ex)
                                {
                                throw new SipParseException(SR.GetString(SR.GeneralParseException, m.Value, "ProductName"), ex);  
								}
                            }
                        }
                    m = _versionRegex.Match(value);
                    if(m != null)
                        {
                        if(!string.IsNullOrEmpty(m.Value))
                            {
							try{
                            ProductVersion = m.Value;

							}
							catch(SipException ex)
								{
								throw new SipParseException("ProductVersion", SR.ParseExceptionMessage(value), ex);
								}
							catch(Exception ex)
                                {
                                throw new SipParseException(SR.GetString(SR.GeneralParseException, m.Value, "ProductVersion"), ex);   
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
            if(!String.IsNullOrEmpty(ProductName))
                {
                if(!String.IsNullOrEmpty(ProductVersion))
                    {
                    return ProductName + SR.ServerProductSeperator + ProductVersion;
                    }
                else
                    {
                    return ProductName;
                    }
                }
            else if(!String.IsNullOrEmpty(_comment))
                {
                return _comment;
                }
            else
                {
                return string.Empty;
                }
        }

        #endregion Methods
    }
}