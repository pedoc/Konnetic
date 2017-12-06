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
using System.Runtime.CompilerServices;

namespace Konnetic.Sip.Headers
{
    /// <summary>
    /// A collection of HeaderFields used by SIP messages to process zero or more HeaderFields 
    /// </summary>
    /// <remarks>
    /// Multiple HeaderFields of the same field name whose value is a comma-separated list can be combined into one HeaderField. Clients should use the <see cref="T:Konnetic.Sip.Headers.HeaderFieldGroup{T}"/> to combine multiple HeaderFields.    /// <para/>
    /// <note type="caution">Duplicate HeaderFields are not allowed. Use <see cref="T:Konnetic.Sip.Headers.HeaderFieldGroup"/> to combine multiple HeaderFields of the same type.</note>
    /// </remarks>
    public class HeaderFieldCollection : Collection<HeaderFieldBase>, IEnumerable<HeaderFieldBase>
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        //private Collection<HeaderFieldBase> _innerList;

        #endregion Fields

        #region Properties
        IEnumerator IEnumerable.GetEnumerator()
            {
            return this.GetEnumerator();
            }
 

        #endregion Properties

        #region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="HeaderFieldCollection"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks> 
        public HeaderFieldCollection()
        {
            //_innerList = new Collection<HeaderFieldBase>();
        }

        #endregion Constructors

        #region Methods
 
        /// <summary>
        /// Adds the specified name value pair.
        /// </summary>
        /// <remarks><note type="caution">No HeaderField validation is conducted on the name or value.<note></remarks>
        /// <param name="nameValuePair">The name value pair.</param>
        /// <exception cref="ArgumentNullException">Is raised when <b>nameValuePair</b> is null or empty.</exception>
        /// <exception cref="ArgumentException">Is raised when a HeaderField with the same name already exists in the collection.</exception> 
        /// <threadsafety static="true" instance="false" />
        public void Add(string nameValuePair)
        {
            PropertyVerifier.ThrowOnNullOrEmptyString(nameValuePair, "nameValuePair");
            string[] newVal = nameValuePair.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
            if (newVal.Length!=2)
                {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Not a valid name/value pair ('{0}')", nameValuePair));
                }
            Add(newVal[0], newVal[1]);
        }

        /// <summary>
        /// Adds a new parameter to the HeaderFieldCollection.
        /// </summary>
        /// <remarks><note type="caution">No HeaderField validation is conducted on the name or value.<note></remarks>
        /// <param name="name">The HeaderField name.</param>
        /// <param name="value">The HeaderField value.</param>
        /// <exception cref="ArgumentNullException">Is raised when <b>name</b> is null.</exception>
        /// <exception cref="ArgumentException">Is raised when <b>name</b> is empty.</exception> 
        /// <exception cref="ArgumentException">Is raised when <paramref name="name"/> already exists in the collection.</exception> 
        /// <threadsafety static="true" instance="false" />
        public void Add(string name, string value)
        {
            PropertyVerifier.ThrowOnNullArgument(value, "value");
            PropertyVerifier.ThrowOnNullOrEmptyString(name, "name");

            StringBuilder sb = new StringBuilder(40);
            sb.Append(name);
            sb.Append(SR.HeaderFieldSeperator);
            sb.Append(value);
            HeaderFieldBase field = HeaderFieldFactory.CreateHeaderField(name);
            field.Parse(sb.ToString());
            Add(field);
        }

        /// <summary>
        /// Adds the specified field.
        /// </summary>
        /// <remarks><note type="caution">No HeaderField validation is conducted on the HeaderField.<note></remarks>
        /// <param name="field">The field.</param>
        /// <exception cref="ArgumentNullException">Is raised when <paramref name="field"/> is null.</exception>
        /// <exception cref="ArgumentException">Is raised when <paramref name="field"/> already exists in the collection.</exception> 
        /// <threadsafety static="true" instance="false" />
        public void Add(HeaderFieldBase field)
        {
            PropertyVerifier.ThrowOnNullArgument(field, "field");
            PropertyVerifier.ThrowIfDuplicateHeaderField(this, field.FieldName);

            base.Add(field);
        }

