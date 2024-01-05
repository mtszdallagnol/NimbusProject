using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nimbus.Api.Dtos.UserNimbus;

namespace Nimbus.Api.Services
{
    public class UserNimbusService : IBaseCrudService
    {
        private readonly DataContext _context;

        public UserNimbusService(DataContext context)
        {
            _context = context;
        }

        public async Task<object> addRegister<T>(T newSave)
        {
            postDTOuser? user = newSave as postDTOuser;
            StateResultHub webSocket = new();

            if (user is null) throw new Exception("O usuário não pode ser nulo");

            UserNimbus nimbusUser = new()
            {
                nmNomeUsuario = user.nmNomeUsuario,
                nmNomeEmailUsuario = user.nmNomeEmailUsuario,
                nrTelefone = user.nrTelefone,
                nmSenha = user.nmSenha,
                dtAlteracao = DateTime.Now,
                dtCriacao = DateTime.Now,
            };

            var userINST = await _context.tbAuthUserData.SingleOrDefaultAsync(x => x.nmNomeUsuario == user.nmNomeUsuario || x.nmNomeEmailUsuario == user.nmNomeEmailUsuario);
            if (userINST != null) { throw new Exception("Cadastro com mesmo nome e/ou email já existente"); }

            _context.tbAuthUserData.Add(nimbusUser);

            await _context.SaveChangesAsync();

            return nimbusUser;
        }

        public async Task<object> deleteRegister<T>(T _id)
        {
            var rmvUser = await _context.tbAuthUserData.SingleOrDefaultAsync(x => x.idUsuario == Convert.ToInt32(_id))
                ?? throw new Exception("O usuário não existe");

            _context.tbAuthUserData.Remove(rmvUser);
            await _context.SaveChangesAsync();

            return await _context.tbAuthUserData.ToListAsync();
        }

        public async Task<object> getAllRegisters()
        {
            return await _context.tbAuthUserData.ToListAsync();
        }

        public async Task<object> getById<T>(T _id)
        {
            return await _context.tbAuthUserData.SingleOrDefaultAsync(x => x.idUsuario == Convert.ToInt32(_id))
                ?? throw new Exception("O usuário não existe");
        }

        public async Task<UserNimbus> getUserByPasswordAndEmail(UserNimbus user)
        {
            return await _context.tbAuthUserData.SingleOrDefaultAsync(x => x.nmNomeEmailUsuario == user.nmNomeEmailUsuario && x.nmSenha == user.nmSenha)
                ?? throw new Exception("Login ou senha inválidos");
        }
    }
}