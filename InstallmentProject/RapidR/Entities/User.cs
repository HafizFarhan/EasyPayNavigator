using System.ComponentModel.DataAnnotations;

namespace RapidR.Entities
{
    public class User
    {
       
      
        public string FirstName { get; set; }
      
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string HashedPassword { get; set; }
    
        [Phone]
        public string Phone { get; set; }

    }
}
