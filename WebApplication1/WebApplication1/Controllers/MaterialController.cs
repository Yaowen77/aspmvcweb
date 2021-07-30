using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Material;

namespace WebApplication1.Controllers
{
    public class MaterialController : Controller
    {
        // GET: Material
        public ActionResult Index(int type, string id, int page = 1, int pageSize = 15)
        {
            if ((string)Session["UserID"] == null)
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
                    if (type == 0)
                    {
                        MaterialSearch.MaterialList = new Material().Get_Material(SearchString);
                        var material = MaterialSearch.MaterialList.OrderBy(x => x.MaterialID).ToPagedList(page, pageSize);
                        return View(material);
                    }
                    else
                    {
                        MaterialSearch.MaterialList = new Material().Get_StoreMaterial(SearchString);
                        var storeMaterial = MaterialSearch.MaterialList.OrderBy(x => x.StoreID).ThenBy(n => n.MaterialID).ToPagedList(page, pageSize);
                        return View(storeMaterial);
                    }
                }
                else
                {
                    if (type == 0)
                    {
                        MaterialSearch.MaterialList = new Material().Get_Material();
                        var material = MaterialSearch.MaterialList.OrderBy(x => x.MaterialID).ToPagedList(page, pageSize);
                        return View(material);
                    }
                    else
                    {
                        MaterialSearch.MaterialList = new Material().Get_StoreMaterial();
                        var storeMaterial = MaterialSearch.MaterialList.OrderBy(x => x.StoreID).ThenBy(n => n.MaterialID).ToPagedList(page, pageSize);
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
            var StoreStorge = new StoreStorge().Get_Storage(materialID);
            return PartialView(StoreStorge);
        }

        // GET: Material/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Material/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Material/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Material/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Material/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Delete(int type ,string materialID, string storeId)
        {


            ViewBag.MaterialType0 = "";
            ViewBag.MaterialType1 = "";

            ViewBag.MaterialType0 = (type == 0) ? "active" : "";
            ViewBag.MaterialType1 = (type == 1) ? "active" : "";
            ViewBag.Type = type;

            try
            {
                 var result = new Material().Delete_Material(type, materialID, storeId);
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
    }
}
