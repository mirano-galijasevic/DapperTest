using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DataLayer
{
    public class AddressRepository : IAddressRepository
    {
        /// <summary>
        /// Connection
        /// </summary>
        private string _connection;

        /// <summary>
        /// Connection
        /// </summary>
        /// <param name="connectionString"></param>
        public AddressRepository( string connectionString )
        {
            _connection = connectionString;
        }

        /// <summary>
        /// Get all addresses
        /// </summary>
        /// <returns></returns>
        public async Task<dynamic> GetAll()
        {
            string sql = "SELECT * FROM Addresses";

            using ( var connection = new SqlConnection( _connection ) )
            {
                var contacts = await connection.QueryAsync<dynamic>( sql );
                return contacts;
            }
        }

        /// <summary>
        /// Get addresses and states
        /// </summary>
        /// <returns></returns>
        public async Task<List<Addresses>> GetAddressesAndStates()
        {
            string sql = "select a.*, s.StateName " +
                "from Addresses a " +
                "inner join States s on a.StateId = s.StateId";

            using ( var connection = new SqlConnection( _connection ) )
            {
                var addressesAndStates = await connection.QueryAsync<Addresses, States, Addresses>(
                        sql,
                        ( address, state ) =>
                        {
                            address.State = state;
                            return address;
                        },
                        splitOn: "StateName"
                        );

                return addressesAndStates.AsList();
            }
        }
    }
}
