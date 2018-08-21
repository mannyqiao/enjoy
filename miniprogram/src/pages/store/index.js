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
    "bannerList": [
      {
        "pic": "https://www.yourc.club/media/brand/1.jpg",
        "linkType": 9,
        "linkTo": "137",
        "linkName": ""
      },
      {
        "pic": "https://www.yourc.club/media/brand/2.jpg",
        "linkType": 2,
        "linkTo": "3456",
        "itemNumId": 100
      },
      {
        "pic": "https://www.yourc.club/media/brand/3.jpg",
        "linkType": 8,
        "linkTo": "1"
      },
      {
        "pic": "https://www.yourc.club/media/brand/4.jpg",
        "linkType": 8,
        "linkTo": "1"
      }
    ],
    "nav": {
      "pic": "http://bgo.mamhao.cn/15798732-c55c-470d-9fec-72031c88a2d4.jpg@100p.png",
      "linkType": 8,
      "linkTo": "1"
    },
    "shopList": [
      {
        "shopId": 43388,
        "lng": 120.165651,
        "lat": 30.243801,
        "shopAddr": "杭州市上城区延安路98号银泰西湖店地下一楼",
        "shopName": "PUMA Kids银泰西湖店",
        "shopLogo": "https://www.yourc.club/media/brand/4.jpg",
        "type": 1, //
        //标识该门店类型 1-热门店 2-购买过 3-关注店 4-附近店
        "shopActList": [
          {
            "iconName": "满减",
            "iconColor": "#ff4d61",
            "actName": "满5000减1000,满10000减2000  满5000减1000,满10000减2000 满5000减1000,满10000减2000"
          },
          {
            "iconName": "公告",
            "iconColor": "#ff4d61",
            "actName": "这是一个门店公告"
          }
        ]
      },
      {
        "shopId": 43388,
        "lng": 120.165651,
        "lat": 30.243801,
        "shopAddr": "杭州市上城区延安路98号银泰西湖店地下一楼",
        "shopName": "PUMA Kids银泰西湖店",
        "shopLogo": "http://bgo.mamhao.cn/f8e3bb18-cd83-4f33-9171-511a8c281585.jpg",
        "type": 1, //
        //标识该门店类型 1-热门店 2-购买过 3-关注店 4-附近店
        "shopActList": [
          {
            "iconName": "公告",
            "iconColor": "#ff4d61",
            "actName": "这是一个门店公告"
          },
          {
            "iconName": "满减",
            "iconColor": "#ff4d61",
            "actName": "满5000减1000,满10000减2000"
          },
          {
            "iconName": "满减2",
            "iconColor": "#ff4d61",
            "actName": "满5000减1000,满10000减2000"
          }
        ]
      }
    ],
    "totalRow": 100,
    "deliveryAddress": '成都市',
    "actOffset": [
      { offset: 0 },
      { offset: 0 }
    ]
  },
  onLoad() {
    const me = this;

    //配送地址
    Map.getRegeo().then(res => {
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
      title: '门店购',
      path: 'pages/stores/index'
    }
  }
});