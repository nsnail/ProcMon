<template>
	<Edit :formData="form" @submit="submit" />
</template>
<script>
import { v4 as uuidv4 } from "uuid";

import Edit from "@/components/Edit.vue";

export default {
	name: "New",
	components: {
		Edit,
	},
	data() {
		return {
			form: {
				guid: uuidv4(),
				exePath: "",
				workFolder: "",
				startArgs: "",
				logPath: "",
				checkLog: true,
				checkExe: true,
				logTimeout: 300,
			},
		};
	},
	methods: {
		submit(formData) {
			this.axios
				.post(`${this.common.cfg.apiGateway}/Monitors/Add`, formData)
				.then((res) => {
					this.$message({
						message: "记录添加成功！",
						type: "success",
					});
					this.$router.push({ name: "Home" });
				});
		},
	},
};
</script>