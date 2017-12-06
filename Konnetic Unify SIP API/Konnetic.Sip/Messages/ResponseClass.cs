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
    public enum ResponseClass
    {
        /// <summary>
        ///
        /// </summary>
        Unknown=0,
        /// <summary>
        ///
        /// </summary>
        Provisional=100,
        /// <summary>
        ///
        /// </summary>
        Successful=200,
        /// <summary>
        ///
        /// </summary>
        Redirection=300,
        /// <summary>
        ///
        /// </summary>
        RequestFailure=400,
        /// <summary>
        ///
        /// </summary>
        ServerFailure=500,
        /// <summary>
        ///
        /// </summary>
        GlobalFailure=600
    }

    #endregion Enumerations
}