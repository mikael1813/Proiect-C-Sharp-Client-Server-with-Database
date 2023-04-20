using AppModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_C_Sharp_Client_Server
{
    public interface ParticipantRepository : Repository<int, Participant>
    {
        IEnumerable<Participant> filterByName(String name);

        IEnumerable<Participant> filterByVarsta(int varsta);
    }
}
