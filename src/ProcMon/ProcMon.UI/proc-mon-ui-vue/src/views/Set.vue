<template>
	<Edit :formData="formData" @submit="submit" />
</template>
<script>
import Edit from "@/components/Edit.vue";

export default {
	data() {
		return {
			formData: this.$route.params,
		};
	},
	name: "Set",
	components: {
		Edit,
	},
	mounted() {},
	methods: {
		submit(formData) {
			this.axios
				.put(`${this.common.cfg.apiGateway}/Monitors/Set`, formData)
				.then((res) => {
					this.$message({
						message: "修改成功！",
						type: "success",
					});
					this.$router.push({ name: "Home" });
				});
		},
	},
};
</script>