<template>
  <div class="page-container">
    <md-app md-waterfall md-mode="fixed">
      <md-app-toolbar class="md-primary">
        <span class="md-title">My Title</span>
      </md-app-toolbar>

      <md-app-drawer md-permanent="full">
        <md-toolbar class="md-transparent" md-elevation="0">
          Navigation
        </md-toolbar>

        <md-list>
          <md-list-item>
            <md-icon>all_inbox</md-icon>
            <span class="md-list-item-text">All Tasks</span>
          </md-list-item>

          <md-list-item>
            <md-icon>delete</md-icon>
            <span class="md-list-item-text">Trash</span>
          </md-list-item>

          <md-list-item>
            <md-icon>error</md-icon>
            <span class="md-list-item-text">Spam</span>
          </md-list-item>

          <md-list-item v-for="list in lists" v-bind:key="list.Id">
            <md-icon>label</md-icon>
            <span class="md-list-item-text">{{list.Name}}</span>
          </md-list-item>
        </md-list>
      </md-app-drawer>

      <md-app-content>
        <p>Hello, world</p>
      </md-app-content>
    </md-app>
  </div>
</template>

<script>

import axios from 'axios';

export default {
  name: "App",
  components: {
  },
  data: () => ({
    lists: []
  }),
  async created() {
    const response = await axios.get("/api/todo/lists");

    this.lists = response.data;
  },
  methods: {
  },
};
</script>

<style lang="scss" scoped>
  .md-drawer
  {
    width: 230px;
    max-width: calc(100vw - 125px);
  }

.md-app
{
  min-height: 100vh;
}
</style>