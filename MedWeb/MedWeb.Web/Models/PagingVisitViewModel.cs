using System.Collections.Generic;

namespace MedWeb.Web.Models
{
    public class PagingVisitViewModel
    {
        public List<RegisteredVisitViewModel> RegisteredVisits { get; set; }
        public PagingInfoModel PagingInfoModel { get; set; }
    }
}