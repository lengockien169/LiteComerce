using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021111.DataLayers
{
    /// <summary>
    /// Định nghĩa các phép xử lý chung choc các dữ liệu đơn giản tương tự nhau
    /// </summary>
    public interface ICommonDAL<T> where T : class
    {
        /// <summary>
        /// Tìmm kiếm và lấy danh sách dữ liệu dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần hiển thị</param>
        /// <param name="pageSize">Số dòng được hiển thị trên 1 trang (0 tất là không yêu cầu phân trang)</param>
        /// <param name="searchValue">Giá trí tìm kiếm (chuỗi rỗng nếu không tìm kiếm theo tên)</param>
        /// <returns></returns>
        IList<T> List(int page = 1, int pageSize = 0, string searchValue = "");
        /// <summary>
        /// Đếm số dòng dữ liệu tìm được
        /// </summary>
        /// <param name="searchValue">Giá trị cần tìm (chuỗi rỗng nếu không tìm kiếm theo tên)</param>
        /// <returns></returns>
        int Count(string searchValue = "");
        /// <summary>
        /// Lấy một dòng dữ liệu đưa vào id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(int id);
        /// <summary>
        /// Bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns>ID của dữ liệu được tạo mới</returns>
        int Add(T data);
        /// <summary>
        /// Cập nhật
        /// </summary>
        /// <param name="data"></param>
        /// <returns>ID dữ liệu được cập nhật</returns>
        bool Update(T data);
        /// <summary>
        /// Xoá
        /// </summary>
        /// <param name="id">id của dữ liệu cần xoá</param>
        /// <returns></returns>
        bool Delete(int id);
        /// <summary>
        /// Kiểm tra xem nhà cung cấp hiện có dữ liệu liên quan hay không
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool InUsed(int id);
    }
}
