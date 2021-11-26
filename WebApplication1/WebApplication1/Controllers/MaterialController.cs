using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Infrastructure.MemberResults;
using NKLibrary;

namespace WebApplication1.Controllers
{
    public class MaterialController : Controller
    {
        // GET: Material
        [Authorize]
        public ActionResult Index(int type, string id, int page = 1, int pageSize = 15)
        {


            NKDLL NKDLL = new NKDLL();
            if (!NKDLL.LoginCheck())
            {
                return RedirectToAction("Index", "Login");
            }


            String SearchString = id;
            try
            {

                ViewBag.MaterialType0 = "";
                ViewBag.MaterialType1 = "";

                ViewBag.MaterialType0 = (type == 0) ? "active" : "";
                ViewBag.MaterialType1 = (type == 1) ? "active" : "";
                ViewBag.Type = type;

                if (!String.IsNullOrEmpty(SearchString))
                {
                    page = 1;
                    pageSize = 15;
                    if (type == 0)
                    {
                        Models.Material.MaterialSearch.MaterialList = new Models.Material.Material().Get_Material(SearchString);
                        var material = Models.Material.MaterialSearch.MaterialList.OrderBy(x => x.MaterialID).ToPagedList(page, pageSize).FirstOrDefault();
                        return View(material);
                    }
                    else
                    {
                        Models.Material.MaterialSearch.MaterialList = new Models.Material.Material().Get_StoreMaterial(SearchString);
                        var storeMaterial = Models.Material.MaterialSearch.MaterialList.OrderBy(x => x.StoreID).ThenBy(n => n.MaterialID).ToPagedList(page, pageSize);
                        return View(storeMaterial);
                    }
                }
                else
                {
                    if (type == 0)
                    {
                        Models.Material.MaterialSearch.MaterialList = new Models.Material.Material().Get_Material();
                        var material = Models.Material.MaterialSearch.MaterialList.OrderBy(x => x.MaterialID).ToPagedList(page, pageSize);
                        return View(material);
                    }
                    else
                    {
                        Models.Material.MaterialSearch.MaterialList = new Models.Material.Material().Get_StoreMaterial();
                        var storeMaterial = Models.Material.MaterialSearch.MaterialList.OrderBy(x => x.StoreID).ThenBy(n => n.MaterialID).ToPagedList(page, pageSize);
                        return View(storeMaterial);
                    }
                }
                   
            }
            catch (Exception e)
            {
                ViewBag.ResultMessage = e.ToString();
                return View();
            }
        }

