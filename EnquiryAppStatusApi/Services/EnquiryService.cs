using EnquiryAppStatusApi.Models;
using EnquiryAppStatusApi.Models.Dto;
using EnquiryAppStatusApi.Models.ViewModel;

namespace EnquiryAppStatusApi.Services
{
    public class EnquiryService
    {

        public Enquiry MapEnquiryInfoDtoToEntity(EnquiryInfoDto enquiryInfoDto)
        {
            return new Enquiry
            {
                EnquiryId = enquiryInfoDto.EnquiryId,
                PermitId = enquiryInfoDto.PermitId,
                Status = enquiryInfoDto.Status,
                CreatedBy = enquiryInfoDto.CreatedBy,
                DateTimeCreated = DateTime.Now,
                UpdatedBy = enquiryInfoDto.UpdatedBy,
                DateTimeUpdated = DateTime.Now
            };
        }

        public EnquiryProfileViewModel MapEntityToEnquiryProfileViewModel(Enquiry enquiry)
        {
            return new EnquiryProfileViewModel
            {
                Id = enquiry.EnquiryId,
                PermitId = enquiry.PermitId,
                Status = enquiry.Status,
                CreatedBy = enquiry.CreatedBy,
                DateTimeCreated = enquiry.DateTimeCreated,
                UpdatedBy = enquiry.UpdatedBy,
                DateTimeUpdated = enquiry.DateTimeUpdated,
            };
        }
    }
}
