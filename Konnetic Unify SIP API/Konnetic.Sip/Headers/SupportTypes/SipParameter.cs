/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Text;

namespace Konnetic.Sip.Headers
{
    /// <summary>
    /// Represents a parameter name/value pair on a HeaderField. Responsible for storing and representing the parameter correctly. 
    /// </summary>
    /// <remarks> 
    /// <b>Standards: RFC3261, RFC2616</b> 
    /// <para/>
    /// Even though an arbitrary number of parameter pairs may be attached to a header field value, any given parameter-name must not appear more than once.
    /// <para/>
    /// Unless otherwise stated in the definition of a particular header field, parameter names, and parameter values are case-insensitive.
    /// <para/> 
    /// <b>RFC 3261 Syntax:</b> 
    /// <table> 
    /// <tr><td style="border-bottom:none">parameter = </td><td style="border-bottom:none">attribute "=" value</td></tr>
    /// <tr><td style="border-bottom:none">attribute = </td><td style="border-bottom:none">token</td></tr>
    /// <tr><td style="border-bottom:none">value = </td><td style="border-bottom:none">token | quoted-string</td></tr>
    /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" ) </td></tr>
    /// <tr><td style="border-bottom:none">quoted-string = </td><td style="border-bottom:none">SWS DOUBLE_QUOTE *(qdtext / quoted-pair ) DOUBLE_QUOTE </td></tr>
    /// <tr><td style="border-bottom:none">qdtext = </td><td style="border-bottom:none">LWS / %x21 / %x23-5B / %x5D-7E / UTF8-NONASCII </td></tr>
    /// <tr><td style="border-bottom:none">LWS = </td><td style="border-bottom:none">[*WSP CRLF] 1*WSP ; linear whitespace </td></tr>
    /// <tr><td style="border-bottom:none">SWS = </td><td style="border-bottom:none">[LWS] ; sep whitespace </td></tr>
    /// <tr><td style="border-bottom:none">host = </td><td style="border-bottom:none">hostname / IPv4address / IPv6reference </td></tr>
    /// </table> 
    /// <para/> 
    /// <example>
    /// <list type="bullet">
    /// <item>Contact: &lt;sip:alice@atlanta.com&gt;;expires=3600</item>
    /// <item>CONTACT: &lt;sip:alice@atlanta.com&gt;;ExPiReS=3600</item>
    /// <item>Contact: "Mr. Watson" &lt;sip:watson@worcester.bell-telephone.com&gt;;q=0.7; expires=3600</item> 
    /// </list>
    /// </example> 
    /// </remarks>
    public class SipParameter : IEquatable<SipParameter>
    {
        #region Fields

        /// <summary>
        /// Indicates that the parameter is case sensitive for comparisons
        /// </summary>
        private bool _caseSensitiveComparison;
        private bool _isQuoted;

        /// <summary>
        /// Indicates that the parameter deliberately has no value
        /// </summary>
        private bool _valueLessParameter;

        /// <summary>
        /// The parameter name.
        /// </summary>
        private string _name;

        /// <summary>
        /// The parameter value.
        /// </summary>
        private string _value;

        private bool _isMediaParameter = false;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance is a media parameter.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is a media parameter; otherwise, <c>false</c>.
        /// </value>
        public bool IsMediaParameter
            {
            get { return _isMediaParameter; }
            set { _isMediaParameter = value; }
            }
        /// <summary>
        /// Gets or sets a value indicating whether the Parameter uses case sensitive comparison.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if case sensitive comparison is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool CaseSensitiveComparison
        {
            get { return _caseSensitiveComparison; }
            set { _caseSensitiveComparison = value; }
        }

