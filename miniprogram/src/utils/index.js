import cfg from '../config/index.js';
import ApiList from '../config/api';
import { promisify, complete } from 'promisify';
import { co, Promise, regeneratorRuntime } from 'co-loader';
import request from 'request';

/*
 * 获取用户信息
 * 引用：import {getUserInfo} from  'utils/index';
 * 调用：
 * co(function *() {
 *    var res = yield app.getUserInfo()
 *    console.info('userInfo', res)
 * })
 * 或
 * app.getUserInfo().then(res=>{
 *   console.info('userInfo', res)
 * })
 * */
const getUserInfo = co.wrap(function* () {
  const key_user = cfg.localKey.user; 
  let userInfo = wx.getStorageSync(key_user);
  if(userInfo){
    return userInfo;
  }
  else{
    userInfo = { wx: null, mmh: null };
    const basic = yield promisify(wx.login)();    
    console.log("basic---",basic);
    const user = yield promisify(wx.getUserInfo)({ withCredentials: true });
    userInfo.wx = user.userInfo;
    console.log(userInfo.wx);
     //code换取session_key
    const session = yield request({
      url: ApiList.getAuth,
      method: "POST",
      data: {
        appid: cfg.appid,
        secret: cfg.secret,
        js_code: basic.code,
        grant_type: "authorization_code"
      }
    });

    console.log("session",basic.code, session);
  }
  
  //在javascript中，只要obj不是false、0、undefined、null，if (obj) { } 就会被执行
 


  // if (!userInfo) {
  //   console.log("load userinfo");



    
    
    
    
  //   //解密encryptedData
  //   console.log("Decryp context", {
  //     appId: cfg.appid,
  //     data: user.encryptedData,
  //     iv: user.iv,
  //     sessionKey: session.data.session_key
  //   });
  //   if (session.data) {
  //     const decodeInfo = yield request({
  //       url: ApiList.decryptUserInfo,
  //       method: "POST",
  //       data: {
  //         appId: cfg.appid,
  //         data: user.encryptedData,
  //         iv: user.iv,
  //         sessionKey: session.data.session_key
  //       }
  //     });
  //     console.info('decodeInfo---', decodeInfo);

  //     if (decodeInfo.data) {
  //       userInfo.mmh = decodeInfo.data.mmhUser;
  //       userInfo.wx = decodeInfo.data.wxUser;
  //     }
  //   }
   
  // }
  return userInfo;
});

export { getUserInfo };

