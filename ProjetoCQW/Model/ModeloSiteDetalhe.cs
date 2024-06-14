using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoCQW.Model
{
    [Table("ModeloSiteDetalhe")]
    public class ModeloSiteDetalhe : EntidadeBase
    {
        public ModeloSiteDetalhe() { }

        public ModeloSiteDetalhe(string url, string modelo, string cor, string img, string valor, string nome)
        {
            this.UrlSite = url;
            this.XpathModelo = modelo;
            this.XpathCor = cor;
            this.XpathImg = img;
            this.XpathValor = valor;
            this.XpathNome = nome;
            this.DataCriacao = DateTime.Now;
            this.DataAtualizacao = DateTime.Now;

        }

        public string XpathNome { get; set; }
        public string UrlSite { get; set; }
        public string XpathModelo { get; set; }
        public string XpathCor { get; set; }
        public string XpathImg { get; set; }
        public string XpathValor { get; set; }



    }
}