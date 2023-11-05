
namespace MicrobApp.Models
{
    public class Instance
    {
        public int TenantInstanceId { get; set; }
        public string Nombre { get; set; }
        public string Dominio { get; set; }
        public string Logo { get; set; }
        public string Tematica { get; set; }
        public string Description { get; set; }
        public int EsquemaColores { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
