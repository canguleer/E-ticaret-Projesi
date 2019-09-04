using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_ticaret_projesi.Models.i
{
    public class ProductModel
    {
        public DB.Products Product { get; set; }
        public List<DB.Comments> Comments { get; set; }
    }
}