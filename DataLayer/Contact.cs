using System;

namespace DataLayer
{
    public class Contact
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Company { get; set; }

        public int AddressId { get; set; }

        /// <returns></returns>
        public override string ToString()
        {
            return $"Id: {Id}, FirstName: {FirstName}, LastName: {LastName}, Email: {Email}, Company: {Company}";
        }
    }
}
