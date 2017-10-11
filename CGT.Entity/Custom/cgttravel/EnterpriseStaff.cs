using PetaPoco.NetCore;

namespace CGT.Entity.CgtTravelModel
{
    public partial class EnterpriseStaff
    {
        [ResultColumn] public string EnterpriseName { get; set; }

        [ResultColumn] public string PayCenterCode { get; set; }
    }
}
