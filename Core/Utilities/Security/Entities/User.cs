using Core.Entities;
using Core.Utilities.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concretes
{
    public class User : BaseEntity<int>
    {
       
        public string  UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string NationalIdentity { get; set; }

        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }
        public User()
        {
            UserOperationClaims = new HashSet<UserOperationClaim>();
        }

        public User(int id, string userName, string firstName, string lastName, DateTime dateOfBirth, string nationalIdentity, string email, byte[] passwordHash, byte[] passwordSalt, ICollection<UserOperationClaim> userOperationClaims)
        {
            Id = id;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            NationalIdentity = nationalIdentity;
            Email = email;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            UserOperationClaims = userOperationClaims;
        }

        
    }
}
