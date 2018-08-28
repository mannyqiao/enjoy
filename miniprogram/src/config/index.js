// 小程序通过配置信息;
const AppConfig = {
  appid: "wx3ec55fbaa7dcefc7",                //app id
  secret: "e1374e932b2eef3d4b0fa8f0e936496a",           //app secret

    ak: "db185e5dba386e42aeea9723d4a1fc66",                // 高德地图key
    bk: "your baidu map key",           // 百度地址key

    //定位信息
    location: {
        //默认
        defaults: {
            lat: 30.22965,
            lng: 120.192567,
            areaId: "330102",
            province: "四川省",
            city: "成都市",
            district: "锦江区",
            street: "某街道",
            source: "default"
        },
        //当前
        current: null,
        //GPS
        gps: null
    },

    // 本地存储名称集合
    localKey: {
        user: "_user_info",                     // 用户信息
        qcRpt: "_qc_report",                    // 质检报告
    },
};

export default AppConfig;