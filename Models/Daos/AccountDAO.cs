using BaggageWeb.Models.ModelView;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace BaggageWeb.Models.Daos
{
    public class AccountDAO
    {
        private static AccountDAO _instance = null;
        private AccountDAO() { }
        public static AccountDAO Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AccountDAO();
                }
                return _instance;
            }
        }
        public MemberView ReturnMember(string usr, string pwd)
        {
            try
            {
                Entities.DbEntities en = new Entities.DbEntities();
                var q = en.tbl_member.Where(d => d.C_acc_username.Equals(usr) && d.C_acc_userpass.Equals(pwd))
                    .Select(d => new MemberView
                    {
                        Id = d.C_id,
                        UserName = d.C_acc_username,
                        Password = d.C_acc_userpass,
                        Active = (int)d.C_active,
                        IdAuthentication = (int)d.C_id_auth,

                    }).FirstOrDefault();
                if (q != null)
                {
                    return q;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return new MemberView();
        }
    }
}