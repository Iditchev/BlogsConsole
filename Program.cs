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

                 // Create and save a new Blog
                 Console.WriteLine("1. Display all Blogs");
                 Console.WriteLine("2. Add Blog");
                 Console.WriteLine("3. Create Post");
                 Console.WriteLine("4. Display Posts");
                 
                 userinput = Console.ReadLine();
                 
                 if (userinput == "1")
                 {
                     // Display all Blogs from the database
                        var query = db.Blogs.OrderBy(b => b.Name);

                        Console.WriteLine("All blogs in the database:");
                        foreach (var item in query)
                            {
                        Console.WriteLine(item.Name);
                            }   
                 }
                 else if (userinput == "2")
                 {
                    try 
                    {
                        Console.WriteLine("Enter a name for a new blog:");
                        var name = Console.ReadLine();

                        var blog = new Blog { Name = name };

                        var db = new BloggingContext();
                        db.AddBlog(blog);
                        logger.Info("Blog added - {name}", name);

                        
                    } catch
                        {
                         logger.Error(ex.Message);
                        }

                 }    
            }  
             while (userinput == "1" || userinput == "2" || userinput == "3" || userinput == "4");
             
                
             


            logger.Info("Program ended");
        }
    }
}
