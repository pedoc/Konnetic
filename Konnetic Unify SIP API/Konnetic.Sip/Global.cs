/* 
Copyright (c) 2009-2010 Konnetic Ltd.
*/
/* Change history
* 20 Jan 2010  James Wright james@konnetic.com Baseline Implementation
*/

using System;

namespace Konnetic.Sip
{
    #region Delegates

    /// <summary>
    /// 
    /// </summary>
    public delegate void NewRequestEventHandler(NewRequestReceivedEventArgs e);

    /// <summary>
    /// 
    /// </summary>
    public delegate void SendingResponseReceivedEventHandler(object sender, BeforeSendingResponseEventArgs e);

    /// <summary>
    /// 
    /// </summary>
	public delegate void SentResponseReceivedEventHandler(object sender, AfterSendingResponseEventArgs e);

    /// <summary>
    /// 
    /// </summary>
    public delegate void UnmatchedResponseEventHandler(UnmatchedResponseEventArgs e);

    /// <summary>
    /// 
    /// </summary>
	internal delegate void PacketReceivedEventHandler(PacketReceivedEventArgs e);

    #endregion Delegates
}