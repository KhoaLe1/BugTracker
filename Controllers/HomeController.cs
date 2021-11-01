using BugTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;


namespace BugTracker.Controllers
{
    [Authorize]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            List<UserRoleModel> CurrRole = ApplicationRole();
            return View(new SelectList(CurrRole, "RoleID", "UserRole"));
        }
        [HttpPost]
        public IActionResult Index(string UserRole, string RoleNum)
        {
            ViewBag.Message = "User Role is " + UserRole;
            ViewBag.Message += "\\nRole Id is " + RoleNum;
            return View();
        }
        private static List<UserRoleModel> ApplicationRole()
        {
            string constr = @"Data Source=KHOA-PC;Database=BugTracker;integrated security=true";
            //"Server=KHOA-PC;Database=BugTracker;Trusted_Connection=True;MultipleActiveResultSets=true"
            List<UserRoleModel> CurrRole = new List<UserRoleModel>();
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "SELECT UserRole, RoleID FROM ApplicationUser";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            CurrRole.Add(new UserRoleModel
                            {
                                UserRole = sdr["UserRole"].ToString(),
                                RoleID = Convert.ToInt32(sdr["RoleID"])
                            });
                        }
                    }
                    con.Close();
                }
            }
            return CurrRole;
        }
        

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
