/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;

namespace Konnetic.Sip.Headers
{
    /// <summary>
    /// When present in an INVITE request, the Alert-Info HeaderField specifies an alternative ring tone to the server. When present in a 180 (Ringing) response, the Alert-Info HeaderField specifies an alternative ringback tone to the client. A typical usage is for a proxy to insert this HeaderField to provide a distinctive ring feature.
    /// </summary>
    /// <remarks>
    /// <b>Standards: RFC3261, RFC2616</b> 
    /// <para/>
    /// The Alert-Info HeaderField can introduce security risks. If a callee fetches the URIs provided by a malicious caller, the callee may be at risk for displaying inappropriate or offensive content, dangerous or illegal content, and so on. Therefore, it is recommended that a client only render the information in the Alert-Info HeaderField if it can verify the authenticity of the element that originated the HeaderField and trusts that element. This need not be the peer client; a proxy can insert this HeaderField into requests. 
    /// <para/>
    /// <note type="implementnotes">In addition, a user should be able to disable this feature selectively.</note> 
    /// <para/>
    /// <b>RFC 3261 Syntax:</b> 
    /// <table >
    /// <tr><td colspan="2" style="border-bottom:none">"Alert-Info" ":" alert-param *("," alert-param)</td></tr>
    /// <tr><td style="border-bottom:none">alert-param = </td><td style="border-bottom:none">"&lt;" absoluteURI "&gt;" *( SEMI generic-param )</td></tr>
    /// </table>
    /// <para/>
    /// <note type="implementnotes">This HeaderField allows HeaderField grouping (<see cref="T:Konnetic.Sip.Headers.HeaderFieldGroup{T}"/>).</note> 
    /// <example>
    /// <list type="bullet">
    /// <item>Alert-Info: &lt;http://www.example.com/sounds/moo.wav&gt;</item>
    /// </list> 
    /// </example>
    /// </remarks>
    /// <seealso cref="T:Konnetic.Sip.Headers.AbsoluteUriHeaderFieldBase"/>
    /// <seealso cref="T:Konnetic.Sip.Headers.CallInfoHeaderField"/> 
    public sealed class AlertInfoHeaderField : AbsoluteUriHeaderFieldBase
    {
        #region Fields

        internal const string CompareName = "ALERT-INFO";
        internal const string CompareShortName = CompareName; 
        internal const string LongName = "Alert-Info";

        #endregion Fields
        #region Properties
        /// <summary>
        /// Gets the generic parameters.
        /// </summary>
        /// <value>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection&lt;SipParameter&gt;"/> field parameter.</value>
        public System.Collections.ObjectModel.ReadOnlyCollection<SipParameter> GenericParameters
            {
            get { return InternalGenericParameters; }
            }
        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public void AddParameter(string name, string value)
            {
            InternalAddGenericParameter(name, value);
            }
        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void AddParameter(SipParameter parameter)
            {
            InternalAddGenericParameter(parameter);
            }
        #endregion Properties
        #region Constructors
        /// <summary>
		/// Initializes a new instance of the <see cref="AlertInfoHeaderField"/> class.
		/// </summary>
		/// <remarks>The default constructor.</remarks>
		/// <overloads>
		/// <summary>The method has two overloads.</summary>
		/// </overloads>
        public AlertInfoHeaderField()
            : base()
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlertInfoHeaderField"/> class.
        /// </summary>
        /// <param name="uri"></param>
        public AlertInfoHeaderField(string uri)
            : this(new  Uri(uri))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlertInfoHeaderField"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        public AlertInfoHeaderField( Uri uri)
            : base(uri)
        {
            Init();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.String"/> to <see cref="Konnetic.Sip.Headers.AlertInfoHeaderField"/>.
        /// </summary>
        /// <param name="value">The string value representing the HeaderField.</param>
        /// <returns>A new <see cref="Konnetic.Sip.Headers.AlertInfoHeaderField"/> populated from the <paramref name="value"/> parameter.</returns>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="value"/>.</exception>
        public static implicit operator AlertInfoHeaderField(String value)
        {
            PropertyVerifier.ThrowOnNullArgument(value,"value");

            AlertInfoHeaderField hf = new AlertInfoHeaderField();
            hf.Parse(value);
            return hf;
        }
        /// <summary>
        /// Performs an explicit conversion from <see cref="Konnetic.Sip.Headers.AlertInfoHeaderField"/> to <see cref="System.String"/>.
        /// </summary>
        /// <param name="headerField">The HeaderField to convert.</param>
        /// <returns>A string representation of the HeaderField.</returns>
        /// <exception cref="ArgumentNullException">Thrown on null (<b>Nothing</b> in Visual Basic) <paramref name="headerField"/>.</exception>
        public static explicit operator string(AlertInfoHeaderField headerField)
        {
            PropertyVerifier.ThrowOnNullArgument(headerField, "headerField");
            return headerField.ToString();
        }

        /// <summary>
        /// Creates a deap  copy of this instance.
        /// </summary> 
        /// <remarks>Creates and returns a deep copy of the Header. This method ensures a deep copy of the HeaderField, when a message is cloned the HeaderField can be modified without effecting the original HeaderField in the message.</remarks>
        /// <returns>A deep copy of <see cref="T:Konnetic.Sip.Headers.AlertInfoHeaderField"/>.</returns>
        /// <threadsafety static="true" instance="false" /> 
        public override HeaderFieldBase Clone()
        {
        AlertInfoHeaderField newObj = new AlertInfoHeaderField(AbsoluteUri);
        CopyParametersTo(newObj); 
            newObj.AbsoluteUriSet = AbsoluteUriSet;
            return newObj;
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

            AlertInfoHeaderField p1 = obj as AlertInfoHeaderField;
            if((object)p1 == null)
                {
                HeaderFieldGroup<AlertInfoHeaderField> p = obj as HeaderFieldGroup<AlertInfoHeaderField>;
                if((object)p == null)
                    {
                    return false;
                    }
                else
                    {
                    return p.Equals(this);
                    }
                }
            else
                {
                return this.Equals(p1);
                }
        }
        /// <summary>
        /// Parses string representation of the HeaderField.
        /// </summary>
        /// <remarks>   
        /// <b>RFC 3261 Syntax:</b> 
        /// <table >
        /// <tr><td colspan="2" style="border-bottom:none">"Alert-Info" ":" alert-param *("," alert-param)</td></tr>
        /// <tr><td style="border-bottom:none">alert-param = </td><td style="border-bottom:none">"&lt;" absoluteURI "&gt;" *( SEMI generic-param )</td></tr>
        /// </table>
        /// <example>
        /// <list type="bullet">
        /// <item>Alert-Info: &lt;http://www.example.com/sounds/moo.wav&gt;</item>
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
                base.Parse(value);
            }
        }

        private void Init()
        {
            AllowMultiple = true;
            FieldName = AlertInfoHeaderField.LongName;
            CompactName = AlertInfoHeaderField.LongName;
        }

        #endregion Methods
    }
}