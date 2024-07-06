import {
	createApp
} from 'vue'
import App from './App.vue'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
import router from './router/index'
import BaiduMap from 'vue-baidu-map'
import {
	createPinia
} from 'pinia'

const pinia = createPinia()


createApp(App).use(pinia).use(router).use(ElementPlus).mount('#app')
