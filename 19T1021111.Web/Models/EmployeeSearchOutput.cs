using _19T1021111.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021111.Web.Models
{
    /// <summary>
    /// Kết quả tìm kiếm nhân viên
    /// </summary>
    public class EmployeeSearchOutput : PaginationSearchOutput
    {
        /// <summary>
        /// Danh sách nhân viên
        /// </summary>
        public List<Employee> Data { get; set; }
    }
}