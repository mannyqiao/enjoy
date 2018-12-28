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

import {  topup} from "../../utils/endpoints";

let app = getApp();
Page({
  topup,
  data: {
    moneyBlocks: [
      { "text": "50元", "money": 50 },
      { "text": "100元", "money": 100 },
      { "text": "200元", "money": 200 },
      { "text": "300元", "money": 300 }],
    "money": 50,
    topup: {
      appid: "",
      money: 0,
      openid: "",
      unionid: "",
      cardid: "",
      code: ""
    }
  },
  onLoad() {
    const me = this;
    let token = wx.getStorageSync(cfg.localKey.token);    
    me.setData({
      topup:{
        appid: cfg.appid,
        money: me.data.money,
        openid: token.token.openid,
        unionid: token.unionId,
        cardid: "ddd",
        code: "ddd"
      }
    });
    console.log(me.data.topup);
  },
  clickMoneyBlock: function (event) {
    const me = this;
    console.log(event.target.dataset["money"]);
    if (event.target.dataset["money"]) {
      me.setData({ money: event.target.dataset["money"] });
    }
  },
  changeMoney:function(event){
    console.log(event)
  },
  clickPayment: function (event) {
    const me = this;            
    let context = me.data.topup;
   me.topup(context).then((ctx)=>{
     wx.requestPayment({
       timeStamp: ctx.data.timeStamp,
       nonceStr: ctx.data.nonceStr,
       package: ctx.data.package,
       signType: ctx.data.signType,
       paySign: ctx.data.paySign,
       'success': function (res) { 
         console.log(res);
       },
       'fail': function (res) {
         console.log(res);
        },
       'complete': function (res) { 
         console.log(res);
       }
     })
   });
  }

});