using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace spaceship.Models{

    public class Comment
    {
        [Key]
        public int Commentid {get;set;}

        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        public string Text {get;set;}

        public int Userid { get; set; }
        public User Commenter {get; set;} 
        public int Postid { get; set; }
        public Post Smack {get; set;}

        public List<Comment_Like> Comment_Likes {get; set;}

        public DateTime Created_At {get; set;} = DateTime.Now;
        
        [NotMapped]
        public string Ago {get; set;}

        public Comment()
        {
            List<Comment_Like> Comment_Likes = new List<Comment_Like>();
        }
    }
}