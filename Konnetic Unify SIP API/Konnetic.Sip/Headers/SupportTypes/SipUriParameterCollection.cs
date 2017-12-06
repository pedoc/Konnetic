/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;

namespace Konnetic.Sip.Headers
    {

    public class SipUriParameterCollection : Collection<SipParameter>, IEnumerable<SipParameter>
        {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private string _seperator;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="T:Konnetic.Sip.Headers.SipParameter"/> collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> for the <see cref="T:Konnetic.Sip.Headers.SipParameter"/> collection instance.
        /// </returns>
        /// <threadsafety static="true" instance="false" />
        //public override IEnumerator GetEnumerator()
        //    {
        //    return this.InnerList.GetEnumerator();
        //    }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="T:Konnetic.Sip.Headers.SipParameter"/> collection
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> for the <see cref="T:Konnetic.Sip.Headers.SipParameter"/> collection.
        /// </returns>
        /// <threadsafety static="true" instance="false" />
        IEnumerator IEnumerable.GetEnumerator()
            {
            return this.GetEnumerator();
            }
        /// <summary>
        /// Copies the contents of the collection to the <paramref name="parameters"/> <see cref="T:Konnetic.Sip.Headers.SipParameterCollection"/>.
        /// </summary>
        /// <param name="parameters">A <see cref="T:Konnetic.Sip.Headers.SipParameterCollection"/> to populate.</param>
        /// <threadsafety static="true" instance="false" />
        public void CopyTo(SipParameterCollection parameters)
            {
            PropertyVerifier.ThrowOnNullArgument(parameters, "parameters");
            if(base.Count > 0)
                {
                for(int i = 0; i < base.Count; i++)
                    {
                    parameters.Add(this[i].Clone());
                    }
                }
            }
        /// <summary>
        /// Copies the contents of the collection to the <paramref name="parameters"/>
        /// 	<see cref="T:System.Array"/>.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection"/>. The <see cref="T:System.Array"/> must have zero-based indexing.</param>
        /// <param name="index">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        /// <threadsafety static="true" instance="false"/>
        public void CopyTo(Array array, int index)
            {
            PropertyVerifier.ThrowOnNullArgument(array, "array");
            if(array.Length <= index)
                {
                throw new SipOutOfRangeException("Index cannot be larger than array size");
                }
            if(base.Count > 0)
                {
                for(int i = 0; i < base.Count; i++)
                    {
                    if(((IList)array).Count >= index - 1)
                        {
                        break;
                        }
                    ((IList)array)[index] = this[i].Clone();
                    index++;
                    }
                }
            }
        //public int Count
        //{
        //    get { return this.Count; }
        //}

        /// <summary>
        /// Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection"/> is synchronized (thread safe).
        /// </summary>
        /// <value></value>
        /// <returns>true if access to the <see cref="T:System.Collections.ICollection"/> is synchronized (thread safe); otherwise, false.
        /// </returns>
        public bool IsSynchronized
            {
            get { return false; }
            }


        /// <summary>
        /// Gets the seperator between parameters.
        /// </summary>
        /// <value>The seperator between parameters.</value>
        internal string Seperator
            {
            get { return _seperator; }
            }

        #endregion Properties

        #region Events

        /// <summary>
        /// Occurs when the collection is altered (add, update or delete).
        /// </summary>
        public event EventHandler OnChange;

        #endregion Events

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SipParameterCollection"/> class.
        /// </summary>
        /// <remarks>The default constructor.</remarks>
        /// <overloads>
        /// <summary>The method has two overloads.</summary>
        /// </overloads>
        public SipUriParameterCollection()
            {
            string s = new string(SR.ParameterPrefix,1); 
            if(string.IsNullOrEmpty(s))
                {
                s = ";";
                }
            _seperator = s;
            }

        /// <summary>
        /// Initializes a new instance of the <see cref="SipParameterCollection"/> class.
        /// </summary>
        /// <param name="seperator">The seperator.</param>
        public SipUriParameterCollection(string seperator)
            : this()
            {
            _seperator = seperator;
            }

        /// <summary>
        /// Initializes a new instance of the <see cref="SipParameterCollection"/> class.
        /// </summary>
        /// <param name="parameter">Initialises and populates the collection with the first <see cref="T:Konnetic.Sip.Headers.SipParameter"/>.</param>
        public SipUriParameterCollection(SipParameter parameter)
            : this()
            {
            PropertyVerifier.ThrowOnNullArgument(parameter, "parameter");
            this.Add(parameter);
            }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Adds a new parameter to the <see cref="T:Konnetic.Sip.Headers.SipParameterCollection"/>.
        /// </summary>
        /// <remarks><note type="caution">No parameter validation is conducted on the name or value.<note></remarks>
        /// <param name="name">The parameter name.</param>
        /// <param name="value">The parameter value.</param>
        /// <exception cref="ArgumentNullException">Is raised when <b>name</b> is null or empty.</exception>
        /// <exception cref="ArgumentException">Is raised when the parameter with specified name 
        /// already exists in the collection.</exception>
        /// <threadsafety static="true" instance="false" />
        public int Add(string name, string value)
            {
            //NULL is ValueLessParam
            PropertyVerifier.ThrowOnNullOrEmptyString(name, "name");
            if(this.Contains(name))
                {
                throw new SipFormatException(string.Format(CultureInfo.InvariantCulture, "Parameter '{0}' already exists in the collection.", name));
                }
            return this.Add(new SipParameter(name, value));
            }

        /// <summary>
        /// Adds the specified parameter to the <see cref="T:Konnetic.Sip.Headers.SipParameterCollection"/>.
        /// </summary>
        /// <param name="parameter">The parameter to add.</param>
        /// <returns></returns>
        /// <threadsafety static="true" instance="false" />
        public int Add(SipParameter parameter)
            {
            PropertyVerifier.ThrowOnNullArgument(parameter, "parameter");
            if(Contains(parameter))
                {
                throw new SipDuplicateItemException(string.Format(CultureInfo.InvariantCulture, "{0}, duplicate Parameters are illegal", parameter.Name));
                }
            if(OnChange != null)
                {
                OnChange(this, new EventArgs());
                }
            parameter.Name = parameter.Name.ToLowerInvariant();
            base.Add(parameter);
            return Count-1;
            }

        /// <summary>
        /// Clears this instance of all SipParameters.
        /// </summary>
        /// <threadsafety static="true" instance="false"/>
        public void Clear()
            {
            if(OnChange != null)
                {
                OnChange(this, new EventArgs());
                }
            base.Clear();
            }
        /// <summary>
        /// Creates a deep copy of this instance.
        /// </summary> 
        /// <remarks>Creates and returns a deep copy of the Header. This method ensures a deep copy of the HeaderField, when a message is cloned the HeaderField can be modified without effecting the original HeaderField in the message.</remarks>
        /// <returns>A deep copy of <see cref="T:Konnetic.Sip.Headers.SipParameterCollection"/>.</returns>
        /// <threadsafety static="true" instance="false" />
        public SipParameterCollection Clone()
            {
            SipParameterCollection newParams = new SipParameterCollection(_seperator);
            foreach(SipParameter p in this)
                {
                newParams.Add(p.Clone());
                }
            return newParams;
            }

        /// <summary>
        /// Checks if the collection contains a parameter with the specified name.
        /// </summary>
        /// <param name="name">The parameter name.</param>        
        /// <returns>
        /// 	<c>true</c> if the collection contains the parameter specified; otherwise, <c>false</c>.
        /// </returns>
        /// <threadsafety static="true" instance="false" />
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public bool Contains(string name)
            {
            PropertyVerifier.ThrowOnNullArgument(name, "name");
            return this[name] != null;
            }

        /// <summary>
        /// Checks if the collection contains a parameter.
        /// </summary>
        /// <param name="parameter">The parameter to match against.</param>
        /// <returns>
        /// 	<c>true</c> if the collection contains the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        /// <threadsafety static="true" instance="false"/>
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public bool Contains(SipParameter parameter)
            {
            PropertyVerifier.ThrowOnNullArgument(parameter, "parameter");
            return this[parameter.Name] != null;
            }

        /// <summary>
        /// Compare this <see cref="T:Konnetic.Sip.Headers.SipParameterCollection"/> for equality with another <see cref="T:Konnetic.Sip.Headers.SipParameterCollection"/> object.</summary>
        /// <remarks>
        /// All SipParameters in each collection must match, as well as the collection counts. All parameters are compared using object equality that is each parameter in the collection is used for comparision. Unless otherwise stated in the definition of a particular parameter, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are case-sensitive. 
        /// </remarks>
        /// <param name="other">The <see cref="T:Konnetic.Sip.Headers.SipParameterCollection"/> to compare to this instance.</param>
        /// <returns><c>true</c> if <paramref name="other" /> is an instance of this class representing the same SIPParameter collection as this, <c>false</c> otherwise.</returns>    
        /// <overloads>
        /// <summary>This method is overloaded.</summary>
        /// <remarks>Overloads allow for equality against <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/> and <see cref="T:System.Object"/>.</remarks>
        /// </overloads>  
        /// <threadsafety static="true" instance="false" />
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public bool Equals(SipParameterCollection other)
            {
            // If parameter is null return false:
            if((object)other == null)
                {
                return false;
                }

            if(other.Count != this.Count)
                {
                return false;
                }

            ArrayList checkedParams = new ArrayList();
            checkedParams.Clear();
            foreach(SipParameter sp in this)
                {
                string paramName = Uri.UnescapeDataString(sp.Name);
                checkedParams.Add(sp.Name);
                SipParameter spOther = other[paramName];
                if(!sp.Equals(spOther))
                    {
                    return false;
                    }
                }

            foreach(SipParameter sp in other)   //check the other way for missing keys
                {
                if(!checkedParams.Contains(sp.Name))
                    {
                    string paramName = Uri.UnescapeDataString(sp.Name);
                    SipParameter spOther = this[paramName];
                    if(!sp.Equals(spOther))
                        {
                        return false;
                        }
                    }
                }
            return true;
            }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// 	<c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>This method overrides the <c>equals</c> method in <see cref="System.Object"/>.
        /// All SipParameters in each collection must match, as well as the collection counts. All parameters are compared using object equality that is each parameter in the collection is used for comparision. Unless otherwise stated in the definition of a particular parameter, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are case-sensitive. 
        /// </remarks>
        /// <threadsafety static="true" instance="false"/>
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public override bool Equals(object obj)
            {
            // If parameter is null return false.
            if(obj == null)
                {
                return false;
                }
            // If parameter cannot be cast to HeaderParameter return false:
            SipParameterCollection p = obj as SipParameterCollection;
            if((object)p == null)
                {
                return false;
                }

            // Return true if the fields match:
            return this.Equals(p);
            }

        /// <summary>
        /// Inserts the parameter name\value at the index.
        /// </summary>
        /// <param name="index">The index position to insert the new parameter.</param>
        /// <param name="name">The parameter name.</param>
        /// <param name="value">The parameter value.</param>
        /// <threadsafety static="true" instance="false"/>
        /// <exception cref="ArgumentNullException">Is raised when <paramref value="name"/> is null.</exception>
        /// <exception cref="SipException">Is raised when the addition of the parameter would cause a duplicate.</exception>
        public void Insert(int index, string name, string value)
            {
            //NULL is ValueLessParam
            PropertyVerifier.ThrowOnNullOrEmptyString(name, "name");
            if(this.Contains(name))
                {
                throw new SipException(string.Format(CultureInfo.InvariantCulture, "Parameter '{0}' already exists in the collection !", name));
                }
            if(OnChange != null)
                {
                OnChange(this, new EventArgs());
                }
            base.Insert(index, new SipParameter(name, value));
            }

        /// <summary>
        /// Parses string representation of the Parameter.
        /// </summary>
        /// <remarks> 
        /// <b>RFC 3261 Syntax:</b> 
        /// <table> 
        /// <tr><td style="border-bottom:none" colspan="2">field-name: field-value *(;parameter-name=parameter-value)</td></tr> 
        /// </table> 
        /// <para/> 
        /// <example>
        /// <list type="bullet"> 
        /// <item>Contact: "Mr. Watson" &lt;sip:watson@worcester.bell-telephone.com&gt;;q=0.7; expires=3600</item> 
        /// </list>
        /// </example>
        /// <param name="value">The parameter string to parse.</param>
        /// <exception cref="T:Konnetic.Sip.SipParseException">Thrown when an invalid (non-standard) value is encountered.</exception>
        /// <exception cref="T:Konnetic.Sip.ArgumentException">Thrown when the <paramref name="nameValuePair"/> is not formatted correctly as parameter-name[=parameter-value].</exception>
        /// <threadsafety static="true" instance="false" />
        public void Parse(string nameValuePair)
            {

            if(nameValuePair != null)
                {
                string[] newParam = nameValuePair.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);

                if(newParam.Length == 1)
                    {
                    Add(newParam[0], null);
                    }
                else if(newParam.Length == 2)
                    {
                    Add(newParam[0], newParam[1]);
                    }
                else
                    {
                    throw new ArgumentException("Invalid argument format use [name](=[value]).");
                    }
                }
            }

        /// <summary>
        /// Removes the specified parameter from the collection.
        /// </summary>
        /// <param name="name">The SipParameter name.</param>
        /// <threadsafety static="true" instance="false" />
        public void Remove(string name)
            {
            PropertyVerifier.ThrowOnNullArgument(name, "name");
            SipParameter para = this[name];
            if(para != null)
                {
                this.Remove(para);
                if(OnChange != null)
                    {
                    OnChange(this, new EventArgs());
                    }
                }
            }

        /// <summary>
        /// Removes the specified parameter from the collection.
        /// </summary>
        /// <param name="parameter">The parameter to remove.</param>
        /// <threadsafety static="true" instance="false"/>
        public void Remove(SipParameter parameter)
            {
            PropertyVerifier.ThrowOnNullArgument(parameter, "parameter");

            if(OnChange != null)
                {
                OnChange(this, new EventArgs());
                }
            base.Remove(parameter);
            }

        /// <summary>
        /// Adds or updates a parameter with a specified name/value.
        /// </summary>
        /// <param name="index">The index position to insert the new parameter.</param>
        /// <param name="name">The parameter name.</param>
        /// <param name="value">The parameter value.</param>
        /// <threadsafety static="true" instance="false"/>
        public void Set(int index, string name, string value)
            {
            PropertyVerifier.ThrowOnNullOrEmptyString(name, "name");
            if(Contains(name))
                {
                if((object)value == null)
                    {
                    this[name].Value = string.Empty;
                    this[name].ValuelessParameter = true;
                    if(OnChange != null)
                        {
                        OnChange(this, new EventArgs());
                        }
                    }
                else
                    {
                    this[name].Value = value;
                    this[name].ValuelessParameter = false;
                    if(OnChange != null)
                        {
                        OnChange(this, new EventArgs());
                        }
                    }
                }
            else
                {
                Insert(index, name, value);
                }
            }

        /// <summary>
        /// Adds or updates a parameter with a specified name/value.
        /// </summary>
        /// <param name="name">The parameter name.</param>
        /// <param name="value">The parameter value.</param>
        /// <threadsafety static="true" instance="false" />
        public void Set(string name, string value)
            {
            PropertyVerifier.ThrowOnNullOrEmptyString(name, "name");
            if(Contains(name))
                {
                if((object)value == null)
                    {
                    this[name].Value = string.Empty;
                    this[name].ValuelessParameter = true;
                    if(OnChange != null)
                        {
                        OnChange(this, new EventArgs());
                        }
                    }
                else
                    {
                    this[name].Value = value;
                    this[name].ValuelessParameter = false;
                    if(OnChange != null)
                        {
                        OnChange(this, new EventArgs());
                        }
                    }
                }
            else
                {
                Add(name, value);
                }
            }

        /// <summary>
        /// Adds or updates the specified parameter to the collection.
        /// </summary>
        /// <param name="parameter">The parameter to add or update.</param>
        /// <threadsafety static="true" instance="false"/>
        public void Set(SipParameter parameter)
            {
            PropertyVerifier.ThrowOnNullArgument(parameter, "parameter");
            if(Contains(parameter))
                {
                this[parameter.Name].Value = parameter.Value;
                if(OnChange != null)
                    {
                    OnChange(this, new EventArgs());
                    }
                }
            else
                {
                Add(parameter);
                }
            }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        /// <threadsafety static="true" instance="false"/>
        public override string ToString()
            {
            //TODO Return a valid string
            return ToString(new string[] { "" });
            }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance in a format suitable for URIs.
        /// </summary>        
        /// <remarks>The string is escaped.</remarks>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.</returns>
        /// <threadsafety static="true" instance="false" />
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings")]
        public string ToUriString()
            {
            //TODO Return a valid string
            return Uri.EscapeUriString(ToString(new string[] { "" }));
            }
        /// <summary>
        /// Validates this instance against the standard. Indicated whether it reaches minimum compliance.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if instance represents a valid parameter collection; otherwise, <c>false</c>.
        /// </returns> 
        /// <threadsafety static="true" instance="false"/>
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public bool IsValid()
            {
            bool retVal = true;
            foreach(SipParameter sp in this)
                {
                if(!sp.IsValid())
                    {
                    return false;
                    }
                }
            return retVal;
            }

        /// <summary>
        /// Returns the collection as a SIP URI string.
        /// </summary>
        /// <param name="exclude">The excluded parameters.</param>
        /// <returns>A string representation of the collection as a SIP URI.</returns>
        /// <exception cref="ArgumentNullException">Is raised when <b>exclude</b> is null.</exception>
        /// <threadsafety static="true" instance="false" />
        internal string ToString(string[] exclude)
            {
            PropertyVerifier.ThrowOnNullArgument(exclude, "exclude");
            bool drop;
            StringBuilder sb = new StringBuilder(100);
            foreach(SipParameter para in this)
                {
                drop = false;
                if(exclude.Length > 0)
                    {
                    foreach(string s in exclude)
                        {
                        if(para.Name.Equals(s, StringComparison.OrdinalIgnoreCase))
                            {
                            drop = true;
                            break;
                            }
                        }
                    }

                if(!drop)
                    {
                    bool first = true;
                    string s = para.ToString();
                    if(!string.IsNullOrEmpty(s))
                        {
                        if(!first)
                            {
                            sb.Append(SR.SingleWhiteSpace);
                            }
                        else { first = false; }
                        sb.Append(_seperator);
                        sb.Append(para.ToString());
                        }
                    }
                }

            return sb.ToString();
            }

        #endregion Methods

        #region Indexers

        /// <summary>
        /// Gets or sets the <see cref="Konnetic.Sip.Headers.SipParameter"/> at the specified index.
        /// </summary>
        /// <remarks>Returns null if parameter with specified name doesn't exist.</remarks>
        /// <value>The <see cref="Konnetic.Sip.Headers.SipParameter"/> at the <param name="index"/> index in the collection.</value>
        /// <threadsafety static="true" instance="false"/>
        public new SipParameter this[Int32 index]
            {
            get
                {
                if(base.Count == 0)
                    {
                    return null;
                    }

                return base[index] as SipParameter;
                }
            private set
                {
                if(base.Count > 0)
                    {
                    base[index] = value;
                    if(OnChange != null)
                        {
                        OnChange(this, new EventArgs());
                        }
                    }
                }
            }

        /// <summary>
        /// Gets specified parameter from collection. 
        /// </summary>
        /// <remarks>Returns null if parameter with specified name doesn't exist.</remarks>
        /// <param name="parameterName">The parameter name.</param>
        /// <returns>Returns parameter with specified name or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Is raised when <paramref value="fieldName"/> is null.</exception>
        /// <threadsafety static="true" instance="false" />
        public SipParameter this[string parameterName]
            {
            get
                {
                PropertyVerifier.ThrowOnNullArgument(parameterName, "parameterName");
                return this[parameterName, StringComparison.OrdinalIgnoreCase];
                }
            private set
                {
                PropertyVerifier.ThrowOnNullArgument(parameterName, "parameterName");
                this[parameterName, StringComparison.OrdinalIgnoreCase] = value;
                if(OnChange != null)
                    {
                    OnChange(this, new EventArgs());
                    }
                }
            }

        /// <summary>
        /// Gets specified parameter from collection. 
        /// </summary>
        /// <remarks>Returns null if parameter with specified name doesn't exist.</remarks>
        /// <param name="parameterName">Parameter name.</param>
        /// <param name="comparisonType">The string comparision type.</param>
        /// <returns>Returns parameter with specified name or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Is raised when <paramref value="parameterName"/> is null.</exception>
        /// <threadsafety static="true" instance="false" />
        internal SipParameter this[string parameterName, StringComparison comparisonType]
            {
            get
                {
                PropertyVerifier.ThrowOnNullArgument(parameterName, "parameterName");
                for(int i = 0; i < this.Count; i++)
                    {
                    SipParameter retVal = this[i];
                    if(retVal.Name.Equals(parameterName, comparisonType))
                        {
                        return retVal;
                        }
                    }

                return null;
                }
            private set
                {
                PropertyVerifier.ThrowOnNullArgument(parameterName, "parameterName");
                for(int i = 0; i < this.Count; i++)
                    {
                    SipParameter retVal = this[i];
                    if(retVal.Name.Equals(parameterName, comparisonType))
                        {
                        this[i] = value;
                        if(OnChange != null)
                            {
                            OnChange(this, new EventArgs());
                            }
                        }
                    }

                }
            }




        #endregion Indexers




        }
    }