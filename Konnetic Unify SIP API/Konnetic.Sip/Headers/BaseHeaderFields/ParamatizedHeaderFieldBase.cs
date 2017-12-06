/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;

namespace Konnetic.Sip.Headers
{
    /// <summary> 
    /// The <see cref="T:Konnetic.Sip.Headers.ParamatizedHeaderFieldBase"/> provides parameters for HeaderFields.
    /// </summary>
    /// <remarks>
    /// <b>Standards: RFC3261, RFC2616</b>
    /// <para/>
    /// Many existing header fields will adhere to the general form of a value followed by a semi-colon/comma/single-space separated sequence of parameter-name, parameter-value pairs:
    /// <para/>
    /// <note type="caution"/>The Konnetic SIP API treats Server Values, Authentication and Authorisation parameters in the same way as typical header and generic parameters, except the seperators are different. This is a slight departure in terminology from the standard.</note>
    /// <para>When comparing header fields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular header field, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are casesensitive.</para>
    /// <note type="implementnotes">The Konnetic SIP API converts all parameter names to lower-case. Although not explicitly stated in the standard, it is implicitly taken to be the general form from the extensive examples.</note>
    /// <para/>The abstract base class is used by <see cref="T:Konnetic.Sip.Headers.AbsoluteUriHeaderFieldBase"/>, <see cref="T:Konnetic.Sip.Headers.MediaTypeHeaderFieldBase"/>, <see cref="T:Konnetic.Sip.Headers.QValueHeaderFieldBase"/>, <see cref="T:Konnetic.Sip.Headers.SecurityHeaderFieldBase"/>, <see cref="T:Konnetic.Sip.Headers.SipUriHeaderFieldBase"/>, <see cref="T:Konnetic.Sip.Headers.ContentDispositionHeaderField"/>, <see cref="T:Konnetic.Sip.Headers.RetryAfterHeaderField"/> and <see cref="T:Konnetic.Sip.Headers.ViaHeaderField"/> headers.  
    /// <b>RFC 3261 Syntax:</b> 
    /// <table > 
    /// <tr><td style="border-bottom:none" colspan="2">field-name: field-value *(;parameter-name=parameter-value)</td></tr>
    /// <tr><td style="border-bottom:none">generic-param = </td><td style="border-bottom:none">token [ EQUAL gen-value ]</td></tr>
    /// <tr><td style="border-bottom:none">gen-value = </td><td style="border-bottom:none">token / host / quoted-string</td></tr>
    /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr>
    /// <tr><td style="border-bottom:none">quoted-string = </td><td style="border-bottom:none">DOUBLE_QUOTE *(qdtext / quoted-pair ) DOUBLE_QUOTE</td></tr>
    /// <tr><td style="border-bottom:none">qdtext = </td><td style="border-bottom:none">LWS / %x21 / %x23-5B / %x5D-7E / UTF8-NONASCII</td></tr>
    /// <tr><td style="border-bottom:none">quoted-pair = </td><td style="border-bottom:none">"\\" (%x00-09 / %x0B-0C / %x0E-7F)</td></tr>
    /// <tr><td style="border-bottom:none">host = </td><td style="border-bottom:none">hostname / IPv4address / IPv6reference </td></tr>
    /// </table>
    /// <example>
    /// <list type="bullet">
    /// <item>c: text/html; charset=ISO-8859-4</item> 
    /// <item>From: sip:+12125551212@server.phone2net.com;tag=887s</item> 
    /// <item>Proxy-Authenticate: Digest realm="atlanta.com", domain="sip:ss1.carrier.com", qop="auth", nonce="f84f1cec41e6cbe5aea9c8e88d359", opaque="", stale=FALSE, algorithm=MD5</item> 
    /// <item>Server: HomeServer v2</item> 
    /// </list> 
    /// </example>
    /// </remarks> 
    public abstract class ParamatizedHeaderFieldBase : HeaderFieldBase
    {
        #region Fields
    /// <summary>
    /// Indicate whether the type should include a leading seperator in output.
    /// </summary>
    private bool _includeLeadingSeperatorInOutput = true;
    /// <summary>
    /// 
    /// </summary>
    private bool _allowGenericParams = true;

