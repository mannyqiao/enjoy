using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enjoy.Core.EnjoyModels
{
    public class CardCouponQueryFilter:QueryFilter<long>
    {        
        public long? MerchantId { get; set; }
        public CardTypes[] Types { get; set; }
        public CardCouponStates[] States { get; set; }
    }
}