using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace E_comerceAPI.Src.Modelos
{
    /// <summary>
    /// <para> Resumo: Classe responsavel por representar tb_produtos no banco. </para>
    /// <para>Criado por: Grupo 4</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 22/08/2022</para>
    /// </summary>
    
    [Table("tb_produtos")]
    public class Produto
    {
        #region Atributos

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int QtdProduto { get; set; }
        public int QtdLimite { get; set; }
        public string URL_Imagem { get; set; }

        [JsonIgnore, InverseProperty("Produto")]
        public List<Acao> Acoes { get; set; }

        [ForeignKey("fk_usuario")]
        public Usuario Criador { get; set; }

        #endregion
    }
}