<template>
	<div id="DL_Img">
		<el-carousel height="650px" class="ec" :autoplay='false'>
			<el-carousel-item v-for="item in detaimg" :key="item" class="eci">
				<img :src="item" />
			</el-carousel-item>
		</el-carousel>
	</div>
</template>

<script>
	import {
		oteurlsa
	} from '../../../network/Deta.js'
	export default {
		name: 'DL_Img',

		data() {
			return {
				detaimg: null
			}
		},

		created() {
			oteurlsa(this.$route.params.showguid).then(res => {
				for (var i = 0; i < res.length; i++) {
					res[i] = res[i].replace('file:///E:', 'http://192.168.80.1')
				}
				this.detaimg = res
			}).catch(err => {})
		}

	}
</script>

<style>
	#DL_Img {
		border-radius: 30px 0px 0px 30px;
		width: 100%;
		height: 100%;
	}

	.ec {
		background-color: white;
		border-radius: 30px 0px 0px 30px;
	}

	.eci {
		display: flex;
		justify-content: center;
		align-items: center;
	}

	.eci img {
		width: 100%;
	}

	#DL_Img .el-carousel {
		--el-carousel-indicator-width: 5px;
	}

	#DL_Img .el-carousel__item:nth-child(2n + 1) {
		background-color: white;
	}

	#DL_Img .el-carousel__item:nth-child(2n) {
		background-color: white;
	}

	.demonstration {
		color: var(--el-text-color-secondary);
	}

	.el-carousel__item h3 {
		color: #475669;
		opacity: 0.75;
		line-height: 150px;
		margin: 0;
		text-align: center;
	}

	.el-carousel__item:nth-child(2n) {
		background-color: #99a9bf;
	}

	.el-carousel__item:nth-child(2n + 1) {
		background-color: #d3dce6;
	}
</style>
