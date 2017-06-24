using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarFinder.Models
{
    public class CarViewModel
    {
        public string Recalls { get; set; }
        public IEnumerable<ImageResults> ImgSearchResult { get; set; }
    }
}