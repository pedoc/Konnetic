/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;

namespace Konnetic.Sip.Headers
{
    #region Enumerations

    /// <summary>
    /// Indicates the urgency of the request as perceived by the client
    /// </summary>
    [Serializable]
    public enum Priority
    {
        /// <summary>
        /// The default enumeration. No priority.
        /// </summary>
        None,
        /// <summary>
        /// When life, limb, or property are in imminent danger.
        /// </summary>
        Emergency,
        /// <summary>
        /// Compelling or requiring immediate action or attention.
        /// </summary>
        Urgent,
        /// <summary>
        /// Typical priority.
        /// </summary>
        Normal,
        /// <summary>
        /// Not urgent or informational.
        /// </summary>
        NonUrgent
    }

    #endregion Enumerations
}