<template>
	<div id="HR-Find">
		<HRFindItem></HRFindItem>
		<HRFindBottom></HRFindBottom>
	</div>
</template>

<script>
	import HRFindItem from './HRF-top/HRF-Top.vue'
	import HRFindBottom from './HRF-bottom/HRF-Bottom.vue'
	export default {
		name: 'HR-Find',

		components: {
			HRFindItem,
			HRFindBottom
		},

	}
</script>

<style>
</style>
