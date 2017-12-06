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

using Konnetic.Sip.Headers;
using Konnetic.Sip.Messages;

namespace Konnetic.Sip
    {
    /// <summary>
    /// Provides a custom constructor for SIP uniform resource identifiers (URIs) and modifies SIP URIs for the <see cref="T:Konnetic.Sip.SipUri"/> class.
    /// </summary>
    /// <remarks>
    /// The <see cref="T:Konnetic.Sip.SipUri"/> class provides a convenient way to modify the contents of a Uri instance without creating a new <see cref="T:Konnetic.Sip.SipUri"/> instance for each modification.
    /// <para/>
    /// The <see cref="T:Konnetic.Sip.SipUri"/> properties provide read/write access to the read-only <see cref="T:Konnetic.Sip.SipUri"/> properties so that they can be modified.
    /// </remarks>
    public sealed class SipUriBuilder
        {

        #region Fields
        private const int DEFAULTPORT = 5060;
        private const int DEFAULTSECUREPORT = 5061;
        private HeaderFieldCollection _headers;
        private SipUriParameterCollection _parameters;
        private bool _hasChanged;
        private SipUri _sipUri;
        private string _sipUriString;
        private UriBuilder _target;
        private SipStyleUriParser _parser;
        private string _host;  
        private string _password;
        private int? _port;
        private string _scheme; 
        private string _userName;
        private bool _isIPv6;
        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the <see cref="T:Konnetic.Sip.HeaderFieldCollection"/> collection of header fields to be included in a request constructed from the SipUri.
        /// </summary>
        /// <remarks>The return value is a read/write collection. Headers fields in the SIP request can be specified with the "?" mechanism within a URI. The header names and values are encoded in ampersand separated hname = hvalue pairs. The special hname "body" indicates that the associated hvalue is the message-body of the SIP request.</remarks>
        /// <value>The <see cref="T:Konnetic.Sip.HeaderFieldCollection"/> headers.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="Headers"/>.</exception>  
        public System.Collections.ObjectModel.ReadOnlyCollection<HeaderFieldBase> Headers
            {
            get { return new System.Collections.ObjectModel.ReadOnlyCollection<HeaderFieldBase>(_headers); } 
            }

        public bool IsIPv6
            {
            get { return _isIPv6; }
            }
        /// <summary>
        /// Gets the <see cref="T:Konnetic.Sip.SipUriParameterCollection"/> parameters affecting a request constructed from the URI.
        /// </summary>
        /// <remarks>The return value is a read/write collection.</remarks>
        /// <value>The <see cref="T:Konnetic.Sip.SipUriParameterCollection"/> parameters affecting a request constructed from the URI.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="Parameters"/>.</exception> 
        public System.Collections.ObjectModel.ReadOnlyCollection<SipParameter> Parameters
            {
            get { return new System.Collections.ObjectModel.ReadOnlyCollection<SipParameter>(_parameters); }
            }

        /// <summary>
        /// Gets or sets the Domain Name System (DNS) host name or IP address of the host providing the SIP resource.
        /// </summary>
        /// <remarks>The Host property contains the fully qualified DNS host name or IP address of the host providing the SIP resource. The host part contains either a fully-qualified domain name or numeric IPv4 or IPv6 address. Using the fully-qualified domain name form is RECOMMENDED whenever possible.</remarks>
        /// <value>The DNS host name or IP address of the host providing the SIP resource.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="Host"/>.</exception> 
        /// <exception cref="SipFormatException">Thrown when a user attempts to add characters not consistent with a host string.</exception> 
        public string Host
            {
            get { return _host; }
            set
                {
                PropertyVerifier.ThrowOnNullArgument(value, "Host");
                PropertyVerifier.ThrowUriExceptionOnInvalidHostString(value, "Host");
                if(value.Contains(":"))
                    {
                    value = "[" + value + "]"; //IPv6
                    _isIPv6 = true;
                    }
                _host = value;
                _hasChanged = true;
                }
            }

        /// <summary>
        /// Gets or sets a value indicating whether the Uri represents a loose router ("lr").
        /// </summary>
        /// <remarks>Indicating whether the Uri represents a loose router ("lr").</remarks>
        /// <value><c>true</c> if the Uri represents a loose router; otherwise, <c>false</c>.</value> 
        [DefaultValue(true)]
        public bool LooseRouter
            {
            [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
            get
                {
                return _parameters["lr"] != null;
                }
            set
                {
                if(value == true)
                    {
                    _parameters.Set("lr", "lr");
                    }
                else
                    {
                    _parameters.Remove("lr");
                    }
                _hasChanged = true;
                }
            }

        /// <summary>
        /// Gets or sets the multicast address ("maddr") which indicates the server address to be contacted for this user, overriding any address derived from the host field..
        /// </summary>
        /// <remarks>The IP address for the multicast address ("maddr"). The maddr parameter indicates the server address to be contacted for this user, overriding any address derived from the host field. When an maddr parameter is present, the port and transport components of the URI apply to the address indicated in the maddr parameter value.</remarks>
        /// <value>The multicast address ("maddr").</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="Maddr"/>.</exception> 
        /// <exception cref="SipFormatException">Thrown when a user attempts to add characters not consistent with a host string.</exception>  
        public string MulticastAddress
            {
            get
                {
                SipParameter sp = _parameters["maddr"];
                if((object)sp == null)
                    {
                    return string.Empty;
                    }
                else
                    {
                    return sp.Value;
                    }
                }
            set
                {
                PropertyVerifier.ThrowOnNullArgument(value, "Maddr");
                PropertyVerifier.ThrowUriExceptionOnInvalidHostString(value, "Maddr");
                if(value.Length > 0)
                    {
                    _parameters.Set("maddr", value);
                    }
                else
                    {
                    _parameters.Remove("maddr");
                    }
                _hasChanged = true;
                }
            }

        /// <summary>
        /// Gets or sets the SIP <see cref="T:Konnetic.Sip.Messages.SipMethod"/> of the SipUri.
        /// </summary>
        /// <remarks>The SIP <see cref="T:Konnetic.Sip.Messages.SipMethod"/> of the SipUri.</remarks>
        /// <value>The SIP <see cref="T:Konnetic.Sip.Messages.SipMethod"/> of the SipUri.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="Method"/>.</exception> 
        /// <exception cref="SipUriFormatException">Thrown when a user attempts to add non-token characters.</exception>  
        [DefaultValue("INVITE")]
        public SipMethod Method
            {
            get
                {
                SipParameter sp = _parameters["method"];
                if((object)sp == null)
                    {
                    return SipMethod.Empty;
                    }
                else
                    {
                    return new SipMethod(sp.Value);
                    }
                }
            set
                {

                if(value == SipMethod.Empty)
                    {
                    _parameters.Remove("method");
                    }
                else
                    {
                    PropertyVerifier.ThrowUriExceptionOnInvalidToken(value.Method, "Method");
                    _parameters.Set("method", value);
                    }
                _hasChanged = true;
                }
            }


        /// <summary>
        /// Gets or sets the password associated with the user that accesses the <see cref="T:Konnetic.Sip.SipUri"/>.
        /// </summary>
        /// <remarks>While the SIP and SIPS URI syntax allows this field to be present, its use is NOT RECOMMENDED , because the passing of authentication information in clear text (such as URIs) has proven to be a security risk in almost every case where it has been used. For instance, transporting a PIN number in this field exposes the PIN.</remarks>
        /// <value>The password of the user that accesses the <see cref="T:Konnetic.Sip.SipUri"/>.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="Password"/>.</exception> 
        /// <exception cref="SipUriFormatException">Is raised when <paramref name="Password"/> contains invalid characters for a password.</exception> 
        public string Password
            {
            get
                {
                return _password;
                }
            set
                {
                PropertyVerifier.ThrowOnNullArgument(value, "Password");
                PropertyVerifier.ThrowUriExceptionOnReservedPassword(value, "Password");
                _password = value;
                _hasChanged = true;
                }
            }

        /// <summary>
        /// Gets or sets the port number of the <see cref="T:Konnetic.Sip.SipUri"/>.
        /// </summary>
        /// <value>The port number of the <see cref="T:Konnetic.Sip.SipUri"/>.</value>
        /// <exception cref="SipOutOfRangeException">Thrown when <paramref name="Port"/> is not between zero and 65535.</exception> 
        [DefaultValue(5060)]
        public int? Port
            {
            get { return _port; }
            set
                {
                if(value != null) 
                    {
                    PropertyVerifier.ThrowIfIntOutOfRange(value, 0, 65535, "Port");
                    }
                _port = value;
                _hasChanged = true;
                }
            }

        /// <summary>
        /// Gets or sets the scheme name of the <see cref="T:Konnetic.Sip.SipUri"/>.
        /// </summary>
        /// <value>The scheme name of the <see cref="T:Konnetic.Sip.SipUri"/>.</value>
        /// <remarks>Valid scheme names for the <see cref="P:Konnetic.Sip.SipUriBuilder.Scheme"/> property are "sip" and "sips". The property is case-insensive.</remarks>       
        [DefaultValue(SipScheme.Sip)]
        public SipScheme Scheme
            {
            get
                {
                if(_scheme.ToUpperInvariant() == "SIPS")
                    {
                    return SipScheme.Sips;
                    }
                else
                    {
                    return SipScheme.Sip;
                    }
                }
            set
                {
                _scheme = Enum<SipScheme>.Description(value);
                _hasChanged = true;
                }
            }

        /// <summary>
        /// Gets or sets the time-to-live ("ttl") used by the <see cref="T:Konnetic.Sip.SipUri"/>.
        /// </summary>
        /// <value>The time-to-live ("ttl") used by the <see cref="T:Konnetic.Sip.SipUri"/>. The ttl parameter determines the time-to-live value of the UDP multicast packet and MUST only be used if maddr is a multicast address and the transport protocol is UDP</value>
        /// <remarks>The time-to-live ("ttl") used by the <see cref="T:Konnetic.Sip.SipUri"/>. A null value removes the property from the <see cref="T:Konnetic.Sip.SipUri"/>.</remarks>
        [DefaultValue(255)]
        public Byte? TimeToLive
            {
            get
                {
                Byte retVal = 0;
                SipParameter sp = _parameters["ttl"];
                if((object)sp == null)
                    {
                    retVal = 0;
                    }
                else
                    {
                        retVal = Byte.Parse(sp.Value, CultureInfo.InvariantCulture);
                    }
                return retVal;

                }
            set
                {
                if(value == null)
                    {
                    _parameters.Remove("ttl");
                    }
                else
                    {
                    _parameters.Set("ttl", ((byte)value).ToString(CultureInfo.InvariantCulture));
                    }
                _hasChanged = true;
                }
            }

        /// <summary>
        /// Gets or sets the transport type used by the <see cref="T:Konnetic.Sip.SipUri"/>.
        /// </summary>
        /// <value>The transport type used by the <see cref="T:Konnetic.Sip.SipUri"/>.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="Transport"/>.</exception> 
        /// <exception cref="SipUriFormatException">Thrown when a user attempts to add invalid characters for the Transport.</exception> 
        [DefaultValue("TCP")]
        public string Transport
            {
            get
                {
                SipParameter sp = _parameters["transport"];
                if((object)sp == null)
                    {
                    return string.Empty;
                    }
                else
                    {
                    return sp.Value;
                    }
                }
            set
                {
                PropertyVerifier.ThrowOnNullArgument(value, "Transport");
                PropertyVerifier.ThrowUriExceptionOnInvalidToken(value, "Transport");
                if(value.Length > 0)
                    {
                    _parameters.Set("transport", value);
                    }
                else
                    {
                    _parameters.Remove("transport");
                    }
                _hasChanged = true;
                }
            }

 
        /// <summary>
        /// The user name associated with the user that accesses the <see cref="T:Konnetic.Sip.SipUri"/>.
        /// </summary>
        /// <value>The user name associated with the user that accesses the <see cref="T:Konnetic.Sip.SipUri"/>.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="UserName"/>.</exception> 
        /// <exception cref="SipUriFormatException">Is raised when <paramref name="UserName"/> contains invalid characters for a username.</exception> 
        public string UserName
            {
            get { return _userName; }
            set
                {
                _hasChanged = true;
                PropertyVerifier.ThrowOnNullArgument(value, "UserName");
                PropertyVerifier.ThrowUriExceptionOnReservedUsername(value, "UserName"); 
                _userName = value;
                }
            }

        /// <summary>
        /// Gets or sets the identifier of a particular resource at the host being addressed.
        /// </summary>
        /// <value>The identifier of a particular resource at the host being addressed.</value>
        /// <remarks>The identifier of a particular resource at the host being addressed. The term "host" in this context frequently refers to a domain. The userinfo of a URI consists of this user field, the password field, and the @ sign following them. The userinfo part of a URI is optional and MAY be absent when the destination host does not have a notion of users or when the host itself is the resource being identified. If the @ sign is present in a SIP or SIPS URI, the user field MUST NOT be empty.
        /// <para/>
        /// If the host being addressed can process telephone numbers, for instance, an Internet telephony gateway, a telephone- subscriber field defined in RFC 2806 MAY be used to populate the user field.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="UserParameter"/>.</exception> 
        /// <exception cref="SipFormatException">Thrown when a user attempts to add non-token characters.</exception> 
        [DefaultValue("IP")]
        public string UserParameter
            {
            get
                {
                SipParameter sp = _parameters["User"];
                if((object)sp == null)
                    {
                    return string.Empty;
                    }
                else
                    {
                    return sp.Value;
                    }
                }
            set
                {
                PropertyVerifier.ThrowOnNullArgument(value, "UserParameter");
                PropertyVerifier.ThrowUriExceptionOnInvalidToken(value, "UserParameter");
                if(value.Length > 0)
                    {
                    _parameters.Set("user", value);
                    }
                else
                    {
                    _parameters.Remove("user");
                    }
                _hasChanged = true;
                }
            }

        /// <summary>
        /// Gets the <see cref="T:Konnetic.Sip.SipUri"/> instance constructed by the specified <see cref="T:Konnetic.Sip.SipUriBuilder"/> instance.
        /// </summary>
        /// <value>A <see cref="T:Konnetic.Sip.SipUri"/> instance constructed by the specified <see cref="T:Konnetic.Sip.SipUriBuilder"/> instance.</value>
        public SipUri SipUri
            {
            get
                {
                if(_hasChanged)
                    {
                    ToString(); //populates the _sipUri.
                    }
                return _sipUri; 
                }
            }

        #endregion Properties

        #region Constructors
        
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Konnetic.Sip.SipUriBuilder"/> class with the specified SipUri.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <overloads>
        /// <summary>The method has two overload.</summary>
        /// <remarks>Overloads allow for initialising the SipUri.</remarks>
        /// </overloads>
        public SipUriBuilder(string uri)
            : this()
            {
            if(!uri.ToUpperInvariant().StartsWith("SIP"))
                {
                throw new SipUriFormatException("Not a SIP Uri.");
                } 

            init(uri);
            }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Konnetic.Sip.SipUriBuilder"/> class.
        /// </summary>
        public SipUriBuilder()
            { 
            _parameters = new SipUriParameterCollection();
            _headers = new HeaderFieldCollection();
            Scheme = SipScheme.Sip;
            _parser = new SipStyleUriParser();
            _hasChanged = false;
            _sipUri = new SipUri(); 
            _sipUriString = string.Empty;  
            _host = "localhost";  
            _password = string.Empty;
            _port = null; 
            _userName = string.Empty;
            }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Konnetic.Sip.SipUriBuilder"/> class with the specified <see cref="T:Konnetic.Sip.SipUri"/> instance..
        /// </summary>
        /// <param name="uri">An instance of the <see cref="T:Konnetic.Sip.SipUri"/> class.</param> 
        /// <exception cref="T:System.ArgumentNullException">Thrown when the <paramref name="uri"/> parameter is null (<b>Nothing</b> in Visual Basic).</exception>
        public SipUriBuilder(SipUri uri) : this()
            {
            PropertyVerifier.ThrowOnNullArgument(uri, "uri");
            init(uri.OriginalString);
            }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Returns a display <see cref="System.String"/> for this instance.
        /// </summary>
        /// <returns>The <see cref="System.String"/> that contains the unescaped display string of the <see cref="T:Konnetic.Sip.SipUri"/> class.
        /// </returns>
        /// <remarks>The display string always contains the <see cref="P:Konnetic.Sip.SipUriBuilder.Port"/> property value, even if it is the default port for the <see cref="P:Konnetic.Sip.SipUriBuilder.Scheme"/>. The <see cref="System.String"/> returned by the <see cref="M:Konnetic.Sip.SipUri.ToString()"/> method only contains the port if it is not the default port for the scheme.</remarks>
        public override string ToString()
            {

            if(String.IsNullOrEmpty(UserName) && Password.Length > 0)
                {
                return string.Empty;
                //throw new SipUriFormatException("Illegal to have password and no username.");
                }
            if(String.IsNullOrEmpty(Host))
                {
                return string.Empty;
                //throw new SipUriFormatException("Illegal to have no host.");
                }
            string strScheme = (this.Scheme != SipScheme.Unknown) ? (Enum<SipScheme>.Description(this.Scheme) + ":") : string.Empty;
            if(String.IsNullOrEmpty(strScheme))
                {
                return string.Empty;
                //throw new SipUriFormatException("Illegal to have no scheme.");
                }

            if(_hasChanged)
                {
                string[] strArray = new string[]
                {	
                    strScheme,
                    UserName,
                    (Password.Length > 0) ? (":" + Password) : string.Empty,
                    (UserName.Length > 0) ? "@" : string.Empty,
                    Host,
                    (Port == DEFAULTPORT || Port == DEFAULTSECUREPORT || Port == null) ? string.Empty : (":" + Port),
                    (_parameters.Count > 0) ? _parameters.ToUriString() : string.Empty,
                    (_headers.Count > 0) ? _headers.ToUriString() : string.Empty
                };
                _sipUriString = string.Concat(strArray);
                _sipUri = new SipUri(_sipUriString);
                _hasChanged = false;
                }
            return _sipUriString;
            }



        /// <summary>
        /// Initializes the specified URI.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <exception cref="T:System.SipUriFormatException">Thrown when the <paramref name="uri"/> parameter is invalid.</exception>
        private void init(string uri)
            {
            try
                {
                _target = new UriBuilder(uri); //TODO. Remove this once we have a fully tested SipUriParser
                SipUriParameterCollection pCol;
                SipStyleUriParser.ParseParameters(uri, out pCol);
                for(int i = 0; i <= pCol.Count - 1; i++)
                    {
                    AddParameter(pCol[i]);
                    }
                SipStyleUriParser.ParseHeaders(uri, out _headers); 
                string retVal = _parser.GetComponents(uri, SipUriComponents.Port);
                if(string.IsNullOrEmpty(retVal))
                    {
                    Port = null;
                    }
                else
                    {
                    Port = Int32.Parse(retVal);
                    } 
                Host = _target.Host; //parser.GetComponents(uri, SipUriComponents.Host);
                retVal = _parser.GetComponents(uri, SipUriComponents.Scheme).ToUpperInvariant();
                if(retVal == "SIP")
                    {
                    Scheme = SipScheme.Sip;
                    }
                else if(retVal == "SIPS")
                    {
                    Scheme = SipScheme.Sips;
                    }
                else
                    {
                    Scheme = SipScheme.Unknown;
                    }
                Password = _parser.GetComponents(uri, SipUriComponents.Password);
                UserName = _parser.GetComponents(uri, SipUriComponents.UserName);
                retVal = _parser.GetComponents(uri, SipUriComponents.Method);
                Method = string.IsNullOrEmpty(retVal) ? SipMethod.Empty : new SipMethod(retVal);
                LooseRouter = _parser.GetComponents(uri, SipUriComponents.LooseRouter).Length > 0;
                MulticastAddress = _parser.GetComponents(uri, SipUriComponents.MulticastAddress);
                retVal = _parser.GetComponents(uri, SipUriComponents.TimeToLive);
                if(string.IsNullOrEmpty(retVal))
                    {
                    TimeToLive = null;
                    }
                else
                    {
                    TimeToLive = byte.Parse(retVal);
                    }
                Transport = _parser.GetComponents(uri, SipUriComponents.Transport);
                UserParameter = _parser.GetComponents(uri, SipUriComponents.UserParameter);

                if(Transport.ToUpperInvariant()!="UDP")
                    {
                    if(TimeToLive > 0)
                        {
                        throw new SipException("Transport must be 'UDP' if TimeToLive is set.");
                        }
                    }

                }
            catch(UriFormatException ex)
                {
                throw new SipUriFormatException("The URI constructed by the UriBuilder was invalid", ex);
                }

            }


        /// <summary>
        /// Removes the header from the header collection.
        /// </summary>
        /// <param name="headerfieldName">The name of the header.</param> 
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="headerfieldName"/>.</exception>
        /// <exception cref="ArgumentException">Thrown on empty <paramref name="headerfieldName"/>.</exception>
        /// <threadsafety static="true" instance="false" />
        public void RemoveHeader(string headerfieldName)
            {
            PropertyVerifier.ThrowOnNullOrEmptyString(headerfieldName, "headerfieldName");
            _headers.Remove(headerfieldName);
            _hasChanged = true;
            }
        /// <summary>
        /// Clears the headers.
        /// </summary>
        /// <threadsafety static="true" instance="false" />
        public void ClearHeaders()
            {
            _headers.Clear();
            _hasChanged = true;
            }
        
        /// <summary>
        /// Adds a header to the SipUri.
        /// </summary>
        /// <param name="header">The header to add.</param>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="header"/>.</exception>
        /// <threadsafety static="true" instance="false" />  
        public void AddHeader(HeaderFieldBase header)
            {
            if(!HeaderContain(header.FieldName))
                {
                _headers.Add(header);
                }
            _hasChanged = true;
            }


        /// <summary>
        /// Removes the parameter from the parameter collection.
        /// </summary>
        /// <param name="parameterName">The name of the parameter.</param> 
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="parameterName"/>.</exception>
        /// <exception cref="ArgumentException">Thrown on empty <paramref name="parameterName"/>.</exception>
        /// <threadsafety static="true" instance="false" />
        public void RemoveParameter(string parameterName)
            {
            PropertyVerifier.ThrowOnNullOrEmptyString(parameterName, "parameterName");
            _parameters.Remove(parameterName);
            _hasChanged = true;
            }

        /// <summary>
        /// Clears the parameters.
        /// </summary>
        /// <threadsafety static="true" instance="false" />
        public void ClearParameters()
            {
            _parameters.Clear();
            _hasChanged = true;
            }

        /// <summary>
        /// Adds a generic parameter to the SipUri.
        /// </summary>
        /// <param name="parameter">The parameter to add.</param>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="parameter"/>.</exception> 
        /// <threadsafety static="true" instance="false" />  
        public void AddParameter(SipParameter parameter)
            {
            switch(parameter.Name.ToUpperInvariant())
                {
                case "TTL":
                    try
                        {
                        byte ttl = byte.Parse(parameter.Value);
                        if(ttl>0)
                            {
                            if(parameter.Value.ToUpperInvariant() != "UDP")
                                {
                                throw new SipException("Transport must be 'UDP' if TimeToLive is set.");
                                }
                            }
                        TimeToLive = ttl;
                        }
                    catch(FormatException ex)
                        {
                        throw new SipUriFormatException("Time-to-live must be a numeric.", ex);
                        }
                    catch(OverflowException ex)
                        {
                        throw new SipOutOfRangeException("ttl", "Time-to-live must be between zero and 255.", ex);
                        }
                    break;
                case "MADDR":
                    MulticastAddress = parameter.Value;
                    break;
                case "LR":
                    LooseRouter = true;
                    break;
                case "METHOD":
                    Method = new SipMethod(parameter.Value);
                    break;
                case "USER":
                    UserParameter = parameter.Value;
                    break;
                case "TRANSPORT":
                    if(parameter.Value.ToUpperInvariant() != "UDP")
                        {
                        if(TimeToLive > 0)
                            {
                            throw new SipException("Transport must be 'UDP' if TimeToLive is set.");
                            }
                        }
                    Transport = parameter.Value;
                    break; 
                default:
                    if(!ParamsContain(parameter.Name))
                        {
                        _parameters.Add(parameter);
                        }
                    break;
                }
            _hasChanged = true;
            }
        private bool HeaderContain(string headername)
            {
            return null != _headers[headername, StringComparison.OrdinalIgnoreCase];
            }
        private bool ParamsContain(string paramname)
            {
            return null != _parameters[paramname, StringComparison.OrdinalIgnoreCase];
            }
        #endregion Methods
        }
    }