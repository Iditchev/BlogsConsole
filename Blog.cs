using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
 namespace BlogsConsole
 {
         public class Blog
     {
         public int BlogId { get; set; }
         public string Name { get; set; }

         public List<Post> Posts { get; set; }
     }
 }