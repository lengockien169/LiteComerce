using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021111.DomainModels
{
    /// <summary>
    /// Mặt hàng
    /// </summary>
    public class Product
    {
        ///<summary>
        ///ID của mặt hàng
        ///</summary>
        public int ProductID { get; set; }
        ///<summary>
        ///Tên của mặt hàng
        ///</summary>
        public string ProductName { get; set; }
        ///<summary>
        ///ID của nhà cung cấp
        ///</summary>
        public int SupplierID { get; set; }
        ///<summary>
        ///Id của loại hàng
        ///</summary>
        public int CategoryID { get; set; }
        ///<summary>
        ///Đơn vị tính
        ///</summary>
        public string Unit { get; set; }
        ///<summary>
        ///Giá
        ///</summary>
        public decimal Price { get; set; }
        ///<summary>
        ///Ảnh
        ///</summary>
        public string Photo { get; set; }
    }

}

