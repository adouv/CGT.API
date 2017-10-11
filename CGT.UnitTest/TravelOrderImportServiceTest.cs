using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CGT.Api.Service.Boss.TravelOrder;
using NUnit.Framework;

namespace CGT.UnitTest
{
    [TestFixture]
    public  class TravelOrderImportServiceTest
    {

        [Test]
        public void TraveOrderImport()
        {
            FileStream fs = new FileStream("",FileMode.Open);

            TravelOrderImportService travelOrderImportService = new TravelOrderImportService();
            
            travelOrderImportService.ExcelStream = fs;

            var responseMessage =   travelOrderImportService.Execute();

            Console.WriteLine(responseMessage);
        }

        [Test]
        public void ExcelTable2Enetity()
        {

        }
    }
}
