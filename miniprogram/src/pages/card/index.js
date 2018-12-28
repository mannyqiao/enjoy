// pages/card/index.js
import cfg from '../../config/index.js';
import ApiList from '../../config/api';
import {
  promisify,
  complete
} from '../../utils/promisify';
import {
  co,
  Promise,
  regeneratorRuntime
} from '../../utils/co-loader';
import request from '../../utils/request';
import { queryCardsByMid } from "../../utils/endpoints";
let app = getApp();
Page({
  /**
   * Page initial data
   */
  queryCardsByMid,
  data: {
    granted: false, //是否已授权获取用户信息
    hasMobile: false,
    mobile: null,
    merchant: "柠檬工坊",
    address: "眉山市东坡区商业水街2号楼14号",
    userCards: [{
      brandName: "金卡", balance: 90, reward:5
    }],
    //商户会员卡
    cards: [],
    token: null,

  },
  /**
   * Lifecycle function--Called when page load
   */
  onLoad: function(options) {
    let me = this;
    let token = wx.getStorageSync(cfg.localKey.token);
    me.setData({token:token});
    me.queryCardsByMid(1,[6]).then(res=>{      
      me.setData({"cards":res.data});
      console.log(res.data);
    });
    
  },
  
  getPhoneNumber: function(event) {
    const me = this;
    console.log(event)
    if (event.detail.errMsg === "getPhoneNumber:ok") {
      app.getUserSession(false).then(res => {
        console.log("session", res);
        //请求服务器解密手机号码
        request({
          url: ApiList.bindMobile + "?time=" + new Date(),
          method: "POST",
          data: {
            appid: cfg.appid,
            data: event.detail.encryptedData,
            sessionKey: session.session_key,
            iv: event.detail.iv,
            wx: {
              "unionId": me.userInfo.wx.unionId
            }
          },
          success: function(res) {
            me.setData({
              hasMobile: res.detail.state.hasMobile,
              mobile: res.mobile
            });
          }
        });
      });
    }
  },
 
})