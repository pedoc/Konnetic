/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Konnetic.Sip.Headers
{
    /// <summary>
    /// Combines multiple HeaderFields with the same name, as a comma, space or CRLF-seperated HeaderField list.
    /// </summary>        
    /// <remarks>
    /// Groups contain only one type of HeaderField. The <see cref="T:Konnetic.Sip.Headers.HeaderFieldGroup"/> behaves must like a singular HeaderField. Multiple message-header fields with the same field-name may be present in a message if and only if the entire field-value for that header field is defined as a separated list [i.e., #(values)]. It must be possible to combine the multiple header fields into one "field-name: field-value" pair, without changing the semantics of the message, by appending each subsequent field-value to the first, each separated by a SEPERATOR. The order in which header fields with the same field-name are received is therefore significant to the interpretation of the combined field value, and thus a proxy must not change the order of these field values when a message is forwarded.
    /// <para/>
    /// <note type="implementnotes"><see cref="T:Konnetic.Sip.Headers.HeaderFieldGroup"/> derives from <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/> and can be used to represent one HeaderField. This is useful for use within the <see cref="T:Konnetic.Sip.Headers.HeaderFieldCollection"/> which does not allow duplicate HeaderFields.</note>
    /// <para/>
    /// The exceptions to this rule are the WWW-Authenticate, Authorization, Proxy-Authenticate, and Proxy-Authorization header fields. Multiple header field rows with these names may be present in a message, but since their grammar does not follow the general form listed, they must not be combined into a single header field row. The <see cref="T:Konnetic.Sip.Headers.AuthHeaderFieldGroup"/> is used to combine multiple sucurity headers.
    /// <b>RFC 3261 Syntax:</b> 
    /// <table >
    /// <tr><td colspan="2" style="border-bottom:none">header = header-name HCOLON header-value *(SEPERATOR header-value)</td></tr> 
    /// </table>
    /// <para/>
    /// The SEPERATOR is typically the comma (","), but can be whitespace. 
    /// </remarks>
    /// <example>
    /// <list type="bullet">
    /// <item>Route: &lt;sip:alice@atlanta.com&gt;, &lt;sip:bob@biloxi.com&gt;, &lt;sip:carol@chicago.com&gt;</item>
    /// <item>Server: HomeServer v2</item>
    /// </list> 
    /// </example>
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    public class HeaderFieldGroup<T> : HeaderFieldBase, IEnumerable, ICollection
        where T : HeaderFieldBase, new()
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private ArrayList _headers;
        /// <summary>
        /// 
        /// </summary>
        private string _seperator;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.ICollection"/>.
        /// </summary>
        /// <value>The count.</value>
        /// <returns>
        /// The number of elements contained in the <see cref="T:System.Collections.ICollection"/>.
        /// </returns>
        public int Count
        {
            get { return Headers.Count; }
        }

        /// <summary>
        /// Copies the contents of the collection to the <paramref name="headers"/> parameter.
        /// </summary>
        /// <param name="headers">A <see cref="T:Konnetic.Sip.Headers.HeaderFieldGroup"/> to populate.</param>
		public void CopyTo(HeaderFieldGroup<T> headers)
			{
			PropertyVerifier.ThrowOnNullArgument(headers, "headers");
			if(Count > 0)
				{
				for(int i = 0; i < Count; i++)
					{
					headers.Add((T)(this[i].Clone()));
					}
				}
			}
        /// <summary>
        /// Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection"/> is synchronized (thread safe).
        /// </summary>
        /// <value>Whether access to the <see cref="T:System.Collections.ICollection"/> is synchronized.</value>
        /// <returns>true if access to the <see cref="T:System.Collections.ICollection"/> is synchronized (thread safe); otherwise, false.
		/// </returns>
		//[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public bool IsSynchronized
        {
            get { return false; }
        }

        /// <summary>
        /// Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection"/>.
        /// </summary>
        /// <value>The synchronized object.</value>
        /// <returns>
        /// An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection"/>.
        /// </returns>
        public object SyncRoot
        {
            get { return Headers.SyncRoot; }
        }

        /// <summary>
        /// Gets or sets the seperator.
        /// </summary>
        /// <value>The seperator.</value>
        internal string Seperator
        {
            get { return _seperator; }
            private set {
            if(value != null)
                {
                _seperator = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the headers.
        /// </summary>
        /// <value>The headers.</value>
        private ArrayList Headers
        {
            get { return _headers; }
            set { _headers = value; }
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
        /// Initializes a new instance of the <see cref="HeaderFieldGroup&lt;T&gt;"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <overloads>
		/// <summary>The method has one overload.</summary>
        /// </overloads>        
        /// <exception cref="SipException">Is raised when the HeaderField is marked as not allowing multiple HeaderFields.</exception>
        public HeaderFieldGroup()
            : base()
        {
            Headers = new ArrayList();
            T n = new T();
            if(!n.AllowMultiple)
                {
                throw new SipException(string.Format(CultureInfo.InvariantCulture, "HeaderField ({0}) is not allowed in a group", n.FieldName));
                }

            ThrowOnSecurityGroup();

            FieldName = n.FieldName;
            AllowMultiple = true;
            CompactName = n.CompactName;
            Seperator = new string(SR.MultiheaderFieldSuffix,1);
            if(n.AllowMultiple == false)
                {
                throw new ArgumentException("HeaderField marked as not allowing multiple values in a group");
                }
        }
 

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderFieldGroup&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="seperator">The seperator used to seperate the HeaderFields.</param>
        /// <exception cref="ArgumentNullException">Is raised when <paramref value="seperator"/> is null.</exception>
        /// <exception cref="SipException">Is raised when the HeaderField is marked as not allowing multiple HeaderFields.</exception>
        public HeaderFieldGroup(string seperator)
            : this()
        {
            PropertyVerifier.ThrowOnNullOrEmptyString(seperator, "seperator");
            Seperator = seperator;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Adds the specified HeaderField.
        /// </summary>
        /// <param name="header">The new HeaderField.</param>
        /// <returns>The position of the inserted HeaderField</returns>
        /// <exception cref="ArgumentNullException">Is raised when <paramref value="header"/> is null.</exception>
        /// <threadsafety static="true" instance="false" />
        public int Add(T header)
        {
            PropertyVerifier.ThrowOnNullArgument(header, "header");
            Headers.Add(header);
            if(OnChange != null)
                {
                OnChange(this, new EventArgs());
                }
            return Headers.Count-1;
        }

        /// <summary>
        /// Clears this instance of all HeaderFields.
        /// </summary>
        /// <threadsafety static="true" instance="false" />
        public void Clear()
        {
        Headers.Clear();
        if(OnChange != null)
            {
            OnChange(this, new EventArgs());
            }
        }

        /// <summary>
		/// Creates a deep copy of this instance.
		/// </summary> 
        /// <remarks>Creates and returns a deep copy of the HeaderField group. This method ensures a deep copy of the group, when a message is cloned the group can be modified without effecting the original group or HeaderField in the message.</remarks>
        /// <returns>A deep copy of <see cref="T:Konnetic.Sip.Headers.HeaderFieldGroup"/>.</returns>
/// <threadsafety static="true" instance="false" />
public override HeaderFieldBase Clone()
        {
            HeaderFieldGroup<T> group = new HeaderFieldGroup<T>(_seperator);
            if(Count > 0)
                {
                int i = 0;
                do
                    {
                    T b = (T)this[i].Clone();
                    group.Add(b);
                    i++;
                    } while(i < Count);
                }
            return group;
        }

        /// <summary>
        /// Checks if the collection contains a HeaderField with the specified name.
        /// </summary>
/// <param name="fieldValue"/>The HeaderField value.</param>        
        /// <returns>
        /// 	<c>true</c> if the collection contains the parameter specified; otherwise, <c>false</c>.
/// </returns>
/// <exception cref="ArgumentNullException">Is raised when <paramref value="fieldValue"/> is null.</exception>
/// <threadsafety static="true" instance="false" />
		[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public bool Contains(string fieldValue)
        {
            PropertyVerifier.ThrowOnNullArgument(fieldValue, "fieldValue");
            return this[fieldValue] != null;
        }

        /// <summary>
        /// Checks if the collection contains a HeaderField matching the one supplied.
        /// </summary>
        /// <param name="headerField">The HeaderField to match against.</param>
        /// <returns>
        /// 	<c>true</c> if the collection contains the specified HeaderField; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">Is raised when <paramref value="headerField"/> is null.</exception>
        /// <threadsafety static="true" instance="false"/>
		[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public bool Contains(T headerField)
        {
            PropertyVerifier.ThrowOnNullArgument(headerField, "headerField");
            for(int i =0; i<=Count-1;i++)
                {
                if(Headers[i].Equals(headerField))
                    {
                    return true;
                    }
                }
            return false;
        }

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.ICollection"/> to an <see cref="T:System.Array"/>, starting at a particular <see cref="T:System.Array"/> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection"/>. The <see cref="T:System.Array"/> must have zero-based indexing.</param>
        /// <param name="index">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// 	<paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// 	<paramref name="index"/> is less than zero.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        /// 	<paramref name="array"/> is multidimensional.
        /// -or-
        /// <paramref name="index"/> is equal to or greater than the length of <paramref name="array"/>.
        /// -or-
        /// The number of elements in the source <see cref="T:System.Collections.ICollection"/> is greater than the available space from <paramref name="index"/> to the end of the destination <paramref name="array"/>.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        /// The type of the source <see cref="T:System.Collections.ICollection"/> cannot be cast automatically to the type of the destination <paramref name="array"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">Is raised when <paramref value="array"/> is null.</exception>
        /// <threadsafety static="true" instance="false" />
        public void CopyTo(Array array, int index)
        {
            PropertyVerifier.ThrowOnNullArgument(array, "array");
            if(Headers.Count > 0)
                {
                for(int i = 0; i < Headers.Count; i++)
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

/// <summary>
        /// Compare this <see cref="T:Konnetic.Sip.Headers.HeaderFieldGroup<T>"/> for equality with another <see cref="T:Konnetic.Sip.Headers.HeaderFieldGroup<T>"/> object.</summary>
/// <remarks>
/// All HeaderFields in each group must match, as well as the group counts. All optional headers are compared using object equality that is each field in the header is used for comparision. When comparing HeaderFields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular HeaderField, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are case-sensitive. 
/// </remarks>
        /// <param name="other">The <see cref="T:Konnetic.Sip.Headers.HeaderFieldGroup"/> to compare to this instance.</param>
/// <returns><c>true</c> if <paramref name="other" /> is an instance of this class representing the same SIP HeaderField Group as this, <c>false</c> otherwise.</returns>    
/// <overloads>
/// <summary>This method is overloaded.</summary>
/// <remarks>Overloads allow for equality against <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>, <see cref="T:System.Object"/> and another instance of this HeaderField.</remarks>
/// </overloads>  
/// <threadsafety static="true" instance="false" />
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)] 
        public bool Equals(HeaderFieldGroup<T> other)
        {
            if((object)other == null)
                {
                return false;
                }

            if(Count != other.Count)
                {
                return false;
                }

            foreach(T hf in Headers)
                {
                if(!other.Contains(hf))
                    {
                    return false;
                    }
                }

            foreach(T hf in other.Headers)
                {
                if(!Contains(hf))
                    {
                    return false;
                    }
                }

            return base.Equals((HeaderFieldBase)other);
        }

        /// <summary>
        /// Compare this SIP Header for equality with the base <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>.
        /// </summary>
        /// <param name="other">The <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/> to compare to this instance.</param>
        /// <returns>
        /// 	<c>true</c> if <paramref name="other"/> is an instance of this class representing the same SIP HeaderField as this, <c>false</c> otherwise.
        /// </returns>
        /// <remarks>This method overrides the <c>equals</c> method in <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>.
        /// All optional headers are compared using object equality that is each field in the header is used for comparision. When comparing HeaderFields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular HeaderField, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are case-sensitive.
        /// </remarks>
        /// <overloads>
        /// 	<summary>This method is overloaded.</summary>
        /// 	<remarks>Overloads allow for equality against <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>, <see cref="T:System.Object"/> and another instance of this HeaderField.</remarks>
        /// </overloads>
        /// <threadsafety static="true" instance="false"/>
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public override bool Equals(HeaderFieldBase other)
            {
            return Equals((object)other);
            }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// 	<c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>This method overrides the <c>equals</c> method in <see cref="System.Object"/>.
        /// All optional headers are compared using object equality that is each field in the header is used for comparision. When comparing HeaderFields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular HeaderField, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are case-sensitive.
        /// </remarks>
        /// <overloads>
        /// 	<summary>This method is overloaded.</summary>
        /// 	<remarks>Overloads allow for equality against <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>, <see cref="T:System.Object"/> and another instance of this HeaderField.</remarks>
        /// </overloads>
        /// <threadsafety static="true" instance="false"/>
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public override bool Equals(object obj)
        {
            if(obj == null)
                {
                return false;
                }

            HeaderFieldGroup<T> p = obj as HeaderFieldGroup<T>;
            if((object)p == null)
                {
                T p1 = obj as T;
                if((object)p1 == null)
                    {
                    return false;
                    }
                else
                    {
                    if(Count > 1)
                        {
                        return false;
                        }
                    return p1.Equals(Headers[0]);
                    }
                }
            else
                {
                return this.Equals(p);
                }
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <threadsafety static="true" instance="false" />
        public IEnumerator GetEnumerator()
        {
            return _headers.GetEnumerator();
        }

 
        /// <summary>
        /// Returns the HeaderField at the specified index value.
        /// </summary>
        /// <returns>A HeaderField.</returns>
        /// <threadsafety static="true" instance="false" />
        public T GetHeaderField(int index)
        {
            return this[index];
        }

        /// <summary>
        /// Inserts the specified HeaderField at the index.
        /// </summary>
        /// <param name="index">The index position to insert the new header.</param>
        /// <param name="header">The HeaderField.</param>
        /// <threadsafety static="true" instance="false" />
        /// <exception cref="ArgumentNullException">Is raised when <paramref value="header"/> is null.</exception>
        public void Insert(int index,T header )
        {
        PropertyVerifier.ThrowOnNullArgument(header, "header");
        if(OnChange != null)
            {
            OnChange(this, new EventArgs());
            }
            Headers.Insert(index, header);
        }

        /// <summary>
        /// Validates this instance against the standard. Indicated whether it reaches minimum compliance.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if instance represents a valid HeaderField; otherwise, <c>false</c>.
        /// </returns> 
        /// <threadsafety static="true" instance="false"/>
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)] 
        public override bool IsValid()
        {
            foreach(T hf in Headers)
                {
                if(hf.IsValid() == false)
                    {
                    return false;
                    }
                }
            return base.IsValid();
        }

        /// <summary>
        /// Parses string representation of the HeaderField.
        /// </summary>
        /// <remarks> 
        /// <b>RFC 3261 Syntax:</b> 
        /// <table >
        /// <tr><td colspan="2" style="border-bottom:none">header = header-name HCOLON header-value *(SEPERATOR header-value)</td></tr> 
        /// </table>
        /// <para/>
        /// The SEPERATOR is typically the comma (","), but can be whitespace. 
        /// </remarks>
        /// <example>
        /// <list type="bullet">
        /// <item>Route: &lt;sip:alice@atlanta.com&gt;, &lt;sip:bob@biloxi.com&gt;, &lt;sip:carol@chicago.com&gt;</item>
        /// <item>Server: HomeServer v2</item>
        /// </list> 
        /// </example>
        /// <param name="value">The HeaderField string to parse.</param>
        /// <exception cref="T:Konnetic.Sip.SipParseException">Thrown when an invalid (non-standard) value is encountered.</exception>
        /// <exception cref="T:Konnetic.Sip.SipException">Thrown when a processing exception is encountered.</exception>
        /// <threadsafety static="true" instance="false" />
        public override void Parse(string value)
        {
            if(value != null)
            {
            value = Syntax.ReplaceFolding(value);
            Clear();
            //TODO This fundametally needs to change! Not sophisticated enough to simply split on a ","!
            Regex _groupSeperator = new Regex(Seperator, RegexOptions.Compiled | RegexOptions.IgnoreCase );

            string[] lines = _groupSeperator.Split(value);
            if(lines.Length > 0)
                {
                foreach(string s in lines)
                    {
                    if(!string.IsNullOrEmpty(s))
                        {
                        T b = new T();
                        b.Parse(s);
                        Add(b);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Removes the specified HeaderField from the collection.
        /// </summary>
        /// <param name="fieldValue">The HeaderField value.</param>
        /// <exception cref="ArgumentNullException">Is raised when <paramref value="fieldValue"/> is null.</exception>
        /// <threadsafety static="true" instance="false" />
        public void Remove(string fieldValue)
        {
            PropertyVerifier.ThrowOnNullArgument(fieldValue, "fieldValue");
            T header = this[fieldValue];
            if(header != null)
                {
                Remove(header); 
                }
        }

        /// <summary>
        /// Removes the specified header.
        /// </summary>
        /// <param name="header">The header.</param>
        /// <exception cref="ArgumentNullException">Is raised when <paramref value="header"/> is null.</exception>
        /// <threadsafety static="true" instance="false" />
        public void Remove(T header)
        {
        PropertyVerifier.ThrowOnNullArgument(header, "header");
        if(OnChange != null)
            {
            OnChange(this, new EventArgs());
            }
            Headers.Remove(header);
        }

        /// <summary>
        /// Removes the HeaderField at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <threadsafety static="true" instance="false" />
        public void RemoveAt(int index)
        {
        Headers.RemoveAt(index);
        if(OnChange != null)
            {
            OnChange(this, new EventArgs());
            }
        }

        /// <summary>
        /// Adds or updates specified HeaderField value.
        /// </summary>
        /// <param name="oldHeader">The old HeaderField.</param>
        /// <param name="newHeader">The new HeaderField.</param>
        /// <threadsafety static="true" instance="false" />
        /// <exception cref="ArgumentNullException">Is raised when <paramref value="oldHeader"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Is raised when <paramref value="newHeader"/> is null.</exception>
        public void Set(T oldHeader, T newHeader)
        {
            PropertyVerifier.ThrowOnNullArgument(oldHeader, "oldHeader");
            PropertyVerifier.ThrowOnNullArgument(newHeader, "newHeader");
            if(Contains(oldHeader.GetStringValue()))
                {
                this[oldHeader.GetStringValue()] = newHeader;
                if(OnChange != null)
                    {
                    OnChange(this, new EventArgs());
                    }
                }
            else
                {
                Add(newHeader);
                }
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
            return ToUriString(new string[] { "" });
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the group of HeaderFields.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents the group of HeaderFields.</returns>
        /// <threadsafety static="true" instance="false" />
        public override string GetStringValue()
        {
            StringBuilder sb = new StringBuilder(200);
            bool first = true;
            for(int i = 0; i < Headers.Count; i++)
                {
                string s = this[i].GetStringValue();
                if(!string.IsNullOrEmpty(s))
                    {
                    if(!first) { sb.Append(Seperator); sb.Append(SR.SingleWhiteSpace); } else { first = false; }
                    sb.Append(s);
                    }
                }
            return sb.ToString();
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance in a format suitable for URIs.
        /// </summary>
        /// <param name="exclude">The exclude.</param>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        /// <remarks>The string is escaped.</remarks>
        /// <threadsafety static="true" instance="false"/>
        internal string ToUriString(string[] exclude)
        {
            PropertyVerifier.ThrowOnNullArgument(exclude, "exclude");
            bool drop;
            StringBuilder sb = new StringBuilder(200);
            foreach(T header in this)
                {
                drop = false;
                foreach(string s in exclude)
                    {

                    if(header.FieldName.Equals(s, StringComparison.OrdinalIgnoreCase))
                        {
                        drop = true;
                        break;
                        }
                    }

                if(!drop)
                    {
                    if(sb.Length == 0)
                        {
                        sb.Append("?");
                        }
                    else
                        {
                        sb.Append("&");
                        }
                    sb.Append(header.FieldName);
                    sb.Append("=");
                    sb.Append(header.GetStringValue());
                    }
                }
            return sb.ToString();
        }

        /// <summary>
        /// Throws if the user attempts to instantiate the generic class with a security HeaderField.
        /// </summary>
        /// <exception cref="SipException">Is raised when the user attempts to instantiate generic class with a security HeaderField.</exception>
        /// <threadsafety static="true" instance="false" />
        protected virtual void ThrowOnSecurityGroup()
        {
            T n = new T();
            string typeName = n.GetType().Name;
            if(typeName == "AuthorizationHeaderField"
                || typeName == "ProxyAuthenticateHeaderField"
                || typeName == "ProxyAuthorizationHeaderField"
                || typeName == "WwwAuthenticateHeaderField")
                {
                throw new SipException("Use AuthHeaderFieldGroup for Authentication/Authorization header fields.");
                }
        }

        #endregion Methods

        #region Indexers

        /// <summary>
        /// Gets or sets the <see cref="T"/> at the specified index.
        /// </summary>
        /// <value>The HeaderField at the <param name="index"/> index in the collection.</value> 
        /// <threadsafety static="true" instance="false" />
        public T this[Int32 index]
        {
            get
            { 
                if(Headers.Count == 0)
                    {
                    return null;
                    }

                return Headers[index] as T;
                }
            private set
            { 
                if(Headers.Count > 0)
                    {
                    if(OnChange != null)
                        {
                        OnChange(this, new EventArgs());
                        }
                    Headers[index] = value;
                    }
                }
        }

        /// <summary>
        /// Gets or sets the specified HeaderField from collection. Returns null if parameter with specified name doesn't exist.
        /// </summary>
        /// <value>
        /// The HeaderField matching the <param name="fieldValue"/> parameter.
        /// </value>
        /// <returns>Returns parameter with specified name or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Is raised when <paramref value="fieldValue"/> is null.</exception>
        /// <threadsafety static="true" instance="false"/>
        public T this[string fieldValue]
        {
            get
            {
            PropertyVerifier.ThrowOnNullArgument(fieldValue, "fieldValue");
            return this[fieldValue, StringComparison.OrdinalIgnoreCase];
            }
            set
            {
            PropertyVerifier.ThrowOnNullArgument(fieldValue, "fieldValue");
            this[fieldValue, StringComparison.OrdinalIgnoreCase] = value;
            if(OnChange != null)
                {
                OnChange(this, new EventArgs());
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="T"/> with the specified field value. Returns null if parameter with specified name doesn't exist.
        /// </summary>
        /// <param name="comparisonType">The string comparision algorithm.</param>
        /// <value>
        /// The HeaderField matching the <param name="fieldValue"/> parameter.
        /// </value>
        /// <returns>Returns parameter with specified name or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Is raised when <paramref value="fieldValue"/> is null.</exception>
        /// <threadsafety static="true" instance="false"/>
		internal T this[string fieldValue, StringComparison comparisonType]
        {
            get
            {
            PropertyVerifier.ThrowOnNullArgument(fieldValue, "fieldValue");
                foreach(T hf in Headers)
                    {
                    if(hf.GetStringValue().Equals(fieldValue, comparisonType))
                        {
                        return hf;
                        }
                    }

                return null;
                }
            set
            {
            PropertyVerifier.ThrowOnNullArgument(fieldValue, "fieldValue");
                for(int i = 0; i < Headers.Count; i++)
                    {
                    if(this[i].GetStringValue().Equals(fieldValue, comparisonType))
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