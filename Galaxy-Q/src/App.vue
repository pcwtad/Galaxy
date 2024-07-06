<script>
	import * as signalR from '@microsoft/signalr';
	import Cookies from 'js-cookie'
	import {
		useCounterStore
	} from './Pinia/Show.js'
	export default {
		data() {
			return {
				connection: null,
				counterStores: null
			}
		},

		methods: {
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
					.withUrl('https://localhost:7189/Myhub?userId=' + Cookies.get('GalaxyID')) // 替换为你的SignalR服务URL，携带请求头发送
					.withAutomaticReconnect() //断开自动连接
					.build();

				//注册
				this.connection.on('ReceiveMessage', (res) => {
					this.counterStores.followconnection(res)
					console.log(res)
				});

				//开始连接
				this.connection.start();
			},
		},
		created() {
			this.startConnection();
			const ChatcounterStores = useCounterStore()
			this.counterStores = ChatcounterStores
			ChatcounterStores.setconnection(this.connection)
		}
	}
</script>

<template>
	<div>
		<router-view></router-view>
	</div>
</template>

<style>
	body {
		padding: 0;
		margin: 0;
	}
</style>