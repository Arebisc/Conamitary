using System;
using System.Collections.Generic;

namespace Conamitary.Database.Models
{
    public class Receipe
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Instructions { get; set; }
        public string Ingredients { get; set; }
        public ICollection<File> Photos { get; set; }
    }
}
