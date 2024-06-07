using System.ComponentModel.DataAnnotations;

namespace ProjetoCQW.Model
{
    public class EntidadeBase
    {
        [Key]
        public int id { get; set; }

        public DateTime DataCriacao { get; set; }

        public DateTime DataAtualizacao { get;  set; }

        public EntidadeBase() 
        { 
            DataCriacao = DateTime.Now;
            DataAtualizacao = DateTime.Now;
        }

        public void AtualizarData()
        {
            DataAtualizacao = DateTime.Now;
        }
    }
}
