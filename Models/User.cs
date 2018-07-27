using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace spaceship.Models{
    public class User
    {
        [Key]
        public int Userid {get;set;}

        [Required]
        [MinLength(2),MaxLength(45)]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use letters only")]
        public string Username { get; set; }

        [Column("image")]
        [MaxLength(255)]
        public string Image { get; set; }

        public List<Post> Posts {get; set;}
        public List<Comment> Comments {get; set;}

        public DateTime Created_At {get; set;} = DateTime.Now;
        public int Highscore {get;set;}

        
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8),MaxLength(45)]
        public string Password { get; set; }

        [NotMapped]
        [CompareAttribute("Password")]
        public string ConfirmPassword { get; set; }

        public User()
        {
            Created_At = DateTime.Now;
            Posts = new List<Post>();
            Comments = new List<Comment>();
            Highscore = 0;
        }
    }
        public class UserLog
        {

        [Required(ErrorMessage = "Email for login doesn't exist")]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use letters only")]
        [Column("username")]
        public string LogUsername{ get; set; }
                
        [Required(ErrorMessage = "The password doesn't match the account.")]
        [DataType(DataType.Password)]
        [MinLength(8)]
        [Column("password")]
        public string LogPassword { get; set; }
}

        public class EditUser
            {
                
                [MinLength(2),MaxLength(45)]
                [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use letters only")]
                public string Username { get; set; }

                [Required]
                [DataType(DataType.Password)]
                [MinLength(8),MaxLength(45)]
                [Column("password")]
                public string editPassword { get; set; }
                [Column("image")]
                [MaxLength(255)]
                public string editImage { get; set; }

                [NotMapped]
                [CompareAttribute("editPassword")]
                public string editConfirmPassword { get; set; }

            }

    }

    
