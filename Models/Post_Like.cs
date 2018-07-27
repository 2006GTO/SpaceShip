using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace spaceship.Models{
    public class Post_Like
    {
        [Key]
        public int Postlikeid {get;set;}
        
        public int Userid {get; set;}
        public User Liked_Post {get; set;}

        public int Postid {get; set;}
        public Post Has_Post_Likes { get; set; }
    
    }
}