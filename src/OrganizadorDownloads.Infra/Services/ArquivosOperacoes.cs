using OrganizadorDownloads.Infra.Interfaces;

namespace OrganizadorDownloads.Infra.Services;

public class ArquivosOperacoes : IArquivosOperacoes
{
    public bool DiretorioExiste(string caminho) => Directory.Exists(caminho);
    public void CriarDiretorio(string caminho) => Directory.CreateDirectory(caminho);
    public string[] ObterArquivos(string caminho, string filtroBusca = "*.*") => Directory.GetFiles(caminho, filtroBusca);
    public string ObterArquivosExtensao(string caminhoArquivo) => Path.GetExtension(caminhoArquivo).ToLowerInvariant();
    public DateTime ObterDataModificacao(string caminhoArquivo) => File.GetLastWriteTime(caminhoArquivo);
    public void MoverArquivo(string caminhoFonte, string caminhoDestino)
    {
        var diretorioDestino = Path.GetDirectoryName(caminhoDestino);
        if (!string.IsNullOrEmpty(diretorioDestino))
        {
            CriarDiretorio(diretorioDestino);
        }
        File.Move(caminhoFonte, caminhoDestino, overwrite: true);
    }

    public string ObterArquivoNome(string caminhoArquivo) => Path.GetFileName(caminhoArquivo);
    public long ObterTamanhoArquivo(string caminhoArquivo) => new FileInfo(caminhoArquivo).Length;
}