using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
      
        public ICollection<BookCategory> BookCategories { get; set; }
    }
}
