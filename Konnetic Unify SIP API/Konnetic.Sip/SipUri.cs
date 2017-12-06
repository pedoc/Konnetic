/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Permissions;
using System.Runtime.Serialization;

using Konnetic.Sip.Headers;

namespace Konnetic.Sip
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public   class SipUri : Uri
    {
        #region Fields

        internal const string DEFAULTURI = "sip:localhost:5060";

        private SipStyleUriParser _parser;
        //private SipUriBuilder _builder;
        private bool _uriSet; 

        #endregion Fields

        #region Properties

        public string AbsoluteUri
            {
            get { return GetSipComponents(SipUriComponents.AbsoluteUri); }
            }
        public string SchemeAndServer
            {
            get { return GetSipComponents(SipUriComponents.); }
            }
        public string UserInfo
            {
            get { return GetSipComponents(SipUriComponents.UserInfo); }
            }
        public string UserAtHost
            {
            get {     
            StringBuilder sb = new StringBuilder();
            if (UserInfo != null)
            {
            sb.Append(this.UserInfo);
            sb.Append("@");
            }
            if (IsIPv6)
            {
            sb.Append("[");
            sb.Append(this.Host);
            sb.Append("]");
            }
            else
            {
            sb.Append(this.Host);
            }
            return sb.ToString();
 
        }
   
 
            }

        public string Authority
            {
            get { return GetSipComponents(SipUriComponents.StrongAuthority); }
            }
        public string Query
            {
            get { return GetSipComponents(SipUriComponents.Query); }
            }
        public string Path
            {
            get { return GetSipComponents(SipUriComponents.Path); }
            }
        public string PathAndQuery
            {
            get { return GetSipComponents(SipUriComponents.PathAndQuery); }
            }
        public string Headers
        {
            get { return GetSipComponents(SipUriComponents.Headers); }
        }

        public string HostPort
        {
            get { return GetSipComponents(SipUriComponents.HostPort); }
        }
        public bool IsIPv6
            {
            [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
            get { return Host.Contains(":"); }
            }

        public bool IsSecureTransport
            {
            [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
            get {
            return GetSipComponents(SipUriComponents.Scheme).ToUpperInvariant() == "SIPS" ? true : false;
            }
        }

        public bool LooseRouter
            {
            [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
            get { return GetSipComponents(SipUriComponents.LooseRouter).ToUpperInvariant() == "LR" ? true : false; }
        }

        public string Method
        {
            get { return GetSipComponents(SipUriComponents.Method); }
        }
         
        public string MulticastAddress
        {
            get { return GetSipComponents(SipUriComponents.MulticastAddress); }
        }

        public string Parameters
        {
            get { return GetSipComponents(SipUriComponents.Parameters); }
        }

        public string Password
        {
            get { return GetSipComponents(SipUriComponents.Password); }
        }

        public bool IsPortSet
            {
            [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
            get { return GetSipComponents(SipUriComponents.Port).Length>0; }
        }

        public Byte TimeToLive
        {
            get {
            Byte retVal = 0;
            string sTTL = string.Empty;
            try
            {
            sTTL = GetSipComponents(SipUriComponents.TimeToLive);
            if(string.IsNullOrEmpty(sTTL))
                {
                retVal = 0;
                }
            else
                {
                retVal = Byte.Parse(sTTL, CultureInfo.InvariantCulture);
                }
            }
            catch(ArgumentNullException)
            {
            retVal = 0;
            }
            catch(OverflowException ex)
                {
                throw new SipOutOfRangeException("Time To Live", SR.GetString(SR.OutOfRangeException, sTTL, "Time To Live", 0, int.MaxValue), ex); 
            }
            catch(FormatException)
            {
            throw new SipUriFormatException("Time To Live value is invalid. Check it is between 0 and 255.");
            }
            return retVal;
            }
        }

        public string Transport
        {
            get { return GetSipComponents(SipUriComponents.Transport); }
        }

        public bool IsUriSet
            {
            [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
            get { return _uriSet; }
            set { _uriSet = value; }
        }

        /// <summary>
        /// Gets or sets the User.
        /// </summary>
        /// <value>The User.</value>
        public string UserName
        {
            get { return GetSipComponents(SipUriComponents.UserName); }
        }

        public string UserParameter
        {
            get { return GetSipComponents(SipUriComponents.UserParameter); }
        }

        private SipStyleUriParser Parser
        {
            get { return _parser; }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SipUri"/> class.
        /// </summary>
        /// <param name="uri">The SIP URI.</param>
        public SipUri(string uri)
            : this(uri, string.Empty)
        {
        }

        public SipUri(string uri, string domain)
            : base(uri)
        {
            _parser = new SipStyleUriParser();
            //TODO pass domain to Parser or change it in the builder 
            SipUriParameterCollection pCol;
            SipStyleUriParser.ParseParameters(this, out pCol);
            for(int i = 0; i <= pCol.Count - 1; i++)
                {
                ValidateParameter(pCol[i]);
                }

            HeaderFieldCollection hCol;
            SipStyleUriParser.ParseHeaders(this, out hCol); 

            _uriSet = true;
            if(String.IsNullOrEmpty(Host))
                {
                throw new UriFormatException("Host is mandatory.");
                }
        }

        /// <summary>
        /// Validates the parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void ValidateParameter(SipParameter parameter)
            {
            string value = parameter.Value.Trim();
            switch(parameter.Name.ToUpperInvariant())
                {
                case "TTL":
                    try
                        {
                        byte.Parse(value);
                        }
                    catch(FormatException ex)
                        {
                        throw new SipUriFormatException("Time-to-live must be a numeric.", ex);
                        }
                    catch(OverflowException ex)
                        {
                        throw new SipUriFormatException("Time-to-live must be between zero and 255.", ex);
                        }
                    break;
                case "MADDR":
                    if(String.IsNullOrEmpty(value))
                        {
                        throw new SipUriFormatException("'maddr' parameter cannot be empty.");
                        }
                    PropertyVerifier.ThrowUriExceptionOnInvalidHostString(value, "Maddr");
                    break;
                case "LR":
                    if(value.Length > 2)
                        {
                        throw new SipUriFormatException("'LR' parameter must be valueless.");
                        } 
                    break;
                case "METHOD":
                    if(String.IsNullOrEmpty(value))
                        {
                        throw new SipUriFormatException("'method' parameter cannot be empty.");
                        }
                    break;
                case "USER":
                    if(String.IsNullOrEmpty(value))
                        {
                        throw new SipUriFormatException("'user' parameter cannot be empty.");
                        }
                    PropertyVerifier.ThrowUriExceptionOnInvalidToken(value, "UserParameter");
                    break;
                case "TRANSPORT":
                    if(String.IsNullOrEmpty(value))
                        {
                        throw new SipUriFormatException("'transport' parameter cannot be empty.");
                        }
                    PropertyVerifier.ThrowUriExceptionOnInvalidToken(value, "Transport");
                    break;
                default: 
                    break;
                } 
            }


        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
            {
            info.AddValue("UriString", ToString());
            }         
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SipUri"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param> 
        internal SipUri(Uri uri)
            : this(uri.OriginalString)
        {
        }

        #endregion Constructors

        #region Methods
 
        [ 
        EditorBrowsable(EditorBrowsableState.Advanced)]
        public static new bool Equals(Object target, Object other)
        {
			SipUri uri1 = target as SipUri;
			SipUri uri2 = other as SipUri;

            Match m1 = SipStyleUriParser.Scheme.Match(uri1.OriginalString);
            Match m2 = SipStyleUriParser.Scheme.Match(uri2.OriginalString);

            //Match Scheme
            if(!m1.Value.Equals(m2.Value))
                {
                return false;
                }

            //Match username (case sensitive)
            m1 = SipStyleUriParser.User.Match(uri1.OriginalString);
            m2 = SipStyleUriParser.User.Match(uri2.OriginalString);

            string s1 = Uri.UnescapeDataString(m1.Value);
            string s2 = Uri.UnescapeDataString(m2.Value);
            if(!s1.Equals(s2))
                return false;

            //password (case sensitive)
            m1 = SipStyleUriParser.Password.Match(uri1.OriginalString);
            m2 = SipStyleUriParser.Password.Match(uri2.OriginalString);

            s1 = Uri.UnescapeDataString(m1.Value);
            s2 = Uri.UnescapeDataString(m2.Value);
            if(!s1.Equals(s2))
                return false;

            //Match host + port (case insensitive)
            m1 = SipStyleUriParser.Host.Match(uri1.OriginalString);
            m2 = SipStyleUriParser.Host.Match(uri2.OriginalString);
            s1 = Uri.UnescapeDataString(m1.Value);
            s2 = Uri.UnescapeDataString(m2.Value);
            if(!s1.Equals(s2, StringComparison.OrdinalIgnoreCase))
                return false;

            m1 = SipStyleUriParser.Port.Match(uri1.OriginalString);
            m2 = SipStyleUriParser.Port.Match(uri2.OriginalString);
            s1 = Uri.UnescapeDataString(m1.Value);
            s2 = Uri.UnescapeDataString(m2.Value);
            if(!s1.Equals(s2, StringComparison.OrdinalIgnoreCase))
                return false;
            HeaderFieldCollection d1;
                SipStyleUriParser.ParseHeaders(uri1, out d1);
            HeaderFieldCollection d2;
                SipStyleUriParser.ParseHeaders(uri2, out d2);

            ArrayList checkedParams = new ArrayList();

            foreach(HeaderFieldBase hf in d1)
                {
                string hfName = Uri.UnescapeDataString(hf.FieldName);
                checkedParams.Add(hfName);
                HeaderFieldBase hfOther = d2[hfName];
                if(!CompareHeaderFields(hf, hfOther))
                    {
                    return false;
                    }
                }

            //check the other way for missing keys
            foreach(HeaderFieldBase hf in d2)
                {
                if(!checkedParams.Contains(hf.FieldName))
                    {
                    string hfName = Uri.UnescapeDataString(hf.FieldName);
                    HeaderFieldBase hfOther = d1[hfName];
                    if(!CompareHeaderFields(hf, hfOther))
                        {
                        return false;
                        }
                    }
                }

            SipUriParameterCollection d3;
            SipStyleUriParser.ParseParameters(uri1, out d3);
            SipUriParameterCollection d4;
            SipStyleUriParser.ParseParameters(uri2, out d4);

            checkedParams.Clear();

            foreach(SipParameter sp in d3)
                {
                string paramName = Uri.UnescapeDataString(sp.Name);
                checkedParams.Add(sp.Name);
                SipParameter spOther = d4[paramName];
                if(!CompareParameters(sp, spOther))
                    {
                    return false;
                    }
                }

            foreach(SipParameter sp in d4)   //check the other way for missing keys
                {
                if(!checkedParams.Contains(sp.Name))
                    {
                    string paramName = Uri.UnescapeDataString(sp.Name);
                    SipParameter spOther = d3[paramName];
                    if(!CompareParameters(sp, spOther))
                        {
                        return false;
                        }
                    }
                }
            return true;
        }

        /// <summary>
        /// Creates a new SipUri.
        /// </summary>
        /// <param name="input">The SIP URI input.</param>
        /// <returns>A valid and populated SIP Uri</returns>
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public static bool TryParse(string uri, out SipUri sipUri)
            {
            sipUri = null;
            if((uri == null) || (uri.Length == 0))
                {
                return false;
                }

            try
            {
                sipUri = new SipUri(uri);
            } 
			catch(Exception)
                { 
                return false;
				}
            return true;
            ;
        }

        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public override bool Equals(object obj)
        {
            if(obj == null)
                {
                return false;
                }

            SipUri p = obj as SipUri;
            if((object)p == null)
                {
                return false;
                }

            return this.Equals(p);
        }

        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public bool Equals(SipUri comparand)
        {
            return SipUri.Equals(comparand,this);
        }

        public string GetSipComponents(SipUriComponents components)
        {
            if(components == SipUriComponents.Headers)
                {
                return System.Uri.UnescapeDataString(Parser.GetComponents(this, components));
                }
            else
                {
                return Parser.GetComponents(this, components);
                }
        }

        public override string ToString()
        {
            string hostSeperator = "@";
            string userAndPortSeperator = ":";

            if(String.IsNullOrEmpty(UserName) && Password.Length > 0)
                {
                return string.Empty;
                //throw new UriFormatException("Illegal to have password and no username.");
                }
            if(String.IsNullOrEmpty(Host) && Port > 0)
                {
                return string.Empty;
                //throw new UriFormatException("Illegal to have a port and no host.");
                }
            string s = Scheme.Trim();
            string str = (s.Length != 0) ? (s + ":") : string.Empty;
            string pwd = Password;
            string usr = UserName;
            int portNum = Port;
            string[] strArray = new string[]
            {	str,
                usr,
                (pwd.Length > 0) ? (userAndPortSeperator + pwd) : string.Empty,
                (usr.Length > 0) ? hostSeperator : string.Empty,
                Host,
                (IsPortSet && portNum>0) ? (":" + portNum.ToString(CultureInfo.InvariantCulture)) : string.Empty,
                Parameters,
                (Headers.Length>0) ? Headers : string.Empty};
            return System.Uri.EscapeUriString(string.Concat(strArray));
        }

        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        private static bool CompareHeaderFields(HeaderFieldBase hf, HeaderFieldBase other)
        {
            if(hf != null)
                {
                if(other != null)
                    {
                    return hf.Equals(other);
                    }
                }
            return false;
        }

        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        private static bool CompareParameters(SipParameter sp, SipParameter other)
        {
            string paramName = Uri.UnescapeDataString(sp.Name).ToUpperInvariant();

            if((object)other == null)
                {
                if((paramName == "USER" || paramName == "TTL" || paramName == "METHOD" || paramName == "MADDR"))
                    {
                    return false;
                    }
                else
                    {
                    if(paramName == "TRANSPORT")
                        {
                        return false;
                        }
                    else
                        {
                        return true;
                        }
                    }
                }
            else
                {
                return sp.Equals(other);
                }
        }

        #endregion Methods
    }
}