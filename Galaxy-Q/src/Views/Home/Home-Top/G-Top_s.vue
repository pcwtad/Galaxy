<template>
	<div id="G_Top_s">
		<div class="G_Top_x">
			<span id="sign">Galaxy</span>
			<div id="search-for">
				<el-input v-model="input1" id="search-input" class="w-50 m-2" size="large" placeholder="登录探索更多内容" />
			</div>
			<div id="create-text">
				<el-button round>创作中心</el-button>
			</div>
		</div>
	</div>
</template>

<script>
	export default {
		name: 'G_Top_s',
		data() {
			return {
				input1: ''
			}
		}
	}
</script>

<style>
	#G_Top_s {
		position: fixed;
		background-color: white;
		border-bottom: 1px solid #eee;
		height: 72px;
		width: 100%;
	}

	@media screen and (max-width: 800px) {
		#G_Top_s {
			width: 800px;
		}
	}

	.G_Top_x {
		display: flex;
		align-items: center;
		justify-content: space-between;
		height: 100%;
		width: 100%;
	}

	#sign {
		margin-left: 30px;
		font-family: "阿里妈妈东方大楷 Regular";
		font-weight: 400;
		src: url("//at.alicdn.com/wf/webfont/p5oIvlAb019v/OGIqjhXIGOVg.woff2") format("woff2"),
			url("//at.alicdn.com/wf/webfont/p5oIvlAb019v/lzFpsDyWRBPX.woff") format("woff");
		font-display: swap;
		color: rgb(214, 32, 75);
		font-size: 40px;
	}

	@font-face {
		font-family: "阿里妈妈东方大楷 Regular";
		font-weight: 400;
		src: url("//at.alicdn.com/wf/webfont/p5oIvlAb019v/OGIqjhXIGOVg.woff2") format("woff2"),
			url("//at.alicdn.com/wf/webfont/p5oIvlAb019v/lzFpsDyWRBPX.woff") format("woff");
		font-display: swap;
	}

	#search-for .el-input__wrapper {
		border-radius: 90px;
	}

	#search-for .el-input {
		--el-input-focus-border-color: rgb(214, 32, 75);
	}

	#search-input {
		width: 450px;
		height: 40px;
	}

	#create-text .el-button {
		border-color: #ffffff;
		--el-button-hover-text-color: rgb(214, 32, 75);
		--el-button-hover-bg-color: #f7f7f7;
		--el-button-hover-border-color: rgb(214, 32, 75);
		background-color: #f7f7f7;
		height: 40px;
		width: 97px;
	}

	#create-text {
		margin-right: 30px;
	}
</style>
