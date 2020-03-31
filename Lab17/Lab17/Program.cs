using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Lab17
{
    class Program
    {
        static void Main(string[] args)
        {
            LabContext db = new LabContext();
            db.Database.CreateIfNotExists();

        //explicit
            var childrens = db.Childrens.ToList();
         
            foreach (var children in childrens)
            {
                Console.WriteLine("1. День рождения ребенка: {0} \n" +
                    "2. ФИО родителя: {1} ",
                    children.Birthdate, children.FIO_reb);
                //db.Entry(children).Collection("TroopList").Load();
                //foreach (var troop in children.Parental.TroopLists)
                //{
                //    Console.WriteLine("4. Место проживания ребёнка: " + troop.Place);
                //}
                Console.WriteLine();
            }

        //lazzy load
            //var childrens = db.Childrens.ToList();

            //foreach (var children in childrens)
            //{
            //    Console.WriteLine("1. День рождения ребенка: {0} \n" +
            //        "2. ФИО родителя: {1} \n",
            //        "3. Информация личной карточки ребёнка: {2}",
            //        children.Birthdate, children.Parental.FIO_rod, children.Cards.Information);

            //    foreach (var troop in children.Parental.TroopLists)
            //    {
            //        Console.WriteLine("4. Место проживания ребёнка: " + troop.Place);
            //    }
            //    Console.WriteLine();
            //}
        //eager
            //var childrens = db.Childrens.Include(t => t.TroopList).ToList();

            //foreach (var children in childrens)
            //{
            //    Console.WriteLine("1. День рождения ребенка: {0} \n" +
            //        "2. ФИО родителя: {1} \n",
            //        "3. Информация личной карточки ребёнка: {2}",
            //        children.Birthdate, children.Parental.FIO_rod, children.Cards.Information);
            //    db.Entry(children).Collection("TroopList").Load();
            //    foreach (var troop in children.Parental.TroopLists)
            //    {
            //        Console.WriteLine("4. Место проживания ребёнка: " + troop.Place);
            //    }
            //    Console.WriteLine();
            //}
            Console.WriteLine("End");
            Console.Read();
        }
    }
}