        public ActionResult _StorageDialog(string materialID)
        {
            var StoreStorge = new Models.Material.StoreStorge().Get_Storage(materialID);
            return PartialView(StoreStorge);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(int Type, Models.Material.Material postback)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    if (!new Models.Material.Material().IsMaterialIdExist(postback, Type))
                    {
                        ViewBag.ResultMessage = "商品編號已存在";
                        ViewBag.Type = Type;
                        return View(postback);
                    }
                    else
                    {
                        var result = new Models.Material.Material().Post_Material(postback, Type, (string)Session["UserID"]);
                        if (result)
                        {
                            TempData["ResultMessage"] = String.Format("商品[{0}]成功新增", postback.MaterialID);
                            return RedirectToAction("Index", "Material", new { type = Type });
                        }
                        else
                        {
                            ViewBag.ResultMessage = "資料有誤，請檢查";
                            ViewBag.Type = Type;
                            return View(postback);
                        }
                    }

                }
                else
                {
                    ViewBag.ResultMessage = "資料有誤，請檢查";
                    ViewBag.Type = Type;
                    return View(postback);
                }
            }
            catch (Exception e)
            {
                ViewBag.ResultMessage = e.ToString();
                return View();
            }
        }


        [Authorize]
        public ActionResult Create(int type)
        {
            ViewBag.Type = type;
            var categoryId = new Models.Material.Material().Get_Categpoy();
            ViewBag.CategoryList = categoryId;
            var categoryId2 = new Models.Material.Material().Get_Categpoy2();
            ViewBag.CategoryList2 = categoryId2;
            var supplierId = new Models.Material.Material().Get_Supplier();
            ViewBag.SupplierList = supplierId;
            return View();
        }

        [Authorize]
        public ActionResult Edit(int Type, string materialId,string storeId)
        {
            try
            {
                var categoryId = new Models.Material.Material().Get_Categpoy();
                ViewBag.CategoryList = categoryId;     
                var categoryId2 = new Models.Material.Material().Get_Categpoy2();
                ViewBag.CategoryList2 = categoryId2;

                var supplierId = new Models.Material.Material().Get_Supplier();
                ViewBag.SupplierList = supplierId;

                if (Type == 0)
                {
                    var material = new Models.Material.Material().Get_Edit_Material(materialId);
                    if (material != default(Models.Material.Material))
                    {
                        ViewBag.Type = Type;
                        return View(material);
                    }
                    else
                    {   //如果沒有資料則顯示錯誤訊息並導回Index頁面
                        TempData["resultMessage"] = "資料有誤，請重新操作";
                        return RedirectToAction("Index", "Material", new { type = Type });
                    }
                }
                else
                {
                 
                    var storeMaterial = new Models.Material.Material().Get_Edit_StoreMaterial(materialId, storeId);
                    if (storeMaterial != default(Models.Material.Material))
                    {
                        ViewBag.Type = Type;
                        return View(storeMaterial);
                    }
                    else
                    {   //如果沒有資料則顯示錯誤訊息並導回Index頁面
                        TempData["resultMessage"] = "資料有誤，請重新操作";
                        return RedirectToAction("Index", "Material", new { type = Type });
                    }

                }
            }
            catch (Exception e)
            {
                ViewBag.ResultMessage = e.ToString();
                ViewBag.Type = Type;
                return View();
            }
        }

     
        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult EditMaterial(int Type, Models.Material.Material postback)
        {
            try
            {

                if (this.ModelState.IsValid)
                {
                    var result = new Models.Material.Material().Patch_Material(postback, Type, (string)Session["UserID"]);
                    if (result)
                    {
                        TempData["ResultMessage"] = String.Format("商品[{0}]成功編輯", postback.MaterialID);
                        return RedirectToAction("Index", "Material", new { type = Type });
                    }
                    else
                    {
                        ViewBag.ResultMessage = "資料有誤，請檢查";
                        ViewBag.Type = Type;
                        return View(postback);
                    }

                }
                else
                {
                    ViewBag.ResultMessage = "資料有誤，請檢查";
                    ViewBag.Type = Type;
                    return View(postback);
                }
            }
            catch (Exception e)
            {
                ViewBag.ResultMessage = e.ToString();
                ViewBag.Type = Type;
                return View();
            }
        }

        [Authorize]
        public ActionResult Delete(int type ,string materialID, string storeId)
        {


            ViewBag.MaterialType0 = "";
            ViewBag.MaterialType1 = "";

            ViewBag.MaterialType0 = (type == 0) ? "active" : "";
            ViewBag.MaterialType1 = (type == 1) ? "active" : "";
            ViewBag.Type = type;

            try
            {
                 var result = new Models.Material.Material().Delete_Material(type, materialID, storeId);
                 if (result)
                 {
                     TempData["ResultMessage"] = String.Format("商品[{0}]成功刪除", storeId);
                     return RedirectToAction("Index", "Material", new { type = type });
                 }
                 else
                 {   //如果沒有資料則顯示錯誤訊息並導回Index頁面
                     TempData["ResultMessage"] = "資料有誤，請重新操作";
                     return RedirectToAction("Index", "Material", new { type = type });
                 }
            }
            catch (Exception e)
            {
                ViewBag.ResultMessage = e.ToString();
                return View();
            }

        }

        [HttpPost]
        [Authorize]
        public ActionResult HasData()
        {
            JObject jo = new JObject();
            bool result = !Models.Material.MaterialSearch.MaterialList.Count.Equals(0);
            jo.Add("Msg", result.ToString());
            return Content(JsonConvert.SerializeObject(jo), "application/json");
        }

        [Authorize]
        public ActionResult Export()
        {
            var exportSpource = this.GetExportDataWithAllColumns();
            var dt = JsonConvert.DeserializeObject<DataTable>(exportSpource.ToString());

            var exportFileName =
                string.Concat("MaterialData_", DateTime.Now.ToString("yyyyMMddHHmmss"), ".xlsx");

            return new ExportExcelResult
            {
                SheetName = "商品資料",
                FileName = exportFileName,
                ExportData = dt
            };
        }

        [Authorize]
        private JArray GetExportDataWithAllColumns()
        {
            //var query = db.Customers.OrderBy(x => x.CustomerID);
            var query = Models.Material.MaterialSearch.MaterialList.OrderBy(x => x.StoreID).ThenBy(n => n.MaterialID);
            JArray jObjects = new JArray();

            foreach (var item in query)
            {
                var jo = new JObject
                {
                    {"Category01", item.Category01},
                    {"Category02", item.Category02},
                    {"SupplierID1", item.SupplierID1},
                    {"StoreID", item.StoreID},
                    {"MaterialID", item.MaterialID},
                    {"MaterialNmae", item.MaterialNmae},
                    {"SalePrice", item.SalePrice},
                };
                jObjects.Add(jo);
            }
            return jObjects;
        }

    }
}
