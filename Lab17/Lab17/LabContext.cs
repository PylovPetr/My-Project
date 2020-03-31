using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab17
{
    public class LabContext : DbContext
    {
        public DbSet<Children> Childrens { get; set; }
        public DbSet<Parental> Parentals { get; set; }
        public DbSet<CardChildren> CardChildrens { get; set; }
        public DbSet<TroopList> TroopListss { get; set; }

        public LabContext() : base("LabContext")
        { }

        static LabContext()
        {
            Database.SetInitializer(new Init());
        }

        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //многие ко многим
            modelBuilder.Entity<Parental>()
                .HasMany(pt => pt.TroopLists)
                .WithMany(p => p.Parentals);
            //один ко многим
            modelBuilder.Entity<Parental>()
                .HasMany(p => p.Childrens)
                .WithRequired(p => p.Parental);
            //один ко многим
            modelBuilder.Entity<TroopList>()
                .HasMany(p => p.Childrens)
                .WithRequired(p => p.TroopList);
            //один к одной
            modelBuilder.Entity<CardChildren>()
                .HasRequired(c => c.Children)
                .WithRequiredPrincipal(c => c.Cards);
        }
    }

    public class Init : DropCreateDatabaseAlways<LabContext>
    {
        protected override void Seed(LabContext context)
        {
            var tp1 = new TroopList { Place = "A", NumberRoom = "123", };
            var tp2 = new TroopList { Place = "B", NumberRoom = "563", };
            var tp3 = new TroopList { Place = "C", NumberRoom = "123", };
            var tp4 = new TroopList { Place = "C", NumberRoom = "123", };
            var tp5 = new TroopList { Place = "C", NumberRoom = "123", };

            var childrens = new List<Parental>
            {
               new Parental {FIO_rod = "ППП", Phone= "8(495)999-000-12", TroopLists = new[] { tp1, tp2 }},
               new Parental {FIO_rod = "ПП1", Phone= "8(495)456-000-88", TroopLists = new[] { tp2, tp3, tp4 }},
               new Parental {FIO_rod = "ПП2", Phone= "8(495)111-000-56", TroopLists = new[] { tp1, tp4, tp5 }},
            };

            childrens.ForEach(c => context.Parentals.Add(c));
            context.SaveChanges();


            Children child1 = new Children { FIO_reb = "Колодезов Роберт Андреевич", Birthdate = "12.04.02" };
            Children child2 = new Children { FIO_reb = "Фирман Том Карлович", Birthdate = "12.05.02"};
            Children child3 = new Children { FIO_reb = "Фирман Том Карлович", Birthdate = "12.05.02"};
            CardChildren card = new CardChildren { Information = "Талантливый художник" };
            CardChildren card2 = new CardChildren { Information = "Шахматист уровня гроссмейстера" };
            CardChildren card3 = new CardChildren { Information = "Шахматист уровня гроссмейстера" };

            context.CardChildrens.Add(card);
            context.Childrens.Add(child1);
            context.SaveChanges();
            context.CardChildrens.Add(card2);
            context.Childrens.Add(child2);
            context.SaveChanges();
            context.CardChildrens.Add(card3);
            context.Childrens.Add(child3);
            context.SaveChanges();
        }
    }
}