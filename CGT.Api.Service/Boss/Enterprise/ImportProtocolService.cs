using CGT.Api.DTO;
using CGT.Api.DTO.Boss.Enterprise;
using CGT.DDD.Config;
using CGT.DDD.Utils.File;
using CGT.DDD.Utils.Http;
using CGT.PetaPoco.Repositories.CgtTravel;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace CGT.Api.Service.Boss.Enterprise {
    /// <summary>
    /// 上传差旅协议
    /// </summary>
    public class ImportProtocolService : ApiBase<RequestImportProtocol> {
        #region 注入仓储
        public EnterpriseTempoaryRep EpRep { get; set; }
        #endregion

        /// <summary>
        /// Api赋值
        /// </summary>
        /// <param name="json"></param>
        public override void SetData(RequestModel json) {
            base.SetData(json);
        }

        private IFormFile file;

        public void SetFileData(IFormFile _file) {
            file = _file;
        }

        /// <summary>
        /// 验证数据
        /// </summary>
        protected override void Validate() {
            base.Validate();
        }

        protected override void ExecuteMethod() {
            string guild = Guid.NewGuid().ToString();
            string ext = file.FileName.Substring(file.FileName.LastIndexOf("."));
            string fileconfig = JsonConfig.JsonRead("ProtocolHardStatic", "CGTExcel");
            string filepath = Path.Combine(Directory.GetCurrentDirectory(), fileconfig);
            string StaticFile = filepath + guild + ext;
            var Current = DDD.Utils.Http.MyHttpContext.Current;
            string WebFile = "http://" + Current.Request.Host.Host + ":" + Current.Request.Host.Port + JsonConfig.JsonRead("ExcelProtocol", "CGTExcel") + guild + ext;
            long filelength = Current.Request.ContentLength != null ? Convert.ToInt64(Current.Request.ContentLength) : 0;
            FileUtily.StreamToFile(file.OpenReadStream(), StaticFile, filelength);
            int reuslt = EpRep.SaveEnterpriseProtocol(Parameter.UserId, Parameter.ID, WebFile);
            if (reuslt > 0) {
                this.Result.Data = WebFile;
            }
            else {
                throw new Exception("文件地址更新失败.SQL异常");
            }
        }
    }
}
