/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Options = System.GenericUriParserOptions;
using System.Globalization;
using System.Text.RegularExpressions;

using Konnetic.Sip.Headers;

namespace Konnetic.Sip
{
    /// <summary>
    /// A customizable parser based on the SIP scheme.
    /// </summary>
    public sealed class SipStyleUriParser : GenericUriParser
    {
        #region Fields 
        private static readonly Regex _headers = new Regex(@"(?<=[\?&])([\w\[\]/:&\+\$-.!~*'\(\)%]+\=[^&;]+)(?=[&]|$)", RegexOptions.Compiled | RegexOptions.IgnoreCase); 
        private static readonly Regex _host = new Regex(@"(?<=^\s*sip(s)?\s*:\s*)[\w:\-.\[\]%]+(?=(:[0-9]{2,5}))|(?<=^\s*sip(s)?\s*:\s*)[\w:\-.\[\]%]+", RegexOptions.Compiled | RegexOptions.IgnoreCase); 
        private static readonly Regex _lr = new Regex(@"(?<=^\s*sip(?:s)?:.*;)lr((?=[;?])|(?=$))", RegexOptions.Compiled | RegexOptions.IgnoreCase); 
        private static readonly Regex _maddr = new Regex(@"(?<=^\s*sip(?:s)?:.*;maddr=)([\w:\-.\[\]%]*(?=[;?])|[\w:\-.\[\]%]*(?=$))", RegexOptions.Compiled | RegexOptions.IgnoreCase); 
        private static readonly Regex _method = new Regex(@"(?<=^\s*sip(?:s)?:.*;method=)([\w-.!%_*+`'~]*(?=[;?])|[\w-.!%_*+`'~]*(?=$))", RegexOptions.Compiled | RegexOptions.IgnoreCase); 
        private static readonly Regex _parameters = new Regex(@"(?<=;)([\w\[\]/:&\+\$-.!~*'\(\)%]+\=?[\w\[\]/:&\+\$-.!~*'\(\)%]+)(?=[;\?]|$)", RegexOptions.Compiled | RegexOptions.IgnoreCase); 
        private static readonly Regex _password = new Regex(@"(?:(?<=^\s*sip(?:s)?:[^:@]*:))(?<Password>[^:@;?/]*)(?=[@])", RegexOptions.Compiled | RegexOptions.IgnoreCase); 
        private static readonly Regex _port = new Regex(@"(?<=^\s*sip(s)?(:|.*@)[\w:\-./\\]*:)([0-9]{2,5})(?=([;\?\s]|$))", RegexOptions.Compiled | RegexOptions.IgnoreCase); 
        private static readonly Regex _scheme = new Regex(@"(?<=^\s*)sip(?:s)?", RegexOptions.Compiled | RegexOptions.IgnoreCase); 
        private static readonly Regex _transport = new Regex(@"(?<=^\s*sip(?:s)?:.*;transport=)([\w-.!%_*+`'~]*(?=[;?])|[\w-.!%_*+`'~]*(?=$))", RegexOptions.Compiled | RegexOptions.IgnoreCase); 
        private static readonly Regex _ttl = new Regex(@"(?<=^\s*sip(?:s)?:.*;ttl=)([0-9]*(?=[;?])|[0-9]*(?=$))", RegexOptions.Compiled | RegexOptions.IgnoreCase); 
        private static readonly Regex _user = new Regex(@"(?:(?<=^\s*sip(?:s)?:))[^:@]*(?=[@]|:.*@)", RegexOptions.Compiled | RegexOptions.IgnoreCase); 
        private static readonly Regex _userinfoReplace = new Regex(@"(?<=^\s*sip(s)?\s*:\s*)([^@]*@)", RegexOptions.Compiled | RegexOptions.IgnoreCase); 
        private static readonly Regex _userparam = new Regex(@"(?<=^\s*sip(?:s)?:.*;user=)([\w-.!%_*+`'~]*(?=[;?])|[\w-.!%_*+`'~]*(?=$))", RegexOptions.Compiled | RegexOptions.IgnoreCase); 
        private static object s_InternalSyncObject; 
        private LastValue LastHostValue;
        private int? _defaultPort = 5060;
        
