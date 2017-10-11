namespace CGT.Api.DTO.Boss.User.Request {
    /// <summary>
    ///代理列表请求实体
    /// </summary>
    public class RequestGetInterfaceAccountList : RequestBaseModel {
         public int PageIndex { get; set; }

         public int PageSize { get; set; }
    }
}
