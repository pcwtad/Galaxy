<template>
	<div id="FrientChat">
		<div class="Frientshow">
			<el-scrollbar class="Frientshowscr" height="100%" @scroll="Newscroll($event)" :min-size="NaN" ref="contentDatas">
				<div class="Frientshows" ref="element">
					<div class="spanjzai" v-text="isdiv?'上划加载更多':'没有更多了'"></div>
					<div v-for="(item,index) in chattext">
						<div
							v-if="item.formUserId==paramsid&&item.toUserId==this.$route.params.id||item.toUserId==paramsid&&item.formUserId==this.$route.params.id"
							:class="paramsid==item.formUserId?'paramsidb':'paramsida'">
							<img :src="paramsid==item.formUserId?myuser.headImg:newuser.headImg" />
							<div></div>
							<span v-text="item.message"></span>
							<h6 v-text="paramsid==item.formUserId?myuser.userName:newuser.userName"></h6>
						</div>
					</div>
				</div>
			</el-scrollbar>
		</div>
		<div class="Frientsend">
			<div class="Frientsenda">
				<textarea v-model="text" @input="checkLength">
				</textarea>
			</div>
			<div class="Frientsendb">
				<div class="Frientsendbx" :style="textLength==100?'color: #d6204b;':''">{{textLength}}/100</div>
				<div class="Frientsendby" @click="send">发送</div>
			</div>
		</div>
	</div>
</template>

<script>
	import * as signalR from '@microsoft/signalr';
	import {
		useCounterStore
	} from '../../../../../Pinia/Show.js'
	import {
		SendChats
	} from '../../../../../network/chat.js'
	import Cookies from 'js-cookie'
	export default {
		name: 'FrientChat',

		data() {
			return {
				useStores: null,
				text: '',
				connection: null,
				chattext: [],
				paramsid: null,

				myuser: null,
				newuser: null,
				index: 1,
				isdiv: true
			}
		},

		watch: {
			'$route'(to, from) {
				//清空所有数据
				this.useStores = null
				this.text = ''
				this.connection = null
				this.chattext = []
				this.paramsid = null
				this.index = 1
				this.isdiv = true
				// 当路由改变时，重置数据  
				this.startConnection();
				console.log(this.$route.params.id)
				const useStore = useCounterStore()
				this.myuser = useStore.myuser
				this.newuser = useStore.newuser
				this.useStores = useStore.connection
				this.paramsid = Cookies.get('GalaxyID')


				SendChats(Cookies.get('GalaxyID'), this.$route.params.id, this.index).then(res => {
					this.chattext.push(...res)
					if (res.length != 20) {
						this.isdiv = false
					}
					this.index++
					const timeoutId = setTimeout(() => {
						this.$refs.contentDatas.scrollTo(0, this.$refs.element.offsetHeight)
					}, 100);
				}).catch(err => {})

			}
		},

		computed: {
			//返回输入字符
			textLength() {
				return this.text.length;
			},
		},

		methods: {
			//加载更多聊天记录
			Newscroll(item) {
				if (item.scrollTop == 0 && this.isdiv == true) {
					SendChats(Cookies.get('GalaxyID'), this.$route.params.id, this.index).then(res => {
						this.chattext.unshift(...res)
						if (res.length != 20) {
							this.isdiv = false
						}
						this.index++
					}).catch(err => {})
				}
			},

			//字符限制输入
			checkLength() {
				if (this.text > 100) {
					//截断超过100的部分
					this.text = this.text.slice(0, 100);
				}
			},

			//发送
			send() {
				if (this.text.length <= 0) {
					alert("请输入要发送的内容")
					return
				}
				this.connection.invoke('SendMsgToUser', Cookies.get('GalaxyID'), this.$route.params.id, this.text)
				this.text = ''
			},

			//开始连接
			startConnection() {
				var options = {
					skipNegotiation: true,
					transport: signalR.HttpTransportType.WebSockets
				};
				//this.JWTkey你的JWT令牌
				// options.accessTokenFactory = () => Cookies.get('gJwt');
				//创建连接
				this.connection = new signalR.HubConnectionBuilder()
					.withUrl('https://localhost:7189/ChatHub?userId=' + Cookies.get('GalaxyID'))
					.withAutomaticReconnect() //断开自动连接
					.build();

				//注册
				this.connection.on('ChatMessage', (res) => {
					this.chattext.push(res)

					const timeoutId = setTimeout(() => {
						this.$refs.contentDatas.scrollTo(0, this.$refs.element.offsetHeight)
					}, 100);
				});

				//开始连接
				this.connection.start();
			},
		},

		created() {
			this.startConnection();
			const useStore = useCounterStore()
			this.myuser = useStore.myuser
			this.newuser = useStore.newuser
			this.useStores = useStore.connection
			this.paramsid = Cookies.get('GalaxyID')


			SendChats(Cookies.get('GalaxyID'), this.$route.params.id, this.index).then(res => {
				this.chattext.push(...res)
				this.index++
				if (res.length != 20) {
					this.isdiv = false
				}
				const timeoutId = setTimeout(() => {
					this.$refs.contentDatas.scrollTo(0, this.$refs.element.offsetHeight)
				}, 100);
			}).catch(err => {})

		},

		updated() {
			this.$nextTick(() => {
				this.$refs.contentDatas.scrollTo(0, this.$refs.element.offsetHeight)
			});
		}

	}
