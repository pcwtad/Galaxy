<template>
	<div id="DT_Item">
		<div class="DTItema">
			<div class="DTItemaa" v-for="(item,index) in user" :key="item">
				<div class="DTItemaaa">
					<img @click="gouser" :src="item.headImg" />
					<span @click="gouser">{{item.userName}}</span>
				</div>
				<div class="DTItemaab" v-text="isfollow?'已关注':'关注'" @click="followcilck">
				</div>
			</div>
		</div>
		<div class="DTIscro" @click.stop="quxiaohf">
			<el-scrollbar height="100%" :min-size="NaN">
				<div class="scrilldiva">
					<div class="DTItemb">
						<div v-text="show.showbrin" class="DTItemba"></div>
						<div v-text="show.showinformation" class="DTItembb"></div>
						<div class="DTItembc">
							<div v-text="show.showDatetime.substring(0, 10)"></div>
							<div v-text="show.showwhere" style="margin-left: 10px;"></div>
						</div>
					</div>
					<div class="DTIComment">
						<Commenttext @childtoparent="handleValues"></Commenttext>
					</div>
				</div>
			</el-scrollbar>
		</div>
		<div class="DTIText">
			<div class="inputtext">
				<input type="text" v-model="sendtext" :placeholder="placeholdertext" />
			</div>
			<div class="sendtext" @click="sendComment">发送</div>
		</div>
	</div>
</template>

<script>
	import {
		ElMessage
	} from 'element-plus'
	import {
		useCounterStore
	} from '../../../Pinia/Show.js'
	import {
		ObtainUsersa
	} from '../../../network/Deta.js'
	import {
		lookFollowsa,
		followAsyncsa,
		removeFollowsa
	} from '../../../network/Follow.js'
	import {
		AddParenteveAsyncsa,
		AddChildeveAsyncsa
	} from '../../../network/Comment.js'
	import Cookies from 'js-cookie'
	import Commenttext from './Commenttext.vue'
	export default {
		name: 'DT_Item',

		data() {
			return {
				show: null,
				user: null,
				isfollow: false,
				follow: {
					followers: '',
					beingfollowed: ''
				},

				sendtext: '',
				placeholdertext: '',

				parenteveid: '',
				coverId: ''
			}
		},

		components: {
			Commenttext
		},

		methods: {

			quxiaohf() {
				this.placeholdertext = ""
			},

			handleValues(parenteveid, userid, name) {
				this.placeholdertext = "回复@" + name
				this.parenteveid = parenteveid
				this.coverId = userid
			},

			sendComment() {
				if (this.sendtext.length == 0) {
					alert("请输入发送内容")
					return
				}
				if (this.placeholdertext == "") { //发送顶级评论
					const comment = {
						"showId": this.$route.params.showguid,
						"userId": Cookies.get('GalaxyID'),
						"title": this.sendtext
					}
					//评论顶级评论
					AddParenteveAsyncsa(comment).then(res => {
						if (res) {
							ElMessage({
								message: '评论成功！',
								type: 'success',
							})
						}
					}).catch(err => {})
				} else { //发送二级评论
					const comment = {
						"parenteveId": this.parenteveid,
						"userId": Cookies.get('GalaxyID'),
						"coverId": this.coverId,
						"title": this.sendtext
					}
					AddChildeveAsyncsa(comment).then(res => {
						console.log(res)
						if (res) {
							ElMessage({
								message: '评论成功！',
								type: 'success',
							})
						}
					}).catch(err => {})
				}

				this.placeholdertext = ''
				this.sendtext = ''
			},

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

			gouser() {
				this.$router.replace(/Home/ + this.show.userId)
			}

		},

		created() {
			const counterStore = useCounterStore()
			this.show = counterStore.count
			this.follow.beingfollowed = this.show.userId
			this.follow.followers = Cookies.get('GalaxyID')
			ObtainUsersa(this.show.userId).then(res => {
				this.user = res
			}).catch(err => {})

			lookFollowsa(this.follow).then(res => {
				this.isfollow = res
			}).catch(err => {})
		}

	}
</script>

<style scoped>
	#DT_Item {
		width: 100%;
		height: 100%;
		/* position: relative; */
	}

	.scrilldiva {
		display: flex;
		flex-direction: column;
		align-items: center;
	}

	.DTItemaa {
		width: 90%;
		height: 100%;
		display: flex;
		justify-content: space-between;
		align-items: center;
	}

	.DTItemaaa {
		display: flex;
		align-items: center;
	}

	.DTItemaa>div>img {
		border: 1px solid #e3e3e3;
		border-radius: 50%;
		width: 40px;
		height: 40px;
	}

	.DTItemaa>div>img:hover {
		cursor: pointer;
	}

	.DTItemaa>div>span:hover {
		cursor: pointer;
	}

	.DTItemaa>div>span {
		margin-left: 10px;
		color: #7f7f7f;
		font-weight: 300;
	}

	.DTItemaab {
		display: flex;
		justify-content: center;
		align-items: center;
		border-radius: 50px;
		background-color: #d6204b;
		color: white;
		width: 96px;
		height: 40px;
	}

	.DTItemaab:hover {
		cursor: pointer;
	}

	.DTItema {
		/* position: absolute; */
		height: 72px;
		width: 100%;
		border-bottom: 1px solid #dedede;
		display: flex;
		justify-content: center;
		align-items: center;
	}

	.DTItemb {
		margin-top: 5px;
		width: 90%;
		border-bottom: 1px solid #dedede;
	}

	.DTItemba {
		font-weight: 600;
		font-size: 24px;
	}

	.DTItembb {
		margin-top: 7px;
	}

	.DTItembc {
		margin-top: 8px;
		display: flex;
		height: 35px;
		font-size: 14px;
		font-weight: 400;
		color: #9d9d9d;
	}

	.DTIComment {
		width: 100%;
		height: 100%;
	}

	.DTIscro {
		width: 100%;
		height: 510px;
	}

	.DTIText {
		border-top: 1px solid #d0d0d0;
		width: 100%;
		height: 62px;
		border-radius: 0px 0px 30px 0px;
		display: flex;
		justify-content: space-between;
		align-items: center;

	}

	.inputtext {
		margin-left: 15px;
		border-radius: 50px;
		height: 40px;
		width: 280px;
		display: flex;
		justify-content: center;
		align-items: center;
		background-color: #eee;
	}

	.inputtext>input {
		border: none;
		width: 90%;
		height: 90%;
		border-radius: 50px;
		background-color: #eee;
	}

	.inputtext>input:focus {
		outline: none;
	}

	.sendtext {
		width: 80px;
		height: 40px;
		border-radius: 50px;
		background-color: #d6204b;
		display: flex;
		align-items: center;
		justify-content: center;
		color: white;
		margin-right: 15px;
	}

	.sendtext:hover {
		cursor: pointer;
	}
</style>