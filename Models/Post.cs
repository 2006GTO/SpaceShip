using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace spaceship.Models{

    public class Post
    {
        [Key]
        public int Postid {get;set;}

        [Required(ErrorMessage = "Message must be between 2 and 255 characters.")]
        [MinLength(2)]
        [MaxLength(255)]
        public string Message {get;set;}

        public int Userid { get; set; }
        public User Poster {get; set;} 

        public List<Post_Like> Post_Likes {get; set;}
        public List<Comment> Comments {get; set;}
        
        public DateTime Created_At {get; set;} = DateTime.Now;
        
        [NotMapped]
        public string Ago {get; set;}

        public Post()
        {
            List<Post_Like> Post_Likes = new List<Post_Like>();
            List<Comment> Comments = new List<Comment>();
        }
    }
}