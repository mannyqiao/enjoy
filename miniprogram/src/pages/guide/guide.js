import ApiList from '../../config/api.js';
import request from '../../utils/request';
import { co, Promise, regeneratorRuntime } from '../../utils/co-loader';
import { getUserInfo } from '../../utils/index';
let app = getApp()
Page({
  data: {
    imgs: [
      "https://www.yourc.club/media/app/guide-1.png",
      "https://www.yourc.club/media/app/guide-2.png",
      "https://www.yourc.club/media/app/guide-3.png",
      "https://www.yourc.club/media/app/guide-4.png"
    ],
    img: "https://www.yourc.club/media/app/guide.jpg",
  },
  start() {  
    const me = this;
    co(function* () {
      const userInfo = yield app.getUserInfo();          
      me.setData({
        userInfo: userInfo
      });     
    });    
    wx.switchTab({
      url: '/pages/store/index'
    });
  } 
})