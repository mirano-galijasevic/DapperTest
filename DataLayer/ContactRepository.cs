using System;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using Dapper;

using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ContactRepository : IContactRepository
    {
        /// <summary>
        /// Connection string
        /// </summary>
        private readonly string _connection;

        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="connectionString"></param>
        public ContactRepository( string connectionString )
        {
            _connection = connectionString;
        }

        /// <summary>
        /// Get all contacts
        /// </summary>
        /// <returns></returns>
        public async Task<List<Contact>> GetAll()
        {
            string sql = "SELECT * FROM dbo.Contacts";

            using ( var connection = new SqlConnection( _connection ) )
            {
                var contacts = await connection.QueryAsync<Contact>( sql );
                return contacts.ToList<Contact>();
            }
        }

        /// <summary>
        /// Find one
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Contact> Find( int id )
        {
            string sql = $"SELECT * FROM dbo.Contacts WHERE Id = @id";

            using ( var connection = new SqlConnection( _connection ) )
            {
                var contact = await connection.QueryAsync<Contact>( sql, new { @id = id } );
                return contact.FirstOrDefault();
            }
        }

        /// <summary>
        /// Add one
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public async Task<Contact> Add( Contact newContact )
        {
            string sql = "INSERT INTO Contacts(FirstName, LastName, Email, Company) VALUES (@FirstName, @LastName, @Email, @Company) ";

            using ( var connection = new SqlConnection( _connection ) )
            {
                int recAffected = await connection.ExecuteAsync( sql, newContact );

                if ( recAffected == 1 )
                {
                    var contact = await
                        connection.QueryAsync<Contact>( "SELECT * FROM Contacts WHERE Email = @email", new { @email = newContact.Email } );

                    return contact.FirstOrDefault();
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public async Task<Contact> Update( Contact contact )
        {
            string sql = "UPDATE Contacts SET FirstName = @firstName WHERE Id = @id";

            using ( var connection = new SqlConnection( _connection ) )
            {
                int recAffected = await connection.ExecuteAsync( sql, new { @id = contact.Id, @firstName = contact.FirstName } );

                if ( recAffected == 1 )
                {
                    var updatedContact = await
                        connection.QueryAsync<Contact>( "SELECT * FROM Contacts WHERE Id = @id", new { @id = contact.Id } );

                    return updatedContact.FirstOrDefault();
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="id"></param>
        public async Task Remove( int id )
        {
            string sql = "Contacts_Del";

            using ( var connection = new SqlConnection( _connection ) )
            {
                int recAffected = await connection.ExecuteAsync( sql, new { @contactId = id }, commandType: CommandType.StoredProcedure );
            }
        }
    }
}
