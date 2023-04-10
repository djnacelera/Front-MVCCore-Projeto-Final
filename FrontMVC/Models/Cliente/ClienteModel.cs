namespace FrontMVC.Models.Cliente
{
    public class ClienteModel
    {
        public Guid Id { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
    }
}
