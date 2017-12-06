/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;

namespace Konnetic.Sip.Headers
{
    /// <summary>
    /// Provides data for the HeaderFieldChange event. 
    /// </summary>
    public class HeaderFieldEventArgs : EventArgs
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private bool _cancel;

        /// <summary>
        /// 
        /// </summary>
        private string _headerFieldName;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the event should be canceled.
        /// </summary>
		/// <value><c>true</c> if cancel; otherwise, <c>false</c>.</value>
        public bool Cancel
            {
            [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
            get { return _cancel; }
            internal set { _cancel = value; }
        }

        /// <summary>
        /// Gets or sets the name of the HeaderField.
        /// </summary>
        /// <value>The name of the HeaderField.</value>
        public string HeaderFieldName
        {
            get { return _headerFieldName; }
            internal set { _headerFieldName = value; }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Konnetic.Sip.Headers.HeaderFieldEventArgs"/> class.
        /// </summary>
        /// <param name="cancel">If set to <c>true</c> cancel the operation.</param>
        /// <param name="headerFieldName">Name of the HeaderField.</param>
        public HeaderFieldEventArgs(bool cancel, string headerFieldName)
        {
            _cancel = cancel;
            _headerFieldName = headerFieldName;
        }

        #endregion Constructors
    }
}