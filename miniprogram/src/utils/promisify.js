import Promise from '../libs/es6-promise.min';

const promisify = (api) => {
  console.log("api-->", options);
    return (options, ...params) => {
        return new Promise((resolve, reject) => {
            api(Object.assign({}, options, { success: resolve, fail: reject }), ...params);
        });
    };
};

const complete = (api) => {
    return (options, ...params) => {
        return new Promise((resolve, reject) => {
            api(Object.assign({}, options, { complete: resolve }), ...params);
        });
    };
};

export {promisify, complete}