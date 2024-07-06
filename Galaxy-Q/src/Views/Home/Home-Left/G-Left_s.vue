<template>
	<div id="G_Left_s">
		<div style="width: 100%;">
			<GLeftSlot path="/Home/HR_Find" activeColor="#f7f7f7">
				<template #GL_Slot_Img>
					<img src="../../../Img/Home-Lefts/发现.svg" />
				</template>
				<template #GL_Slot_Text>
					<span>发现</span>
				</template>
			</GLeftSlot>
			<GLeftSlot path="/Home/HR_Notice" activeColor="#f7f7f7">
				<template #GL_Slot_Img>
					<img src="../../../Img/Home-Lefts/通知.svg" />
				</template>
				<template #GL_Slot_Text>
					<span>通知</span>
				</template>
			</GLeftSlot>
			<GLeftSlot path="/Home/HR_Release" activeColor="#f7f7f7">
				<template #GL_Slot_Img>
					<img src="../../../Img/Home-Lefts/发布.svg" />
				</template>
				<template #GL_Slot_Text>
					<span>发布</span>
				</template>
			</GLeftSlot>
			<GLeftSlot :path="isuserid" activeColor="#f7f7f7" v-if="a">
				<template #GL_Slot_Img>
					<img src="../../../Img/Home-Lefts/我的.svg" />
				</template>
				<template #GL_Slot_Text>
					<span>我的</span>
				</template>
			</GLeftSlot>
			<div class="dldiv" @click="godl" v-if="!a">
				登录
			</div>
			<div class="tcdl" @click="removedl" v-if="a">退出登录</div>
		</div>
	</div>
</template>

<script>
	import GLeftSlot from './H_Left_Slot/G-Left_Slot.vue'
	import Cookies from 'js-cookie'
	import {
		useCounterStore
	} from '../../../Pinia/Show.js'
	export default {
		name: 'G_Left_s',

		components: {
			GLeftSlot
		},


		data() {
			return {
				a: false,
				isuserid: '/Home/' + Cookies.get('GalaxyID'),
				counterStores: null
			}
		},

		methods: {
			godl() {
				this.$router.push('/Login')
			},

			removedl() {
				Cookies.remove('GalaxyJWT')
				Cookies.remove('GalaxyID')
				this.a = false
				this.$router.push('/Home/HR_Find')
			}
		},

		created() {
			if (Cookies.get('GalaxyID') != undefined) {
				this.a = true
			}
			const ChatcounterStores = useCounterStore()
			this.counterStores = ChatcounterStores
		}

	}
</script>

<style>
	#G_Left_s {
		flex: 1;
		width: 100%;
	}

	.dldiv {
		width: 70%;
		height: 44px;
		border-radius: 90px;
		display: flex;
		justify-content: center;
		align-items: center;
		background-color: #d6204b;
		margin-top: 15px;
		margin-left: 30px;
		font-size: 20px;
		font-weight: 500;
		color: white;
		cursor: pointer;
	}

	.tcdl {
		width: 70%;
		height: 44px;
		border-radius: 90px;
		display: flex;
		justify-content: center;
		align-items: center;
		background-color: #d6204b;
		margin-top: 15px;
		margin-left: 30px;
		font-size: 20px;
		font-weight: 500;
		color: white;
		cursor: pointer;
	}
</style>
