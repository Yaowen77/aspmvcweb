using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.Models.Login;
using NKLibrary;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Index2()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index2(Login postback)
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
                    NKDLL NKDLL = new NKDLL();
                    var receivePassword = NKDLL.Encoded(postback.UserPwd + postback.UserID);
                    if (await Login.LoginAccountAsync(postback.UserID, receivePassword))
                    {
                        Session["UserID"] = postback.UserID;
                        DateTime DTnow = DateTime.Now;

                        // 以下需要搭配 System.Web.Security 命名空間。                
                        var authTicket = new FormsAuthenticationTicket(   // 登入成功，取得門票 (票證)。請自行填寫以下資訊。
                            version: 1,   //版本號（Ver.）
                            name: postback.UserID, // ***自行放入資料（如：使用者帳號、真實名稱）

                            issueDate: DTnow,  // 登入成功後，核發此票證的本機日期和時間（資料格式 DateTime）
                            expiration: DTnow.AddDays(1),  //  "一天"內都有效（票證到期的本機日期和時間。）
                            isPersistent: true,  // 記住我？ true or false（畫面上通常會用 CheckBox表示）

                            userData: "1",   // ***自行放入資料（如：會員權限、等級、群組） 
                                             // 與票證一起存放的使用者特定資料。
                                             // 需搭配 Global.asax設定檔 - Application_AuthenticateRequest事件。

                            cookiePath: FormsAuthentication.FormsCookiePath
                            );


                        //                                                                                                                        // *** 把上面的 ticket資訊 "加密"  ****** 
                        var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket))
                        {   // 重點！！避免 Cookie遭受攻擊、盜用或不當存取。請查詢關鍵字「」。
                            HttpOnly = true  // 必須上網透過http才可以存取Cookie。不允許用戶端（寫前端程式）存取

                            //HttpOnly = true,  // 必須上網透過http才可以存取Cookie。不允許用戶端（寫前端程式）存取
                            //Secure = true;      // 需要搭配https（SSL）才行。
                        };


                        if (authTicket.IsPersistent)
                        {
                            authCookie.Expires = authTicket.Expiration;    // Cookie過期日（票證到期的本機日期和時間。）
                        }
                        Response.Cookies.Add(authCookie);   // 完成 Cookie，寫入使用者的瀏覽器與設備中

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("password", "登入失敗，請確認登入資訊。");
                        return View("Login");
                    }
                }
                else
                {
                    // return RedirectToAction("Index", "Login");
                    return View("Login");
                }
            }
            catch (Exception e)
            {
                //ViewBag.ResultMessage = "連線失敗，請確認網路是否正常。";
                ViewBag.ResultMessage = e.ToString();
                return View();
            }



        }


        [HttpPost]
        [ValidateAntiForgeryToken]
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
                    NKDLL NKDLL = new NKDLL();
                    var receivePassword = NKDLL.Encoded(postback.UserPwd + postback.UserID);
                    if (await Login.LoginAccountAsync(postback.UserID, receivePassword))
                    {
                        Session["UserID"] = postback.UserID;
                        DateTime DTnow = DateTime.Now;

                        // 以下需要搭配 System.Web.Security 命名空間。                
                        var authTicket = new FormsAuthenticationTicket(   // 登入成功，取得門票 (票證)。請自行填寫以下資訊。
                            version: 1,   //版本號（Ver.）
                            name: postback.UserID, // ***自行放入資料（如：使用者帳號、真實名稱）

                            issueDate: DTnow,  // 登入成功後，核發此票證的本機日期和時間（資料格式 DateTime）
                            expiration: DTnow.AddDays(1),  //  "一天"內都有效（票證到期的本機日期和時間。）
                            isPersistent: true,  // 記住我？ true or false（畫面上通常會用 CheckBox表示）

                            userData: "1",   // ***自行放入資料（如：會員權限、等級、群組） 
                                                                     // 與票證一起存放的使用者特定資料。
                                                                     // 需搭配 Global.asax設定檔 - Application_AuthenticateRequest事件。

                            cookiePath: FormsAuthentication.FormsCookiePath
                            );


                        //                                                                                                                        // *** 把上面的 ticket資訊 "加密"  ****** 
                        var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket))
                        {   // 重點！！避免 Cookie遭受攻擊、盜用或不當存取。請查詢關鍵字「」。
                            HttpOnly = true  // 必須上網透過http才可以存取Cookie。不允許用戶端（寫前端程式）存取

                            //HttpOnly = true,  // 必須上網透過http才可以存取Cookie。不允許用戶端（寫前端程式）存取
                            //Secure = true;      // 需要搭配https（SSL）才行。
                        };


                        if (authTicket.IsPersistent)
                        {
                            authCookie.Expires = authTicket.Expiration;    // Cookie過期日（票證到期的本機日期和時間。）
                        }
                        Response.Cookies.Add(authCookie);   // 完成 Cookie，寫入使用者的瀏覽器與設備中
                                                            
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
            FormsAuthentication.SignOut();  // 登出
            Session.Abandon();
            return View("Index");
        }
    }
}