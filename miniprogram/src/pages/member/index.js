// pages/my/index.js
import request from '../../utils/request.js';
import APIs from '../../config/api.js';
import WeToast from '../../components/wetoast/index.js';
import { co, Promise, regeneratorRuntime } from '../../utils/co-loader';
import { getUserInfo } from '../../utils/index';
import cfg from '../../config/index.js';
let app = getApp()

Page({
  getUserInfo,
  WeToast,
  data: {
    '__modal__': {
      show: false
    },
    name: '',     // 用户昵称
    avatar: '',   // 用户头像    
    logined: false //是否已登陆
  },
  onLoad() {
    const me = this;  
    wx.setNavigationBarTitle({
      title: '会员中心',
    })
    let userInfo = wx.getStorageSync(cfg.localKey.user);//从本地缓存中获取 user        
    if(userInfo){ //如果userInfo ！= null !=undefined       
      me.setData({ logined: true });
      let { nickName: name, avatarUrl: avatar } = userInfo.wx;
      me.setData({ name, avatar });  
    }   
  },
  onReady() {
  },
  start() {
   
  }
});