        #endregion Fields

        #region Properties
        /// <summary>
        /// Gets or sets the default port for the scheme.
        /// </summary>
        /// <value>The default port.</value>
        /// <exception cref="SipOutOfRangeException">Thrown when <paramref name="DefaultPort"/> is not between zero and 65535.</exception> 
        public int? DefaultPort
            {
            get { return _defaultPort; }
            set
                {
                if(value == null)
                    {
                    _defaultPort = null;
                    }
                else
                    {
                    PropertyVerifier.ThrowIfIntOutOfRange(value, 0, 65535, "defaultPort");
                    _defaultPort = value;
                    }
                }
            }
        /// <summary>
        /// Gets the host.
        /// </summary>
        /// <value>The host.</value>
        internal static Regex Host
        {
            get { return SipStyleUriParser._host; }
        }

        /// <summary>
        /// Gets the password.
        /// </summary>
        /// <value>The password.</value>
        internal static Regex Password
        {
            get { return SipStyleUriParser._password; }
        }

        /// <summary>
        /// Gets the port.
        /// </summary>
        /// <value>The port.</value>
        internal static Regex Port
        {
            get { return SipStyleUriParser._port; }
        }

        /// <summary>
        /// Gets the scheme.
        /// </summary>
        /// <value>The scheme.</value>
        internal static Regex Scheme
        {
            get { return SipStyleUriParser._scheme; }
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <value>The user.</value>
        internal static Regex User
        {
            get { return SipStyleUriParser._user; }
        }

        /// <summary>
        /// Gets the internal sync object.
        /// </summary>
        /// <value>The internal sync object.</value>
        private static object InternalSyncObject
        {
            get
                {
                if(s_InternalSyncObject == null)
                    {
                    object obj2 = new object();
                    System.Threading.Interlocked.CompareExchange(ref s_InternalSyncObject, obj2, null);
                    }
                return s_InternalSyncObject;
                }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Konnetic.Sip.SipStyleUriParser"/> class.
        /// </summary>
        public SipStyleUriParser()
            : base(Options.NoFragment | Options.GenericAuthority | Options.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Konnetic.Sip.SipStyleUriParser"/> class.
        /// </summary>
        /// <remarks>This constructor initializes the <see cref="T:System.GenericUriParserOptions"/> enumeration of the base class to the passed <paramref name="options"/> argument.</remarks>
        /// <param name="options">The options for this <see cref="T:System.GenericUriParser"/>.</param>
        public SipStyleUriParser(GenericUriParserOptions options)
            : base(options)
        { 
        }

        #endregion Constructors

        #region Methods
        /// <summary>
        /// Parses the parameters and returns a collection of header fields.
        /// </summary>
        /// <param name="uri">The <see cref="T:System.String"/> containing a URI.</param>
        /// <returns>A collection of Headers</returns>        
        /// <overloads>
        /// <summary>The method has two overloads.</summary>
        /// <remarks>Overloads allow for accepting a <see cref="T:System.String"/> or <see cref="T:Konnetic.Sip.SipUri"/>.</remarks>
        /// </overloads>
        internal static void ParseHeaders(string uri, out HeaderFieldCollection headers)
            {
            headers = new HeaderFieldCollection();
            if(String.IsNullOrEmpty(uri))
            { 
                 return;
            }

            string tempstr = string.Empty;
            MatchCollection mc;
            //Ignore the Username and Password
            int usernameSeperator = uri.IndexOf('@');
            if(usernameSeperator == -1)
                {
                usernameSeperator = 0;
                }
            //Find the headers (if any)
            int headerSeperator = uri.IndexOf('?', usernameSeperator);
            if(headerSeperator == -1)
                {
                headerSeperator = uri.Length;
                }
            //Find all headers
            mc = _headers.Matches(uri, headerSeperator-1);
            if(mc.Count > 0)
                {
                foreach(Match m in mc)
                    {
                    tempstr = m.Value;
                    if(!string.IsNullOrEmpty(tempstr) || tempstr != "&")
                        {
                        headers.Add(tempstr);
                        }
                    }
                } 
            }
        /// <summary>
        /// Parses the headers.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns></returns> 
        internal static void ParseHeaders(SipUri uri, out HeaderFieldCollection headers)
            {
            ParseHeaders(uri.OriginalString, out headers);
            }

        /// <summary>
        /// Parses the parameters and returns a collection of parameters.
        /// </summary>
        /// <param name="uri">The <see cref="T:System.String"/> containing a URI.</param>
        /// <returns>A collection of SipParamaters</returns>        
        /// <overloads>
        /// <summary>The method has two overloads.</summary>
        /// <remarks>Overloads allow for accepting a <see cref="T:System.String"/> or <see cref="T:Konnetic.Sip.SipUri"/>.</remarks>
        /// </overloads>
        internal static void ParseParameters(string uri, out SipUriParameterCollection parameters)
            {
            parameters = new SipUriParameterCollection();
            if(String.IsNullOrEmpty(uri))
            { return; }

            string tempstr = string.Empty;
            MatchCollection mc;
            //Ignore the Username and Password
            int usernameSeperator = uri.IndexOf('@');
            if(usernameSeperator == -1)
                {
                usernameSeperator = 0;
                }
            //Find the headers (if any)
            int parameterSeperator = uri.IndexOf(';', usernameSeperator);
            if(parameterSeperator == -1)
                {
                parameterSeperator = uri.Length;
                }
            mc = _parameters.Matches(uri, parameterSeperator-1);
            try
                {
                if(mc.Count > 0)
                    {
                    foreach(Match m in mc)
                        {
                        tempstr = m.Value;
                        if(!string.IsNullOrEmpty(tempstr))
                            {
                            parameters.Parse(tempstr);
                            }
                        }
                    }
                }
            catch(SipFormatException ex)
                {
                throw new SipUriFormatException("Exception parsing parameters.", ex);
                } 
            }
        /// <summary>
        /// Parses the parameters.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns></returns> 
        internal static void ParseParameters(SipUri uri, out SipUriParameterCollection parameters)
            {
            ParseParameters(uri.OriginalString, out parameters);
            }

        /// <summary>
        /// Gets the components from a <see cref="T:Konnetic.Sip.SipUri"/>.
        /// </summary>
        /// <param name="uri">The <see cref="T:Konnetic.Sip.SipUri"/> to parse.</param>
        /// <param name="components">The <see cref="T:Konnetic.Sip.SipUriComponents"/> to retrieve from the URI.</param>
        /// <returns>A string that contains the components.</returns>
        /// <exception cref="T:System.ArgumentNullException">Thrown when the <paramref name="uri"/> property is null.</exception>
        /// <overloads>
        /// <summary>The method has three overload.</summary>
        /// <remarks>Overloads allow for accepting a <see cref="T:System.String"/>, <see cref="T:Konnetic.Sip.SipUri"/> or <see cref="T:System.Uri"/>.</remarks>
        /// </overloads>
        public string GetComponents(SipUri uri, SipUriComponents components)
            {
            return GetComponents(uri.OriginalString, components);
            }
        /// <summary>
        /// Gets the components from a <see cref="T:Konnetic.Sip.SipUri"/>.
        /// </summary>
        /// <param name="uri">The <see cref="T:Konnetic.Sip.SipUri"/> to parse.</param>
        /// <param name="components">The <see cref="T:Konnetic.Sip.SipUriComponents"/> to retrieve from the URI.</param>
        /// <returns>A string that contains the components.</returns>
        /// <exception cref="T:System.ArgumentNullException">Thrown when the <paramref name="uri"/> property is null.</exception>
        public string GetComponents(string uri, SipUriComponents components)
            {
            PropertyVerifier.ThrowOnNullArgument(uri, "uri");

            string t = "";

            UriFormat defaultFormat = UriFormat.Unescaped;
            switch(components)
                {

                case SipUriComponents.Fragment:
                    return string.Empty;

                case SipUriComponents.AbsoluteUri:
                    goto case SipUriComponents.SerializationInfoString;

                case SipUriComponents.Host | SipUriComponents.Port:
                    goto case SipUriComponents.HostPort;

                case SipUriComponents.StrongPort:
                    goto case SipUriComponents.Port;
                     
                case SipUriComponents.Parameters:
                    goto case SipUriComponents.Path;

                case SipUriComponents.Headers:
                    goto case SipUriComponents.Query;   

                case SipUriComponents.HttpRequestUrl:
                    return this.GetComponents(uri, SipUriComponents.SchemeAndServer) +
                        GetComponents(uri, SipUriComponents.PathAndQuery);

                case SipUriComponents.HostPort:
                    return (GetComponents(uri, SipUriComponents.Host) + ":" +
                        GetComponents(uri, SipUriComponents.Port)).TrimEnd(new char[] { ':' });
                    
                case SipUriComponents.SipRequestUrl:
                    return GetComponents(uri, SipUriComponents.SchemeAndServer) +
                        GetComponents(uri, SipUriComponents.PathAndQuery);

                case SipUriComponents.PathAndQuery:
                    return GetComponents(uri, SipUriComponents.Path | SipUriComponents.KeepDelimiter) +
                        GetComponents(uri, SipUriComponents.Query | SipUriComponents.KeepDelimiter);

                case SipUriComponents.SchemeAndServer:
                    return GetComponents(uri, SipUriComponents.Scheme) + ":" +
                        GetComponents(uri, SipUriComponents.HostPort);

                case SipUriComponents.SerializationInfoString:
                    return GetComponents(uri, SipUriComponents.Scheme) + ":" +
                        GetComponents(uri, SipUriComponents.UserInfo ) + "@" +
                        GetComponents(uri, SipUriComponents.HostPort) +
                        GetComponents(uri, SipUriComponents.PathAndQuery);

                case SipUriComponents.StrongAuthority:
                    return this.GetComponents(uri, SipUriComponents.UserInfo) +
                        ":" + this.GetComponents(uri, SipUriComponents.StrongPort);

                case SipUriComponents.Path | SipUriComponents.KeepDelimiter:
                    t = GetComponents(uri, SipUriComponents.Path);
                    if(string.IsNullOrEmpty(t))
                        return string.Empty;
                    return t;

                case SipUriComponents.Query | SipUriComponents.KeepDelimiter:
                    t = GetComponents(uri, SipUriComponents.Query);
                    if(string.IsNullOrEmpty(t))
                        return string.Empty;
                    return t;

                case SipUriComponents.Host: 
                    int i = uri.GetHashCode();
                    if(LastHostValue.Hash != i)
                        {
                        lock(InternalSyncObject)
                            {
                            if(LastHostValue.Hash != i)
                                {
                                LastHostValue.Hash = i;
                                uri = _userinfoReplace.Replace(uri, "");
                                t = _host.Match(uri).Value;
                                if(t != null)
                                    {
                                    LastHostValue.Result = t;
                                    }
                                else
                                    {
                                    LastHostValue.Result = string.Empty;
                                    }
                                }
                            }
                        }
                    return LastHostValue.Result;

                case SipUriComponents.Path:
                    //Ignore the Username and Password
                    int usernameSeperator = uri.IndexOf('@');
                    if(usernameSeperator == -1)
                        {
                        usernameSeperator = 0;
                        }
                    //Find the headers (if any)
                    int parameterSeperator = uri.IndexOf(';', usernameSeperator);
                    if(parameterSeperator == -1)
                        {
                        parameterSeperator = uri.Length;
                        }
                    MatchCollection mc = _parameters.Matches(uri, parameterSeperator);
                    t = "";
                    if(mc.Count > 0)
                        {
                        foreach(Match m in mc)
                            if(!string.IsNullOrEmpty(m.Value))
                                {
                                t = t + ";" + m.Value;
                                }
                        if(t.Length > 0)
                            return Uri.UnescapeDataString(t);
                        }
                    return string.Empty;

                case SipUriComponents.Query:
                    t = "";
                    //Ignore the Username and Password
                    usernameSeperator = uri.IndexOf('@');
                    if(usernameSeperator == -1)
                        {
                        usernameSeperator = 0;
                        }
                    //Ignore the Username and Password
                    int headerSeperator = uri.IndexOf('?', usernameSeperator);
                    if(headerSeperator == -1)
                        {
                        headerSeperator = uri.Length;
                        }
                    //Include leading ? for Regex
                    headerSeperator--;
                    //Find all headers
                    mc = _headers.Matches(uri, headerSeperator);
                    if(mc.Count > 0)
                        {
                        t = "?";
                        foreach(Match m in mc)
                            if(!string.IsNullOrEmpty(m.Value))
                                {
                                if(t != "?")
                                    {
                                    t = t + "&";
                                    }
                                t = t + m.Value;
                                }
                        return Uri.UnescapeDataString(t);
                        }
                    return string.Empty;
                case SipUriComponents.Port:
                    t = _port.Match(uri).Value;
                    if(string.IsNullOrEmpty(t))
                        return string.Empty; //_defaultPort;
                    else
                        return t;
                     
                case SipUriComponents.Scheme:
                    t = _scheme.Match(uri).Value;
                    if(string.IsNullOrEmpty(t))
                        return string.Empty;
                    else
                        return t;

                case SipUriComponents.Password:
                    t = _password.Match(uri).Value;
                    if(string.IsNullOrEmpty(t))
                        return string.Empty;
                    else
                        return t;

                case SipUriComponents.UserName:
                    t = _user.Match(uri).Value;
                    if(string.IsNullOrEmpty(t))
                        return string.Empty;
                    else
                        return t;

                case SipUriComponents.UserInfo:
                    t = GetComponents(uri, SipUriComponents.UserName);
                    if(!string.IsNullOrEmpty(t))
                        {
                        string tPassword = GetComponents(uri, SipUriComponents.Password);

                        if(!string.IsNullOrEmpty(tPassword))
                            {
                            t = t + ":" + tPassword;
                            }
                        // t = t + "@";
                        }
                    return t;
 
                case SipUriComponents.MulticastAddress:
                    t = _maddr.Match(uri).Value;
                    if(string.IsNullOrEmpty(t))
                        return string.Empty;
                    else
                        return t;

                case SipUriComponents.TimeToLive:
                    t = _ttl.Match(uri).Value;
                    if(string.IsNullOrEmpty(t))
                        return string.Empty;
                    else
                        return t;

                case SipUriComponents.Transport:
                    t = _transport.Match(uri).Value;
                    if(string.IsNullOrEmpty(t))
                        return string.Empty;
                    else
                        return t;

                case SipUriComponents.LooseRouter:
                    t = _lr.Match(uri).Value;
                    if(string.IsNullOrEmpty(t))
                        return string.Empty;
                    else
                        return t;

                case SipUriComponents.Method:
                    t = _method.Match(uri).Value;
                    if(string.IsNullOrEmpty(t))
                        return string.Empty;
                    else
                        return t;

                case SipUriComponents.UserParameter:
                    t = _userparam.Match(uri).Value;
                    if(string.IsNullOrEmpty(t))
                        return string.Empty;
                    else
                        return t;

                default:
                    return string.Empty;
                }
            }

        /// <summary>
        /// Gets the components from a URI.
        /// </summary>
        /// <param name="uri">The URI to parse.</param>
        /// <param name="components">The <see cref="T:System.UriComponents"/> to retrieve from <paramref name="uri"/>.</param>
        /// <param name="format">One of the <see cref="T:System.UriFormat"/> values that controls how special characters are escaped.</param>
        /// <returns>A string that contains the components.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// 	<paramref name="uriFormat"/> is invalid.
        /// - or -
        /// <paramref name="uriComponents"/> is not a combination of valid <see cref="T:System.UriComponents"/> values.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// 	<paramref name="uri"/> is not an absolute URI. Relative URIs cannot be used with this method.
        /// </exception>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override string GetComponents(Uri uri, UriComponents components, UriFormat format)
        {
   

            switch(components)
                {
                case UriComponents.Host | UriComponents.Port:
                    return GetComponents(uri.OriginalString, SipUriComponents.Host | SipUriComponents.Port);

                case UriComponents.AbsoluteUri:
                    return GetComponents(uri.OriginalString, SipUriComponents.AbsoluteUri);

                case UriComponents.Fragment:
                    return GetComponents(uri.OriginalString, SipUriComponents.Fragment);

                case UriComponents.Host:
                    return GetComponents(uri.OriginalString, SipUriComponents.Host);

                case UriComponents.HostAndPort:
                    return GetComponents(uri.OriginalString, SipUriComponents.HostPort);

                case UriComponents.HttpRequestUrl:
                    return GetComponents(uri.OriginalString, SipUriComponents.HttpRequestUrl);

                case UriComponents.Path:
                    return GetComponents(uri.OriginalString, SipUriComponents.Path);

                case UriComponents.Path | UriComponents.KeepDelimiter:
                    return GetComponents(uri.OriginalString, SipUriComponents.Path | SipUriComponents.KeepDelimiter);

                case UriComponents.PathAndQuery:
                    return GetComponents(uri.OriginalString, SipUriComponents.PathAndQuery);

                case UriComponents.Port:
                    return GetComponents(uri.OriginalString, SipUriComponents.Port);

                case UriComponents.Query:
                    return GetComponents(uri.OriginalString, SipUriComponents.Query);

                case UriComponents.Query | UriComponents.KeepDelimiter:
                    return GetComponents(uri.OriginalString, SipUriComponents.Query | SipUriComponents.KeepDelimiter);

                case UriComponents.Scheme:
                    return GetComponents(uri.OriginalString, SipUriComponents.Scheme);

                case UriComponents.SchemeAndServer:
                    return GetComponents(uri.OriginalString, SipUriComponents.SchemeAndServer);

                case UriComponents.SerializationInfoString:
                    return GetComponents(uri.OriginalString, SipUriComponents.SerializationInfoString);

                case UriComponents.StrongAuthority:
                    return GetComponents(uri.OriginalString, SipUriComponents.StrongAuthority);

                case UriComponents.StrongPort:
                    return GetComponents(uri.OriginalString, SipUriComponents.StrongPort);

                case UriComponents.UserInfo:
                    return GetComponents(uri.OriginalString, SipUriComponents.UserInfo);

                default:
                    return string.Empty;
                }
        }

        /// <summary>
        /// Initialize the state of the parser and validate the URI.
        /// </summary>
        /// <param name="uri">The <see cref="T:System.Uri"/> to validate.</param>
        /// <param name="parsingError">Validation errors, if any.</param>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override void InitializeAndValidate(Uri uri, out UriFormatException parsingError)
        {
            parsingError = null;
        }

        /// <summary>
        /// Invoked by the Framework when a <see cref="T:System.UriParser"/> method is registered.
        /// </summary>
        /// <param name="schemeName">The scheme that is associated with this <see cref="T:System.UriParser"/>.</param>
        /// <param name="defaultPort">The port number of the scheme.</param>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override void OnRegister(string schemeName, int defaultPort)
        {
		PropertyVerifier.ThrowOnNullOrEmptyString(schemeName, "schemeName");
		PropertyVerifier.ThrowIfIntOutOfRange(defaultPort,0, 65535,"defaultPort");

            string s = schemeName.ToUpperInvariant();
			if(s == "SIP" || s == "SIPS")
				{
				this._defaultPort = defaultPort;
				}
			else
				{
				throw new SipException("Unrecognised Scheme. Must be SIP or SIPS.");
				}
        }
  
        #endregion Methods

        #region Nested Types

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Auto)]
        private struct LastValue
        {
            #region Fields

            public int Hash;
            public string Result;

            #endregion Fields
        }

        /*
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        private class UriComparer : IEqualityComparer<string>
            {
            #region Methods

            [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
            public bool Equals(string x, string y)
                {
                return Uri.UnescapeDataString(x).Equals(Uri.UnescapeDataString(y), StringComparison.InvariantCultureIgnoreCase);
                }

            public int GetHashCode(string obj)
                {
                string s = obj as string;
                return s.ToUpperInvariant().GetHashCode();
                }

            #endregion Methods
            }
        */
        #endregion Nested Types
    }
}