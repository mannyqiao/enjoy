

namespace Enjoy.Core
{
    using Enjoy.Core.ViewModels;
    using Orchard;
    using Models = Enjoy.Core.Models;
    public interface IMerchantService : IDependency
    {

        /// <summary>
        /// 获取当前用户默认商户
        /// </summary>
        /// <returns></returns>
        Models::MerchantModel GetDefaultMerchant();
        /// <summary>
        /// 保存商户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Models::ActionResponse<Models::MerchantModel> SaveOrUpdate(Models::MerchantModel model);
        /// <summary>
        /// 商户审核
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Models::WxResponseWapper<Models::MerchantModel> Audit(Models::MerchantModel model);

        /// <summary>
        /// 查询商户审核状态
        /// </summary>
        /// <param name="merchantid"></param>
        /// <returns></returns>
        Models::WxResponseWapper<Models::MerchantModel> QueryApproveStatus(string merchantid);


        Models::PagingData<Models::MerchantModel> QueryMyMerchants(int userid, int page);
        
    }
}