    /// <summary>
    /// Gets or sets a value indicating whether the type allow generic parameters.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if the type allows generic parameters; otherwise, <c>false</c>.
    /// </value>
    public bool AllowGenericParameters
        {
        get { return _allowGenericParams; }
        set { _allowGenericParams = value; }
        }



    /// <summary>
    /// 
    /// </summary>
    private List<string> _knownHeaderRegister; 



        /// <summary>
        /// Represents the field's parameter.
        /// </summary>
        private SipParameterCollection _genericParameters; 
        /// <summary>
        /// Represents the field's parameter.
        /// </summary>
        private SipParameterCollection _headerParameters;
        #endregion Fields

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether the leading seperator is included.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if we include the leading seperator; otherwise, <c>false</c>.
        /// </value>
        protected bool IncludeLeadingSeperatorInOutput
            {
            get { return _includeLeadingSeperatorInOutput; }
            set { _includeLeadingSeperatorInOutput = value; }
            }

        /// <summary>
        /// Gets a value indicating whether this instance has a parameter.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has a parameter; otherwise, <c>false</c>.
		/// </value> 
        public bool HasParameters
        {
            get { return ((_genericParameters.Count+_headerParameters.Count) > 0); } 
        }
        /// <summary>
        /// Gets or sets the entity parameters.
        /// </summary> 
        /// <value>A <see cref="HeaderParameters"/> field parameter.</value>
        protected SipParameterCollection HeaderParameters
            {
            get { return _headerParameters; } 
            }
        /// <summary>
        /// Gets the generic parameters.
        /// </summary>
        /// <value>A <see cref="InternalGenericParameters"/> field parameter.</value>
        protected ReadOnlyCollection<SipParameter> InternalGenericParameters
            {
            get { return new ReadOnlyCollection< SipParameter>(_genericParameters); } 
            }

        #endregion Properties

        #region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ParamatizedHeaderFieldBase"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <overloads>
		/// <summary>The method has two overloads.</summary>
		/// </overloads>
        protected ParamatizedHeaderFieldBase()
            : this(";")
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ParamatizedHeaderFieldBase"/> class.
        /// </summary>
        /// <param name="seperator">The seperator.</param>
        protected ParamatizedHeaderFieldBase(string seperator)
            : base()
        {
            PropertyVerifier.ThrowOnNullArgument(seperator, "seperator");
            _knownHeaderRegister = new List<string>();            
            _genericParameters = new SipParameterCollection(seperator);
            _headerParameters = new SipParameterCollection(seperator);
            _allowGenericParams = true;
        }

 
        #endregion Constructors

        #region Methods


