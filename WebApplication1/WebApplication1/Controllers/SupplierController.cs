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

namespace WebApplication1.Controllers
{
    public class SupplierController : Controller
    {
        // GET: Supplier
        public ActionResult Index(string id, int page = 1, int pageSize = 15)
        {
            if ((string)Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }


            String SearchString = id;


            try
            {
                if (!String.IsNullOrEmpty(SearchString))
                {
                    page = 1;
                    pageSize = 15;
                    Models.Supplier.SupplierSearch.SupplierList = Models.Supplier.Supplier.Get_Supplier(SearchString);
                    var suppliers = Models.Supplier.SupplierSearch.SupplierList.OrderBy(x => x.SupplierID).ToPagedList(page, pageSize); 
                    return View(suppliers);
                }
                else
                {
                    Models.Supplier.SupplierSearch.SupplierList = Models.Supplier.Supplier.Get_Supplier();
                    var result = Models.Supplier.SupplierSearch.SupplierList.OrderBy(x => x.SupplierID).ToPagedList(page, pageSize);
                    return View(result);
                }
            }
            catch (Exception e)
            {
                ViewBag.ResultMessage = e.ToString();
                return View();
            }

        }

        public ActionResult Edit(string id)
        {
            try
            {
                var result = new Models.Supplier.Supplier().Get_Edit_Supplier(id);
                if (result != default(Models.Supplier.Supplier))
                {
                    return View(result);
                }
                else
                {   //如果沒有資料則顯示錯誤訊息並導回Index頁面
                    TempData["resultMessage"] = "資料有誤，請重新操作";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ViewBag.ResultMessage = e.ToString();
                return View();
            }
        }



        [HttpPost]
        public ActionResult Edit(Models.Supplier.Supplier postback)
        {
            try
            {
           
                if (this.ModelState.IsValid)
                {
                    var result = new Models.Supplier.Supplier().Patch_Supplier(postback, (string)Session["UserID"]);
                    if (result)
                    {
                        TempData["ResultMessage"] = String.Format("廠商[{0}]成功編輯", postback.SupplierID);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.ResultMessage = "資料有誤，請檢查";
                        return View(postback);
                    }

                }
                else
                {
                    ViewBag.ResultMessage = "資料有誤，請檢查";
                    return View(postback);
                }
            }
            catch (Exception e)
            {
                ViewBag.ResultMessage = e.ToString();
                return View();
            }
        }


        public ActionResult Delete(string id)
        {

            try
            {
                var result = new Models.Supplier.Supplier().Delete_Supplier(id);
                if (result) //判斷此id是否有資料
                {
                    TempData["ResultMessage"] = String.Format("會員[{0}]成功刪除", id);
                    return RedirectToAction("Index");
                }
                else
                {   //如果沒有資料則顯示錯誤訊息並導回Index頁面
                    TempData["ResultMessage"] = "資料有誤，請重新操作";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ViewBag.ResultMessage = e.ToString();
                return View();
            }

        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult HasData()
        {
            JObject jo = new JObject();
            bool result = !Models.Supplier.SupplierSearch.SupplierList.Count.Equals(0);
            jo.Add("Msg", result.ToString());
            return Content(JsonConvert.SerializeObject(jo), "application/json");
        }

        public ActionResult Export()
        {
            var exportSpource = this.GetExportDataWithAllColumns();
            var dt = JsonConvert.DeserializeObject<DataTable>(exportSpource.ToString());

            var exportFileName =
                string.Concat("SupplierData_", DateTime.Now.ToString("yyyyMMddHHmmss"), ".xlsx");

            return new ExportExcelResult
            {
                SheetName = "廠商資料",
                FileName = exportFileName,
                ExportData = dt
            };
        }

        private JArray GetExportDataWithAllColumns()
        {
            var query = Models.Supplier.SupplierSearch.SupplierList.OrderBy(x => x.SupplierID);
            JArray jObjects = new JArray();

            foreach (var item in query)
            {
                var jo = new JObject
                {
                    {"SupplierID", item.SupplierID},
                    {"FullName", item.FullName},
                };
                jObjects.Add(jo);
            }
            return jObjects;
        }



        [HttpPost]
        public ActionResult Create(Models.Supplier.Supplier postback)
        {

            try
            {
             
                if (this.ModelState.IsValid)
                {
                    if (!new Models.Supplier.Supplier().IsSupplieridExist(postback.SupplierID))
                    {
                        ViewBag.ResultMessage = "廠商編號已存在";
                        return View(postback);
                    }
                    else
                    {
                        var result = new Models.Supplier.Supplier().Post_Supplier(postback ,(string)Session["UserID"]);
                        if (result)
                        {
                            TempData["ResultMessage"] = String.Format("廠商[{0}]成功新增", postback.SupplierID);

                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewBag.ResultMessage = "資料有誤，請檢查";
                            return View(postback);
                        }
                    }

                }
                else
                {
                    ViewBag.ResultMessage = "資料有誤，請檢查";
                    return View(postback);
                }
            }
            catch (Exception e)
            {
                ViewBag.ResultMessage = e.ToString();
                return View();
            }
        }
    }



}