using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class User
    {
        [Key]
        public int ID { get;private set; }
        
        [Required,MaxLength(20)]  
        public string FirstName { get; set; }
        
        [Required, MaxLength(20)]
        public string Surname { get; set; }
        
        [Required,Range(18,81)]
        public byte Age { get; set; }
        
        [Required,MaxLength(20)]
        public string Username { get; set; }
        
        [Required, MaxLength(70)]
        public string Password { get; set; }
        
        [Required, MaxLength(20)]
        public string Email { get; set; }

        public IEnumerable<User> Friends { get; set; }

        public IEnumerable<Interest> Interests { get; set; }

        private User()
        {

        }
        public User(string firstName, string surname, byte age, string username, string password, string email)
        {
            FirstName = firstName;
            Surname = surname;
            Age = age;
            Username = username;
            Password = password;
            Email = email;

        }

        

    }
}
