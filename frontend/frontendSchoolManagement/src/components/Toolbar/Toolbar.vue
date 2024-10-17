<template>
  <v-toolbar flat>
    <v-toolbar-title>{{ title }}</v-toolbar-title>
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