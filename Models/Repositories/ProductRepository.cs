﻿using BaggageWeb.Models.Entities;
using BaggageWeb.Models.ModelView;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace BaggageWeb.Models.Repositories
{
    public sealed class ProductRepository : IRepository<ProductView>
    {
        private ProductRepository() { }
        private static ProductRepository _instance = null;
        public static ProductRepository Instance {  
            get { 
                if (_instance == null)
                {
                    _instance = new ProductRepository();
                }
                return _instance;
            } }
        public HashSet<ProductView> All()
        {
            try
            {
                DbEntities en = new DbEntities();
                var item = en.tbl_products.Where(d=>d.pro_active==1).Select(d=> new ProductView
                {
                    Id = d.pro_id,
                    Name = d.pro_name,
                    Description = d.pro_des,
                    Active = (int)d.pro_active,
                    ImageName = d.pro_image,
                }).ToHashSet();
                return item;
            }
            catch (Exception ex)
            {
            }
            return new HashSet<ProductView>();
        }
        public bool CheckSameName(ProductView productView)
        {
            try
            {
                DbEntities en = new DbEntities();
                var rs = en.tbl_products.Any(d=>d.pro_name==productView.Name);
                return rs;
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
            return false;
        }
     

        public bool create(ProductView entity)
        {
            try
            {
                DbEntities en = new DbEntities();
                tbl_products view = new tbl_products
                {
                    pro_name = entity.Name,
                    pro_des = entity.Description,
                    pro_active = entity.Active,
                    pro_image = entity.ImageName,
                };
                if (!CheckSameName(entity))
                {
                    en.tbl_products.Add(view);
                    en.SaveChanges();
                    entity.Id = view.pro_id;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return false ;
        }

        public int delete(ProductView entity)
        {
            int result = 0;
            try
            {
                DbEntities en = new DbEntities();
                var item = en.tbl_products.Any(p=> (int)p.pro_id == entity.Id);
                if (item)
                {
                    result = 1;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return result;
        }

        public ProductView findById(int id)
        {
            try
            {
                DbEntities en = new DbEntities();
                var item = en.tbl_products.Where(d=>d.pro_id==id).Select(d=>new ProductView
                {
                    Id = d.pro_id,
                    Name = d.pro_name,
                    Description = d.pro_des,
                    Active = (int)d.pro_active,
                }).FirstOrDefault();
                return item;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new ProductView();
        }
        public void ChangeProductStatus(int productId, int status)
        {
            try
            {
                DbEntities en = new DbEntities();
                var item = en.tbl_products.Where(d => d.pro_id == productId).FirstOrDefault();
                item.pro_active = status;
                en.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public HashSet<ProductView> findByKeyWord(string name)
        {
            try
            {
                DbEntities en = new DbEntities();
                var item = en.tbl_products.Where(d => d.pro_name.ToLower().Contains(name.ToLower())||d.pro_name.ToLower().Equals(name.ToLower())).Select(d=> new ProductView
                {
                    Id = d.pro_id,
                    Name = d.pro_name,
                    Description = d.pro_des,
                    Active = (int)d.pro_active,
                    ImageName = d.pro_image,
                }).ToHashSet();
                return item;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new HashSet<ProductView>();
        }

        public int update(ProductView entity)
        {
            try
            {
                DbEntities en = new DbEntities();
                var item = en.tbl_products.Where(d=> d.pro_id == entity.Id).FirstOrDefault();
                item.pro_name = entity.Name;
                item.pro_active = entity.Active;
                item.pro_des = entity.Description;
                item.pro_image = entity.ImageName;
                en.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                
            }
            return 0;
        }

        void IRepository<ProductView>.create(ProductView entity)
        {
            throw new NotImplementedException();
        }
    }
}