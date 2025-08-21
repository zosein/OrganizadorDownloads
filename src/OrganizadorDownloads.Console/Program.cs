using System.CommandLine;
using System.CommandLine.Parsing;
using OrganizadorDownloads.Core.Models;
using OrganizadorDownloads.Core.Services;
using OrganizadorDownloads.Core.Strategies;
using OrganizadorDownloads.Infra.Services;
using OrganizadorDownloads.Core.Interfaces;



var arquivosOperacoes = new ArquivosOperacoes();
var estrategias = new IOrganizacao[]
{
    new OrganizarPorExtensaoStrategy(arquivosOperacoes),
    new OrganizacaoPorDataStrategy(arquivosOperacoes)
};

var organizacaoService = new OrganizacaoService(estrategias);

var rootCommand = new RootCommand("Organizador de Downloads - organizando sua pasta de downloads");

var organizarCommand = new Command("organize", "Organiza os arquivos na pasta de downloads");

var directoryOption = new Option<string>("--directory", "Diretório a ser organizado (default: pasta de Downloads do usuário)");
directoryOption.SetDefaultValue(GetDefaultDownloadsPath());

var estrategiaOpcao = new Option<string>("--by", "Estratégia de organização a ser utilizada (data ou extensão)")
{
    IsRequired = false
};

directoryOption.SetDefaultValue(GetDefaultDownloadsPath());
estrategiaOpcao.SetDefaultValue("extension");

organizarCommand.Add(directoryOption);
organizarCommand.Add(estrategiaOpcao);

organizarCommand.SetHandler(async (directory, strategy) =>
{
    Console.WriteLine($"Iniciando organização do diretório: {directory}");
    Console.WriteLine($"Estratégia selecionada: {strategy}");
    Console.WriteLine("-----");

    var estrategiaSelecionada = organizacaoService.ObterEstrategia(strategy);

    if (estrategiaSelecionada == null)
    {
        Console.WriteLine($"Estratégia '{strategy}' não encontrada.");
        Console.WriteLine($"Estratégias disponíveis: {string.Join(", ", organizacaoService)}");
        return;
    }

    try
    {
        var resultado = await estrategiaSelecionada.OrganizarAsync(directory);

        Console.WriteLine("-----");
        Console.WriteLine("Organização concluída com sucesso.");
        Console.WriteLine($"Arquivos movidos: {resultado.ArquivosMovidos}");
        Console.WriteLine($"Tamanho total movido: {resultado.TamanhoTotalMovido} bytes");
        Console.WriteLine($"Arquivos ignorados: {resultado.ArquivosIgnorados}");
        Console.WriteLine("Erros:");

        foreach (var erro in resultado.Erros)
        {
            Console.WriteLine($" - {erro}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao organizar: {ex.Message}");
    }
}, directoryOption, estrategiaOpcao);

rootCommand.AddCommand(organizarCommand);

return await rootCommand.InvokeAsync(args);


static string GetDefaultDownloadsPath()
{
    return Environment.OSVersion.Platform switch
    {
        PlatformID.Win32NT => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads"),
        PlatformID.Unix => Path.Combine(Environment.GetEnvironmentVariable("HOME") ?? "/home", "Downloads"),
        _ => throw new NotSupportedException("Plataforma não suportada")
    };
}