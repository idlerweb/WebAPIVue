<template>
    <div class="crud">
        <table class="table">
            <thead>
                <tr>
                    <th scope="col">#</th>
                    <th scope="col">UserName</th>
                    <th scope="col">Department</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="user in users">
                    <td>{{user.id}}</td>
                    <td>{{user.userName}}</td>
                    <td>{{user.departmentName}}</td>
                </tr>
            </tbody>
        </table>
    </div>
</template>
<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import { User } from '../models/user';
    import { Department } from '../models/department';
    import { RestService } from '../services/restService';

@Component
export default class List extends Vue {
    private departments: Department[] = [];
    private users: User[] = [{
        id: 0,
        userName: "Загрузка....",
        departmentName: "....",
        departmentId: 0
    }];

    private inputUser!: string;
    private inputDepartment!: number;

    private mounted() {
        const service = new RestService<User>('user');
        service.getList().then(userResult => {
            const serviceDepartment = new RestService<Department>('department');
            serviceDepartment.getList().then(departmentrResult => {
                this.departments = departmentrResult;
                let users = userResult.map(u => {
                    let user = new User();
                    user.id = u.id;
                    user.userName = u.userName;
                    user.departmentId = u.departmentId;
                    const departmentName = departmentrResult.find((d) => d.id == u.departmentId);
                    user.departmentName = departmentName ? departmentName.title : '';
                    return user;
                });
                this.users = users;
            });
        });
    }

    private add() {
        const service = new RestService<User>('user');
        let user = new User();
        user.userName = this.inputUser;
        user.departmentId = this.inputDepartment;
        service.post(user);
    }
}
</script>
<style scoped lang="scss">
    .modal-mask {
  position: fixed;
  z-index: 9998;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, .5);
  display: table;
  transition: opacity .3s ease;
}

.modal-wrapper {
  display: table-cell;
  vertical-align: middle;
}
</style>