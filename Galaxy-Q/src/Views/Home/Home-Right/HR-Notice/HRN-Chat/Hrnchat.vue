<template>
	<div id="Hrnchat">
		<div class="Friend">
			<el-scrollbar style="width: 100%;" height="100%" :min-size="NaN">
				<div v-if="isDom" class="friendif">
					<loading></loading>
				</div>
				<div v-else class="friendel">
					<div v-for="item in user" @click="gochat(item.beiid,item)" :class="isid==item.beiid?'friendfor':''">
						<img :src="item.headImg">
						<div>{{item.userName}}</div>
					</div>
				</div>
			</el-scrollbar>
		</div>
		<div class="record">
			<router-view></router-view>
		</div>
	</div>
</template>

<script>
	import loading from '../../../../../MyJs/logCSSing.vue'
	import {
		GetBeingfollowedsa
	} from '../../../../../network/Follow.js'
	import {
		ObtainUsersa
	} from '../../../../../network/Deta.js'
	import {
		useCounterStore
	} from '../../../../../Pinia/Show.js'
	import Cookies from 'js-cookie'
	export default {
		name: 'Hrnchat',

		data() {
			return {
				myid: null,
				userid: null,
				user: {},
				isDom: false,
				isid: 0
			}
		},

		components: {
			loading
		},

		methods: {
			gochat(id, item) {
				const useStore = useCounterStore()
				useStore.setnewuser(item)
				this.isid = id
				this.$router.push('/Home/HR_Notice/Hrnchat/' + id)
			}
		},

		created() {
			this.myid = Cookies.get('GalaxyID')
			GetBeingfollowedsa(this.myid).then(res => {
				this.userid = res
			}).catch(err => {})


			const timeoutId = setTimeout(() => {
					for (var i = 0; i < this.userid.length; i++) {
						const id = this.userid[i]
						ObtainUsersa(id).then(res => {
							const a = {
								"headImg": res[0].headImg,
								"userName": res[0].userName,
								"beiid": id
							}
							this.user[id] = a
						}).catch(err => {})
					}
				},
				300);

			const timeoutIds = setTimeout(() => {
				this.isDoms = true
			}, 400);

		}
	}
</script>

<style>
	#Hrnchat {
		width: 100%;
		height: 100%;
		display: flex;
	}

	.Friend {
		height: 100%;
		width: 20%;
		border-right: 1px solid #cecece;
	}

	.friendel {
		width: 100%;
		display: flex;
		flex-direction: column;
		align-items: center;
	}

	.friendel>div {
		height: 70px;
		width: 95%;
		margin-top: 5px;
		border-radius: 30px;
		display: flex;
		align-items: center;
	}

	.friendel>div:hover {
		cursor: pointer;
		background-color: #e8e8e8;
	}

	.friendfor {
		background-color: #ebebeb;
		box-shadow: 1px 1px 2px 0px rgba(175, 175, 175, 0.7);
	}

	.friendel>div>img {
		width: 45px;
		height: 45px;
		border: 1px solid #cbcbcb;
		border-radius: 50%;
		margin-left: 25px;
	}

	.friendel>div>div {
		margin-bottom: 25px;
		margin-left: 10px;
		font-weight: 300;
		color: #747474;
	}

	.record {
		width: 80%;
	}
</style>