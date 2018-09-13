import cfg from '../config/index.js';
import ApiList from '../config/api';
import {
  promisify,
  complete
} from 'promisify';
import {
  co,
  Promise,
  regeneratorRuntime
} from 'co-loader';
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
const getUserInfo = co.wrap(function*() {
  const userInfo = {
    wx: null,
    enjoy: null,
    openid: null,
    unionId: null
  };
  let session = yield getUserSession();
  const user = yield promisify(wx.getUserInfo)({
    withCredentials: true
  });
  userInfo.wx = user.userInfo;
  //获取加密数据
  if (session) {
    const decodeInfo = yield request({
      url: ApiList.decryptUserInfo,
      method: "POST",
      data: {
        appId: cfg.appid,
        data: user.encryptedData,
        iv: user.iv,
        sessionKey: session.session_key
      }
    });
    console.log("decodeinfo", decodeInfo.data);
    if (decodeInfo) { //得到加密信息以后将信息存储到本地
      userInfo.enjoy = decodeInfo.data;
      userInfo.unionId = decodeInfo.data.unionId;
      userInfo.openid = decodeInfo.data.openid;
    }
  }
  wx.setStorageSync(cfg.localKey.user, userInfo)
  return userInfo;
});

const getUserSession = co.wrap(function*(reset) {
  if(reset){          
    return yield resetUserSession();  
  }
  else{
    let localUserSession =  wx.getStorageSync(cfg.localKey.session);        
    if (localUserSession){      
      return localUserSession;
    }else{
      return yield resetUserSession();
    }
  } 
});
//重置 user session
const resetUserSession = co.wrap(function* (){
  const basic =yield promisify(wx.login)();  
  const userSession = {
    code: null,
    session_key: null,
    openid: null,
    expires_in: 0
  };
  const session =yield request({
    url: ApiList.getSession + "?time=" + new Date(),
    method: "POST",
    data: {
      appid: cfg.appid,
      secret: cfg.secret,
      js_code: basic.code,
      grant_type: "authorization_code"
    }
  });
  
  userSession.code = basic.code;
  userSession.session_key = session.data.session_key;
  userSession.openid = session.data.openid;
  userSession.expires_in = session.expires_in;
  wx.setStorageSync(cfg.localKey.session, userSession);  
  return userSession;
});
export {
  getUserInfo,
  getUserSession,
  resetUserSession
};