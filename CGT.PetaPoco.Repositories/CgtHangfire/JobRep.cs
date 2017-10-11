using System;
using System.Linq;
using System.Collections.Generic;
using CGT.Entity.CgtHangfireModel;

namespace CGT.PetaPoco.Repositories.CgtHangfire
{
    public class JobRep
    {
        public List<Job> GetAllJob()
        {
            using (var db = CgtHangfireDB.GetInstance())
            {
                return db.Query<Job>("SELECT * FROM cgt_hangfire.HangFire.Job WHERE StateName <> 'Deleted'").ToList();
            }
        }
    }
}
