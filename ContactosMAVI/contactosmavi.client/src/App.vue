<script setup>
 
    import Tabla from './components/Tabla.vue'
    import Formulario from './components/Formulario.vue'
</script>

<template >
   <nav class="navbar navbar-expand-lg bg-body-tertiary">
        <div class="container-fluid">
            <a class="navbar-brand" href="#">Mavi</a>

        
        </div>

    </nav>
<div class="container">
    <router-view></router-view>
 
</div>
   
</template>

<style scoped>
 
</style>
