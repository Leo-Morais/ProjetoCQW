using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoCQW.Model
{
    [Table("Montadora")]
    public class Montadora : EntidadeBase
    {

        public string Nome { get;  set; }
        
        public string UrlSite { get; set; }


        public Montadora(string nome, string urlSite)
        {
            this.Nome = nome;
            this.UrlSite = urlSite;
            this.DataCriacao = DateTime.Now;
            this.DataAtualizacao = DateTime.Now;
        }

    }
}
