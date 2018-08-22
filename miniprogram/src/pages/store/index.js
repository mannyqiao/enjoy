// pages/stores/index.js
import ApiList from '../../config/api';
import request from '../../utils/request';
import promisify from '../../utils/promisify';
import Map from '../../utils/map';

let app = getApp();

Page({
  data: {
    __dialog__: {
      showDialog: false
    },
    "bannerList": [],
    "nav": {
      "pic": "http://bgo.mamhao.cn/15798732-c55c-470d-9fec-72031c88a2d4.jpg@100p.png",
      "linkType": 8,
      "linkTo": "1"
    },
    "shopList": [],
    "page":0,
    "pageSize":10,
    "totalRow": 100,
    "deliveryAddress": '成都市',
    "actOffset": [
      { offset: 0 },
      { offset: 0 }
    ]
  },
  onLoad() {
    const me = this;   

    //设置banner    
    wx.request({
      url: ApiList.QueryNearbyMerchant,
      data: {page: me.data.page+1 ,size:me.data.pageSize },
      method:"POST",
      success:function(res) {
          me.setData({
            bannerList: res.data
          });
      }
    });  
    //配送地址
    Map.getRegeo().then(res => {
      console.log(res);
      me.setData({
        deliveryAddress: res.street
      })
    });

    /*wx.request({
        method: 'post',
        url: ApiList.storeList,
        success: function (res) {
            console.log(res)
        }
    })*/

    me.data.actOffset.map((v, i) => {
      const shopActList = me.data.shopList[i].shopActList;
      if (shopActList.length > 1) {
        let actIndex = 0;
        setInterval(() => {
          actIndex++;
          if (actIndex > shopActList.length) actIndex = 0;
          let offset = 0;
          if (actIndex < shopActList.length) {
            offset = -actIndex * 100 + '%';
          } else {
            actIndex = 0;
          }
          me.setData({
            [`actOffset[${i}].offset`]: offset
          })
        }, 3000)
      }
    })
  },
  switchAddress() {
    const me = this;
    request({
      url: ApiList.deliveryAddr,
      login: true,
      success(res) {
        console.log('deliveryAddr', res)
        me.setData({
          '__dialog__.showDialog': true,
          '__dialog__.content': res.data
        })
      }
    })

  },
  closeDialog() {
    this.setData({
      '__dialog__.showDialog': false
    })
  },
  onShareAppMessage() {
    return {
      title: '商户',
      path: 'pages/stores/index'
    }
  }
});