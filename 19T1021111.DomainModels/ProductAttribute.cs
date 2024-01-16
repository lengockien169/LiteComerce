﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021111.DomainModels
{
    /// <summary>
    /// Thuộc tính của mặt hàng
    /// </summary>
    public class ProductAttribute
    {
        ///<summary>
        ///ID của thuộc tính
        ///</summary>
        public long AttributeID { get; set; }
        ///<summary>
        ///ID của mặt hàng
        ///</summary>
        public int ProductID { get; set; }
        ///<summary>
        ///Tên thuộc tính
        ///</summary>
        public string AttributeName { get; set; }
        ///<summary>
        ///Giá trị thuộc tính
        ///</summary>
        public string AttributeValue { get; set; }
        ///<summary>
        ///Thứ tự hiển thị
        ///</summary>
        public int DisplayOrder { get; set; }
    }

}