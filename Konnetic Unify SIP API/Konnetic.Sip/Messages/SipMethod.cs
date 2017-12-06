/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Konnetic.Sip.Messages
{
    /// <summary>
    /// 
    /// </summary>
    [DebuggerDisplay("{Method}")]
    [StructLayout(LayoutKind.Auto)]
    public struct SipMethod
    {
        #region Fields

        /// <summary>
        /// static SipMethod()
  
        /// </summary>
		public static readonly SipMethod Ack = new SipMethod("ACK");

        /// <summary>
        /// 
        /// </summary>
		public static readonly SipMethod Cancel = new SipMethod("CANCEL");

        /// <summary>
        /// 
        /// </summary>
        public static readonly SipMethod Empty= new SipMethod("");

        /// <summary>
        /// 
        /// </summary>
        public static readonly SipMethod Info= new SipMethod("INFO");

        /// <summary>
        /// 
        /// </summary>
        public static readonly SipMethod Invite= new SipMethod("INVITE");

        /// <summary>
        /// 
        /// </summary>
        public static readonly SipMethod Message= new SipMethod("MESSAGE");

        /// <summary>
        /// 
        /// </summary>
        public static readonly SipMethod Notify= new SipMethod("NOTIFY");

        /// <summary>
        /// 
        /// </summary>
        public static readonly SipMethod Ok= new SipMethod("OK");

        /// <summary>
        /// 
        /// </summary>
        public static readonly SipMethod Options= new SipMethod("OPTIONS");

        /// <summary>
        /// 
        /// </summary>
        public static readonly SipMethod Prack = new SipMethod("PRACK");

        /// <summary>
        /// 
        /// </summary>
        public static readonly SipMethod Publish= new SipMethod("PUBLISH");

        /// <summary>
        /// 
        /// </summary>
        public static readonly SipMethod Refer= new SipMethod("REFER");

        /// <summary>
        /// 
        /// </summary>
        public static readonly SipMethod Register = new SipMethod("REGISTER");

        /// <summary>
        /// 
        /// </summary>
        public static readonly SipMethod Subscribe= new SipMethod("SUBSCRIBE");

        /// <summary>
        /// 
        /// </summary>
        public static readonly SipMethod Update= new SipMethod("UPDATE");

        private string _value;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        /// <value>The method.</value>
        public string Method
        {
            get { return _value; }
            private set {
            value = value.Trim();
            PropertyVerifier.ThrowOnInvalidToken(value, "Method");  
            _value = value.ToUpperInvariant(); 
            }
        }

        #endregion Properties

        #region Constructors



        /// <summary>
        /// Initializes a new instance of the <see cref="SipMethod"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public SipMethod(string value)
            : this()
        {
        Method = value;
        String.Intern(value); //optomise access in future.
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.String"/> to <see cref="Konnetic.Sip.Messages.SipMethod"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns> 
        public static implicit operator SipMethod(string value)
        {
            if(String.IsNullOrEmpty(value))
            {
            return new SipMethod(string.Empty);
            }
            return new SipMethod(value);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Konnetic.Sip.Messages.SipMethod"/> to <see cref="System.String"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(SipMethod value)
        {
            return value.Method;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="s1">The first <see cref="SipMethod"/> instance.</param>
        /// <param name="s2">The second <see cref="SipMethod"/> instance.</param>
        /// <returns>The result of the operator.</returns>
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public static bool operator !=(SipMethod s1, SipMethod s2)
        {
            return !(s1.Equals(s2));
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="s1">The first <see cref="SipMethod"/> instance.</param>
        /// <param name="s2">The second <see cref="SipMethod"/> instance.</param>
        /// <returns>The result of the operator.</returns>
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public static bool operator ==(SipMethod s1, SipMethod s2)
        {
            return s1.Equals(s2);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// 	<c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public override bool Equals(object obj)
        {
            if(obj == null)
                {
                return false;
                }

            if(((object)this).GetType() != obj.GetType())
                {
                return false;
                }

            return this.Equals((SipMethod)obj);
        }

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public bool Equals(SipMethod other)
        {
            if((object)other == null)
                {
                return false;
                }
            return other.Method.Equals(this.Method);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Method;
        }

        #endregion Methods
    }
}