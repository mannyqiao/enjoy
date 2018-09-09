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
    clickedAuth: false,
    mobile: null,
    verifyCode:null,//正确的验证码
    session:null
  },
  onLoad() {
    const me = this;
    me.setData({ session: wx.getStorageSync(cfg.localKey.session)});    
    wx.getSetting({
      success: (res) => {
        if (res.authSetting["scope.userInfo"]) {//已授权               
          me.setData({ showAuth: false });
          app.getUserInfo(me.data.session).then(res => {                        
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

    // this.setData({ showAuth: app.globalData.withCredentials});
    // co(function* () {
    //   const userInfo = yield app.getUserInfo();
    //   console.log("xx", userInfo);
    //   me.setData({
    //     userInfo: userInfo.wx
    //   });
    // });
    // me.setData({ showAuth: this.data.userInfo == null });
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
      console.log("true", strMobile);
    }
    else{
      me.setData({ mobile: null });
    }
  },
  bindVerifyCode(e){
    const me = this;
    me.setData({ verifyCode:e.detail.value});
  },
  auth() {
    const me = this;
    me.setData({ clickedAuth: true });
    wx.getSetting({
      success: (res) => {
        if (res.authSetting["scope.userInfo"]) {
          me.setData({
            showAuth: false
          });
        }
      },
      fail: (error) => {
        console.log(error);
      }
    });
  },
  start() {
    const me = this;
    wx.getSetting({
      success: (res) => {
        if (res.authSetting["scope.userInfo"]) {
          me.setData({ showAuth: false });
          app.getUserInfo().then(res => {
            console.log("xx", res);
            me.setData({
              userInfo: userInfo.wx
            });
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
  submit(){
    const me = this;
    console.log("sss",me.data.userInfo);
    let mytoast = new app.WeToast();
    if(!me.data.mobile|| !me.data.verifyCode){//验证手机号码以及验证码，手机号码必须正确，验证码不能为空
      mytoast.toast({
        icon: 'fail',
        title: '手机号不正确或验证码为空'
      });      
    }
    else{            
      request(
      {
          url: ApiList.checkVerifyCode,
          data: { mobile: me.data.mobile, verifyCode: me.data.verifyCode },
          success:function(res){
            if(res.data){//验证码正确,开始绑定用户
              


            }else{
              mytoast.toast({
                icon: 'fail',
                title: '验证码不正确'
              });             
            }
          }
      })     
    }
  }
});