using System.Text.Json.Serialization;

namespace E_comerceAPI.Src.Utilidades
{
    
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

}
