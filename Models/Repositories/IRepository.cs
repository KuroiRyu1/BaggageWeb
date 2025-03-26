using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaggageWeb.Models.Repositories
{
        internal interface IRepository<T> where T : class
        {
            void create(T entity);
            int update(T entity);
            int delete(T entity);
            HashSet<T> All();
            T findById(int id);
            HashSet<T> findByKeyWord(string name);
        }
}