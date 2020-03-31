using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Lab17
{
    public class Children
    {
        public int ChildrenId { get; set; }
        public string FIO_reb { get; set; }
        public string Birthdate { get; set; }


        // Многие к одному
        public Parental Parental { get; set; }
        
        // один к одной
        public CardChildren Cards { get; set; }

        //один ко многим
        public TroopList TroopList { get; set; }
    }
}
