/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Collections.Generic;
using System.Timers;

using Konnetic.Sip.Messages;

namespace Konnetic.Sip
{
    public class InviteServerTransaction : ServerTransaction, IDisposable
    {
        #region Fields

        private InviteServerTransactionState _state;
        private Timer _timerG;
        private Timer _timerH;
        private Timer _timerI;
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

        internal InviteServerTransactionState State
        {
            get { return _state; }
            private set { _state = value; }
        }

        #endregion Properties

        #region Events

		public event SendingResponseReceivedEventHandler OnSendingTrying;

		public event SentResponseReceivedEventHandler OnSentTrying;

        #endregion Events

        #region Constructors

        public InviteServerTransaction(TransportType transport)
            : base()
        {
            Transport = transport;
            State = InviteServerTransactionState.Started;
            _timerG = new Timer(500);
            _timerH = new Timer(64*500);
            if(Transport == TransportType.Udp)
                {
                _timerI = new Timer(5000);
                }
            else
                {
                _timerI = new Timer(1);
                }

            _timerI.Elapsed += new ElapsedEventHandler(TimerI_Elapsed);
            _timerH.Elapsed += new ElapsedEventHandler(TimerH_Elapsed);
            _timerG.Elapsed += new ElapsedEventHandler(TimerG_Elapsed);

            //GC.KeepAlive(_timerG);
            //GC.KeepAlive(_timerH);
            //GC.KeepAlive(_timerI);
        }

        public InviteServerTransaction(Invite request, TransportType transport)
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
                throw new ObjectDisposedException("InviteServerTransaction");
                }
            if(request.Method == SipMethod.Invite)
                {
                Invite invite = null;
                try
                    {
                    invite = (Invite)request;
                    }
                catch(SipException ex)
                    {
                    State = InviteServerTransactionState.Completed;
                    //Send BadRequest
                    SendResponse(new Response(request,StandardResponseCode.BadRequest));
                    throw new SipException("Could not covert request to an Invite", ex);
                    }
                ProcessInviteRequest(invite);
                }
            else if(request.Method == SipMethod.Ack)
                {
                ProcessInviteAckRequest();
                }
            else{
                State = InviteServerTransactionState.Completed;
                //Send BadRequest
                SendResponse(new Response(request,StandardResponseCode.BadRequest));
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
                                if(_timerG!=null)
                                    _timerG.Dispose();
                                if(_timerH != null)
                                    _timerH.Dispose();
                                if(_timerI != null)
                                    _timerI.Dispose();
                                _timerG = null;
                                _timerH = null;
                                _timerI = null;
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

        private void ProcessInviteAckRequest()
        {
            if(disposed)
                {
                throw new ObjectDisposedException("InviteServerTransaction");
                }
            if(State == InviteServerTransactionState.Completed)
                {
                State = InviteServerTransactionState.Confirmed;

                _timerG.Dispose();
                _timerH.Dispose();
                _timerI.Enabled = true;
                }
        }

        private void ProcessInviteRequest(Invite invite)
        {
            if(disposed)
                {
                throw new ObjectDisposedException("InviteServerTransaction");
                }
            if(State == InviteServerTransactionState.Started)
                    {
                    Response t = new Response(invite,StandardResponseCode.Trying);
                    t.CopyHeaderFields(invite.Headers);
                    BeforeSendingResponseEventArgs b = new BeforeSendingResponseEventArgs(t);
                    if(OnSendingTrying != null)
                        {
                        OnSendingTrying(this, b);
                        }
                    if(!b.Cancel)
                        {
                        TransportClient.Send(t);
                        AfterSendingResponseEventArgs a = new AfterSendingResponseEventArgs(t);
                        if(OnSentTrying != null)
                            {
                            OnSentTrying(this, a);
                            }
                        }
                    State = InviteServerTransactionState.Proceeding;
                    }
                if(State == InviteServerTransactionState.Proceeding)
                    {
                    Response r = GetLastProvisionalResponse();
                    if(r == null)
                        {
                        SendResponse(new Response(invite, StandardResponseCode.Forbidden));
                        }
                    else
                        {
                        SendResponse(r);
                        }
                    }
                else if(State == InviteServerTransactionState.Completed)
                    {
                    Response r = GetLastNoneProvisionalResponse();
                    if(r==null)
                        {
                        SendResponse(new Response(invite, StandardResponseCode.Forbidden));
                        }
                    else{
                        SendResponse(r);
                        }
                    }
        }

        private void SendResponse(Response response)
        {
            if(disposed)
                {
                throw new ObjectDisposedException("InviteServerTransaction");
                }
            if(State == InviteServerTransactionState.Proceeding)
            {
            switch(response.Class)
                {
                case ResponseClass.Provisional:
                case ResponseClass.Successful:
                    TransportClient.Send(response);
                    break;
                case ResponseClass.GlobalFailure:
                case ResponseClass.RequestFailure:
                case ResponseClass.ServerFailure:
                case ResponseClass.Redirection:
                    SendErrorResponse(response);
                    State = InviteServerTransactionState.Completed;
                    _timerH.Enabled = true;
                    if(Transport == TransportType.Udp)
                        {
                        _timerG.Enabled = true;
                        }
                    break;
                }
            Responses.Add(response);
            }
        }

        void TimerG_Elapsed(object sender, ElapsedEventArgs e)
        {
            _timerG.Enabled = false;
            if(State == InviteServerTransactionState.Completed)
                {
                Response r = GetLastNoneProvisionalResponse();
                if(r != null)
                    {
                    TransportClient.Send(r);
                    }
                _timerG.Interval = Math.Min(2 * _timerG.Interval, 4000);
                }
            _timerG.Enabled = true;
        }

        void TimerH_Elapsed(object sender, ElapsedEventArgs e)
        {
            _timerH.Enabled = false;
            if(State == InviteServerTransactionState.Completed)
                {
                State = InviteServerTransactionState.Terminated;
                Dispose(true);
                }
        }

        void TimerI_Elapsed(object sender, ElapsedEventArgs e)
        {
            _timerI.Enabled = false;
            State= InviteServerTransactionState.Terminated;
            Dispose(true);
        }

        #endregion Methods
    }
}