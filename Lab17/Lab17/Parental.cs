using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab17
{
    public class Parental
    {
        public int ParentalId { get; set; }
        public string FIO_rod { get; set; }
        public string Phone { get; set; }


        //один ко многим
        public ICollection<Children> Childrens { get; set; }
        //многие
        public virtual ICollection<TroopList> TroopLists { get; set; }
        public Parental()
        {
            TroopLists = new List<TroopList>();
            Childrens = new List<Children>();
        }
    }
}
