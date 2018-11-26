using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IAddressRepository
    {
        Task<dynamic> GetAll();

        Task<List<Addresses>> GetAddressesAndStates();
    }
}
