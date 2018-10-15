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
    public class ApplicantSignUpController : Controller
    {
        private readonly SecurityLoginLogic _loginLogic;
        private readonly SecurityRoleLogic _roleLogic;
        private readonly SecurityLoginsRoleLogic _loginRoleLogic;

        public ApplicantSignUpController()
        {
            var loginRepo = new EFGenericRepository<SecurityLoginPoco>(false);
            var roleRepo = new EFGenericRepository<SecurityRolePoco>(false);
            var loginRoleRepo = new EFGenericRepository<SecurityLoginsRolePoco>(false);
            _loginLogic = new SecurityLoginLogic(loginRepo);
            _roleLogic = new SecurityRoleLogic(roleRepo);
            _loginRoleLogic = new SecurityLoginsRoleLogic(loginRoleRepo);
        }
        public ApplicantSignUpController(IDataRepository<SecurityLoginPoco> repo)
        {
            _loginLogic = new SecurityLoginLogic(repo);
        }
        // GET: ApplicantSignUp
        public ActionResult Index()
        {
            return View();
        }

        // POST: SecurityLogin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Id,Login,Password,Created,PasswordUpdate,AgreementAccepted,IsLocked,IsInactive,EmailAddress,PhoneNumber,FullName,ForceChangePassword,PrefferredLanguage,TimeStamp")] SecurityLoginPoco security_Logins)
        {
            if (ModelState.IsValid)
            {
                object _userRole = null;
                security_Logins.Id = Guid.NewGuid();
                security_Logins.Created = DateTime.Now;
                security_Logins.AgreementAccepted = DateTime.Now;
                security_Logins.ForceChangePassword = false;
                security_Logins.IsInactive = false;
                security_Logins.IsLocked = false;
                _loginLogic.Add(new SecurityLoginPoco[] { security_Logins });
                Session["UserID"] = security_Logins.Id;

                string connectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
                SqlConnection conn = new SqlConnection(connectionString);
                using (conn)
                {
                    SqlCommand cmd = new SqlCommand { Connection = conn };
                    conn.Open();
                   if (Session["UserId"] != null)
                    {
                        cmd.CommandText = @"Select [Id] FROM dbo.Security_Roles AS A WHERE A.Role = 'Applicants'";
                        SqlDataReader rolereader = cmd.ExecuteReader();
                        while (rolereader.Read())
                        {
                            IDataRecord myreader = (IDataRecord)rolereader;
                            _userRole = (Guid)myreader[0];
                        }
                        rolereader.Close();

                        cmd.CommandText = @"INSERT INTO dbo.Applicant_Profiles (Id, Login) 
                         VALUES   (@Id, @RLogin);";
                        cmd.Parameters.AddWithValue("@Id", Guid.NewGuid());
                        cmd.Parameters.AddWithValue("@RLogin", security_Logins.Id);

                        int EffectedRows = cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                SecurityLoginsRolePoco securityLoginsRolePoco = new SecurityLoginsRolePoco
                {
                    Id = Guid.NewGuid(),
                    Login = (Guid)Session["UserID"],
                    Role = (Guid)_userRole
                };
                _loginRoleLogic.Add(new SecurityLoginsRolePoco[] { securityLoginsRolePoco });


                return RedirectToAction("Index","ApplicantDashboard");
            }

            return View();
        }
    }
}