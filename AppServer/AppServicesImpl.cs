﻿using AppModel;
using AppServices;
using Proiect_C_Sharp_Client_Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppServer
{
    class AppServicesImpl : MarshalByRefObject, IAppServices
    {
        private UtilizatorRepository utilizatorRepository;
        private ProbaRepository probaRepository;
        private ParticipantRepository participantRepository;
        private InscriereRepository inscriereRepository;
        private readonly IDictionary<int, IAppObserver> loggedClients;
        public AppServicesImpl(UtilizatorRepository repo1, ProbaRepository probaRepository, ParticipantRepository participantRepository, InscriereRepository inscriereRepository)
        {

            utilizatorRepository = repo1;
            this.probaRepository = probaRepository;
            this.participantRepository = participantRepository;
            this.inscriereRepository = inscriereRepository;
            loggedClients = new Dictionary<int, IAppObserver>();
        }
        public List<int> getNrParticipanti()
        {
            List<int> list = new List<int>();
            foreach(Proba p in probaRepository.FindAll())
            {
                int s = 0;
                foreach(Inscriere i in inscriereRepository.FindAll())
                {
                    if(i.Proba.id == p.id)
                    {
                        s = s + 1;
                    }
                }
                list.Add(s);
            }
            return list;
        }

        public IEnumerable<Participant> getParticipantiDupaProba(Proba p)
        {
            List<Participant> list = new List<Participant>();

            foreach (Inscriere i in inscriereRepository.FindAll())
            {
                if (i.Proba.id == p.id)
                {
                    list.Add(i.Participant);
                }
            }
            return list;
        }

        public IEnumerable<Proba> getProbe()
        {
            return probaRepository.FindAll();
        }

        public IEnumerable<Proba> getProbeDupaParticipanti(Participant p)
        {
            List<Proba> list2 = new List<Proba>();

            foreach (Inscriere i in inscriereRepository.FindAll())
            {
                if (i.Participant.id == p.id)
                {
                    list2.Add(i.Proba);
                }
            }
            return list2;
        }

        public void Inscrie(Participant participant, List<Proba> probe)
        {
            bool ok = false;
            foreach (Participant p in participantRepository.FindAll())
            {
                if (p.nume == participant.nume && p.varsta == participant.varsta)
                {
                    ok = true;
                    participant.id = p.id;
                }
            }

            if (!ok)
            {
                participantRepository.Save(participant);
            }

            foreach (Participant p in participantRepository.FindAll())
            {
                if (p.nume == participant.nume && p.varsta == participant.varsta)
                {
                    participant.id = p.id;
                }
            }

            foreach (Proba proba in probe)
            {
                if (!existaInscriere(participant, proba))
                {
                    Inscriere inscriere = new Inscriere(participant, proba);
                    inscriereRepository.Save(inscriere);
                }
            }
            newInscriere();
        }

        public bool existaInscriere(Participant participant, Proba proba)
        {
            foreach (Inscriere i in inscriereRepository.FindAll())
            {
                if (i.Participant.id == participant.id && i.Proba.id == proba.id)
                {
                    return true;
                }
            }
            return false;
        }

        public void login(Utilizator user, IAppObserver client)
        {
            Console.WriteLine("[ChatServicesImpl.login] {0}" + user);
            bool loginOk = false;
            foreach(Utilizator u in utilizatorRepository.FindAll())
            {
                if(u.user == user.user && u.parola == user.parola)
                {
                    loginOk = true;
                    user.id = u.id;
                }
            }
            if (loginOk)
            {
                if (loggedClients.ContainsKey(user.id))
                    throw new AppException("User already logged in.");
                loggedClients[user.id] = client;
                //notifyFriendsLoggedIn(user);
            }
            else
                throw new AppException("Authentication failed.");
        }


        private void newInscriere()
        {
            List<Thread> threads = new List<Thread>();
            List<Utilizator> users = (List<Utilizator>)utilizatorRepository.FindAll();
            Console.WriteLine("notify logged friends " + users.Count());
            foreach (Utilizator us in users)
            {
                if (loggedClients.ContainsKey(us.id))
                {
                    IAppObserver chatClient = loggedClients[us.id];
                    chatClient.update(getProbe());
                    var t = new Thread(() => ThreadProc(chatClient));
                    threads.Add(t);
                }
            }
            foreach(Thread t in threads)
            {
                t.Start();
            }
            foreach (Thread t in threads)
            {
                t.Join();
            }
        }

        public static void ThreadProc(IAppObserver chatClient)
        {
            chatClient.newInscriere();
        }

        public void logout(Utilizator user, IAppObserver client)
        {
            foreach (Utilizator u in utilizatorRepository.FindAll())
            {
                if (u.user == user.user && u.parola == user.parola)
                {
                    user.id = u.id;
                }
            }
            IAppObserver localClient = loggedClients[user.id];
            if (localClient == null)
                throw new AppException("User " + user.id + " is not logged in.");
            loggedClients.Remove(user.id);
            //notifyFriendsLoggedOut(user);
        }
    }
}
