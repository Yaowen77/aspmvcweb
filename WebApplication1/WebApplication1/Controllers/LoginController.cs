using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Login;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Index(Login postback)
        {

            //webapi 登入寫法
            /*try
            {
                if (this.ModelState.IsValid)
                {
                    var user = apis.CallAPI<Result, Login>(postback, "/api/nk/Login");

                    if (user.Code == "S001")
                    {
                        Session["UserID"] = postback.UserID;
                        return RedirectToAction("Index", "Home");
                    }
                    else if (user.Code == "E001")
                    {
                        ModelState.AddModelError("password", "登入失敗，請確認登入資訊。");
                        return View("Index");
                    }
                    else
                    {
                        ViewBag.ResultMessage = "未知錯誤";
                        return View("Index");
                    }
                }
                else
                {
                    // return RedirectToAction("Index", "Login");
                    return View("Index");
                }
            }
            catch (Exception e)
            {
                //ViewBag.ResultMessage = "連線失敗，請確認網路是否正常。";
                ViewBag.ResultMessage = e.ToString();
                return View();
            }*/

            try
            {
                if (this.ModelState.IsValid)
                {
                    var receivePassword = Login.Encoded(postback.UserPwd + postback.UserID);
                    if (await Login.LoginAccountAsync(postback.UserID, receivePassword))
                    {
                        Session["UserID"] = postback.UserID;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("password", "登入失敗，請確認登入資訊。");
                        return View("Index");
                    }
                }
                else
                {
                    // return RedirectToAction("Index", "Login");
                    return View("Index");
                }
            }
            catch (Exception e)
            {
                //ViewBag.ResultMessage = "連線失敗，請確認網路是否正常。";
                ViewBag.ResultMessage = e.ToString();
                return View();
            }



        }

        public ActionResult Logout()
        {
            Session.Clear();
            return View("Index");
        }
    }
}