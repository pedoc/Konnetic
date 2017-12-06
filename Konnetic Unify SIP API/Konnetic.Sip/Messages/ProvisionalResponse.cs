/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;

using Konnetic.Sip.Headers;

namespace Konnetic.Sip.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ProvisionalResponse : Response
    {
        #region Properties

        /// <summary>
        /// Gets or sets the contact.
        /// </summary>
        /// <value>The contact.</value>
        public HeaderFieldGroup<ContactHeaderField> Contact
        {
            get
                {
                return (HeaderFieldGroup<ContactHeaderField>)GetHeader(ContactHeaderField.LongName);
                }
            set
                {
                Set(value);
                }
        }

        #endregion Properties
    }
}