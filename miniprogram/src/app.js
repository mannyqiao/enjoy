//app.js
import cfg from 'config/index.js'
import WeToast from '/components/wetoast/index.js'; //导入构造函数
import {
  getUserInfo
} from 'utils/index';
import {
  getUserSession,
  getUserGranted
} from 'utils/index';
var app = getApp();
//app.js
App({
  WeToast,
  getUserInfo,
  getUserSession,
  getUserGranted,
  globalData: {
    session: null,
    grantedScope: {
      canUseUserInfo: false,
      canUseMobile: false,
      canUseLocation: false
    },
    cardid: null,
    userInfo: null
  },
  onLaunch() {
   


  },
  
  readlyUserInfoCallback: function(user) {
    this.globalData.userInfo = user;
   
  },
  readlyGettUserGrantedCallback: function(scope) {
    this.globalData.grantedScope = scope;
  },
  onReady(){
   
  },
  onShow(){
    const me = this;
    let option = wx.getLaunchOptionsSync();
    option.path = '/pages/card/index';
    
    if (me.globalData.grantedScope.canUseUserInfo) {
      wx.navigateTo({
        url: '/pages/card/index',
      })
    } else {
      wx.checkSession({
        success: function (res) {
          getUserGranted().then(res => {
            me.globalData.grantedScope = res;
            if (!me.globalData.grantedScope.canUseUserInfo) {

            } else {

            }
          });
        },
        fail: function (res) {
          getUserSession(true).then(res => { //重置session并返回
            console.log("load from local from server", res);
          });
        }
      })
    }
 
  }
});