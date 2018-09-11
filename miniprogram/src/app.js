//app.js
import cfg from 'config/index.js'
import WeToast from '/components/wetoast/index.js';//导入构造函数
import { getUserInfo } from 'utils/index';
import { getUserSession } from 'utils/index';
var app = getApp();

//app.js
App({
  WeToast,
  getUserInfo,
  getUserSession,
  globalData: {
    withCredentials: false,
    session: null
  },
  onLaunch() {
    const me = this;    
    wx.checkSession({
      success: function (res) {                
        let user = wx.getStorageSync(cfg.localKey.user);
        console.log("user",user);
        if (user && user.enjoy && user.enjoy.state.hasMobile) {          
          wx.navigateTo({
            url: '/pages/member/index',
          })
        }
      },
      fail: function (res) {        
        me.getUserSession().then(res => {
          me.globalData.session = res;
        });
      }
    });
  }
});