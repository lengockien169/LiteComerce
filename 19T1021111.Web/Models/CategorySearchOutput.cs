using _19T1021111.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021111.Web.Models
{
    /// <summary>
    /// Kết quả tìm kiếm nhà cung cấp
    /// </summary>
    public class CategorySearchOutput : PaginationSearchOutput
    {
        /// <summary>
        /// Danh sách nhà cung cấp
        /// </summary>
        public List<Category> Data { get; set; }
    }
}