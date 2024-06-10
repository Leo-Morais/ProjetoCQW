using System.Drawing;
using System.Runtime.ConstrainedExecution;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoCQW.Model
{
    [Table("ModeloCarro")]
    public class ModeloCarro : EntidadeBase
    {
        public ModeloCarro() { }

        public ModeloCarro(int ano, string nome,string versao, string imagem, string cor, float valor, int montadoraId)
        { 
            this.Ano = ano;
            this.Nome = nome;
            this.Imagem = imagem;
            this.Cor = cor;
            this.Valor = valor;
            this.Versao = versao;
            this.DataCriacao = DateTime.Now;
            this.DataAtualizacao = DateTime.Now;
            //this.Montadora_Id = montadoraId;
        }

        public int Ano { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }
        public string Cor { get; set; }
        public float Valor { get; set; }
        public string Versao { get; set; }

        //Ligar para foreign key
        public Montadora Montadora { get; set; }
        public int Montadora_Id { get; set; }

        
    }
}
