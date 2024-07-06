import {
	request
} from './request'

export function DisplayFinds(x) {
	return request({
		method: 'post',
		url: '/ShowService/api/Find/DisplayFind',
		params: {
			x
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

export function DisplayMys(x, id) {
	return request({
		method: 'post',
		url: '/ShowService/api/Find/DisplayMy',
		params: {
			x,
			id
		}
	})
}
