<template>
	<div id="HR_Find_Item">
		<el-scrollbar>
			<div class="scrollbar-flex-content">
				<p :style="item==this.NewHRF_Address ? IsClick : ''" v-for="item in this.HRF_Address" :key="item"
					class="scrollbar-demo-item" @click="HRF_ItemC(item)">
					{{ item }}
				</p>
			</div>
		</el-scrollbar>
	</div>
</template>

<script>
	import {
		ref
	} from "vue";
	export default {
		name: 'HR_Find_Item',

		data() {
			return {
				HRF_Address: [
					'默认', '河北', '山西', '辽宁', '吉林', '江苏', '浙江', '安徽', '福建', '江西', '山东', '河南', '湖北', '湖南', '广东', '海南',
					'四川',
					'贵州', '云南', '陕西', '甘肃', '青海', '台湾', '黑龙江', '北京', '天津', '上海', '重庆', '内蒙古', '广西', '西藏', '宁夏', '新疆',
					'香港', '澳门'
				],
				NewHRF_Address: '默认',
			}
		},

		setup() {
			const address = ref("")
			return {
				address
			}
		},

		methods: {
			HRF_ItemC(NHRF_Address) {
				this.NewHRF_Address = NHRF_Address
				this.address = NHRF_Address
				// const useaddress = useaddres()
				// useaddress.address = NHRF_Address
				// this.$emit("getList")
			}
		},

		computed: {
			IsClick() {
				return {
					background: 'rgb(214, 32, 75)',
					color: '#fff',
				}
			}
		},

	}
</script>

<style scoped>
	.scrollbar-flex-content {
		width: 100px;
		display: flex;
	}

	#HR_Find_Item {
		width: 100%;
	}

	.el-scrollbar {
		width: 100%;
	}

	.scrollbar-demo-item:hover {
		background: rgb(214, 32, 75);
		color: #fff;
	}

	.scrollbar-demo-item {
		font-size: 12px;
		flex-shrink: 0;
		display: flex;
		align-items: center;
		justify-content: center;
		width: 45px;
		height: 30px;
		margin: 10px 5px 10px 5px;
		text-align: center;
		border-radius: 99px;
		color: #878a8f;
		cursor: pointer;
	}
</style>
