import {
	request
} from './request'

export function AddParenteveAsyncsa(comment) {
	return request({
		method: 'post',
		url: '/CommentService/api/Comment/AddParenteveAsyncs',
		data: {
			showId: comment.showId,
			userId: comment.userId,
			title: comment.title
		},
	})
}

export function GetParentevesa(showId, index) {
	return request({
		method: 'post',
		url: '/CommentService/api/Comment/GetParenteves',
		params: {
			showId,
			index
		}
	})
}

export function AddChildeveAsyncsa(comment) {
	return request({
		method: 'post',
		url: '/CommentService/api/Comment/AddChildeveAsyncs',
		data: {
			parenteveId: comment.parenteveId,
			userId: comment.userId,
			coverId: comment.coverId,
			title: comment.title
		},
	})
}

export function GetChildevesa(parenteveId, index) {
	return request({
		method: 'post',
		url: '/CommentService/api/Comment/GetChildeves',
		params: {
			parenteveId,
			index
		}
	})
}