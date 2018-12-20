// pages/welcome/index.js
import cfg from '../../config/index.js';
import ApiList from '../../config/api';
import { promisify, complete } from '../../utils/promisify';
import { co, Promise, regeneratorRuntime } from '../../utils/co-loader';
import request from '../../utils/request';
let app = getApp();
Page({

  /**
   * 页面的初始数据
   */
  data: {
    images:[
      "/images/welcome/welcome-01.jpg",
      "/images/welcome/welcome-02.jpg"
    ],
    image:"/images/welcome/welcome-01.jpg"
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {

  },
  getUserInfo: function (event){
    const me = this;    
    if (event.detail.userInfo) { //用户点了接受按钮     
      app.getUserInfo().then(res => {     
        console.log(res)   ;
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
      .catch(error => { 
        console.log(error);
      });
    } else {
      //用户按了拒绝按钮            
      app.readlyGettUserGrantedCallback(false);
    }
  }

})