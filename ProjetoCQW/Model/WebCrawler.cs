namespace ProjetoCQW.Model
{
    public class WebCrawler
    {
        public string Nome { get; set; }
        public string Valor { get; set; }
        public string Modelo { get; set; }
        public string Imagem { get; set; }
        public string Cor { get; set; }
        public string Url { get; set; }


        public ModeloCarro ModeloCarro { get; set; }
        public ModeloSiteDetalhe ModeloSiteDetalhe { get; set; }
    }
}
