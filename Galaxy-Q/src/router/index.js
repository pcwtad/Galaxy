import {
	createRouter,
	createWebHistory
} from 'vue-router'
import Cookies from 'js-cookie'

import Home from '../Views/Home/home.vue'
import HR_Find from '../Views/Home/Home-Right/HR-Find/HR-Find.vue'
import HR_My from '../Views/Home/Home-Right/HR-My/HR-My.vue'
import HR_Notice from '../Views/Home/Home-Right/HR-Notice/HR-Notice.vue'
import HR_Release from '../Views/Home/Home-Right/HR-Release/HR-Release.vue'
import Hrnchat from '../Views/Home/Home-Right/HR-Notice/HRN-Chat/Hrnchat.vue'
import Hrnnotice from '../Views/Home/Home-Right/HR-Notice/HRN-Notice/Hrnnotice.vue'
import FrientChat from '../Views/Home/Home-Right/HR-Notice/HRN-Chat/FrientChat.vue'

import D_etails from '../Views/Galaxy-details/details.vue'

import Login from '../Views/Login/login.vue'
import Loginone from '../Views/Login/newlogin_one.vue'
import Logintwo from '../Views/Login/newlogin_two.vue'

const router = createRouter({
	history: createWebHistory(),
	routes: [{
			path: '/',
			redirect: 'Home'
		},
		{
			path: '/Home',
			component: Home,
			children: [{
				path: '/Home',
				redirect: '/Home/HR_Find'
			}, {
				path: '/Home/HR_Find',
				component: HR_Find,
				children: [{
					path: '/Home/HR_Find/:showguid',
					component: D_etails
				}]
			}, {
				path: '/Home/HR_Notice',
				component: HR_Notice,
				children: [{
					path: '/Home/HR_Notice',
					redirect: '/Home/HR_Notice/Hrnnotice'
				}, {
					path: '/Home/HR_Notice/Hrnnotice',
					component: Hrnnotice
				}, {
					path: '/Home/HR_Notice/Hrnchat',
					component: Hrnchat,
					children: [{
						path: '/Home/HR_Notice/Hrnchat/:id',
						component: FrientChat
					}]
				}]
			}, {
				path: '/Home/HR_Release',
				component: HR_Release,
			}, {
				path: '/Home/:id',
				component: HR_My,
				children: [{
					path: '/Home/:id/:showguid',
					component: D_etails
				}]
			}]
		},
		{
			path: '/Login',
			component: Login,
			name: "Login",
			children: [{
				path: '/Login',
				redirect: '/Login/Loginone'
			}, {
				path: '/Login/Loginone',
				component: Loginone,
				name: "Loginone",
			}, {
				path: '/Login/Logintwo',
				component: Logintwo,
			}]
		}
	]
})

router.beforeEach((to, from, next) => {
	if (to.fullPath == '/Home/HR_Find') {
		next()
	} else if (to.fullPath == '/Login/Logintwo') {
		next()
	} else if (Cookies.get('GalaxyID') == undefined && to.fullPath != '/Login/Loginone') {
		next({
			name: 'Loginone'
		})
	} else {
		next()
	}
})



export default router