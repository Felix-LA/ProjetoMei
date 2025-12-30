using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoMei.Data;
using ProjetoMei.DataModel;
using ProjetoMei.Interfaces;
using ProjetoMei.Model;
using System;
using System.Linq;

namespace ProjetoMei.Repository
{
    public class UsuarioMeiRepository : IUsuarioMeiRepository
    {
        private readonly MeiDbContext _meiDbContext;

        public UsuarioMeiRepository(MeiDbContext meiDbContext)
        {
            _meiDbContext = meiDbContext;
        }

        public UsuarioMeiModel Adicionar(UsuarioMeiDataModel usuarioMeiDataModel)
        {
            UsuarioMeiModel usuarioMeiModel = new UsuarioMeiModel();

            usuarioMeiModel.Nome = usuarioMeiDataModel.Nome;
            usuarioMeiModel.Id = usuarioMeiDataModel.Id;

            _meiDbContext.Meis.Add(usuarioMeiModel);
            _meiDbContext.SaveChanges();

            return usuarioMeiModel;

        }

        public List<UsuarioMeiModel> BuscarUsuarios()
        {
            var usuarios = _meiDbContext.Meis.ToList();

            return usuarios;
        }

        public UsuarioMeiModel BuscarUsuarioPorId(int Id)
        {
            var usuario = _meiDbContext.Meis.Find(Id);
            
            if (usuario == null)
            {
                throw new Exception("Usuário Não Encontrado");
            }

            return usuario;
        }


        public UsuarioMeiModel Atualizar(UsuarioMeiDataModel usuarioMeiDataModel, int Id)
        {
            throw new NotImplementedException();
        }

        /*
        public UsuarioMeiModel BuscarUsuarioPorNome(UsuarioMeiDataModel usuarioMeiDataModel, string Nome)
        {
            var usuario = _meiDbContext.Meis.Where(nome => nome.Nome.Contains(Nome)).ToList();

            if (usuario == null)
            {
                throw new Exception("Usuário Não Encontrado");
            }

            return usuario;
        }
        
        */
        

        public bool Deletar(int Id)
        {
            var deletar = new UsuarioMeiModel { Id = Id };

            if (deletar.Id <= 0)
            {
                throw new Exception("Usuário não Encontrado");
            }
            else if (deletar == null)
            {
                throw new Exception("Usuário não Encontrado");
            }

            _meiDbContext.Meis.Remove(deletar);
            _meiDbContext.SaveChanges();

            return true;
        }

    }
}
