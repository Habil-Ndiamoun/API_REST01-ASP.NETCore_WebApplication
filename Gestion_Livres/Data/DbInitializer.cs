using Gestion_Livres.Models;

namespace Gestion_Livres.Data
{
    public static class DbInitializer
    {
        public static void Initialize(LivreContext context)
        {
            if (context.Livres.Any()
                && context.Exemplaires.Any())
            {
                return;   // DB has been seeded
            }

            var exemplaire1 = new Exemplaire { EstEmprunte = false, LivreId = 130 };
            var exemplaire2 = new Exemplaire { EstEmprunte = false, LivreId = 100 };
            var exemplaire3 = new Exemplaire { EstEmprunte = true, LivreId = 70 };
            var exemplaire4 = new Exemplaire { EstEmprunte = false, LivreId = 50 };
            var exemplaire5 = new Exemplaire { EstEmprunte = false, LivreId = 75 };
            var exemplaire6 = new Exemplaire { EstEmprunte = false, LivreId = 130 };
            var exemplaire7 = new Exemplaire { EstEmprunte = false, LivreId = 100 };
            var exemplaire8 = new Exemplaire { EstEmprunte = false, LivreId = 70 };
            var exemplaire9 = new Exemplaire { EstEmprunte = true, LivreId = 50 };
            var exemplaire10 = new Exemplaire { EstEmprunte = false, LivreId = 75 };

            var livres = new Livre[]
            {
                new Livre
                    {
                        Titre = "Homo deus : une brève histoire de l'avenir",
                        Exemplaires = new List<Exemplaire>
                            {
                                exemplaire1,
                                exemplaire2,
                                exemplaire3,
                                exemplaire4
                            }
                    },
                new Livre
                    {
                        Titre = "Les fourmis",
                        Exemplaires = new List<Exemplaire>
                            {
                                exemplaire5,
                                exemplaire6,
                                exemplaire7
                            }
                    },
                new Livre
                    {
                        Titre="Clean Code",
                        Exemplaires = new List<Exemplaire>
                            {
                                exemplaire8,
                                exemplaire9,
                                exemplaire10
                            }
                   }
            };

            context.Livres.AddRange(livres);
            context.SaveChanges();
        }
    }
}
