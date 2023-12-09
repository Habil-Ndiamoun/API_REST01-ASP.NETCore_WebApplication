namespace Gestion_Livres.Models
{
    public class Livre
    {
        public int LivreId { get; set; }
        public string? Titre { get; set; }   
        public ICollection<Exemplaire>? Exemplaires { get; set; }
    }
}
