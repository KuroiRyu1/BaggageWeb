using BaggageWeb.Models.Entities;
using BaggageWeb.Models.ModelView;
using BaggageWeb.Models.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace BaggageWeb.Models.Repositories
{
    public class MemberRepository : IRepository<MemberView>
    {
        private MemberRepository() { }
        private static MemberRepository _instance = null;
        public static MemberRepository Instance {  
            get {
                if (_instance == null)
                {
                    _instance = new MemberRepository();
                }
                return _instance;
            } 
        }
        public bool checkSameUserName(string userName)
        {
            try
            {
                DbEntities en = new DbEntities();
                var rs = en.tbl_member.Any(d => d.C_acc_username == userName);
                return rs;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return false;
        }
        public string SignUp(MemberView entity)
        {
            try
            {
                DbEntities en = new DbEntities();
                var item = new tbl_member
                {
                    C_acc_username = entity.UserName,
                    C_acc_userpass = entity.Password,
                    C_email = entity.Email,
                    C_active = 1,
                    C_id_auth = 2,
                };
                if (!checkSameUserName(entity.UserName))
                {
                    en.tbl_member.Add(item);
                    en.SaveChanges();
                    entity.Id = item.C_id;
                }
                else
                {
                    return StringValue.Message_Create_Account_Failed;
                }
            }
            catch (Exception ex)
            {
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            return StringValue.Message_Create_Account_Success;
        }

        public void create(MemberView entity)
        {
            throw new NotImplementedException();
        }

        public int update(MemberView entity)
        {
            throw new NotImplementedException();
        }

        public int delete(MemberView entity)
        {
            throw new NotImplementedException();
        }

        public HashSet<MemberView> All()
        {
            throw new NotImplementedException();
        }

        public MemberView findById(int id)
        {
            try
            {
                DbEntities en = new DbEntities();
                var item = en.tbl_member.Where(d=> d.C_id == id).Select(d => new MemberView
                {
                    Id = (int)d.C_id,
                    Email = d.C_email,
                    UserName = d.C_acc_username,
                    Password = d.C_acc_userpass,
                    IdAuthentication = (int)d.C_id_auth,
                    Active = (int)d.C_active,
                }).FirstOrDefault();
                return item;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return new MemberView();
        }

        public HashSet<MemberView> findByKeyWord(string name)
        {
            throw new NotImplementedException();
        }
        public void ChangeEmail(string email,int id)
        {
            try
            {
                DbEntities en = new DbEntities();
                var item = en.tbl_member.Where(d=>d.C_id==id).FirstOrDefault();
                item.C_email = email;
                en.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}