/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

using Konnetic.Sip.Headers;

namespace Konnetic.Sip.Messages
{
    /// <summary>
    /// Represents a SIP message that can be sents and received by the client.
    /// </summary>
    /// <remarks>
    /// Can be used to represent a packet.
    /// </remarks>
    public abstract class SipMessage
    {
        #region Fields

        /// <summary>
        /// Represents the header of the message. Responsible for listing the headerfields of the message.
        /// </summary>
        /// <remarks>The HeaderFields are similar to HTTP HeaderFields.</remarks>
        /// <standard>RFC3261: 7.3</standard>
        /// <standard>Conforms to RFC2234</standard>
        private HeaderFieldCollection _headerFields;

        /// <summary>
        /// Represents the data payload of the message. Responsible for storing the data for the message. 
        /// </summary>
        /// <remarks>Typically the body will be SDP (Session Description Protocol).</remarks>
        private byte[] _body;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the body. Represents the data payload of the message. Responsible for storing the data for the message. 
        /// </summary>
        /// <value>The body.</value>
        /// <remarks>Typically the body will be SDP (Session Description Protocol).</remarks>
        public byte[] Body
        {
            get { return _body; }
            set { if(value!=null){_body = value;} }
        }

        /// <summary>
        /// Represents the header of the message. Responsible for listing the headerfields of the message.
        /// </summary>
        /// <value>The header.</value>
        /// <remarks>The HeaderFields are similar to HTTP HeaderFields.</remarks>
        /// <standard>RFC3261: 7.3</standard>
        /// <standard>Conforms to RFC2234</standard>
        public HeaderFieldCollection Headers
        {
            get { return _headerFields; }
            set { _headerFields = value; }
        }

        /// <summary>
        /// Gets the type of the message.
        /// </summary>
        /// <value>The type of the message.</value>
        public abstract MessageType MessageType
        {
            get;
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SipMessage"/> class.
        /// </summary>
        protected SipMessage()
        {
            _headerFields = new HeaderFieldCollection();
            //TODO add a ContentLength header
            _body = new byte[0];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SipMessage"/> class.
        /// </summary>
        protected SipMessage(byte[] body)
        {
            _body = body;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SipMessage"/> class.
        /// </summary>
        /// <param name="headerFields">The HeaderFields.</param>
        protected SipMessage(HeaderFieldCollection headerFields)
            : this()
        {
            _headerFields = headerFields;
        }

        //TODO Constructor that takes SDP data for the body
        /// <summary>
        /// Initializes a new instance of the <see cref="SipMessage"/> class.
        /// </summary>
        /// <param name="headerFields">The HeaderFields.</param>
        /// <param name="body">The body.</param>
        protected SipMessage(HeaderFieldCollection headerFields, byte[] body)
            : this(headerFields)
        {
            _body = body;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets the bytes.
        /// </summary>
        /// <param name="message">The message.</param>
        public virtual void GetBytes(out byte[] message )
        {
            GetBytes(out message,  false);
        }

        public virtual void GetBytes(out byte[] message, bool useCompactForm)
        {
            //if(_body != null) //TODO reinstate later
                {
                byte[] header = GetHeaderBytes( useCompactForm);
                int headerLength = header.Length;
                int bodyLength = _body.Length;
                message = new byte[headerLength + bodyLength];

                Buffer.BlockCopy(header, 0, message, 0, headerLength);
                Buffer.BlockCopy(_body, 0, message, headerLength, bodyLength);
                }
        }

        /// <summary>
        /// Finds the specified HeaderField.
        /// </summary>
        /// <param name="fieldName">The name of the HeaderField to find.</param>
        /// <returns>A HeaderField or null if no field is found. Only the first field is returned.</returns>
        public HeaderFieldBase GetHeader(string headerName)
        {
            HeaderFieldBase returnHf = null;
            if(!string.IsNullOrEmpty(headerName))
                {
                foreach(HeaderFieldBase hf in Headers)
                    {
                    if(hf.FieldName.ToUpperInvariant() == headerName.ToUpperInvariant())
                        {
                        returnHf = hf;
                        break;
                        }
                    }
                }
            return returnHf;
        }

        /// <summary>
        /// Returns a byte array of the header.
        /// </summary> 
        public abstract byte[] GetHeaderBytes(bool useCompactForm);

        /// <summary>
        /// Finds the value associated with a field name.
        /// </summary>
        /// <param name="fieldName">The name of the HeaderField to find.</param>
        /// <returns>A string value or null if no field is found. Only the first field is returned.</returns>
        public string GetHeaderValue(string headerName)
        {
            string retVal = null;
            foreach(HeaderFieldBase hf in Headers)
                {
                if(hf.FieldName.ToUpperInvariant() == headerName.ToUpperInvariant())
                    {
                    retVal = hf.GetStringValue();
                    break;
                    }
                }
            return retVal;
        }

        /// <summary>
        /// Creates stream from the instance.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="useCompactForm">if set to <c>true</c> the the stram uses the compact form for each filed name.</param>
        public virtual void GetStream(Stream stream,  bool useCompactForm)
            {
             //TODO: Should we do this if we have a Formatter?!
            byte[] header = GetHeaderBytes( useCompactForm);

            stream.Write(header, 0, header.Length);
            stream.Write(_body, 0, _body.Length);
        }

        /// <summary>
        /// Determines whether this instance is valid.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsValid();

        /// <summary>
        /// Removes the header.
        /// </summary>
        /// <param name="headerName">Name of the header.</param>
        public void RemoveHeader(string headerName)
        {
            Headers.Remove(headerName);
        }

        /// <summary>
        /// Sets the specified header.
        /// </summary>
        /// <param name="header">The header.</param>
        public void Set(HeaderFieldBase header)
        {
            if(Headers.Contains(header))
                {
                Headers[header.FieldName] = header;
                }
            else
                {
                Headers.Add(header);
                }
        }

        /// <summary>
        /// Adds a HeaderField to the header.
        /// </summary>
        /// <param name="field">The HeaderField.</param>
        /// <returns>
        /// 	<c>true</c> if field was valid and added to the headers; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>The method first calls ValidateHeaderField to check if the field can be added.</remarks>
        /// <exception cref="T:System.NullReferenceException">
        /// The <paramref name="field"/>  parameter is null (<b>Nothing</b> in Visual Basic).
		/// </exception>
		[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public bool TryAddHeader(HeaderFieldBase field)
        {
            try
            {
            Headers.Add(field);
            }
            catch(ArgumentNullException)
                {
                return false;
            }
            catch(ArgumentException)
            {
            return false;
            }
            return true;
        }

        #endregion Methods
    }
}