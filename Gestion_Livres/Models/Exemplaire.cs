namespace Gestion_Livres.Models
{
    public class Exemplaire
    {
        public int ExemplaireId { get; set; }
        public bool EstEmprunte { get; set; }
        public int LivreId { get; set; }
    }
}
