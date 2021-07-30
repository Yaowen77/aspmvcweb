﻿using Newtonsoft.Json;
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
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index(int type ,string id,int page = 1, int pageSize = 15)
        {
            if ((string)Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }


             String SearchString = id;
            try
            {

                ViewBag.CategoryType0 = "";
                ViewBag.CategoryType1 = "";
                ViewBag.CategoryType2 = "";
                ViewBag.CategoryType3 = "";

                ViewBag.CategoryType0 = (type == 0) ? "active" : "";
                ViewBag.CategoryType1 = (type == 1) ? "active" : "";
                ViewBag.CategoryType2 = (type == 2) ? "active" : "";
                ViewBag.CategoryType3 = (type == 3) ? "active" : "";
                ViewBag.Type = type;


                 if (!String.IsNullOrEmpty(SearchString))
                {
                    Models.Category.CategorySearch.CategoryList = Models.Category.Category.Get_Category(SearchString, type);
                    var category = Models.Category.CategorySearch.CategoryList.OrderBy(x => x.CategoryID).ToPagedList(page, pageSize); ;
                    //IEnumerable<Models.Category.CategorySearch.CategoryList> category = Models.Category.CategorySearch.CategoryList.OrderBy(x => x.CategoryID).ToPagedList(page, pageSize);
                    //IEnumerable< Models .Category.Category>  category  = Models.Category.CategorySearch.CategoryList.OrderBy(x => x.CategoryID).ToPagedList(page, pageSize);
                    return View(category);
                }
                else
                {
                    Models.Category.CategorySearch.CategoryList = Models.Category.Category.Get_Gategory(type);
                    var result = Models.Category.CategorySearch.CategoryList.OrderBy(x => x.CategoryID).ToPagedList(page, pageSize);
                    return View(result);
                }
            }
            catch (Exception e)
            {
                ViewBag.ResultMessage = e.ToString();
                return View();
            }
        }


        public ActionResult Delete(int type, string categoryID)
        {


            ViewBag.CategoryType0 = "";
            ViewBag.CategoryType1 = "";
            ViewBag.CategoryType2 = "";
            ViewBag.CategoryType3 = "";

            ViewBag.CategoryType0 = (type == 0) ? "active" : "";
            ViewBag.CategoryType1 = (type == 1) ? "active" : "";
            ViewBag.CategoryType2 = (type == 2) ? "active" : "";
            ViewBag.CategoryType3 = (type == 3) ? "active" : "";
            ViewBag.Type = type;

            try
            {
                var result = new Models.Category.Category().Delete_Category(type, categoryID);            
                if (result) 
                {
                    TempData["ResultMessage"] = String.Format("類別[{0}]成功刪除", categoryID);
                    return RedirectToAction("Index", "Category", new { type = type });
                }
                else
                {   //如果沒有資料則顯示錯誤訊息並導回Index頁面
                    TempData["ResultMessage"] = "資料有誤，請重新操作";
                    return RedirectToAction("Index", "Category", new { type = type });
                }
            }
            catch (Exception e)
            {
                ViewBag.ResultMessage = e.ToString();
                return View();
            }

        }

        public ActionResult Create(int type)
        {
            ViewBag.Type = type;
            return View();
        }


        [HttpPost]
        public ActionResult Create(int Type , Models.Category.Category postback)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    if (!new Models.Category.Category().IsCategoryidExist(postback.CategoryID, Type))
                    {
                        ViewBag.ResultMessage = "類別編號已存在";
                        ViewBag.Type = Type;
                        return View(postback);
                    }
                    else
                    {
                        var result = new Models.Category.Category().Post_Category(postback, Type, (string)Session["UserID"]);
                        if (result)
                        {
                            TempData["ResultMessage"] = String.Format("類別[{0}]成功新增", postback.CategoryID);
                            return RedirectToAction("Index", "Category", new { type = Type });
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


        public ActionResult Edit(int Type, string categoryID)
        {
            try
            {
                var result = new Models.Category.Category().Get_Edit_Category(categoryID, Type);
                if (result != default(Models.Category.Category))
                {
                    ViewBag.Type = Type;
                    return View(result);
                }
                else
                {   //如果沒有資料則顯示錯誤訊息並導回Index頁面
                    TempData["resultMessage"] = "資料有誤，請重新操作";
                    return RedirectToAction("Index", "Category", new { type = Type });
                }
            }
            catch (Exception e)
            {
                ViewBag.ResultMessage = e.ToString();
                ViewBag.Type = Type;
                return View();
            }
        }


        [HttpPost]
        public ActionResult Edit(int Type, Models.Category.Category postback)
        {
            try
            {
   
                if (this.ModelState.IsValid)
                {
                    var result = new Models.Category.Category().Patch_Category(postback, Type,(string)Session["UserID"]);
                    if (result)
                    {
                        TempData["ResultMessage"] = String.Format("會員[{0}]成功編輯", postback.CategoryID);
                        return RedirectToAction("Index", "Category", new { type = Type });
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


        [HttpPost]
        public ActionResult HasData()
        {
            JObject jo = new JObject();
            bool result = !Models.Category.CategorySearch.CategoryList.Count.Equals(0);
            jo.Add("Msg", result.ToString());
            return Content(JsonConvert.SerializeObject(jo), "application/json");
        }

        public ActionResult Export()
        {
            var exportSpource = this.GetExportDataWithAllColumns();
            var dt = JsonConvert.DeserializeObject<DataTable>(exportSpource.ToString());

            var exportFileName =
                string.Concat("CategoryData_", DateTime.Now.ToString("yyyyMMddHHmmss"), ".xlsx");

            return new ExportExcelResult
            {
                SheetName = "類別資料",
                FileName = exportFileName,
                ExportData = dt
            };
        }

        private JArray GetExportDataWithAllColumns()
        {
            //var query = db.Customers.OrderBy(x => x.CustomerID);
            var query = Models.Category.CategorySearch.CategoryList.OrderBy(x => x.CategoryID);
            JArray jObjects = new JArray();

            foreach (var item in query)
            {
                var jo = new JObject
                {
                    {"CategoryID", item.CategoryID},
                    {"CategoryName", item.CategoryName},
                };
                jObjects.Add(jo);
            }
            return jObjects;
        }

    }

}
