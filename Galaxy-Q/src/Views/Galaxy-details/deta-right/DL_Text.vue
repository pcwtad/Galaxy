<template>
	<div id="DL_Text">
		<el-scrollbar height="100%">
			<DTItem></DTItem>
		</el-scrollbar>
	</div>
</template>

<script>
	import DTItem from './DT_Item.vue'

	export default {
		name: 'DL_Text',

		components: {
			DTItem
		},

	}
</script>

<style>
	#DL_Text {
		position: relative;
		width: 410px;
		height: 100%;
		border-radius: 0px 30px 30px 0px;
		background-color: #ffffff;
	}
</style>
