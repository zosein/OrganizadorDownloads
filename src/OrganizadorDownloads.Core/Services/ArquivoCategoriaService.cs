using OrganizadorDownloads.Core.Models;

namespace OrganizadorDownloads.Core.Services;

public static class ArquivoCategoriaService
{
    private static readonly Dictionary<string, ArquivoCategoria> CategoriasExtensao = new()

    {
        //Documentos
        { ".pdf", ArquivoCategoria.Documentos },
        { ".doc", ArquivoCategoria.Documentos },
        { ".docx", ArquivoCategoria.Documentos },
        { ".txt", ArquivoCategoria.Documentos },
        { ".ppt", ArquivoCategoria.Documentos },
        { ".pptx", ArquivoCategoria.Documentos },
        { ".xls", ArquivoCategoria.Documentos },
        { ".xlsx", ArquivoCategoria.Documentos },
        { ".odt", ArquivoCategoria.Documentos },
        { ".rtf", ArquivoCategoria.Documentos },

        //Imagens
        { ".jpg", ArquivoCategoria.Imagens },
        { ".jpeg", ArquivoCategoria.Imagens },
        { ".png", ArquivoCategoria.Imagens },
        { ".gif", ArquivoCategoria.Imagens },
        { ".bmp", ArquivoCategoria.Imagens },
        { ".tiff", ArquivoCategoria.Imagens },
        { ".svg", ArquivoCategoria.Imagens },
        { ".webp", ArquivoCategoria.Imagens },

        //Arquivos
        { ".zip", ArquivoCategoria.Arquivos },
        { ".rar", ArquivoCategoria.Arquivos },
        { ".7z", ArquivoCategoria.Arquivos },
        { ".tar", ArquivoCategoria.Arquivos },
        { ".gz", ArquivoCategoria.Arquivos },
        { ".bz2", ArquivoCategoria.Arquivos },

        //Videos
        { ".mp4", ArquivoCategoria.Videos },
        { ".avi", ArquivoCategoria.Videos },
        { ".mkv", ArquivoCategoria.Videos },
        { ".mov", ArquivoCategoria.Videos },
        { ".wmv", ArquivoCategoria.Videos },
        { ".flv", ArquivoCategoria.Videos },
        { ".webm", ArquivoCategoria.Videos },

        //Audio
        { ".mp3", ArquivoCategoria.Audios },
        { ".wav", ArquivoCategoria.Audios },
        { ".aac", ArquivoCategoria.Audios },
        { ".flac", ArquivoCategoria.Audios },
        { ".ogg", ArquivoCategoria.Audios },
        { ".wma", ArquivoCategoria.Audios },

        //Executavéis
        { ".exe", ArquivoCategoria.Executaveis },
        { ".msi", ArquivoCategoria.Executaveis },
        { ".app", ArquivoCategoria.Executaveis },
        { ".bat", ArquivoCategoria.Executaveis },
        { ".cmd", ArquivoCategoria.Executaveis },
        { ".sh", ArquivoCategoria.Executaveis },

    };

    public static ArquivoCategoria ObterCategoriaPorExtensao(string extensao)
    {
        return CategoriasExtensao.TryGetValue(extensao, out var categoria)
            ? categoria
            : ArquivoCategoria.Outros;
    }

    public static string ObterCategoriaPastaNome(ArquivoCategoria categoria)
    {
        return categoria switch
        {
            ArquivoCategoria.Documentos => "Documentos",
            ArquivoCategoria.Imagens => "Imagens",
            ArquivoCategoria.Arquivos => "Arquivos",
            ArquivoCategoria.Videos => "Videos",
            ArquivoCategoria.Audios => "Audios",
            ArquivoCategoria.Executaveis => "Executaveis",
            _ => "Outros"
        };
    }
}
