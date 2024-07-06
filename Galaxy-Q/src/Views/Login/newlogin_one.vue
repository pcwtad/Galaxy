<template>
	<div id="newloginone">
		<div class="loginone">
			<div class="loginonex">
				<div @click="passwd">密码登录</div>
				<div @click="code">验证码登录</div>
			</div>
			<div class="loginoney">
				<div>
					<span>邮箱：</span>
					<input type="text" v-model="mail" />
				</div>
				<div v-show="!isdiv">
					<span>密码：</span>
					<input type="text" v-model="userpasswod" />
				</div>
				<div v-show="isdiv" class="logintwocode">
					<input type="text" v-model="codes" />
					<div :class="iscode?'codediv':''" @click="fs">{{iscode?Math.trunc(countDownTime/1000)+'s':'发送'}}
					</div>
				</div>
				<div class="dl" @click="dl">登录</div>
			</div>
		</div>
		<div class="qzc" @click="qzc">去注册></div>
	</div>
</template>

<script>
	import {
		sendcode,
		passwdlogin,
		codelogin
	} from '../../network/Login.js'
	import Cookies from 'js-cookie'
	export default {
		name: 'newloginone',

		data() {
			return {
				isdiv: false,
				mail: '',
				iscode: false,
				countDownTime: 120000,
				timer: null,
				userpasswod: '',
				codes: ''
			}
		},

		methods: {
			qzc() {
				this.$router.push('./Logintwo');
			},

			dl() {
				if (this.isdiv) {
					codelogin(this.mail, this.codes).then(res => {
						if (res.length == 0) {
							alert("登录失败")
						} else {
							Cookies.set('GalaxyJWT', res.jwt, {
								expires: 7
							});
							Cookies.set('GalaxyID', res.id, {
								expires: 7
							});
							this.$router.replace('/Home/HR_Find')
						}
					}).catch(err => {})
				} else {
					passwdlogin(this.mail, this.userpasswod).then(res => {
						if (res.length == 0) {
							alert("登录失败")
						} else {
							Cookies.set('GalaxyJWT', res.jwt, {
								expires: 7
							});
							Cookies.set('GalaxyID', res.id, {
								expires: 7
							});
							this.$router.replace('/Home/HR_Find')
						}
					}).catch(err => {})
				}
			},

			passwd() {
				this.isdiv = false;
			},

			code() {
				this.isdiv = true;
			},

			fs() {
				if (this.iscode) return;
				let DateCode = new Date(new Date().getTime() + 120000);
				Cookies.set("CookiesTime", DateCode, {
					expires: DateCode
				})
				let cookiesTime = Date.parse(DateCode);
				this.timer = setInterval(() => {
					let nowTime = Date.now();
					this.countDownTime = cookiesTime - nowTime;
					if (this.countDownTime <= 0) {
						this.countFinish();
					}
				}, 1000);
				this.iscode = true;

				sendcode(this.mail).then(res => {
					alert("发送成功");
				}).catch(err => {
					alert("发送失败");
				})
			},

			countFinish() {
				clearInterval(this.timer);
				this.countDownTime = 120000;
				this.iscode = false;
			},

		},

		mounted() {
			let cookiesTime = Date.parse(Cookies.get('CookiesTime'));
			if (cookiesTime) {
				this.iscode = true;
				this.timer = setInterval(() => {
					let nowTime = Date.now();
					this.countDownTime = cookiesTime - nowTime;
					if (this.countDownTime <= 0) {
						this.countFinish();
					}
				}, 1000);
			} else {
				this.countFinish();
			}
		},

	}
</script>

<style scoped>
	#newloginone {
		width: 100%;
		height: 100%;
		display: flex;
		align-items: center;
		justify-content: center;
		flex-direction: column;
	}

	.loginone>div {
		padding: 10px 0px;
	}

	.loginonex {
		display: flex;
		justify-content: space-around;
	}

	.loginonex>div {
		display: flex;
		align-items: center;
		justify-content: center;
		height: 30px;
		background-color: #d6204b;
		width: 45%;
		color: white;
		border-radius: 7px;
		cursor: pointer;
	}

	.loginoney>div {
		padding: 5px 0px;
		letter-spacing: 3px;
	}

	input {
		border: none;
		outline: none;
		border: 1px solid #bfbfbf;
		border-radius: 5px;
		height: 30px;
	}

	.dl {
		width: 80%;
		border: 1px solid #d6204b;
		margin: auto;
		display: flex;
		align-items: center;
		justify-content: center;
		color: #d6204b;
		margin-top: 5px;
		cursor: pointer;
		letter-spacing: 7px;
		border-radius: 10px;
	}

	.dl:hover {
		background-color: #d6204b;
		color: white;
	}

	.logintwocode {
		display: flex;
		justify-content: space-between;
	}

	.logintwocode>div {
		display: flex;
		align-items: center;
		justify-content: center;
		width: 60px;
		background-color: #d6204b;
		color: white;
		border-radius: 5px;
		letter-spacing: 2px;
		cursor: pointer;
	}

	.logintwocode>input {
		width: 140px;
	}

	.qzc {
		cursor: pointer;
		color: #d6204b;
		margin-top: 25px;
		margin-left: 160px;
	}

	.codediv:hover {
		cursor: not-allowed;
	}
</style>
