# Ao subir o projeto, umas das pastas não subiu. Camada Services > Models> Logs que continha as classes EpidemiologicaIDataLog e UserAcessLog. 

Propriedades das classes: 
#   public class EpidemiologicalDataLog
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

#     public class UserAccessLog
    {
        public int Id { get; set; } 
        public string usuarioId { get; set; } 
        public DateTime DataAcesso { get; set; } 
        public string Acao { get; set; } 
    }
 }


# No Controller DadosEpidemiologicos, é necessário atualizar a rota do método GetDadosFiltrados. A rota correta para esse endPoint é [HttpGet("municipios/filtrados")], com isso, o swagger exibirá os endpoints. 
