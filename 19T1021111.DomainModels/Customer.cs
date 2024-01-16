using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021111.DomainModels
{
    /// <summary>
    /// Thông tin của khách hàng
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Id của khách hàng
        /// </summary>
        public int CustomerID { get; set; }
        /// <summary>
        /// Tên của khách hàng
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// Tên giao dịch
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Thành phố
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Mã bưu điện
        /// </summary>
        public string PostalCode { get; set; }
        /// <summary>
        /// Quốc gia
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
    }
}
