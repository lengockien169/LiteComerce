using _19T1021111.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021111.Web.Models
{
    public class ProductEditModel : Product
    {
        public List<ProductAttribute> Attributes { get; set; }
        public List<ProductPhoto> Photos { get; set; }
        public Product Product { get; set; }
    }
}