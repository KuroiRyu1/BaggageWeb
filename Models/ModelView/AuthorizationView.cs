using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaggageWeb.Models.ModelView
{
    public class AuthorizationView
    {
        public int Id { get; set; }
        public string Title {  get; set; }
        public string Url {  get; set; }
        public int IdAuthen { get; set; }
        public int Active {  get; set; }
    }
}