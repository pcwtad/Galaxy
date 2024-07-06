<template>
	<div id="Hrnnotice">
		<el-scrollbar height="100%" :min-size="NaN">
			<div class="Hrnnoticex">
				<div v-for="(item,index) in follows" :key="item" class="Hrnnoticexx">
					<div v-if="isDom" class="Hrnnoticexy">
						<img :src="homeuser[item.followers].headImg" @click="gouser(item.followers)" />
						<div>用户：{{homeuser[item.followers].userName}}</div>
					</div>
					<div v-else class="Hrnnoticexy">
						<img src="../../../../../Img/Home-Lefts/线_稍等.svg" />
						<div>数据加载中...</div>
					</div>
					<div class="Hrnnoticexa">
						<div class="Hrnnoticexb">我{{item.follow}}了你</div>
						<div class="Hrnnoticexd">刚刚</div>
						<div class="Hrnnoticexc" v-if="isfalse[index]"
							@click="gzhu(index,item.beingfollowed,item.followers)">
							回关</div>
						<div class="Hrnnoticexc" @click="qxgz(index,item.beingfollowed,item.followers)" v-else>已关注</div>
						<div v-for="i in retugz(index,item.beingfollowed,item.followers)"></div>
					</div>
				</div>
				<div v-for="(item,index) in huser" :key="item" class="Hrnnoticexx">
					<div v-if="isDoms" class="Hrnnoticexy">
						<img :src="homeusers[item.followers].headImg" @click="gouser(item.followers)" />
						<div>用户：{{homeusers[item.followers].userName}}</div>
					</div>
					<div v-else class="Hrnnoticexy">
						<img src="../../../../../Img/Home-Lefts/线_稍等.svg" />
						<div>数据加载中...</div>
					</div>
					<div class="Hrnnoticexa">
						<div class="Hrnnoticexb">我关注了你</div>
						<div class="Hrnnoticexd">{{item.followTime}}</div>
						<div class="Hrnnoticexc" v-if="isfalse[index]"
							@click="gzhu(index,item.beingfollowed,item.followers)">
							回关</div>
						<div class="Hrnnoticexc" @click="qxgz(index,item.beingfollowed,item.followers)" v-else>已关注</div>
						<div v-for="i in retugz(index,item.beingfollowed,item.followers)"></div>
					</div>
				</div>
			</div>
		</el-scrollbar>
	</div>
</template>

<script>
	import {
		useCounterStore
	} from '../../../../../Pinia/Show.js'
	import {
		lookFollowsa,
		followAsyncsa,
		removeFollowsa,
		getAllFollowsa
	} from '../../../../../network/Follow.js'
	import {
		ObtainUsersa
	} from '../../../../../network/Deta.js'
	import {
		ElMessage
	} from 'element-plus'
	import Cookies from 'js-cookie'
	export default {
		name: 'Hrnnotice',

		data() {
			return {
				follows: null,
				user: [],
				homeuser: {},
				isDom: false,
				timerId: null,
				isfalse: {},
				index: 1,
				myid: null,

				huser: null,
				ishuserfalse: {},
				isDoms: false,
				homeusers: {}

			}
		},

		methods: {
			gouser(id) {
				this.$router.replace(/Home/ + id)
				console.log(id)
			},

			returnuser(id, index) {
				ObtainUsersa(id).then(res => {
					this.user[index] = res[0]
				}).catch(err => {})
			},

			retugz(index, x, y) {
				const follow = {
					"followers": x,
					"beingfollowed": y
				}
				lookFollowsa(follow).then(res => {
					this.isfalse[index] = !res
				})
			},

			gzhu(index, x, y) {
				const follow = {
					"followers": x,
					"beingfollowed": y
				}
				followAsyncsa(follow).then(res => {
					if (res) {
						ElMessage({
							message: '回关成功！',
							type: 'success',
						})
						this.isfalse[index] = !this.isfalse[index]
					} else {
						ElMessage.error('错误！')
					}
				}).catch(err => {})
			},

			qxgz(index, x, y) {
				const follow = {
					"followers": x,
					"beingfollowed": y
				}
				removeFollowsa(follow).then(res => {
					if (res) {
						ElMessage({
							message: '已取关！',
							type: 'warning',
						})
						this.isfalse[index] = !this.isfalse[index]
					} else {
						ElMessage.error('错误！')
					}
				}).catch(err => {})
			}

		},

		created() {
			this.myid = Cookies.get('GalaxyID')

			getAllFollowsa(this.myid, this.index).then(res => {
				this.huser = res
				if (res.length > 0) {
					for (var i = 0; i < res.length; i++) {
						const c = res[i].followers
						ObtainUsersa(c).then(re => {
							const d = {
								"headImg": re[0].headImg,
								"userName": re[0].userName
							}
							this.homeusers[c] = d
						}).catch(err => {})
						this.ishuserfalse[i] = true
					}

					const timeoutId = setTimeout(() => {
						this.isDoms = true
					}, 300);

				}
			})

			//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
			const followcount = useCounterStore()
			this.follows = followcount.follows

			if (followcount.follows.length <= 0) return
			for (var i = 0; i < followcount.follows.length; i++) {
				const b = this.follows[i].followers
				ObtainUsersa(b).then(res => {
					const a = {
						"headImg": res[0].headImg,
						"userName": res[0].userName
					}
					this.homeuser[b] = a
				}).catch(err => {})
				this.isfalse[i] = true
			}
			const timeoutId = setTimeout(() => {
				this.isDom = true
			}, 300);

		}
	}
</script>

<style>
	#Hrnnotice {
		width: 100%;
		height: 100%;
	}

	.Hrnnoticex {
		width: 100%;
		height: 100%;
		display: flex;
		flex-direction: column;
		align-items: center;
	}

	.Hrnnoticexx {
		border-radius: 20px;
		margin-top: 5px;
		width: 95%;
		height: 70px;
		display: flex;
		border: 1px solid #eee;
		align-items: center;
		justify-content: space-between;
		box-shadow: 1px 1px 2px 0px rgba(144, 144, 144, 0.7);
	}

	.Hrnnoticexy {
		display: flex;
	}

	.Hrnnoticexx>div>img:hover {
		cursor: pointer;
	}

	.Hrnnoticexx>div>img {
		border: 1px solid #d9d9d9;
		width: 50px;
		height: 50px;
		border-radius: 50%;
		margin-left: 25px;
	}

	.Hrnnoticexx>div>div {
		margin-left: 20px;
	}

	.Hrnnoticexa {
		display: flex;
	}

	.Hrnnoticexb {
		margin-right: 15px;
		margin-top: 30px;
	}

	.Hrnnoticexc {
		width: 100px;
		height: 40px;
		border-radius: 99px;
		background-color: #d6204b;
		color: white;
		margin-right: 40px;
		display: flex;
		align-items: center;
		justify-content: center;
		font-weight: 400;
		font-size: 16px;
	}

	.Hrnnoticexc:hover {
		cursor: pointer;
	}

	.Hrnnoticexd {
		width: 78px;
		display: flex;
		align-items: center;
		color: #9c9c9c;
		font-size: 14px;
		white-space: nowrap;
		overflow: hidden;
	}
</style>
