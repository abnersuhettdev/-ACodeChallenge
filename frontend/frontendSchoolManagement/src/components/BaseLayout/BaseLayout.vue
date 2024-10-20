<template>
  <v-app>
    <v-navigation-drawer app>
      <v-list dense>
        <v-list-item>
          <v-list-item-title class="title">Módulo Acadêmico</v-list-item-title>
        </v-list-item>
        <v-divider></v-divider>
        <v-list-item>
          <router-link to="/" exact class="back-button">
            Alunos
          </router-link>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>
    <v-main class="content-container">
      <v-container>
        <slot></slot>
      </v-container>
    </v-main>
  </v-app>
</template>

<script>
export default {
  name: 'LayoutBase',
};
</script>

<style scoped>
.v-navigation-drawer {
  width: 200px;
}

.title{
  text-align: center;
  font-weight: bold;
}

.v-list-item {
  padding: 0 !important;
}

.back-button {
  display: inline-flex;
  width: 100%;
  padding: 10px 15px;
  color: black;
  text-decoration: none;
  background-color: transparent; 
  transition: background-color 0.3s ease, color 0.3s ease;
}

.back-button:hover {
  background-color: #5b6c5dab; 
  color: #ffff;
}

.router-link-exact-active {
  background-color: #4A5248; 
  color: #ffff;
  font-weight: bold;
}

.content-container{
  background-color: #f5f5f5;
}
</style>