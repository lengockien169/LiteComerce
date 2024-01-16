using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021111.DomainModels
{
    /// <summary>
    /// Thông tin của nhân viên
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// ID của nhân viên
        /// </summary>
        public int EmployeeID { get; set; }
        /// <summary>
        /// Họ của nhân viên
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Tên của nhân viên
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// Ảnh
        /// </summary>
        public String Photo { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Notes { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
    }
}
