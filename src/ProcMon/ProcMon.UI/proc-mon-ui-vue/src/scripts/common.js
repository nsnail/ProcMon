import Vue from "vue"
import { Loading } from 'element-ui'

export default {
    cfg: {
        apiGateway: 'http://localhost:5000'
    },
    install() {
        this.initAxios();
        Vue.prototype.common = this;
    },
    initAxios() {
        var loading;
        Vue.axios.interceptors.request.use((config) => {
            loading = Loading.service();
            return config;
        });
        Vue.axios.interceptors.response.use(
            (rsp) => {
                loading.close();
                return rsp;
            },
            (error) => {
                loading.close();
                var showErrorTip = true;
                switch (error.response.status) {
                    // 授权错误
                    case 401:
                        break;
                    // 服务器程序错误
                    case 500:
                        break;
                    default:
                        break;
                }
                if (showErrorTip) {
                    // common.toastBad("发生了一点问题");
                }
                return Promise.reject(error);
            }
        );


    }
}