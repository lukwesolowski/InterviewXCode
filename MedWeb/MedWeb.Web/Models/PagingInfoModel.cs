using System;

namespace MedWeb.Web.Models
{
    public class PagingInfoModel
    {
        public int TotalItems { get; set; }
        public int ItemsOnPage { get; set; }
        public int CurrentPage { get; set; }

        public int CountTotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsOnPage); }
        }
    }
}