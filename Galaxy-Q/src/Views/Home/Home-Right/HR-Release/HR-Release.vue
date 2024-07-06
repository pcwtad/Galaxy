<template>
	<div id="HR-Release">
		<el-scrollbar height="100%" :min-size="NaN">
			<div class="HRRupload">
				<span>图片编辑</span>
				<label for="file-upload" class="custom-file-upload">
					+ 上传图片
				</label>
				<input id="file-upload" type="file" multiple="multiple" ref="Newfile" v-on:input="jsMoney" />
			</div>
			<div class="HRRupimg">
				<div v-for="(item,index) in upimg">
					<img :src="item" class="imgopac" />
					<img src="../../../../ccets/Home-Right/HR-Relese/删 除 .svg" class="divopac" @click="reimg(index)" />
				</div>
			</div>
			<div id="HRRinformation">
				<el-input v-model="text.Showbrin" maxlength="20" placeholder="填写标题" show-word-limit type="text"
					class="HRRinforcss" />
				<el-input v-model="text.Showinformation" maxlength="200" placeholder="填写更全面的描述信息" show-word-limit
					type="textarea" class="textareacss" />
			</div>
			<div class="HRRinpimg">
				<div class="HRRinp">
					<el-autocomplete v-model="Mapinput" :fetch-suggestions="querySearchAsync" placeholder="请选择地址"
						@select="handleSelect" style="width: 100%;"></el-autocomplete>
				</div>
				<div class="HRRimg">
					<img src="../../../../ccets/Home-Right/HR-Relese/位置.svg" @click="Maping" />
				</div>
			</div>
			<div id="map-container" style="width:70%;height:300px; margin-top: 10px;"></div>
			<div @click="onReles" class="HRRdiv">发布</div>
		</el-scrollbar>
	</div>
</template>

<script>
	import Cookies from 'js-cookie'
	import loadBMap from '../../../../MyJs/loadBMap.js'
	import {
		reactive,
		ref
	} from 'vue';
	import {
		Showincreases
	} from '../../../../network/Show.js'
	export default {
		name: 'HR-Release',

		data() {
			return {
				upimg: [],
				text: {
					userId: Cookies.get("GalaxyID"),
					Showbrin: "",
					Showwhere: "",
					Showaddress: "",
					Showinformation: "",

					poittext: "",
				},

				PostImg: [],


				form: {
					address: '', //详细地址
					addrPoint: { //详细地址经纬度
						lng: 116.40370858745837,
						lat: 39.919867922653935
					}
				},
				map: '', //地图实例
				mk: '', //Marker实例
			}
		},

		setup() {
			const Mapinput = ref("") //详细地址
			return {
				Mapinput
			}
		},
		methods: {

			jsMoney() {
				if (this.upimg.length + this.$refs.Newfile.files.length > 9) {
					alert("最多九张")
				} else {
					for (var i = 0; i < this.$refs.Newfile.files.length; i++) {
						this.PostImg.push(this.$refs.Newfile.files[i])
						let newFiles = new FileReader();
						newFiles.onload = (e) => {
							this.upimg.push(e.target.result)
						}
						newFiles.readAsDataURL(this.$refs.Newfile.files[i])
					}
				}
			},

			reimg(index) {
				this.PostImg.splice(index, 1)
				this.upimg.splice(index, 1)
			},

			initMap() {
				let vm = this;
				this.map = new BMap.Map("map-container", {
					enableMapClick: false
				}) //新建地图实例，enableMapClick:false ：禁用地图默认点击弹框
				var point = new BMap.Point(this.form.addrPoint.lng, this.form.addrPoint.lat);
				this.map.centerAndZoom(point, 14)
				this.map.enableScrollWheelZoom(true); //地图设置滚轮缩放
				this.map.setMapType(BMAP_NORMAL_MAP); //设置地图类型
				var scaleCtrl = new BMap.ScaleControl(); // 添加比例尺控件
				this.map.addControl(scaleCtrl);
				var cityCtrl = new BMap.CityListControl(); // 添加城市列表控件
				this.map.addControl(cityCtrl);
				this.map.setMapStyleV2({
					styleId: 'f65bcdd9048f8dc87fcc181abf3a84c6'
				}); //设置个性化地图

				var geoc = new BMap.Geocoder(); // 创建地理编码实例
				this.map.addEventListener('click', function(e) {
					// console.log(123)
					// console.log(e.Af.lng + ', ' + e.Af.lat) //获取经纬度
					geoc.getLocation(e.Af, function(rs) {
						vm.map.panTo(rs.point) //将地图的中心点更改为给定的点
						var addComp = rs.content;
						vm.Mapinput = addComp.address + addComp.poi_desc;
						vm.poittext = addComp.address_detail.city;
					}) //逆地址解析
				});
			},

			Maping() {
				let vm = this;
				var geolocation = new BMap.Geolocation();
				geolocation.getCurrentPosition(function(r) {
					if (this.getStatus() == 0) {
						console.log(r)
						var Newaddress = r.address;
						vm.Mapinput = Newaddress.province + Newaddress.city + Newaddress.district +
							Newaddress.street + Newaddress.street_number;
						vm.poittext = Newaddress.province;
						// vm.mk.setPosition(r.point) //重新设置标注的地理坐标
						vm.map.panTo(r.point) //将地图的中心点更改为给定的点
					} else {
						function myFun(result) {
							vm.Mapinput = result.name;
							vm.map.panTo(result.center);
						}
						var myCity = new BMap.LocalCity();
						myCity.get(myFun);
					}
				});
			},

			querySearchAsync(str, cb) {
				var options = {
					onSearchComplete: function(res) { //检索完成后的回调函数
						var s = [];
						if (local.getStatus() == BMAP_STATUS_SUCCESS) {
							for (var i = 0; i < res.getCurrentNumPois(); i++) {
								var tite = res.getPoi(i)
								var x = {
									"value": tite.city + tite.title + tite.address,
									"poit": tite.point,
									"poittext": tite.province
								}
								s.push(x);
							}
							cb(s)
						} else {}
					}
				}
				var local = new BMap.LocalSearch(this.map, options) //创建LocalSearch构造函数
				local.search(str) //调用search方法，根据检索词str发起检索
			},

			handleSelect(item) {
				this.Mapinput = item.value;
				console.log(item)
				this.text.poittext = item.poittext;
				this.map.panTo(item.poit) //将地图的中心点更改为选定坐标点
			},

			onReles() {
				if (this.PostImg.length == 0) {
					alert('至少需要一张图片')
					return
				}
				if (this.text.Showbrin == "") {
					alert('标题不能为空')
					return
				}
				if (this.text.Showinformation == "") {
					alert('描述信息不能为空')
					return
				}
				window.event.preventDefault()
				let newFiles = new FormData();
				for (var i = 0; i < this.PostImg.length; i++) {
					newFiles.append('Files', this.PostImg[i])
				}
				console.log(this.text.poittext)
				if (this.text.poittext == undefined) this.text.poittext = "未知"
				if (this.text.poittext.length == 0) this.text.poittext = "未知"
				this.text.Showwhere = this.text.poittext
				this.text.Showaddress = this.Mapinput
				Showincreases(newFiles, this.text.userId, this.text.Showbrin, this.text.Showwhere, this.text.Showaddress,
					this.text.Showinformation).then(res => {
					if (this.upimg.length == res) {
						this.text.Showbrin = ""
						this.text.Showinformation = ""
						this.PostImg.splice(0, this.PostImg.length)
						this.upimg.splice(0, this.upimg.length)
						alert("上传成功")
					} else {
						alert("上传失败")
					}
				}).catch(err => {})

			}


		},

		async mounted() {
			await loadBMap('XawdSlUxam1xYMSZiqtZzeTOd8ypxXhG') //加载引入BMap
			this.initMap()
		}


	}
