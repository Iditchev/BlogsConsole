using System;
using NLog.Web;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace BlogsConsole
{
    class Program
    {
          // create static instance of Logger
         private static NLog.Logger logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {
            logger.Info("Program started");
            string userinput;
            do
            {

                 Console.WriteLine("Enter your selection: ");
                 Console.WriteLine("1. Display all Blogs");
                 Console.WriteLine("2. Add Blog");
                 Console.WriteLine("3. Create Post");
                 Console.WriteLine("4. Display Posts");
                 Console.WriteLine("Enter any other key to quit");
                 
                 userinput = Console.ReadLine();
                 
                 if (userinput == "1")
                 {
                     logger.Info($"Option \"{userinput}\" selected");
                     // Display all Blogs from the database
                        var db = new BloggingContext();
                        var count = db.Blogs.Count();
                        var query = db.Blogs.OrderBy(b => b.Name);

                        Console.WriteLine($"{count} Blogs returned");
                        foreach (var item in query)
                            {
                        Console.WriteLine(item.Name);
                            }   
                 }
                 else if (userinput == "2")
                 {
                    logger.Info($"Option \"{userinput}\" selected");
                    try 
                    {
                        Console.WriteLine("Enter a name for a new blog:");
                        var name = Console.ReadLine();
                        if (name == "")

                        {
                            logger.Error("Blog name cannot be null");
                            
                        }
                        else
                        {
                        var blog = new Blog { Name = name };

                        var db = new BloggingContext();
                        db.AddBlog(blog);
                        logger.Info("Blog added - {name}", name);
                        }
                        
                    } catch (Exception ex)
                        {
                         logger.Error(ex.Message);
                        }

                 } 
                 else if (userinput == "3") 
                 {
                     logger.Info($"Option \"{userinput}\" selected");
                     Console.WriteLine("Select the Blog you want to post to:");
                        var db = new BloggingContext();
                        var query = db.Blogs.OrderBy(b => b.BlogId);
                        var count = db.Blogs.Count();
                        
                        foreach (var item in query)
                            {
                        Console.WriteLine($"{item.BlogId}. {item.Name}");
                            }    
                      
                       
                        bool success = Int32.TryParse (Console.ReadLine(),out int blogId); 
                        if (success)
                       { var blog = db.Blogs.FirstOrDefault(m => m.BlogId == blogId);
                            if (blog != null)
                            {
                            Post post = new Post();
                            Console.WriteLine("Enter Post Title:");
                            post.Title = Console.ReadLine();
                            Console.WriteLine("Enter Post Content:");
                            post.Content = Console.ReadLine();
                            post.BlogId = blog.BlogId;
                            db.AddPost(post);
                            } 
                            else 
                            {
                            logger.Error("There are no blogs saved with that Id");
                            }    
                        }
                        else { logger.Error("Invalid entry, please try again");}
                       
                        
                 }      
                 else if (userinput == "4")
                 {
                     logger.Info($"Option \"{userinput}\" selected");
                     Console.WriteLine("Select the blog's posts to display:");
                     var db = new BloggingContext();
                     var query = db.Blogs.OrderBy(b => b.BlogId);

                      foreach (var item in query)
                            {
                            Console.WriteLine($"{item.BlogId}. Posts from {item.Name}");
                            }

                        bool success = Int32.TryParse (Console.ReadLine(),out int blogId); 
                        if (success)
                       { var blog = db.Blogs.FirstOrDefault(m => m.BlogId == blogId);
                            if (blog != null)
                            {
                                var count = db.Posts.Where(m => m.BlogId == blogId).Count();
                                var posts = db.Posts.Where(m => m.BlogId == blogId).OrderBy(p => p.Title);

                                Console.WriteLine($"{count} Posts returned");
                                    foreach (var item in posts)
                                    {
                                        Console.WriteLine($"Blog:{item.Blog.Name}");
                                        Console.WriteLine($"Title:{item.Title}");
                                        Console.WriteLine($"Content:{item.Content}");
                                    }   
                            } else {logger.Error("There are no blogs saved with that Id");}
                        } else {logger.Error("Invalid entry, please try again");}
             
                }
            }while (userinput == "1" || userinput == "2" || userinput == "3" || userinput == "4");
        }    
    }   
}