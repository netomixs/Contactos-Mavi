<template>
    <nav class="navbar navbar-expand-lg bg-body-tertiary">
        <div class="container-fluid">
            <a class="navbar-brand" href="#">Lista de contactos </a>

            <div class="d-flex" role="search">
                <input v-model="nombreInput" class="form-control me-2" type="search" placeholder="Buscar contacto"
                    aria-label="Search">
                <button class="btn btn-outline-success" @click="filtrarPorNombre()">Buscar</button>
            </div>
        </div>

    </nav>
    <div class="weather-component">

        <div v-if="contactos" class="content">
            <table class="table table-striped">
                <thead>
                    <tr>
                        <th scope="col" class="h5">Nombre</th>
                        <th scope="col" class="h5">Tipo</th>
                        <th scope="col" class="h5">Telefono fijo</th>
                        <th scope="col" class="h5">Telefono movil</th>
                        <th scope="col" class="h5">Fecha de nacimiento</th>
                        <th scope="col" class="h5">Sexo</th>
                        <th scope="col" class="h5">Estado civil</th>

                    </tr>

                </thead>
                <tbody>
                    <tr v-for="contacto in contactos" :key="contacto.id">
                        <td>{{ contacto.nombre }}</td>
                        <td>{{ getTipoContactoText(contacto.tipo_Contacto) }}</td>
                        <td>{{ contacto.telefono_Fijo }}</td>
                        <td>{{ contacto.telefono_Movil }}</td>
                        <td>{{
                            getFormaterFecha(contacto.nacimiento) }}</td>
                        <td>{{ getTipoSexo(contacto.sexo) }}</td>
                        <td>{{
                            getTipoEstosCivil(contacto.estado_Civil) }}</td>


                        <td> <button  class="btn btn-primary" @click="verDatos(contacto.id )">
                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor"
                                    class="bi bi-eye" viewBox="0 0 16 16">
                                    <path
                                        d="M16 8s-3-5.5-8-5.5S0 8 0 8s3 5.5 8 5.5S16 8 16 8M1.173 8a13 13 0 0 1 1.66-2.043C4.12 4.668 5.88 3.5 8 3.5s3.879 1.168 5.168 2.457A13 13 0 0 1 14.828 8q-.086.13-.195.288c-.335.48-.83 1.12-1.465 1.755C11.879 11.332 10.119 12.5 8 12.5s-3.879-1.168-5.168-2.457A13 13 0 0 1 1.172 8z" />
                                    <path
                                        d="M8 5.5a2.5 2.5 0 1 0 0 5 2.5 2.5 0 0 0 0-5M4.5 8a3.5 3.5 0 1 1 7 0 3.5 3.5 0 0 1-7 0" />
                                </svg>
                            </button></td>
                        <td> <button @click=" editarDatos(contacto.id )" class="btn btn-success"><svg xmlns="http://www.w3.org/2000/svg" width="16"
                                    height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16">
                                    <path
                                        d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" />
                                    <path fill-rule="evenodd"
                                        d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5z" />
                                </svg></button> </td>
                        <td> <button @click="confirmarAccion(  contacto.id )" class="btn btn-danger"> <svg
                                    xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor"
                                    class="bi bi-trash-fill" viewBox="0 0 16 16">
                                    <path
                                        d="M2.5 1a1 1 0 0 0-1 1v1a1 1 0 0 0 1 1H3v9a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V4h.5a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H10a1 1 0 0 0-1-1H7a1 1 0 0 0-1 1zm3 4a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 .5-.5M8 5a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7A.5.5 0 0 1 8 5m3 .5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 1 0" />
                                </svg></button> </td>

                    </tr>
                </tbody>
            </table>
            <button @click="nuevoRegistro()" type="button" class="btn btn-primary">Nuevo contacto</button>
        </div>
    </div>
    
</template>

<script lang="js">
import { defineComponent } from 'vue';
 
export default defineComponent({
 
    data() {
        return {
            loading: false,
            contactos: null,
            nombreInput: "",
        };
    },
    created() {

        this.fetchData();
 
    },
    watch: {

    },
    methods: {
        /**
         * Elimina el elemento de la base de datos y de la tabla
         * @param id 
         */
        confirmarAccion(id) {
            const confirmacion = window.confirm('¿Estás seguro de que quieres continuar?');

            if (confirmacion) {
                fetch('transacciones/'+id,
                    {
                        method: 'DELETE',
                    }
                ).then(response => {
                    if (!response.ok) {
                        throw new Error('Red de respuesta no OK');
                    } else {
                        const index = this.contactos.findIndex(contacto => contacto.id === id);
                        this.contactos.splice(index, 1);
                    }
                }
                )

            } else {

            }
        },
        /*
        *Utiliza la filtracion si es que hay texto e el input y si no carga todos los contactos
        */
        filtrarPorNombre() {
            this.loading = true;
            if (this.nombreInput.length > 0) {
                fetch('transacciones/' + this.nombreInput)
                    .then(r => r.json())
                    .then(json => {
                        this.contactos = json;
                        this.loading = false;
                        return;
                    });
            } else {
                this.fetchData();
            }

        },
        /**
         * Permita seleccionar la palabra abreviada
         * @param tipo 
         */
        getTipoContactoText(tipo) {
            const tipoMap = {
                'AM': 'Amigo',
                'FA': 'Familiar',
                'TR': 'Trabajo',
            };
            return tipoMap[tipo] || 'Desconocido';
        },
         /**
         * Permita seleccionar la palabra abreviada
         * @param tipo 
         */
        getTipoSexo(tipo) {
            const tipoMap = {
                'F': 'Mujer',
                'M': 'Hombre',

            };
            return tipoMap[tipo] || 'Desconocido';
        },
         /**
         * Permita seleccionar la palabra abreviada
         * @param tipo 
         */
        getTipoEstosCivil(tipo) {
            const tipoMap = {
                'S': 'Soltero',
                'C': 'Casado',
                'D': 'Divorciado',
                'V': 'Viudez',
                'U': 'Union libre',

            };
            return tipoMap[tipo] || 'Desconocido';
        },
        /**
         * Formate de la fecha
         * @param date 
         */
        getFormaterFecha(date) {
            if (!date) return '';
            const options = { year: 'numeric', month: 'long', day: 'numeric' };
            return new Date(date).toLocaleDateString('es-ES', options);
        },
        /**
         * Obtiene todos los contactos
         */
        fetchData() {
            this.contactos = null;
            this.loading = true;

            fetch('transacciones')
                .then(r => r.json())
                .then(json => {
                    this.contactos = json;
                    this.loading = false;
                    return;
                });
        },
        verDatos(id){
            this.$router.push('/form/'+id+'/1');
        },
        editarDatos(id){
            this.$router.push('/form/'+id+'/0');
        },
        nuevoRegistro(){
            this.$router.push('/form');
        }
    },
});
</script>

<style scoped></style>