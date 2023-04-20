using AppModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServices
{
    public interface IAppObserver
    {
        void newInscriere();
        void update(IEnumerable<Proba> list);
    }
}
