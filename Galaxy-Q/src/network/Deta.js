import {
	request
} from './request'

export function oteurlsa(id) {
	return request({
		method: 'post',
		url: '/ShowService/api/Deta/oteurls',
		params: {
			id
		}
	})
}

export function ObtainUsersa(id) {
	return request({
		method: 'post',
		url: '/LoginService/api/Login/ObtainUsers',
		params: {
			id
		}
	})
}
