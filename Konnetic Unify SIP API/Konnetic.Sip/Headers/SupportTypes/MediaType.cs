/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;

namespace Konnetic.Sip.Headers
{
    #region Enumerations

    /// <summary>
    /// Internet Media Types are used in the HeaderField in order to provide open and extensible data typing and type negotiation.
    /// </summary>
    [Serializable]
    public enum MediaType
    {
    /// <summary>
    /// The default enumeration. Equates to STAR (*).
    /// </summary>
        All,
        /// <summary>
        /// Textual information.
        /// </summary>
        /// <remarks>The subtype "plain" in particular indicates plain text containing no formatting commands or directives of any sort. Plain text is intended to be displayed "as-is". No special software is required to get the full meaning of the text, aside from support for the indicated character set. Other subtypes are to be used for enriched text in forms where application software may enhance the appearance of the text, but such software must not be required in order to get the general idea of the content.  Possible subtypes of "text" thus include any word processor format that can be read without resorting to software that understands the format.  In particular, formats that employ embeddded binary formatting information are not considered directly readable. A very simple and portable subtype, "richtext", was defined in RFC 1341, with a further revision in RFC 1896 under the name "enriched".</remarks>
        Text,
        /// <summary>
        /// Image data.  
        /// </summary>
        /// <remarks>"Image" requires a display device (such as a graphical display, a graphics printer, or a FAX machine) to view the information. An initial subtype is defined for the widely-used image format JPEG. Subtypes are defined for two widely used image formats, jpeg and gif.</remarks>
        Image,
        /// <summary>
        /// Audio data.
        /// </summary>
        /// <remarks>"Audio" requires an audio output device (such as a speaker or a telephone) to "display" the contents.  An initial subtype "basic" is defined in this document.</remarks>
        Audio,
        /// <summary>
        /// Video data.
        /// </summary>
        /// <remarks>"Video" requires the capability to display moving images, typically including specialized hardware and software.  An initial subtype "mpeg" is defined in this document.</remarks>
        Video,
        /// <summary>
        /// An encapsulated message.
        /// </summary>
        /// <remarks>A body of media type "message" is itself all or a portion of some kind of message object.  Such objects may or may not in turn contain other entities.  The "rfc822" subtype is used when the encapsulated content is itself an RFC 822 message.  The "partial" subtype is defined for partial RFC 822 messages, to permit the fragmented transmission of bodies that are thought to be too large to be passed through transport facilities in one piece.  Another subtype, "external-body", is defined for specifying large bodies by reference to an external data source.</remarks>
        Message,
        /// <summary>
        /// Data consisting of multiple entities of independent data types.
        /// </summary>
        /// <remarks>Four subtypes are initially defined, including the basic "mixed" subtype specifying a generic mixed set of parts, "alternative" for representing the same data in multiple formats, "parallel" for parts intended to be viewed simultaneously, and "digest" for multipart entities in which each part has a default type of "message/rfc822".</remarks>
        Multipart,
        /// <summary>
        /// Some other kind of data, typically either uninterpreted binary data or information to be processed by an application.
        /// </summary>
        /// <remarks>The subtype "octet-stream" is to be used in the case of uninterpreted binary data, in which case the simplest recommended action is to offer to write the information into a file for the user.  The "PostScript" subtype is also defined for the transport of PostScript material.  Other expected uses for "application" include spreadsheets, data for mail-based scheduling systems, and languages for "active" (computational) messaging, and word processing formats that are not directly readable. Note that security considerations may exist for some types of application data, most notably "application/ PostScript" and any form of active messaging.</remarks>
        Application
    }

    #endregion Enumerations
}