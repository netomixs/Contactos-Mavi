<template>
    <div class="container">
        <form class="row g-3 needs-validation" novalidate @submit.prevent="handleSubmit">
            <div class="col-md-4">
                <label for="validationNombre" class="form-label">Nombre</label>
                <input type="text" class="form-control" id="validationNombre" v-model="nombreInput"
                    :class="{ 'is-invalid': formSubmitted && !isValidNombre }" required />
                <div v-if="formSubmitted && !isValidNombre" class="invalid-feedback">
                    Completa este campo
                </div>
            </div>

            <div class="col-md-4">
                <label for="validationContacto" class="form-label">Tipo de contacto</label>
                <select id="validationContacto" class="form-select" v-model="contactoInput"
                    :class="{ 'is-invalid': formSubmitted && !isValidContacto }" required>
                    <option value="">Selecciona...</option>
                    <option value="AM">Amigo</option>
                    <option value="FA">Familiar</option>
                    <option value="TR">Trabajo</option>
                </select>
                <div v-if="formSubmitted && !isValidContacto" class="invalid-feedback">
                    Selecciona un tipo de contacto
                </div>
            </div>

            <div class="col-md-4">
                <label for="validationFecha" class="form-label">Fecha de nacimiento</label>
                <div class="input-group has-validation">
                    <span class="input-group-text" id="inputGroupPrepend">@</span>
                    <input type="date" class="form-control" id="validationFecha" aria-describedby="inputGroupPrepend"
                        v-model="fechaInput" :class="{ 'is-invalid': formSubmitted && !isValidFecha }" required />
                    <div v-if="formSubmitted && !isValidFecha" class="invalid-feedback">
                        Ingresa una fecha válida
                    </div>
                </div>
            </div>

            <div class="col-md-4">
                <label for="validationSexo" class="form-label">Sexo</label>
                <select id="validationSexo" class="form-select" v-model="sexoInput"
                    :class="{ 'is-invalid': formSubmitted && !isValidSexo }" required>
                    <option value="">Selecciona...</option>
                    <option value="F">Mujer</option>
                    <option value="M">Hombre</option>
                </select>
                <div v-if="formSubmitted && !isValidSexo" class="invalid-feedback">
                    Selecciona un sexo
                </div>
            </div>

            <div class="col-md-4">
                <label for="validationEstdo" class="form-label">Estado civil</label>
                <select id="validationEstdo" class="form-select" v-model="estado_CivilInput"
                    :class="{ 'is-invalid': formSubmitted && !isValidEstadoCivil }" required>
                    <option value="">Selecciona...</option>
                    <option value="S">Soltero</option>
                    <option value="C">Casado</option>
                    <option value="D">Divorciado</option>
                    <option value="U">Unión libre</option>
                </select>
                <div v-if="formSubmitted && !isValidEstadoCivil" class="invalid-feedback">
                    Selecciona un estado civil
                </div>
            </div>

            <div class="row g-3">
                <div class="col-4">
                    <label for="validationTelefonoMovil" class="form-label">Teléfono móvil</label>
                    <input type="tel" class="form-control" id="validationTelefonoMovil" v-model="telefono_MovilInput"
                        :class="{ 'is-invalid': formSubmitted && !isValidTelefonoMovil }" required />
                    <div class="invalid-feedback">
                        El teléfono móvil debe ser un número y tener menos de 13 caracteres
                    </div>
                </div>

                <div class="col-4">
                    <label for="validationTelefonoFijo" class="form-label">Teléfono fijo</label>
                    <input type="tel" class="form-control" id="validationTelefonoFijo"
                        :class="{ 'is-invalid': formSubmitted && telefono_fijoInput && !isValidTelefonoFijo }"
                        v-model="telefono_fijoInput" />
                    <div class="invalid-feedback">
                        El teléfono fijo debe ser un número y tener menos de 13 caracteres
                    </div>
                </div>
            </div>

            <div class=" ">

                <button :disabled="!isNuevoRegistro" type="submit" class="  btn btn-primary">Registrar</button>
                <button @click="actualizarDatos()" :disabled="isSoloVista" type="button" class="ms-3 btn btn-success">Editar datos</button>
                <button @click="eliminar()" :disabled="isSoloVista"type="button" class="ms-3 btn btn-danger">Eliminar</button>

            </div>
        </form>
    </div>
</template>
<script lang="js">
import { defineComponent } from 'vue';

