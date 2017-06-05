using System.Collections.Generic;

namespace MedWeb.Web.Models
{
    public class PagingPatientViewModel
    {
        public List<PatientViewModel> PatientViewModel { get; set; }
        public PagingInfoModel PagingInfoModel { get; set; }

        public PagingPatientViewModel(List<PatientViewModel> _PatientViewModel,
            PagingInfoModel _PagingInfoModel)
        {
            PatientViewModel = _PatientViewModel;
            PagingInfoModel = _PagingInfoModel;
        }
    }
}