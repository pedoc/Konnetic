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
    public class NewRequestReceivedEventArgs : EventArgs
    {
        #region Fields

        private Request _request;
        private TransportType _transport;

        #endregion Fields

        #region Properties

        public Request @Request
        {
            get { return _request; }
              set { _request = value; }
        }

        public TransportType Transport
        {
            get { return _transport; }
            set { _transport = value; }
        }

        #endregion Properties

        #region Constructors

        internal NewRequestReceivedEventArgs(Request request, TransportType transport )
        {
            _transport = transport;
            _request = request;
        }

        #endregion Constructors
    }
}