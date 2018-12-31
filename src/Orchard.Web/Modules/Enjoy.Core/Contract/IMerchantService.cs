

namespace Enjoy.Core
{
    using Enjoy.Core.EnjoyModels;
    using Enjoy.Core.ViewModels;
    using Orchard;
    using System;
    using Enjoy.Core.WeChatModels;
    using Enjoy.Core.Records;
    using System.Collections.Generic;

    public interface IMerchantService : IDependency
    {

        /// <summary>
        /// 获取当前用户默认商户
        /// </summary>
        /// <returns></returns>
        MerchantModel GetDefaultMerchant();
        MerchantModel GetDefaultMerchant(string mcode);
        /// <summary>
        /// 根据商户ID获取商户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MerchantModel GetDefaultMerchant(long id);
        /// <summary>
        /// 根据WeChat上的商户Id获取子商户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MerchantModel GetDefaultMerchantByWeChatMerchantId(long id);
        /// <summary>
        /// 保存商户并提交审核
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ActionResponse<MerchantModel> SaveAndPushToWeChat(
            MerchantModel model,
            Action<MerchantModel> push = null);
        /// <summary>
        /// 保存子商户
        /// </summary>
        /// <param name="model"></param>        
        /// <returns></returns>
        ActionResponse<MerchantModel> SaveOrUpdate(MerchantModel model);
        /// <summary>
        /// 查询商户审核状态
        /// </summary>
        /// <param name="merchantid"></param>
        /// <returns></returns>
        WxResponseWapper<AuditStatus> QueryMerchantStatus(long merchantid);
        BaseResponse Delete(long id);
        PagingData<MerchantModel> QueryMerchants(WebQueryFilter filter, PagingCondition condition);

        void UpdateMerchantStatus(long merchantId, AuditStatus status, string reson);
        
        
        


    }
}