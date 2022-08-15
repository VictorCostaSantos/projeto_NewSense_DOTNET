using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using E_comerceAPI.Src.Utilidades;

namespace E_comerceAPI.Src.Modelos
{
    [Table("tb_acao")]
    public class Acao
    {
        #region Atributos

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        public DateTime DataAcao { get; set; }
        public string QtdAcao { get; set; }
        public StatusAcao Status { get; set; }

        [ForeignKey("FK_Usuario")]
        public Usuario Ong { get; set; }

        [ForeignKey("FK_Produto")]
        public Produto Produto { get; set; }

        #endregion
    }
}