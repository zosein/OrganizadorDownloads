
namespace OrganizadorDownloads.Core.Models
{
    public class OrganizacaoResultado
    {
        public int ArquivosMovidos { get; set; }
        public int ArquivosIgnorados { get; set; }
        public long TamanhoTotalMovido { get; set; }
        public Dictionary<string, int> ArquivosPorCategoria { get; set; } = new();
        public List<string> Erros { get; set; } = new();
    }
}