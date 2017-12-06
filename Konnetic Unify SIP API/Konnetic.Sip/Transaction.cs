/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;

using Konnetic.Sip.Messages;

namespace Konnetic.Sip
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Transaction : IDisposable
    {
        #region Fields

        private SipMethod _method;
        private bool disposed;
       // private object syncLock = new object();

        #endregion Fields

        #region Properties

        public string Key
        {
            get;
            protected set;
        }

        public SipMethod Method
        {
            get { return _method; }
            set { _method = value; }
        }

        #endregion Properties

        #region Events

		public event SendingResponseReceivedEventHandler OnSendingErrorResponse;

		public event SentResponseReceivedEventHandler OnSentErrorResponse;

        #endregion Events

        #region Methods

        public void Close()
        {
            Dispose(true);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public abstract bool IsValid();

        protected virtual void Dispose(bool disposing)
        {
            if(!disposed)
                {
                    try
                        {
                        _method = null;
                        disposed = true;
                        }
                    finally
                        {
                        if(disposing)
                            {
                            GC.SuppressFinalize(this);
                            }
                        }
                }
        }

        protected void SendErrorResponse(Response response)
        {
            BeforeSendingResponseEventArgs b = new BeforeSendingResponseEventArgs(response);
            if(OnSendingErrorResponse != null)
                {
                OnSendingErrorResponse(this, b);
                }
            if(!b.Cancel)
                {
                TransportClient.Send(response);
                AfterSendingResponseEventArgs a = new AfterSendingResponseEventArgs(response);
                if(OnSentErrorResponse != null)
                    {
                    OnSentErrorResponse(this, a);
                    }
                }
        }

        #endregion Methods
    }
}