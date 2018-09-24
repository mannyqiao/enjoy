/**
 * api list
 */
const host = "https://www.yourc.club/";
//const host = "http://localhost/";
export default {
    "getSession":           `${host}api/enjoy/GetSessionKey`,                                        //获取微信用户信息
    "decryptUserInfo":      `${host}api/enjoy/DecryptUserInfo`,                           //解密微信用户信息
    "queryMerchants":       `${host}api/enjoy/QueryMerchants`,                          //查询商信息
    "queryShops":           `${host}api/enjoy/QueryShops`,                          //查询附近门店
    "vcode":                `${host}api/enjoy/SendVerifyCode`,                           //绑定获取验证码
    "checkVerifyCode":      `${host}api/enjoy/CheckVerifyCode`,                  
    "bindMobile":           `${host}api/enjoy/BindMobile`,                                     //绑定手机号码
    "getCenterInfo":        `${host}V1/basic/memberInfo.htm`,                               //个人中心


    //门店相关
    "storeList":            `${host}V3/member/shop/queryMemberShopIndex.htm`,               //门店列表
    "storeInfo":            `${host}V2/shop/basic/queryShopBasicInfo.htm`,                  //门店首页
    "storeSimInfo":         `${host}V1/shop/basic/queryShopSimBasicInfo.htm`,               //门店详情
    "storeGoodsList":       `${host}V3/shop/goods/list.htm`,                                //门店商品列表


    //商品分类
    "goodsType":            `${host}V1/goods/queryGoodsType.htm`,                           // 商品分类
    "goodsTypeTree":        `${host}V1/goods/queryGoodsTypeTree.htm`,                       // 商品二级分类


    //商品详情相关
    "goodsDetail":          `${host}V1/goods/queryGoodsDetail.htm`,                         // 商品详情

}
