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
    public class SupplierSearchOutput : PaginationSearchOutput
    {
        /// <summary>
        /// Danh sách các nhà cung cấp
        /// </summary>
        public List<Supplier> Data { get; set; }
    }
}