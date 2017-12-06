/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Konnetic.Sip.Headers
{
    /// <summary>Indicates what version of the MIME protocol was used to construct the message.
    /// </summary>
    /// <remarks> 
    /// <b>Standards: RFC3261, RFC2616, RFC2045</b>
    /// <para/>
    /// SIP is not a MIME-compliant protocol. However, SIP messages may include a single MIME-Version general-HeaderField to indicate what version of the MIME protocol was used to construct the message. Use of the MIME-Version HeaderField indicates that the message is in full compliance with the MIME protocol (as defined in RFC 2045).
    /// <para/>
    /// MIME version "1.0" is the default.
    /// <para/>
    /// <note type="implementnotes">SIP message parsing and semantics are defined by RFC3261 and not the MIME specification.</note> 
    /// <para/>
    /// <b>RFC 3261 Syntax:</b> 
    /// <table >
    /// <tr><td colspan="2" style="border-bottom:none">"MIME-Version" ":" 1*DIGIT "." 1*DIGIT</td></tr>  
    /// </table> 
    /// <para/>
    /// <note type="implementnotes">This HeaderField does not allow HeaderField grouping.</note> 
    /// <example>
    /// <list type="bullet">
    /// <item>MIME-Version: 1.0</item>  
    /// </list> 
    /// </example>
    /// </remarks> 
    /// <seealso cref="T:Konnetic.Sip.Methods.Options"/>
    public sealed class MimeVersionHeaderField : HeaderFieldBase
    {
        #region Fields

        internal const string CompareName = "MIME-VERSION";
        internal const string CompareShortName = CompareName;

        /// <summary>
        /// The long form of the name.
        /// </summary>
        internal const string LongName = "MIME-Version";

        private byte? _majorVersion;
        private byte? _minorVersion;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the major version.
        /// </summary>
        /// <value>The major version.</value>
        public byte? MajorVersion
        {
            get { return _majorVersion; }
              set
			{ 
              _majorVersion = value;
              }
        }

        /// <summary>
        /// Gets or sets the minor version.
        /// </summary>
        /// <value>The minor version.</value>
        public byte? MinorVersion
        {
            get { return _minorVersion; }
            set
			{ 
                _minorVersion = value;
                }
        }

        #endregion Properties

        #region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="MimeVersionHeaderField"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <remarks>Defaults: 
		/// <list type="bullet">
		/// <item><c>MajorVersion</c> is set to 1 (one).</item>
		/// <item><c>MinorVersion</c> is set to 0 (zero).</item>
		/// </list> </remarks>
		/// <overloads>
		/// <summary>The method has one overload.</summary>
		/// </overloads>
        public MimeVersionHeaderField()
			: this(1, 0)
			{  
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MimeVersionHeaderField"/> class.
        /// </summary>
        /// <param name="majorVersion">The major version.</param>
        /// <param name="minorVersion">The minor version.</param>
        public MimeVersionHeaderField(byte? majorVersion, byte? minorVersion)
            : base()
        {
            MajorVersion = majorVersion;
            MinorVersion = minorVersion;
            Init();
        }

        #endregion Constructors

        #region Methods
		/// <summary>
		/// Performs an implicit conversion from <see cref="System.String"/> to <see cref="Konnetic.Sip.Headers.MimeVersionHeaderField"/>.
		/// </summary>
		/// <param name="value">The string value representing the HeaderField.</param>
        /// <returns>A new <see cref="Konnetic.Sip.Headers.MimeVersionHeaderField"/> populated from the <paramref name="value"/> parameter.</returns>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="value"/>.</exception>
        public static implicit operator MimeVersionHeaderField(String value)
        {
            PropertyVerifier.ThrowOnNullArgument(value,"value");

            MimeVersionHeaderField hf = new MimeVersionHeaderField();
            hf.Parse(value);
            return hf;
        }
		/// <summary>
		/// Performs an explicit conversion from <see cref="Konnetic.Sip.Headers.MimeVersionHeaderField"/> to <see cref="System.String"/>.
		/// </summary>
		/// <param name="headerField">The HeaderField to convert.</param>
		/// <returns>A string representation of the HeaderField.</returns>
		/// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="headerField"/>.</exception>
        public static explicit operator string(MimeVersionHeaderField headerField)
        {
            PropertyVerifier.ThrowOnNullArgument(headerField, "headerField");
            return headerField.ToString();
        }

        /// <summary>
        /// Creates a deap  copy of this instance.
        /// </summary> 
        /// <remarks>Creates and returns a deep copy of the Header. This method ensures a deep copy of the HeaderField, when a message is cloned the HeaderField can be modified without effecting the original HeaderField in the message.</remarks>
        /// <returns>A deep copy of <see cref="T:Konnetic.Sip.Headers.MimeVersionHeaderField"/>.</returns>
        /// <threadsafety static="true" instance="false" /> 
public override HeaderFieldBase Clone()
        {
            MimeVersionHeaderField newObj = new MimeVersionHeaderField(MajorVersion, MinorVersion);
            return newObj;
        }
        ///<summary>
/// Compare this SIP Header for equality with another <see cref="T:Konnetic.Sip.Headers.MimeVersionHeaderField"/> object.</summary>
/// <remarks>
/// All optional headers are compared using object equality that is each field in the header is used for comparision. When comparing HeaderFields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular HeaderField, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are case-sensitive. 
/// </remarks>
/// <param name="other">The <see cref="T:Konnetic.Sip.Headers.MimeVersionHeaderField"/> to compare to this instance.</param>
/// <returns><c>true</c> if <paramref name="other" /> is an instance of this class representing the same SIP HeaderField as this, <c>false</c> otherwise.</returns>    
/// <overloads>
/// <summary>This method is overloaded.</summary>
/// <remarks>Overloads allow for equality against <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>, <see cref="T:System.Object"/> and another instance of this HeaderField.</remarks>
/// </overloads>  
/// <threadsafety static="true" instance="false" />
[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
public bool Equals(MimeVersionHeaderField other)
    {
    if((object)other == null)
        {
        return false;
        }

    return base.Equals((HeaderFieldBase)other) && MajorVersion.Equals(other.MajorVersion) && MinorVersion.Equals(other.MinorVersion);
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

            MimeVersionHeaderField p1 = obj as MimeVersionHeaderField;
            if((object)p1 == null)
                {
                HeaderFieldGroup<MimeVersionHeaderField> p = obj as HeaderFieldGroup<MimeVersionHeaderField>;
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
        /// <tr><td colspan="2" style="border-bottom:none">"MIME-Version" ":" 1*DIGIT "." 1*DIGIT</td></tr>  
        /// </table> 
        /// <para/>
        /// <note type="implementnotes">This HeaderField does not allow HeaderField grouping.</note> 
        /// <example>
        /// <list type="bullet">
        /// <item>MIME-Version: 1.0</item>  
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
                MajorVersion = null;
                MinorVersion = null;
                if(!string.IsNullOrEmpty(value))
                    {
                    Regex _major = new Regex(@"(?<=^\s*)[0-9]+", RegexOptions.Compiled | RegexOptions.IgnoreCase );
                    Regex _minor = new Regex(@"(?<=^\s*[0-9]+\.)[0-9]+", RegexOptions.Compiled | RegexOptions.IgnoreCase );

                    Match m = _major.Match(value);
                    if(m != null)
                        {
                        try
                            {
                            if(!string.IsNullOrEmpty(m.Value))
                                {
                                    byte nVal = byte.Parse(m.Value, CultureInfo.InvariantCulture);
                                    MajorVersion = nVal; 
                                }
                            }
							catch(SipException ex)
								{
								throw new SipParseException("MajorMIMEVersion", SR.ParseExceptionMessage(value), ex);
								}
						catch(FormatException ex)
                            {
                            throw new SipParseException(SR.GetString(SR.ByteConvertException, m.Value, "MajorVersion"), ex);
							}
						catch(OverflowException ex)
                            {
                            throw new SipParseException(SR.GetString(SR.OverflowException, m.Value, "MajorVersion"), ex);
							}
							catch(Exception ex)
                            {
                            throw new SipParseException(SR.GetString(SR.GeneralParseException, m.Value, "MajorVersion"), ex);  
								}
                        }
                    m = _minor.Match(value);
                    if(m != null)
                        {
                        try
                            {
                            if(!string.IsNullOrEmpty(m.Value))
                                {
                                byte nVal = byte.Parse(m.Value, CultureInfo.InvariantCulture);
                                MinorVersion = nVal;
                                }
                            }
                        catch(FormatException ex)
                            {
                            throw new SipParseException(SR.GetString(SR.ByteConvertException, m.Value, "MinorVersion"), ex);
                            }
                        catch(OverflowException ex)
                            {
                            throw new SipParseException(SR.GetString(SR.OverflowException, m.Value, "MinorVersion"), ex); 
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
        string s = string.Empty;
        if(MajorVersion.HasValue)
            {
            s += ((byte)MajorVersion).ToString(System.Globalization.CultureInfo.InvariantCulture);
            }
        if(MinorVersion.HasValue)
            {
            s += SR.VersionSeperator + ((byte)MinorVersion).ToString(System.Globalization.CultureInfo.InvariantCulture);
            }
            return s;
        }

        private void Init()
        {
            AllowMultiple = false;
            CompactName = MimeVersionHeaderField.LongName;
            FieldName = MimeVersionHeaderField.LongName;
        }

        #endregion Methods
    }
}