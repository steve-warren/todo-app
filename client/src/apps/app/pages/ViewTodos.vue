<template>
    <div>
        <md-table style="margin: 0px;" v-model="items" md-card @md-selected="onSelect">
        <md-table-toolbar class="md-dense">
            <md-button class="md-icon-button md-raised md-dense md-accent" @click="createTaskDialog.show = true">
                <md-icon>add</md-icon>
            </md-button>
        </md-table-toolbar>

        <md-table-row slot="md-table-row" slot-scope="{ item }" md-auto-select>
            <md-table-cell md-label="Name"><strong>{{ item.Name }}</strong></md-table-cell>
        </md-table-row>
        </md-table>
        <md-dialog :md-active.sync="createTaskDialog.show">
        <md-dialog-title>Create a Task</md-dialog-title>
            <div style="padding: 24px;">
                <md-field>
                    <label>Name</label>
                    <md-input v-model="createTaskDialog.model.name"></md-input>
                    <span class="md-helper-text">Give your task a name</span>
                </md-field>
                    <md-datepicker v-model="createTaskDialog.model.reminder" :md-open-on-focus="true" md-immediately>
                    <label>Reminder</label>
                    <span class="md-helper-text">Set a reminder</span>
                    </md-datepicker>
                <md-field>
                    <label>Priority</label>
                    <md-select v-model="createTaskDialog.model.priority">
                        <md-option value="None">None</md-option>
                        <md-option value="Low">Low</md-option>
                        <md-option value="Medium">Medium</md-option>
                        <md-option value="High">High</md-option>
                    </md-select>
                </md-field>
                <md-field>
                    <label>Notes</label>
                    <md-textarea v-model="createTaskDialog.model.notes"></md-textarea>
                </md-field>
            </div>
            <md-dialog-actions>
                <md-button class="md-raised md-accent" @click="create(); createTaskDialog.show = false">Create</md-button>
            </md-dialog-actions>
        </md-dialog>
    </div>
</template>

<script>
    import axios from 'axios';

    export default {
    name: "App",
    components: {
    },
    data: () => ({
        items: [],
        createTaskDialog:
        {
            show: false,
            model:
            {
                name: '',
                reminder: '',
                priority: '',
                listId: -1,
                notes: ''
            }
        }
    }),
    async created() {
        const listId = parseInt(this.$route.params.listId);
        this.createTaskDialog.model.listId = listId;
        await this.loadItems(listId);
    },
    watch: {
        async $route(to)
        {
            const listId = parseInt(to.params.listId);
            await this.loadItems(listId);
        }
    },
    methods: {
      onSelect (items) {
        this.selected = items
      },
      async loadItems(listId)
      {
          this.createTaskDialog.model.listId = listId;

        var url = '';

        if (listId > 0) url = `/api/todo/items?listId=${listId}`;
        else url = `/api/todo/items`;

        const response = await axios.get(url);

        if (response.data)
            this.items = response.data;
        
        else
            this.items = [];
        },
        async create()
        {
            var result = await axios.post('/api/todo/items', this.createTaskDialog.model);

            if (result.status == 200)
                this.clearForm();
        },
        clearForm()
        {
            var listId = this.createTaskDialog.model.listId;

            this.createTaskDialog.model = {
                name: '',
                reminder: '',
                priority: '',
                listId: listId,
                notes: ''
            };
        }
    },
    };
</script>

<style lang="scss" scoped>
  .md-dialog /deep/.md-dialog-container
  {
    width: 768px;
  }
</style>