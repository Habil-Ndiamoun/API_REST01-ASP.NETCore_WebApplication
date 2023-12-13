using Gestion_Livres.Data;
using Gestion_Livres.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Livres.Services
{
    public class ExemplaireService
    {
        private readonly LivreContext m_context;
        private readonly LivreService m_service;
        public ExemplaireService(LivreContext p_context, LivreService p_service)
        {
            m_context = p_context;
            m_service = p_service;

        }

        //1.Demander la liste complète d'exemplaires d'un livre
        public IEnumerable<Exemplaire> GetAllExemplaires(int p_livreId)
        {
            if(p_livreId < 0)
            {
                throw new ArgumentOutOfRangeException("Le paramètre \"p_livreId\" doit être supérieur à 0", nameof(p_livreId));
            }

            var livre = m_context.Livres.FirstOrDefault(l => l.LivreId == p_livreId);

            if(livre == null) 
            {
                throw new InvalidOperationException($"Le livre de numéro {p_livreId} n'existe pas dans la bd!");
            }

            return m_context.Exemplaires
                    .AsNoTracking()
                    .Where(e => e.LivreId == livre.LivreId)
                    .ToList();
        }

        //2.Demander un seul exemplaire d'un livre 
        public Exemplaire? GetUnExemplaireById(int p_livreId, int p_exemplaireRechercheId)
        {
            if(p_livreId < 0)
            {
                throw new ArgumentOutOfRangeException("Le paramètre \"p_livreId\" doit être supérieur à 0", nameof(p_livreId));
            }
            if (p_exemplaireRechercheId < 0)
            {
                throw new ArgumentOutOfRangeException("Le paramètre \"p_exemplaireRechercheId\" doit être supérieur à 0", nameof(p_exemplaireRechercheId));
            }

            var livre = m_service.GetById(p_livreId);
            var exemplaireRecherche = m_context.Exemplaires.SingleOrDefault(e => e.LivreId == p_livreId && e.ExemplaireId == p_exemplaireRechercheId);

            if (livre == null || exemplaireRecherche == null)
            {
                throw new InvalidOperationException($"Le livre de numéro {p_livreId} ou l'exemplaire de numéro {p_exemplaireRechercheId} n'existe pas dans la bd!");
            }

            return exemplaireRecherche;
        }

        //3.Ajouter un nouvel exemplaire d'un livre
        public Exemplaire Create(int p_livreId, Exemplaire p_exemplaireAAjouter)
        {
            if(p_livreId < 0)
            {
                throw new ArgumentOutOfRangeException("Le paramètre \"p_livreId\" doit être supérieur à 0", nameof(p_livreId));
            }

            if(p_exemplaireAAjouter == null)
            {
                throw new ArgumentNullException($"Le paramètre {p_exemplaireAAjouter} ne peut pas être null", nameof(p_exemplaireAAjouter));
            }

            if(!m_context.Livres.Any(l => l.LivreId == p_livreId))
            {
                throw new InvalidOperationException($"Le livre de numéro {p_livreId} n'existe pas dans la base de données!");
            }

            m_context.Exemplaires.Add((p_exemplaireAAjouter));
            m_context.SaveChanges();

            return p_exemplaireAAjouter;
        }

        //4.Modifier un exemplaire
        public Exemplaire Update(int p_livreId, int p_exemplaireAModifierId, Exemplaire p_nouvelExemplaire)
        {
            if (p_livreId < 0)
            {
                throw new ArgumentOutOfRangeException("Le paramètre \"p_livreId\" doit être supérieur à 0", nameof(p_livreId));
            }
            if (p_exemplaireAModifierId < 0)
            {
                throw new ArgumentOutOfRangeException("Le paramètre \"p_exemplaireAModifierId\" doit être supérieur à 0", nameof(p_exemplaireAModifierId));
            }

            if (p_nouvelExemplaire == null)
            {
                throw new ArgumentNullException($"Le paramètre {p_nouvelExemplaire} ne peut pas être null", nameof(p_nouvelExemplaire));
            }

            var livre = m_service.GetById(p_livreId);
            var exemplaireAModifier = GetUnExemplaireById(p_livreId, p_exemplaireAModifierId);

            if (livre == null)
            {
                throw new InvalidOperationException($"Le livre de numéro {p_livreId} n'existe pas dans la bd!");
            }

            if (exemplaireAModifier == null)
            {
                throw new InvalidOperationException($"L'exemplaire de numéro {p_exemplaireAModifierId} n'existe pas dans la base de données!");
            }

            m_context.Exemplaires.Update(p_nouvelExemplaire);
            m_context.SaveChanges();

            return p_nouvelExemplaire;
        }

        //5.Supprimer un exemplaire
        public void DeleteById(int p_livreId, int p_exemplaireId)
        {
            if (p_livreId < 0)
            {
                throw new ArgumentOutOfRangeException("Le paramètre \"p_livreId\" doit être supérieur à 0", nameof(p_livreId));
            }

            if (p_exemplaireId < 0)
            {
                throw new ArgumentOutOfRangeException("Le paramètre \"p_exemplaireId\" doit être supérieur à 0", nameof(p_exemplaireId));
            }

            var livre = m_service.GetById(p_livreId);

            if (livre == null)
            {
                throw new InvalidOperationException($"Le livre de numéro {p_livreId} n'existe pas dans la bd!");
            }

            var exemplaireASupprimer = m_context.Exemplaires.SingleOrDefault(e => e.LivreId == p_livreId && e.ExemplaireId == p_exemplaireId);

            if (exemplaireASupprimer == null)
            {
                throw new InvalidOperationException($"L'exemplaire de numéro {exemplaireASupprimer} n'existe pas dans la bd!");
            }

            m_context.Exemplaires.Remove(exemplaireASupprimer);
            m_context.SaveChanges();

        }
    }
}
