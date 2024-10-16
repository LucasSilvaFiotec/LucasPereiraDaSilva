namespace InfoDengue.Api.Models.Relatorios
{
    public class DadosEpidemiologicos
    {
        public string Municipio { get; set; }
        public string DataSemanaInicio { get; set; }
        public string DataSemanaFim { get; set; }
        public string Arbovirose { get; set; }
        public int CasosConfirmados { get; set; }
    }
}