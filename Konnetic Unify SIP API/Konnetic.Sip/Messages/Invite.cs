/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Collections.Generic;

using Konnetic.Sip.Headers;

namespace Konnetic.Sip.Messages
{
    /// <summary>
    /// Represents an Invite Request, which is responsible for representing valid data for an Invite Method.
    /// </summary>
    public sealed class Invite : Request
    {
        #region Properties

        /// <summary>
        /// Gets or sets the proxy authentication.
        /// </summary>
        /// <value>The proxy authentication.</value>
        public SchemeAuthHeaderFieldBase ProxyAuthentication
        {
            get
                {
                //TODO get LongName
                return (SchemeAuthHeaderFieldBase)GetHeader("p");
                }
            set
                {
                Set(value);
                }
        }

        /// <summary>
        /// Gets or sets the route.
        /// </summary>
        /// <value>The route.</value>
        public HeaderFieldGroup<RouteHeaderField> Route
        {
            get
                {
                return (HeaderFieldGroup<RouteHeaderField>)GetHeader(RouteHeaderField.LongName);
                }
            set
                {
                Set(value);
                }
        }

        /// <summary>
        /// Gets or sets the supported.
        /// </summary>
        /// <value>The supported.</value>
        public SupportedHeaderField Supported
        {
            get
                {
                return (SupportedHeaderField)GetHeader(SupportedHeaderField.LongName);
                }
            set
                {
                Set(value);
                }
        }

        /// <summary>
        /// Gets or sets the WWW authentication.
        /// </summary>
        /// <value>The WWW authentication.</value>
        public SchemeAuthHeaderFieldBase WwwAuthentication
        {
            get
            {
            //TODO get LongName
                return (SchemeAuthHeaderFieldBase)GetHeader("w");
                }
            set
                {
                Set(value);
                }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Invite"/> class.
        /// </summary>
        public Invite()
            : base()
        {
            Method = SipMethod.Invite;

            TryAddHeader(new RouteHeaderField());
            TryAddHeader(new CSeqHeaderField());
            CSeq.RecreateSequence();
            CSeq.Method = this.Method;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Invite"/> class.
        /// </summary>
        /// <param name="to">The logical recipient.</param>
        public Invite(SipUri to )
            : this()
        {
            To = new ToHeaderField(to);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Invite"/> class.
        /// </summary>
        /// <param name="to">The logical recipient.</param>
        /// <param name="from">The initiator of the request.</param>
        public Invite(string to, string from )
            : this(new SipUri(to), new SipUri(from))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Invite"/> class.
        /// </summary>
        /// <param name="to">The logical recipient.</param>
        /// <param name="from">The initiator of the request.</param>
        public Invite(SipUri to, SipUri from)
            : this(to)
        {
            From = new FromHeaderField(from);
            From.RecreateTag();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Determines whether this instance is valid.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public override bool IsValid()
        {
            if(ProxyAuthentication!=null && WwwAuthentication!=null && Supported != null && Route!=null)
                {
                return false;
                }
            return base.IsValid() && ProxyAuthentication.IsValid() && WwwAuthentication.IsValid() && Supported.IsValid() && Route.IsValid();
        }

 

        #endregion Methods
    }
}