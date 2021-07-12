using System;

namespace Conamitary.Dtos.Receipes
{
    public class ReceipeDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Instructions { get; set; }
        public string Ingredients { get; set; }
        public string[] ImagesIds { get; set; }
    }
}
