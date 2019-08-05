<template>
    <div class="list">
        <div class="btn-group mb-5" role="group">
            <router-link type="button" class="btn btn-primary" to="/add">Add</router-link>
            <router-link type="button" class="btn btn-primary" to="/edit">Edit</router-link>
            <router-link type="button" class="btn btn-primary" to="/delete">Delete</router-link>
        </div>
    </div>
</template>

<script lang="ts">import { Component, Vue } from 'vue-property-decorator';

@Component
export default class Menu extends Vue {

}</script>
<style scoped lang="scss">

</style>