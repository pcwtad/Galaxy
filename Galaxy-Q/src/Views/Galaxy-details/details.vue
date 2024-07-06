<template>
	<div id="detailss">
		<div id="deta-opacity" @click="goback"></div>
		<div id="deta-text">
			<div class="dt-img">
				<DLImg></DLImg>
			</div>
			<div class="dt-text">
				<DLText></DLText>
			</div>
		</div>
		<div class="deta-X" @click="goback">
			<span>×</span>
		</div>
	</div>
</template>

<script>
	import DLImg from './deta-left/DL_Img.vue'
	import DLText from './deta-right/DL_Text.vue'

	export default {
		name: 'detailss',

		components: {
			DLImg,
			DLText
		},

		methods: {
			goback() {
				this.$router.back()
			},
		},


	}
</script>

<style>
	.dt-img {
		width: 500px;
		height: 100%;
		border-radius: 30px 0px 0px 30px;
		background-color: #000000;
	}

	#detailss {
		position: absolute;
		width: 100%;
		height: 100%;
		top: 0;
		left: 0;
		right: 0;
		bottom: 0;
	}

	#deta-opacity {
		width: 100%;
		height: 100%;
		top: 0;
		left: 0;
		right: 0;
		bottom: 0;
		background-color: #000000;
		opacity: .3;
	}

	#deta-text {
		position: absolute;
		top: 0;
		left: 0;
		right: 0;
		bottom: 0;
		margin: auto;
		background-color: #fff;
		border-radius: 30px;
		width: 910px;
		display: flex;
		height: 650px;
	}

	.deta-X {
		position: absolute;
		top: 15px;
		left: 15px;
		color: #fff;
		vertical-align: middle;
		width: 40px;
		height: 40px;
		background-color: #8f8f8f;
		border-radius: 50%;
		display: flex;
		align-items: center;
		justify-content: center;
		cursor: pointer;
	}

	.deta-X span {
		font-size: 20px;
	}
</style>
