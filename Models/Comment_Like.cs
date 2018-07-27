using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace spaceship.Models{
    public class Comment_Like
    {
        [Key]
        public int Commentlikeid {get;set;}
        
        public int Userid {get; set;}
        public User Liked_Comment {get; set;}

        public int Commentid {get; set;}
        public Comment Has_Comment_Likes { get; set; }
    
    }
}