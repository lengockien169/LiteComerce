using _19T1021111.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021111.Web.Models
{
    /// <summary>
    /// Kết quả tìm kiếm người giao hàng
    /// </summary>
    public class ShipperSearchOutput : PaginationSearchOutput
    {
        /// <summary>
        /// Danh sách Shipper
        /// </summary>
        public List<Shipper> Data {get;set;}
    }
}