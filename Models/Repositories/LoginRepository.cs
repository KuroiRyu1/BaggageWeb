using BaggageWeb.Models.Daos;
using BaggageWeb.Models.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaggageWeb.Models.Repositories
{
    public class LoginRepository
    {
        private static LoginRepository _instance = null;
        private LoginRepository() { }
        public static LoginRepository Instance
        {
            get {
                if (_instance == null)
                {
                    _instance = new LoginRepository();
                }
                return _instance;
            }
        }
        public MemberView checkLogin(string username, string password)
        {
            return AccountDAO.Instance.ReturnMember(username, password);
        }
        public bool GetAuthorization(int id_authen,string absoluteUrl)
        {
            return AuthorizationDAO.Instance.GetAuthorizationMember(id_authen, absoluteUrl);
        }
    }
}