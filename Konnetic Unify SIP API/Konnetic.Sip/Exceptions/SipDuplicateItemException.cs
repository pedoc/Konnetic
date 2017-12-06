/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Globalization;

namespace Konnetic.Sip
{
    /// <summary>
    /// The exception that is thrown when an item is passed to a method that would result in an illegal duplicate item in a collection.  
    /// </summary>
    /// <remarks>
    /// <see cref="T:Konnetic.Sip.SipDuplicateItemException"/> is thrown when a method is invoked and it is detected that the passed arguments would result in illegal duplication within a collection.  
    /// </remarks>
    [Serializable]
    public class SipDuplicateItemException : SipException, ISerializable
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private string _duplicatedItem;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the name of the HeaderField.
        /// </summary>
        /// <value>The name of the HeaderField.</value>
        public string DuplicatedItem
        {
        get { return _duplicatedItem; }
        set { _duplicatedItem = value; }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Konnetic.Sip.SipDuplicateItemException"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor initializes the <see cref="P:Konnetic.Sip.SipDuplicateItemException.DuplicatedItem"/> property of the new instance to an empty string.
        /// </remarks>
        public SipDuplicateItemException()
            : base(String.Empty)
        {
        DuplicatedItem = String.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Konnetic.Sip.SipDuplicateItemException"/> class with the name of the item that would have caused the duplication.
        /// </summary>
        /// <remarks>This constructor initializes the <see cref="P:Konnetic.Sip.SipDuplicateItemException.DuplicatedItem"/> property of the new instance to the passed <paramref name="item"/> argument. The content of <paramref name="item"/> is intended to be understood by administrators examining this exception.</remarks>
        /// <param name="item">The name of the parameter that caused the duplication.</param>
        public SipDuplicateItemException(string item)
        {
        DuplicatedItem = item;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Konnetic.Sip.SipDuplicateItemException"/> class with the name of the item that would have caused the duplication and the exception that caused this exception.
        /// </summary>
        /// <remarks>This constructor initializes the <see cref="P:Konnetic.Sip.SipDuplicateItemException.DuplicatedItem"/> property of the new instance to the passed <paramref name="item"/> argument. The content of <paramref name="item"/> is intended to be understood by administrators examining this exception.</remarks>
        /// <param name="item">The item that caused the duplication.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<b>Nothing</b> in Visual Basic) if no inner exception is specified.</param>
        public SipDuplicateItemException(string item, Exception innerException)
            : base(item, innerException)
        {
        DuplicatedItem = item;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Konnetic.Sip.SipDuplicateItemException"/> class with the name of the item that would have caused the duplication and a specified error message and the exception that caused this exception.
        /// </summary>
        /// <remarks>This constructor initializes the <see cref="P:Konnetic.Sip.SipDuplicateItemException.DuplicatedItem"/> property of the new instance to the passed <paramref name="item"/> argument. The content of <paramref name="item"/> is intended to be understood by administrators examining this exception.
        /// <para>This constructor initializes the base <see cref="P:Konnetic.Sip.SipException.Message"/> property of the new instance using the value of the <paramref name="message"/> parameter.  The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</para>
        /// </remarks>
        /// <param name="item">The item that caused the duplication.</param>
        /// <param name="message">The error message that explains the reason for this exception. </param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<b>Nothing</b> in Visual Basic) if no inner exception is specified.</param>
        public SipDuplicateItemException(string item, string message, Exception innerException)
            : base(message, innerException)
        {
        DuplicatedItem = item;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Konnetic.Sip.SipDuplicateItemException"/> class with the name of the item that would have caused the duplication and a specified error message.
        /// </summary>
        /// <remarks>This constructor initializes the <see cref="P:Konnetic.Sip.SipDuplicateItemException.DuplicatedItem"/> property of the new instance to the passed <paramref name="item"/> argument. The content of <paramref name="item"/> is intended to be understood by administrators examining this exception.
        /// <para>This constructor initializes the base <see cref="P:Konnetic.Sip.SipException.Message"/> property of the new instance using the value of the <paramref name="message"/> parameter.  The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</para>
        /// </remarks>
        /// <param name="item">The item that caused the duplication.</param>
        /// <param name="message">The error message that explains the reason for this exception.</param>
        public SipDuplicateItemException(string item, string message)
            : base(message)
            {
            DuplicatedItem = item;
            }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Konnetic.Sip.SipDuplicateItemException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// The <paramref name="info"/>  parameter is null (<b>Nothing</b> in Visual Basic).
        /// </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">
        /// The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0).
        /// </exception>
        protected SipDuplicateItemException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if(info != null)
                {
                DuplicatedItem = info.GetString("item");
                }
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
                info.AddValue("item", DuplicatedItem);
                }
        }

        /// <summary>
        /// Gets a message that describes the current exception.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The error message that explains the reason for the exception, or an empty string("").
        /// </returns>
        public override string Message
            {
            get
                {
                string message = base.Message;
                if(!string.IsNullOrEmpty(_duplicatedItem))
                    {
                    return (message + Environment.NewLine + string.Format(CultureInfo.CurrentCulture, "Duplicated item: {0}", _duplicatedItem));
                    }
                return message;

                }
            }

        #endregion Methods
    }
}