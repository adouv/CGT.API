using CGT.Entity.CgtHangfireModel;
using CGT.PetaPoco.Repositories.CgtHangfire;
using System;
using System.Collections.Generic;

namespace CGT.Base.Service.Hangfire
{
    public class TaskBaseService 
    {
        public JobRep JobRepositories { get; set; }
        
        public List<Job> GetJobList()
        {
            return JobRepositories.GetAllJob();
        }
    }
}
