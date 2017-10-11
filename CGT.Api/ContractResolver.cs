using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGT.Api
{
    public class ContractResolver : Newtonsoft.Json.Serialization.DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName;
        }
    }
}