</script>

<style>
	.wait {
		position: absolute;
		width: 100%;
		height: 100%;
		top: 0;
		left: 0;
		right: 0;
		bottom: 0;
		display: flex;
		align-items: center;
		justify-content: center;
	}

	.HRRdiv {
		display: flex;
		justify-content: center;
		align-items: center;
		margin: auto;
		height: 40px;
		width: 120px;
		color: white;
		background-color: #d6204b;
		font-size: 20px;
		border-radius: 99px;
		margin-top: 20px;
		cursor: pointer;
	}

	.HRRinpimg {
		width: 100%;
		display: flex;
		margin-top: 10px;
		align-items: center;
	}

	.HRRinp .el-input {
		--el-input-focus-border-color: #d6204b;
	}

	.HRRinp {
		width: 50%;
	}

	.HRRimg {
		height: 100%;
		margin-left: 30px;
		cursor: pointer;
	}

	#HRRinformation {
		margin-top: 15px;
		width: 65%;
	}

	.textareacss {
		margin-top: 15px;
	}

	#HRRinformation .el-input {
		--el-input-focus-border-color: #d6204b;
	}

	#HRRinformation .el-textarea {
		--el-input-focus-border-color: #d6204b;
	}

	#HR-Release {
		width: 100%;
		height: 41.7em;
	}

	.HRRupimg {
		position: relative;
		margin-top: 15px;
		width: 100%;
		height: 80px;
		display: flex;
	}

	.divopac {
		padding: 1px;
		width: 30px;
		text-align: center;
		font-size: 20px;
		border-radius: 7px;
		position: absolute;
		cursor: pointer;
		display: none;
	}

	.HRRupimg>div:hover .divopac {
		display: block;
		border: 1px solid #d6204b;
		z-index: 99;
	}

	.HRRupimg>div:hover .imgopac {
		filter: blur(3px);
	}

	.HRRupimg>div {
		width: 10%;
		border: 1px solid #a1a1a1;
		height: 100%;
		border-radius: 10px;
		display: flex;
		align-items: center;
		justify-content: center;
		background-color: #eee;
		margin-left: 5px;
	}

	.imgopac {
		width: 100%;
		height: 100%;
		border-radius: 10px;
	}

	#path {
		width: 100px;
		height: 20px;
		border: 1px solid red;
	}

	.HRRupload>span {
		font-size: 20px;
		font-weight: 700;
		color: #676767;
	}

	.HRRupload>label {
		margin-left: 20px;
		width: 100px;
		background-color: #bcbcbc;
		color: #fff;
		text-align: center;
		padding-top: 5px;
		padding-bottom: 5px;
		border-radius: 99px;
		cursor: pointer;
	}

	.HRRupload>label:hover {
		background-color: #d6204b;
	}

	.HRRupload {
		margin-top: 10px;
		display: flex;
		align-items: center;
	}

	input[type="file"] {
		display: none;
	}
</style>




