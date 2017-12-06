/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Timers;

using Konnetic.Sip.Messages;

namespace Konnetic.Sip
{
    /// <summary>
    /// 
    /// </summary>
    public class NoneInviteServerTransaction : ServerTransaction, IDisposable
    {
        #region Fields

        private NoneInviteServerTransactionState _state;
        private Timer _timerJ;
        private TransportType _transport;
        private bool disposed;
        private object syncLock = new object();

        #endregion Fields

        #region Properties

        public TransportType Transport
        {
            get { return _transport; }
            set { _transport = value; }
        }

        internal NoneInviteServerTransactionState State
        {
            get { return _state; }
            private set { _state = value; }
        }

        #endregion Properties

        #region Constructors

        public NoneInviteServerTransaction(TransportType transport)
        {
            Transport = transport;
            State = NoneInviteServerTransactionState.Started;

            if(Transport == TransportType.Udp)
                {
                _timerJ = new Timer(64*500);
                }
            else
                {
                _timerJ = new Timer(1);
                }
            _timerJ.Elapsed += new ElapsedEventHandler(TimerJ_Elapsed);
        }

        public NoneInviteServerTransaction(Request request, TransportType transport)
            : this(transport)
        {
            ProcessRequest(request);
        }

        #endregion Constructors

        #region Methods

		public new void Dispose()
        {
            Dispose(true);
        }

        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)] 
        public override bool IsValid()
        {
            //TODO implement
            return true;
        }

        public override void ProcessRequest(Request request)
        {
            if(disposed)
            {
            throw new ObjectDisposedException("NoneInviteServerTransaction");
            }
            Response r = null;
            switch(State)
            {
            case NoneInviteServerTransactionState.Started:
                State = NoneInviteServerTransactionState.Trying;
                break;
            case NoneInviteServerTransactionState.Proceeding:
                r = GetLastProvisionalResponse();
                if(r == null)
                    {
                    SendResponse(new Response(request,StandardResponseCode.Forbidden));
                    }
                else
                    {
                    SendResponse(r);
                    }
                break;
            case NoneInviteServerTransactionState.Completed:
                r = GetLastNoneProvisionalResponse();
                if(r==null)
                    {
                    SendResponse(new Response(request,StandardResponseCode.Forbidden));
                    }
                else{
                    SendResponse(r);
                    }
                break;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if(!disposed)
                {
                    try
                        {
                        lock(syncLock)
                            {
                            if(_timerJ != null)
                                _timerJ.Dispose();
                            _timerJ = null;
                            }
                        disposed = true;
                        }
                    finally
                        {
                        if(disposing)
                            {
                            GC.SuppressFinalize(this);
                            }
                        base.Dispose(disposing);
                        }
                }
        }

        private void SendResponse(Response response)
        {
            if(disposed)
                {
                throw new ObjectDisposedException("NoneInviteServerTransaction");
                }
            if(State == NoneInviteServerTransactionState.Proceeding || State == NoneInviteServerTransactionState.Trying)
                    {
                        switch(response.Class)
                            {
                            case ResponseClass.Provisional:
                                try
                                    {
                                    TransportClient.Send(response);
                                    if(State == NoneInviteServerTransactionState.Trying)
                                        {
                                        State = NoneInviteServerTransactionState.Proceeding;
                                        }
                                    }
                                catch(SipTransportException)
                                    {
                                    Terminate();
                                    }
                                break;
                            case ResponseClass.Successful:
                            case ResponseClass.GlobalFailure:
                            case ResponseClass.RequestFailure:
                            case ResponseClass.ServerFailure:
                            case ResponseClass.Redirection:
                                SendErrorResponse(response);
                                State = NoneInviteServerTransactionState.Completed;
                                _timerJ.Enabled = true;
                                break;
                            }

                        Responses.Add(response);

                    }
        }

        private void Terminate()
        {
            if(disposed)
                {
                return;
                }
            State = NoneInviteServerTransactionState.Terminated;
            Dispose(true);
        }

        void TimerJ_Elapsed(object sender, ElapsedEventArgs e)
        {
            _timerJ.Enabled = false;
            Terminate();
        }

        #endregion Methods
    }
}