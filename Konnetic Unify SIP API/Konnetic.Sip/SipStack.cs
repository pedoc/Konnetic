/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Collections.Generic;
using System.Text; 
using System.ComponentModel;

namespace Konnetic.Sip
{
    /// <summary>
    /// 
    /// </summary>
    public class SipStack : IDisposable
    {
        #region Constructors

        public SipStack()
        {
            TransportServer.Start();
        }

        #endregion Constructors

        #region Methods

        public virtual void Dispose(bool disposing)
        {
            //throw new NotImplementedException();
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public void StopListening(TransportType type)
        {
            //TODO stop listening.
        }

        #endregion Methods
    }
 

}