using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021111.DomainModels;
using _19T1021111.BusinessLayers;
using _19T1021111.Web.Models;
namespace _19T1021111.Web.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private const int PAGE_SIZE = 6;
        private const string EMPLOYEE_SEARCH = "SearchEmployeeCondition";
        //public ActionResult Index(int page = 1, int pageSize = 6, string searchValue = "")
        //{
        //    int rowCount = 0;
        //    var model = CommonDataService.ListOfEmployees(page, pageSize, searchValue, out rowCount);
        //    int pageCount = rowCount / pageSize;
        //    if (rowCount % pageSize > 0)
        //        pageCount += 1;
        //    ViewBag.Page = page;
        //    ViewBag.PageCount = pageCount;
        //    ViewBag.RowCount = rowCount;
        //    ViewBag.PageSize = pageSize;
        //    ViewBag.SearchValue = searchValue;
        //    return View(model);
        //}
        /// <summary>
        /// Giao diện
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            PaginationSearchInput condition = Session[EMPLOYEE_SEARCH] as PaginationSearchInput;
            if (condition == null)
            {
                condition = new PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = ""
                };
            }

            return View(condition);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ActionResult Search(PaginationSearchInput condition)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfEmployees(condition.Page, condition.PageSize, condition.SearchValue, out rowCount);
            var result = new EmployeeSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };

            Session[EMPLOYEE_SEARCH] = condition;
            return View(result);
        }
        /// <summary>
        /// Bổ sung nhân viên
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var data = new Employee()
            {
                EmployeeID = 0
            };
            ViewBag.Title = "Bổ sung nhân viên";
            return View("Edit", data);
        }
        /// <summary>
        /// Cập nhật nhân viên
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Index");
            var data = CommonDataService.GetEmployee(id);
            if (data == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Cập nhật nhân viên";
            return View(data);
        }
        /// <summary>
        /// Lưu dữ liệu
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Employee data, string birthday,HttpPostedFileBase uploadPhoto)
        {
            DateTime? d = Converter.DMYStringToDateTime(birthday);
            if (d == null)
                ModelState.AddModelError("BirthDate", "Ngày không hợp lệ, vui lòng nhập theo định dạng dd/MM/yyyy");
            else
                data.BirthDate = d.Value;
            //Kiểm soát đầu vào
            if (string.IsNullOrWhiteSpace(data.LastName))
                ModelState.AddModelError("LastName", "Họ không được để trống");
            if (string.IsNullOrWhiteSpace(data.FirstName))
                ModelState.AddModelError("FirstName", "Tên không được để trống");
            if (string.IsNullOrWhiteSpace(data.Email))
                ModelState.AddModelError("Email", "Email không được để trống");
            if (string.IsNullOrWhiteSpace(data.Photo))
                data.Photo = "";
            if (!ModelState.IsValid)
            {
                ViewBag.Title = data.EmployeeID == 0 ? "Bổ sung nhân viên" : "Cập nhật nhân viên";
                return View("Edit", data);
            }
            if (uploadPhoto != null)
            {
                string path = Server.MapPath("~/Images");
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string filePath = System.IO.Path.Combine(path, fileName);
                uploadPhoto.SaveAs(filePath);
                data.Photo = fileName;
            }
            if (data.EmployeeID == 0)
            {
                CommonDataService.AddEmployee(data);
            }
            else
            {
                CommonDataService.UpdateEmployee(data);
            }
            return RedirectToAction("Index");

        }
        /// <summary>
        /// Xoá nhân viên
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {
            int employeeId = Convert.ToInt32(id);
            if (Request.HttpMethod == "GET")
            {
                var data = CommonDataService.GetEmployee(employeeId);
                return View(data);
            }
            else
            {
                CommonDataService.DeleteEmployee(employeeId);
                return RedirectToAction("Index");
            }
        }
    }
}