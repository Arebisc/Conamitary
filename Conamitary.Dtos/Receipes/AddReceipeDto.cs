using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conamitary.Dtos.Receipes
{
    public class AddReceipeDto
    {
        public string Title { get; set; }
        public string Instructions { get; set; }
        public string Ingredients { get; set; }
    }
}
