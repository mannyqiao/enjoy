import cfg from  '../../config/index.js'
import ApiList from '../../config/api.js';
import request from '../../utils/request';
import { co, Promise, regeneratorRuntime } from '../../utils/co-loader';
import { getUserInfo } from '../../utils/index';
let app = getApp();
// pages/welcome/index.js
Page({

  /**
   * 页面的初始数据
   */
  data: {

  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {

  },

  /**
   * 生命周期函数--监听页面初次渲染完成
   */
  onReady: function () {
    var userInfo = wx.getStorageSync(cfg.localKey.user);
    console.log("userInfo",userInfo);
    if (userInfo.wx == null) {
      wx.navigateTo({
        url: '../../pages/guide/guide'
      })
    }
    else {
      // wx.navigateTo({
      //   url: '/pages/active/index'
      // })
      wx.switchTab({
        url: '../../pages/store/index'
      });
    }
  },

  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function () {

  },

  /**
   * 生命周期函数--监听页面隐藏
   */
  onHide: function () {

  },

  /**
   * 生命周期函数--监听页面卸载
   */
  onUnload: function () {

  },

  /**
   * 页面相关事件处理函数--监听用户下拉动作
   */
  onPullDownRefresh: function () {

  },

  /**
   * 页面上拉触底事件的处理函数
   */
  onReachBottom: function () {

  },

  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function () {

  }
})