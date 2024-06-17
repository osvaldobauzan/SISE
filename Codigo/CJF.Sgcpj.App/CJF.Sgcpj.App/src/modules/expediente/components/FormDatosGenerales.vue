<template>
  <h2>Datos generales</h2>

  <q-stepper
    v-model="step"
    header-nav
    ref="stepper"
    color="primary"
    animated
    alternative-labels
  >
    <q-step
      :name="1"
      title="Ingresa la información inicial"
      style="box-shadow: none"
    >
      <div class="row space-gap">
        <div class="col">
          <q-input
            filled
            v-model="FechaPresentacion"
            style="padding-bottom: 20px"
            placeholder="Fecha presentación"
          >
            <template v-slot:append>
              <div class="q-input__control-addon">
                <q-icon name="mdi-calendar-blank" class="cursor-pointer">
                  <q-popup-proxy
                    cover
                    transition-show="scale"
                    transition-hide="scale"
                  >
                    <q-date v-model="FechaPresentacion" mask="DD/MM/YYYY">
                      <div class="row items-center justify-end">
                        <q-btn
                          v-close-popup
                          label="Close"
                          color="primary"
                          flat
                        />
                      </div>
                    </q-date>
                  </q-popup-proxy>
                </q-icon>
              </div>
            </template>
          </q-input>
        </div>
        <div class="col">
          <q-input filled v-model="FechaIngreso" placeholder="Fecha ingreso">
            <template v-slot:append>
              <div class="q-input__control-addon">
                <q-icon name="mdi-calendar-blank" class="cursor-pointer">
                  <q-popup-proxy
                    cover
                    transition-show="scale"
                    transition-hide="scale"
                  >
                    <q-date v-model="FechaIngreso" mask="DD/MM/YYYY">
                      <div class="row items-center justify-end">
                        <q-btn
                          v-close-popup
                          label="Close"
                          color="primary"
                          flat
                        />
                      </div>
                    </q-date>
                  </q-popup-proxy>
                </q-icon>
              </div>
            </template>
          </q-input>
        </div>
      </div>
      <div class="row row-center space-gap">
        <div class="col">
          <q-input filled bottom-slots v-model="text" label="Mesa"> </q-input>
        </div>
        <div class="col">
          <q-select
            filled
            v-model="seleccionaSecretario"
            :options="secretarios"
            label="Selecciona un secretario"
          />
        </div>
      </div>
      <q-input
        filled
        v-model="text"
        type="textarea"
        placeholder="Observaciones"
      />
    </q-step>

    <q-step :name="2" title="Amparo promovido por interpósita persona">
      <div class="q-gutter-md">
        <div class="custom-radio" style="margin-left: 0px">
          <q-checkbox
            v-model="mayorEdad"
            checked-icon="check_box"
            unchecked-icon="check_box_outline_blank"
            indeterminate-icon="help"
            size="3rem"
          />
          <label class="custom-radio-label" for="radio-line"
            >Es mayor de edad
          </label>
        </div>
      </div>
      <div class="row row-center space-gap">
        <div class="col-6">
          <q-input filled bottom-slots v-model="nombre" label="Nombre">
            <template v-slot:hint>
              <q-icon size="xs" name="info" style="color: #24135f" />
              Persona que promueve a nombre del quejoso.
            </template>
          </q-input>
        </div>
      </div>
      <div class="q-gutter-md margin-top-div">
        <div class="custom-radio" style="margin-left: 0px">
          <q-checkbox
            v-model="demandaRatificada"
            checked-icon="check_box"
            unchecked-icon="check_box_outline_blank"
            indeterminate-icon="help"
            size="3rem"
          />
          <label class="custom-radio-label" for="radio-line"
            >La demanda de amparo se ratificó
          </label>
        </div>
      </div>
      <div class="row row-center space-gap">
        <div class="col">
          <q-input
            bottom-slots
            filled
            v-model="FechaRatificacion"
            style="padding-bottom: 20px"
            placeholder="Ratificación"
          >
            <template v-slot:append>
              <div class="q-input__control-addon">
                <q-icon name="mdi-calendar-blank" class="cursor-pointer">
                  <q-popup-proxy
                    cover
                    transition-show="scale"
                    transition-hide="scale"
                  >
                    <q-date v-model="FechaRatificacion" mask="DD/MM/YYYY">
                      <div class="row items-center justify-end">
                        <q-btn
                          v-close-popup
                          label="Close"
                          color="primary"
                          flat
                        />
                      </div>
                    </q-date>
                  </q-popup-proxy>
                </q-icon>
              </div>
            </template>
            <template v-slot:hint>
              <q-icon size="xs" name="info" style="color: #24135f" />
              Fecha de ratificación de la demanda.
            </template>
          </q-input>
        </div>
        <div class="col">
          <q-input
            bottom-slots
            filled
            v-model="FechaFenecioTermino"
            style="padding-bottom: 20px"
            placeholder="Feneció el término"
          >
            <template v-slot:append>
              <div class="q-input__control-addon">
                <q-icon name="mdi-calendar-blank" class="cursor-pointer">
                  <q-popup-proxy
                    cover
                    transition-show="scale"
                    transition-hide="scale"
                  >
                    <q-date v-model="FechaFenecioTermino" mask="DD/MM/YYYY">
                      <div class="row items-center justify-end">
                        <q-btn
                          v-close-popup
                          label="Close"
                          color="primary"
                          flat
                        />
                      </div>
                    </q-date>
                  </q-popup-proxy>
                </q-icon>
              </div>
            </template>
            <template v-slot:hint>
              <q-icon size="xs" name="info" style="color: #24135f" />
              Fecha en que feneció el término para ratificar la demanda
            </template>
          </q-input>
        </div>
      </div>
      <div class="row row-center space-gap margin-top-div">
        <div class="col">
          <q-input
            bottom-slots
            filled
            v-model="FechaSuspensionDefinitiva"
            style="padding-bottom: 20px"
            placeholder="Suspensión definitiva"
          >
            <template v-slot:append>
              <div class="q-input__control-addon">
                <q-icon name="mdi-calendar-blank" class="cursor-pointer">
                  <q-popup-proxy
                    cover
                    transition-show="scale"
                    transition-hide="scale"
                  >
                    <q-date
                      v-model="FechaSuspensionDefinitiva"
                      mask="DD/MM/YYYY"
                    >
                      <div class="row items-center justify-end">
                        <q-btn
                          v-close-popup
                          label="Close"
                          color="primary"
                          flat
                        />
                      </div>
                    </q-date>
                  </q-popup-proxy>
                </q-icon>
              </div>
            </template>
            <template v-slot:hint>
              <q-icon size="xs" name="info" style="color: #24135f" />
              Fecha en que se resolvió la suspensión definitiva del acto
              reclamado
            </template>
          </q-input>
        </div>
        <div class="col">
          <q-input
            bottom-slots
            filled
            v-model="FechaSuspensionProcedimiento"
            style="padding-bottom: 20px"
            placeholder="Suspensión del procedimiento"
          >
            <template v-slot:append>
              <div class="q-input__control-addon">
                <q-icon name="mdi-calendar-blank" class="cursor-pointer">
                  <q-popup-proxy
                    cover
                    transition-show="scale"
                    transition-hide="scale"
                  >
                    <q-date
                      v-model="FechaSuspensionProcedimiento"
                      mask="DD/MM/YYYY"
                    >
                      <div class="row items-center justify-end">
                        <q-btn
                          v-close-popup
                          label="Close"
                          color="primary"
                          flat
                        />
                      </div>
                    </q-date>
                  </q-popup-proxy>
                </q-icon>
              </div>
            </template>
            <template v-slot:hint>
              <q-icon size="xs" name="info" style="color: #24135f" />
              Fecha a partir de la que se suspendió el procedimiento (art. 18
              L.A.)
            </template>
          </q-input>
        </div>
      </div>
      <div class="row row-center space-gap margin-top-div">
        <div class="col">
          <q-input
            bottom-slots
            filled
            v-model="FechaConsignacionHechos"
            style="padding-bottom: 20px"
            placeholder="Consignación de los hechos"
          >
            <template v-slot:append>
              <div class="q-input__control-addon">
                <q-icon name="mdi-calendar-blank" class="cursor-pointer">
                  <q-popup-proxy
                    cover
                    transition-show="scale"
                    transition-hide="scale"
                  >
                    <q-date v-model="FechaConsignacionHechos" mask="DD/MM/YYYY">
                      <div class="row items-center justify-end">
                        <q-btn
                          v-close-popup
                          label="Close"
                          color="primary"
                          flat
                        />
                      </div>
                    </q-date>
                  </q-popup-proxy>
                </q-icon>
              </div>
            </template>
            <template v-slot:hint>
              <q-icon size="xs" name="info" style="color: #24135f" />
              Fecha en que se consignaron los hechos al Ministerio Público
              Federal.
            </template>
          </q-input>
        </div>
        <div class="col">
          <q-input
            bottom-slots
            filled
            v-model="FechaInicioTermino"
            style="padding-bottom: 20px"
            placeholder="Inicio de término"
          >
            <template v-slot:append>
              <div class="q-input__control-addon">
                <q-icon name="mdi-calendar-blank" class="cursor-pointer">
                  <q-popup-proxy
                    cover
                    transition-show="scale"
                    transition-hide="scale"
                  >
                    <q-date v-model="FechaInicioTermino" mask="DD/MM/YYYY">
                      <div class="row items-center justify-end">
                        <q-btn
                          v-close-popup
                          label="Close"
                          color="primary"
                          flat
                        />
                      </div>
                    </q-date>
                  </q-popup-proxy>
                </q-icon>
              </div>
            </template>
            <template v-slot:hint>
              <q-icon size="xs" name="info" style="color: #24135f" />
              Fecha a partir de la que inicia término para tener por no
              interpuesta la demanda.
            </template>
          </q-input>
        </div>
      </div>
      <div class="row row-center space-gap margin-top-div">
        <div class="col-6">
          <q-input
            bottom-slots
            filled
            v-model="FechaNoInterpuesta"
            style="padding-bottom: 20px"
            placeholder="No interpuesta"
          >
            <template v-slot:append>
              <div class="q-input__control-addon">
                <q-icon name="mdi-calendar-blank" class="cursor-pointer">
                  <q-popup-proxy
                    cover
                    transition-show="scale"
                    transition-hide="scale"
                  >
                    <q-date v-model="FechaNoInterpuesta" mask="DD/MM/YYYY">
                      <div class="row items-center justify-end">
                        <q-btn
                          v-close-popup
                          label="Close"
                          color="primary"
                          flat
                        />
                      </div>
                    </q-date>
                  </q-popup-proxy>
                </q-icon>
              </div>
            </template>
            <template v-slot:hint>
              <q-icon size="xs" name="info" style="color: #24135f" />
              Fecha en que se tuvo por no interpuesta la demanda (último párrafo
              del art. 18 L.A.)
            </template>
          </q-input>
        </div>
      </div>
    </q-step>

    <q-step :name="3" title="Ingreso o Egreso por Acuerdo del CJF">
      <div class="q-gutter-md">
        <div class="custom-radio" style="margin-left: 0px">
          <q-checkbox
            v-model="ingresoAcuerdo"
            checked-icon="check_box"
            unchecked-icon="check_box_outline_blank"
            indeterminate-icon="help"
            size="3rem"
          />
          <label class="custom-radio-label" for="radio-line"
            >Ingreso por Acuerdo
          </label>
        </div>
      </div>
      <div class="row row-center space-gap" v-if="ingresoAcuerdo">
        <div class="col">
          <q-input
            filled
            v-model="FechaIngresoAcuerdo"
            style="padding-bottom: 20px"
            placeholder="Ingreso por acuerdo"
          >
            <template v-slot:append>
              <div class="q-input__control-addon">
                <q-icon name="mdi-calendar-blank" class="cursor-pointer">
                  <q-popup-proxy
                    cover
                    transition-show="scale"
                    transition-hide="scale"
                  >
                    <q-date v-model="FechaIngresoAcuerdo" mask="DD/MM/YYYY">
                      <div class="row items-center justify-end">
                        <q-btn
                          v-close-popup
                          label="Close"
                          color="primary"
                          flat
                        />
                      </div>
                    </q-date>
                  </q-popup-proxy>
                </q-icon>
              </div>
            </template>
          </q-input>
        </div>
        <div class="col">
          <q-input
            filled
            bottom-slots
            v-model="ingresoAcuerdoTxt"
            label="Número de acuerdo"
          >
            <template v-slot:hint>
              <q-icon size="xs" name="info" style="color: #24135f" />
              Número de Acuerdo del Pleno del CJF por el que ingresó
            </template>
          </q-input>
        </div>
      </div>
      <div class="q-gutter-md">
        <div class="custom-radio" style="margin-left: 0px">
          <q-checkbox
            v-model="egresoAcuerdo"
            checked-icon="check_box"
            unchecked-icon="check_box_outline_blank"
            indeterminate-icon="help"
            size="3rem"
          />
          <label class="custom-radio-label" for="radio-line"
            >Egreso por Acuerdo
          </label>
        </div>
      </div>
      <div class="row row-center space-gap" v-if="egresoAcuerdo">
        <div class="col">
          <q-input
            filled
            v-model="FechaEgresoAcuerdo"
            style="padding-bottom: 20px"
            placeholder="Egreso por acuerdo"
          >
            <template v-slot:append>
              <div class="q-input__control-addon">
                <q-icon name="mdi-calendar-blank" class="cursor-pointer">
                  <q-popup-proxy
                    cover
                    transition-show="scale"
                    transition-hide="scale"
                  >
                    <q-date v-model="FechaEgresoAcuerdo" mask="DD/MM/YYYY">
                      <div class="row items-center justify-end">
                        <q-btn
                          v-close-popup
                          label="Close"
                          color="primary"
                          flat
                        />
                      </div>
                    </q-date>
                  </q-popup-proxy>
                </q-icon>
              </div>
            </template>
          </q-input>
        </div>
        <div class="col">
          <q-input
            filled
            bottom-slots
            v-model="egresoAcuerdoTxt"
            label="Número del Acuerdo"
          >
          </q-input>
        </div>
      </div>
      <div class="q-gutter-md">
        <div class="custom-radio" style="margin-left: 0px">
          <q-checkbox
            v-model="aplicacionDecretoReformo"
            checked-icon="check_box"
            unchecked-icon="check_box_outline_blank"
            indeterminate-icon="help"
            size="3rem"
          />
          <label class="custom-radio-label" for="radio-line"
            >Anotar si el asunto ingresó o egresó por CAR 53/2011. Aplicación
            del Decreto que reformó la Ley de INFONAVIT. 6 de enero de 1997 y en
            los que se reclama la incorporación de retiro voluntario y
            separación voluntaria.</label
          >
        </div>
      </div>
    </q-step>

    <q-step :name="4" title="Asuntos relacionados dentro del propio órgano">
      <div
        class="row row-center space-gap margin-top-div"
        v-for="(form, index) in ausntoRelacionalFormn"
        :key="index"
      >
        <div class="col-4">
          <q-input filled bottom-slots v-model="form.numero" label="Número">
            <template v-slot:hint>
              <q-icon size="xs" name="info" style="color: #24135f" />
              Número de expediente con el que tiene relación
            </template>
          </q-input>
        </div>
        <div class="col-6">
          <q-select
            filled
            v-model="form.tiposAsuntos"
            :options="tiposAsuntos"
            label="Selecciona un tipo de asunto"
          />
        </div>
        <div class="col" v-if="ausntoRelacionalFormn.length > 1">
          <q-btn flat icon="clear" class="btn-add" @click="borrarFormulario" />
        </div>
      </div>
      <div class="contenido-btn-add margin-top-div">
        <q-btn
          flat
          icon="add"
          label="Agregar"
          class="btn-add"
          @click="agregarFormulario"
        />
      </div>
    </q-step>
  </q-stepper>

  <template v-if="showAsuntosRelacionados">
    <div
      class="row row-center space-gap"
      v-for="(form, index) in ausntoRelacionalFormn"
      :key="index"
    >
      <div class="col-4">
        <q-input bottom-slots v-model="form.numero" label="Número">
          <template v-slot:hint>
            <q-icon size="xs" name="info" style="color: #24135f" />
            Número de expediente con el que tiene relación
          </template>
        </q-input>
      </div>
      <div class="col-6">
        <q-select
          v-model="form.tiposAsuntos"
          :options="tiposAsuntos"
          label="Selecciona un tipo de asunto"
        />
      </div>
      <div class="col" v-if="ausntoRelacionalFormn.length > 1">
        <q-btn flat icon="clear" class="btn-add" @click="borrarFormulario" />
      </div>
    </div>
    <div class="contenido-btn-add margin-top-div">
      <q-btn
        flat
        icon="add"
        label="Agregar"
        class="btn-add"
        @click="agregarFormulario"
      />
    </div>
  </template>
  <div class="btn-contenido">
    <q-btn color="primary" label="Guardar" />
  </div>
  <div class="space-bottom"></div>
