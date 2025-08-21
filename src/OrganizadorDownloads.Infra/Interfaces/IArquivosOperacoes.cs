namespace OrganizadorDownloads.Infra.Interfaces;

public interface IArquivosOperacoes
{
    bool DiretorioExiste(string caminho);
    void CriarDiretorio(string caminho);
    string[] ObterArquivos(string caminho, string filtroBusca = "*.*");
    string ObterArquivosExtensao(string caminhoArquivo);
    DateTime ObterDataModificacao(string caminhoArquivo);
    void MoverArquivo(string caminhoFonte, string caminhoDestino);
    string ObterArquivoNome(string caminhoArquivo);
    long ObterTamanhoArquivo(string caminhoArquivo);
}
