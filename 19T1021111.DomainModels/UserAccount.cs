using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021111.DomainModels
{
    /// <summary>
    /// tài khoản người dùng
    /// </summary>
    public class UserAccount
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Họ tên
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Tài khoản
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Mật khẩu
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Vai trò
        /// </summary>
        public string RoleNames { get; set; }
        /// <summary>
        /// Ảnh
        /// </summary>
        public string Photo { get; set; }
    }
}
