// pages/card/index.js

import cfg from '../../config/index.js';
import ApiList from '../../config/api';
import { promisify,  complete} from '../../utils/promisify';
import {  co,  Promise,  regeneratorRuntime} from '../../utils/co-loader';
import request from '../../utils/request';


let app = getApp();
Page({
  /**
   * Page initial data
   */
  data: {
    granted: false, //是否已授权获取用户信息
    hasMobile: false,
    mobile: null,
    userCard: {
      cardid: "112323xxxxxxxxxx",
      merchant: "一味现捞",
      cardName: "铂金卡",
      balance: 100,
      bonus: 10,
      bonusMoney: 10,
    },
     //附近的会员卡列表
    cards: [
      { cardId: "xxxxxxxx", merchantName: "一味现捞", brandName: "铂金卡", privilege:"",logoUrl:""},
      { cardId: "xxxxxxxx", merchantName: "柠檬工坊", brandName: "会员卡", privilege: "", logoUrl: "" }
    ],
    grantedScope: {
      canUseUserInfo: false,
      canUseMobile: false,
      canUseLocation: false
    },
    userInfo: null
  },
  /**
   * Lifecycle function--Called when page load
   */
  onLoad: function(options) {
    let me = this;
    app.getUserGranted().then(res => {
      console.log("me.data.grantedScopexxx", res);
      me.setData({
        grantedScope: res
      });
      me.grantedScope = res;
      app.readlyGettUserGrantedCallback(res);
    })
    app.getUserInfo().then(res => {
      me.setData({
        name: res.wx.nickName,
        avatar: res.wx.avatarUrl,
        hasMobile: res.enjoy.state.hasMobile,
        mobile: res.enjoy.mobile
      });
      me.setData({ userInfo:res.wx});
    });
  },
  getUserInfo: function(event) {
    const me = this;
    if (event.detail.userInfo) { //用户点了接受按钮     
      app.getUserInfo().then(res => {
          console.log("res.enjoy", res.enjoy);
          app.readlyUserInfoCallback(res);
          app.readlyGettUserGrantedCallback(true);
          me.setData({
            grantedScope: {
              canUseUserInfo: true
            },
            name: res.wx.nickName,
            avatar: res.wx.avatarUrl,
            hasMobile: res.enjoy.state.hasMobile,
            mobile: res.enjoy.mobile
          });
        })
        .catch(error => {});
    } else {
      //用户按了拒绝按钮            
      app.readlyGettUserGrantedCallback(false);
    }
  },
  getPhoneNumber: function(event) {
    const me = this;  
    console.log(event)  
    if (event.detail.errMsg === "getPhoneNumber:ok") {
      app.getUserSession(false).then(res=>{
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
          success:function(res){
              me.setData({hasMobile:res.detail.state.hasMobile,mobile:res.mobile});
          }
        });
      });
    }
  }
})