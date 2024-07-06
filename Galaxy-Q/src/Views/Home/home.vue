<template>
	<div id="home">
		<GTops></GTops>
		<div class="G_Top_x72"></div>
		<div class="G_Bottom">
			<GLefts></GLefts>
			<GRights></GRights>
		</div>
	</div>
</template>

<script>
	import GTops from './Home-Top/G-Top_s.vue'
	import GLefts from './Home-Left/G-Left_s.vue'
	import GRights from './Home-Right/G-Right_s.vue'
	export default {
		name: 'home',
		components: {
			GTops,
			GLefts,
			GRights
		}
	}
</script>

<style>
	#home {
		width: 100%;
		height: 100vh;
	}

	@media screen and (max-width: 900px) {
		#home {
			width: 900px;
		}
	}

	.G_Top_x72 {
		height: 72px;
	}

	.G_Bottom {
		width: 100%;
		display: flex;
	}
</style>
