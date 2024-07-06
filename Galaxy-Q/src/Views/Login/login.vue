<template>
	<div id="login">
		<div class="returntop">
			<span @click="GetX">×</span>
		</div>
		<div class="loginbox">
			<div class="loginboxleft">
				<img src="../../Img/Login/讨论.svg" />
			</div>
			<div class="loginboxright">
				<router-view></router-view>
			</div>
		</div>
	</div>
</template>

<script>
	export default {
		name: 'login',

		methods: {

			GetX() {
				this.$router.replace('/Home')
			}

		},

	}
</script>

<style>
	#login {
		display: flex;
		width: 100%;
		height: 100vh;
		background-image: linear-gradient(to right top, #d6204b, #e3546d, #ee7a8e, #f59eae, #fac0cc, #f5c5cf, #efc9d2, #eaced5, #dbb7c0, #cca0ac, #bd8998, #ae7384);
		position: relative;
		align-items: center;
		justify-content: center;
	}

	@media screen and (max-width: 800px) {
		#login {
			width: 800px;
		}
	}

	@media screen and (max-height: 500px) {
		#login {
			height: 500px;
		}
	}

	.returntop {
		top: 10px;
		left: 10px;
		width: 50px;
		height: 50px;
		background-color: white;
		border-radius: 50%;
		position: absolute;
		display: flex;
		align-items: center;
		justify-content: center;
		cursor: pointer;
	}

	.returntop>span {
		width: 100%;
		height: 100%;
		font-size: 50px;
		display: flex;
		margin-bottom: 22px;
		justify-content: center;
	}

	.loginbox {
		width: 600px;
		height: 400px;
		background-color: white;
		border-radius: 30px;
		display: flex;
	}

	.loginboxleft {
		flex: 1;
		margin: auto;
	}

	.loginboxright {
		flex: 1;
	}
</style>
