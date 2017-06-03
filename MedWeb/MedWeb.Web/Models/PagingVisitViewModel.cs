using System.Collections.Generic;

namespace MedWeb.Web.Models
{
    public class PagingVisitViewModel
    {
        public List<RegisteredVisitViewModel> RegisteredVisitsModel { get; set; }
        public PagingInfoModel PagingInfoModel { get; set; }

        public PagingVisitViewModel(List<RegisteredVisitViewModel> _RegisteredVisitsModel,
            PagingInfoModel _PagingInfoModel)
        {
            RegisteredVisitsModel = _RegisteredVisitsModel;
            PagingInfoModel = _PagingInfoModel;
        }
    }
}