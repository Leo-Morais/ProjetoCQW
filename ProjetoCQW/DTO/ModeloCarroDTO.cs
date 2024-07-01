namespace ProjetoCQW.DTO
{
    public class ModeloCarroDTO
    {
        public int Ano { get; set; }
        public string Nome { get; set; }
        public string Cor { get; set; }
        public float Valor { get; set; }
        public string Versao { get; set; }
        public string Imagem { get; set; }

        public int Montadora_Id { get; set; }
        public int ModeloSite_Id { get; set; }
    }
}
