using System;
using System.Text.RegularExpressions;

using Konnetic.Sip;
using Konnetic.Sip.Headers;
using Konnetic.Sip.Messages;

namespace Konnetic.Sip.Examples
{
    class Program
    {
        #region Methods

        static void Main(string[] args)
        {
            //UserAgent UA = new UserAgent();
            //Uri u1 = new Uri("sip:bob@billy.com");

            //UA.Call("sip:bob@billy.com");

            ////Dialog d = new Dialog("Bob");
            //SipStyleUriParser p = new SipStyleUriParser(GenericUriParserOptions.AllowEmptyAuthority |
            //GenericUriParserOptions.DontCompressPath | GenericUriParserOptions.DontConvertPathBackslashes |
            //GenericUriParserOptions.DontUnescapePathDotsAndSlashes | GenericUriParserOptions.GenericAuthority |
            //GenericUriParserOptions.NoFragment);
            //UriParser.Register(p, "sip", 5060);

            //UriParser.Register(new SipStyleUriParser(GenericUriParserOptions.AllowEmptyAuthority |
            //GenericUriParserOptions.DontCompressPath | GenericUriParserOptions.DontConvertPathBackslashes |
            //GenericUriParserOptions.DontUnescapePathDotsAndSlashes | GenericUriParserOptions.GenericAuthority |
            //GenericUriParserOptions.NoFragment), "sips", 5060);
        AlertInfoHeaderField a = new AlertInfoHeaderField();
        AuthenticationInfoHeaderField ahf = new AuthenticationInfoHeaderField(); 
        ahf.RemoveParameter("n");
            //if(!SipStyleUriParser.IsKnownScheme("sip"))
            //{
            //SipStyleUriParser p = new SipStyleUriParser();
            //SipStyleUriParser.Register(p, "sip", 5060);
            //SipStyleUriParser p1 = new SipStyleUriParser();
            //SipStyleUriParser.Register(p1, "sips", 5060);
            //}
            //SipUri u = new SipUri("sip:alice:password@chicago.com;ttl=15;maddr=239.255.255.1;transport=tcp?to=alice");
            //string t1 = u.ToString();

            //u = new SipUri("sip:alice@atlanta.com");
            //u = new SipUri("sip:alice:secretworld@atlanta.com");
            //u = new SipUri("sips:alice@atlanta.com?subject=project%20x&priority=urgent");
            //u = new SipUri("sip:+1-212-555-1212:1234@gateway.com;user=phone");
            //u = new SipUri("sips:1212@gateway.com");
            //u = new SipUri("sip:atlanta.com;method=REGISTER?to=alice%40atlanta.com");
            //u = new SipUri("sip:alice;day=tuesday@atlanta.com");

            ////SipUriBuilder b = new SipUriBuilder(u);

            ////string s1 = u.PathAndQuery;

            //SipUri to = new SipUri("sip:Bob@localhost");
            //SipUri from = new SipUri("sip:Fred@localhost");
            //Invite target = new Invite(to, from);

            //TransportServer.Start();
            //TransactionRegistry.Start();
            //TransactionRegistry.NewRequest += new NewRequestEventHandler(TransactionRegistry_NewRequest);

            ////TransportServer.OnPacketRecieved += new PacketReceivedHandler(TransportServer_OnPacketRecieved);
            //TransportClient.Send(target);
            //int x = 0;
            //while(x<100)
            //    {
            //    System.Threading.Thread.Sleep(10);
            //    x++;
            //    }
        }

        static void TransactionRegistry_NewRequest(NewRequestReceivedEventArgs e)
        {
            if(e.Request.Method == SipMethod.Invite)
                {
                Invite i = (Invite)e.Request;
                InviteServerTransaction iS = new InviteServerTransaction(TransportType.Tcp);
                iS.ProcessRequest(i);
                }
        }

        #endregion Methods

        #region Other

        //static void TransportServer_OnPacketRecieved(PacketReceivedEventArgs e)
        //{
        //    System.Diagnostics.Debug.WriteLine(e.Data.ReadLine());
        //}

        #endregion Other
    }
}