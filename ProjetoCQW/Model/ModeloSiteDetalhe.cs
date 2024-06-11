using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoCQW.Model
{
    [Table("ModeloSiteDetalhe")]
    public class ModeloSiteDetalhe : EntidadeBase
    {
        public ModeloSiteDetalhe() { }
       
        public ModeloSiteDetalhe(string url, string modelo, string ano, string cor, string img, string valor)
        {
            this.UrlSite = url;
            this.XpathModelo = modelo;
            this.XpathAno = ano;
            this.XpathCor = cor;
            this.XpathImg = img;
            this.XpathValor = valor;
            this.DataCriacao = DateTime.Now;
            this.DataAtualizacao = DateTime.Now;

        }

        public string UrlSite { get; set; }
        public string XpathModelo { get; set; }
        public string XpathAno { get; set; }
        public string XpathCor { get; set; }
        public string XpathImg { get; set; }
        public string XpathValor { get; set; }

        public ModeloCarro ModeloCarro { get; set; }
        
        public int ModeloCarro_Id { get; set; }


    }
}
