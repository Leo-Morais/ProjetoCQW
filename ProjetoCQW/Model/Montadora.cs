using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoCQW.Model
{
    [Table("Montadora")]
    public class Montadora : EntidadeBase
    {

        public required string  Nome { get;  set; }
        
        public required string UrlSite { get; set; }


        public Montadora() { }

        public Montadora(string nome, string urlSite)
        {
            this.Nome = nome;
            this.UrlSite = urlSite;
            this.DataCriacao = DateTime.Now;
            this.DataAtualizacao = DateTime.Now;
        }

       
    }
}
