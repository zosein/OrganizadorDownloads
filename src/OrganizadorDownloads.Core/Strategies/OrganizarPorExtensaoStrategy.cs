using OrganizadorDownloads.Core.Interfaces;
using OrganizadorDownloads.Core.Models;
using OrganizadorDownloads.Core.Services;
using OrganizadorDownloads.Infra.Interfaces;

namespace OrganizadorDownloads.Core.Strategies;

public class OrganizarPorExtensaoStrategy : IOrganizacao
{
    private readonly IArquivosOperacoes _arquivosOperacoes;

    public string Nome => "extension";

    public OrganizarPorExtensaoStrategy(IArquivosOperacoes arquivosOperacoes)
    {
        _arquivosOperacoes = arquivosOperacoes;
    }

    public async Task<OrganizacaoResultado> OrganizarAsync(string diretorioFonte, CancellationToken token = default)
    {
        var resultado = new OrganizacaoResultado();

        if (!_arquivosOperacoes.DiretorioExiste(diretorioFonte))
            throw new DirectoryNotFoundException($"O diretório {diretorioFonte} não existe.");

        var arquivos = _arquivosOperacoes.ObterArquivos(diretorioFonte);
        foreach (var arquivo in arquivos)
        {
            try
            {
                token.ThrowIfCancellationRequested();

                var extension = _arquivosOperacoes.ObterArquivosExtensao(arquivo);
                var categoria = ArquivoCategoriaService.ObterCategoriaPorExtensao(extension);
                var categoriaPasta = ArquivoCategoriaService.ObterCategoriaPastaNome(categoria);

                resultado.ArquivosMovidos++;
                resultado.TamanhoTotalMovido += new System.IO.FileInfo(arquivo).Length;

                if (resultado.ArquivosPorCategoria.ContainsKey(categoriaPasta))
                    resultado.ArquivosPorCategoria[categoriaPasta]++;
                else
                    resultado.ArquivosPorCategoria[categoriaPasta] = 1;



                var caminhoDiretorio = Path.Combine(diretorioFonte, categoriaPasta);
                var caminhoDestino = Path.Combine(caminhoDiretorio, _arquivosOperacoes.ObterArquivoNome(arquivo));

                _arquivosOperacoes.MoverArquivo(arquivo, caminhoDestino);
                Console.WriteLine($"Arquivo {arquivo} movido para {caminhoDestino}");
            }
            catch (Exception ex)
            {
                resultado.ArquivosIgnorados++;
                resultado.Erros.Add($"Erro ao mover arquivo {arquivo}: {ex.Message}");
                Console.WriteLine($"Erro ao mover arquivo {arquivo}: {ex.Message}");
            }
        }


        await Task.CompletedTask;

        return resultado;
    }
}
