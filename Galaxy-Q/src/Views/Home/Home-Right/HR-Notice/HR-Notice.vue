<template>
	<div id="HR-Notice">
		<div class="HRN_center">
			<div class="HRN_center_item">
				<div :style="item==this.NewHRNtext ? 'border-bottom: 3px solid #d6204b;':''" v-for="item in HRNtext" :key="item"
					@click="Gotext(item)">
					{{item}}
				</div>
			</div>
			<div class="HRN_center_bootom">
				<router-view></router-view>
			</div>
		</div>
	</div>
</template>

<script>
	import {
		useCounterStore
	} from '../../../../Pinia/Show.js'
	import {
		ObtainUsersa
	} from '../../../../network/Deta.js'
	import Cookies from 'js-cookie'
	export default {

		name: 'HR-Notice',

		data() {
			return {
				HRNtext: ['通知', '消息'],
				NewHRNtext: "通知",
				connection: null
			}
		},

		methods: {

			Gotext(item) {
				this.NewHRNtext = item;
				if (item == '通知') {
					this.$router.push('/Home/HR_Notice/Hrnnotice')
				} else if (item == '消息') {
					this.$router.push('/Home/HR_Notice/Hrnchat')
				}
			},

		},

		created() {
			const counterStore = useCounterStore()
			this.connection = counterStore.connection

			ObtainUsersa(Cookies.get('GalaxyID')).then(res => {
				counterStore.setmyuser(res[0])
			}).catch(err => {})
		}

	}
</script>

<style>
	#HR-Notice {
		margin-top: 10px;
		display: flex;
		align-items: center;
		justify-content: center;
		width: 100%;
		height: 87vh;
	}

	.HRN_center {
		box-shadow: 2px 2px 1px 0px rgba(175, 175, 175, 0.7);
		border-radius: 25px;
		width: 90%;
		height: 100%;
		border: 1px solid #d4d4d4;
	}

	.HRN_center_item {
		height: 50px;
		width: 100%;
		display: flex;
		align-items: center;
		border-bottom: 1px solid #d4d4d4;
	}

	.HRN_center_item>div {
		margin-left: 35px;
		width: 50px;
		text-align: center;
		height: 27px;
		border-bottom: 3px solid #cfcfcf;
	}

	.HRN_center_item>div:hover {
		border-bottom: 3px solid #d6204b;
		cursor: pointer;
	}

	.HRN_center_bootom {
		width: 100%;
		height: 90%;
		border-radius: 0px 0px 25px 25px;
	}
</style>