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
    public class ShipperController : Controller
    {
        private const int PAGE_SIZE = 5;
        private const string SHIPPER_SEARCH = "SearchShipperCondition";
        //public ActionResult Index(int page = 1, int pageSize = 10, string searchValue = "")
        //{
        //    int rowCount = 0;
        //    var model = CommonDataService.ListOfShippers(page, pageSize, searchValue, out rowCount);
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
            PaginationSearchInput condition = Session[SHIPPER_SEARCH] as PaginationSearchInput;
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
            var data = CommonDataService.ListOfShippers(condition.Page, condition.PageSize, condition.SearchValue, out rowCount);
            var result = new ShipperSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };

            Session[SHIPPER_SEARCH] = condition;
            return View(result);
        }
        /// <summary>
        /// Bổ sung khách hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var data = new Shipper()
            {
                ShipperID = 0
            };
            ViewBag.Title = "Bổ sung người giao hàng";
            return View("Edit", data);
        }
        /// <summary>
        /// Sửa người giao hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Index");
            var data = CommonDataService.GetShipper(id);
            if (data == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Cập nhật người giao hàng";
            return View(data);
        }
        /// <summary>
        /// Lưu dữ liệu
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult Save(Shipper data)
        {
                //Kiểm soát đầu vào
                if (string.IsNullOrWhiteSpace(data.ShipperName))
                    ModelState.AddModelError("ShipperName", "Tên người giao hàng được để trống");
                if (string.IsNullOrWhiteSpace(data.Phone))
                    ModelState.AddModelError("Phone", "Số điện thoại không được để trống");
                if (!ModelState.IsValid)
                {
                    ViewBag.Title = data.ShipperID == 0 ? "Bổ sung nhà cung cấp" : "Cập nhật nhà cung cấp";
                    return View("Edit", data);
                }
                if (data.ShipperID == 0)
                {
                    CommonDataService.AddShipper(data);
                }
                else
                {
                    CommonDataService.UpdateShipper(data);
                }
                return RedirectToAction("Index");
           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {
            int shipperId = Convert.ToInt32(id);
            if (Request.HttpMethod == "GET")
            {
                var data = CommonDataService.GetShipper(shipperId);
                return View(data);
            }
            else
            {
                CommonDataService.DeleteShipper(shipperId);
                return RedirectToAction("Index");
            }
        }
    }
}