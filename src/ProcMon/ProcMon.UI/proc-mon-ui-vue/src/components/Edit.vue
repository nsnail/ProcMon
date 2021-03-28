  <template>
	<el-form ref="form" :model="formData" label-width="150px" :rules="rule">
		<el-form-item label="配置编号" prop="guid">
			<el-input v-model="formData.guid" disabled></el-input>
		</el-form-item>
		<el-form-item label="可执行程序路径" prop="exePath">
			<el-input v-model="formData.exePath"></el-input>
		</el-form-item>
		<el-form-item label="工作目录" prop="workFolder">
			<el-input v-model="formData.workFolder"></el-input>
		</el-form-item>
		<el-form-item label="启动参数" prop="startArgs">
			<el-input v-model="formData.startArgs"></el-input>
		</el-form-item>
		<el-form-item label="日志文件路径" prop="logPath">
			<el-input v-model="formData.logPath"></el-input>
		</el-form-item>
		<el-form-item label="日志超时时间" prop="logTimeout">
			<el-input v-model.number="formData.logTimeout" type="number">
				<template slot="append">秒</template>
			</el-input>
		</el-form-item>
		<el-form-item label="检查项目">
			<el-col :span="2">
				<el-switch v-model="formData.checkExe" active-text="进程监视"></el-switch>
			</el-col>
			<el-col :span="2">
				<el-switch v-model="formData.checkLog" active-text="日志监视"></el-switch>
			</el-col>
		</el-form-item>
		<el-form-item>
			<el-button type="primary" @click="submitForm('form')">保存</el-button>
			<el-button @click="resetForm('form')">重置</el-button>
		</el-form-item>
	</el-form>
</template>

  <script>
export default {
	props: ["formData"],
	mounted() {},
	data() {
		return {
			rule: {
				exePath: [
					{
						required: true,
						message: "请输入可执行程序路径",
						trigger: "blur",
					},
				],
				workFolder: [
					{
						required: true,
						message: "请输入工作目录",
						trigger: "blur",
					},
				],
				logTimeout: [
					{
						type: "number",
						message: "请输入数字",
						trigger: "blur",
					},
				],
			},
		};
	},
	methods: {
		resetForm(formName) {
			this.$refs[formName].resetFields();
		},
		submitForm(formName) {
			this.$refs[formName].validate((valid) => {
				if (valid) {
					this.$emit("submit", this.formData);
				} else {
					return false;
				}
			});
		},
	},
};
</script>