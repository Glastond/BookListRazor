using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookListRazor.Model
{
    public class Book
    {
        [Key] // Automatically Creates an Id.
        public int Id { get; set; }

        [Required] // Makes the Property required.
        public string Name { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }

    }
}
