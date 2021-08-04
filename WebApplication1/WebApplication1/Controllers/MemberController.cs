using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Infrastructure.MemberResults;
using WebApplication1.Models.Member;
using Microsoft.Reporting.WebForms;
using MySql.Data.MySqlClient;

namespace WebApplication1.Controllers
{

    public class MemberController : Controller
    {
        // GET: Member

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
                    MemberSearch.MemberList = Member.Get_Member(SearchString);
                    var member = MemberSearch.MemberList.OrderBy(x => x.MemberID).ToPagedList(page, pageSize); 
                    return View(member);
                }
                else
                {
                    MemberSearch.MemberList = Member.Get_Member();
                    var result = MemberSearch.MemberList.OrderBy(x => x.MemberID).ToPagedList(page, pageSize);
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
                var result = new Member().Get_Edit_Member(id);
                if (result != default(Member))
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
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Member postback, HttpPostedFileBase file)
        {
            try
            {
                byte[] FileBytes = null;
                if (file != null)
                {

                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        FileBytes = ms.GetBuffer();
                    }


                    if (file.ContentLength > 2048000)
                    {
                        ViewBag.ResultMessage = "檔案大小不可超過2M";
                        return View(postback);
                    }

                    if (file.FileName.Length > 50)
                    {
                        ViewBag.ResultMessage = "檔案名稱太長";
                        return View(postback);
                    }


                    //檔案是否為圖片
                    if (!Member.IsPicture(file.FileName))
                    {
                        ViewBag.ResultMessage = "檔案不為圖片";
                        return View(postback);
                    }

                    //檔案是否為圖片
                    if (Member.IsImage(file) == null)
                    {
                        ViewBag.ResultMessage = "檔案不為圖片";
                        return View(postback);
                    }
                }

                if (this.ModelState.IsValid)
                {
                    var result = new Member().Patch_Member(postback, file, FileBytes, (string)Session["UserID"]);
                    if (result)
                    {
                        TempData["ResultMessage"] = String.Format("會員[{0}]成功編輯", postback.MemberID);
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
                var result = new Member().Delete_Member(id);
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


        public ActionResult Report()
        {
            ReportViewer reportViewer = new ReportViewer
            {
                ProcessingMode = ProcessingMode.Local,
                AsyncRendering = false,
                SizeToReportContent = true,
                ZoomMode = ZoomMode.FullPage,
                ShowPrintButton = true //顯示列印鈕
                
            };
            reportViewer.LocalReport.EnableExternalImages = true;
            reportViewer.LocalReport.ReportPath = $"{Request.MapPath(Request.ApplicationPath)}Report\\Report1.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", MemberSearch.MemberList));
            reportViewer.LocalReport.Refresh();

            ViewBag.ReportViewer = reportViewer;
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HasData()
        {
            JObject jo = new JObject();
            bool result = !MemberSearch.MemberList.Count.Equals(0);
            jo.Add("Msg", result.ToString());
            return Content(JsonConvert.SerializeObject(jo), "application/json");
        }

        public ActionResult Export()
        {
            var exportSpource = this.GetExportDataWithAllColumns();
            var dt = JsonConvert.DeserializeObject<DataTable>(exportSpource.ToString());

            var exportFileName =
                string.Concat("MemberData_", DateTime.Now.ToString("yyyyMMddHHmmss"), ".xlsx");

            return new ExportExcelResult
            {
                SheetName = "會員資料",
                FileName = exportFileName,
                ExportData = dt
            };
        }

        private JArray GetExportDataWithAllColumns()
        {
            //var query = db.Customers.OrderBy(x => x.CustomerID);
            var query = MemberSearch.MemberList.OrderBy(x => x.MemberID);
            JArray jObjects = new JArray();

            foreach (var item in query)
            {
                var jo = new JObject
                {
                    {"MemberID", item.MemberID},
                    {"MemberName", item.MemberName},
                    {"MobilPhone", item.MobilPhone},
                    {"EMail", item.EMail},
                };
                jObjects.Add(jo);
            }
            return jObjects;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Member postback, HttpPostedFileBase file)
        {

            try
            {
                byte[] FileBytes = null;

                if (file != null)
                {

                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        FileBytes = ms.GetBuffer();
                    }


                    if (file.ContentLength > 2048000)
                    {
                        ViewBag.ResultMessage = "檔案大小不可超過2M";
                        return View(postback);
                    }


                    if (file.FileName.Length > 50)
                    {
                        ViewBag.ResultMessage = "檔案名稱太長";
                        return View(postback);
                    }


                    //檔案是否為圖片
                    if (!Member.IsPicture(file.FileName))
                    {
                        ViewBag.ResultMessage = "檔案不為圖片";
                        return View(postback);
                    }

                    //檔案是否為圖片
                    if (Member.IsImage(file) == null)
                    {
                        ViewBag.ResultMessage = "檔案不為圖片";
                        return View(postback);
                    }

                }

                if (this.ModelState.IsValid)
                {
                    if (!new Member().IsMemberidExist(postback.MemberID))
                    {
                        ViewBag.ResultMessage = "會員編號已存在";
                        return View(postback);
                    }
                    else
                    {
                        var result = new Member().Post_Member(postback, file, FileBytes, (string)Session["UserID"]);
                        if (result)
                        {
                            TempData["ResultMessage"] = String.Format("會員[{0}]成功新增", postback.MemberID);
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