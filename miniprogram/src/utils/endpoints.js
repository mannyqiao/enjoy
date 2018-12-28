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


const queryCardsByMid = co.wrap(function* (mid,types) { 
  const result = yield request({
    url: ApiList.queryCardsByMid,
    method: "POST",
    data: {     
      "MerchantId":mid,
      "Types":types
    }   
  }); 
  return result;
});
const topup=co.wrap(function*(context){
  const result = yield request({
    url: ApiList.topUp,
    data: context
  });
  return result;
})
export {
  queryCardsByMid,
  topup
};