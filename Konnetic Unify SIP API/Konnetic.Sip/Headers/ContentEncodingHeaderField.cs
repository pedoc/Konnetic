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
    /// The Content-Encoding HeaderField is used as a modifier to the media-type. When present, its value indicates what additional content codings have been applied to the entity-body, and thus what decoding mechanisms must be applied in order to obtain the media-type referenced by the Content-Type HeaderField. Content-Encoding is primarily used to allow a body to be compressed without losing the identity of its underlying media type.
    /// </summary>
    /// <remarks>
    /// <b>Standards: RFC3261, RFC2616</b> 
    /// <para/>
    /// Frequently, the entity is stored in coded form, transmitted directly, and only decoded by the recipient.
    /// <para/>Content coding values indicate an encoding transformation that has been or can be applied to an entity. Content codings are primarily used to allow a document to be compressed or otherwise usefully transformed without losing the identity of its underlying media type and without loss of information. Frequently, the entity is stored in coded form, transmitted directly, and only decoded by the recipient.
    /// <para/>All content-coding values are case-insensitive. Although the value describes the content coding, what is more important is that it indicates what decoding mechanism will be required to remove the encoding.
    /// <para/>The Internet Assigned Numbers Authority (IANA) acts as a registry for content-coding value tokens. Initially, the registry contains the following tokens:
    /// <list type="bullet">
    /// <item><i>gzip</i>: An encoding format produced by the file compression program "gzip" (GNU zip) as described in RFC 1952. This format is a Lempel-Ziv coding (LZ77) with a 32 bit CRC.</item>
    /// <item><i>compress</i>: The encoding format produced by the common UNIX file compression program "compress". This format is an adaptive Lempel-Ziv-Welch coding (LZW).
    /// Use of program names for the identification of encoding formats is not desirable and is discouraged for future encodings. Their use here is representative of historical practice, not good design. For compatibility with previous implementations of HTTP, applications should consider "x-gzip" and "x-compress" to be equivalent to "gzip" and "compress" respectively.</item>
    /// <item><i>deflate</i>: The "zlib" format defined in RFC 1950 in combination with the "deflate" compression mechanism described in RFC 1951.</item>
    /// <item><i>identity</i>: The default (identity) encoding; the use of no transformation whatsoever. This content-coding is used only in the Accept-Encoding header, and should not be used in the Content-Encoding header.</item>
    /// </list>
    /// If multiple encodings have been applied to an entity-body, the content codings must be listed in the order in which they were applied. Clients may apply content encodings to the body in requests. A server may apply content encodings to the bodies in responses. The server must only use encodings listed in the Accept-Encoding HeaderField in the request.
    /// <para/>
    /// <b>RFC 3261 Syntax:</b> 
    /// <table >
    /// <tr><td colspan="2" style="border-bottom:none">( "Content-Encoding" / "e" ) ":" token *("," token)</td></tr>
    /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr>
    /// </table> 
    /// <para/>
    /// <note type="implementnotes">The compact form of the Content-Encoding HeaderField is "e".</note> 
    /// <para/>
    /// <note type="implementnotes">This HeaderField allows HeaderField grouping (<see cref="T:Konnetic.Sip.Headers.HeaderFieldGroup{T}"/>).</note> 
    /// <example>
    /// <list type="bullet">
    /// <item>Content-Encoding: gzip</item> 
    /// <item>e: tar</item> 
    /// </list> 
    /// </example>
    /// </remarks>
    /// <seealso cref="T:Konnetic.Sip.Headers.AcceptEncodingTypeHeaderFieldBase"/>
    /// <seealso cref="T:Konnetic.Sip.Headers.ContentTypeHeaderFieldBase"/> 
    public sealed class ContentEncodingHeaderField : HeaderFieldBase
		{
		#region Fields

		internal const string CompareName = "CONTENT-ENCODING";
		internal const string CompareShortName = "E";
		internal const string LongName = "Content-Encoding";
		internal const string ShortName = "e";

		private string _contentEncoding;

		#endregion Fields

		#region Properties

        /// <summary>
        /// Gets or sets the content encoding.
        /// </summary>
        /// <value>The content encoding.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="ContentEncoding"/>.</exception> 
        /// <exception cref="SipFormatException">Thrown when a user attempts to add non-token characters.</exception>
		public string ContentEncoding
			{
			get { return _contentEncoding; }
			set
                {
                PropertyVerifier.ThrowOnNullArgument(value, "ContentEncoding");
                value = value.Trim();
                PropertyVerifier.ThrowOnInvalidToken(value, "ContentEncoding");
				_contentEncoding = value;
				}
			}

		#endregion Properties

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ContentEncodingHeaderField"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <overloads>
		/// <summary>The method has one overload.</summary>
		/// </overloads>
		public ContentEncodingHeaderField()
			: this(string.Empty)
			{
			}

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentEncodingHeaderField"/> class.
        /// </summary>
        /// <param name="contentEncoding">The content encoding.</param>
		public ContentEncodingHeaderField(string contentEncoding)
			: base()
			{
			PropertyVerifier.ThrowOnNullArgument(contentEncoding, "contentEncoding");
			ContentEncoding = contentEncoding;
			AllowMultiple = true;
			FieldName = ContentEncodingHeaderField.LongName;
			CompactName = ContentEncodingHeaderField.ShortName;
			}

		#endregion Constructors

		#region Methods
		/// <summary>
		/// Performs an implicit conversion from <see cref="System.String"/> to <see cref="Konnetic.Sip.Headers.ContentEncodingHeaderField"/>.
		/// </summary>
		/// <param name="value">The string value representing the HeaderField.</param>
        /// <returns>A new <see cref="Konnetic.Sip.Headers.ContentEncodingHeaderField"/> populated from the <paramref name="value"/> parameter.</returns>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="value"/>.</exception>
		public static implicit operator ContentEncodingHeaderField(String value)
			{
			PropertyVerifier.ThrowOnNullArgument(value, "value");

			ContentEncodingHeaderField hf = new ContentEncodingHeaderField();
			hf.Parse(value);
			return hf;
			}
		/// <summary>
		/// Performs an explicit conversion from <see cref="Konnetic.Sip.Headers.ContentEncodingHeaderField"/> to <see cref="System.String"/>.
		/// </summary>
		/// <param name="headerField">The HeaderField to convert.</param>
		/// <returns>A string representation of the HeaderField.</returns>
		/// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="headerField"/>.</exception>
		public static explicit operator string(ContentEncodingHeaderField headerField)
			{
			PropertyVerifier.ThrowOnNullArgument(headerField, "headerField");
			return headerField.ToString();
			}

        /// <summary>
        /// Creates a deap  copy of this instance.
        /// </summary> 
        /// <remarks>Creates and returns a deep copy of the Header. This method ensures a deep copy of the HeaderField, when a message is cloned the HeaderField can be modified without effecting the original HeaderField in the message.</remarks>
        /// <returns>A deep copy of <see cref="T:Konnetic.Sip.Headers.ContentEncodingHeaderField"/>.</returns>
        /// <threadsafety static="true" instance="false" /> 
public override HeaderFieldBase Clone()
			{
			ContentEncodingHeaderField newObj = new ContentEncodingHeaderField(ContentEncoding);
			return newObj;
			}
/// <summary>
/// Compare this SIP Header for equality with another <see cref="T:Konnetic.Sip.Headers.ContentEncodingHeaderField"/> object.</summary>
/// <remarks>
/// All optional headers are compared using object equality that is each field in the header is used for comparision. When comparing HeaderFields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular HeaderField, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are case-sensitive. 
/// </remarks>
/// <param name="other">The <see cref="T:Konnetic.Sip.Headers.ContentEncodingHeaderField"/> to compare to this instance.</param>
/// <returns><c>true</c> if <paramref name="other" /> is an instance of this class representing the same SIP HeaderField as this, <c>false</c> otherwise.</returns>    
/// <overloads>
/// <summary>This method is overloaded.</summary>
/// <remarks>Overloads allow for equality against <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>, <see cref="T:System.Object"/> and another instance of this HeaderField.</remarks>
/// </overloads>  
/// <threadsafety static="true" instance="false" />
[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
public bool Equals(ContentEncodingHeaderField other)
    {
    if((object)other == null)
        {
        return false;
        }

    return base.Equals((HeaderFieldBase)other) && ContentEncoding.Equals(other.ContentEncoding, StringComparison.OrdinalIgnoreCase);
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

			ContentEncodingHeaderField p1 = obj as ContentEncodingHeaderField;
			if((object)p1 == null)
				{
				HeaderFieldGroup<ContentEncodingHeaderField> p = obj as HeaderFieldGroup<ContentEncodingHeaderField>;
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
			return base.IsValid() && !string.IsNullOrEmpty(ContentEncoding);
			}
        /// <summary>
        /// Parses string representation of the HeaderField.
        /// </summary>
        /// <remarks>  
        /// <b>RFC 3261 Syntax:</b> 
        /// <table >
        /// <tr><td colspan="2" style="border-bottom:none">( "Content-Encoding" / "e" ) ":" token *("," token)</td></tr>
        /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr>
        /// </table> 
        /// <para/>
        /// <note type="implementnotes">The compact form of the Content-Encoding HeaderField is "e".</note> 
        /// <para/>
        /// <note type="implementnotes">This HeaderField allows HeaderField grouping (<see cref="T:Konnetic.Sip.Headers.HeaderFieldGroup{T}"/>).</note> 
        /// <example>
        /// <list type="bullet">
        /// <item>Content-Encoding: gzip</item> 
        /// <item>e: tar</item> 
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
				ContentEncoding = string.Empty;
				if(!string.IsNullOrEmpty(value))
					{
					Regex _contentEncodingRegex = new Regex(@"(?<=^\s*)[\w\-.!%*_+`'~]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

					Match m = _contentEncodingRegex.Match(value);
					if(m != null)
						{
						if(!string.IsNullOrEmpty(m.Value))
							{
							try
								{
								ContentEncoding = m.Value.Trim();
								}
							catch(SipException ex)
								{
								throw new SipParseException("ContentEncoding", SR.ParseExceptionMessage(value), ex);
								}
							catch(Exception ex)
                                {
                                throw new SipParseException(SR.GetString(SR.GeneralParseException, m.Value, "ContentEncoding"), ex);  
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
			return ContentEncoding;
			}

		#endregion Methods
		}
	}