</script>

<style scoped>
	#FrientChat {
		width: 100%;
		height: 100%;
		/* display: flex;
		flex-direction: column; */
	}

	textarea {
		width: 99.5%;
		height: 100%;
		border: none;
		font-size: 18px;
		resize: none;
	}

	textarea:focus {
		border: none;
		outline: none;
	}

	.Frientsend {
		width: 100%;
		height: 30%;
		display: flex;
		flex-direction: column;
	}

	.Frientsenda {
		width: 100%;
		height: 85%;
	}

	.Frientsendb {
		width: 100%;
		height: 50px;
		display: flex;
		justify-content: space-between;
		align-items: center;
	}

	.Frientsendbx {
		margin-left: 20px;
		font-size: 14px;
		font-weight: 300;
		color: #9f9f9f;
		margin-top: 20px;
	}

	.Frientsendby {
		width: 100px;
		height: 40px;
		border-radius: 50px;
		background-color: #d6204b;
		color: white;
		display: flex;
		justify-content: center;
		align-items: center;
		margin-right: 20px;
	}

	.Frientsendby:hover {
		cursor: pointer;
	}

	.Frientshow {
		width: 100%;
		height: 70%;
		border-bottom: 1px solid #dadada;
	}

	.Frientshowscr {
		width: 100%;
		height: 100%;
	}

	.Frientshows {
		width: 100%;
		height: 100%;
	}

	.Frientshows>div {
		width: 100%;
		height: 100%;
	}

	.paramsida {
		padding: 0px 20px 0px 20px;
		color: black;
		display: flex;
		margin-top: 10px;
		position: relative;
	}

	.paramsida>img {
		width: 35px;
		height: 35px;
		border: 1px solid #d8d8d8;
		border-radius: 50%;
	}

	.paramsida>span {
		background-color: #eaeaea;
		padding: 5px 8px;
		display: inline-block;
		border-radius: 4px;
		margin: 10px 0px 10px 0px;
		position: relative;
		max-width: 80%;
		word-wrap: break-word;
	}

	.paramsida>div {
		width: 0px;
		height: 0px;
		border: 8px solid;
		border-color: white #eaeaea white white;
		margin-top: 14px;
	}

	.paramsida>h6 {
		font-weight: 400;
		color: #6a6a6a;
		position: absolute;
		top: -32px;
		left: 70px;
	}

	.paramsidb {
		color: white;
		padding: 0px 20px 0px 20px;
		display: flex;
		flex-direction: row-reverse;
		margin-top: 10px;
		position: relative;
	}

	.paramsidb>img {
		width: 35px;
		height: 35px;
		border: 1px solid #d8d8d8;
		border-radius: 50%;
	}

	.paramsidb>h6 {
		position: absolute;
		font-weight: 400;
		color: #6a6a6a;
		top: -32px;
		right: 78px;
	}

	.paramsidb>span {
		border-radius: 50px;
		background-color: #d6204b;
		padding: 5px 8px;
		display: inline-flex;
		border-radius: 6px;
		margin: 10px 0px 10px 0px;
		max-width: 80%;
		word-wrap: break-word;
	}

	.paramsidb>div {
		width: 0px;
		height: 0px;
		border: 8px solid;
		border-color: white white white #d6204b;
		margin-top: 15px;
	}

	.spanjzai {
		width: 100%;
		display: flex;
		align-items: center;
		justify-content: center;
		color: #797979;
	}
</style>

