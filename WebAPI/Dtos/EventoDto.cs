using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dtos
{
    public class EventoDto
    {
        public int Id { get; set; }
        [Required (ErrorMessage="Campo Obrigatório")]
        [StringLength(100, MinimumLength=3, ErrorMessage="Local é entre 3 a 100 caracteres")]
        public string Local { get; set; }
        public string DataEvento { get; set; }
        [Required (ErrorMessage="Campo Tema obrigatório")]
        public string Tema { get; set; }
        [Range(2, 12000, ErrorMessage="Quantidade de Pessoas entre 2 e 12000")]
        public int QtdPessoas { get; set; }
        public string ImagemUrl { get; set; }
        public string Telefone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public List<LoteDto> Lotes { get; set; }
        public List<RedeSocialDto> RedesSociais { get; set; }
        public List<PalestranteDto> Palestrantes { get; set; }
    }
}