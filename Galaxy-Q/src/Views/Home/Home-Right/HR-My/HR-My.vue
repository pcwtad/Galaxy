<template>
	<div id="HR_My">
		<el-scrollbar height="100%" @scroll="Newscroll($event)" :min-size="NaN">
			<div class="MyFind">
				<HRMTop></HRMTop>
				<HRMBottom ref="child"></HRMBottom>
			</div>
		</el-scrollbar>
		<router-view></router-view>
	</div>
</template>

<script>
	import HRMTop from './HRM-Top/HRMTop.vue'
	import HRMBottom from './HRM-Bottom/HRMBottom.vue'
	import throttle from '../../../../MyJs/throttle.js'
	export default {
		name: 'HR_My',

		components: {
			HRMTop,
			HRMBottom
		},

		methods: {
			Newscroll: throttle(function(e) {
				let element = this.$el.querySelector('.MyFind').offsetHeight - 670
				if (e.scrollTop + 100 >= element) {
					this.$refs.child.Popup();
				}
			}, 500),
		},

	}
</script>

<style>
	#HR_My {
		width: 100%;
		height: 41.8em;
	}
</style>
