var app = getApp()
Page({
  data: {
    images: 'https://www.yourc.club/media/',
    items: []
  },
  onLoad: function () {
    // wx.request({
    //   url: 'http://www.easy-mock.com/mock/5906811e7a878d73716e32c9/viplist/itemlist',
    //   method: 'GET',
    //   data: {},
    //   header: {
    //     'Accept': 'application/json'
    //   },
    //   success: (res) => {
    //     console.log(res);
    //     this.setData({
    //       images: res.data.image,
    //       items: res.data.item
    //     });
    //   }
    // })
  }
})