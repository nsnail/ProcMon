<template>
	<el-container>
		<el-popconfirm title="确定要清空配置文件吗？" @confirm="init">
			<el-button type="danger" slot="reference">清空配置文件</el-button>
		</el-popconfirm>
	</el-container>
</template>
<script>
export default {
	methods: {
		init() {
			this.initMonitors();
			this.initStatuses();
		},
		initMonitors() {
			this.axios
				.put(`${this.common.cfg.apiGateway}/Monitors/Write`, {})
				.then((res) => {
					this.$message({
						message: "Monitors初始化完成！",
						type: "success",
					});
					this.$router.push({ name: "Home" });
				});
		},
		initStatuses() {
			this.axios
				.put(`${this.common.cfg.apiGateway}/Statuses/Write`, {})
				.then((res) => {
					this.$message({
						message: "Statuses初始化完成！",
						type: "success",
					});
					this.$router.push({ name: "Home" });
				});
		},
	},
};
</script>