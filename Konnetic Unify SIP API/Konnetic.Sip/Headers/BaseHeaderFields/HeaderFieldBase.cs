/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace Konnetic.Sip.Headers
{
    /// <summary>
    /// The abstract bas class for all HeaderFields.
    /// </summary> 
    public abstract class HeaderFieldBase : IEquatable<HeaderFieldBase>, IComparable<HeaderFieldBase>, ICloneable
    {
        //#region Enumerations

        //private enum DeserliazeState
        //{
        //    Normal,
        //    AtCR,
        //    AtCRLF,
        //    AtSeperation
        //}

        //#endregion Enumerations

        #region Fields

        /// <summary>
        /// Represents whether the field can have multiple values in the header.
        /// </summary>
        private bool _multipleAllowed;

        /// <summary>
        /// Represents the short field name. 
        /// </summary>
        private string _compactName;

        /// <summary>
        /// Represents the field name. 
        /// </summary>
        private string _name;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether multiple fields are allowed in the header block.
        /// </summary>
        /// <value><c>true</c> if [multiple allowed]; otherwise, <c>false</c>.</value>
		[DefaultValue(true)]
        public bool AllowMultiple
            {
            [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
            get { return _multipleAllowed; }
            protected set
            {
            _multipleAllowed = value;
            }
        }

        /// <summary>
        /// The short form of the name.
        /// </summary>
        /// <remarks>SIP provides a mechanism to represent common header field names in an abbreviated form. This may be useful when messages would otherwise become too large to be carried on the transport available to it (exceeding the maximum transmission unit (MTU) when using UDP, for example). A compact form may be substituted for the longer form of a header field name at any time without changing the semantics of the message. A header field name may appear in both long and short forms within the same message. Implementations must accept both the long and short forms of each header name.</remarks>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="CompactName"/>.</exception> 
        /// <exception cref="SipFormatException">Thrown on detection of illegal characters being applied to the name. HeaderField names can only contain "token" values.</exception> 
        public string CompactName
        {
            get { return _compactName; }
        protected set
            {
            PropertyVerifier.ThrowOnInvalidToken(value, "FieldName"); 
            PropertyVerifier.ThrowOnNullArgument(value, "CompactName"); 
            _compactName = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the field.
        /// </summary>
        /// <remarks>Field names are case-insensitive.</remarks>
        /// <value>The name of the field.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="CompactName"/>.</exception> 
        /// <exception cref="SipFormatException">Thrown on detection of illegal characters being applied to the name. HeaderField names can only contain "token" values.</exception> 
        [DefaultValue("Field Name")]
        public string FieldName
        {
            get { return _name; }
            protected set
            {
            PropertyVerifier.ThrowOnNullArgument(value, "FieldName");
            PropertyVerifier.ThrowOnInvalidToken(value, "FieldName"); 
            _name = value;
            }
        }

        ///// <summary>
        ///// Gets or sets (parses) the field value.
        ///// </summary>
        ///// <value>The field value.</value>
        //[DefaultValue("Value")]
        //public virtual string FieldValue
        //    {
        //    get
        //        {
        //        return GetStringValue();
        //        }
        //    set
        //        {
        //        Parse(value);
        //        }
        //    }

        #endregion Properties

        #region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="HeaderFieldBase"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks> 
        protected HeaderFieldBase()
        {
            _name = string.Empty;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="h1">The first <see cref="HeaderFieldBase"/> instance.</param>
        /// <param name="h2">The second <see cref="HeaderFieldBase"/> instance.</param>
        /// <returns>The result of the less-than operator.</returns>
        /// <threadsafety static="true" instance="false" />
		public static bool operator <(HeaderFieldBase h1, HeaderFieldBase h2)
			{
			return h1.CompareTo(h2) < 0;
			}

        /// <summary>
        /// Implements the operator &gt;.
        /// </summary>
        /// <param name="h1">The first <see cref="HeaderFieldBase"/> instance.</param>
        /// <param name="h2">The second <see cref="HeaderFieldBase"/> instance.</param>
        /// <returns>The result of the greater-than operator.</returns>
        /// <threadsafety static="true" instance="false" />
		public static bool operator >(HeaderFieldBase h1, HeaderFieldBase h2)
			{
			return h1.CompareTo(h2) > 0;
			}
        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="h1">The first <see cref="HeaderFieldBase"/> instance.</param>
        /// <param name="h2">The second <see cref="HeaderFieldBase"/> instance.</param>
        /// <returns>The result of the inequality operator.</returns>
        /// <threadsafety static="true" instance="false" />
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public static bool operator !=(HeaderFieldBase h1, HeaderFieldBase h2)
        {
            if((object)h1 == null)
                {
                if((object)h2 != null)
                    {
                    return !h2.Equals(h1);
                    }
                return false;
                }
            return !h1.Equals(h2);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="h1">The first <see cref="HeaderFieldBase"/> instance.</param>
        /// <param name="h2">The second <see cref="HeaderFieldBase"/> instance.</param>
        /// <returns>The result of the equality operator.</returns>
        /// <threadsafety static="true" instance="false" />
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public static bool operator ==(HeaderFieldBase h1, HeaderFieldBase h2)
        {
            if((object)h1 == null)
                {
                if((object)h2 == null)
                    {
                    return true;
                    }
                return false;
                }
            return h1.Equals(h2);
        }

        /// <summary>
        /// Creates a deap  copy of this instance.
        /// </summary> 
        /// <remarks>This is an abstract method. Creates and returns a deep copy of the Header. This method ensures a deep copy of the HeaderField, when a message is cloned the HeaderField can be modified without effecting the original HeaderField in the message.</remarks>
        /// <returns>A deep copy of <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>.</returns>
        /// <threadsafety static="true" instance="false" /> 
        public abstract HeaderFieldBase Clone();

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has the following meanings:
        /// Value
        /// Meaning
        /// Less than zero
        /// This object is less than the <paramref name="other"/> parameter.
        /// Zero
        /// This object is equal to <paramref name="other"/>.
        /// Greater than zero
        /// This object is greater than <paramref name="other"/>.
        /// </returns>
        /// <threadsafety static="true" instance="false" />
        public int CompareTo(HeaderFieldBase other)
        {
            if(other.FieldName == this.FieldName)
                {
                return string.CompareOrdinal(this.GetStringValue(),other.GetStringValue());
                }
            else
                {
                return string.CompareOrdinal(this.FieldName, other.FieldName);
                }
        }
        /*
       public void Deserialize(ref NetworkStream stream)
       {
           
           DeserliazeState state = DeserliazeState.Normal;
           int interation = 1;
           int multiple = 1;
           int count = 0;
           byte[] byteArray = new byte[1024];
           StringBuilder sb = new StringBuilder(100);
           bool breakOut=false;

           while(!breakOut)
               {
               int i =  stream.ReadByte();
               if(i == -1)
                   {
                   sb.Append(Encoding.UTF8.GetChars(byteArray));
                   break;
                   }
               byte byteTmp = Convert.ToByte(i);
               byteArray[multiple + (count++)] = byteTmp;

               if(count == 1023)
                   {
                   sb.Append(Encoding.UTF8.GetChars(byteArray));
                   byteArray = new byte[1024];
                   multiple = 1024 * interation++;
                   count = 0;
                   }

               if(byteTmp == 13 | byteTmp == 10 | byteTmp == 32) //CRLF or SP
                   {
                   switch(state)
                       {
                       case DeserliazeState.Normal:
                           if(byteTmp == 13)
                               state = DeserliazeState.AtCR;
                           break;
                       case DeserliazeState.AtCR:
                           if(byteTmp == 10)
                               state = DeserliazeState.AtCRLF;
                           break;
                       case DeserliazeState.AtCRLF:
                           if(byteTmp == 32) //CRLFSP is folding
                               state = DeserliazeState.Normal;
                           else
                               {
                               state = DeserliazeState.AtSeperation;
                               }
                           break;
                       }
                   if(state == DeserliazeState.Normal)
                       {
                       state = DeserliazeState.AtCR;
                       }
                   }
               else
                   {
                   state = DeserliazeState.Normal;
                   }

               if(state == DeserliazeState.AtSeperation)
                   {
                   breakOut = true;
                   }
               }

           this.Parse(sb.ToString());
           
       }
        */

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
        public virtual bool Equals(HeaderFieldBase other)
        {
            if((object)other == null)
                {
                return false;
                }

            return _name.Equals(other._name, StringComparison.OrdinalIgnoreCase);
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

            HeaderFieldBase p = obj as HeaderFieldBase;
            if((object)p == null)
            {
            return false;
            }

            return this.Equals(p);
        }

        /// <summary>
        /// Returns the representation of the HeaderField as a byte array. Encoding is UTF8.
        /// </summary>
        /// <returns>A byte array that represents this instance.</returns>
        /// <threadsafety static="true" instance="false" />
        public byte[] GetBytes()
        {
            return Encoding.UTF8.GetBytes(GetChars());
        }

        /// <summary>
        /// Returns the representation of the HeaderField as a char array.
        /// </summary>
        /// <returns>A char array that represents this instance.</returns>
        /// <threadsafety static="true" instance="false" />
        public char[] GetChars()
        {
            return  ToString().ToCharArray();
        }


        /// <summary>
        /// Creates a deap  copy of this instance.
        /// </summary> 
        /// <remarks>Creates and returns a deep copy of the Header. This method ensures a deep copy of the HeaderField, when a message is cloned the HeaderField can be modified without effecting the original HeaderField in the message.</remarks>
        /// <returns>A deep copy of <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>.</returns>
        /// <threadsafety static="true" instance="false" /> 
        object ICloneable.Clone()
        {
            return this.Clone();
        }


        /// <summary>
        /// Validates this instance against the standard. Indicated whether it reaches minimum compliance.
        /// </summary>
        /// <remarks>This member is virtual.</remarks>
        /// <returns>
        /// 	<c>true</c> if instance represents a valid HeaderField; otherwise, <c>false</c>.
        /// </returns>
        /// <threadsafety static="true" instance="false" />
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public virtual bool IsValid()
        {
            return !string.IsNullOrEmpty(_name);
        }

        /// <summary>
        /// Parses string representation of the HeaderField.
        /// </summary>
        /// <remarks>This is an abstract method.
        /// </remarks>
        /// <param name="value">The HeaderField string to parse.</param>
        /// <exception cref="T:Konnetic.Sip.SipParseException">Thrown when an invalid (non-standard) value is encountered.</exception>
        /// <exception cref="T:Konnetic.Sip.SipException">Thrown when a processing exception is encountered.</exception>
        /// <threadsafety static="true" instance="false" />
        public abstract void Parse(String value);

        ///// <summary>
        ///// Serializes the specified stream.
        ///// </summary>
        ///// <param name="stream">The stream.</param>
        ///// <param name="useCompactForm">if set to <c>true</c> [use compact form].</param>
        ///// <threadsafety static="true" instance="false" />
        //public void Serialize(ref NetworkStream stream, bool useCompactForm)
        //{
        //    Serialize(ref stream,0,useCompactForm);
        //}

        ///// <threadsafety static="true" instance="false" />
        //public void Serialize(ref NetworkStream stream, int offset, bool useCompactForm)
        //{
        //    char[] sOut = GetString(useCompactForm).ToCharArray();
        //    byte[] fName = Encoding.UTF8.GetBytes(sOut);
        //    stream.Write(fName, offset, fName.Length);
        //}





        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance in a format suitable for URIs.
        /// </summary>        
        /// <remarks>The string is escaped.</remarks>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.</returns>
        /// <threadsafety static="true" instance="false" /> 
        public string ToUriString(bool useCompactForm)
        {
            //Encoding
            return GetString(useCompactForm);
        }

        /// <summary>
        /// Removes the name of the field (with the optional colon).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="longName">The long name.</param>
        /// <param name="shortName">The short name.</param>
        /// <threadsafety static="true" instance="false"/>
        protected internal static void RemoveFieldName(ref string value, string longName, string shortName)
        { 

            if(!string.IsNullOrEmpty(value))
                {

                Regex _header = new Regex(@"(?<=^[ \t]*)"+longName+"|"+shortName+@"(?=[ \t]*:[ \t]*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                //Regex _header = new Regex(@"(?<=^[ \t]*)[\w-.!%_*+`'~]+(?=[ \t]*:[ \t]*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                value = Syntax.ReplaceFolding(value);
                Match m = _header.Match(value);
                if(m != null)
                    {
                    if(!string.IsNullOrEmpty(m.Value))
                        {
                            //if(longName != shortName)
                            //    {
                            //    if(m.Value.ToUpper() != longName.ToUpper() && m.Value.ToUpper() != shortName.ToUpper())
                            //        {
                            //        throw new SipFormatException("Not a valid " + longName + " string.");
                            //        }
                            //    }
                            //else
                            //    {
                            //    if(m.Value.ToUpper() != longName.ToUpper())
                            //        {
                            //        throw new SipFormatException("Not a valid " + longName + " string.");
                            //        }
                            //    }
                        Regex _headerReplace = new Regex(@"^[ \t]*("+longName+"|"+shortName+@")[ \t]*:[ \t]*", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                            value = _headerReplace.Replace(value, string.Empty);

                        }
                    }
                }
        }

        /// <summary>
        /// Gets a string representation of the HeaderField value. The HeaderField name is excluded.
        /// </summary>
        /// <remarks>This is an abstract method.</remarks>
        /// <returns>String representation of the HeaderField value</returns>
        /// <threadsafety static="true" instance="false" />
        public abstract string GetStringValue();

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        /// <threadsafety static="true" instance="false" />
        public sealed override string ToString()
			{
			return GetString(false);
			}

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
            {  
            //Override in ParamatizedHeaderField as using Params would be too slow. 
            return (FieldName+GetStringValue()).GetHashCode();
            }
        /// <summary>
        /// Gets a string representation of the HeaderField value. The HeaderField name is included.
        /// </summary>
        /// <param name="useCompactForm">if set to <c>true</c> if the compact form of each name should be used.</param>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        /// <threadsafety static="true" instance="false" />
        public string GetString(bool useCompactForm)
        {
            StringBuilder sb = new StringBuilder(100);
            sb.Append(useCompactForm ? CompactName : FieldName);
            sb.Append(SR.HeaderFieldSeperator);
            sb.Append(SR.SingleWhiteSpace);
            sb.Append(GetStringValue());
            return sb.ToString();
        }

        #endregion Methods
    }
}