        /// <summary>
        /// Clears this instance of all HeaderFields.
        /// </summary>
        /// <threadsafety static="true" instance="false" />
        public void Clear()
        {
        base.Clear();
        }

        /// <summary>
        /// Checks if the collection contains a HeaderField with the specified name.
        /// </summary>
        /// <param name="name">The HeaderField name.</param>        
        /// <returns>
        /// 	<c>true</c> if the collection contains the HeaderField specified; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">Is raised when <paramref name="name"/> is null.</exception>
        /// <threadsafety static="true" instance="false" />
		[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public bool Contains(string name)
        {
            PropertyVerifier.ThrowOnNullArgument(name, "name");
            return this[name] != null;
        }

        /// <summary>
        /// Checks if the collection contains a HeaderField.
        /// </summary>
        /// <param name="field">The HeaderField to match against.</param>
        /// <returns>
        /// 	<c>true</c> if the collection contains the HeaderField specified; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">Is raised when <paramref name="field"/> is null.</exception>
        /// <threadsafety static="true" instance="false" />
		[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public bool Contains(HeaderFieldBase field)
        {
            PropertyVerifier.ThrowOnNullArgument(field, "field");
            return this[field.FieldName] != null;
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
        return base.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <threadsafety static="true" instance="false" />
        IEnumerator<HeaderFieldBase> IEnumerable<HeaderFieldBase>.GetEnumerator()
        {
        return base.GetEnumerator();
        }

        /// <summary>
        /// Removes the HeaderField specified from the collection.
        /// </summary>
        /// <param name="name">The HeaderField name.</param>
        /// <exception cref="ArgumentNullException">Is raised when <paramref name="name"/> is null.</exception>
        /// <threadsafety static="true" instance="false" />
        public void Remove(string name)
        {
            PropertyVerifier.ThrowOnNullArgument(name, "name");

            HeaderFieldBase para = this[name];
            if(para != null)
                {
                base.Remove(para);
                }
        }

        /// <summary>
        /// Adds or updates specified HeaderField.
        /// </summary>
        /// <param name="name">The HeaderField name.</param>
        /// <param name="value">The HeaderField value.</param>
        /// <exception cref="ArgumentNullException">Is raised when <paramref name="name"/> is empty.</exception>
        /// <exception cref="ArgumentNullException">Is raised when <paramref value="value"/> is null.</exception>
        /// <threadsafety static="true" instance="false" />
        public void Set(string name, string value)
        {
            PropertyVerifier.ThrowOnNullOrEmptyString(name, "name");
            PropertyVerifier.ThrowOnNullArgument(value, "value");
            if(Contains(name))
                {
                Update(name, value);
                }
            else
                {
                Add(name, value);
                }
        }

        /// <summary>
        /// Adds or updates specified HeaderField.
        /// </summary>
        /// <param name="field">The HeaderField to add or update.</param>
        /// <exception cref="ArgumentNullException">Is raised when <paramref value="field"/> is null.</exception>
        /// <threadsafety static="true" instance="false" />
        public void Set(HeaderFieldBase field)
        {
        PropertyVerifier.ThrowOnNullArgument(field, "HeaderField");
        if(Contains(field.FieldName))
                {
                this[field.FieldName] = field;
                }
            else
                {
                Add(field);
                }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <param name="useCompactForm">if set to <c>true</c> then the output used the compact form for applicable HeaderFields.</param>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        /// <threadsafety static="true" instance="false" />
        public string ToString(bool useCompactForm)
        {
            StringBuilder sb = new StringBuilder(100);
            foreach(HeaderFieldBase field in this)
                {
                sb.Append(field.GetString(useCompactForm));
                sb.Append("\r\n");
                }
            return sb.ToString();
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        /// <threadsafety static="true" instance="false" />
        public override string ToString()
        {
            return ToString(false);
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
        /// Updates the header found matching the <paramref name="name">name</paramref> using the new <paramref name="value">value</paramref>.
        /// </summary>
        /// <param name="name">Name of the HeaderField.</param>
        /// <param name="value">The new HeaderField value.</param>
        /// <returns>
        /// 	<c>true</c> if the HeaderField was updated; otherwise, <c>false</c>.
        /// </returns> 
        /// <exception cref="ArgumentNullException">Is raised when <paramref value="name"/> is empty.</exception>
        /// <exception cref="ArgumentNullException">Is raised when <paramref value="value"/> is null.</exception>
        /// <threadsafety static="true" instance="false" />
		[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public void Update(string name, string value)
        {
            PropertyVerifier.ThrowOnNullOrEmptyString(name, "name");
            PropertyVerifier.ThrowOnNullArgument(value, "value");
            HeaderFieldBase hf = this[name];
            if((object)hf != null)
                {
                hf.Parse(value);
                }
        }

        ///// <summary>
        ///// Updates the header found matching the <param name="name">name</param> using the new <param name="value">value</param> and <param name="parameter">parameter</param> parameters.
        ///// </summary>
        ///// <param name="fieldName">The HeaderField name.</param>
        ///// <param name="newValue">The new value.</param>
        ///// <param name="newParameter">The new parameter.</param>
        ///// <returns>
        ///// 	<c>true</c> if the HeaderField was updated; otherwise, <c>false</c>.
        ///// </returns>
        ///// <remarks>Only the first header found is updated.</remarks>
        ///// <exception cref="ArgumentNullException">Is raised when <paramref value="name"/> is empty.</exception>
        ///// <exception cref="ArgumentNullException">Is raised when <paramref value="value"/> is null.</exception>
        ///// <threadsafety static="true" instance="false" />
        //[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        //public bool Update(string name, string value, SipParameterCollection parameters)
        //{
        //    PropertyVerifier.ThrowOnNullOrEmptyString(name, "fieldName");
        //    PropertyVerifier.ThrowOnNullArgument(value, "value");
        //    bool found = false;
        //    ParamatizedHeaderFieldBase p = null;
        //    foreach(HeaderFieldBase hf in _innerList)
        //        {
        //        if(hf.FieldName.ToUpperInvariant() == name.ToUpperInvariant())
        //            {
        //            hf.Parse(value);
        //            if(parameters != null)
        //                {
        //                p = hf as ParamatizedHeaderFieldBase;
        //                }
        //            if(p!=null)
        //                {
        //                p.GenericParameters = parameters;
        //                }
        //            found = true;
        //            break;
        //            }
        //        }
        //    return found;
        //}


        /// <summary>
        /// Toes the URI string.
        /// </summary>
        /// <param name="exclude">The exclude.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Is raised when <paramref value="value"/> is null.</exception>
        /// <threadsafety static="true" instance="false" />
        internal string ToUriString(string[] exclude)
        {
            PropertyVerifier.ThrowOnNullArgument(exclude, "exclude");
            bool drop;
            StringBuilder sb = new StringBuilder(100);
            foreach(HeaderFieldBase field in base.Items)
                {
                drop = false;
                foreach(string s in exclude)
                    {

                    if(field.FieldName.Equals(s, StringComparison.OrdinalIgnoreCase))
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
                    sb.Append(Uri.EscapeUriString(field.FieldName.ToLowerInvariant()));
                    sb.Append(SR.ParameterSeperator);
                    sb.Append(Uri.EscapeUriString(field.GetStringValue()));
                    }
                }
            return sb.ToString();
        }

        #endregion Methods

        #region Indexers

        /// <summary>
        /// Gets or sets the <see cref="Konnetic.Sip.Headers.HeaderFieldBase"/> at the specified index.
        /// </summary>
        /// <value>The HeaderField</value>
        /// <exception cref="ArgumentNullException">Is raised when <paramref value="value"/> is null.</exception>
        /// <exception cref="ArgumentException">Is raised when the change would create a duplicate entry.</exception>
        /// <threadsafety static="true" instance="false" />
        [IndexerName("Fields")]
        public HeaderFieldBase this[int index]
        {
            get
                {
                return base.Items[index];
                }
            set
            {
                PropertyVerifier.ThrowOnNullArgument(value, "value");
                HeaderFieldBase hf = base.Items[index];
                if(hf.FieldName!= value.FieldName)
                    {
                PropertyVerifier.ThrowIfDuplicateHeaderField(this,value.FieldName);
                    }
                base.Items[index] = value;
                }
        }


        /// <summary>
        /// Gets or sets the <see cref="Konnetic.Sip.Headers.HeaderFieldBase"/> with the specified name.
        /// </summary>
        /// <value>The HeaderField</value>
        /// <exception cref="ArgumentNullException">Is raised when <paramref value="name"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Is raised when <paramref value="value"/> is null.</exception>
        /// <exception cref="ArgumentException">Is raised when the change would create a duplicate entry.</exception>
        /// <threadsafety static="true" instance="false" />
        [IndexerName("Fields")]
        public HeaderFieldBase this[string name]
        {
            get
			{
			PropertyVerifier.ThrowOnNullArgument(name, "name");
            return this[name, StringComparison.OrdinalIgnoreCase];
            }
            set
			{
			PropertyVerifier.ThrowOnNullArgument(name, "name");
			PropertyVerifier.ThrowOnNullArgument(value, "value");
			if(!string.IsNullOrEmpty(name))
				{ 
            if(value.FieldName != name)
                {
                PropertyVerifier.ThrowIfDuplicateHeaderField(this, value.FieldName);
                }

            this[name, StringComparison.OrdinalIgnoreCase] = value;
				}
			}
        }

        /// <summary>
        /// Gets or sets the <see cref="Konnetic.Sip.Headers.HeaderFieldBase"/> with the specified field name.
        /// </summary>
        /// <value></value>
        /// <exception cref="ArgumentNullException">Is raised when <paramref value="name"/> is null.</exception>
        /// <threadsafety static="true" instance="false" />
        [IndexerName("Fields")]
        internal HeaderFieldBase this[string name, StringComparison comparisonType]
        {
            get
			{
			PropertyVerifier.ThrowOnNullArgument(name, "name");
            foreach(HeaderFieldBase hf in base.Items)
                    {
                    if(hf.FieldName.Equals(name, comparisonType))
                        {
                        return hf;
                        }
                    }

                return null;
                }
            set
			{
			if(!string.IsNullOrEmpty(name))
				{ 
                for(int i = 0; i < base.Count; i++)
                    {
                    if(base.Items[i].FieldName.Equals(name, comparisonType))
                        {
                        base.Items[i] = value;
                        }
					}
				}
            }
        }

        #endregion Indexers

        #region Other

        ///// <summary>
        ///// Toes the string combine lines.
        ///// </summary>
        ///// <param name="useCompactForm">if set to <c>true</c> [use compact form].</param>
        ///// <returns></returns>
        //private string ToStringCombineLines(bool useCompactForm)
        //{
        //    bool first = true;
        //    StringBuilder retVal = new StringBuilder();
        //    foreach(HeaderFieldBase field in _innerList)
        //        {
        //        if(first)
        //            {
        //            retVal.Append(field.ToString(useCompactForm));
        //            first = false;
        //            }
        //        else
        //            {
        //            retVal.Append(SR.MultiheaderFieldSuffix);
        //            retVal.Append(SR.SingleWhiteSpace);
        //            retVal.Append(field.ToString(useCompactForm));
        //            }
        //        }
        //    return retVal.ToString();
        //}

        #endregion Other



    }
}