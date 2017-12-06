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
    /// <summary> A CSeq HeaderField in a request contains a single decimal sequence number and the request method.
    /// </summary>
    /// <remarks> 
    /// <b>Standards: RFC3261</b>
    /// <para/>
    /// The sequence number must be expressible as a 32-bit unsigned integer. The method part of CSeq is case-sensitive. The CSeq HeaderField serves to order transactions within a dialog, to provide a means to uniquely identify transactions, and to differentiate between new requests and request retransmissions. Two CSeq HeaderFields are considered equal if the sequence number and the request method are identical.
    /// <para/>
    /// <b>RFC 3261 Syntax:</b> 
    /// <table >
    /// <tr><td colspan="2" style="border-bottom:none">"CSeq" ":" 1*DIGIT WHITESPACE Method</td></tr> 
    /// <tr><td style="border-bottom:none">Method = </td><td style="border-bottom:none">INVITE / ACK / OPTIONS / BYE / CANCEL / REGISTER / token</td></tr> 
    /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr> 
    /// </table>   
    /// <para/>
    /// <note type="implementnotes">This HeaderField does not allow HeaderField grouping.</note> 
    /// <example>
    /// <list type="bullet">
    /// <item>CSeq: 4711 INVITE</item>  
    /// </list> 
    /// </example>
    /// </remarks> 
    /// <seealso cref="T:Konnetic.Sip.Headers.MediaTypeHeaderFieldBase"/>
    public sealed class CSeqHeaderField : HeaderFieldBase
    {
        #region Fields

        internal const string CompareName = "CSEQ";
        internal const string CompareShortName = CompareName;

        /// <summary>
        /// The long form of the name.
        /// </summary>
        internal const string LongName = "CSeq";

        /// <summary>
        /// 
        /// </summary>
        private Int64? _sequence;

        /// <summary>
        /// 
        /// </summary>
        private SipMethod _method;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the max sequence.
        /// </summary>
        /// <value>The max sequence.</value>
        public static Int64 MaxSequence
        {
            get{return 4294967295;}
        }

        /// <summary>
        /// Gets the min sequence.
        /// </summary>
        /// <value>The min sequence.</value>
        public static Int64 MinSequence
        {
            get { return 0; }
        }

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        /// <value>The method.</value>
        public SipMethod Method
        {
            get { return _method; }
            set
            {
            _method = value;
            }
        }

        /// <summary>
        /// Gets or sets the sequence.
        /// </summary>
        /// <value>The sequence.</value>
        public Int64? Sequence
        {
            get { return _sequence; }
        set
            {
            if(value.HasValue)
                {
                PropertyVerifier.ThrowIfIntOutOfRange(value, MinSequence, MaxSequence, "CSeq");
                } 
                _sequence = value; 
            }
        }

        #endregion Properties

        #region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="CSeqHeaderField"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <remarks>Defaults: 
		/// <list type="bullet">
		/// <item><c>Sequence</c> is set to a new valid sequence number.</item> 
		/// </list> </remarks>
		/// <overloads>
		/// <summary>The method has two overloads.</summary>
		/// </overloads>
        public CSeqHeaderField()
            : base()
        {
            AllowMultiple = false;
            Method = SipMethod.Empty;
			Sequence = CSeqHeaderField.NewSequence();
            FieldName = CSeqHeaderField.LongName;
            CompactName = CSeqHeaderField.LongName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CSeqHeaderField"/> class.
        /// </summary>
        /// <param name="cseq">The cseq.</param>
        public CSeqHeaderField(Int64? cseq)
            : this()
        {
            Sequence = cseq;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="CSeqHeaderField"/> class.
        /// </summary>
        /// <param name="cseq">The cseq.</param>
        /// <param name="method">The method.</param>
        public CSeqHeaderField(Int64? cseq, SipMethod method)
            : this(cseq)
        {
            Method = method;
        }

        #endregion Constructors

        #region Methods

		/// <summary>
		/// Performs an implicit conversion from <see cref="System.String"/> to <see cref="Konnetic.Sip.Headers.CSeqHeaderField"/>.
		/// </summary>
		/// <param name="value">The string value representing the HeaderField.</param>
        /// <returns>A new <see cref="Konnetic.Sip.Headers.CSeqHeaderField"/> populated from the <paramref name="value"/> parameter.</returns>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="value"/>.</exception>
        public static implicit operator CSeqHeaderField(String value)
        {
            PropertyVerifier.ThrowOnNullArgument(value,"value");

            CSeqHeaderField hf = new CSeqHeaderField();
            hf.Parse(value);
            return hf;
        }

		/// <summary>
		/// Performs an explicit conversion from <see cref="Konnetic.Sip.Headers.CSeqHeaderField"/> to <see cref="System.String"/>.
		/// </summary>
		/// <param name="headerField">The HeaderField to convert.</param>
		/// <returns>A string representation of the HeaderField.</returns>
		/// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="headerField"/>.</exception>
        public static explicit operator string(CSeqHeaderField headerField)
        {
            PropertyVerifier.ThrowOnNullArgument(headerField, "headerField");
            return headerField.ToString();
        }

        /// <summary>
        /// News the sequence.
        /// </summary>
        /// <returns></returns>
        public static Int64 NewSequence()
        {
            int i = SipGuid.NewIntSipGuid();
            return Math.Abs(i);
        }

        /// <summary>
        /// Creates a deap  copy of this instance.
        /// </summary> 
        /// <remarks>Creates and returns a deep copy of the Header. This method ensures a deep copy of the HeaderField, when a message is cloned the HeaderField can be modified without effecting the original HeaderField in the message.</remarks>
        /// <returns>A deep copy of <see cref="T:Konnetic.Sip.Headers.CSeqHeaderField"/>.</returns>
        /// <threadsafety static="true" instance="false" /> 
public override HeaderFieldBase Clone()
        {
            CSeqHeaderField newCSeq = new CSeqHeaderField(Sequence, Method);
            return newCSeq;
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

        /// <summary>
        /// Compare this SIP Header for equality with another <see cref="T:Konnetic.Sip.Headers.CSeqHeaderField"/> object.</summary>
        /// <remarks>
        /// All optional headers are compared using object equality that is each field in the header is used for comparision. When comparing HeaderFields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular HeaderField, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are case-sensitive. 
        /// </remarks>
        /// <param name="other">The <see cref="T:Konnetic.Sip.Headers.CSeqHeaderField"/> to compare to this instance.</param>
        /// <returns><c>true</c> if <paramref name="other" /> is an instance of this class representing the same SIP HeaderField as this, <c>false</c> otherwise.</returns>    
        /// <overloads>
        /// <summary>This method is overloaded.</summary>
        /// <remarks>Overloads allow for equality against <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>, <see cref="T:System.Object"/> and another instance of this HeaderField.</remarks>
        /// </overloads>  
        /// <threadsafety static="true" instance="false" />
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public bool Equals(CSeqHeaderField other)
        {
            if((object)other == null)
                {
                return false;
                }

            return base.Equals((HeaderFieldBase)other) && Method.Equals(other.Method) && Sequence.Equals(other.Sequence) ;
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

            CSeqHeaderField p1 = obj as CSeqHeaderField;
            if((object)p1 == null)
                {
                HeaderFieldGroup<CSeqHeaderField> p = obj as HeaderFieldGroup<CSeqHeaderField>;
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
            return base.IsValid() && Sequence > 0 && !string.IsNullOrEmpty(Method);
        }

        /// <summary>
        /// Nexts this instance.
        /// </summary>
        public void Next()
        {
            if(Sequence >= MaxSequence)
                {
                Sequence = 0;
                }
            unchecked { Sequence++; }
        }

        /// <summary>
        /// Parses string representation of the HeaderField.
        /// </summary>
        /// <remarks>  
        /// <b>RFC 3261 Syntax:</b> 
        /// <table >
        /// <tr><td colspan="2" style="border-bottom:none">"CSeq" ":" 1*DIGIT WHITESPACE Method</td></tr> 
        /// <tr><td style="border-bottom:none">Method = </td><td style="border-bottom:none">INVITE / ACK / OPTIONS / BYE / CANCEL / REGISTER / token</td></tr> 
        /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr> 
        /// </table>   
        /// <para/>
        /// <note type="implementnotes">This HeaderField does not allow HeaderField grouping.</note> 
        /// <example>
        /// <list type="bullet">
        /// <item>CSeq: 4711 INVITE</item>  
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
                Sequence = null;
                Method = SipMethod.Empty;
                if(!string.IsNullOrEmpty(value))
                    {
                    Regex _sequenceRegex = new Regex(@"(?<=^\s*)[0-9]+(?=\s*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    Regex _sequenceReplaceRegex = new Regex(@"^\s*[0-9]+\s*", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    Regex _methodRegex = new Regex(@"(?<=^\s*[0-9]*\s*)[\w-.!%_*+`'~]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    Match m = _sequenceRegex.Match(value);
                    if(m != null)
                        {
                        try
                            {
                            if(!string.IsNullOrEmpty(m.Value))
                                {
                                long nVal = long.Parse(m.Value,CultureInfo.InvariantCulture);
                                Sequence = nVal;
                                }
                            }
						catch(SipException ex)
							{
                            throw new SipParseException("Sequence", SR.ParseExceptionMessage(value), ex);
							}
                        catch(FormatException ex)
							{
                            throw new SipParseException("Sequence", SR.ParseExceptionMessage(value), ex);
                            }
                        catch(OverflowException ex)
                            {
                            throw new SipParseException(SR.GetString(SR.OverflowException, m.Value, "Sequence"), ex);
							}
						catch(Exception ex)
                            {
                            throw new SipParseException(SR.GetString(SR.GeneralParseException, m.Value, "Sequence"), ex); 
							}
                        }
                    value = _sequenceReplaceRegex.Replace(value, "");
                    m = _methodRegex.Match(value);
                    if(m != null)
                        {
                        if(!string.IsNullOrEmpty(m.Value))
                            {
                            Method = new SipMethod(value);
                            }
                        }
                    }
                }
        }

        /// <summary>
        /// Recreates the sequence.
        /// </summary>
        public void RecreateSequence()
        {
            Sequence = CSeqHeaderField.NewSequence();
        }

        /// <summary>
        /// Gets a string representation of the HeaderField value. The HeaderField name is excluded.
        /// </summary>
        /// <remarks>This method overrides the <c>GetStringValue</c> method in <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>. </remarks>
        /// <returns>String representation of the HeaderField value</returns>
        /// <threadsafety static="true" instance="false" />
		public override string GetStringValue()
			{
            string s = string.Empty;
            if(Sequence.HasValue)
                {
                s += ((long)Sequence).ToString(System.Globalization.CultureInfo.InvariantCulture);
                } 
			if(!string.IsNullOrEmpty(Method))
				{
                s += SR.SingleWhiteSpace + Method; 
				}  
            return s;
			}

        #endregion Methods
    }
}