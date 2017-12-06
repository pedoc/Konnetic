/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Konnetic.Sip
{
    /// <summary>The exception that is thrown when a generic SIP error occur.
    /// </summary>
    /// <remarks>
    /// The <see cref="T:Konnetic.Sip.SipException"/> class differentiates between exceptions defined by SIP versus exceptions defined by the system.</remarks>
    [Serializable]
    public class SipException : Exception, ISerializable
    {
        #region Constructors


    /// <summary>
    /// Initializes a new instance of the <see cref="T:Konnetic.Sip.SipException"/> class.
    /// </summary>
    /// <remarks>
    /// This constructor initializes the <see cref="P:System.Exception.Message"/> property of the new instance to an empty string.
    /// </remarks>
        public SipException()
            : this(String.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Konnetic.Sip.SipException"/> class with a specified error message and the exception that caused this exception.
        /// </summary>
        /// <remarks>This constructor initializes the <see cref="P:System.Exception.Message"/> property of the new instance to the passed <paramref name="message"/> argument. The content of <paramref name="message"/> is intended to be understood by administrators examining this exception.</remarks>
        /// <param name="message">The error message that explains the reason for this exception. </param>
        public SipException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Konnetic.Sip.SipException"/> class with a specified error message and the exception that caused this exception and the exception that caused this exception.
        /// </summary>
        /// <remarks>This constructor initializes the <see cref="P:System.Exception.Message"/> property of the new instance to the passed <paramref name="message"/> argument. The content of <paramref name="message"/> is intended to be understood by administrators examining this exception.</remarks>
        /// <param name="message">The error message that explains the reason for this exception. </param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<b>Nothing</b> in Visual Basic) if no inner exception is specified.</param>
        public SipException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Konnetic.Sip.SipException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// The <paramref name="info"/>  parameter is null (<b>Nothing</b> in Visual Basic).
        /// </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">
        /// The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0).
        /// </exception>
        protected SipException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// The <paramref name="info"/> parameter is a null reference (Nothing in Visual Basic).
        /// </exception>
        /// <PermissionSet>
        /// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*"/>
        /// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter"/>
        /// </PermissionSet>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            if(info != null)
                {
                }
        }

        #endregion Methods
    }
}