export default defineComponent({
    created() {
        this.getParamatroId()
    },
    data() {
        return {
            loading: false,
            contactos: null,
            nombreInput: "",
            contactoInput: "",
            fechaInput: "",
            sexoInput: "",
            estado_CivilInput: "",
            telefono_fijoInput: "",
            telefono_MovilInput: "",
            formSubmitted: false,
            id: 0,
            isEditable: false
        };

    }, computed: {
        isNuevoRegistro() {
            if (this.$route.params.id) {
                return false;
            }else{
                return true;
            }
        },
        isSoloVista() {
            if (this.$route.params.view) {
                if (this.$route.params.view == 1) {
                    return true;
                } else {
                    false
                }
            } else {
                return true;
            }

        },
        userId() {

            return this.$route.params.id;
        },
        isValidNombre() {
            return this.nombreInput.trim() !== "";
        },
        isValidContacto() {
            return this.contactoInput !== "";
        },
        isValidFecha() {
            return this.fechaInput !== "";
        },
        isValidSexo() {
            return this.sexoInput !== "";
        },
        isValidEstadoCivil() {
            return this.estado_CivilInput !== "";
        },
        isValidTelefonoMovil() {
            const telefonoMovil = this.telefono_MovilInput.trim();
            return telefonoMovil === "" || (telefonoMovil.length <= 12 && /^[0-9]+$/.test(telefonoMovil));
        },
        isValidTelefonoFijo() {
            const telefonoFijo = this.telefono_fijoInput.trim();
            return (telefonoFijo.length <= 12 && /^[0-9]*$/.test(telefonoFijo));
        }
    },
    methods: {
        /**
         * Validacion de campo y envio de datos
         */
        handleSubmit() {
            this.formSubmitted = true;
            console.log(this.isValidTelefonoFijo);
            if (
                this.isValidNombre &&
                this.isValidContacto &&
                this.isValidFecha &&
                this.isValidSexo &&
                this.isValidEstadoCivil &&
                this.isValidTelefonoMovil
            ) {

                try {
                    var data = {

                        "id": 0,
                        "nombre": this.nombreInput,
                        "tipo_Contacto": this.contactoInput,
                        "telefono_Fijo": this.telefono_fijoInput,
                        "telefono_Movil": this.telefono_MovilInput,
                        "nacimiento": this.fechaInput,
                        "sexo": this.sexoInput,
                        "estado_Civil": this.estado_CivilInput
                    }
                    fetch('transacciones', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json',
                        },
                        body: JSON.stringify(data)
                    }).then(response => {
                        if (!response.ok) {

                        }
                        return response.json();
                    })
                        .then(data => {
                            console.log('Success:', data);
                            window.alert("Registrado")
                        })
                        .catch(error => {
                            console.error('Error:', error);
                        })
                    this.resetForm;
                } catch (error) {
                    console.error('Error:', error);
                    alert('Hubo un problema .');
                }
            } else {


            }
        },
        /**
         * Obtiene los datos de las base de datos
         * @param id 
         */
        getDatos(id) {
            fetch('transacciones/contacto/' + id)
                .then(r => r.json())
                .then(json => {
                    this.contactos = json;
                    this.cargarDatos(json);
                    console.log(json);


                    return;
                });
        },
        /**
         * Verifica si hay un Id y si lo hay cargar los dato de la base de datos
         */
        getParamatroId() {
            console.log(this.userId);
            if (!this.isNuevoRegistro) {
                if (this.userId) {
                    this.getDatos(this.userId)

                }
            }


        },
        /**
         * 
         * @param datos Carga los datos en el formulario
         */
        cargarDatos(datos) {
            this.nombreInput = datos.nombre;
            this.contactoInput = datos.tipo_Contacto;
            this.telefono_fijoInput = datos.telefono_Fijo;
            this.telefono_MovilInput = datos.telefono_Movil;
            this.fechaInput = this.getSimpleDate(datos.nacimiento);
            this.sexoInput = datos.sexo;
            this.estado_CivilInput = datos.estado_Civil;

        },
        /**
         * Convierte el formato de fecha a YYYY-MM-DD
         * @param dateString 
         */
        getSimpleDate(dateString) {
            const date = new Date(dateString);
            const year = date.getFullYear();
            const month = String(date.getMonth() + 1).padStart(2, '0');
            const day = String(date.getDate()).padStart(2, '0');
            return `${year}-${month}-${day}`;
        },
        /**
         * Metodo para elimiar regitro
         */
        eliminar() {
            const confirmacion = window.confirm('¿Estás seguro de que quieres continuar?');

            if (confirmacion) {
                fetch('transacciones/'+this.userId,
                    {
                        method: 'DELETE',
                    }
                ).then(response => {
                    if (!response.ok) {
                        throw new Error('Red de respuesta no OK');
                    } else {
                        this.$router.push('/');
                    }
                }
                )

            } else {

            }
        },
        /**
         * Metodo para actualizar regitro
         */
        actualizarDatos(){
            this.formSubmitted = true;
            console.log(this.isValidTelefonoFijo);
            if (
                this.isValidNombre &&
                this.isValidContacto &&
                this.isValidFecha &&
                this.isValidSexo &&
                this.isValidEstadoCivil &&
                this.isValidTelefonoMovil
            ) {

                try {
                    var data = {

                        "id": this.userId,
                        "nombre": this.nombreInput,
                        "tipo_Contacto": this.contactoInput,
                        "telefono_Fijo": this.telefono_fijoInput,
                        "telefono_Movil": this.telefono_MovilInput,
                        "nacimiento": this.fechaInput,
                        "sexo": this.sexoInput,
                        "estado_Civil": this.estado_CivilInput
                    }
                    fetch('transacciones', {
                        method: 'PUT',
                        headers: {
                            'Content-Type': 'application/json',
                        },
                        body: JSON.stringify(data)
                    }).then(response => {
                        if (!response.ok) {

                        }
                        return response.json();
                    })
                        .then(data => {
                            console.log('Success:', data);
                            window.alert("Actualizado")
                        })
                        .catch(error => {
                            console.error('Error:', error);
                        })
                    this.resetForm;
                } catch (error) {
                    console.error('Error:', error);
                    alert('Hubo un problema .');
                }
            } else {


            }
        }
    }


})
</script>
<style scoped></style>