using System;

namespace Conamitary.Database
{
    public class Receipe
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Instructions { get; set; }
        public string Ingredients { get; set; }
    }
}
