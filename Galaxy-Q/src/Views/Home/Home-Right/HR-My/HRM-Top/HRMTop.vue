<template>
	<div id="HRMTop">
		<div class="myitem" v-for="item in user" :key="item">
			<div class="myitem-a">
				<img :src="item.headImg" />
			</div>
			<div class="myitem-b">
				<div class="myname">{{item.userName}}</div>
				<div class="myin">{{item.userintroduce}}</div>
				<div class="mythree">
					<div>{{item.userfwi}}关注</div>
					<div>{{item.userfans}}粉丝</div>
					<div>{{item.userlike}}点赞</div>
				</div>
			</div>
			<div class="myitem-c" v-text="isfollow?'已关注':'关注'" @click="followcilck">
			</div>
		</div>
	</div>
</template>

<script>
	import {
		ObtainUsersa
	} from '../../../../../network/Deta.js'
	import {
		ElMessage
	} from 'element-plus'
	import {
		lookFollowsa,
		followAsyncsa,
		removeFollowsa
	} from '../../../../../network/Follow.js'
	import Cookies from 'js-cookie'
	export default {
		name: 'HRMTop',

		data() {
			return {
				user: null,
				isfollow: false,
				follow: {
					followers: '',
					beingfollowed: ''
				}
			}
		},

		methods: {

			followcilck() {
				if (this.isfollow) {
					removeFollowsa(this.follow).then(res => {
						if (res) {
							ElMessage({
								message: '已取关！',
								type: 'warning',
							})
							this.isfollow = !this.isfollow
						} else {
							ElMessage.error('错误！')
						}
					}).catch(err => {})
				} else {
					followAsyncsa(this.follow).then(res => {
						if (res) {
							ElMessage({
								message: '已关注！',
								type: 'success',
							})
							this.isfollow = !this.isfollow
						} else {
							ElMessage.error('错误！')
						}
					}).catch(err => {})
				}
			},

		},

		created() {
			this.follow.beingfollowed = this.$route.params.id
			this.follow.followers = Cookies.get('GalaxyID')
			ObtainUsersa(this.$route.params.id).then(res => {
				this.user = res
			}).catch(err => {})

			lookFollowsa(this.follow).then(res => {
				this.isfollow = res
			}).catch(err => {})
		}

	}
</script>

<style>
	#HRMTop {
		display: flex;
		align-items: center;
		justify-content: center;
		height: 220px;
		width: 100%;
		border-bottom: 1px solid #eee;
	}

	.myitem {
		display: flex;
		align-items: center;
		width: 60%;
		height: 100%;
	}

	.myitem-a {
		border: 1px solid #eee;
		border-radius: 50%;
		width: 150px;
		height: 150px;
	}

	.myitem-a img {
		width: 100%;
		border-radius: 50%;
	}

	.myitem-b {
		margin-left: 25px;
		display: flex;
		flex-direction: column;
		justify-content: center;
		width: 50%;
		height: 70%;
	}

	.myname {
		margin-top: 20px;
		font-size: 24px;
		font-weight: 600;
	}

	.myid {
		font-size: 12px;
		font-weight: 300;
		margin-top: 8px;
	}

	.myin {
		margin-top: 8px;
		font-size: 14px;
		font-weight: 400;
	}

	.mythree {
		display: flex;
		margin-top: 10px;
	}

	.mythree div {
		margin-left: 15px;
		font-size: 14px;
		font-weight: 400;
	}

	.myitem-c {
		display: flex;
		justify-content: center;
		align-items: center;
		height: 40px;
		width: 96px;
		border-radius: 99px;
		background-color: rgb(214, 32, 75);
		cursor: pointer;
		color: white;
		font-weight: 600;
	}
</style>
