/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Konnetic.Sip.Headers
{
    /// <summary>
    /// Represents a simple factory creating HeaderFields. 
    /// </summary>
    internal static class HeaderFieldFactory
    {
        #region Methods

        /// <summary>
    /// Creates a new HeaderField. A simple factory that returns a <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/> instance given the name of the field.
        /// </summary>
        /// <param name="name">The name of the HeaderField.</param>
    /// <returns>A <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/> HeaderField instance. A GenericHeaderField is returned if there is no match on the name.</returns>
    /// <exception cref="SipFormatException">Is raised when <paramref value="name"/> is not recognied and is not a valid token.</exception>
    /// <threadsafety static="true" instance="false" />
        public static HeaderFieldBase CreateHeaderField(string name)
        {
            PropertyVerifier.ThrowOnNullArgument(name, "name");

            switch(name.ToUpperInvariant())
            {
            case AcceptHeaderField.CompareName:
                //return new HeaderFieldGroup<AcceptHeaderField>();
                return new AcceptHeaderField();

            case AcceptEncodingHeaderField.CompareName:
                //return new HeaderFieldGroup<AcceptEncodingHeaderField>();
                return new AcceptEncodingHeaderField();

            case AcceptLanguageHeaderField.CompareName:
                //return new HeaderFieldGroup<AcceptLanguageHeaderField>();
                return new AcceptLanguageHeaderField();

            case AlertInfoHeaderField.CompareName:
                //return new HeaderFieldGroup<AlertInfoHeaderField>();
                return new AlertInfoHeaderField();

            case AllowHeaderField.CompareName:
                //return new HeaderFieldGroup<AllowHeaderField>();
                return new AllowHeaderField();

            case AuthorizationHeaderField.CompareName:
                //return new AuthHeaderFieldGroup<AuthorizationHeaderField>();
                return new AuthorizationHeaderField();

            case AuthenticationInfoHeaderField.CompareName:
                return new AuthenticationInfoHeaderField();

            case CallIdHeaderField.CompareShortName:
            case CallIdHeaderField.CompareName:
                return new CallIdHeaderField();

            case CallInfoHeaderField.CompareName:
                //return new HeaderFieldGroup<CallInfoHeaderField>();
                return new CallInfoHeaderField();

            case ContactHeaderField.CompareShortName:
            case ContactHeaderField.CompareName:
                return new ContactHeaderField();

            case ContentDispositionHeaderField.CompareName:
                return new ContentDispositionHeaderField();

            case ContentEncodingHeaderField.CompareShortName:
            case ContentEncodingHeaderField.CompareName:
                //return new HeaderFieldGroup<ContentEncodingHeaderField>();
                return new ContentEncodingHeaderField();

            case ContentLanguageHeaderField.CompareName:
                //return new HeaderFieldGroup<ContentLanguageHeaderField>();
                return new ContentLanguageHeaderField();

            case ContentLengthHeaderField.CompareShortName:
            case ContentLengthHeaderField.CompareName:
                return new ContentLengthHeaderField();

            case ContentTypeHeaderField.CompareShortName:
            case ContentTypeHeaderField.CompareName:
                return new ContentTypeHeaderField();

            case CSeqHeaderField.CompareName:
                return new CSeqHeaderField();

            case DateHeaderField.CompareName:
                return new DateHeaderField();

            case ErrorInfoHeaderField.CompareName:
                //return new HeaderFieldGroup<ErrorInfoHeaderField>();
                return new ErrorInfoHeaderField();

            case ExpiresHeaderField.CompareName:
                return new ExpiresHeaderField();

            case FromHeaderField.CompareShortName:
            case FromHeaderField.CompareName:
                return new FromHeaderField();

            case InReplyToHeaderField.CompareName:
                //return new HeaderFieldGroup<InReplyToHeaderField>();
                return new InReplyToHeaderField();

            case MaxForwardsHeaderField.CompareName:
                return new MaxForwardsHeaderField();

            case MimeVersionHeaderField.CompareName:
                return new MimeVersionHeaderField();

            case MinExpiresHeaderField.CompareName:
                return new MinExpiresHeaderField();

            case OrganizationHeaderField.CompareName:
                return new OrganizationHeaderField();

            case PriorityHeaderField.CompareName:
                return new PriorityHeaderField();

            case ProxyAuthenticateHeaderField.CompareName:
                //return new AuthHeaderFieldGroup<ProxyAuthenticateHeaderField>();
                return new ProxyAuthenticateHeaderField();

            case ProxyAuthorizationHeaderField.CompareName:
                //return new AuthHeaderFieldGroup<ProxyAuthorizationHeaderField>();
                return new ProxyAuthorizationHeaderField();

            case ProxyRequireHeaderField.CompareName:
                //return new HeaderFieldGroup<ProxyRequireHeaderField>();
                return new ProxyRequireHeaderField();

            case RecordRouteHeaderField.CompareName:
                //return new HeaderFieldGroup<RecordRouteHeaderField>();
                return new RecordRouteHeaderField();

            case ReplyToHeaderField.CompareName:
                return new ReplyToHeaderField();

            case RequireHeaderField.CompareName:
                //return new HeaderFieldGroup<RequireHeaderField>();
                return new RequireHeaderField();

            case RetryAfterHeaderField.CompareName:
                return new RetryAfterHeaderField();

            case RouteHeaderField.CompareName:
                //return new HeaderFieldGroup<RouteHeaderField>();
                return new RouteHeaderField();

            case ServerHeaderField.CompareName:
                return new ServerHeaderField();

            case SubjectHeaderField.CompareShortName:
            case SubjectHeaderField.CompareName:
                return new SubjectHeaderField();

            case SupportedHeaderField.CompareShortName:
            case SupportedHeaderField.CompareName:
                //return new HeaderFieldGroup<SupportedHeaderField>();
                return new SupportedHeaderField();

            case TimestampHeaderField.CompareName:
                return new TimestampHeaderField();

            case ToHeaderField.CompareShortName:
            case ToHeaderField.CompareName:
                return new ToHeaderField();

            case UnsupportedHeaderField.CompareName:
                //return new HeaderFieldGroup<UnsupportedHeaderField>();
                return new UnsupportedHeaderField();

            case UserAgentHeaderField.CompareName:
                return new UserAgentHeaderField();

            case ViaHeaderField.CompareShortName:
            case ViaHeaderField.CompareName:
                //return new HeaderFieldGroup<ViaHeaderField>();
                return new ViaHeaderField();

            case WarningHeaderField.CompareName:
                return new WarningHeaderField();

            case WwwAuthenticateHeaderField.CompareName:
                //return new AuthHeaderFieldGroup<WwwAuthenticateHeaderField>();
                return new WwwAuthenticateHeaderField();

            }

            PropertyVerifier.ThrowOnInvalidToken(name, "HeaderField name"); 
            return new ExtensionHeaderField(name);
        }

        /// <summary>
        /// Creates the HeaderField from a message-header line (i.e., a line with ending with a CRLF).
        /// </summary>
        /// <param name="headerLine">The message-header line.</param>
        /// <returns>A new <see cref="T:Konnetic.Sip.Headers.HeaderFieldBase"/>.</returns>
        /// <threadsafety static="true" instance="false" />
        public static HeaderFieldBase CreateHeaderFieldFromLine(string headerLine)
        {
		PropertyVerifier.ThrowOnNullArgument(headerLine, "headerLine"); 

            Regex _name = new Regex(@"(?<=^\s*)[\w-.!%_*+`'~]+(?=\s*:\s*(.|\n)+$)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex _value = new Regex(@"(?<=^\s*[\w-.!%_*+`'~]+\s*:\s*)(.|\n)+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            Match m = _name.Match(headerLine);
            if((object)m == null || string.IsNullOrEmpty(m.Value))
                {
                throw new SipFormatException(String.Format(CultureInfo.InvariantCulture, "Cannot identify the field name in: {0}", headerLine));
                }

            string name = m.Value;
            string value = string.Empty;

            m = _value.Match(headerLine);
            if(m != null && !string.IsNullOrEmpty(m.Value))
                {
                value = m.Value.Trim();
                }

            HeaderFieldBase newVal = CreateHeaderField(name);
            if(value.Length > 0)
                {
                newVal.Parse(value);
                }
            return newVal;
        }

        #endregion Methods
    }
}