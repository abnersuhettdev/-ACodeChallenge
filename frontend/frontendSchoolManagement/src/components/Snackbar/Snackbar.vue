<template>
  <v-snackbar v-model="snackbar" :timeout="3000">
    <div style="text-align: center; width: 100%;"> 
      {{ text }}
    </div>
    <template v-slot:actions>
      <v-btn color="pink" variant="text" @click="snackbar = false">
        <v-icon>mdi-close</v-icon>
      </v-btn>
    </template>
  </v-snackbar>
</template>

<script>
export default {
  props: {
    text: {
      type: String,
      default: ""
    },
    value: { 
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      snackbar: this.value, 
    };
  },
 watch: {
    value(newValue) {
        this.snackbar = newValue; 
    }
  }
}
</script>
