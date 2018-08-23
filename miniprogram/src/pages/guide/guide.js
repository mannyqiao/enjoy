import ApiList from '../../config/api.js';
import request from '../../utils/request';
import { co, Promise, regeneratorRuntime } from '../../utils/co-loader';
import { getUserInfo } from '../../utils/index';

Page({
  data: {
    imgs: [
      "http://www.yourc.club/media/app/guide-1.png",
      "http://www.yourc.club/media/app/guide-2.png",
      "http://www.yourc.club/media/app/guide-3.png",
      "http://www.yourc.club/media/app/guide-4.png"
    ],
    img: "http://www.yourc.club/media/app/guide.jpg",
  },
  start() {  
    const me = this;
    co(function* () {
      const userInfo = yield app.getUserInfo();
      console.log("xx", userInfo);
      me.setData({
        userInfo: userInfo.wx
      });     
    });    
    wx.switchTab({
      url: '/pages/store/index'
    });
  }
})