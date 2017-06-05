using System.Collections.Generic;

namespace MedWeb.Web.Models
{
    public class PagingDoctorViewModel
    {
        public List<DoctorViewModel> DoctorViewModel { get; set; }
        public PagingInfoModel PagingInfoModel { get; set; }

        public PagingDoctorViewModel(List<DoctorViewModel> _DoctorViewModel,
            PagingInfoModel _PagingInfoModel)
        {
            DoctorViewModel = _DoctorViewModel;
            PagingInfoModel = _PagingInfoModel;
        }
    }
}