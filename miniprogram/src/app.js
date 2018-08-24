//app.js
import cfg from 'config/index.js'
import WeToast from 'components/wetoast/index.js';
import { getUserInfo } from 'utils/index';

//app.js
App({
  WeToast,
  getUserInfo,
  onLaunch() {
    const me = this;        
    me.getUserInfo().then(res => {
    });
  }
});