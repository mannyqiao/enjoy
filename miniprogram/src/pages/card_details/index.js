// pages/card_details/index.js
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
    tabs: ["商户介绍", "会员特权", "积分商城"],
    activeIndex: 1,
    sliderOffset: 0,
    sliderLeft: 0
  },

  /**
   * Lifecycle function--Called when page load
   */
  onLoad: function (options) {
    console.log(options.id);
    var that = this;
    wx.getSystemInfo({
      success: function (res) {
        that.setData({
          sliderLeft: (res.windowWidth / that.data.tabs.length - sliderWidth) / 2,
          sliderOffset: res.windowWidth / that.data.tabs.length * that.data.activeIndex
        });
      }
    });
  },
  tabClick: function (e) {
    this.setData({
      sliderOffset: e.currentTarget.offsetLeft,
      activeIndex: e.currentTarget.id
    });
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

  /**
   * Called when user click on the top right corner to share
   */
  onShareAppMessage: function () {

  }
})