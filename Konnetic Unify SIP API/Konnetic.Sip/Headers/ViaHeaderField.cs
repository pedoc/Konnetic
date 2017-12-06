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
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Konnetic.Sip.Headers
{

    /// <summary>The Via HeaderField indicates the path taken by the request so far and indicates the path that should be followed in routing responses.
    /// </summary>
    /// <remarks>
    /// <b>Standards: RFC3261, RFC2616, RFC2543</b>
    /// <para/>
    /// The branch ID parameter in the Via HeaderField values serves as a transaction identifier, and is used by proxies to detect loops. A Via HeaderField value contains the transport protocol used to send the message, the client's host name or network address, and possibly the port number at which it wishes to receive responses. A Via HeaderField value can also contain parameters such as maddr, ttl, received, and branch, whose meaning and use are described in other sections. For implementations compliant to the SIP specification, the value of the branch parameter must start with the magic cookie "z9hG4bK".
    /// <para/>
    /// Transport protocols defined here are UDP, TCP, TLS, and SCTP. TLS means TLS over TCP. When a request is sent to a SIPS URI, the protocol still indicates "SIP", and the transport protocol is TLS.
    /// <para/>
    /// The SIP specification mandates that the branch parameter be present in all requests. 
    /// <para/>
    /// <b>RFC 3261 Syntax:</b> 
    /// <table >
    /// <tr><td colspan="2" style="border-bottom:none">( "Via" / "v" ) ":" via-parm *("," via-parm)</td></tr> 
    /// <tr><td style="border-bottom:none">via-parm = </td><td style="border-bottom:none">sent-protocol WHITESPACE sent-by *( SEMI via-params )</td></tr> 
    /// <tr><td style="border-bottom:none">via-params = </td><td style="border-bottom:none">via-ttl / via-maddr / via-received / via-branch / via-extension</td></tr> 
    /// <tr><td style="border-bottom:none">via-ttl = </td><td style="border-bottom:none">"ttl" EQUAL 1*3DIGIT ; 0 to 255</td></tr> 
    /// <tr><td style="border-bottom:none">via-maddr = </td><td style="border-bottom:none">"maddr" EQUAL host</td></tr>
    /// <tr><td style="border-bottom:none">via-received = </td><td style="border-bottom:none">"received" EQUAL (IPv4address / IPv6address)</td></tr>
    /// <tr><td style="border-bottom:none">via-branch = </td><td style="border-bottom:none">"branch" EQUAL token</td></tr>
    /// <tr><td style="border-bottom:none">via-extension = </td><td style="border-bottom:none">generic-param</td></tr>
    /// <tr><td style="border-bottom:none">sent-protocol = </td><td style="border-bottom:none">protocol-name SLASH protocol-version SLASH transport</td></tr>
    /// <tr><td style="border-bottom:none">protocol-name = </td><td style="border-bottom:none">"SIP" / token</td></tr>
    /// <tr><td style="border-bottom:none">protocol-version = </td><td style="border-bottom:none">token</td></tr>
    /// <tr><td style="border-bottom:none">transport = </td><td style="border-bottom:none">"UDP" / "TCP" / "TLS" / "SCTP" / other-transport</td></tr>
    /// <tr><td style="border-bottom:none">sent-by = </td><td style="border-bottom:none">host [ COLON port ]</td></tr> 
    /// <tr><td style="border-bottom:none">generic-param = </td><td style="border-bottom:none">token [ EQUAL token / host / quoted-string ]</td></tr> 
    /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr>
    /// <tr><td style="border-bottom:none">quoted-string = </td><td style="border-bottom:none">SWS DOUBLE_QUOTE *(qdtext / quoted-pair ) DOUBLE_QUOTE</td></tr>
    /// <tr><td style="border-bottom:none">qdtext = </td><td style="border-bottom:none">LWS / %x21 / %x23-5B / %x5D-7E / UTF8-NONASCII</td></tr>
    /// <tr><td style="border-bottom:none">LWS = </td><td style="border-bottom:none">[*WSP CRLF] 1*WSP ; linear whitespace</td></tr>
    /// <tr><td style="border-bottom:none">SWS = </td><td style="border-bottom:none">[LWS] ; sep whitespace</td></tr>
    /// <tr><td style="border-bottom:none">host = </td><td style="border-bottom:none">hostname / IPv4address / IPv6reference</td></tr>
    /// </table>  
    /// <para/>
    /// <note type="implementnotes">The compact form of the Via HeaderField is "v".</note> 
    /// <para/>
    /// <note type="implementnotes">This HeaderField allows HeaderField grouping (<see cref="T:Konnetic.Sip.Headers.HeaderFieldGroup{T}"/>).</note>
    /// <example>
    /// In this example, the message originated from a multi-homed host with two addresses, 192.0.2.1 and 192.0.2.207. The sender guessed wrong as to which network interface would be used. Erlang.bell-telephone.com noticed the mismatch and added a parameter to the previous hop's Via HeaderField value, containing the address that the packet actually came from.
    /// <list type="bullet">
    /// <item>Via: SIP/2.0/UDP erlang.bell-telephone.com:5060;branch=z9hG4bK87asdks7</item> 
    /// <item>Via: SIP/2.0/UDP 192.0.2.1:5060 ;received=192.0.2.207;branch=z9hG4bK77asjd</item> 
    /// </list> 
    /// <para/>
    /// The host or network address and port number are not required to follow the SIP URI syntax. Specifically, WHITESPACE on either side of the ":" or "/" is allowed, as shown here: 
    /// <list type="bullet">
    /// <item>Via: SIP / 2.0 / UDP first.example.com: 4000;ttl=16;maddr=224.2.0.1 ;branch=z9hG4bKa7c6a8dlze.1</item>  
    /// </list> 
    /// </example>
    /// </remarks> 
    /// <seealso cref="T:Konnetic.Sip.Headers.MediaTypeHeaderFieldBase"/>
    public sealed class ViaHeaderField : ParamatizedHeaderFieldBase
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        public const string BRANCH_MAGIC_COOKIE = "z9hG4bK";

        internal const string CompareName = "VIA";
        internal const string CompareShortName = "V";

        /// <summary>
        /// The long form of the name.
        /// </summary>
        internal const string LongName = "Via";

        /// <summary>
        /// The short form of the name.
        /// </summary>
        internal const string ShortName = "v";

        /// <summary>
        /// 
        /// </summary>
        private TransportType _transport;
        private string _protocolName;
        private string _protocolVersion;

        /// <summary>
        /// 
        /// </summary>
        private string _sentBy;

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
        /// Gets or sets the branch.
        /// </summary>
        /// <value>The branch.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="Branch"/>.</exception> 
        /// <exception cref="SipFormatException">Thrown when a user attempts to add non-token characters.</exception>  
        public string Branch
        {
            get
            {
            SipParameter sp = HeaderParameters["branch"];

            if((object)sp == null)
                {
                return string.Empty;
                }
            return sp.Value;
            }
            set
            {
            PropertyVerifier.ThrowOnNullArgument(value, "Branch");
            PropertyVerifier.ThrowOnInvalidToken(value, "Branch");
            if(string.IsNullOrEmpty(value))
                {
                RemoveParameter("branch");
                }
            else
                {
                HeaderParameters.Set("branch", value);
                }
            }
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>The key.</value>
        public string Key
        {
            get
                {
                StringBuilder sb = new StringBuilder(Branch);
                sb.Append("/");
                sb.Append(SentBy);
                return sb.ToString();
                }
        }

        /// <summary>
        /// Gets or sets the multicast address.
        /// </summary>
        /// <value>The multicast address.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="MulticastAddress"/>.</exception> 
        /// <exception cref="SipFormatException">Thrown when a user attempts to add characters not consistent with a host string.</exception>  
        public string MulticastAddress
        {
            get
            {
            SipParameter sp = HeaderParameters["maddr"];

            if((object)sp == null)
                {
                return string.Empty;
                }
            return sp.Value;
            }
            set
            {
            PropertyVerifier.ThrowOnNullArgument(value, "MulticastAddress");
            PropertyVerifier.ThrowOnInvalidHostString(value, "MulticastAddress");
            if(string.IsNullOrEmpty(value))
                {
                RemoveParameter("maddr");
                }
            else
                {
                HeaderParameters.Set("maddr", value);
                }
            }
        }

        /// <summary>
        /// Gets the name of the protocol.
        /// </summary>
        /// <value>The name of the protocol.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="ProtocolName"/>.</exception> 
        /// <exception cref="SipFormatException">Thrown when a user attempts to add non-token characters.</exception> 
        public string ProtocolName
        {
            get { return _protocolName; }
            set
            {
            PropertyVerifier.ThrowOnNullArgument(value, "ProtocolName");
            PropertyVerifier.ThrowOnInvalidToken(value, "ProtocolName");
            _protocolName = value;
            }
        }

        /// <summary>
        /// Gets the protocol version.
        /// </summary>
        /// <value>The protocol version.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="ProtocolVersion"/>.</exception> 
        /// <exception cref="SipFormatException">Thrown when a user attempts to add non-token characters.</exception> 
        public string ProtocolVersion
        {
            get { return _protocolVersion; }
            set
            {
            PropertyVerifier.ThrowOnNullArgument(value, "ProtocolVersion");
            PropertyVerifier.ThrowOnInvalidToken(value, "ProtocolVersion");
            _protocolVersion = value;
            }
        }

        /// <summary>
        /// Gets or sets the received.
        /// </summary>
        /// <value>The received.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="Received"/>.</exception> 
        /// <exception cref="SipFormatException">Thrown when a user attempts to add characters not consistent with an IPAddress.</exception>  
        public string Received
        {
            get
            {
            SipParameter sp = HeaderParameters["received"];

            if((object)sp == null)
            {
                return string.Empty;
            }
            return sp.Value;
            }
        set
            {
            PropertyVerifier.ThrowOnNullArgument(value, "Received");
            PropertyVerifier.ThrowOnInvalidIPAddressString(value, "Received"); 
            if(string.IsNullOrEmpty(value))
                {
                RemoveParameter("received");
                }
            else
                {
                HeaderParameters.Set("received", value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the sent by.
        /// </summary>
        /// <value>The sent by.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="SentBy"/>.</exception> 
        /// <exception cref="SipFormatException">Thrown when a user attempts to add characters not consistent with a host string.</exception> 
        public string SentBy
        {
            get { return _sentBy; }
            set
            {
            PropertyVerifier.ThrowOnNullArgument(value, "SentBy");
            PropertyVerifier.ThrowOnInvalidHostString(value, "SentBy");
            _sentBy = value;
            }
        }

        /// <summary>
        /// Gets or sets the time to live.
        /// </summary>
        /// <value>The time to live.</value> 
        public byte? TimeToLive
        {
            get
            {
            SipParameter sp = HeaderParameters["ttl"];

            if((object)sp == null)
                {
                return null;
                }
            return byte.Parse(sp.Value, CultureInfo.InvariantCulture);
            }
            set
            {
            if(value == null)
                {
                RemoveParameter("ttl");
                }
            else
                {
                HeaderParameters.Set("ttl", ((byte)value).ToString(CultureInfo.InvariantCulture));
                }
            }
        }

        /// <summary>
        /// Gets or sets the transport.
        /// </summary>
        /// <value>The transport.</value>
        public TransportType Transport
        {
            get { return _transport; }
            set { _transport = value; }
        }

        #endregion Properties

        #region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ViaHeaderField"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <remarks>Defaults: 
		/// <list type="bullet">
		/// <item><c>ProtocolName</c> is set to SIP.</item>
		/// <item><c>ProtocolVersion</c> is set to 2.0.</item>
		/// <item><c>Transport</c> is set to TCP.</item>
		/// <item><c>Branch</c> is set to a new valid branch value.</item>
		/// </list> </remarks>
		/// <overloads>
		/// <summary>The method has one overload.</summary>
		/// </overloads>
        public ViaHeaderField()
			: this(NewBranch())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViaHeaderField"/> class.
        /// </summary>
        /// <param name="branch">The branch.</param>
        public ViaHeaderField(string branch)
            : base()
        {
            PropertyVerifier.ThrowOnNullArgument(branch, "branch");

            RegisterKnownParameter("ttl");
            RegisterKnownParameter("maddr");
            RegisterKnownParameter("received");
            RegisterKnownParameter("branch"); 
			ProtocolName = "SIP";
			ProtocolVersion = "2.0";
			Transport = TransportType.Tcp;
			Branch = branch;
            _sentBy = string.Empty; 
            AllowMultiple = true;
            FieldName = ViaHeaderField.LongName;
            CompactName = ViaHeaderField.ShortName;
        }

 

        #endregion Constructors

        #region Methods
		/// <summary>
		/// Performs an implicit conversion from <see cref="System.String"/> to <see cref="Konnetic.Sip.Headers.ViaHeaderField"/>.
		/// </summary>
		/// <param name="value">The string value representing the HeaderField.</param>
        /// <returns>A new <see cref="Konnetic.Sip.Headers.ViaHeaderField"/> populated from the <paramref name="value"/> parameter.</returns>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="value"/>.</exception>
        public static implicit operator ViaHeaderField(String value)
        {
            PropertyVerifier.ThrowOnNullArgument(value,"value");

            ViaHeaderField hf = new ViaHeaderField();
            hf.Parse(value);
            return hf;
        }
		/// <summary>
		/// Performs an explicit conversion from <see cref="Konnetic.Sip.Headers.ViaHeaderField"/> to <see cref="System.String"/>.
		/// </summary>
		/// <param name="headerField">The HeaderField to convert.</param>
		/// <returns>A string representation of the HeaderField.</returns>
		/// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="headerField"/>.</exception>
        public static explicit operator string(ViaHeaderField headerField)
        {
            PropertyVerifier.ThrowOnNullArgument(headerField, "headerField");
            return headerField.ToString();
        }

        /// <summary>
        /// News the branch.
        /// </summary>
        /// <returns></returns>
        public static string NewBranch()
        {
            return SipGuid.NewSipGuid(BRANCH_MAGIC_COOKIE, string.Empty);
        }

        /// <summary>
        /// Creates a deep copy of this instance.
        /// </summary> 
        /// <remarks>Creates and returns a deep copy of the Header. This method ensures a deep copy of the HeaderField, when a message is cloned the HeaderField can be modified without effecting the original HeaderField in the message.</remarks>
        /// <returns>A deep copy of <see cref="T:Konnetic.Sip.Headers.ViaHeaderField"/>.</returns>
        /// <threadsafety static="true" instance="false" />
public override HeaderFieldBase Clone()
        {
            ViaHeaderField newObj = new ViaHeaderField();
            newObj.ClearParameters();
            CopyParametersTo(newObj); 
            newObj._protocolName = _protocolName;
            newObj._protocolVersion = _protocolVersion;
            newObj._sentBy = _sentBy;
            newObj._transport = _transport;
            return newObj;
        }

        ///<summary>
        /// Compare this SIP Header for equality with another <see cref="T:Konnetic.Sip.Headers.ViaHeaderField"/> object.</summary>
        /// <remarks>
        /// All optional headers are compared using object equality that is each field in the header is used for comparision. When comparing HeaderFields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular HeaderField, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are case-sensitive. 
        /// </remarks>
        /// <param name="other">The <see cref="T:Konnetic.Sip.Headers.ViaHeaderField"/> to compare to this instance.</param>
        /// <returns><c>true</c> if <paramref name="other" /> is an instance of this class representing the same SIP HeaderField as this, <c>false</c> otherwise.</returns>    
        /// <overloads>
        /// <summary>This method is overloaded.</summary>
        /// <remarks>Overloads allow for equality against <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>, <see cref="T:System.Object"/> and another instance of this HeaderField.</remarks>
        /// </overloads>  
        /// <threadsafety static="true" instance="false" />
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public bool Equals(ViaHeaderField other)
        {
        return base.Equals((ParamatizedHeaderFieldBase)other) && SentBy.Equals(other.SentBy, StringComparison.OrdinalIgnoreCase) && Transport.Equals(other.Transport);
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
            ViaHeaderField p = obj as ViaHeaderField;
            if((object)p == null)
                {
                HeaderFieldGroup<ViaHeaderField> p1 = obj as HeaderFieldGroup<ViaHeaderField>;
                if((object)p1 == null)
                    {
                    return false;
                    }
                else
                    {
                    return p1.Equals(this);
                    }
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
            return (Branch.Length>0)  && retVal;
        }

        /// <summary>
        /// Parses string representation of the HeaderField.
        /// </summary>
        /// <remarks> 
        /// <b>RFC 3261 Syntax:</b> 
        /// <table >
        /// <tr><td colspan="2" style="border-bottom:none">( "Via" / "v" ) ":" via-parm *("," via-parm)</td></tr> 
        /// <tr><td style="border-bottom:none">via-parm = </td><td style="border-bottom:none">sent-protocol WHITESPACE sent-by *( SEMI via-params )</td></tr> 
        /// <tr><td style="border-bottom:none">via-params = </td><td style="border-bottom:none">via-ttl / via-maddr / via-received / via-branch / via-extension</td></tr> 
        /// <tr><td style="border-bottom:none">via-ttl = </td><td style="border-bottom:none">"ttl" EQUAL 1*3DIGIT ; 0 to 255</td></tr> 
        /// <tr><td style="border-bottom:none">via-maddr = </td><td style="border-bottom:none">"maddr" EQUAL host</td></tr>
        /// <tr><td style="border-bottom:none">via-received = </td><td style="border-bottom:none">"received" EQUAL (IPv4address / IPv6address)</td></tr>
        /// <tr><td style="border-bottom:none">via-branch = </td><td style="border-bottom:none">"branch" EQUAL token</td></tr>
        /// <tr><td style="border-bottom:none">via-extension = </td><td style="border-bottom:none">generic-param</td></tr>
        /// <tr><td style="border-bottom:none">sent-protocol = </td><td style="border-bottom:none">protocol-name SLASH protocol-version SLASH transport</td></tr>
        /// <tr><td style="border-bottom:none">protocol-name = </td><td style="border-bottom:none">"SIP" / token</td></tr>
        /// <tr><td style="border-bottom:none">protocol-version = </td><td style="border-bottom:none">token</td></tr>
        /// <tr><td style="border-bottom:none">transport = </td><td style="border-bottom:none">"UDP" / "TCP" / "TLS" / "SCTP" / other-transport</td></tr>
        /// <tr><td style="border-bottom:none">sent-by = </td><td style="border-bottom:none">host [ COLON port ]</td></tr> 
        /// <tr><td style="border-bottom:none">generic-param = </td><td style="border-bottom:none">token [ EQUAL token / host / quoted-string ]</td></tr> 
        /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr>
        /// <tr><td style="border-bottom:none">quoted-string = </td><td style="border-bottom:none">SWS DOUBLE_QUOTE *(qdtext / quoted-pair ) DOUBLE_QUOTE</td></tr>
        /// <tr><td style="border-bottom:none">qdtext = </td><td style="border-bottom:none">LWS / %x21 / %x23-5B / %x5D-7E / UTF8-NONASCII</td></tr>
        /// <tr><td style="border-bottom:none">LWS = </td><td style="border-bottom:none">[*WSP CRLF] 1*WSP ; linear whitespace</td></tr>
        /// <tr><td style="border-bottom:none">SWS = </td><td style="border-bottom:none">[LWS] ; sep whitespace</td></tr>
        /// <tr><td style="border-bottom:none">host = </td><td style="border-bottom:none">hostname / IPv4address / IPv6reference</td></tr>
        /// </table>  
        /// <para/>
        /// <note type="implementnotes">The compact form of the Via HeaderField is "v".</note> 
        /// <para/>
        /// <note type="implementnotes">This HeaderField allows HeaderField grouping (<see cref="T:Konnetic.Sip.Headers.HeaderFieldGroup{T}"/>).</note>
        /// <example>
        /// In this example, the message originated from a multi-homed host with two addresses, 192.0.2.1 and 192.0.2.207. The sender guessed wrong as to which network interface would be used. Erlang.bell-telephone.com noticed the mismatch and added a parameter to the previous hop's Via HeaderField value, containing the address that the packet actually came from.
        /// <list type="bullet">
        /// <item>Via: SIP/2.0/UDP erlang.bell-telephone.com:5060;branch=z9hG4bK87asdks7</item> 
        /// <item>Via: SIP/2.0/UDP 192.0.2.1:5060 ;received=192.0.2.207;branch=z9hG4bK77asjd</item> 
        /// </list> 
        /// <para/>
        /// The host or network address and port number are not required to follow the SIP URI syntax. Specifically, WHITESPACE on either side of the ":" or "/" is allowed, as shown here: 
        /// <list type="bullet">
        /// <item>Via: SIP / 2.0 / UDP first.example.com: 4000;ttl=16;maddr=224.2.0.1 ;branch=z9hG4bKa7c6a8dlze.1</item>  
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
                SentBy = string.Empty;
                Transport = TransportType.Unknown;
                TimeToLive = null;
                base.Parse(value);
                if(!string.IsNullOrEmpty(value))
                    {

                    Regex _pcolNameRegex = new Regex(@"(?<=^\s*)[\w-.!%_*+`'~]+(?=/)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    Regex _pcolVersionRegex = new Regex(@"(?<=^\s*[\w-.!%_*+`'~]+/)[\w-.!%_*+`'~]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    Regex _sentByRegex = new Regex(@"(?<=^(.|\n)*(UDP|TCP|TLS|SCTP)\s)([^;\s]+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    Regex _transportRegex = new Regex(@"(?<=(.|\n)*)(UDP|TCP|TLS|SCTP)(?=\s*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    Regex _branchRegex = new Regex(@"(?<=(.|\n)*branch\s*={1}\s*)[\w-.!%_*+`'~]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    Regex _ttlRegex = new Regex(@"(?<=(.|\n)*ttl\s*={1}\s*)[0-9]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    Regex _maddrRegex = new Regex(@"(?<=(.|\n)*maddr\s*={1}\s*)[\w:\-.\[\]%]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    Regex _receivedRegex = new Regex(@"(?<=(.|\n)*received\s*={1}\s*)[\w:\-.\[\]%]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    Match m = _pcolNameRegex.Match(value);
                    string tValue = m.Value;
                    if(!string.IsNullOrEmpty(tValue))
                        {
						try{
                        ProtocolName = tValue;
						}
						catch(SipException ex)
							{
							throw new SipParseException("ProtocolName", SR.ParseExceptionMessage(value), ex);
							} 
						catch(Exception ex)
                            {
                            throw new SipParseException(SR.GetString(SR.GeneralParseException, m.Value, "ProtocolName"), ex);
							}
                        }
                    m = _pcolVersionRegex.Match(value);
                    tValue = m.Value;
                    if(!string.IsNullOrEmpty(tValue))
                        {
						try{
                        ProtocolVersion = tValue;
						}
						catch(SipException ex)
							{
							throw new SipParseException("ProtocolVersion", SR.ParseExceptionMessage(value), ex);
							}
						catch(Exception ex)
                            {
                            throw new SipParseException(SR.GetString(SR.GeneralParseException, m.Value, "ProtocolVersion"), ex);
							}
                        }

                    m = _sentByRegex.Match(value);
                    tValue = m.Value;
                    if(!string.IsNullOrEmpty(tValue))
                        {
						try{
                        SentBy = tValue;
						}
						catch(SipException ex)
							{
							throw new SipParseException("SentBy", SR.ParseExceptionMessage(value), ex);
							}
						catch(Exception ex)
                            {
                            throw new SipParseException(SR.GetString(SR.GeneralParseException, m.Value, "SentBy"), ex); 
							}
                        }
                    m = _transportRegex.Match(value);
                    tValue = m.Value;
                    if(!string.IsNullOrEmpty(tValue))
                        {
                        try
                            {
                            Transport = (TransportType)Enum.Parse(typeof(TransportType), tValue, true);
                            }
						catch(SipException ex)
							{
							throw new SipParseException("Transport", SR.ParseExceptionMessage(value), ex);
							}
						catch(ArgumentException ex)
							{
							throw new SipParseException("Not a valid Transport Type.", ex);
							}
						catch(Exception ex)
                            {
                            throw new SipParseException(SR.GetString(SR.GeneralParseException, m.Value, "Transport"), ex); 
							}
                        }
 
                    m = _branchRegex.Match(value);
                    tValue = m.Value;
                    if(!string.IsNullOrEmpty(tValue))
                        {
						try{
                        Branch = tValue;
						}
						catch(SipException ex)
							{
							throw new SipParseException("Branch", SR.ParseExceptionMessage(value), ex);
							} 
						catch(Exception ex)
                            {
                            throw new SipParseException(SR.GetString(SR.GeneralParseException, m.Value, "Branch"), ex);  
							}
                        }
                    m = _ttlRegex.Match(value);
                    tValue = m.Value;
                    if(!string.IsNullOrEmpty(tValue))
                        {
                        try
                            {
                            TimeToLive = byte.Parse(tValue, CultureInfo.InvariantCulture);
                            }
						catch(SipException ex)
							{
							throw new SipParseException("TimeToLive", SR.ParseExceptionMessage(value), ex);
							}
						catch(FormatException ex)
                            {
                            throw new SipParseException(SR.GetString(SR.ByteConvertException, tValue, "TimeToLive"), ex);
							}
						catch(OverflowException ex)
                            {
                            throw new SipParseException(SR.GetString(SR.OverflowException, tValue, "TimeToLive"), ex);
							}
						catch(Exception ex)
                            {
                            throw new SipParseException(SR.GetString(SR.GeneralParseException, m.Value, "TimeToLive"), ex);  
							}
                        }
                    m = _maddrRegex.Match(value);
                    tValue = m.Value;
                    if(!string.IsNullOrEmpty(tValue))
                        {
						try{
                        MulticastAddress = tValue;
						}
						catch(SipException ex)
							{
							throw new SipParseException("MulticastAddress", SR.ParseExceptionMessage(value), ex);
							} 
						catch(Exception ex)
                            {
                            throw new SipParseException(SR.GetString(SR.GeneralParseException, m.Value, "MulticastAddress"), ex); 
							}
                        }
                    m = _receivedRegex.Match(value);
                    tValue = m.Value;
                    if(!string.IsNullOrEmpty(tValue))
                        {
						try{
                        Received = tValue;
						}
						catch(SipException ex)
							{
							throw new SipParseException("Received", SR.ParseExceptionMessage(value), ex);
							}
						catch(Exception ex)
                            {
                            throw new SipParseException(SR.GetString(SR.GeneralParseException, m.Value, "Received"), ex);  
							}
                        }
                    }
                }
        }

        /// <summary>
        /// Recreates the branch.
        /// </summary>
        public void RecreateBranch()
        {
            Branch = ViaHeaderField.NewBranch();
        }
        /// <summary>
        /// Gets a string representation of the HeaderField value. The HeaderField name is excluded.
        /// </summary>
        /// <remarks>This method overrides the <c>GetStringValue</c> method in <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>. </remarks>
        /// <returns>String representation of the HeaderField value</returns>
        /// <threadsafety static="true" instance="false" />
        //public override string GetStringValue()
        //{
        //    if(string.IsNullOrEmpty(ProtocolName) && string.IsNullOrEmpty(ProtocolVersion) && Transport == TransportType.Unknown)
        //    {
        //    return string.Empty;
        //    }
        //    StringBuilder sb = new StringBuilder(GetStringValueNoParams()); 
        //    sb.Append(TimeToLive > 0 ? SR.ParameterPrefix + "ttl" + SR.ParameterSeperator + TimeToLive.ToString(CultureInfo.InvariantCulture) : string.Empty);
        //    sb.Append(MulticastAddress.Length > 0 ? SR.ParameterPrefix + "maddr" + SR.ParameterSeperator + MulticastAddress : string.Empty);
        //    sb.Append(Received.Length > 0 ? SR.ParameterPrefix + "received" + SR.ParameterSeperator + Received : string.Empty);
        //    sb.Append(Branch.Length > 0 ? SR.ParameterPrefix + "branch" + SR.ParameterSeperator + Branch : string.Empty); 
        //    sb.Append(GenericParameters.ToString());

        //    return sb.ToString();
        //}

        protected override string GetStringValueNoParams()
            {
            if(string.IsNullOrEmpty(ProtocolName) && string.IsNullOrEmpty(ProtocolVersion) && Transport == TransportType.Unknown)
                {
                return string.Empty;
                }
            StringBuilder sb = new StringBuilder(100);
            sb.Append(ProtocolName);
            sb.Append(SR.ViaProtocolSeperator);
            sb.Append(ProtocolVersion);
            sb.Append(SR.ViaProtocolSeperator);
            sb.Append(Enum.GetName(typeof(TransportType), Transport).ToUpperInvariant());
            sb.Append(SR.SingleWhiteSpace);
            sb.Append(SentBy);


            return sb.ToString();
            }

        #endregion Methods
    }
}