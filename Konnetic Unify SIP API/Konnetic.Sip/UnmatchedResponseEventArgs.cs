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
    public class UnmatchedResponseEventArgs : EventArgs
    {
        #region Fields

        private Response _response;

        #endregion Fields

        #region Properties

        public Response @Response
        {
            get { return _response; }
              set { _response = value; }
        }

        #endregion Properties

        #region Constructors

        internal UnmatchedResponseEventArgs(Response response)
        {
            _response = response;
        }

        #endregion Constructors
    }
}