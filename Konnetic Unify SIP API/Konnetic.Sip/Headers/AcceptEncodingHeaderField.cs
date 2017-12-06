/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace Konnetic.Sip.Headers
{
	/// <summary>
    /// The type represents the Accept-Encoding HeaderField. A client includes an <see cref="T:Konnetic.Sip.Headers.AcceptEncodingHeaderField"/> in a <see cref="T:Konnetic.Sip.Messages.Request"/> to tell the server what coding schemes are acceptable in the <see cref="T:Konnetic.Sip.Messages.Response"/> e.g. compress, gzip.
	/// </summary>
	/// <remarks>
	/// <b>Standards: RFC3261, RFC2616</b>
    /// <para/>
    /// A HeaderField is a component of the SIP message header. A HeaderField can appear as one or more HeaderField rows. HeaderField rows consist of a HeaderField name and zero or more HeaderField values. Multiple HeaderField values on a given HeaderField row are separated by commas. Some HeaderFields can only have a single HeaderField value, and as a result, always appear as a single HeaderField row.<para/>
	/// If an Accept-Encoding field is present in a request, and if the server cannot send a response which is acceptable according to the Accept-Encoding header, then the server should send an error response with the 406 (Not Acceptable) status code.
    /// <para/>A server tests whether a content-coding is acceptable, according to an Accept-Encoding field, using these rules:
	/// <list type="number">
	/// <item>If the content-coding is one of the content-codings listed in the Accept-Encoding field, then it is acceptable, unless it is accompanied by a qvalue of 0. (A qvalue of 0 means "not acceptable.")</item>
	/// <item>The special "*" symbol in an Accept-Encoding field matches any available content-coding not explicitly listed in the HeaderField.</item>
	/// <item>If multiple content-codings are acceptable, then the acceptable content-coding with the highest non-zero qvalue is preferred.</item>
	/// <item>The "identity" content-coding is always acceptable, unless specifically refused because the Accept-Encoding field includes "identity;q=0", or because the field includes "*;q=0" and does not explicitly include the "identity" content-coding. </item>
	/// </list> 
    /// An empty Accept-Encoding HeaderField is permissible, it is equivalent to Accept-Encoding of "identity", meaning no encoding is permissible. If no Accept-Encoding HeaderField is present, the server should assume a default value of identity.
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
    /// <para/>
    /// <span id="Example 1"> 
    /// <para/>
    /// <b>RFC 3261 Syntax:</b> 
    /// <table >
    /// <tr><td colspan="2" style="border-bottom:none">"Accept-Encoding" ":" [ encoding *("," encoding) ]</td></tr>
    /// <tr><td style="border-bottom:none">encoding = </td><td style="border-bottom:none">codings *(SEMI accept-param)</td></tr>
    /// <tr><td style="border-bottom:none">codings = </td><td style="border-bottom:none">content-coding / "*"</td></tr>
    /// <tr><td style="border-bottom:none">content-coding = </td><td style="border-bottom:none">token</td></tr>
    /// <tr><td style="border-bottom:none">accept-param = </td><td style="border-bottom:none">("q" EQUAL qvalue) / generic-param</td></tr>
    /// <tr><td style="border-bottom:none">generic-param = </td><td style="border-bottom:none">token [ EQUAL gen-value ]</td></tr>
    /// <tr><td style="border-bottom:none">gen-value = </td><td style="border-bottom:none">token / host / quoted-string</td></tr>
    /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr>
    /// <tr><td style="border-bottom:none">quoted-string = </td><td style="border-bottom:none">SWS DOUBLE_QUOTE *(qdtext / quoted-pair ) DOUBLE_QUOTE</td></tr>
    /// <tr><td style="border-bottom:none">qdtext = </td><td style="border-bottom:none">LWS / %x21 / %x23-5B / %x5D-7E / UTF8-NONASCII</td></tr>
    /// <tr><td style="border-bottom:none">LWS = </td><td style="border-bottom:none">[*WSP CRLF] 1*WSP ; linear whitespace</td></tr>
    /// <tr><td style="border-bottom:none">SWS = </td><td style="border-bottom:none">[LWS] ; sep whitespace</td></tr>
    /// <tr><td style="border-bottom:none">host = </td><td style="border-bottom:none">hostname / IPv4address / IPv6reference</td></tr>
    /// </table>
    /// </span>
    /// <para/>
    /// <note type="implementnotes">This HeaderField allows HeaderField grouping (<see cref="T:Konnetic.Sip.Headers.HeaderFieldGroup{T}"/>).</note> 
    /// <span id="Example 2">
    /// <example>
    /// <list type="bullet">
    /// <item>Accept-Encoding: gzip </item>
    /// <item>Accept-Encoding: compress, gzip</item>
    /// <item>Accept-Encoding: </item>
    /// <item>Accept-Encoding: *</item>
    /// <item>Accept-Encoding: compress;q=0.5, gzip;q=1.0</item>
    /// <item>Accept-Encoding: gzip;q=1.0, identity; q=0.5, *;q=0</item>
    /// </list>
    /// </example> 
    /// </span>
	/// </remarks>
	/// <seealso cref="T:Konnetic.Sip.Headers.QValueHeaderFieldBase"/>
    /// <seealso cref="T:Konnetic.Sip.Headers.ContentEncodingHeaderField"/> 
    public sealed class AcceptEncodingHeaderField : QValueHeaderFieldBase
    {
        #region Fields

        internal const string CompareName = "ACCEPT-ENCODING";
        internal const string CompareShortName = CompareName;
        internal const string LongName = "Accept-Encoding";
        private string _coding;

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
        /// <param name="name">The <see cref="T:Konnetic.Sip.SipParameter"/> name.</param>
        /// <param name="value">The <see cref="T:Konnetic.Sip.SipParameter"/> value.</param>
        public void AddParameter(string name, string value)
            {
            InternalAddGenericParameter(name, value);
            }
        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <param name="parameter">A <see cref="T:Konnetic.Sip.SipParameter"/> parameter.</param>
        public void AddParameter(SipParameter parameter)
            {
            InternalAddGenericParameter(parameter);
            }
		/// <summary>
		/// Gets or sets the content encoding. 
		/// </summary>
		/// <remarks>
        /// An empty Accept-Encoding HeaderField is permissible, it is equivalent to Accept-Encoding of "identity", meaning no encoding is permissible.
		/// The special "*" symbol in an Accept-Encoding field matches any available content-coding not explicitly listed in the HeaderField.</remarks>
		/// <value>The content encoding.</value>
        /// <exception cref="T:System.ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="Encoding"/>.</exception>
        /// <exception cref="SipFormatException">Thrown when a user attempts to add non-token characters.<paramref name="Encoding"/>.</exception> 
        /// <threadsafety static="true" instance="false" />
        public string Encoding
        {
            get
                {
                return _coding;
                }
            set
            {
			PropertyVerifier.ThrowOnNullArgument(value, "Encoding");
            value = value.Trim();
            PropertyVerifier.ThrowOnInvalidToken(value, "Encoding");
            _coding = value;

            }
        }

        #endregion Properties

        #region Constructors


		/// <summary>
        /// Initializes a new instance of the <see cref="T:Konnetic.Sip.Headers.AcceptEncodingHeaderField"/> class.
		/// </summary>
		/// <remarks>The default constructor. <para/>
		/// <b>Default Initializations:</b>
		/// <list type="bullet">
        /// <item><see cref="P:Sip.Konnetic.Headers.AcceptEncodingHeaderField.Encoding" /> is set to "identity".</item>  
		/// </list> </remarks>
		/// <overloads>
		/// <summary>The method has one overload.</summary>
		/// <remarks>Overloads allow for initialising the encoding.</remarks>
		/// </overloads>
        public AcceptEncodingHeaderField()
            : base()
            {
            _coding = "identity";
            AllowMultiple = true;
            FieldName = AcceptEncodingHeaderField.LongName;
            CompactName = AcceptEncodingHeaderField.LongName; 
        }
        /// <summary><inheritdoc /></summary>		
		/// <remarks>
		/// An empty Accept-Encoding HeaderField is permissible, it is equivalent to Accept-Encoding of "identity", meaning no encoding is permissible. If no Accept-Encoding HeaderField is present, the server should assume a default value of identity.
        /// <para/>The special "*" symbol in an Accept-Encoding field matches any available content-coding not explicitly listed in the HeaderField.
        /// <para/>Content coding values indicate an encoding transformation that has been or can be applied to an entity. Content codings are primarily used to allow a document to be compressed or otherwise usefully transformed without losing the identity of its underlying media type and without loss of information. Frequently, the entity is stored in coded form, transmitted directly, and only decoded by the recipient.</remarks>
        /// <param name="encoding">The encoding.</param>
		/// <exception cref="T:System.ArgumentNullException">Thrown when the <paramref name="encoding"/>  parameter is null (<b>Nothing</b> in Visual Basic).</exception>
        public AcceptEncodingHeaderField(string encoding)
            : this()
        {
		PropertyVerifier.ThrowOnNullArgument(encoding, "coding");
		Encoding = encoding;
        }
 

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Performs an implicit conversion from <see cref="T:System.String"/> to <see cref="T:Konnetic.Sip.Headers.AcceptEncodingHeaderField"/>.
        /// </summary>
        /// <param name="value">The <see cref="T:System.String"/> value representing the HeaderField string.</param>
        /// <returns>A new <see cref="Konnetic.Sip.Headers.AcceptEncodingHeaderField"/> populated from the <paramref name="value"/> parameter.</returns>
        /// <threadsafety static="true" instance="false" />
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="value"/>.</exception>
        public static implicit operator AcceptEncodingHeaderField(String value)
            {        
            //M:Konnetic.Sip.Headers.AcceptEncodingHeaderField.op_Implicit(System.String)~Konnetic.Sip.Headers.AcceptEncodingHeaderField
            if(value==null)
                {
                return null;
                }

            AcceptEncodingHeaderField hf = new AcceptEncodingHeaderField();
            hf.Parse(value);
            return hf;
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="T:Konnetic.Sip.Headers.AcceptEncodingHeaderField"/> to <see cref="System.String"/>.
        /// </summary>
        /// <param name="headerField">The HeaderField to convert to a string.</param>
		/// <returns>A string representation of the HeaderField.</returns>
        /// <exception cref="T:System.ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="headerField"/> parameter.</exception>
        /// <threadsafety static="true" instance="false" /> 
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="headerField"/>.</exception>
        public static explicit operator string(AcceptEncodingHeaderField headerField)
        {
            PropertyVerifier.ThrowOnNullArgument(headerField, "headerField");
            return headerField.ToString();
        }

		/// <summary>
		/// Creates a deap  copy of this instance.
		/// </summary> 
		/// <remarks>Creates and returns a deep copy of the Header. This method ensures a deep copy of the HeaderField, when a message is cloned the HeaderField can be modified without effecting the original HeaderField in the message.</remarks>
        /// <returns>A deep copy of <see cref="T:Konnetic.Sip.Headers.AcceptEncodingHeaderField"/>.</returns>
        /// <threadsafety static="true" instance="false" /> 
        public override HeaderFieldBase Clone()
        {
        AcceptEncodingHeaderField newObj = new AcceptEncodingHeaderField(Encoding);
        CopyParametersTo(newObj); 
            return newObj;
        }


        /// <summary>
        /// Compare this SIP Header for equality with another <see cref="T:Konnetic.Sip.Headers.AcceptEncodingHeaderField"/> object.</summary>
        /// <remarks>
        /// All optional headers are compared using object equality that is each field in the header is used for comparision. When comparing HeaderFields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular HeaderField, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are case-sensitive. 
		/// </remarks>
		/// <param name="other">The <see cref="T:Konnetic.Sip.Headers.AcceptEncodingHeaderField"/> to compare to this instance.</param>
        /// <returns><c>true</c> if <paramref name="other" /> is an instance of this class representing the same SIP HeaderField as this, <c>false</c> otherwise.</returns>    
        /// <overloads>
        /// <summary>This method is overloaded.</summary>
        /// <remarks>Overloads allow for equality against <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>, <see cref="T:System.Object"/> and another instance of this HeaderField.</remarks>
        /// </overloads>  
        /// <threadsafety static="true" instance="false" />
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public bool Equals(AcceptEncodingHeaderField other)
        {
            if((object)other == null)
                {
                return false;
                }

            return base.Equals((QValueHeaderFieldBase)other) && Encoding.Equals(other.Encoding, StringComparison.OrdinalIgnoreCase);
        }
        
        /// <summary>Compare this SIP Header for equality with the base <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>.
        /// </summary>
        /// <remarks>This method overrides the <c>equals</c> method in <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>. 
        /// All optional headers are compared using object equality that is each field in the header is used for comparision. When comparing HeaderFields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular HeaderField, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are case-sensitive. 
        /// </remarks>
        /// <param name="other">The <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/> to compare to this instance.</param>
        /// <returns><c>true</c> if <paramref name="other" /> is an instance of this class representing the same SIP HeaderField as this, <c>false</c> otherwise.</returns>
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
        /// <inheritdoc cref="M:Konnetic.Sip.Headers.AcceptEncodingHeaderField.Equals(AcceptEncodingHeaderField)" select="overloads/*" />
        /// <inheritdoc cref="Equals(AcceptEncodingHeaderField)" select="threadsafety" />
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public override bool Equals(object obj)
        {
            if(obj == null)
                {
                return false;
                }

            AcceptEncodingHeaderField p1 = obj as AcceptEncodingHeaderField;
            if((object)p1 == null)
                {
                HeaderFieldGroup<AcceptEncodingHeaderField> p = obj as HeaderFieldGroup<AcceptEncodingHeaderField>;
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
            return base.IsValid() && !(string.IsNullOrEmpty(Encoding) && HeaderParameters.Count>0);
        }

        /// <summary>
        /// Parses string representation of the HeaderField.
        /// </summary>
        /// <remarks> 
        /// <inheritdoc cref="T:Konnetic.Sip.Headers.AcceptEncodingHeaderField" select="span[@id='Example 1']" />
        /// <inheritdoc cref="T:Konnetic.Sip.Headers.AcceptEncodingHeaderField" select="span[@id='Example 2']" /> 
		/// </remarks>
        /// <param name="value">The HeaderField string to parse.</param>
        /// <exception cref="T:Konnetic.Sip.SipParseException">Thrown when an invalid (non-standard) value is encountered during parsing.</exception>
        /// <exception cref="T:Konnetic.Sip.SipException">Thrown when a processing exception is encountered.</exception>
        /// <threadsafety static="true" instance="false" />
        public override void Parse(string value)
        {
		if(value != null)
			{
			RemoveFieldName(ref value, FieldName, CompactName);
			Encoding = string.Empty; 
				base.Parse(value);
				if(!string.IsNullOrEmpty(value))
					{

					Regex _codingRegex = new Regex(@"(?<=^\s*)[\w-.!%_*+`'~]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

					Match m = _codingRegex.Match(value);
					if(m != null)
						{
						if(!string.IsNullOrEmpty(m.Value))
							{
							try{
							Encoding = m.Value;
							}
							catch(SipException ex)
								{
                                throw new SipParseException("Encoding", SR.ParseExceptionMessage(value), ex);
								}
							catch(Exception ex)
                                {
                                throw new SipParseException(SR.GetString(SR.GeneralParseException, m.Value, "Encoding"), ex);    
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
            return Encoding;
        }


        #endregion Methods
    }
}