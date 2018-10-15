using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CareerCloud.MVC.Controllers
{
    public class SecurityLogInController : Controller
    {
        private readonly SecurityLoginLogic _loginLogic;
        private readonly SecurityRoleLogic _roleLogic;
        private readonly SecurityLoginsRoleLogic _loginRoleLogic;

        public SecurityLogInController()
        {
            var loginRepo = new EFGenericRepository<SecurityLoginPoco>(false);
            var roleRepo = new EFGenericRepository<SecurityRolePoco>(false);
            var loginRoleRepo = new EFGenericRepository<SecurityLoginsRolePoco>(false);
            _loginLogic = new SecurityLoginLogic(loginRepo);
            _roleLogic = new SecurityRoleLogic(roleRepo);
            _loginRoleLogic = new SecurityLoginsRoleLogic(loginRoleRepo);
        }
        public SecurityLogInController(IDataRepository<SecurityLoginPoco> repo)
        {
            _loginLogic = new SecurityLoginLogic(repo);
        }

        // GET: SecurityLogIn
        public ActionResult Index()
        {
            return View();
        }
        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Login,Password")] SecurityLoginPoco security_Logins)
        {
//            if (ModelState.IsValid)
//            {
                if (_loginLogic.Authenticate(security_Logins.Login, security_Logins.Password))
                {

                    //string _userRole = _roleLogic.Get(_loginRoleLogic.Get())
                    string VisitorRole = null;
                    object UserID = null;

                    string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
                    SqlConnection conn = new SqlConnection(connectionString);
                    using (conn)
                    {
                        SqlCommand cmd = new SqlCommand { Connection = conn };
                        conn.Open();

                        cmd.CommandText = @"select A.Id, C.Role from (dbo.Security_Logins AS A Join dbo.Security_Logins_Roles As B ON A.Id = B.Login) Join dbo.Security_Roles AS C ON B.Role = C.Id WHERE A.Login =  @Login";
                        cmd.Parameters.AddWithValue("@Login", security_Logins.Login);
                        try
                        {
                           SqlDataReader IDreader = cmd.ExecuteReader();
                            while (IDreader.Read())
                            {
                               IDataRecord myreader = (IDataRecord)IDreader;
                               VisitorRole = myreader[1].ToString().Trim();
                               UserID = (Guid)myreader[0];
                            }
                            IDreader.Close();
                            conn.Close();
                            
                            switch (VisitorRole)
                            {
                                case "Recruiters":
                                    Session["UserId"] = (Guid)UserID;
                                    return RedirectToAction("Index", "CompanyDashboard");
                                case "Applicants":
                                    Session["UserId"] = UserID;
                                    return RedirectToAction("Index", "ApplicantDashboard");
                                case "Administrators":
                                    Session["UserId"] = UserID;
                                    return RedirectToAction("Index", "Admin");
                                default:
                                    return RedirectToAction("Index", "Home");
                            }

                        }
                        catch {}
                        finally { conn.Close(); }                        
                    }
                        conn.Close();
                }
//            }
            return RedirectToAction("Index", "Home");
        }
    }
}