using System.ComponentModel.DataAnnotations;

namespace Plot.WebAPI.DTO
{
    public class FilmeDTO : IValidatableObject
    {
        [Required(ErrorMessage = "O título do filme é obrigatório.")]
        [StringLength(100, ErrorMessage = "O título deve ter no máximo 100 caracteres.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "A sinopse do filme é obrigatória.")]
        public string Sinopse { get; set; }

        [Required(ErrorMessage = "A data de lançamento é obrigatória.")]
        [DataType(DataType.Date)]
        public DateTime DataLancamento { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "A duração deve ser positiva.")]
        public int Duracao { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ArquivoImagem is not null && !string.isNullOrWhiteSpace(imagemCapa){

                yield return new ValidationResult;
            }
        }
    }
}