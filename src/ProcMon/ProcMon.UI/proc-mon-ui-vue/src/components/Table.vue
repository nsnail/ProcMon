  <template>
	<el-table :data="monitorsData" stripe border>
		<el-table-column label="序号" sortable>
			<template slot-scope="scope">{{scope.$index+1}}</template>
		</el-table-column>
		<el-table-column prop="exePath" label="可执行程序路径" sortable></el-table-column>
		<el-table-column prop="workFolder" label="工作目录" sortable></el-table-column>
		<el-table-column prop="startArgs" label="启动参数" sortable></el-table-column>
		<el-table-column prop="logPath" label="日志文件路径" sortable></el-table-column>
		<el-table-column prop="logTimeout" label="日志超时时间" sortable></el-table-column>
		<el-table-column label="状态" sortable>
			<template slot-scope="scope">
				<el-tag :type="scope.row.statusType">{{scope.row.statusStr}}</el-tag>
			</template>
		</el-table-column>
		<el-table-column label="检查项">
			<template slot-scope="scope">
				<el-col>
					<el-switch v-model="scope.row.checkExe" active-text="进程监视" disabled></el-switch>
				</el-col>
				<el-col>
					<el-switch v-model="scope.row.checkLog" active-text="日志监视" disabled></el-switch>
				</el-col>
			</template>
		</el-table-column>
		<el-table-column label="操作">
			<template slot-scope="scope">
				<el-col :span="5">
					<el-button type="primary" icon="el-icon-edit" circle @click="editClick(scope.row)"></el-button>
				</el-col>
				<el-col :span="5">
					<el-popconfirm title="确定要删除这个配置吗？" @confirm="delClick(scope.row)">
						<el-button type="danger" icon="el-icon-delete" slot="reference" circle></el-button>
					</el-popconfirm>
				</el-col>
			</template>
		</el-table-column>
	</el-table>
</template>

  <script>
export default {
	inject: ["reload"],
	data() {
		return {
			monitorsData: [],
		};
	},
	mounted() {
		this.axios
			.get(`${this.common.cfg.apiGateway}/Monitors/GetAll`)
			.then((res) => {
				for (var i in res.data.items) {
					res.data.items[i].statusStr = "Loading...";
					res.data.items[i].statusType = "info";
				}
				this.monitorsData = res.data.items;
			});

		setInterval(() => {
			this.axios
				.get(`${this.common.cfg.apiGateway}/Statuses/GetAll`)
				.then((res) => {
					for (var i in res.data.items) {
						var monitor = this.monitorsData.filter(
							(x) => x.guid == res.data.items[i].guid
						)[0];
						if (!monitor) break;

						switch (res.data.items[i].status) {
							case 1:
								monitor.statusType = "success";
								monitor.statusStr = "正常";
								break;
							case 2:
								monitor.statusType = "danger";
								monitor.statusStr = "进程退出";
								break;
							case 3:
								monitor.statusType = "danger";
								monitor.statusStr = "日志超时";
								break;
						}
					}
				});
		}, 3000);
	},
	methods: {
		editClick(item) {
			this.$router.push({
				name: "Set",
				params: item,
			});
		},
		delClick(item) {
			this.axios
				.delete(`${this.common.cfg.apiGateway}/Monitors/Delete`, {
					params: {
						guid: item.guid,
					},
				})
				.then((res) => {
					this.$message({
						message: "删除成功",
						type: "success",
					});
					this.reload();
				});
		},
	},
};
</script> 