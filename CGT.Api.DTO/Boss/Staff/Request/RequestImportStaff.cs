namespace CGT.Api.DTO.Boss.Staff
{
    public class RequestImportStaff : RequestBaseModel
    {
        public long UserId { get; set; }

        public long EnterpriseId { get; set; }
    }
}
