namespace FrontMVC.Models.Prato
{
    public class PratoModel
    {
        public Guid Id { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public string Descricao { get; set; }
        public string Foto { get; set; }
        public decimal Valor { get; set; }
        public bool Status { get; set; }
    }
}
