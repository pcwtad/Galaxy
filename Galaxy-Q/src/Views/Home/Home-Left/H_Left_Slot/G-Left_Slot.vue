<template>
	<div id="G_Left_Slot" @click="ItemClick" :style="activeStyle">
		<div id="G_Slot_Img">
			<slot name="GL_Slot_Img"></slot>
		</div>
		<div id="G_Slot_Text">
			<slot name="GL_Slot_Text"></slot>
		</div>
		<div id="G_Slot_xhd" v-if="path=='/Home/HR_Notice'&&isfollow==false">
			<slot name="G_Slot_xhd"></slot>
		</div>
	</div>
</template>

<script>
	import {
		useCounterStore
	} from '../../../../Pinia/Show.js'
	import {
		watch,
		ref
	} from 'vue'
	export default {
		name: 'G_Left_Slot',
		props: {
			path: String,
			activeColor: String
		},

		data() {
			return {
				a: true
			}
		},

		setup() {
			const isfollow = ref(true)
			const followcount = useCounterStore()
			watch(() => followcount.follow, (newValue, oldValue) => {
				isfollow.value = false
			})

			return {
				isfollow
			}
		},

		computed: {
			isActive() {
				return !this.$route.path.indexOf(this.path)
			},
			activeStyle() {
				return this.isActive ? {
					background: this.activeColor
				} : {}
			}
		},
		methods: {
			ItemClick() {
				this.$router.replace(this.path)
				if (this.path == "/Home/HR_Notice") {
					this.isfollow = true
				}
			}
		}
	}
</script>

<style>
	#G_Left_Slot {
		display: flex;
		width: 70%;
		height: 44px;
		border-radius: 90px;
		padding-left: 17px;
		margin-top: 15px;
		font-size: 16px;
		font-weight: 600;
		color: #7e7e7e;
		margin-left: 30px;
		cursor: pointer;
	}

	#G_Left_Slot:hover {
		background: #f7f7f7;
	}

	#G_Left_Slot>div {
		display: flex;
		align-items: center;
	}

	#G_Slot_Text {
		margin-left: 10px;
	}

	#G_Slot_xhd {
		margin-top: 10px;
		margin-left: 15px;
		width: 6px;
		height: 6px;
		background-color: red;
		border-radius: 50%;
	}

	img {
		width: 80%;
	}
</style>
