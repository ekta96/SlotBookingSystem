using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SlotBookingSystem.Models;
using SlotBookingSystem.Helper;
using System.Data;
using NpgsqlTypes;
using System.Threading.Tasks;

namespace SlotBookingSystem.Controllers
{
    public class UserController : ApiController
    {
        [HttpGet]
        public List<User> GetAllUser()
        {
            using (Postgres dbObj = new Postgres())
            {
                String sql = "SELECT * FROM slotBooking.tbl_user";
                DataTable dataTable = dbObj.ExecuteReader(sql);
                List<User> userList = new List<User>();
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        User userData = new User();
                        userData.UserID = Convert.ToString(row["user_id"]);
                        userData.FirstName = Convert.ToString(row["first_name"]);
                        userData.LastName = Convert.ToString(row["last_name"]);
                        userData.EmailID = Convert.ToString(row["email_id"]);
                        userData.Password = Convert.ToString(row["password"]);
                        userList.Add(userData);
                    }
                }
                return userList;
            }
        }

        [HttpGet]
        public User GetUserById(string userID)
        {
            using (Postgres dbObj = new Postgres())
            {
                string sql = "SELECT * FROM slotBooking.tbl_user where user_id = @UserId";
                dbObj.AddParameter("@UserId", NpgsqlDbType.Varchar, userID);
                DataTable dataTable = dbObj.ExecuteReader(sql);
                User userData = new User();
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        userData.UserID = Convert.ToString(row["user_id"]);
                        userData.FirstName = Convert.ToString(row["first_name"]);
                        userData.LastName = Convert.ToString(row["last_name"]);
                        userData.EmailID = Convert.ToString(row["email_id"]);
                        userData.Password = Convert.ToString(row["password"]);
                    }
                }
                return userData;
            }
        }
       
    }
}
