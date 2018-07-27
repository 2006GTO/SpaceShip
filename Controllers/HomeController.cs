using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Threading;
using spaceship.Models;
using System.Web;
using Newtonsoft.Json;




namespace spaceship.Controllers
{
    public class HomeController : Controller
    {

    private YourContext _context;
    public HomeController(YourContext context)
    {
        _context = context;
    }

        [Route("")]
        public IActionResult Index()
        {
            ViewModel logreg = new ViewModel()
            {
                regUser = new User(),
                loginUser = new UserLog()
            };
            return View(logreg);
        }

        [Route("/Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        [Route("/Login")]
        [HttpPost]
        public IActionResult Login(ViewModel FormData)
        {
         UserLog loguser = FormData.loginUser;
            if(ModelState.IsValid){
            var user = _context.users.SingleOrDefault(u => u.Username == loguser.LogUsername);
            if(user != null && user.Password != null)
            {
                var Hasher = new PasswordHasher<User>();
                if(0 != Hasher.VerifyHashedPassword(user, user.Password, loguser.LogPassword))
                {
                    ViewBag.num = (int)user.Userid;
                    _context.SaveChanges();
                    HttpContext.Session.SetString("name", user.Username);
                    System.Console.WriteLine("***Logging in a User***");
                    HttpContext.Session.SetInt32("id", (int)ViewBag.num);
                    return RedirectToAction("Game");}
            }
            ModelState.AddModelError("LogPassword", "Incorrect password.");
            ViewBag.error = "Error occured during login, please try again.";
            ViewBag.img = "display:none;";
            return View("Index");
            }
            ModelState.AddModelError("LogEmail", "Incorrect email.");
            ViewBag.error = "Error occured during login, please try again.";
            ViewBag.img = "display:none;";
            return View("Index");
        }

        [Route("/Register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }

        [Route("/Register")]
        [HttpPost]
        public IActionResult Register(ViewModel FormData)
        {
            User user = FormData.regUser;
            if(ModelState.IsValid){
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            user.Password = Hasher.HashPassword(user, user.Password);
            _context.Add(user);
            _context.SaveChanges();
            System.Console.WriteLine("***Created a User***");
            HttpContext.Session.SetInt32("id", user.Userid);
            HttpContext.Session.SetString("name", user.Username);
            return RedirectToAction("Game");
            }
            ViewBag.img = "display:none;";
            ViewBag.error = "Error during registration, please try again.";
            System.Console.WriteLine("User creation rejected*******");
            return View("Index");
        }

        [Route("/Game")]
        public IActionResult Game()
        {
            ViewBag.id = HttpContext.Session.GetInt32("id");

            List<Post> AllPosts = _context.posts.Include(u => u.Poster).Include(c => c.Comments).ThenInclude(i => i.Comment_Likes).Include(p => p.Post_Likes).ThenInclude(g => g.Liked_Post).ToList();
            foreach(var i in AllPosts){
              i.Ago = i.Created_At.TimeAgo();
              foreach(var c in i.Comments){
                  c.Ago = c.Created_At.TimeAgo();
              }
            }
            ViewBag.posts = AllPosts.OrderByDescending(x => x.Post_Likes.Count).Take(5);

            return View();
        }

        [Route("/SmackWall")]
        public IActionResult Wall(ViewModel FormData)
        {
            if(HttpContext.Session.GetInt32("id") == null) { 
                return RedirectToAction("Logout");}

            return View();
        }

        [HttpGet]
        [Route("/Smack")]
        
        public IActionResult Smack()
        {
            // if(HttpContext.Session.GetInt32("id") == null) { 
            //     return RedirectToAction("Logout");}
            // JavaScriptSerializer TheSerializer = new JavaScriptSerializer();

            ViewBag.name = HttpContext.Session.GetString("name");
            ViewBag.id = HttpContext.Session.GetInt32("id");
            List<Post> AllPosts = _context.posts.Include(u => u.Poster).Include(c => c.Comments).ThenInclude(i => i.Comment_Likes).Include(p => p.Post_Likes).ThenInclude(g => g.Liked_Post).ToList();
            foreach(var i in AllPosts){
              i.Ago = i.Created_At.TimeAgo();
              foreach(var c in i.Comments){
                  c.Ago = c.Created_At.TimeAgo();
              }
            }
        // JsonConvert.SerializeObject(AllPosts);

        // string postdata = JsonConvert.SerializeObject(AllPosts);
        //     string postdata = JsonConvert.SerializeObject(AllPosts, new JsonSerializerSettings(){
        //       PreserveReferencesHandling = PreserveReferencesHandling.Objects,
        //     Formatting = Formatting.Indented
        // });
            ViewBag.posts = AllPosts.OrderByDescending(x => x.Created_At);
            // return Json(postdata);
            return View();

        }

        [Route("/New")]
        [HttpPost]
        public IActionResult New(ViewModel FormData)
        {
            // if(HttpContext.Session.GetInt32("id") == null) { 
            //     return RedirectToAction("Logout");};
            Post Post = FormData.Post;
            if(ModelState.IsValid){
            User user = _context.users.SingleOrDefault(u => u.Userid == HttpContext.Session.GetInt32("id"));
            Post.Poster = user;
            _context.Add(Post);
            _context.SaveChanges();
            System.Console.WriteLine("***Created a Post***");
            return RedirectToAction("Smack");
            }
            System.Console.WriteLine("Post creation rejected*******");
            return View("Wall");
        }

        
        [HttpPost]
        public IActionResult addComment(ViewModel FormData)
        {
            // if(HttpContext.Session.GetInt32("id") == null) { 
            //     return RedirectToAction("Logout");}
            Comment Comment = FormData.Comment;
            if(ModelState.IsValid){
            User user = _context.users.SingleOrDefault(u => u.Userid== HttpContext.Session.GetInt32("id"));
            Post post = _context.posts.SingleOrDefault(p => p.Postid== Int32.Parse(Request.Form["post_id"]));
            // int PostID = Int32.Parse(Request.Form["post_id"]);
            // System.Console.WriteLine(PostID+" is the post id***************");
            System.Console.WriteLine();
            Comment.Commenter = user;
            Comment.Smack = post;
            _context.Add(Comment);
            _context.SaveChanges();
            return RedirectToAction("Wall");
            }
            System.Console.WriteLine("Comment creation rejected*******");
            return View("Wall");
        }


        [Route("/Account")]
        public IActionResult Account()
        {

        User user = _context.users.SingleOrDefault(u => u.Userid== HttpContext.Session.GetInt32("id"));
        ViewBag.name = HttpContext.Session.GetString("name");
        ViewBag.id = HttpContext.Session.GetInt32("id");
        ViewBag.picture = user.Image;

            List<Post> AllPosts = _context.posts.Include(u => u.Poster).Include(c => c.Comments).ThenInclude(i => i.Comment_Likes).Include(p => p.Post_Likes).ThenInclude(g => g.Liked_Post).ToList();
            foreach(var i in AllPosts){
              i.Ago = i.Created_At.TimeAgo();
              foreach(var c in i.Comments){
                  c.Ago = c.Created_At.TimeAgo();
              }
            }
            ViewBag.posts = AllPosts.OrderByDescending(x => x.Created_At).Take(5);



            return View("Account");
        }

        [Route("/Update")]
        public IActionResult Update(ViewModel FormData)
        {
            User LoggedUser= _context.users.SingleOrDefault(user => user.Userid == (int)HttpContext.Session.GetInt32("id"));
            ViewBag.LoggedUser = LoggedUser;

            if(ModelState.IsValid)
                {
                    if(FormData.editUser.Username != null)
                    {
                        User user = _context.users.SingleOrDefault(u => u.Username == FormData.editUser.Username);
                        if(user == null)
                        {
                            LoggedUser.Username = FormData.editUser.Username;
                            _context.Update(LoggedUser);
                            _context.SaveChanges();
                            TempData["success"] = "Username successfully changed!";
                            return View("Account");
                        }
                        if(FormData.editUser.Username == LoggedUser.Username)
                        {
                        }
                        else
                        {
                            ModelState.AddModelError("editUser.Username", "Username already exists.");
                            return View("Account");
                        }
                    }
                    if(FormData.editUser.editPassword != null)
                    {         
                        User user = _context.users.SingleOrDefault(u => u.Username == FormData.editUser.Username);                              
                        PasswordHasher<User> Hasher = new PasswordHasher<User>();
                        user.Password = Hasher.HashPassword(user, FormData.editUser.editPassword);
                            _context.Update(LoggedUser);
                            _context.SaveChanges(); 
                            TempData["success"] = "Password successfully changed!";
                    }
                     if(FormData.editUser.editImage != null)
                    {         
                        User user = _context.users.SingleOrDefault(u => u.Username == FormData.editUser.Username);
                        user.Image = FormData.editUser.editImage;
                        ViewBag.picture = FormData.editUser.editImage;
                            _context.Update(LoggedUser);
                            _context.SaveChanges(); 
                            TempData["success"] = "Password successfully changed!";
                    }
                    return View("Account");
                }               
            return View("Account", FormData);           
        }

        [HttpGet]
        [Route("/Delete/{num}")]
        public IActionResult Delete(int num)
        {
            //    if(HttpContext.Session.GetInt32("id") == null) { 
            //     return RedirectToAction("Logout");};

            Post del = _context.posts.SingleOrDefault(u => u.Postid == num);
            _context.Remove(del);
            _context.SaveChanges();
            return RedirectToAction("Wall");
        }

        [HttpGet]
        [Route("/Like/{num}/{word}")]
        public IActionResult Like(int num, string word)
        {
            // if(HttpContext.Session.GetInt32("id") == null) { 
            //     return RedirectToAction("Logout");};

            Post it = _context.posts.Include(g => g.Post_Likes).ThenInclude(a => a.Liked_Post).SingleOrDefault(u => u.Postid == num);
            User user = _context.users.SingleOrDefault(u => u.Userid== HttpContext.Session.GetInt32("id"));

            Post_Like newl = new Post_Like{
                Userid = user.Userid,
                Liked_Post = user,
                Postid = it.Postid,
                Has_Post_Likes = it
            };
            it.Post_Likes.Add(newl);
            _context.SaveChanges();

            if(word == "game"){
            return RedirectToAction("Game");

            }
            return RedirectToAction("Wall");
        }

        [HttpGet]
        [Route("/Delete_Comment/{num}")]
        public IActionResult Delete_Comment(int num)
        {
            //    if(HttpContext.Session.GetInt32("id") == null) { 
            //     return RedirectToAction("Logout");};

            Comment del = _context.comments.SingleOrDefault(u => u.Commentid == num);
            _context.Remove(del);
            _context.SaveChanges();
            return RedirectToAction("Wall");
        }

        [HttpGet]
        [Route("/Like_Comment/{num}")]
        public IActionResult Like_Comment(int num)
        {
            // if(HttpContext.Session.GetInt32("id") == null) { 
            //     return RedirectToAction("Logout");};

            Comment it = _context.comments.Include(g => g.Comment_Likes).ThenInclude(a => a.Liked_Comment).SingleOrDefault(u => u.Commentid == num);
            User user = _context.users.SingleOrDefault(u => u.Userid== HttpContext.Session.GetInt32("id"));

            Comment_Like newl = new Comment_Like{
                Userid = user.Userid,
                Liked_Comment = user,
                Commentid = it.Commentid,
                Has_Comment_Likes = it
            };
            it.Comment_Likes.Add(newl);
            _context.SaveChanges();
            return RedirectToAction("Wall");}



            public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            System.Console.WriteLine("***User is logging out ***");
            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
