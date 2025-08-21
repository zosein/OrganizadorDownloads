using OrganizadorDownloads.Infra.Interfaces;
using OrganizadorDownloads.Core.Interfaces;
using OrganizadorDownloads.Core.Models;

namespace OrganizadorDownloads.Core.Strategies
{
    public class OrganizacaoPorDataStrategy : IOrganizacao
    {
        private readonly IArquivosOperacoes _arquivosOperacoes;

        public string Nome => "data";

        public OrganizacaoPorDataStrategy(IArquivosOperacoes arquivosOperacoes)
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

                    var dataModificacao = _arquivosOperacoes.ObterDataModificacao(arquivo);
                    var dataPasta = $"{dataModificacao:yyyy-MM-dd}";

                    resultado.ArquivosMovidos++;
                    resultado.TamanhoTotalMovido += new System.IO.FileInfo(arquivo).Length;

                    if (resultado.ArquivosPorCategoria.ContainsKey(dataPasta))
                        resultado.ArquivosPorCategoria[dataPasta]++;
                    else
                        resultado.ArquivosPorCategoria[dataPasta] = 1;

                    var caminhoDiretorio = Path.Combine(diretorioFonte, dataPasta);
                    var caminhoDestino = Path.Combine(caminhoDiretorio, _arquivosOperacoes.ObterArquivoNome(arquivo));

                    _arquivosOperacoes.MoverArquivo(arquivo, caminhoDestino);
                    Console.WriteLine($"Arquivo {arquivo} movido para ->{caminhoDestino}");

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
}