</template>

<script setup>
import { ref, onMounted } from "vue";

const step = ref(1);

const FechaPresentacion = ref("");
const FechaIngreso = ref("");

const nombre = ref("");
const mayorEdad = ref(false);
const FechaRatificacion = ref("");

const FechaFenecioTermino = ref("");
const demandaRatificada = ref(false);
const FechaSuspensionDefinitiva = ref("");

const FechaSuspensionProcedimiento = ref("");
const FechaConsignacionHechos = ref("");

const FechaInicioTermino = ref("");
const FechaNoInterpuesta = ref("");

const ingresoAcuerdo = ref(false);
const ingresoAcuerdoTxt = ref("");
const FechaIngresoAcuerdo = ref("");

const egresoAcuerdo = ref(false);
const FechaEgresoAcuerdo = ref("");
const egresoAcuerdoTxt = ref("");

const aplicacionDecretoReformo = ref(false);

const secretarios = [
  "Secretario 1",
  "Secretario 2",
  "Secretario 3",
  "Secretario 4",
  "Secretario 5",
];
const seleccionaSecretario = ref("");

//const organoJuri = ref(false);
const tiposAsuntos = [
  "Asuntos 1",
  "Asuntos 2",
  "Asuntos 3",
  "Asuntos 4",
  "Asuntos 5",
];

