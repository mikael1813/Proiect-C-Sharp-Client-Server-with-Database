using AppModel;
using AppServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppClient
{
    public class AppClientCtrl : MarshalByRefObject, IAppObserver
    {
        public event EventHandler<AppUserEventArgs> updateEvent; //ctrl calls it when it has received an update
        private readonly IAppServices services;
        private Utilizator currentUser;
        public AppClientCtrl(IAppServices services)
        {
            this.services = services;
            currentUser = null;
        }

        public List<int> getNrParticipanti()
        {
            return services.getNrParticipanti();
        }

        public IEnumerable<Proba> getProbe()
        {
            return services.getProbe();
        }

        public void login(String userId, String pass)
        {
            Utilizator user = new Utilizator(userId, pass);
            services.login(user, this);
            Console.WriteLine("Login succeeded ....");
            currentUser = user;
            Console.WriteLine("Current user {0}", user);
        }

        public IEnumerable<Participant> getParticipantiDupaProba(Proba p)
        {
            return services.getParticipantiDupaProba(p);
        }

        public IEnumerable<Proba> getProbeDupaParticipanti(Participant p)
        {
            return services.getProbeDupaParticipanti(p);
        }

        public void Inscrie(Participant p, List<Proba> list)
        {
            services.Inscrie(p, list);
        }

        //public void messageReceived(Message message)
        //{
        //    String mess = "[" + message.Sender.Id + "]: " + message.Text;
        //    ChatUserEventArgs userArgs = new ChatUserEventArgs(ChatUserEvent.NewMessage, mess);
        //    Console.WriteLine("Message received");
        //    OnUserEvent(userArgs);
        //}

        //public void friendLoggedIn(User friend)
        //{
        //    Console.WriteLine("Friend logged in " + friend);
        //    ChatUserEventArgs userArgs = new ChatUserEventArgs(ChatUserEvent.FriendLoggedIn, friend.Id);
        //    OnUserEvent(userArgs);
        //}

        //public void friendLoggedOut(User friend)
        //{
        //    Console.WriteLine("Friend logged out" + friend);
        //    ChatUserEventArgs userArgs = new ChatUserEventArgs(ChatUserEvent.FriendLoggedOut, friend.Id);
        //    OnUserEvent(userArgs);
        //}

        public void logout()
        {
            Console.WriteLine("Ctrl logout");
            services.logout(currentUser, this);
            currentUser = null;
        }

        protected virtual void OnUserEvent(AppUserEventArgs e)
        {
            if (updateEvent == null) return;
            updateEvent(this, e);
            Console.WriteLine("Update Event called");
        }
        //public IList<String> getLoggedFriends()
        //{
        //    IList<String> loggedFriends = new List<string>();
        //    User[] friends = services.getLoggedFriends(currentUser);
        //    foreach (var user in friends)
        //    {
        //        loggedFriends.Add(user.Id);
        //    }
        //    return loggedFriends;
        //}

        //public void sendMessage(string id, string txt)
        //{
        //    //display the sent message on the user window
        //    String mess = "[" + currentUser.Id + "-->" + id + "]: " + txt;
        //    ChatUserEventArgs userArgs = new ChatUserEventArgs(ChatUserEvent.NewMessage, mess);
        //    OnUserEvent(userArgs);
        //    //sends the message to the server
        //    User receiver = new User(id);
        //    Message message = new Message(currentUser, receiver, txt);
        //    services.sendMessage(message);
        //}

        public void newInscriere()
        {
            Console.WriteLine("New Inscriere");
            AppUserEventArgs userArgs = new AppUserEventArgs(AppUserEvent.NewInscriere);
            OnUserEvent(userArgs);
        }

        public void update(IEnumerable<Proba> list)
        {
            Console.WriteLine("Update");
            AppUserEventArgs userArgs = new AppUserEventArgs(AppUserEvent.update,list);
            OnUserEvent(userArgs);
        }

    }
}
