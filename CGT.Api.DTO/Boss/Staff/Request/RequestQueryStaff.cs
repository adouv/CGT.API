namespace CGT.Api.DTO.Boss.Staff
{
    public class RequestQueryStaff : RequestBaseModel
    {
        public string PayCenterCode { get; set; }

        public long? EnterpriseId { get; set; }

        public string StaffName { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