        private bool HeaderParamsContain(string paramname)
            {
            return null != _knownHeaderRegister.Find(delegate(string str)
            {
                return str.Equals(paramname.ToUpperInvariant());
            });
            }
        ///<summary>
        /// Compare this SIP Header for equality with another <see cref="T:Konnetic.Sip.Headers.ParamatizedHeaderFieldBase"/> object.</summary>
        /// <remarks>
        /// All optional headers are compared using object equality that is each field in the header is used for comparision. When comparing HeaderFields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular HeaderField, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are case-sensitive. 
        /// </remarks>
        /// <param name="other">The <see cref="T:Konnetic.Sip.Headers.ParamatizedHeaderFieldBase"/> to compare to this instance.</param>
        /// <returns><c>true</c> if <paramref name="other" /> is an instance of this class representing the same SIP HeaderField as this, <c>false</c> otherwise.</returns>    
        /// <overloads>
        /// <summary>This method is overloaded.</summary>
        /// <remarks>Overloads allow for equality against <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>, <see cref="T:System.Object"/> and another instance of this HeaderField.</remarks>
        /// </overloads>  
        /// <threadsafety static="true" instance="false" />
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public bool Equals(ParamatizedHeaderFieldBase other)
        {
            if((object)other == null)
                {
                return false;
                }
            if(other._headerParameters.Count != this._headerParameters.Count)
                {
                return false;
                }
            if(other._genericParameters.Count != this._genericParameters.Count)
                {
                return false;
                }
            return base.Equals((HeaderFieldBase)other) && other._headerParameters.Equals(this._headerParameters) && other._genericParameters.Equals(this._genericParameters); 
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
            ParamatizedHeaderFieldBase p = obj as ParamatizedHeaderFieldBase;
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
            bool retVal = base.IsValid();
            if(HasParameters)
                {
                retVal = retVal && _headerParameters.IsValid() && _genericParameters.IsValid();  
                }
            return retVal;
            }
        /// <summary>
        /// Parses string representation of the HeaderField.
        /// </summary>
        /// <remarks>  
        /// <b>RFC 3261 Syntax:</b> 
        /// <table > 
        /// <tr><td style="border-bottom:none">generic-param = </td><td style="border-bottom:none">token [ EQUAL gen-value ]</td></tr>
        /// <tr><td style="border-bottom:none">gen-value = </td><td style="border-bottom:none">token / host / quoted-string</td></tr>
        /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr>
        /// <tr><td style="border-bottom:none">quoted-string = </td><td style="border-bottom:none">DOUBLE_QUOTE *(qdtext / quoted-pair ) DOUBLE_QUOTE</td></tr>
        /// <tr><td style="border-bottom:none">qdtext = </td><td style="border-bottom:none">LWS / %x21 / %x23-5B / %x5D-7E / UTF8-NONASCII</td></tr>
        /// <tr><td style="border-bottom:none">quoted-pair = </td><td style="border-bottom:none">"\\" (%x00-09 / %x0B-0C / %x0E-7F)</td></tr>
        /// <tr><td style="border-bottom:none">host = </td><td style="border-bottom:none">hostname / IPv4address / IPv6reference </td></tr>
        /// </remarks>
        /// <param name="value">The HeaderField string to parse.</param>
        /// <exception cref="T:Konnetic.Sip.SipParseException">Thrown when an invalid (non-standard) value is encountered.</exception>
        /// <exception cref="T:Konnetic.Sip.SipException">Thrown when a processing exception is encountered.</exception>
        /// <threadsafety static="true" instance="false" />
        public override void Parse(string value)
        {
            if(value != null)
                {
                _headerParameters.Clear();
                _genericParameters.Clear();
                if(!string.IsNullOrEmpty(value.Trim()))
                    {

                    /*

                    Regex _params = new Regex(_parameters.Seperator.Trim()+@"[\w-.!%_*+`'~]+(\s*=\s*([\w-.!%_*+`':~]+|(""(.|\n)*?([^\\]""|\\""))))*", RegexOptions.Compiled | RegexOptions.IgnoreCase );

                    MatchCollection mc = _params.Matches(value);

                    if(mc.Count > 0)
                        {
                        //Start at one as zero should be headerfield value
                        for(int i = 1; i < mc.Count; i++)
                            {
                            string s = mc[i].Value.Trim();
                            if(!string.IsNullOrEmpty(s))
                                {
                                SipParameter sp = new SipParameter(s);
                                Parameters.Set(sp);
                                }
                            }
                        }
                     */

                    if(_allowGenericParams)
                        {
                    Regex _param = new Regex(_genericParameters.Seperator.Trim(), RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    string[] lines = _param.Split(value);
                    if(lines.Length > 0)
                        {
                        //Start at one as zero should be headerfield value
                        for(int i = 1; i < lines.Length; i++)
                            {   
                            string s = lines[i].Trim();
                            if(!string.IsNullOrEmpty(s))
                                {
								try
									{
									SipParameter sp = new SipParameter(s);

                                    if(!HeaderParamsContain(sp.Name))
                                        {
                                        if(_allowGenericParams)
                                            {
                                            _genericParameters.Add(sp);
                                            }
                                        }
									}
							catch(SipException ex)
								{
								throw new SipParseException("SipParameter", SR.ParseExceptionMessage(value), ex);
								}
							catch(Exception ex)
                                    {
                                    throw new SipParseException(SR.GetString(SR.GeneralParseException, value, "SipParameter"), ex);   
								}
                                }
                            }
                        }

                        }
                    //Regex _firstParam = new Regex(@"^\s*[\w\-.!%*_+`'~]+=(.|\n)*[;\s$]", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    //Match m = _firstParam.Match(value);
                    //if(m != null)
                    //    {
                    //    if(!string.IsNullOrEmpty(m.Value))
                    //        {
                    //        SipParameter sp = new SipParameter(m.Value);
                    //        Parameters.Set(sp);
                    //        }
                    //    }
                    }
                }
        }

        /// <summary>
        /// Gets a string representation of the HeaderField value. The HeaderField name is excluded.
        /// </summary>
        /// <remarks>This method overrides the <c>GetStringValue</c> method in <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>. </remarks>
        /// <returns>String representation of the HeaderField value</returns>
        /// <threadsafety static="true" instance="false" />
        public sealed override string GetStringValue()
            {
            //return string.Empty;
            string s = string.Empty;
            if(_headerParameters.Count>0)
                {
                s += _headerParameters.ToString();
                }

            if(_genericParameters.Count > 0)
                {
                s += _genericParameters.ToString();
                } 
            if(string.IsNullOrEmpty(s))
                {
                return GetStringValueNoParams().TrimEnd();
                }

            if(!IncludeLeadingSeperatorInOutput)
                {
                //Remove a Seperator (comma, semi etc) from start as the Parameters. 
                s = s.TrimStart(_headerParameters.Seperator.ToCharArray());
                }
            return GetStringValueNoParams() + s;  
        }

        /// <summary>
        /// Gets the string value no params.
        /// </summary>
        /// <returns></returns>
        protected virtual string GetStringValueNoParams()
            {
            return string.Empty;
            }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public sealed override int GetHashCode()
            {
            return (FieldName + GetStringValueNoParams()).GetHashCode();
            }
        /// <summary>
        /// Copies the contents of the collection to the <paramref name="parameters"/> of the <see cref="T:Konnetic.Sip.Headers.ParamatizedHeaderFieldBase"/>.
        /// </summary>
        /// <param name="parameters">A <see cref="T:Konnetic.Sip.Headers.ParamatizedHeaderFieldBase"/> to populate.</param>
        /// <threadsafety static="true" instance="false" />
        protected void CopyParametersTo(ParamatizedHeaderFieldBase parameters)
            {
            this._genericParameters.CopyTo(parameters._genericParameters);
            this._headerParameters.CopyTo(parameters._headerParameters);
            }

        /// <summary>
        /// Unregisters a name parameter.
        /// </summary>
        /// <remarks>Known parameters are those parameters specified in the specification. They are seperate from Generic parameters which are unknown but allowed. Known parameters are rendered first in any output.</remarks>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="parameterName"/>.</exception>
        /// <exception cref="ArgumentException">Thrown on empty <paramref name="parameterName"/>.</exception>
        /// <threadsafety static="true" instance="false" />
        protected void UnRegisterKnownParameter(string parameterName)
            {
            PropertyVerifier.ThrowOnNullOrEmptyString(parameterName, "parameterName");
            if(HeaderParamsContain(parameterName))
                {
                _knownHeaderRegister.Remove(parameterName.ToUpperInvariant());
                }
            }

        /// <summary>
        /// Registers a parameter name as a known header parameter.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <remarks>Known parameters are those parameters specified in the specification. They are seperate from Generic parameters which are unknown but allowed. Known parameters are rendered first in any output. Derived types should register known parameters before adding generic parameters.</remarks>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="parameterName"/>.</exception>
        /// <exception cref="ArgumentException">Thrown on empty <paramref name="parameterName"/>.</exception>
        /// <threadsafety static="true" instance="false" />
        protected void RegisterKnownParameter(string parameterName)
            {
            PropertyVerifier.ThrowOnNullOrEmptyString(parameterName, "parameterName"); 
            if(!HeaderParamsContain(parameterName))
                {
                _knownHeaderRegister.Add(parameterName.ToUpperInvariant());
                }
            }

        /// <summary>
        /// Removes the parameter (header or generic) from the parameter collection.
        /// </summary>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <remarks>Known parameters are those parameters specified in the specification. They are seperate from Generic parameters which are unknown but allowed. Known parameters are rendered first in any output.</remarks>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="parameterName"/>.</exception>
        /// <exception cref="ArgumentException">Thrown on empty <paramref name="parameterName"/>.</exception>
        /// <threadsafety static="true" instance="false" />
        public void RemoveParameter(string parameterName)
            {
            PropertyVerifier.ThrowOnNullOrEmptyString(parameterName, "parameterName");
            if(HeaderParamsContain(parameterName))
                {
                _headerParameters.Remove(parameterName);
                }
            else
                {
                _genericParameters.Remove(parameterName);
                }
            }

        /// <summary>
        /// Removes the parameter (header or generic) from the parameter collection.
        /// </summary>
        /// <remarks>Known parameters are those parameters specified in the specification. They are seperate from Generic parameters which are unknown but allowed. Known parameters are rendered first in any output.</remarks>
        /// <param name="parameter">The parameter to remove.</param>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="parameter"/>.</exception>
        /// <exception cref="SipDuplicateItemException">Thrown when attempting to remove a Generic parameter that matches a registered known parameter.</exception>
        /// <threadsafety static="true" instance="false" />
        public void RemoveParameter(SipParameter parameter)
            {
            PropertyVerifier.ThrowOnNullArgument(parameter, "parameter");
            if(HeaderParamsContain(parameter.Name))
                {
#if DEBUG
                if(_genericParameters.Contains(parameter))
                    {
                    //Catch design time error for derived types
                    throw new SipDuplicateItemException(parameter.Name,"Duplicate parameters are not allowed.");
                    }
#endif
                _headerParameters.Remove(parameter);
                }
            else
                {
                _genericParameters.Remove(parameter);
                }
            }

        /// <summary>
        /// Clears the header and generic parameters.
        /// </summary>
        public void ClearParameters()
            {
            _headerParameters.Clear();
            _genericParameters.Clear();
            }

        /// <summary>
        /// Adds a generic parameter at the specified index.
        /// </summary>
        /// <remarks>Known parameters are those parameters specified in the specification. They are seperate from Generic parameters which are unknown but allowed. Known parameters are rendered first in any output.</remarks>
        /// <param name="index">The index where to insert the new parameter.</param>
        /// <param name="name">The parameter name.</param>
        /// <param name="value">The parameter value.</param> 
    /// <b>RFC 3261 Syntax:</b> 
    /// <table > 
    /// <tr><td style="border-bottom:none">generic-param = </td><td style="border-bottom:none">token [ EQUAL gen-value ]</td></tr>
        /// <tr><td style="border-bottom:none">gen-value = </td><td style="border-bottom:none">token / host / quoted-string</td></tr>
        /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr>
        /// <tr><td style="border-bottom:none">quoted-string = </td><td style="border-bottom:none">DOUBLE_QUOTE *(qdtext / quoted-pair ) DOUBLE_QUOTE</td></tr>
        /// <tr><td style="border-bottom:none">qdtext = </td><td style="border-bottom:none">LWS / %x21 / %x23-5B / %x5D-7E / UTF8-NONASCII</td></tr>
        /// <tr><td style="border-bottom:none">quoted-pair = </td><td style="border-bottom:none">"\\" (%x00-09 / %x0B-0C / %x0E-7F)</td></tr>
        /// <tr><td style="border-bottom:none">host = </td><td style="border-bottom:none">hostname / IPv4address / IPv6reference </td></tr>
    /// </table>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="name"/>.</exception>
        /// <exception cref="ArgumentException">Thrown on empty <paramref name="name"/>.</exception>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="value"/>.</exception>
        /// <exception cref="ArgumentException">Thrown on empty <paramref name="value"/>.</exception>
        /// <exception cref="SipException">Is raised when <see cref="P:Konnetic.Sip.Headers.ParamatizedHeaderFieldBase.AllowGenericParameters"/> is set to false.</exception>
        /// <exception cref="SipOutOfRangeException">Is raised when <paramref value="index"/> is beyond the collection's bounds.</exception>
        /// <threadsafety static="true" instance="false" />
        protected void InternalAddGenericParameter(int index, string name, string value)
            {
            PropertyVerifier.ThrowOnNullOrEmptyString(value, "value");
            PropertyVerifier.ThrowOnNullOrEmptyString(name, "name");
            PropertyVerifier.ThrowIfIntOutOfRange(index, 0, _genericParameters.Count - 1, "index");
            if(!_allowGenericParams)
                {
                throw new SipException("Generic parameters are not allowed for this header field.");
                }
            if(HeaderParamsContain(name))
                {
                throw new SipDuplicateItemException(name,SR.GetString(SR.DuplicateHeaderParameter, name));
                }
                _genericParameters.Set(index, name, value);
            }

        /// <summary>
        /// Adds a generic parameter.
        /// </summary>
        /// <remarks>Known parameters are those parameters specified in the specification. They are seperate from Generic parameters which are unknown but allowed. Known parameters are rendered first in any output.</remarks>
        /// <param name="name">The parameter name.</param>
        /// <param name="value">The parameter value.</param>
        /// <b>RFC 3261 Syntax:</b> 
        /// <table > 
        /// <tr><td style="border-bottom:none">generic-param = </td><td style="border-bottom:none">token [ EQUAL gen-value ]</td></tr>
        /// <tr><td style="border-bottom:none">gen-value = </td><td style="border-bottom:none">token / host / quoted-string</td></tr>
        /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr>
        /// <tr><td style="border-bottom:none">quoted-string = </td><td style="border-bottom:none">DOUBLE_QUOTE *(qdtext / quoted-pair ) DOUBLE_QUOTE</td></tr>
        /// <tr><td style="border-bottom:none">qdtext = </td><td style="border-bottom:none">LWS / %x21 / %x23-5B / %x5D-7E / UTF8-NONASCII</td></tr>
        /// <tr><td style="border-bottom:none">quoted-pair = </td><td style="border-bottom:none">"\\" (%x00-09 / %x0B-0C / %x0E-7F)</td></tr>
        /// <tr><td style="border-bottom:none">host = </td><td style="border-bottom:none">hostname / IPv4address / IPv6reference </td></tr>
        /// </table>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="name"/>.</exception>
        /// <exception cref="ArgumentException">Thrown on empty <paramref name="name"/>.</exception>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="value"/>.</exception>
        /// <exception cref="ArgumentException">Thrown on empty <paramref name="value"/>.</exception>
        /// <exception cref="SipException">Is raised when <see cref="P:Konnetic.Sip.Headers.ParamatizedHeaderFieldBase.AllowGenericParameters"/> is set to false.</exception>
        /// <threadsafety static="true" instance="false" />
        protected void InternalAddGenericParameter(string name, string value)
            {
            value = value.Trim();
            PropertyVerifier.ThrowOnNullOrEmptyString(value, "value");
            PropertyVerifier.ThrowOnNullOrEmptyString(name, "name");
            if(!_allowGenericParams)
                {
                throw new SipException("Generic parameters are not allowed for this header field.");
                }
            if(HeaderParamsContain(name))
                {
                throw new SipDuplicateItemException(name,SR.GetString(SR.DuplicateHeaderParameter, name));
                } 
                _genericParameters.Set(name, value); 
            }

        /// <summary>
        /// Adds a generic parameter.
        /// </summary>
        /// <param name="parameter">The parameter to add.</param>
        /// <remarks>Known parameters are those parameters specified in the specification. They are seperate from Generic parameters which are unknown but allowed. Known parameters are rendered first in any output.</remarks>
        /// <b>RFC 3261 Syntax:</b> 
        /// <table > 
        /// <tr><td style="border-bottom:none">generic-param = </td><td style="border-bottom:none">token [ EQUAL gen-value ]</td></tr>
        /// <tr><td style="border-bottom:none">gen-value = </td><td style="border-bottom:none">token / host / quoted-string</td></tr>
        /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr>
        /// <tr><td style="border-bottom:none">quoted-string = </td><td style="border-bottom:none">DOUBLE_QUOTE *(qdtext / quoted-pair ) DOUBLE_QUOTE</td></tr>
        /// <tr><td style="border-bottom:none">qdtext = </td><td style="border-bottom:none">LWS / %x21 / %x23-5B / %x5D-7E / UTF8-NONASCII</td></tr>
        /// <tr><td style="border-bottom:none">quoted-pair = </td><td style="border-bottom:none">"\\" (%x00-09 / %x0B-0C / %x0E-7F)</td></tr>
        /// <tr><td style="border-bottom:none">host = </td><td style="border-bottom:none">hostname / IPv4address / IPv6reference </td></tr>
        /// </table> 
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="parameter"/>.</exception>
        /// <exception cref="SipDuplicateItemException">Thrown when attempting to add a Generic parameter that matches a registered known parameter.</exception>
        /// <threadsafety static="true" instance="false" />
        protected void InternalAddGenericParameter(SipParameter parameter)
            {
            PropertyVerifier.ThrowOnNullArgument(parameter, "parameter");
            if(!_allowGenericParams)
                {
                throw new SipException("Generic parameters are not allowed for this header field.");
                }
            if(HeaderParamsContain(parameter.Name))
                {
                throw new SipDuplicateItemException(parameter.Name,SR.GetString(SR.DuplicateHeaderParameter, parameter.Name));
                } 
                _genericParameters.Set(parameter); 
            }

        /// <summary>
        /// Adds a generic parameter at the index specified.
        /// </summary>
        /// <param name="parameter">The parameter to remove.</param>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="parameter"/>.</exception>
        /// <exception cref="SipDuplicateItemException">Thrown when attempting to add a Generic parameter that matches a registered known parameter.</exception>
        /// <threadsafety static="true" instance="false" />
        /// <remarks>Known parameters are those parameters specified in the specification. They are seperate from Generic parameters which are unknown but allowed. Known parameters are rendered first in any output.</remarks>
        /// <b>RFC 3261 Syntax:</b> 
        /// <table > 
        /// <tr><td style="border-bottom:none">generic-param = </td><td style="border-bottom:none">token [ EQUAL gen-value ]</td></tr>
        /// <tr><td style="border-bottom:none">gen-value = </td><td style="border-bottom:none">token / host / quoted-string</td></tr>
        /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr>
        /// <tr><td style="border-bottom:none">quoted-string = </td><td style="border-bottom:none">DOUBLE_QUOTE *(qdtext / quoted-pair ) DOUBLE_QUOTE</td></tr>
        /// <tr><td style="border-bottom:none">qdtext = </td><td style="border-bottom:none">LWS / %x21 / %x23-5B / %x5D-7E / UTF8-NONASCII</td></tr>
        /// <tr><td style="border-bottom:none">quoted-pair = </td><td style="border-bottom:none">"\\" (%x00-09 / %x0B-0C / %x0E-7F)</td></tr>
        /// <tr><td style="border-bottom:none">host = </td><td style="border-bottom:none">hostname / IPv4address / IPv6reference </td></tr>
        /// </table> 
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="parameter"/>.</exception>
        /// <exception cref="SipOutOfRangeException">Is raise when the <paramref name="index"/> argument is out of the collection's bounds.</exception>
        /// <exception cref="SipException">Is raised when <see cref="P:Konnetic.Sip.Headers.ParamatizedHeaderFieldBase.AllowGenericParameters"/> is set to false.</exception>
        /// <threadsafety static="true" instance="false" />
        protected void InternalAddGenericParameter(int index, SipParameter parameter)
            {
            PropertyVerifier.ThrowIfIntOutOfRange(index, 0, _genericParameters.Count-1, "index");
            PropertyVerifier.ThrowOnNullArgument(parameter,"parameter");
            if(!_allowGenericParams)
                {
                throw new SipException("Generic parameters are not allowed for this header field.");
                }
            if(HeaderParamsContain(parameter.Name))
                {
                throw new SipDuplicateItemException(parameter.Name,SR.GetString(SR.DuplicateHeaderParameter, parameter.Name));
                } 
                _genericParameters.Set(index, parameter); 
            }


        #endregion Methods
    }
}