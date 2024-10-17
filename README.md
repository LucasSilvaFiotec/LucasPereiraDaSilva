# Ao subir o projeto, umas das pastas não subiu. Camada Services > Models> Logs que continha as classes EpidemiologicaIDataLog e UserAcessLog. 
# propriedades das classes: 
 # namespace InfoDengue.Api.Models.Logs
{
   public class EpidemiologicalDataLog
    {
        public int Id { get; set; } 
        public DateTime Data { get; set; } 
        public string Localizacao { get; set; } 
        public int CasosReportados { get; set; } 
        public int MortesReportadas { get; set; } 
        public string Observacoes { get; set; } 
    }
}
#
namespace InfoDengue.Api.Models.Logs
{
    public class UserAccessLog
    {
        public int Id { get; set; } 
        public string usuarioId { get; set; } 
        public DateTime DataAcesso { get; set; } 
        public string Acao { get; set; } 
    }
}
