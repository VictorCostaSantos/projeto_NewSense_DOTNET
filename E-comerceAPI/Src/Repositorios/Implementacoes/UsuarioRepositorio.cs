using E_comerceAPI.Src.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_comerceAPI.Src.Repositorios.Implementacoes
{
    /// <summary>
    /// <para>Resumo: Classe responsavel por implementar IUsuario</para>
    /// <para>Criado por: Grupo 4</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 22/08/2022</para>
    /// </summary>
    public class UsuarioRepositorio : IUsuario
    {
        #region Atributos

        private readonly EcomerceContexto _contextos;

        #endregion Atributos

        #region Construtores
        public UsuarioRepositorio(EcomerceContexto contextos)
        {
            _contextos = contextos;
        }
        public async Task<List<Usuario>> PegarTodosUsuariosAsync()
        {
            return await _contextos.Usuarios.ToListAsync();
        }
        #endregion

        #region Métodos

        /// <summary>
        /// <para>Resumo: Método assíncrono pegar usuário pelo Id</para>
        /// </summary>
        /// <param> 'name="idUsuario">Id do usuário</param>
        /// <return>Usuario pelo Id</return>
        /// <exception cref="Exception">Id do usuario não pode ser nulo</exception>
        public async Task<Usuario> PegarUsuarioPeloIdAsync(int id)
        {
            if (!ExisteId(id)) throw new Exception("Id do usuario não encontrado");
            return await _contextos.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

            // funções auxiliares
            bool ExisteId(int id)
            {
                var auxiliar = _contextos.Usuarios.FirstOrDefault(u => u.Id == id);
                return auxiliar != null;
            }
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para salvar novo usário</para>
        /// </summary>
        /// <param> 'name="usuarios">Construtor para cadastrar usuário</param>
        /// <exception cref="Exception">Usuário já existe</exception>
        /// <exception cref="Exception">Email já existe</exception>
        public async Task NovoUsuarioAsync(Usuario usuarios)
        {
            if (await ExisteUsuario(usuarios.Nome)) throw new Exception("Usuário já existente no sistema!");
            if (await ExisteEmail(usuarios.Email)) throw new Exception("Email já existente no sistema!");

            await _contextos.Usuarios.AddAsync(new Usuario
            {
                Nome = usuarios.Nome,
                Senha = usuarios.Senha,
                Email = usuarios.Email,
                Endereco = usuarios.Endereco,
                Documento = usuarios.Documento,
                Tipo = usuarios.Tipo,
                Condicao = usuarios.Condicao
            });
            await _contextos.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar um usuario pelo email</para>
        /// </summary>
        /// <param name="email">Email do usuario</param>
        /// <return>Usuario</return>
        public async Task<Usuario> PegarUsuarioPeloEmailAsync(string email)
        {
            return await _contextos.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para atualizar usuário</para>
        /// </summary>
        /// <param> 'name="usuarios">Construtor para atualizar usuário</param>
        /// <exception cref="Exception">Usuário já existe</exception>
        /// <exception cref="Exception">Email já existe</exception>
        public async Task AtualizarUsuarioAsync(Usuario usuarios)
        {
            if (await ExisteUsuario(usuarios.Nome)) throw new Exception("Usuário já existente no sistema!");
            if (await ExisteEmail(usuarios.Email)) throw new Exception("Email já existente no sistema!");

            var auxiliar = await PegarUsuarioPeloIdAsync(usuarios.Id);
            auxiliar.Nome = usuarios.Nome;
            auxiliar.Email = usuarios.Email;
            auxiliar.Senha = CodificarSenha(usuarios.Senha);
            auxiliar.Endereco = usuarios.Endereco;
            auxiliar.Documento = usuarios.Documento;
            auxiliar.Condicao = usuarios.Condicao;
            _contextos.Usuarios.Update(auxiliar);
            await _contextos.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para deletar um usuário</para>
        /// </summary>
        /// <param name="id">Id do usuário</param>
        public async Task DeletarUsuarioAsync(int id)
        {
            _contextos.Usuarios.Remove(await PegarUsuarioPeloIdAsync(id));
            await _contextos.SaveChangesAsync();
        }

        // Funções auxiliares
        private async Task<bool> ExisteUsuario(string nome)
        {
            var auxiliar = await _contextos.Usuarios.FirstOrDefaultAsync(u => u.Nome == nome);

            return auxiliar != null;
        }

        private async Task<bool> ExisteEmail(string email)
        {
            var auxiliar = await _contextos.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

            return auxiliar != null;
        }
        //chamando codificar senha para atualizar o usuário
        public string CodificarSenha(string senha)
        {
            var bytes = Encoding.UTF8.GetBytes(senha);
            return Convert.ToBase64String(bytes);
        }
    }
    

    #endregion

}