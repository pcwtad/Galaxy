<template>
	<div id="Commenttext">
		<div v-for="(item,index) in commentdata" :key="item">
			<div class="commtextitem" v-if="isDom">
				<img src="../../../Img/Home-Lefts/线_稍等.svg" />
				<div>加载中</div>
			</div>
			<div class="commtextitem" v-else>
				<img :src="user[item.userId].headImg" @click.stop="goid(user[item.userId].userid)" />
				<div>{{user[item.userId].userName}}</div>
			</div>
			<div class="commtextbottom">
				<span>{{item.title}}</span>
				<div>
					<span v-text="item.commentDate.substring(0, 10)"></span>
					<div @click.stop="huifu(item.id,item.userId,user[item.userId].userName)">回复</div>
				</div>
			</div>
			<div v-if="!isDom">
				<span class="zkhuifu" v-if="chilindex[item.id].isdom" @click.stop="ckhf(item.id)">——展开回复</span>
				<div v-if="chilindex[item.id].ischil" class="coverdiv">
					<div v-for="(i,j) in chilindex[item.id].childeve">
						<div class="zkhufo" v-if="chilindex[item.id].isifdiv">
							<img src="../../../Img/Home-Lefts/线_稍等.svg" />
							<div>加载中</div>
						</div>
						<div class="zkhufo" v-else>
							<img :src="newuser[i.userId].headImg" @click.stop="goid(newuser[i.userId].userid)" />
							<div>{{newuser[i.userId].userName}}</div>
						</div>
						<span class="spanhfu"
							v-if="!chilindex[item.id].isifdiv">回复{{newuser[i.coverId].userName}}:{{i.title}}</span>
						<div class="spandivfu">
							<span v-text="i.commentDate.substring(0, 10)"></span>
							<div @click.stop="newhuifu(i,newuser[i.userId].userName)">回复</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
</template>

<script>
	import {
		GetParentevesa,
		GetChildevesa
	} from '../../../network/Comment.js'
	import {
		ObtainUsersa
	} from '../../../network/Find.js'
	export default {
		name: 'Commenttext',

		data() {
			return {
				index: 1,
				commentdata: null,
				user: {},
				isDom: true,

				chilindex: {},

				newuser: {}
			}
		},

		methods: {
			goid(id) {
				this.$router.replace(/Home/ + id)
			},

			newhuifu(i, name) {
				console.log(name)
				this.$emit('childtoparent', i.parenteveId, i.userId, name);
			},

			huifu(parenteveid, userid, name) {
				this.$emit('childtoparent', parenteveid, userid, name);
			},

			ckhf(id) {
				this.chilindex[id].isifdiv = true
				GetChildevesa(id, this.chilindex[id].index).then(res => {
					this.chilindex[id].childeve = res
					if (res.length < 10) {
						this.chilindex[id].isdom = false
					}
				}).catch(err => {})
				this.chilindex[id].index++
				this.chilindex[id].ischil = true


				const timeoutId = setTimeout(() => {
						const chileds = this.chilindex[id].childeve
						for (var i = 0; i < chileds.length; i++) {
							const id = chileds[i].userId
							ObtainUsersa(id).then(res => {

								const user = {
									"headImg": res[0].headImg,
									"userName": res[0].userName,
									"userid": id
								}
								this.newuser[id] = user
							}).catch(err => {})
						}
					},
					200);


				const timeoutIds = setTimeout(() => {
					this.chilindex[id].isifdiv = false
				}, 400)

			}


		},

		created() {

			GetParentevesa(this.$route.params.showguid, this.index).then(res => {
				this.commentdata = res
			}).catch(err => {})
			this.index++

			const timeoutId = setTimeout(() => {
				for (var i = 0; i < this.commentdata.length; i++) {
					const isindex = {
						"index": 1,
						"isdom": true,
						"childeve": null,
						"ischil": false,
						"isifdiv": true
					}
					this.chilindex[this.commentdata[i].id] = isindex

					const id = this.commentdata[i].userId
					ObtainUsersa(id).then(res => {
						const user = {
							"headImg": res[0].headImg,
							"userName": res[0].userName,
							"userid": id
						}
						this.user[id] = user
					}).catch(err => {})
				}
			}, 200);

			const timeoutIds = setTimeout(() => {
				this.isDom = false
			}, 400);

		}

	}
</script>

<style>
	#Commenttext {
		width: 100%;
		height: 100%;
	}

	#Commenttext>div {
		display: flex;
		flex-direction: column;
	}

	.commtextitem {
		display: flex;
	}

	.commtextitem>img {
		border-radius: 50%;
		border: 1px solid #dedede;
		width: 32px;
		height: 32px;
		margin-top: 5px;
		margin-left: 20px;
	}

	.commtextitem>img:hover {
		cursor: pointer;
	}

	.commtextitem>div {
		margin-top: 5px;
		color: #8a8a8a;
		font-size: 12px;
		font-weight: 300;
		margin-left: 10px;
	}

	.commtextbottom {
		display: flex;
		flex-direction: column;
	}

	.commtextbottom>span {
		margin-left: 63px;
		max-width: 300px;
		font-weight: 400;
		margin-top: -14px;
		word-wrap: break-word;
	}

	.commtextbottom>div {
		margin-top: 3px;
		margin-left: 62px;
		font-size: 10px;
		font-weight: 300;
		display: flex;
	}

	.commtextbottom>div>div {
		margin-left: 20px;
	}

	.commtextbottom>div>div:hover {
		cursor: pointer;
	}

	.zkhuifu {
		margin-left: 70px;
		font-size: 10px;
		font-weight: 300;
		color: #999999;
	}

	.zkhuifu:hover {
		cursor: pointer;
	}

	.coverdiv {
		display: flex;
		flex-direction: column;
	}

	.zkhufo {
		display: flex;
	}

	.zkhufo>img {
		border-radius: 50%;
		border: 1px solid #dedede;
		width: 25px;
		height: 25px;
		margin-top: 5px;
		margin-left: 60px;
	}

	.zkhufo>img:hover {
		cursor: pointer;
	}

	.zkhufo>div {
		margin-top: 5px;
		color: #8a8a8a;
		font-size: 12px;
		font-weight: 300;
		margin-left: 10px;
	}

	.spanhfu {
		display: flex;
		max-width: 250px;
		word-wrap: break-word;
		font-weight: 300;
		font-size: 13px;
		margin-left: 95px;
		margin-top: -10px;
	}

	.spandivfu {
		display: flex;
	}

	.spandivfu>span {
		margin-top: 5px;
		font-size: 10px;
		font-weight: 300;
		margin-left: 95px;
	}

	.spandivfu>div {
		margin-top: 5px;
		margin-left: 10px;
		font-size: 10px;
	}

	.spandivfu>div:hover {
		cursor: pointer;
	}
</style>