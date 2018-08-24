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
  if (userInfo) {
    console.log("Load user from local storage.",userInfo);

    return userInfo;
  }
  else {
    userInfo = { wx: null, mmh: null,enjoy:null};
    const basic = yield promisify(wx.login)();
    console.log("basic---", basic);
    const user = yield promisify(wx.getUserInfo)({ withCredentials: true });
    userInfo.wx = user.userInfo;
    console.log(userInfo.wx);

    console.log("Do request", basic.code);

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
    //获取加密信息  
    if (session.data) {
      //获取加密信息
      const decodeInfo = yield request({
        url: ApiList.decryptUserInfo,
        method: "POST",
        data: {
          appId: cfg.appid,
          data: user.encryptedData,
          iv: user.iv,
          sessionKey: session.data.session_key
        }
      });
      console.log("加密信息", decodeInfo);
      if (decodeInfo.data) {
        userInfo.mmh = decodeInfo.data;        
        console.log(userInfo);
        wx.setStorageSync(key_user, userInfo)
      }
      
    }
    return userInfo;
  }
});

export { getUserInfo };

