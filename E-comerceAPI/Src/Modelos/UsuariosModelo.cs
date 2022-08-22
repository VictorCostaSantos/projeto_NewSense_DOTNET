using E_comerceAPI.Src.Utilidades;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

/// <summary>
/// <para>Resumo: Classe responsavel por representar tb_usuarios no banco.</para >
/// <para>Criado por: Grupo4</para>
/// <para>Versão: 1.0</para>
/// <para>Data: 22/08/2022</para>
/// </summary>
/// 
namespace E_comerceAPI.Src.Modelos
{
    [Table("tb_usuarios")]
    public class Usuario
    {
        #region Atributos

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
        public string Documento { get; set; }
        [Required]
        public TipoUsuario Tipo { get; set; }

        public CondicaoUsuario Condicao { get; set; }

        [JsonIgnore, InverseProperty("Ong")]
        public List<Acao> MinhasDoacoes { get; set; }

        [JsonIgnore, InverseProperty("Criador")]
        public List<Produto> MeusProdutos { get; set; }

        #endregion
    }
}