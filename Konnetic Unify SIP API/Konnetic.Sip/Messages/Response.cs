/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;

using Konnetic.Sip.Headers;

namespace Konnetic.Sip.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public class Response : SipMessage
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private ResponseClass _responseClass;

        /// <summary>
        /// 
        /// </summary>
        private StatusLineHeaderField _statusLine;

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
        /// Gets or sets the call id.
        /// </summary>
        /// <value>The call id.</value>
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
        /// Gets or sets the class.
        /// </summary>
        /// <value>The class.</value>
        public ResponseClass Class
        {
            get { return _responseClass; }
            protected set { _responseClass = value; }
        }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
        public Int16? Code
        {
            get { return StatusLine.StatusCode; }
            protected set { StatusLine.StatusCode = value; }
        }

        /// <summary>
        /// Gets or sets from.
        /// </summary>
        /// <value>From.</value>
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
        /// Gets the type of the message.
        /// </summary>
        /// <value>The type of the message.</value>
        public override MessageType MessageType
        {
            get
                {
                return MessageType.Response;
                }
        }

        /// <summary>
        /// Gets or sets the reason phrase.
        /// </summary>
        /// <value>The reason phrase.</value>
        public string ReasonPhrase
        {
            get { return StatusLine.ReasonPhrase; }
            set { StatusLine.ReasonPhrase = value; }
        }

        /// <summary>
        /// Gets or sets the status line.
        /// </summary>
        /// <value>The status line.</value>
        public StatusLineHeaderField StatusLine
        {
            get { return _statusLine; }
            set { _statusLine = value; }
        }

        /// <summary>
        /// Gets or sets to.
        /// </summary>
        /// <value>To.</value>
        public ToHeaderField To
        {
            get
                {
                return (ToHeaderField)GetHeader(ToHeaderField.LongName);
                }
            set
                {
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
        /// Initializes a new instance of the <see cref="Response"/> class.
        /// </summary>
        public Response()
        {
            StatusLine = new StatusLineHeaderField();
            Code = (Int16)StandardResponseCode.None;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Response"/> class.
        /// </summary>
        /// <param name="request">The request.</param>
        public Response(Request request)
            : this()
        {
            CopyHeaderFields(request.Headers);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Response"/> class.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="code">The code.</param>
        public Response(Request request,StandardResponseCode code)
            : this(request,(Int16)code)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Response"/> class.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="code">The code.</param>
        public Response(Request request, Int16 code)
            : this(request)
        {
            if(code < 100 || code > 699)
                {
                throw new SipException("Unknown Response class. Must be between 100 and 699");
                }

            if(code >= 600)
                {
                Class = (ResponseClass)600;
                }
            if(code >= 500)
                {
                Class = (ResponseClass)500;
                }
            if(code >= 400)
                {
                Class = (ResponseClass)400;
                }

            if(code >= 300)
                {
                Class = (ResponseClass)300;
                }

            if(code >= 200)
                {
                Class = (ResponseClass)200;
                }

            if(code >= 100)
                {
                Class = (ResponseClass)100;
                }

            ReasonPhrase = ResponsePhrases.LookupPhrase(code);
            if(string.IsNullOrEmpty(ReasonPhrase))
                {
                throw new SipException("No registered Reason Phrase. Add code and phrase to ResponsePhrases.");
                }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Response"/> class.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="code">The code.</param>
        internal Response(Response request, StandardResponseCode code)
            : this()
        {
            Code = (Int16)code;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Copies the HeaderFields.
        /// </summary>
        /// <param name="headers">The headers.</param>
        public void CopyHeaderFields(HeaderFieldCollection headers)
        {
            foreach(HeaderFieldBase hf in headers)
                {
                    Headers.Add(hf.Clone());
                }
        }

        /// <summary>
        /// Returns a byte array of the header.
        /// </summary>
        /// <param name="useCompactForm"></param>
        /// <returns></returns>
        public override byte[] GetHeaderBytes(bool useCompactForm)
        {
            throw new NotImplementedException();
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
            if(!StatusLine.IsValid())
            {
            return false;
            }

            if(!(Via != null && To != null && From != null && CallId!=null && CSeq!=null))
            {
            return false;
            }

            if(!Via.IsValid() || !To.IsValid() || !From.IsValid() || !CallId.IsValid() || !CSeq.IsValid())
            {
            return false;
            }

            return true;
        }

 

        #endregion Methods
    }
}