        /// <summary>
        /// Gets a value indicating whether this collection is empty. 
        /// </summary>
        /// <value><c>true</c> if this collection is empty; otherwise, <c>false</c>.</value>
        public bool IsEmpty
        {
            get { return String.IsNullOrEmpty(_name); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Parameter value is quoted.
        /// </summary>
        /// <value><c>true</c> if the Parameter value is quoted; otherwise, <c>false</c>.</value>
        public bool IsQuoted
        {
            get { return _isQuoted; }
            set { _isQuoted = value; }
        }

        /// <summary>
        /// Gets or sets the parameter name.
        /// </summary>
        /// <value>The parameter name.</value>
        /// <exception cref="SipFormatException">Thrown when the name value contains illegal characters.</exception>
        public string Name
        {
            get { return _name; }
            set
            {
            if(value != null)
                {
                value = value.Trim();
                if(!Syntax.IsUnReservedAllParameter(value))
                    {
                    throw new SipFormatException(SR.GetString(SR.IllegalParameterNameException));
                    }
                if(_name != value) { 
                    _name = value;
                    String.Intern(value); //Optomise access to string in future.
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the parameter value.
        /// </summary>
        /// <remarks>The value is quoted if it contains reserved characters. Quoted values employ case-sensitive comparison.</remarks>
        /// <value>The parameter value.</value>
        public string Value
        {
            get { return ValuelessParameter ?  string.Empty :  _value; }
        set
            { 
            if(value != null)
                {
                value = value.Trim();
                if(_value != value)
                    {
                    if(IsMediaParameter)
                        {
                        if(value.Length > 0 && !IsQuoted && !(Syntax.IsToken(value)))
                            {
                            if(Syntax.IsQuotedString(value))
                                {
                                value = Syntax.UnQuotedString(value);
                                }
                            value = Syntax.ConvertToQuotedString(value);
                            CaseSensitiveComparison = true;
                            IsQuoted = true;
                            }
                        }
                    else
                        {
                        if(value.Length > 0 && !IsQuoted && !(Syntax.IsToken(value) || Syntax.IsUnReservedHost(value)))
                            {
                            if(Syntax.IsQuotedString(value))
                                {
                                value = Syntax.UnQuotedString(value);
                                }
                            value = Syntax.ConvertToQuotedString(value);
                            CaseSensitiveComparison = true;
                            IsQuoted = true;
                            }
                        }

                    _value = value;
                    }
                if(!string.IsNullOrEmpty(value))
                    {
                    ValuelessParameter = false;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the parameter deliberately has no value.
        /// </summary>
        /// <value><c>true</c> if the parameter has no value; otherwise, <c>false</c>.</value>
        public bool ValuelessParameter
        {
            get { return _valueLessParameter; }
            set { _valueLessParameter = value; }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Konnetic.Sip.Headers.SipParameter"/> class.
        /// </summary>
        public SipParameter()
        {
            CaseSensitiveComparison = false;
            ValuelessParameter = false;
            _value = String.Empty;
            _name = String.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Konnetic.Sip.Headers.SipParameter"/> class.
        /// </summary>
        /// <param name="nameValuePair">The name value pair to parse.</param>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="nameValuePair"/> parameter.</exception>
        public SipParameter(string nameValuePair)
            : this()
        {
        PropertyVerifier.ThrowOnNullArgument(nameValuePair, "nameValuePair");
        Parse(nameValuePair);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Konnetic.Sip.Headers.SipParameter"/> class.
        /// </summary>
        /// <param name="name">The SipParameter name.</param>
        /// <param name="value">The SipParameter value.</param>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="name"/> parameter.</exception>
        public SipParameter(string name, string value)
            : this(name,value,false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Konnetic.Sip.Headers.SipParameter"/> class.
        /// </summary>
        /// <remarks>A null <paramref name="value"/> parameter indicates a valueless parameter</remarks>
        /// <param name="name">The parameter name.</param>
        /// <param name="value">The parameter value.</param>
        /// <param name="isQuoted"> if set to <c>true</c> the parameter value is quoted.</param>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="name"/> parameter.</exception>
        public SipParameter(string name, string value, bool isQuoted)
            : this()
        {
            PropertyVerifier.ThrowOnNullOrEmptyString(name, "name"); 
            IsQuoted = isQuoted;
            Name = name;
            if(string.IsNullOrEmpty(value))
                {
                ValuelessParameter = true;
                }
            else
                {
                Value = value;
                }
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.String"/> to <see cref="Konnetic.Sip.Headers.SipParameter"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        //public static implicit operator SipParameter(string value)
        //{
        //    ArgumentVerification.ThrowOnNullArgument(value, "value");

        //    return new SipParameter(value);
        //}

        /// <summary>
        /// Performs an explicit conversion from <see cref="T:Konnetic.Sip.Headers.SipParameter"/> to <see cref="System.String"/>.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>The result of the conversion.</returns>
        //public static explicit operator string(SipParameter parameter)
        //{
        //    ArgumentVerification.ThrowOnNullArgument(parameter, "parameter");
        //    return parameter.ToString();
        //}

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="header1">The first <see cref="SipParameter"/>HeaderParameter instance.</param>
        /// <param name="header2">The second <see cref="SipParameter"/>HeaderParameter instance.</param>
        /// <returns>The result of the inequality operation: <c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
        /// <threadsafety static="true" instance="false" /> 
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public static bool operator !=(SipParameter header1, SipParameter header2)
        {
            return !(header1 == header2);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="param1">The first <see cref="T:Konnetic.Sip.Headers.SipParameter"/>HeaderParameter instance.</param>
        /// <param name="param2">The second <see cref="T:Konnetic.Sip.Headers.SipParameter"/>HeaderParameter instance.</param>
        /// <returns>The result of the equality operation:<c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
        /// <threadsafety static="true" instance="false" /> 
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public static bool operator ==(SipParameter param1, SipParameter param2)
        {
            if(System.Object.ReferenceEquals(param1, param2))
            {
            return true;
            }
            if(((object)param1 == null) || ((object)param1 == null))
            {
            return false;
            }

            return param1.Equals(param2);
        }
        /// <summary>
        /// Creates a deep copy of this instance.
        /// </summary> 
        /// <remarks>Creates and returns a deep copy of the Header. This method ensures a deep copy of the HeaderField, when a message is cloned the HeaderField can be modified without effecting the original HeaderField in the message.</remarks>
        /// <returns>A deep copy of <see cref="T:Konnetic.Sip.Headers.SipParameter"/>.</returns>
        /// <threadsafety static="true" instance="false" />
        public SipParameter Clone()
        {
            return new SipParameter(this.Name, this.Value);
        }

        /// <summary>
        /// Compares the specified <see cref="T:Konnetic.Sip.Headers.SipParameter"/> param to this instance.
        /// </summary>
        /// <param name="other">A <see cref="T:Konnetic.Sip.Headers.SipParameter"/> instance to compare with this instance.</param>
        /// <returns>The result of the equality operation:<c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
        /// <threadsafety static="true" instance="false" /> 
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public bool Equals(SipParameter other)
        {
            // If parameter is null return false:
            if((object)other == null)
            {
            return false;
            }

            if(CaseSensitiveComparison)
                {
                // Return true if the fields match:
                return _name.Equals(other._name, StringComparison.OrdinalIgnoreCase) && _value.Equals(other._value, StringComparison.Ordinal);
                }
            else
                {
                // Return true if the fields match:
                return _name.Equals(other._name, StringComparison.OrdinalIgnoreCase) && _value.Equals(other._value, StringComparison.OrdinalIgnoreCase);
                }
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// 	<c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">
        /// The <paramref name="obj"/>  parameter is null (<b>Nothing</b> in Visual Basic).
        /// </exception>
        /// <threadsafety static="true" instance="false" /> 
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            if(obj == null)
            {
            return false;
            }
            // If parameter cannot be cast to HeaderParameter return false:
            SipParameter p = obj as SipParameter;
            if((object)p == null)
            {
            return false;
            }

            // Return true if the fields match:
            return  this.Equals(p);
        }

        /// <summary>
        /// Validates this instance against the standard. Indicated whether it reaches minimum compliance.
        /// </summary> 
        /// <returns>
        /// 	<c>true</c> if instance represents a valid SIP Parameter; otherwise, <c>false</c>.
        /// </returns>
        /// <threadsafety static="true" instance="false" />
        public bool IsValid()
        {
            return (!ValuelessParameter && String.IsNullOrEmpty(Value) && string.IsNullOrEmpty(_name)) || !string.IsNullOrEmpty(_name); //Support valueless parameters e.g. 'lr'
        }

        /// <summary>
        /// Parses string representation of the SipParameter.
        /// </summary>
        /// <remarks>  
        /// <b>RFC 3261 Syntax:</b> 
        /// <table> 
        /// <tr><td style="border-bottom:none">parameter = </td><td style="border-bottom:none">attribute "=" value</td></tr>
        /// <tr><td style="border-bottom:none">attribute = </td><td style="border-bottom:none">token</td></tr>
        /// <tr><td style="border-bottom:none">value = </td><td style="border-bottom:none">token | quoted-string</td></tr>
        /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" ) </td></tr>
        /// <tr><td style="border-bottom:none">quoted-string = </td><td style="border-bottom:none">SWS DOUBLE_QUOTE *(qdtext / quoted-pair ) DOUBLE_QUOTE </td></tr>
        /// <tr><td style="border-bottom:none">qdtext = </td><td style="border-bottom:none">LWS / %x21 / %x23-5B / %x5D-7E / UTF8-NONASCII </td></tr>
        /// <tr><td style="border-bottom:none">LWS = </td><td style="border-bottom:none">[*WSP CRLF] 1*WSP ; linear whitespace </td></tr>
        /// <tr><td style="border-bottom:none">SWS = </td><td style="border-bottom:none">[LWS] ; sep whitespace </td></tr>
        /// <tr><td style="border-bottom:none">host = </td><td style="border-bottom:none">hostname / IPv4address / IPv6reference </td></tr>
        /// </table> 
        /// <para/> 
        /// <example>
        /// <list type="bullet">
        /// <item>Contact: &lt;sip:alice@atlanta.com&gt;;expires=3600</item>
        /// <item>CONTACT: &lt;sip:alice@atlanta.com&gt;;ExPiReS=3600</item>
        /// <item>Contact: "Mr. Watson" &lt;sip:watson@worcester.bell-telephone.com&gt;;q=0.7; expires=3600</item> 
        /// </list>
        /// </example> 
        /// </remarks>
        /// <param name="value">The SipParameter string to parse.</param>
        /// <exception cref="T:Konnetic.Sip.SipParseException">Thrown when an invalid (non-standard) value is encountered.</exception>
        /// <exception cref="T:Konnetic.Sip.SipException">Thrown when a processing exception is encountered.</exception>
        /// <threadsafety static="true" instance="false" />
        public void Parse(string value)
        {
            if(String.IsNullOrEmpty(value))
                {
                throw new ArgumentNullException("nameValuePair");
                }

            string[] newParam = value.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);

            if(newParam.Length == 1)
                {
                Name = newParam[0];
                ValuelessParameter = true;
                }
            else if(newParam.Length == 2)
                {
                Name = newParam[0];
                Value = newParam[1];
                if(Syntax.IsQuotedString(Value))
                    {
                    CaseSensitiveComparison = true;
                    }
                }
            else
                {
                throw new ArgumentException("Invalid argument format");
                }
        }

        /// <summary>
        /// Represents the parameter as a byte array. Encoding is UTF8.
        /// </summary>
        /// <returns>A byte array that represents this instance.</returns>
        public byte[] ToBytes()
        {
            return System.Text.UTF8Encoding.UTF8.GetBytes(ToChars());
        }

        /// <summary>
        /// Returns the parameter as a char array.
        /// </summary>
        /// <returns>A char array that represents this instance.</returns>
        public char[] ToChars()
        {
            return ToString().ToCharArray();
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance. Supports Valueless parameters such as 'lr'.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        /// <value>Uses the format [parameter-name]=[parameter-value]</value>
        /// <specification>RFC3261: 7.3.1</specification>
        public override string ToString()
        {
            if(!IsValid())
            {
            return string.Empty;
            }
            else
            {
                StringBuilder sb = new StringBuilder(40);
                sb.Append(Name);
                if(!ValuelessParameter)
                    {
                    sb.Append(SR.ParameterSeperator);
                    sb.Append(Value);
                    }
                return sb.ToString();
            }
        }

        #endregion Methods
    }
}