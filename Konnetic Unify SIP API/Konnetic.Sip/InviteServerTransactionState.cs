/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;

namespace Konnetic.Sip
{
    #region Enumerations

    /// <summary>
    /// 
    /// </summary>
    public enum InviteServerTransactionState
    {
    /// <summary>
    /// 
    /// </summary>
        Started,
        /// <summary>
        /// 
        /// </summary>
        Proceeding,
        /// <summary>
        /// 
        /// </summary>
        Completed,
        /// <summary>
        /// 
        /// </summary>
        Confirmed,
        /// <summary>
        /// 
        /// </summary>
        Terminated,
        /// <summary>
        /// 
        /// </summary>
        Disposed
    }

    #endregion Enumerations
}