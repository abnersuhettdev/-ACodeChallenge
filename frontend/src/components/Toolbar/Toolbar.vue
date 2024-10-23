<template>
  <v-toolbar flat>
    <v-toolbar-title class="toolbar">{{ title }}</v-toolbar-title>
    <v-spacer></v-spacer>
    <slot></slot>
  </v-toolbar>
</template>

<script>
export default {
  name: 'Toolbar',
  props: {
    title: {
      type: String,
      required: true
    }
  }
};
</script>

<style scoped>

.toolbar {
  background-color: #4A5248;
  color: #FFFFFF;
  border-radius: 10px 10px 0px 0px;
}

</style>