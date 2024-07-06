<template>
	<div id="HR_Find_Bottom">
		<el-scrollbar height="100%" @scroll="Newscroll($event)" :min-size="NaN" ref="contentData">
			<div class="HRFBdiv">
				<div v-for="(item,index) in Show" :key="index" class="qbdiv">
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
			<div v-if="!isscroll" class="myl">————————没有了————————</div>
		</el-scrollbar>
		<div class="xshang" v-if="isxishang" @click="goshang">
			<img src="../../../../../ccets/Home-Right/HR-Find/向上.svg" />
		</div>
		<router-view></router-view>
	</div>
</template>

<script>
	import {
		DisplayFinds,
		ObtainUsersa
	} from '../../../../../network/Find.js'
	import throttle from '../../../../../MyJs/throttle.js'
	import {
		useCounterStore
	} from '../../../../../Pinia/Show.js'
	export default {
		name: 'HR_Find_Bottom',

		data() {
			return {
				index: 1,
				Show: null,
				isscroll: true,
				isxishang: false
			}
		},

		methods: {

			Newscroll: throttle(function(e) {
				if (e.scrollTop > 800) {
					this.isxishang = true
				} else if (e.scrollTop < 800) {
					this.isxishang = false
				}
				if (!this.isscroll) return;
				let element = this.$el.querySelector('.HRFBdiv').offsetHeight - 570
				if (e.scrollTop + 70 >= element) {
					DisplayFinds(this.index).then(res => {
						for (var i = 0; i < res.length; i++) {
							res[i].showcover = res[i].showcover.replace('file:///E:',
								'http://192.168.80.1')
						}
						this.Show.push(...res)
						this.index++;
						if (res.length < 10) this.isscroll = false
					}).catch(err => {})
				}
			}, 500),

			getid(item) {
				const counterStore = useCounterStore()
				counterStore.setCount(item)
				this.$router.push('/Home/HR_Find/' + item.id)
			},

			goshang() {
				this.$refs.contentData.scrollTo(0, 0)
			}

		},

		created() {
			DisplayFinds(this.index).then(res => {
				for (var i = 0; i < res.length; i++) {
					res[i].showcover = res[i].showcover.replace('file:///E:', 'http://192.168.80.1')
				}
				this.Show = res
				this.index++;
				if (res.length < 10) this.isscroll = false
			}).catch(err => {})
		},

	}
</script>

<style scoped>
	#HR_Find_Bottom {
		width: 100%;
		height: 38.6em;
	}

	.HRFBdiv {
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

	.xshang {
		position: absolute;
		right: 40px;
		bottom: 50px;
		width: 40px;
		height: 40px;
		background-color: white;
		border-radius: 50%;
		border: 1px solid #eee;
		display: flex;
		align-items: center;
		justify-content: center;
		cursor: pointer;
	}

	.xshang img {
		width: 45%;
	}

	.myl {
		display: flex;
		justify-content: center;
		align-items: center;
		width: 100%;
		height: 40px;
	}
</style>