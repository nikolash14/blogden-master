using MindfireSolutions.Custom;
using MindfireSolutions.DataAccess;
using MindfireSolutions.Models;
using MindfireSolutions.Service.ServiceInterface;
using MindfireSolutions.ViewModel;
using System;
using System.IO;
using System.Linq;
using System.Web;

namespace MindfireSolutions.Service.ServiceClass
{
    public class ManageUser : IManageUser
    {
        DAL dbReference = new DAL();
        private ICustomHelper _customHelper;
        string filePath = "";
        string title = "";
        public ManageUser(CustomHelper customHelper)
        {
            this._customHelper = customHelper;
        }
        public string Create(VMUser userDetails)
        {
            //if (dbReference.Users.Where(m => m.Email == userDetails.Email).SingleOrDefault()== null)
            if (dbReference.Users.SingleOrDefault(m => m.Email == userDetails.Email) == null)
            {

                if (userDetails.ImageUpload != null)
                {
                    string filename = Path.GetFileName(userDetails.ImageUpload.FileName);
                    filePath = Path.Combine("\\Images\\ProfileImages\\", _customHelper.HashValue(filename.ToString() + DateTime.Now) + Path.GetExtension(filename));
                    title = _customHelper.HashValue(filename.ToString() + DateTime.Now) + Path.GetExtension(filename);
                    string directoryPath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/ProfileImages"), title);
                    userDetails.ImageUpload.SaveAs(directoryPath);
                }
                else
                {
                    filePath = "\\Images\\ProfileImages\\default.png";
                    title = "default.png";
                }
                var _user = new User()
                {
                    Email = userDetails.Email,
                    Name = userDetails.Name,
                    Password = _customHelper.HashValue(userDetails.Password),
                    Mobile = userDetails.Mobile,
                    CreationTime = DateTime.Now,
                    ImageUrl = filePath,
                    DateOfBirth = DateTime.Now,
                    Rank=0
                };
                dbReference.Users.Add(_user);
                dbReference.SaveChanges();
                return userDetails.Email;
            }

            return null;
        }
    }
}