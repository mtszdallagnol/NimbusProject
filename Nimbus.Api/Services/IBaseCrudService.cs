using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nimbus.Api
{
    public interface IBaseCrudService
    {
        Task<object> getAllRegisters();
        Task<object> getById<T>(T _id);
        Task<object> addRegister<T>(T newSave);
        Task<object> deleteRegister<T>(T _id);
    }
}