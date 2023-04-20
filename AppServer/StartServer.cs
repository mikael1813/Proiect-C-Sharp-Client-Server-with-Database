using System;
using System.Collections;

using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using AppModel;
using Proiect_C_Sharp_Client_Server;
using Proiect_C_Sharp_Client_Server.database;
//using System.Runtime.Remoting.Channels.Tcp;
using Hashtable = System.Collections.Hashtable;

namespace AppServer
{
    class StartServer
    {
        static void Main(string[] args)
        {
            BinaryServerFormatterSinkProvider serverProv = new BinaryServerFormatterSinkProvider();
            serverProv.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
            BinaryClientFormatterSinkProvider clientProv = new BinaryClientFormatterSinkProvider();
            IDictionary props = new Hashtable();

            props["port"] = 55555;
            TcpChannel channel = new TcpChannel(props, clientProv, serverProv);
            ChannelServices.RegisterChannel(channel, false);

            ParticipantRepository repo2 = new ParticipantDBRepository();
            ProbaRepository repo3 = new ProbaDBRepository();
            InscriereRepository repo = new InscriereDBRepository(repo2, repo3);
            UtilizatorRepository repo1 = new UtilizatorDBRepository();
            foreach(Utilizator u in repo1.FindAll())
            {
                Console.WriteLine(u);
            }
            var server = new AppServicesImpl(repo1, repo3, repo2, repo);
            //var server = new ChatServerImpl();
            RemotingServices.Marshal(server, "Chat");
            //RemotingConfiguration.RegisterWellKnownServiceType(typeof(ChatServerImpl), "Chat",
            //    WellKnownObjectMode.Singleton);

            // the server will keep running until keypress.
            Console.WriteLine("Server started ...");
            Console.WriteLine("Press <enter> to exit...");
            Console.ReadLine();
        }
    }
}
