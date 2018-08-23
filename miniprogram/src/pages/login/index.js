import ApiList from  '../../config/api.js';
import request from '../../utils/request';
import {co, Promise, regeneratorRuntime} from '../../utils/co-loader';
import { getUserInfo } from '../../utils/index';
let app = getApp();

Page({
    data: {
        userInfo: null,
        vcodeBtn: {
            text:'获取验证码',
            disabled: false
        },
        mobile:null,
        showAuth:true       
    },
    onLoad (){              
        const me = this;
        co(function *() {
            const userInfo = yield app.getUserInfo();
            console.log("xx",userInfo);
            me.setData({
                userInfo: userInfo.wx
            });
        });
        me.setData({ showAuth: this.data.userInfo==null });        
    },
    getVcode(){      
      const me = this;     
       
        var mobileNumber = me.data.mobile;        
        console.log(mobileNumber);
        if (!/^((1[3,5,8][0-9])|(14[5,7])|(17[0,6,7,8])|(19[7]))\d{8}$/.test(mobileNumber)) {
          me.wetoast.toast({
            icon: 'fail',
            title: '手机号输入不正确'
          });
        }
        else{
            wx.request({
              url: ApiList.vcode,
              method:"POST",
              data: { mobile: mobileNumber},
               success:function(res){
                 if(res.data.has_error){
                   me.wetoast.toast({
                     icon: 'fail',
                     title: res.data.error_message
                   });
                 }
                 else{                      
                   var next = 120;                       
                   var interval = setInterval(function(){                     
                     me.setData({
                       'vcodeBtn.text': '重发(' + next +')秒',
                       'vcodeBtn.disabled': true
                     })
                     next = next-1;
                     if(next<=0){
                       clearInterval(interval); 
                       me.setData({
                         'vcodeBtn.text': '获取验证码',
                         'vcodeBtn.disabled': false
                       })
                     }
                   },1000);
                 }
               }
            })
        }
    },
    bindAccount(e){
        const wx_user = this.data.userInfo;
        const me = this;
        console.log("ddd", e.detail.value);
        console.log(e.detail.value);
        let {mobile, vcode} = e.detail.value;
        me.setData({
          "mobile":mobile
        });
      
        if(!/^1[3,4,5,7,8]\d{9}$/.test({mobile})){
            this.wetoast.toast({
                icon: 'fail',
                title: '手机号输入不正确'
            });
        }

        request({
            url: ApiList.bind,
            data: Object.assign({mobile, vcode}, {
                unionId: wx_user.unionId,
                openId: wx_user.openId,
                nickName: wx_user.nickName,
                thirdHeadPic: wx_user.avatarUrl
            }),
            success(res){

            }
        })
    },
    bindMobile(e){
      const me = this;
      me.setData({
        mobile: e.detail.value
      });      
    }
});