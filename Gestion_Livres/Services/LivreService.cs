using Gestion_Livres.Data;
using Gestion_Livres.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Livres.Services
{
    public class LivreService
    {
        public readonly LivreContext m_context;
        public LivreService(LivreContext p_context)
        {
            m_context = p_context;
        }

        //1.Demander la liste complète de livres
        public IEnumerable<Livre> GetAll() 
        { 
            return m_context.Livres
                    .Include(l => l.Exemplaires)
                    .AsNoTracking()
                    .ToList();
        }

        //2.Demander un seul livre 
        public Livre? GetById(int p_id)
        {
            if(p_id < 0)
            {
                throw new ArgumentOutOfRangeException("Le paramètre \"p_id\" doit être supérieur à 0", nameof(p_id));
            }

            return m_context.Livres
                    .Include(l => l.Exemplaires)
                    .AsNoTracking()
                    .FirstOrDefault(l => l.LivreId == p_id);
        }

        //3.Ajouter un nouveau livre
        public Livre? Create(Livre p_nouveauLivre)
        {
            if(p_nouveauLivre == null)
            {
                throw new ArgumentNullException($"Le paramètre {p_nouveauLivre} ne peut pas être null", nameof(p_nouveauLivre));
            }

            if(m_context.Livres.Any(l => l.LivreId == p_nouveauLivre.LivreId))
            {
                throw new InvalidOperationException($"Le livre de numéro {p_nouveauLivre.LivreId} existe déjà dans la base de données!");
            }

            m_context.Livres.Add(p_nouveauLivre);
            m_context.SaveChanges();

            return p_nouveauLivre;
        }

        //4.Ajouter Un exemplaire dans un livre
        public void AddUnExemplaireDansUnLivre(int p_idLivre, int p_idExemplaireAAjouter)
        {
            if (p_idLivre < 0)
            {
                throw new ArgumentOutOfRangeException("Le paramètre \"p_idLivre\" doit être supérieur à 0", nameof(p_idLivre));
            }
            if (p_idExemplaireAAjouter < 0)
            {
                throw new ArgumentOutOfRangeException("Le paramètre \"p_idExemplaireAAjouter\" doit être supérieur à 0", nameof(p_idExemplaireAAjouter));
            }

            var livre = m_context.Livres.Find(p_idLivre);
            var exemplaireAAjouter = m_context.Exemplaires.Find(p_idExemplaireAAjouter);

            if(livre == null || exemplaireAAjouter == null)
            {
                throw new InvalidOperationException($"Le livre de numéro {p_idLivre} ou l'exemplaire de numéro {p_idExemplaireAAjouter} n'existe pas dans la bd!");
            }

            if(livre.Exemplaires == null)
            {
                livre.Exemplaires = new List<Exemplaire>();
            }

            livre.Exemplaires.Add(exemplaireAAjouter);
            m_context.SaveChanges();
        }

        //5.Modifier un livre
        public Livre? Update(Livre p_livre)
        {
            if (p_livre == null) 
            {
                throw new ArgumentNullException($"Le paramètre {p_livre} ne peut pas être null", nameof(p_livre));
            }

            var livreAModifier = m_context.Livres.FirstOrDefault(l => l.LivreId == p_livre.LivreId);

            if(livreAModifier == null)
            {
                throw new InvalidOperationException($"Le livre de numéro {p_livre.LivreId} n'existe pas dans la base de données!");
            }

            m_context.Livres.Update(livreAModifier);
            m_context.SaveChanges();

            return livreAModifier;
        }

        //6.Supprimer un livre par Id:
        public void DeleteById(int p_id)
        {
            if (p_id < 0)
            {
                throw new ArgumentOutOfRangeException("Le paramètre \"p_id\" doit être supérieur à 0", nameof(p_id));
            }

            var livreASupprimer = m_context.Livres.Find(p_id);

            if (livreASupprimer == null)
            {
                throw new InvalidOperationException($"Le livre de numéro {p_id} n'existe pas dans la base de données!");
            }

            m_context.Livres.Remove(livreASupprimer);
            m_context.SaveChanges();
        }
    }
}
