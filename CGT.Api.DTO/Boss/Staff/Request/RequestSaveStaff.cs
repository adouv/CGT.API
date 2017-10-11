namespace CGT.Api.DTO.Boss.Staff
{
    public class RequestSaveStaff : RequestBaseModel
    {
        public long ID { get; set; }

        public string StaffName { get; set; }

        public string IdentificationNumber { get; set; }

        public byte? DocumentType { get; set; }

        public string PhoneNumber { get; set; }

        public string SubsidiaryDepartment { get; set; }

        public string JobPosition { get; set; }

        public byte? IsJob { get; set; }
    }
}
