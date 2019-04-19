using MvcSiteMapProvider;
using SerwisKsiazkowy.DAL;
using SerwisKsiazkowy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SerwisKsiazkowy.Infrastructure
{
    public class BookDetailsDynamicNodeProvider : DynamicNodeProviderBase
    {
        private BookContext db = new BookContext();
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {
            var value = new List<DynamicNode>();
            foreach(Book b in db.Books)
            {
                DynamicNode d = new DynamicNode();
                d.Title = b.Title;
                d.Key = "Book_" + b.BookId;
                d.ParentKey = "Genre_" + b.GenreId;
                d.RouteValues.Add("id", b.BookId);
                d.RouteValues.Add("_title", b.Title.Replace(" ", "-").ToLower().ToString());

                value.Add(d);
            }
            return value;
        }
    }
}