import {
	request
} from './request'

export function SendChats(fromuserid, touserid, index) {
	return request({
		method: 'post',
		url: '/ChatService/api/Chat/SendChat',
		params: {
			fromuserid,
			touserid,
			index
		}
	})
}