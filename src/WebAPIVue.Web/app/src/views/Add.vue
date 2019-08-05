<template>
    <div class="add">
        <div class="form-group">
            <label for="exampleInputEmail1">User</label>
            <input type="text" v-model="inputUser" class="form-control" id="userInput" aria-describedby="user" placeholder="Enter name">
        </div>
        <select class="form-control" v-model="inputDepartment">
            <option v-for="department in departments" v-bind:value="department.id">{{department.title}}</option>
        </select>
        <button type="button" class="btn btn-primary" v-on:click="add">Add</button>
    </div>
</template>
<script lang="ts">import { Component, Prop, Vue } from 'vue-property-decorator';
    import { User } from '../models/user';
    import { Department } from '../models/department';
    import { RestService } from '../services/restService';

@Component
export default class Add extends Vue {
    private departments: Department[] = [];

    private inputUser!: string;
    private inputDepartment!: number;

    private mounted() {
        const serviceDepartment = new RestService<Department>('department');
        serviceDepartment.getList().then(departmentrResult => {
            this.departments = departmentrResult;
        });
    }

    private add() {
        const service = new RestService<User>('user');
        let user = new User();
        user.userName = this.inputUser;
        user.departmentId = this.inputDepartment;
        service.post(user).then((r) => this.$router.push('/'));
    }
}</script>
<style scoped lang="scss">

</style>