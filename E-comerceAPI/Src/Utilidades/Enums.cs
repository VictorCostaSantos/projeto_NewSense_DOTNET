using System.Text.Json.Serialization;

namespace E_comerceAPI.Src.Utilidades
{
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CondicaoUsuario
    {
        EmpresaDoadora,
        PessoaDoadora,
        ONG
    }


}
