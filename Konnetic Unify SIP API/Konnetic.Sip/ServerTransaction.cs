/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Konnetic.Sip.Messages;

namespace Konnetic.Sip
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ServerTransaction : Transaction, IDisposable
    {
        #region Fields

        private Collection<Response> _responses;
        private bool disposed;
       // private object syncLock = new object();

        #endregion Fields

        #region Properties

        public Collection<Response> Responses
        {
            get { return _responses; }
            set { _responses = value; }
        }

        #endregion Properties

        #region Constructors

        protected ServerTransaction()
        {
        }

        protected ServerTransaction(Request request)
            : this()
        {
            ProcessRequest(request);
        }

        #endregion Constructors

        #region Methods

        public new void Dispose()
        {
            Dispose(true);
        }

        public Response GetLastNoneProvisionalResponse()
        {
            foreach(Response r in Responses)
                    {
                    if(r.Code != (Int16)ResponseClass.Provisional)
                        {
                        return r;
                        }
                    }

                return null;
        }

        public Response GetLastProvisionalResponse()
        {
            foreach(Response r in Responses)
                {
                if(r.Code == (Int16)ResponseClass.Provisional)
                    {
                    return r;
                    }
                }

            return null;
        }

        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public override bool IsValid()
        {
            return true;
        }

        public abstract void ProcessRequest(Request request);

        protected override void Dispose(bool disposing)
        {
            if(!disposed)
                {
                    try
                        {
                        _responses = null;
                        disposed = true;
                        }
                    finally
                        {
                        if(disposing)
                            {
                            GC.SuppressFinalize(this);
                            base.Dispose(disposing);
                            }
                        }
                }
        }

        #endregion Methods
    }
}