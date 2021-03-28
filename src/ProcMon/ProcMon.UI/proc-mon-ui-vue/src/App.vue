<template>
	<div id="app">
		<el-container>
			<el-header>
				<h1>进程监视管理器</h1>
			</el-header>
			<el-main>
				<el-container>
					<el-aside>
						<el-menu :default-active="$route.path" router>
							<el-menu-item index="/init">
								<i class="el-icon-delete"></i>
								<span slot="title">初始化</span>
							</el-menu-item>
							<el-menu-item index="/">
								<i class="el-icon-view"></i>
								<span slot="title">查看</span>
							</el-menu-item>
							<el-menu-item index="/new">
								<i class="el-icon-circle-plus-outline"></i>
								<span slot="title">新增</span>
							</el-menu-item>
						</el-menu>
					</el-aside>
					<el-main>
						<router-view v-if="isRouterAlive" />
					</el-main>
				</el-container>
			</el-main>

			<el-footer>&copy; nsnail</el-footer>
		</el-container>
	</div>
</template>  
<script>
export default {
	data() {
		return {
			isRouterAlive: true,
		};
	},
	provide() {
		return {
			reload: this.reload,
		};
	},
	methods: {
		reload() {
			this.isRouterAlive = false;
			this.$nextTick(() => (this.isRouterAlive = true));
		},
	},
};
</script>
