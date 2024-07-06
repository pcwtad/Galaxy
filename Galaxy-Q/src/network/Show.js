import {
	request
} from './request'

export function Showincreases(file, userId, showbrin, showwhere, showaddress, showinformation) {
	return request({
		method: 'post',
		url: '/ShowService/api/Show/Showincrease',
		headers: {
			'Content-Type': 'multipart/form-data'
		},
		params: {
			userId,
			showbrin,
			showwhere,
			showaddress,
			showinformation
		},
		data: file
	})
}
