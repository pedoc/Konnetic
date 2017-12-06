/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Konnetic.Sip
{
    #region Enumerations

    /// <summary>
    /// 
    /// </summary>
    public enum DialogState
    {
    /// <summary>
    /// 
    /// </summary>
        Unknown,
        /// <summary>
        /// 
        /// </summary>
        Confirmed,
        /// <summary>
        /// 
        /// </summary>
        Terminating,
        /// <summary>
        /// 
        /// </summary>
        Terminated,
        /// <summary>
        /// 
        /// </summary>
        Disposed,
        /// <summary>
        /// 
        /// </summary>
        Invalid,
    }

    #endregion Enumerations
}