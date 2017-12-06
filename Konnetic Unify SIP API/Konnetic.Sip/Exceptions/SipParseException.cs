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
    /// <summary>The exception that is thrown when an error occurs whilst parsing. 
    /// </summary>
    /// <remarks>
    /// <see cref="T:Konnetic.Sip.SipParseException"/> is thrown when the parse method is invoked and it is detected that the passed arguments would result in illegal SIP syntax or semantics. It does not signify an invalid object.  
    /// </remarks>
    [Serializable]
    public class SipParseException : SipException
    {
        #region Constructors

    private string _propertyName;

    /// <summary>
    /// Gets or sets the name of the property that was being parsed when the error occured.
    /// </summary>
    /// <value>The name of the property.</value>
    public string PropertyName
        {
        get { return _propertyName; }
        set { _propertyName = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Konnetic.Sip.SipParseException"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor initializes the <see cref="P:System.Exception.Message"/> property of the new instance to an empty string.
        /// <para> 
    /// This constructor initializes the <see cref="P:Konnetic.Sip.SipParseException.PropertyName"/> property of the new instance to an empty string.
        /// </remarks>
        public SipParseException()
			: this(String.Empty,String.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Konnetic.Sip.SipParseException"/> class with the name of the property that caused the parsing error and a specified error message.
        /// </summary>
        /// <remarks>
        /// This constructor initializes the <see cref="P:Konnetic.Sip.SipParseException.PropertyName"/> property of the new instance to the passed <paramref name="propertyName"/> argument. The content of <paramref name="propertyName"/> is intended to be understood by administrators examining this exception.
        /// <para>This constructor initializes the base <see cref="P:System.Exception.Message"/> property of the new instance using the value of the <paramref name="message"/> parameter.  The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</para>
        /// </remarks>
        /// <param name="propertyName">The property that caused the parsing error.</param>
        /// <param name="message">The error message that explains the reason for this exception.</param>
		public SipParseException(string propertyName, string message)
            : base(message)
        {
		_propertyName = propertyName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Konnetic.Sip.SipParseException"/> class with a specified error message and the exception that caused this exception..
        /// </summary>
        /// <remarks>
        /// This constructor initializes the base <see cref="P:System.Exception.Message"/> property of the new instance using the value of the <paramref name="message"/> parameter.  The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</para>
        /// This constructor initializes the <see cref="P:Konnetic.Sip.SipParseException.PropertyName"/> property of the new instance to an empty string.
        /// </remarks>
        /// <param name="message">The error message that explains the reason for this exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<b>Nothing</b> in Visual Basic) if no inner exception is specified.</param>
        public SipParseException(string message, Exception innerException)
            : base(message, innerException)
			{
			_propertyName = string.Empty;
			}
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Konnetic.Sip.SipParseException"/> class with the name of the property that caused the parsing error and a specified error message and the exception that caused this exception.
        /// </summary>
        /// <remarks>
        /// This constructor initializes the <see cref="P:Konnetic.Sip.SipParseException.PropertyName"/> property of the new instance to the passed <paramref name="propertyName"/> argument. The content of <paramref name="propertyName"/> is intended to be understood by administrators examining this exception.
        /// <para>This constructor initializes the base <see cref="P:System.Exception.Message"/> property of the new instance using the value of the <paramref name="message"/> parameter.  The content of the message parameter is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</para>
        /// </remarks>
        /// <param name="propertyName">The property that caused the parsing error.</param>
        /// <param name="message">The error message that explains the reason for this exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<b>Nothing</b> in Visual Basic) if no inner exception is specified.</param>
        public SipParseException(string propertyName, string message, Exception innerException)
            : base(message, innerException)
			{
			_propertyName = propertyName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SipParseException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// The <paramref name="info"/>  parameter is null (<b>Nothing</b> in Visual Basic).
        /// </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">
        /// The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0).
        /// </exception>
		protected SipParseException(SerializationInfo info, StreamingContext context)
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

        /// <summary>
        /// Gets a message that describes the current exception.
        /// </summary> 
        /// <returns>
        /// The error message that explains the reason for the exception, or an empty string("").
        /// </returns>
		public override string Message
			{
			get
				{
				string message = base.Message;
				if(!string.IsNullOrEmpty(_propertyName))
					{
					return (message + Environment.NewLine + string.Format(CultureInfo.CurrentCulture, "Property {0} parse exception.", _propertyName));
					}
				return message;

				}
			}
        #endregion Methods
    }
}