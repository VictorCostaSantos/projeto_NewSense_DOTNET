using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace E_comerceAPI.Src.Modelos
{
    [Table("tb_produtos")]
    public class Produtos
    {
        #region Atributos

        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Produto { get; set; }
        public string Descricao { get; set; }
        public int QtdProduto { get; set; }
        public int QtdLimite { get; set; }
        public string URL_Imagem { get; set; }

        [JsonIgnore, InverseProperty("Produto")]
        public List<Acao> MeusProdutos { get; set; }

        #endregion
    }
}