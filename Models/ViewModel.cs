using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace spaceship.Models
{
    public class ViewModel
    {
        public User regUser {get; set;}
        public UserLog loginUser {get; set;}
        public EditUser editUser {get; set;}
        public Post Post {get; set;}
        public Comment Comment  { get; set;}
        public Post_Like Post_Like { get; set;}
        public Comment_Like Comment_Like { get; set;}
        public List<Post_Like> allPost_Likes { get; set;}
        public List<string> errors {get;set;}
        public List<Comment_Like> allComment_Likes { get; set;}
        public List<User> allUsers {get; set;}
        public List<Post> allPosts {get; set;}
        public List<Comment> allComments {get; set;}
    }
}