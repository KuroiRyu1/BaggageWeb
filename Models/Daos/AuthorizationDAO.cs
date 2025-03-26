using BaggageWeb.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaggageWeb.Models.Daos
{
    public class AuthorizationDAO
    {
        private static AuthorizationDAO instance=null;
        private AuthorizationDAO() { }
        public static AuthorizationDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AuthorizationDAO();
                }
                return instance;
            }
        }
        public bool GetAuthorizationMember(int id_authen,string ablsoluteUrl)
        {
            try
            {
                var en = new DbEntities();
                var res = en.tbl_authorization.Any(d=>d.C_id_authen== id_authen&& ablsoluteUrl.ToLower().Contains(d.C_url.ToLower()));
                return res;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }
}