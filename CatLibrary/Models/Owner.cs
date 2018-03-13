using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatLibrary.Models
{
    public class Owner
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public List<Pet> Pets { get; set; }
    }
}