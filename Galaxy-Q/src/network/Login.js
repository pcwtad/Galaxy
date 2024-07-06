import {
	request
} from './request'

export function sendcode(mail) {
	return request({
		method: 'get',
		url: '/LoginService/api/Login/SendCode',
		params: {
			mail
		}
	})
}

export function userregis(user, code) {
	return request({
		method: 'post',
		url: '/LoginService/api/Login/UserRegis',
		headers: {
			'Accept': 'text/plain',
			'Content-Type': 'application/json'
		},
		params: {
			code
		},
		data: {
			userName: user.userName,
			usermail: user.usermail,
			userPasswod: user.userPasswod
		},
	})
}

export function passwdlogin(usermail, userpasswod) {
	return request({
		method: 'post',
		url: '/LoginService/api/Login/PasswdLogin',
		params: {
			usermail,
			userpasswod
		},
	})
}

export function codelogin(usermail, code) {
	return request({
		method: 'post',
		url: '/LoginService/api/Login/CodeLogin',
		params: {
			usermail,
			code
		},
	})
}
