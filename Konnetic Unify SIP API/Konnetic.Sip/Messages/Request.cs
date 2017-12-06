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
using System.Text;

using Konnetic.Sip.Headers;

namespace Konnetic.Sip.Messages
{
    /// <summary>
    /// Represents a SIP Request.
    /// </summary>
    /// <remarks>SIP Methods are represented as Requests.</remarks>
    public abstract class Request : SipMessage
    {
        #region Fields

        /// <summary>
        /// Represents the the SIP method associated with the Request.
        /// </summary> 
        private RequestLineHeaderField _requestLine;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the C seq.
        /// </summary>
        /// <value>The C seq.</value>
        public CSeqHeaderField CSeq
        {
            get
                {
                return (CSeqHeaderField)GetHeader(CSeqHeaderField.LongName);
                }
            set
                {
                Set(value);
                }
        }

        /// <summary>
        /// Gets or sets the Call-ID.
        /// </summary>
        /// <value>The Call-ID.</value>
        /// <remarks>A unique identifier. Use the static methods on CallIdHeaderField to generate a new value.</remarks>
        public CallIdHeaderField CallId
        {
            get
                {
                return (CallIdHeaderField)GetHeader(CallIdHeaderField.LongName);
                }
            set
                {
                Set(value);
                }
        }

        /// <summary>
        /// Gets or sets the From value.
        /// </summary>
        /// <value>The From value.</value>
        /// <remarks>Indicates the initiator of the request. Requests sent by the callee to the caller use the callee's address in the From HeaderField. The optional display-name is meant to be understood by a human. </remarks>
        public FromHeaderField From
        {
            get
                {
                return (FromHeaderField)GetHeader(FromHeaderField.LongName);
                }
            set
                {
                Set(value);
                }
        }

        /// <summary>
        /// Gets or sets the max forwards.
        /// </summary>
        /// <value>The max forwards.</value>
        public MaxForwardsHeaderField MaxForwards
        {
            get
                {
                return (MaxForwardsHeaderField)GetHeader(MaxForwardsHeaderField.LongName);
                }
            set
                {
                Set(value);
                }
        }

        /// <summary>
        /// Gets the type of the message.
        /// </summary>
        /// <value>The type of the message.</value>
        public override MessageType MessageType
        {
            get
                {
                return MessageType.Request;
                }
        }

        /// <summary>
        /// Gets or sets the SIP method. Represents the the SIP method associated with the Request.
        /// </summary>
        /// <value>The SIP method.</value>
        public SipMethod Method
        {
            get { return RequestLine.Method; }
            protected set { RequestLine.Method = value; }
        }

        /// <summary>
        /// Gets or sets the request line.
        /// </summary>
        /// <value>The request line.</value>
        public RequestLineHeaderField RequestLine
        {
            get
                {
                if(_requestLine == null)
                    {
                    _requestLine = new RequestLineHeaderField();
                    }
                return _requestLine;
                }
            set
                {
                _requestLine = value;
                }
        }

        /// <summary>
        /// Gets or sets the To value.
        /// </summary>
        /// <value>The To value.</value>
        /// <remarks>Indicates the logical recipient of the request. The optional display-name is meant to be understood by a human. </remarks>
        public ToHeaderField To
        {
            get
                {
                return (ToHeaderField)GetHeader(ToHeaderField.LongName);
                }
            set
                {
                RequestLine.RequestUri = value.Uri;
                Set(value);
                }
        }

        /// <summary>
        /// Gets or sets the via.
        /// </summary>
        /// <value>The via.</value>
        public HeaderFieldGroup<ViaHeaderField> Via
        {
            get
                {
                return (HeaderFieldGroup<ViaHeaderField>)GetHeader(ViaHeaderField.LongName);
                }
            set
                {
                Set(value);
                }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Request"/> class.
        /// </summary>
        /// <remarks>Calls CreateHeader() to add default HeaderFields.</remarks>
        protected Request()
            : base()
        {
        RequestLine = new RequestLineHeaderField();
        Method = SipMethod.Empty;  
        HeaderFieldGroup<ViaHeaderField> hfgVia = new HeaderFieldGroup<ViaHeaderField>();
        hfgVia.Add(new ViaHeaderField());
        TryAddHeader(hfgVia);
        TryAddHeader(new ToHeaderField());
        TryAddHeader(new FromHeaderField());
        TryAddHeader(new CallIdHeaderField());
        TryAddHeader(new MaxForwardsHeaderField());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Request"/> class.
        /// </summary>
        /// <param name="method">A SIP method to assign to the Request.</param>
        protected Request(SipMethod method)
            : this()
        {
            Method = method;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Request"/> class.
        /// </summary>
        /// <param name="method">A SIP method to assign to the Request.</param>
        protected Request(string method)
            : this()
        {
            Method = new SipMethod(method);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Returns a byte array of the header.
        /// </summary>
        /// <param name="useCompactForm"></param>
        /// <returns></returns>
        public override byte[] GetHeaderBytes(bool useCompactForm)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(RequestLine.ToString());
            sb.Append("\r\n");
            sb.Append(Headers.ToString(useCompactForm));

            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        /// <summary>
        /// Determines whether this instance is valid.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public override bool IsValid()
        {
            if(!RequestLine.IsValid())
                {
                return false;
                }

            if(To == null || From == null || CallId == null || CSeq == null || Via == null || MaxForwards == null)
                {
                return false;
                }

            if(!To.IsValid() || !From.IsValid() || !CallId.IsValid() || !CSeq.IsValid() || !Via.IsValid() || !MaxForwards.IsValid())
                {
                return false;
                }

            return true;
        }

 

        #endregion Methods
    }
}