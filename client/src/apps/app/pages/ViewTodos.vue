<template>
    <div>
        <md-table style="margin: 0px;" v-model="items" md-card @md-selected="onSelect">
        <md-table-toolbar class="md-dense">
            <md-button class="md-icon-button md-raised md-dense">
                <md-icon>add</md-icon>
            </md-button>
        </md-table-toolbar>

        <md-table-row slot="md-table-row" slot-scope="{ item }" md-auto-select>
            <md-table-cell md-label="Name"><strong>{{ item.Name }}</strong></md-table-cell>
        </md-table-row>
        </md-table>
    </div>
</template>

<script>
    import axios from 'axios';

    export default {
    name: "App",
    components: {
    },
    data: () => ({
        items: []
    }),
    async created() {
        const listId = this.$route.params.listId;
        await this.load(listId);
    },
    watch: {
        async $route(to)
        {
            const listId = to.params.listId;
            await this.load(listId);
        }
    },
    methods: {
      onSelect (items) {
        this.selected = items
      },
      async load(listId)
      {
        const response = await axios.get(`/api/todo/items?listId=${listId}`);

        if (response.data)
            this.items = response.data;
        
        else
            this.items = [];
        }
    },
    };
</script>