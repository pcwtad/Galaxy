<template>
	<div id="newlogintwo">
		<div class="newlogintinp">
			<div>
				<span>昵称：</span>
				<input type="text" placeholder="输入昵称" v-model="user.userName" />
			</div>
			<div>
				<span>密码：</span>
				<input type="text" placeholder="输入密码" v-model="user.userPasswod" />
			</div>
			<div>
				<span>邮箱：</span>
				<input type="text" v-model="user.usermail" placeholder="输入邮箱" />
			</div>
			<div class="logincode">
				<input type="text" placeholder="输入验证码" v-model="code" />
				<div :class="iscode?'codediv':''" @click="fs">{{iscode?Math.trunc(countDownTime/1000)+'s':'发送'}}</div>
			</div>
			<div class=" logincilck" @click="zc">注册</div>
		</div>
		<div class="qdl" @click="qdl">
			<span>去登录></span>
		</div>
	</div>
</template>

<script>
	import {
		sendcode,
		userregis
	} from '../../network/Login.js'
	import Cookies from 'js-cookie'
	export default {
		name: 'newlogintwo',

		data() {
			return {
				iscode: false,
				countDownTime: 120000,
				timer: null,

				user: {
					'userName': '',
					'usermail': '',
					'userPasswod': ''
				},
				code: ''
			}
		},

		methods: {

			qdl() {
				this.$router.push('./Loginone');
			},

			zc() {
				userregis(this.user, this.code).then(res => {
					if (res = true) {
						alert("注册成功")
					} else {
						alert("注册失败")
					}
				}).catch(err => {
					console.log(err)
				})
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

				sendcode(this.user.usermail).then(res => {
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
		}

	}
</script>

<style scoped>
	#newlogintwo {
		width: 100%;
		height: 100%;
		display: flex;
		align-items: center;
		justify-content: center;
		flex-direction: column;
	}

	.newlogintinp {
		display: flex;
		flex-direction: column;
	}

	.newlogintinp>div {
		padding: 5px 0px;
	}

	.newlogintinp>div>span {
		letter-spacing: 3px;
	}

	.logincode {
		display: flex;
		justify-content: space-between;
	}

	.logincode>input {
		width: 140px;
	}

	.logincode>div {
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

	.logincilck {
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

	.logincilck:hover {
		background-color: #d6204b;
		color: white;
	}

	input {
		border: none;
		outline: none;
		border: 1px solid #bfbfbf;
		border-radius: 5px;
		height: 30px;
	}

	.qdl {
		cursor: pointer;
		color: #d6204b;
		margin-top: 25px;
		margin-left: 160px;
	}

	.codediv:hover {
		cursor: not-allowed;
	}
</style>
