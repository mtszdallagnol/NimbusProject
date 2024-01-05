using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nimbus.Api.Dtos.NimbusBox;

namespace Nimbus.Api.Services
{
    public class NimbusUserUnionService : IBaseCrudService
    {
        private readonly DataContext _context;

        public NimbusUserUnionService(DataContext context)
        {
            _context = context;
        }

        public async Task<object> addRegister<T>(T newSave)
        {
            throw new NotImplementedException();
        }

        public async Task<object> deleteRegister<T>(T _id)
        {
            throw new NotImplementedException();
        }

        public async Task<object> getAllRegisters()
        {
            return await _context.tbNimbusUserData.ToListAsync();
        }

        public async Task<object> getById<T>(T _id)
        {
            throw new NotImplementedException();
        }
    }
}