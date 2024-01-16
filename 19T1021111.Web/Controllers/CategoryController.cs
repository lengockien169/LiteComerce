using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021111.BusinessLayers;
using _19T1021111.DomainModels;
using _19T1021111.DataLayers;
using _19T1021111.Web.Models;

namespace _19T1021111.Web.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private const int PAGE_SIZE = 5;
        private const string CATEGORY_SEARCH = "SearchCategoryCondition";
        //public ActionResult Index(int page = 1, int pageSize = 10, string searchValue = "")
        //{
        //    int rowCount = 0;
        //    var model = CommonDataService.ListOfCategories(page, pageSize, searchValue, out rowCount);
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
            PaginationSearchInput condition = Session[CATEGORY_SEARCH] as PaginationSearchInput;
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
            var data = CommonDataService.ListOfCategories(condition.Page, condition.PageSize, condition.SearchValue, out rowCount);
            var result = new CategorySearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };

            Session[CATEGORY_SEARCH] = condition;
            return View(result);
        }
        /// <summary>
        /// Bổ sung loại hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var data = new Category()
            {
                CategoryID = 0
            };
            ViewBag.Title = "Bổ sung loại hàng";
            return View("Edit", data);
        }
        /// <summary>
        /// Sửa loại hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Index");
            var data = CommonDataService.GetCategory(id);
            if (data == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Cập nhật loại hàng";
            return View(data);
        }
        /// <summary>
        /// Lưu dữ liệu
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult Save(Category data)
        {
                //Kiểm soát đầu vào
                if (string.IsNullOrWhiteSpace(data.CategoryName))
                    ModelState.AddModelError("CategoryName", "Tên loại hàng không được để trống");
                if (string.IsNullOrWhiteSpace(data.Description))
                    ModelState.AddModelError("Description", "Mô tả không được để trống");
                if (!ModelState.IsValid)
                {
                    ViewBag.Title = data.CategoryID == 0 ? "Bổ sung loại hàng" : "Cập nhật loại hàng";
                    return View("Edit", data);
                }
                if (data.CategoryID == 0)
                {
                    CommonDataService.AddCategory(data);
                }
                else
                {
                    CommonDataService.UpdateCategory(data);
                }
                return RedirectToAction("Index");
        }
        /// <summary>
        /// Xoá loại hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {
            int categoryId = Convert.ToInt32(id);
            if (Request.HttpMethod == "GET")
            {
                var data = CommonDataService.GetCategory(categoryId);
                return View(data);
            }
            else
            {
                CommonDataService.DeleteCategory(categoryId);
                return RedirectToAction("Index");
            }
        }
    }
}