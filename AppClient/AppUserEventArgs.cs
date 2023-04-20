using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppClient
{
    public enum AppUserEvent
    {
        NewInscriere,update
    };
    public class AppUserEventArgs : EventArgs
    {
        private readonly AppUserEvent userEvent;
        private readonly Object data;

        public AppUserEventArgs(AppUserEvent userEvent)
        {
            this.userEvent = userEvent;
        }

        public AppUserEventArgs(AppUserEvent userEvent, object data)
        {
            this.userEvent = userEvent;
            this.data = data;
        }

        public AppUserEvent UserEventType
        {
            get { return userEvent; }
        }

        public object Data
        {
            get { return data; }
        }
    }
}
