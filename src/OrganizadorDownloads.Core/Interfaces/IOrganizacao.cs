using OrganizadorDownloads.Core.Models;

namespace OrganizadorDownloads.Core.Interfaces;

public interface IOrganizacao
{
    string? Nome { get; }
    Task<OrganizacaoResultado> OrganizarAsync(string diretorioFonte, CancellationToken token = default);
}
