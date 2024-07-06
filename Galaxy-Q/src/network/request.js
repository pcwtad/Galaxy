import axios from "axios"
import qs from 'qs'
export function request(config) {
	const instance = axios.create({
		baseURL: 'http://127.0.0.1:81',
		timeout: 5000,
	})
	//判断是否用data传值
	if (config.headers == "'Content-Type': 'application/json'") {} else {
		// data传值要使用格式
		// config.data=qs.stringify(config.data)
		// config.data=JSON.stringify(config.data)
	}
	//判断网络请求是否为空
	if (config.method == undefined) {
		//为空默认GET请求
	} else {
		config.method = config.method;
	}
	//请求拦截器
	instance.interceptors.request.use(config => {
		return config;
	}, err => {
		console.log(config.method + "请求失败")
	})
	//响应拦截器
	instance.interceptors.response.use(res => {
		return res.data;
	}, err => {
		console.log(config.method + "响应失败")
	})

	return instance(config)

}
