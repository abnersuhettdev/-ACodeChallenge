<template>
  <v-snackbar 
    v-model="snackbar" 
    :timeout="3000" 
    :color="snackbarColor" 
    dark
  >
    <div style="text-align: center; width: 100%;"> 
      {{ text }}
    </div>
    <template v-slot:actions>
      <v-btn color="white" variant="text" @click="snackbar = false">
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
    },
    color: { 
      type: String, 
      default: "#4392F1"  
    }
  },
  data() {
    return {
      snackbar: this.value, 
    };
  },
  computed: {
    snackbarColor() {
      return this.color || "#4392F1"; 
    }
  },
  watch: {
    value(newValue) {
      this.snackbar = newValue; 
    }
  }
}
</script>

