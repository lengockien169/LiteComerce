using _19T1021111.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021111.Web.Models
{


    /// <summary>
    /// Kết quả tìm kiếm mặt hàng
    /// </summary>
    public class ProductSearchOutput : PaginationSearchOutput
    {
        /// <summary>
        /// Danh sách mặt hàng
        /// </summary>
        public List<Product> Data { get; set; }
    }
}