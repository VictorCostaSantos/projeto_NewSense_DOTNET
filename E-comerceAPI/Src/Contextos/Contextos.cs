using E_comerceAPI.Src.Modelos;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// <para>Resumo: Classe contexto, responsavel por carregar contexto e definirDbSets </ para >
/// <para>Criado por: Grupo4</para>
/// <para>Versão: 1.0</para>
/// <para>Data: 09/08/2022</para>
/// </summary>
public class EcomerceContexto : DbContext
{
    #region Atributos

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Acao> Acoes { get; set; }
    public DbSet<Produto> Produtos { get; set; }

    #endregion

    #region Construtores

    public EcomerceContexto(DbContextOptions<EcomerceContexto> opt) :base(opt)
    {

    }

    #endregion

}