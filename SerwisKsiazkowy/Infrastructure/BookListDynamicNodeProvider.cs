using MvcSiteMapProvider;
using SerwisKsiazkowy.DAL;
using SerwisKsiazkowy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SerwisKsiazkowy.Infrastructure
{
    public class BookListDynamicNodeProvider : DynamicNodeProviderBase
    {
        private BookContext db = new BookContext();
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {
            var value = new List<DynamicNode>();
            foreach (Genre b in db.Genres)
            {
                DynamicNode d = new DynamicNode();
                d.Title = b.Name;
                d.Key = "Genre_" + b.GenreId;
                d.RouteValues.Add("genrename", b.Name.ToLower());
                value.Add(d);
            }
            return value;
        }
    }
}
