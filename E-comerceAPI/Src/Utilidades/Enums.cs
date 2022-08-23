using System.Text.Json.Serialization;

namespace E_comerceAPI.Src.Utilidades
{
    /// <summary>
    /// <para>Resumo: Classe responsavel por enumerar as possiveis condições da tipo de usuario|Condicao de usuario|Status do usuario|</para>
    /// <para>Criado por: Generation</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 23/08/2022</para>
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CondicaoUsuario
    {
        DOADOR,
        ONG
    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatusAcao
    {
        Pendente,
        Concluido,
        Cancelado
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TipoUsuario
    {
        NORMAL,
        ADMINISTRADOR
    }


}
