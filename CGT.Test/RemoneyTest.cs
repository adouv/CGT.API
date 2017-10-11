using CGT.Api.Service.Manage.Remoney;
using CGT.Event.Model.Manage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Test {
    /// <summary>
    /// 返现测试
    /// </summary>
    [TestClass]
    public class RemoneyTest {
        [TestMethod]
        public void TestBatchRemoney() {
            var BaseRiskModellist = new List<BaseRiskModel>();
            var baseRiskModel = new BaseRiskModel() {
                ArriveCode = "SHA",
                Cabin = "Y",
                DepartCode = "PEK",
                DepartureTime = DateTime.Now,
                EnterpriseID = 1089,
                EnterpriseName = "新测试企业五",
                EtermType = 0,
                EtermStatus = 1,
                EtermSuccessRate = 0,
                FlightNo = "CA1234",
                OrderAmount = 100,
                TicketNum = "9995783618889",
                PayCenterCode = "CSSX41080209",
                PayCenterName = "苏测试分销一",
                PersonName = "朱明",
                TicketPrice = 1200,
                TicketTime = DateTime.Now,
                TravelBatchId = "636403794183510244",
                TravelRiskType = 1,
                PNR = "QWERTY",
                WhiteFailRate = 0,
                WhiteSuccessRate = 0
            };
            BaseRiskModellist.Add(baseRiskModel);
            var ManageRisk = new ManageRiskModel() {
                AccountPeriod = "61",
                BackReapalNo = "100000000001678",
                EnterpriseID = 1089,
                TravelBatchId = "636403794183510244",
                FactoringEmail = "fukh@reapal.com",
                EnterpriseName = "新测试企业五",
                FactoringInterestRate = 0.018m,
                FactoringName = "测试保理",
                FactoringReapalNo = "100000000001024",
                InterestRate = 0,
                PayCenterCode = "CSSX41080209",
                PayCenterName = "苏测试分销一",
                UserFactoringId = 1,
                UserName = "3224132821@qq.com",
                baseRiskModelList = BaseRiskModellist
            };
            new RemoneyService(ManageRisk).Execute();
        }
    }
}
