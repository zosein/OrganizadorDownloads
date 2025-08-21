using OrganizadorDownloads.Core.Interfaces;

namespace OrganizadorDownloads.Core.Services;

public class OrganizacaoService
{
    private readonly IEnumerable<IOrganizacao> _estrategias;

    public OrganizacaoService(IEnumerable<IOrganizacao> estrategias)
    {
        _estrategias = estrategias;
    }

    public IOrganizacao? ObterEstrategia(string nomeEstrategia)
    {
        return _estrategias.FirstOrDefault(e => string.Equals(e.Nome, nomeEstrategia, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<string> ObterEstrategiasAcessiveis()
    {
        return _estrategias.Select(e => e.Nome ?? string.Empty).Where(nome => !string.IsNullOrEmpty(nome)).Distinct();
    }


}
