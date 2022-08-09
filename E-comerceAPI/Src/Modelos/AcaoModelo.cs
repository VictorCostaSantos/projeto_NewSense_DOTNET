using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_comerceAPI.Src.Modelos
{
    [Table("tb_acao")]
    public class Acao
    {
        #region Atributos

        public int Id { get; set; } 
        public DateTime DataAcao { get; set; }
        public string QtdAcao { get; set; }
        public string TipoAcao { get; set; }

        [ForeignKey("FK_Usuario")]
        public Usuarios Usuario { get; set; }
        [ForeignKey("FK_Produto")]
        public Produtos Produto { get; set; }

        #endregion
    }
}