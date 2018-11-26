using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetAll();

        Task<Contact> Find( int id );

        Task<Contact> Add( Contact contact );

        Task<Contact> Update( Contact contact );

        Task Remove( int id );
    }
}
