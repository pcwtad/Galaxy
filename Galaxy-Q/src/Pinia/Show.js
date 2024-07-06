import {
	defineStore
} from 'pinia'

export const useCounterStore = defineStore('counter', {
	state: () => ({
		count: null,
		connection: null,
		follow: null,
		follows: [],
		myuser: {
			'headImg': null,
			'userName': null
		},
		newuser: {
			'headImg': null,
			'userName': null
		}
	}),
	getters: {

	},

	actions: {
		setnewuser(newValue) {
			this.newuser.headImg = newValue.headImg
			this.newuser.userName = newValue.userName

		},
		setmyuser(newValue) {
			this.myuser.headImg = newValue.headImg
			this.myuser.userName = newValue.userName
		},
		setCount(newValue) {
			this.count = newValue
		},
		setconnection(newValue) {
			this.connection = newValue
		},
		followconnection(newValue) {
			this.follow = newValue
			if (newValue.follow == "关注") {
				this.followconnections(newValue)
			}
		},
		followconnections(newValue) {
			this.follows.push(newValue)
		}
	},

})