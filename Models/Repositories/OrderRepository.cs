using BaggageWeb.Models.Entities;
using BaggageWeb.Models.ModelView;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace BaggageWeb.Models.Repositories
{
    public class OrderRepository : IRepository<OrderView>
    {
        private OrderRepository() { }
        private static OrderRepository _instance = null;
        public static OrderRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new OrderRepository();
                }
                return _instance;
            }
        }
        public HashSet<OrderView> All()
        {
            var result = new HashSet<OrderView>();
            try
            {
                DbEntities en = new DbEntities();
                result = en.tbl_orders.Select(d => new OrderView
                {
                    Id = d.o_id,
                    ProductId = (int)d.pro_id,
                    MemberId = (int)d.m_id,
                    price = (int)d.o_price,
                    CreatedDate = (DateTime)d.o_date,
                    OrderConfirm = (int)d.o_order_confirm,
                    OrderStatus = (int)d.o_status,
                }).ToHashSet();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return result;
        }
        public HashSet<OrderView> AllUserOrder(int memberId)
        {
            var result = new HashSet<OrderView>();
            try
            {
                DbEntities en = new DbEntities();
                result = en.tbl_orders.Where(d => d.m_id == memberId).Select(d => new OrderView
                {
                    Id = d.o_id,
                    MemberId = (int)d.m_id,
                    OrderConfirm = (int)d.o_order_confirm,
                    OrderStatus = (int)d.o_status,
                    ProductId = (int)d.pro_id,
                    CreatedDate = (DateTime)d.o_date,
                    price = (int)d.o_price,
                }).ToHashSet();
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return result;
        }
        public void create(OrderView entity)
        {
            try
            {
                DbEntities en = new DbEntities();
                var item = new tbl_orders
                {
                    m_id = entity.MemberId,
                    o_date = DateTime.Now,
                    o_status = 1,
                    pro_id = entity.ProductId,
                    o_order_confirm = 0,
                    o_price = 0,
                };
                en.tbl_orders.Add(item);
                en.SaveChanges();
                entity.Id = item.o_id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public void ChangeOrderStatus(int OrderId, int status)
        {
            try
            {
                DbEntities en = new DbEntities();
                var item = en.tbl_orders.Where(d => d.o_id == OrderId).FirstOrDefault();
                item.o_order_confirm = status;
                en.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public void ChangePrice(int OrderId, int price)
        {
            try
            {
                DbEntities en = new DbEntities();
                var item = en.tbl_orders.Where(d => d.o_id == OrderId).FirstOrDefault();
                item.o_price = price;
                en.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public int delete(OrderView entity)
        {
            try
            {
                DbEntities en = new DbEntities();
                var item = en.tbl_orders.Where(d=>d.o_id==entity.Id).FirstOrDefault();
                en.tbl_orders.Remove(item);
                en.SaveChanges();
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return 0;
        }

        public OrderView findById(int id)
        {
            OrderView result = new OrderView();
            try
            {
                DbEntities en = new DbEntities();
                result = en.tbl_orders.Where(d => d.o_id == id).Select(d => new OrderView
                {
                    Id = d.o_id,
                    ProductId = (int)d.pro_id,
                    MemberId = (int)d.m_id,
                    price = (int)d.o_price,
                    CreatedDate = (DateTime)d.o_date,
                    OrderConfirm = (int)d.o_order_confirm,
                    OrderStatus = (int)d.o_status,
                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return result;
        }

        public HashSet<OrderView> findByKeyWord(string name)
        {
            throw new NotImplementedException();
        }

        public int update(OrderView entity)
        {
            throw new NotImplementedException();
        }
    }
}