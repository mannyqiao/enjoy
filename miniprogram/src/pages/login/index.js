// //index.js
// //获取应用实例
// const app = getApp()

// Page({
//   data: {
//     motto: 'Hello World',
//     userInfo: {},
//     hasUserInfo: false,
//     canIUse: wx.canIUse('button.open-type.getUserInfo')
//   },
//   //事件处理函数
//   bindViewTap: function() {
//     wx.navigateTo({
//       url: '../logs/logs'
//     })
//   },
//   onLoad: function () {
//     console.log(this.data.canIUse);
//     if (app.globalData.userInfo) {
//       this.setData({
//         userInfo: app.globalData.userInfo,
//         hasUserInfo: true
//       })
//     } else if (this.data.canIUse){
//       // 由于 getUserInfo 是网络请求，可能会在 Page.onLoad 之后才返回
//       // 所以此处加入 callback 以防止这种情况
//       app.userInfoReadyCallback = res => {
//         this.setData({
//           userInfo: res.userInfo,
//           hasUserInfo: true
//         })
//       }
//     } else {
//       // 在没有 open-type=getUserInfo 版本的兼容处理
//       wx.getUserInfo({
//         success: res => {
//           app.globalData.userInfo = res.userInfo
//           this.setData({
//             userInfo: res.userInfo,
//             hasUserInfo: true
//           })
//         }
//       })
//     }
//   },
//   getUserInfo: function(e) {
//     console.log(e)
//     app.globalData.userInfo = e.detail.userInfo
//     this.setData({
//       userInfo: e.detail.userInfo,
//       hasUserInfo: true
//     })
//   }
// })
import ApiList from '../../config/api.js';
import cfg from '../../config/index.js';
import request from '../../utils/request';
import { co, Promise, regeneratorRuntime } from '../../utils/co-loader';
import { getUserInfo } from '../../utils/index';
let app = getApp();

Page({
  data: {
    userInfo: null,
    vcodeBtn: {
      text: '获取验证码',
      disabled: false
    },
    showAuth: true,    
    mobile: null,
    verifyCode: null,//正确的验证码    
  },
  onLoad() {
    const me = this;
    me.setData({ session: wx.getStorageSync(cfg.localKey.session) });
    wx.getSetting({
      success: (res) => {
        if (res.authSetting["scope.userInfo"]) {//已授权               
          me.setData({ showAuth: false });

          app.getUserInfo(app.globalData.session).then(res => {
            me.setData({
              userInfo: res
            });
          });
        }
        else {//需要授权
          me.setData({ showAuth: true });
          console.log("未授权");
        }
      }
    });
  },
  getVcode() {
    const me = this;
    let mytoast = new app.WeToast();
    var mobileNumber = me.data.mobile;
    console.log(mobileNumber);
    if (!/^((1[3,5,8][0-9])|(14[5,7])|(17[0,6,7,8])|(19[7]))\d{8}$/.test(mobileNumber)) {
      mytoast.toast({
        icon: 'fail',
        title: '手机号输入不正确'
      });
    }
    else {
      wx.request({
        url: ApiList.vcode,
        method: "POST",
        data: { mobile: mobileNumber },
        success: function (res) {
          if (res.data.has_error) {
            mytoast.toast({
              icon: 'fail',
              title: res.data.error_message
            });
          }
          else {
            var next = 120;
            var interval = setInterval(function () {
              me.setData({
                'vcodeBtn.text': '重发(' + next + ')秒',
                'vcodeBtn.disabled': true
              })
              next = next - 1;
              if (next <= 0) {
                clearInterval(interval);
                me.setData({
                  'vcodeBtn.text': '获取验证码',
                  'vcodeBtn.disabled': false
                })
              }
            }, 1000);
          }
        }
      })
    }
  },

  bindMobile(e) {
    let strMobile = e.detail.value;
    const me = this;
    const mytoast = new app.WeToast();
    if (/^((1[3,5,8][0-9])|(14[5,7])|(17[0,6,7,8])|(19[7]))\d{8}$/.test(strMobile)) {
      me.setData({ mobile: e.detail.value });
    }
    else {
      me.setData({ mobile: null });
    }
  },
  bindVerifyCode(e) {
    const me = this;
    me.setData({ verifyCode: e.detail.value });
  },
  auth() {
    const me = this;
    
    wx.getSetting({
      success: (res) => {        
        if (res.authSetting["scope.userInfo"]) {
          me.setData({showAuth: false});
          app.getUserInfo(app.globalData.session, me.getUserInfoCallback).then(res=>{
             console.log("auth and get userinfo ",res) ;
          })
          .catch(error=>{

          });
        }
      },
      fail: (error) => {
        console.log("error",error);
      }
    });
  },
  start() {
    const me = this;
    wx.getSetting({
      success: (res) => {
        if (res.authSetting["scope.userInfo"]) {          
          console.log("app.globalData.session", app.globalData.session);
          app.getUserInfo(app.globalData.session, me.getUserInfoCallback).then(res => {
          })
          .catch(err => {
              console.log(err);
          });
        }
        else {
          me.setData({ showAuth: true });
        }
      },
      fail: (error) => {
        console.log(error);
      }
    });
  },
  getUserInfoCallback(info){
    console.log("callback",info);
    const me = this;
    me.setData({ userInfo: info });
    console.log(info);
    if (info.enjoy.state.hasMobile) {
      wx.navigateTo({
        url: '../../pages/member/index',
      });
      me.setData({ showAuth: false });
      console.log("set showAuth false")
    }
    else{
      console.log("set showAuth true");
      me.setData({ showAuth: true });
    }
  },
  submit() {
    const me = this;
    console.log("sss", me.data.userInfo);
    let mytoast = new app.WeToast();
    if (!me.data.mobile || !me.data.verifyCode) {//验证手机号码以及验证码，手机号码必须正确，验证码不能为空
      mytoast.toast({
        icon: 'fail',
        title: '手机号不正确或验证码为空'
      });
    }
    else {
      request(
        {
          url: ApiList.checkVerifyCode,
          data: { mobile: me.data.mobile, verifyCode: me.data.verifyCode },
          success: function (res) {
            if (res.data) {//验证码正确,开始绑定用户
              submitBindMobile(
                {
                  id: me.data.userInfo.enjoy.id,
                  mobile: me.data.mobile,
                  verifyCode: me.data.verifyCode
                });
            } else {
              mytoast.toast({
                icon: 'fail',
                title: '验证码不正确'
              });
              me.submitBindMobile(
                {
                  id: me.data.userInfo.enjoy.id,
                  mobile: me.data.mobile,
                  verifyCode: me.data.verifyCode
                });
            }
          }
        })
    }
  },
  submitBindMobile(option) {
    let mytoast = new app.WeToast();
    request(
      {
        url: ApiList.bindMobile,
        data: option,
        success: function (res) {
          if (res.data) {//验证码正确,开始绑定用户
            mytoast.toast({ icon: 'success', title: '绑定成功' });
            wx.navigateTo({
              url: '../../pages/member/index',
            });
          } else {
            mytoast.toast({ icon: 'fail', title: '绑定失败' });
          }
        }
      })
  }
});