const showAsuntosRelacionados = ref(false);
const ausntoRelacionalFormn = ref([]);

const agregarFormulario = () => {
  ausntoRelacionalFormn.value.push({ numero: "", tipoAsunto: null });
};

const borrarFormulario = (index) => {
  ausntoRelacionalFormn.value.splice(index, 1);
};

onMounted(() => {
  agregarFormulario();
});
</script>

<style scoped>
h2 {
  margin-top: 0px;
  margin-bottom: 0px;
  color: #000;
  font-size: 24px;
  font-family: Roboto, sans-serif;
  font-weight: 600;
}
.sub-title {
  color: #000;
  font-size: 16px;
}
.space-gap {
  gap: 1rem;
}
.seccion {
  color: #000;
  font-size: 14px;
  font-weight: 700;
}
.row-center {
  text-align: center;
}
.col-center {
  align-self: center;
}
.custom-radio-container {
  display: flex;
  align-items: center;
}
.custom-radio-container-center {
  justify-content: center;
}
.custom-radio {
  display: flex;
  align-items: center;
}

.custom-radio-input {
  margin-right: 8px;
}

.custom-radio-label {
  margin: 0;
  color: #000;
  font-size: 14px;
}
.space-bottom {
  height: 10rem;
}
.margin-top-div {
  margin-top: 1.9rem;
}
.contenido-btn-add {
  text-align: center;
}
.btn-add {
  color: #24135f;
  font-weight: bold;
}
.q-stepper__title {
  width: 1rem !important;
}
.q-stepper >>> .q-stepper__title {
  width: 9rem !important;
}
.q-stepper {
  box-shadow: none;
}
.btn-contenido {
  padding-left: 1.5rem;
  padding-bottom: 1.5rem;
  margin-top: 1rem;
}
</style>
