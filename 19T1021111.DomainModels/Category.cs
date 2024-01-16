using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021111.DomainModels
{
    /// <summary>
    ///  Thông tin loại hàng
    /// </summary>
    public class Category
    {
        /// <summary>
        ///  ID của loại hàng
        /// </summary>
        public int CategoryID { get; set; }
        /// <summary>
        /// Tên của loại hàng
        /// </summary>
        public String CategoryName { get; set; }
        /// <summary>
        /// Mô tả loại hàng
        /// </summary>
        public String Description { get; set; }
    }
}
