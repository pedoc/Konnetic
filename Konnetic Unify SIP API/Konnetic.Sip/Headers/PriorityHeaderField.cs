/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Text.RegularExpressions;

using Konnetic.Sip.Messages;

namespace Konnetic.Sip.Headers
    {
    /// <summary>The Priority HeaderField indicates the urgency of the request as perceived by the client.
    /// </summary>
    /// <remarks> 
    /// <b>Standards: RFC3261, RFC2076</b>
    /// <para/>
    /// The Priority HeaderField describes the priority that the SIP request should have to the receiving human or its agent. For example, it may be factored into decisions about call routing and acceptance. For these decisions, a message containing no Priority HeaderField should be treated as if it specified a Priority of normal. The Priority HeaderField does not influence the use of communications resources such as packet forwarding priority in routers or access to circuits in PSTN gateways. The HeaderField can have the values non-urgent, normal, urgent, and emergency, but additional values can be defined elsewhere. It is recommended by the SIP standard that the value of emergency only be used when life, limb, or property are in imminent danger. Otherwise, there are no semantics defined for this HeaderField.
    /// <para/>
    /// <b>RFC 3261 Syntax:</b> 
    /// <table >
    /// <tr><td colspan="2" style="border-bottom:none">"Priority" ":" "emergency" / "urgent" / "normal" / "non-urgent" / token</td></tr>  
    /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr> 
    /// </table>  
    /// <para/>  
    /// <note type="implementnotes">It is not particularly useful to encrypt this field.</note> 
    /// <example>
    /// <list type="bullet">
    /// <item>Priority: emergency</item>  
    /// </list> 
    /// </example>
    /// </remarks> 
    /// <seealso cref="T:Konnetic.Sip.Headers.MediaTypeHeaderFieldBase"/>
    public sealed class PriorityHeaderField : HeaderFieldBase
    {
        #region Fields

        internal const string CompareName = "PRIORITY";
        internal const string CompareShortName = CompareName;
        internal const string LongName = "Priority";

        private string _priority;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>The priority.</value>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="Priority"/>.</exception> 
        /// <exception cref="SipFormatException">Thrown when a user attempts to add non-token characters.<paramref name="Priority"/>.</exception> 
        public string Priority
        {
            get { return _priority; }
              set
            {
            PropertyVerifier.ThrowOnNullArgument(value, "Priority");
            value = value.Trim();
            PropertyVerifier.ThrowOnInvalidToken(value, "Priority");
            _priority = value;
              }
        }

        #endregion Properties

        #region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="PriorityHeaderField"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <overloads>
		/// <summary>The method has two overloads.</summary>
		/// </overloads>
        public PriorityHeaderField()
            : this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityHeaderField"/> class.
        /// </summary>
        /// <param name="priority">The priority.</param>
        public PriorityHeaderField(string priority)
            : base()
        {
            PropertyVerifier.ThrowOnNullArgument(priority, "priority");
            Priority = priority;
            AllowMultiple = false;
            FieldName = PriorityHeaderField.LongName;
            CompactName = PriorityHeaderField.LongName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityHeaderField"/> class.
        /// </summary>
        /// <param name="priority">The priority.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
        public PriorityHeaderField(Priority priority)
            : this(Enum.GetName(typeof(Priority),priority).ToLowerInvariant())
        {
        }

        #endregion Constructors

        #region Methods
		/// <summary>
		/// Performs an implicit conversion from <see cref="System.String"/> to <see cref="Konnetic.Sip.Headers.PriorityHeaderField"/>.
		/// </summary>
		/// <param name="value">The string value representing the HeaderField.</param>
        /// <returns>A new <see cref="Konnetic.Sip.Headers.PriorityHeaderField"/> populated from the <paramref name="value"/> parameter.</returns>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="value"/>.</exception>
        public static implicit operator PriorityHeaderField(String value)
        {
            PropertyVerifier.ThrowOnNullArgument(value,"value");

            PriorityHeaderField hf = new PriorityHeaderField();
            hf.Parse(value);
            return hf;
        }
		/// <summary>
		/// Performs an explicit conversion from <see cref="Konnetic.Sip.Headers.PriorityHeaderField"/> to <see cref="System.String"/>.
		/// </summary>
		/// <param name="headerField">The HeaderField to convert.</param>
		/// <returns>A string representation of the HeaderField.</returns>
		/// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="headerField"/>.</exception>
        public static explicit operator string(PriorityHeaderField headerField)
        {
            PropertyVerifier.ThrowOnNullArgument(headerField, "headerField");
            return headerField.ToString();
        }
        /// <summary>
        /// Creates a deap  copy of this instance.
        /// </summary> 
        /// <remarks>Creates and returns a deep copy of the Header. This method ensures a deep copy of the HeaderField, when a message is cloned the HeaderField can be modified without effecting the original HeaderField in the message.</remarks>
        /// <returns>A deep copy of <see cref="T:Konnetic.Sip.Headers.PriorityHeaderField"/>.</returns>
        /// <threadsafety static="true" instance="false" /> 
public override HeaderFieldBase Clone()
        {
            PriorityHeaderField newObj = new PriorityHeaderField(this.Priority);
            return newObj;
        }        
        ///<summary>
/// Compare this SIP Header for equality with another <see cref="T:Konnetic.Sip.Headers.PriorityHeaderField"/> object.</summary>
/// <remarks>
/// All optional headers are compared using object equality that is each field in the header is used for comparision. When comparing HeaderFields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular HeaderField, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are case-sensitive. 
/// </remarks>
/// <param name="other">The <see cref="T:Konnetic.Sip.Headers.PriorityHeaderField"/> to compare to this instance.</param>
/// <returns><c>true</c> if <paramref name="other" /> is an instance of this class representing the same SIP HeaderField as this, <c>false</c> otherwise.</returns>    
/// <overloads>
/// <summary>This method is overloaded.</summary>
/// <remarks>Overloads allow for equality against <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>, <see cref="T:System.Object"/> and another instance of this HeaderField.</remarks>
/// </overloads>  
/// <threadsafety static="true" instance="false" />
[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
public bool Equals(PriorityHeaderField other)
    {
    if((object)other == null)
        {
        return false;
        }

    return base.Equals((HeaderFieldBase)other) && Priority.Equals(other.Priority, StringComparison.OrdinalIgnoreCase);
    }

/// <summary>Compare this SIP Header for equality with the base <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>.
/// </summary>
/// <remarks>This method overrides the <c>equals</c> method in <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>. 
/// All optional headers are compared using object equality that is each field in the header is used for comparision. When comparing HeaderFields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular HeaderField, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are case-sensitive. 
/// </remarks>
/// <param name="other">The <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/> to compare to this instance.</param>
/// <returns><c>true</c> if <paramref name="other" /> is an instance of this class representing the same SIP HeaderField as this, <c>false</c> otherwise.</returns>
/// <overloads>
/// <summary>This method is overloaded.</summary>
/// <remarks>Overloads allow for equality against <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>, <see cref="T:System.Object"/> and another instance of this HeaderField.</remarks>
/// </overloads>  
/// <threadsafety static="true" instance="false" />         
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public override bool Equals(HeaderFieldBase other)
        {
            return Equals((object)other);
        }

        /// <summary>Compare this SIP Header for equality with an instance of <see cref="T:System.Object"/>.
        /// </summary>
        /// <remarks>This method overrides the <c>equals</c> method in <see cref="System.Object"/>. 
        /// All optional headers are compared using object equality that is each field in the header is used for comparision. When comparing HeaderFields, field names are always case-insensitive. Unless otherwise stated in the definition of a particular HeaderField, field values, parameter names, and parameter values are case-insensitive. Tokens are always case-insensitive. Unless specified otherwise, values expressed as quoted strings are case-sensitive. 
        /// </remarks>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare to this instance.</param>
        /// <returns><c>true</c> if <paramref name="obj" /> is an instance of this class representing the same SIP HeaderField as this, <c>false</c> otherwise. </returns>        
        /// <overloads>
        /// <summary>This method is overloaded.</summary>
        /// <remarks>Overloads allow for equality against <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>, <see cref="T:System.Object"/> and another instance of this HeaderField.</remarks>
        /// </overloads> 
        /// <threadsafety static="true" instance="false" />
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public override bool Equals(object obj)
        {
            if(obj == null)
                {
                return false;
                }

            PriorityHeaderField p = obj as PriorityHeaderField;
            if((object)p == null)
                {
                HeaderFieldGroup<PriorityHeaderField> p1 = obj as HeaderFieldGroup<PriorityHeaderField>;
                if((object)p1 == null)
                    {
                    return false;
                    }
                else
                    {
                    return p1.Equals(this);
                    }
                }

            return this.Equals(p);
        }
        /// <summary>
        /// Validates this instance against the standard. Indicated whether it reaches minimum compliance.
        /// </summary>
        /// <remarks>This member overrides the <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/> instance.</remarks>
        /// <returns>
        /// 	<c>true</c> if instance represents a valid HeaderField; otherwise, <c>false</c>.
        /// </returns>
        /// <threadsafety static="true" instance="false" />
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.U1)]
        public override bool IsValid()
        {
            return base.IsValid() && !String.IsNullOrEmpty(Priority);
        }
        /// <summary>
        /// Parses string representation of the HeaderField.
        /// </summary>
        /// <remarks>  
        /// <b>RFC 3261 Syntax:</b> 
        /// <table >
        /// <tr><td colspan="2" style="border-bottom:none">"Priority" ":" "emergency" / "urgent" / "normal" / "non-urgent" / token</td></tr>  
        /// <tr><td style="border-bottom:none">token = </td><td style="border-bottom:none">1*(alphanum / "-" / "." / "!" / "%" / "*" / "_" / "+" / "‘" / "'" / "˜" )</td></tr> 
        /// </table>  
        /// <para/>  
        /// <note type="implementnotes">It is not particularly useful to encrypt this field.</note> 
        /// <example>
        /// <list type="bullet">
        /// <item>Priority: emergency</item>  
        /// </list> 
        /// </example>
        /// </remarks>
        /// <param name="value">The HeaderField string to parse.</param>
        /// <exception cref="T:Konnetic.Sip.SipParseException">Thrown when an invalid (non-standard) value is encountered.</exception>
        /// <exception cref="T:Konnetic.Sip.SipException">Thrown when a processing exception is encountered.</exception>
        /// <threadsafety static="true" instance="false" />
        public override void Parse(string value)
        {
            if(value != null)
            {
            RemoveFieldName(ref value, FieldName, CompactName);
            Priority = string.Empty;
            if(!string.IsNullOrEmpty(value))
                {
                Regex _priorityRegex = new Regex(@"(?<=^\s*)[\w-.!%_*+`'~]+(?=\s*\z)", RegexOptions.Compiled | RegexOptions.IgnoreCase );
				Match m = _priorityRegex.Match(value);
                if(m != null)
                    {
                    if(!string.IsNullOrEmpty(m.Value))
                        {
						try{
                        Priority = m.Value;
						}
						catch(SipException ex)
							{
							throw new SipParseException("Priority", SR.ParseExceptionMessage(value), ex);
							}
						catch(Exception ex)
                            {
                            throw new SipParseException(SR.GetString(SR.GeneralParseException, m.Value, "Priority"), ex);  
							}
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Gets a string representation of the HeaderField value. The HeaderField name is excluded.
        /// </summary>
        /// <remarks>This method overrides the <c>GetStringValue</c> method in <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>. </remarks>
        /// <returns>String representation of the HeaderField value</returns>
        /// <threadsafety static="true" instance="false" />
        public override string GetStringValue()
        {
            return Priority;
        }

        #endregion Methods
    }
}