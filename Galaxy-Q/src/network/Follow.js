import {
	request
} from './request'

export function lookFollowsa(follow) {
	return request({
		method: 'post',
		url: '/FollowService/api/Follow/LookFollows',
		data: {
			followers: follow.followers,
			beingfollowed: follow.beingfollowed
		},
	})
}

export function followAsyncsa(follow) {
	return request({
		method: 'post',
		url: '/FollowService/api/Follow/FollowAsyncs',
		data: {
			followers: follow.followers,
			beingfollowed: follow.beingfollowed
		},
	})
}

export function removeFollowsa(follow) {
	return request({
		method: 'post',
		url: '/FollowService/api/Follow/RemoveFollows',
		data: {
			followers: follow.followers,
			beingfollowed: follow.beingfollowed
		},
	})
}


export function getAllFollowsa(beingfollowed, index) {
	return request({
		method: 'post',
		url: '/FollowService/api/Follow/GetAllFollows',
		params: {
			beingfollowed,
			index
		}
	})
}

export function GetBeingfollowedsa(followers) {
	return request({
		method: 'post',
		url: '/FollowService/api/Follow/GetBeingfolloweds',
		params: {
			followers
		}
	})
}