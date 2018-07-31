

namespace Enjoy.Core
{
    using Enjoy.Core.Models;
    using Enjoy.Core.ViewModels;
    using Orchard;
    using System;
    using Models = Enjoy.Core.Models;
    public interface IMerchantService : IDependency
    {

        /// <summary>
        /// 获取当前用户默认商户
        /// </summary>
        /// <returns></returns>
        Models::MerchantModel GetDefaultMerchant();
        /// <summary>
        /// 根据商户ID获取商户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Models::MerchantModel GetDefaultMerchant(long id);
        /// <summary>
        /// 根据WeChat上的商户Id获取子商户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Models::MerchantModel GetDefaultMerchantByWeChatMerchantId(long id);
        /// <summary>
        /// 保存商户并提交审核
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Models::ActionResponse<Models::MerchantModel> SaveAndPushToWeChat(
            Models::MerchantModel model,
            Action<Models::MerchantModel> push = null);
        /// <summary>
        /// 保存子商户
        /// </summary>
        /// <param name="model"></param>        
        /// <returns></returns>
        Models::ActionResponse<Models::MerchantModel> SaveOrUpdate(
            Models::MerchantModel model);
        /// <summary>
        /// 查询商户审核状态
        /// </summary>
        /// <param name="merchantid"></param>
        /// <returns></returns>
        Models::WxResponseWapper<AuditStatus> QueryMerchantStatus(long merchantid);


        Models::PagingData<Models::MerchantModel> QueryMyMerchants(long userid, int page);

        void UpdateMerchantStatus(long merchantId, AuditStatus status, string reson);

    }
}