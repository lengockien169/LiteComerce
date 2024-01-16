using _19T1021111.BusinessLayers;
using _19T1021111.DomainModels;
using _19T1021111.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _19T1021111.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [RoutePrefix("product")]
    public class ProductController : Controller
    {
        private const int PAGE_SIZE = 8;
        private const string PRODUCT_SEARCH = "SearchProductCondition";
        /// <summary>
        /// Tìm kiếm, hiển thị mặt hàng dưới dạng phân trang
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ProductSearchInput condition = Session[PRODUCT_SEARCH] as ProductSearchInput;
            if (condition == null)
            {
                condition = new ProductSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = "",
                    CategoryID = 0,
                    SupplierID = 0
                };
            }
            return View(condition);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ActionResult Search(ProductSearchInput condition)
        {
            int rowCount = 0;
            var data = ProductDataService.ListProducts(condition.Page, condition.PageSize, condition.SearchValue, condition.CategoryID, condition.SupplierID, out rowCount);
            var result = new ProductSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                CategoryID = condition.CategoryID,
                SupplierID = condition.SupplierID,
                Data = data
            };

            Session[PRODUCT_SEARCH] = condition;
            return View(result);
        }
        /// <summary>
        /// Tạo mặt hàng mới
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var data = new Product()
            {
                ProductID = 0
            };
            ViewBag.Title = "Bổ sung mặt hàng";
            return View(data);
        }
        /// <summary>
        /// Cập nhật thông tin mặt hàng, 
        /// Hiển thị danh sách ảnh và thuộc tính của mặt hàng, điều hướng đến các chức năng
        /// quản lý ảnh và thuộc tính của mặt hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Index");
            int productID = Convert.ToInt32(id);
            var photos = ProductDataService.ListPhotos(productID);
            var attributes = ProductDataService.ListAttributes(productID);
            var product = ProductDataService.Get(productID);
            var data = new ProductEditModel
            {
                Product = product,
                Attributes = attributes,
                Photos = photos
            };
            if (product == null)
                return RedirectToAction("Index");
            ViewBag.Title = "Cập nhật mặt hàng";
            return View(data);
        }
        [HttpPost]
        public ActionResult SaveCreate(Product data, HttpPostedFileBase uploadPhoto)
        {
            if (string.IsNullOrWhiteSpace(data.ProductName))
                ModelState.AddModelError("ProductName", "Tên khách hàng không được để trống");
            if (string.IsNullOrEmpty(data.CategoryID.ToString()))
                ModelState.AddModelError("CategoryID", "Phải chọn loại hàng");
            if (string.IsNullOrEmpty(data.SupplierID.ToString()))
                ModelState.AddModelError("SupplierID", "Phải chọn nhà cung cấp");
            if (string.IsNullOrWhiteSpace(data.Unit))
                ModelState.AddModelError("Unit", "Tên đơn vị không được để trống");
            if (string.IsNullOrEmpty(data.Price.ToString()))
                ModelState.AddModelError("Price", "Giá hàng không được để trống");
            if (string.IsNullOrWhiteSpace(data.Photo))
                data.Photo = "";
            if (!ModelState.IsValid)
            {
                return View("Create", data);
            }
            if (uploadPhoto != null)
            {
                string path = Server.MapPath("~/Images/Products");
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string filePath = System.IO.Path.Combine(path, fileName);
                uploadPhoto.SaveAs(filePath);
                data.Photo = fileName;
            }
            ProductDataService.AddProduct(data);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Lưu dữ liệu
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(ProductEditModel data, HttpPostedFileBase uploadPhoto)
        {
            //Kiểm soát đầu vào
            if (!ModelState.IsValid)
            {
                return View(data);
            }
            if (uploadPhoto != null)
            {
                string path = Server.MapPath("~/Images/Products");
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string filePath = System.IO.Path.Combine(path, fileName);
                uploadPhoto.SaveAs(filePath);
                data.Product.Photo = fileName;
            }
            //if (data.Product.ProductID == 0)
            //{
            //    ProductDataService.AddProduct(data.Product);
            //}
            //else
            //{
            ProductDataService.UpdateProduct(data.Product);
            //}
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Xóa mặt hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        public ActionResult Delete(int id = 0)
        {
            int productId = Convert.ToInt32(id);
            if (Request.HttpMethod == "GET")
            {
                var data = ProductDataService.Get(productId);
                return View(data);
            }
            else
            {
                ProductDataService.DeleteProduct(productId);
                return RedirectToAction("Index");
            }
        }


        /// <summary>
        /// Các chức năng quản lý ảnh của mặt hàng
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="photoID"></param>
        /// <returns></returns>
        [Route("photo/{method?}/{productID?}/{photoID?}")]
        public ActionResult Photo(string method = "add", int productID = 0, long photoID = 0)
        {
            switch (method)
            {
                case "add":
                    ViewBag.Title = "Bổ sung ảnh";
                    var productPhoto = new ProductPhoto
                    {
                        PhotoID = 0,
                        ProductID = productID,
                        DisplayOrder = 1
                    };
                    return View(productPhoto);

                case "edit":
                    ViewBag.Title = "Thay đổi ảnh";
                    var photo = ProductDataService.GetPhoto(photoID);
                    return View(photo);
                case "delete":
                    //ProductDataService.DeletePhoto(photoID);
                    ProductDataService.DeletePhoto(photoID);
                    return RedirectToAction("Edit", new { id = productID }); //return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult SavePhoto(ProductPhoto data, HttpPostedFileBase uploadPhoto)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.Title = "Bổ sung ảnh mặt hàng";
                return View(data);
            }
            if (uploadPhoto != null)
            {
                string path = Server.MapPath("~/Images/Products/Photos");
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string filePath = System.IO.Path.Combine(path, fileName);
                uploadPhoto.SaveAs(filePath);
                data.Photo = fileName;
            }
            if (data.PhotoID == 0)
            {
                if (!ModelState.IsValid)
                    return View("Photo", data);

                ProductDataService.AddPhoto(data);
            }
            else
            {
                if (!ModelState.IsValid)
                    return RedirectToAction("Photo", new { method = "edit", productID = data.ProductID, photoID = data.PhotoID });

                ProductDataService.UpdatePhoto(data);
            }

            return RedirectToAction("Edit", new { id = data.ProductID });
        }
        /// <summary>
        /// Các chức năng quản lý thuộc tính của mặt hàng
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        [Route("attribute/{method?}/{productID}/{attributeID?}")]
        public ActionResult Attribute(string method = "add", int productID = 0, long attributeID = 0)
        {
            switch (method)
            {
                case "add":
                    ViewBag.Title = "Bổ sung thuộc tính";
                    var productAttribute = new ProductAttribute
                    {
                        AttributeID = 0,
                        ProductID = productID
                    };
                    return View(productAttribute);
                case "edit":
                    ViewBag.Title = "Thay đổi thuộc tính";
                    var attribute = ProductDataService.GetAttribute(attributeID);
                    return View(attribute);

                case "delete":
                    //ProductDataService.DeleteAttribute(attributeID);
                    ProductDataService.DeleteAttribute(attributeID);
                    return RedirectToAction("Edit", new { id = productID }); //return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Index");


            }
        }
        [HttpPost]
        public ActionResult SaveAttribute(ProductAttribute data)
        {
            if (string.IsNullOrWhiteSpace(data.AttributeName))
                ModelState.AddModelError("AttributeName", "Tên thuộc tính không được để trống");
            if (string.IsNullOrEmpty(data.AttributeValue.ToString()))
                ModelState.AddModelError("AttributeValue", "Giá trị thuộc tính không được để trống");
            if (string.IsNullOrEmpty(data.DisplayOrder.ToString()))
                ModelState.AddModelError("DisplayOrder", "Thứ tự hiển thị không được để trống");
            if (data.AttributeID == 0)
            {
                if (!ModelState.IsValid)
                    return View("Attribute", data);

                ProductDataService.AddAttribute(data);
            }
            else
            {
                if (!ModelState.IsValid)
                    return RedirectToAction("Attribute", new { method = "edit", productID = data.ProductID, attributeID = data.AttributeID });

                ProductDataService.UpdateAttribute(data);
            }

            return RedirectToAction("Edit", new { id = data.ProductID });
        }

    }
}