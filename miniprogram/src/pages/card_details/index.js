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
import { queryCardsByMid } from "../../utils/endpoints";

let app = getApp();
Page({
  
  /**
   * Page initial data
   */
  data: {
    bannerList: [
      { linkTo: "", pic: "https://www.yourc.club/media/banners/banner-01.jpg" },
      { linkTo: "", pic: "https://www.yourc.club/media/banners/banner-01.jpg" },
      { linkTo: "", pic: "https://www.yourc.club/media/banners/banner-01.jpg" }
    ],  
    activeIndex: 1,
    sliderOffset: 0,
    sliderLeft: 0,
    details:{
      id:"",
      merchantName:"",
      brandName:"",
      privilege:"",
      wxno:"",
      fromOpenId:"",
    },
    openid:""
  },

  /**
   * Lifecycle function--Called when page load
   */
  onLoad: function (options) {
    const me = this;
    let token = wx.getStorageSync(cfg.localKey.token);
    me.setData({details:options,openid:token.token.openid});
    

    console.log(options);
  },
  /**
   * Lifecycle function--Called when page is initially rendered
   */
  onReady: function () {

  },

  /**
   * Lifecycle function--Called when page show
   */
  onShow: function () {

  },

  /**
   * Lifecycle function--Called when page hide
   */
  onHide: function () {

  },

  /**
   * Lifecycle function--Called when page unload
   */
  onUnload: function () {

  },

  /**
   * Page event handler function--Called when user drop down
   */
  onPullDownRefresh: function () {

  },

  /**
   * Called when page reach bottom
   */
  onReachBottom: function () {

  },
  onTouchAddCardBag: function (event) {    
    let details = event.target.dataset["details"];    
    let token = wx.getStorageSync(cfg.localKey.token);
    console.log(token);
    request({
      url: ApiList.getCardExtString + "?time=" + new Date(),
      method: "POST",
      data: {
        appid: cfg.appid,
        secret: cfg.secret,
        cardid: details.wxno,
        openid: token.token.openid
      },
      success: function (res) {        
        var card = {
          cardId: details.wxno,
          cardExt: res.data
        }    
        wx.addCard({
          cardList: [card],
          success(res){
            console.log(res);
          }
        })
      }
    });
     //getCardExtString
    
  } ,
  /**
   * Called when user click on the top right corner to share
   */
  onShareAppMessage: function (event) {
    console.log(event);
  } , 
  onSharingCard:function(event){
    let details = event.target.dataset["details"];  
    
    
  }
})


