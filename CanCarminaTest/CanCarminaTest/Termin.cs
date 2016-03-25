using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanCarminaAppo1.Assets
{
    class Termin
    {
        int ID;
        DateTime DatumBeg;
        string Detail;
        bool Status;
        public Termin(int ID, DateTime DateBeg, string Deail, bool status)
        {
            this.ID = ID;
            this.DatumBeg = DatumBeg;
            this.Detail = Detail;
            this.Status = status;
        }
    }
}
