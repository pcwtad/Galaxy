<template>
	<div id="HRMBottom">
		<div v-for="(item,index) in show" :key="index" class="qbdiv">
			<div class="qbdiv-a" @click="getid(item)">
				<img :src="item.showcover" />
			</div>
			<div class="qbdiv-b">{{item.showbrin}}</div>
			<div class="qbdiv-c">
				<div class="qbdivc-a">
					<img src="../../../../../ccets/Home-Right/HR-Relese/位置.svg" />
					<span>{{item.showwhere}}</span>
				</div>
				<div class="qbdivc-b">
					<img src="../../../../../ccets/Home-Right/HR-Find/爱心.svg" />
					<span>{{item.showindex}}</span>
				</div>
			</div>
		</div>
	</div>
</template>

<script>
	import {
		DisplayMys
	} from '../../../../../network/Find.js'
	import {
		useCounterStore
	} from '../../../../../Pinia/Show.js'
	export default {
		name: 'HRMBottom',

		data() {
			return {
				index: 1,
				show: null,
				isscroll: true,
			}
		},

		methods: {

			Popup() {
				if (!this.isscroll) return
				console.log(123)
				DisplayMys(this.index, this.$route.params.id).then(res => {
					for (var i = 0; i < res.length; i++) {
						res[i].showcover = res[i].showcover.replace('file:///E:',
							'http://192.168.80.1')
					}
					this.show.push(...res)
					this.index++
					if (res.length < 10) this.isscroll = false
				}).catch(err => {})
			},

			getid(item) {
				const counterStore = useCounterStore()
				counterStore.setCount(item)
				this.$router.push('/Home/'+this.$route.params.id+'/' + item.id)
			},

		},

		created() {
			DisplayMys(this.index, this.$route.params.id).then(res => {
				for (var i = 0; i < res.length; i++) {
					res[i].showcover = res[i].showcover.replace('file:///E:',
						'http://192.168.80.1')
				}
				this.show = res
				this.index++
				if (res.length < 10) this.isscroll = false
			}).catch(err => {})
		}

	}
</script>

<style>
	#HRMBottom {
		width: 100%;
		height: 100%;
		display: flex;
		flex-wrap: wrap;
	}

	.qbdiv {
		width: 220px;
		display: flex;
		flex-direction: column;
		padding: 10px;
	}

	.qbdiv-a {
		width: 100%;
		border-radius: 22px;
		height: 300px;
	}

	.qbdiv-a img {
		border-radius: 22px;
		height: 300px;
		width: 100%;
	}

	.qbdiv-a>img:hover {
		-webkit-filter: blur(1px);
		filter: blur(1px);
		cursor: pointer;
	}

	.qbdiv-b {
		width: 200px;
		margin-top: 5px;
		display: flex;
		margin-left: 10px;
		font-weight: 100;
		font-size: 14px;
	}

	.qbdiv-c {
		display: flex;
		justify-content: space-between;
		margin-top: 5px;
	}

	.qbdivc-a {
		display: flex;
		margin-left: 10px;
		align-items: center;
		cursor: pointer;
	}

	.qbdivc-a img {
		width: 18px;
	}

	.qbdivc-a span {
		font-size: 12px;
		font-weight: 300;
		margin-left: 5px;
	}

	.qbdivc-b {
		display: flex;
		align-items: center;
		margin-right: 10px;
	}

	.qbdivc-b img {
		width: 20px;
		margin-right: 5px;
	}

	.qbdivc-b span {
		font-size: 12px;
		margin-right: 5px;
		font-weight: 400;
	}
</style>
