using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SearchSchool.Models
{
    public class School
    {
        // _id
        [JsonProperty("_id")]
       public int Id { get; set; }

        //"data_extracao": "2021-01-01T04:16:45",
        [JsonProperty("data_extracao")]
        public DateTime DataExtracao { get; set; }
        [JsonProperty("dep_administrativa")]

        //"dep_administrativa": "MUNICIPAL",
        public string DepAdministrativa { get; set; }
        //"tipo": "EDUCAÇÃO INFANTIL",
        [JsonProperty("tipo")]
        public string Tipo { get; set; }
        //"codigo": 101,
        [JsonProperty("codigo")]
        public int Codigo { get; set; }
        //"inep": 43107672,
        [JsonProperty("inep")]
        public int Inep { get; set; }
        //"nome": "EMEI JP CANTINHO AMIGO                                  ",
        [JsonProperty("nome")]
        public string Nome { get; set; }
        //"abr_nome": "CANTINHO                      ",
        [JsonProperty("abr_nome")]
        public string AbrNome { get; set; }
        //"logradouro": "PCA GARIBALDI                                     ",
        [JsonProperty("logradouro")]
        public string Logradouro { get; set; }
        //"numero": 1,
        [JsonProperty("numero")]
        public int Numero { get; set; }
        //"bairro": "AZENHA              ",
        [JsonProperty("bairro")]
        public string Bairro { get; set; }
        //"cep": 90050020,
        [JsonProperty("cep")]
        public int Cep { get; set; }
        //"latitude": -30.0441,
        [JsonProperty("latitude")]
        public double Latitude { get; set; }
        //"longitude": -51.2194
        [JsonProperty("longitude")]
        public double Longitude { get; set; }
        //"telefone": 32271906,
        [JsonProperty("telefone")]
        public string Telefone { get; set; }
        //"email": "emei.cantinhoamigo@smed.prefpoa.com.br            ",
        [JsonProperty("email")]
        public string Email { get; set; }
        //"url_website": "http://websmed.portoalegre.rs.gov.br/escolas/cantinhoamigo/                                         ",
        [JsonProperty("url_website")]
        public string UrlWebsite { get; set; }
        //"blog": "",
        [JsonProperty("blog")]
        public string Blog { get; set; }
        //"twitter": "",
        [JsonProperty("twitter")]
        public string Twitter { get; set; }
        //"facebook": ""
        [JsonProperty("facebook")]
        public string Facebook { get; set; }
        //"reg_conselho_tutelar": 8,
        [JsonProperty("reg_conselho_tutelar")]
        public int RegConselhoTutelar { get; set; }
        //"desc_reg_conselho_tutelar": "CENTRO",
        [JsonProperty("desc_reg_conselho_tutelar")]
        public string DescRegConselhoTutelar { get; set; }
        //"reg_orcamento_part": 16,
        [JsonProperty("reg_orcamento_part")]
        public int RegPrcamentoPart { get; set; }
        //"desc_reg_orcamento_part": "CENTRO",
        [JsonProperty("desc_reg_orcamento_part")]
        public string DescRegOrcamentoPart { get; set; }
        //"situacao_funcionamento": "EM ATIVIDADE",
        [JsonProperty("situacao_funcionamento")]
        public string SituacaoFuncionamento { get; set; }
        //"convenio_municipal": "NÃO",
        [JsonProperty("convenio_municipal")]
        public string ConvenioMunicipal { get; set; }
        //"convenio_estadual": "NÃO",
        [JsonProperty("convenio_estadual")]
        public string ConvenioEstadual { get; set; }
        //"convenio_federal": "NÃO",
        [JsonProperty("convenio_federal")]
        public string ConvenioFederal { get; set; }
        //"convenio_fasc": "NÃO",
        [JsonProperty("convenio_fasc")]
        public string ConvenioFasc { get; set; }
        //"escola_especial": "NÃO",
        [JsonProperty("escola_especial")]
        public string EscolaEspecial { get; set; }
        //"cat_part_privada": "NÃO",
        [JsonProperty("cat_part_privada")]
        public string CatPartPrivada { get; set; }
        //"cat_part_comunitaria": "NÃO",
        [JsonProperty("cat_part_comunitaria")]
        public string CatPartComunitaria { get; set; }
        //"cat_part_confessional": "NÃO",
        [JsonProperty("cat_part_confessional")]
        public string CatPartConfessional { get; set; }
        //"cat_part_filantropica": "NÃO",
        [JsonProperty("cat_part_filantropica")]
        public string CatPartFilantropica { get; set; }
        //"mant_empresa_privada": "NÃO",
        [JsonProperty("mant_empresa_privada")]
        public string MantEmpresaPrivada { get; set; }
        //"mant_sindicato": "NÃO",
        [JsonProperty("mant_sindicato")]
        public string MantSindicato { get; set; }
        //"mant_sistema_s": "NÃO",
        [JsonProperty("mant_sistema_s")]
        public string MantSistemaS { get; set; }
        //"mant_ong": "NÃO",
        [JsonProperty("mant_ong")]
        public string MantOng { get; set; }
        //"mant_apae": "NÃO"
        [JsonProperty("mant_apae")]
        public string MantApae { get; set; }

        public double Distance { get; set; }
    }
}
