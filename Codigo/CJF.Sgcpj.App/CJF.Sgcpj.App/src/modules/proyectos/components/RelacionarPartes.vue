<template>
  <p>
    Relaciona las <strong>Partes</strong> con sus <strong>{{ motivoPrincipal.title }}</strong>
  </p>
  <div v-if="sentidosPParte.length > 0">
    <q-tabs
      v-model="tabGeneral"
      keep-alive
      dense
      class="text-grey"
      active-color="primary"
      indicator-color="primary"
      align="justify"
      narrow-indicator
    >
      <q-tab
        name="TODOS"
        label="Todos"
        icon="people"
        />
      <q-tab
      name="PARTES"
      label="Partes" v-if="motivoId != motivosProyectoKeys.ActosReclamados"
      icon="person"
      />
    </q-tabs>
    <q-separator />
    <q-tab-panels keep-alive v-model="tabGeneral" animated>
      <q-tab-panel name="TODOS">
        <div class="column q-col-gutter-sm q-mb-md">
          <q-toolbar>
            <q-space />
            <q-btn
              color="primary"
              @click="addMotivo(0)"
              label="Nuevo registro"
              :disable="!sentidosPParte[0]?.at(-1).completed"
              icon="mdi-plus"
            />

          </q-toolbar>
          <div class="col">
            <q-banner
            class="light-blue-3 doc-note doc-note--tip"
            >
              <q-icon name="mdi-information" color="secondary" size="sm" />
              <label>
                Selecciona y arrastra las filas de los registros para reorganizarlas.
              </label>
            </q-banner>
          </div>

        </div>
        <draggable
          ghost-class="ghost"
          v-model="sentidosPParte[0]"
          @start="drag = true" @end="drag = false"
          :rules="[(val) => Validaciones.validaSelectRequerido(val)]"
          item-key="id">
          <template #item="{ element, index }">
            <q-card class="q-mb-md q-pa-md">
              <div class="row">
                <q-item-section style="max-width:min-content;">
                  <q-icon size="lg" name="mdi-drag" />
                </q-item-section>
                <q-item-section>
                  <q-selector>
                    <q-select
                      dense
                      filled
                      :label="motivoPrincipal.label"
                      :options="optionsProyectoMotivo"
                      v-model="element.motivo"
                      :loading="loadingCatalogs"
                      option-value="descripcion"
                      :rules="[(val) => Validaciones.validaSelectRequerido(val)]"
                      @update:model-value="cambioForm(0, index)"
                      option-label="descripcion">
                    </q-select>
                  </q-selector>
                </q-item-section>
                <q-item-section>
                  <q-selector>
                    <q-select
                      dense
                      filled
                      label="Sentido"
                      :options="optionsSentidoSentencia"
                      v-model="element.sentido"
                      :loading="loadingCatalogs"
                      option-value="id"
                      @update:model-value="cambioForm(0, index)"
                      :rules="[(val) => Validaciones.validaSelectRequerido(val)]"
                      option-label="descripcion">
                    </q-select>
                  </q-selector>
                </q-item-section>
                <q-item-section>
                  <q-input
                    dense
                    v-model="element.descripcion"
                    filled
                    @update:model-value="cambioForm(0, index)"
                    :rules="[(val) => Validaciones.validaSelectRequerido(val)]"
                    label="Descripción"
                    autogrow />
                </q-item-section>
                <q-item-section style="max-width:min-content;" >
                  <q-btn
                    @click="deleteMotivo(0, index)"
                    :disable="sentidosPParte[0].length <= 1"
                    push
                    size="xs"
                    color="negative"
                    round
                    icon="mdi-close" />
                </q-item-section>
              </div>
            </q-card>
          </template>
        </draggable>
        <q-separator></q-separator>
        <q-card-actions class="q-gutter-xl q-px-lg">
          <q-btn
            class="col"
            outline
            label="Regresar"
            no-caps
            color="primary"
            @click="emit('regresar')"
            text-color="primary"
          />
          <q-btn
            color="primary"
            class="col"
            @click="asignarMotivoTodos()"
            label="Subir"
            :disable="!sentidosPParte[0]?.at(-1).completed"
            no-caps
          />
          <q-btn
            class="col"
            outline
            @click="emit('cancelar')"
            label="Cancelar"
            no-caps
            color="primary"
            text-color="primary"
          />
        </q-card-actions>
      </q-tab-panel>
      <q-tab-panel name="PARTES">
        <q-tabs
          v-model="tab"
          dense
          class="text-grey"
          active-color="primary"
          indicator-color="primary"
          align="left"
          narrow-indicator>
          <template v-for="(parte, index) in partesRelacionadas" :key="index">
            <q-tab :class="sentidosPParte[index+1]?.at(-1).class" :label="`${parte.nombre} ${parte.aMaterno} ${parte.aPaterno}`" :name="parte.personaId"></q-tab>
          </template>
        </q-tabs>
        <q-separator />
        <q-tab-panels keep-alive v-model="tab" animated>
          <template v-for="(parte, indexParte) in partesRelacionadas" :key="indexParte">
            <q-tab-panel :name="parte.personaId">
              <div class="column q-col-gutter-sm q-mb-md">
              <q-toolbar>
                <q-space />
                <q-btn
                  color="primary"
                  @click="addMotivo(indexParte+1)"
                  label="Nuevo registro"
                  :disable="!sentidosPParte[indexParte+1]?.at(-1).completed"
                  icon="mdi-plus"
                />
              </q-toolbar>
              <div class="col">
                <q-banner
                class="light-blue-3 doc-note doc-note--tip"
                >
                  <q-icon name="mdi-information" color="secondary" size="sm" />
                  <label>
                    Selecciona y arrastra las filas de los registros para reorganizarlas.
                  </label>
                </q-banner>
              </div>
            </div>

              <draggable
                ghost-class="ghost"
                v-model="sentidosPParte[indexParte+1]" @start="drag = true"
                @end="drag = false"
                :rules="[(val) => Validaciones.validaSelectRequerido(val)]"
                item-key="id">
                <template #item="{ element, index }">
                  <q-card class="q-mb-md q-pa-md">
                    <div class="row">
                      <q-item-section style="max-width:min-content;">
                        <q-icon size="lg" name="mdi-drag" />
                      </q-item-section>
                      <q-item-section>
                        <q-selector>
                          <q-select
                            dense
                            filled
                            :label="motivoPrincipal.label"
                            :options="optionsProyectoMotivo"
                            v-model="element.motivo"
                            :loading="loadingCatalogs"
                            option-value="descripcion"
                            :rules="[(val) => Validaciones.validaSelectRequerido(val)]"
                            @update:model-value="cambioForm(indexParte+1, index)"
                            option-label="descripcion">
                          </q-select>
                        </q-selector>
                      </q-item-section>
                      <q-item-section>
                        <q-selector>
                          <q-select
                            dense
                            filled
                            label="Sentido"
                            :options="optionsSentidoSentencia"
                            v-model="element.sentido"
                            :loading="loadingCatalogs"
                            option-value="id"
                            @update:model-value="cambioForm(indexParte+1, index)"
                            :rules="[(val) => Validaciones.validaSelectRequerido(val)]"
                            option-label="descripcion">
                          </q-select>
                        </q-selector>
                      </q-item-section>
                      <q-item-section>
                        <q-input
                          dense
                          v-model="element.descripcion"
                          filled
                          @update:model-value="cambioForm(indexParte+1, index)"
                          :rules="[(val) => Validaciones.validaSelectRequerido(val)]"
                          label="Descripción"
                          autogrow />
                      </q-item-section>
                      <q-item-section style="max-width:min-content;">
                        <q-btn
                          @click="deleteMotivo(indexParte+1, index)"
                          :disable="sentidosPParte[indexParte+1].length <= 1"
                          push
                          size="xs"
                          color="negative"
                          round
                          icon="mdi-close"/>
                      </q-item-section>
                    </div>
                  </q-card>
                </template>
              </draggable>
              <q-separator></q-separator>
              <q-card-actions class="q-gutter-xl q-px-lg">
                <q-btn
                  class="col"
                  outline
                  label="Regresar"
                  no-caps
                  color="primary"
                  @click="emit('regresar')"
                  text-color="primary"
                />
                <q-btn
                  color="primary"
                  class="col"
                  @click="asignarMotivoPartes()"
                  label="Subir"
                  no-caps
                />
                <q-btn
                  class="col"
                  outline
                  @click="emit('cancelar')"
                  label="Cancelar"
                  no-caps
                  color="primary"
                  text-color="primary"
                />
              </q-card-actions>
            </q-tab-panel>
          </template>
        </q-tab-panels>
      </q-tab-panel>
    </q-tab-panels>
  </div >

</template>
<script setup>
import { onMounted, ref, onActivated } from "vue";
import draggable from 'vuedraggable';
import { useCatalogosStore } from "src/stores/catalogos-store";
import { manejoErrores } from "src/helpers/manejo-errores";
import { useUsuariosStore } from "src/stores/usuarios-store";
import { motivosProyecto, motivosProyectoKeys } from "../store/proyectos-store";
import { Validaciones } from "src/helpers/validaciones";

const catalogosStore = useCatalogosStore();
const registroPartes = ref([]);
const optionsProyectoMotivo = ref([]);
const usuariosStore = useUsuariosStore();
const partesRelacionadas = ref([]);
const sentidosPParte = ref([]);
const forms = ref([]);
const loadedParts = ref(false);
const motivoId = ref(motivosProyectoKeys.ActosReclamados);
const validateParts = ref(false);
const emit = defineEmits(["update:motivosPartes","regresar", "cancelar"]);
const neun = ref(null);

const props = defineProps({
  item: {
    type: Object,
    required: false,
    default: null,
  },
  version: {
    type: Object,
    required: true,
    default: null,
  },
  catTipoAsuntoId: {
    type: Number,
    required: true,
    default: null,
  },
  asuntoNeunId: {
    type: Number,
    required: true,
    default: null,
  }
});
const tab = ref("");
const tabGeneral = ref("TODOS");
const optionsSentidoSentencia = ref([]);
const resultadoMotivos = ref([]);
const motivoPrincipal = ref({
  label: "Motivo",
  title: "Motivos"
});

onMounted(async () => {
  neun.value = props.asuntoNeunId;

  await actualizarCatalogos();


});

onActivated(async () => {
  sentidosPParte.value = [];
  if(neun.value != props.asuntoNeunId)
    await actualizarCatalogos();
});

async function actualizarCatalogos() {


  try {
    await catalogosStore.obtenerProyectoMotivos(props.catTipoAsuntoId);
  } catch (error) {

    manejoErrores.mostrarError(error);
  }

  optionsProyectoMotivo.value = catalogosStore.proyectoMotivos.datos;
  motivoId.value = catalogosStore.proyectoMotivos.tipoCatalogo;
  motivoPrincipal.value = motivosProyecto[catalogosStore.proyectoMotivos.tipoCatalogo];

  optionsSentidoSentencia.value = catalogosStore.sentidosSentencia;

  sentidosPParte.value.push([{
    motivo: null,
    sentido: null,
    descripcion: null,
    completed: false,
    class: 'e-gray'
  }]);

  registroPartes.value = [];

  try {
    let partes = await usuariosStore.obtenerParteExistente(
      props.asuntoNeunId,
      null,
      3,
      1
    );
    partesRelacionadas.value = partes;
    tab.value = partes[0].personaId;

    partes.forEach(() => {
      forms.value.push(false);
      sentidosPParte.value.push([{
        motivo: null,
        sentido: null,
        descripcion: null,
        completed: false,
        class: 'e-gray'
      }]);
    });

    loadedParts.value = true;
  }
  catch (error) {
    manejoErrores.mostrarError(error);
  }
}

async function cambioForm(indexParte, indexMotivo) {
  let motivo = sentidosPParte.value[indexParte][indexMotivo];
  if (motivo.motivo != null && motivo.sentido != null && (motivo.descripcion != null && (motivo.descripcion?.trim() ?? "") != "")) {
    motivo.completed = true;
    motivo.class = 'e-green';
  }
  else {
    motivo.completed = false;
    motivo.class = 'e-gray';
  }

  validateParts.value = sentidosPParte.value.some( (parte,index) => {
    if(index == 0)
    return false;
    return !parte.at(-1).completed;
  });
}
function asignarMotivoTodos () {
  resultadoMotivos.value = [];
  partesRelacionadas.value.forEach((parte) => {
    sentidosPParte.value[0].forEach((sentido) => {
      resultadoMotivos.value.push({
        IdParte: parte.personaId,
        IdMotivo: sentido.motivo.id,
        IdSentido: sentido.sentido.id,
        Descripcion: sentido.descripcion
      });
    });
  });
  emit("update:motivosPartes", resultadoMotivos.value);
}
function asignarMotivoPartes () {
  resultadoMotivos.value = [];
  partesRelacionadas.value.forEach((parte, index) => {
    sentidosPParte.value[index+1].forEach((sentido) => {
      resultadoMotivos.value.push({
        IdParte: parte.personaId,
        IdMotivo: sentido.motivo.id,
        IdSentido: sentido.sentido.id,
        Descripcion: sentido.descripcion
      });
    });
  });
  emit("update:motivosPartes", resultadoMotivos.value);
}
function deleteMotivo(index, indexMotivo) {
  sentidosPParte.value[index].splice(indexMotivo, 1);
}
function addMotivo(index) {
  sentidosPParte.value[index].push({
    motivo: null,
    sentido: null,
    descripcion: null,
    completed: false,
    class: 'e-gray'
  });
}
</script>
<style scoped>
.ghost {
  opacity: 0.5;
  background: #c8ebfb;
}
.not-draggable {
  cursor: no-drop;
}
.q-icon {
  cursor: grab;
}
.e-green:before {
  content: " ";
  display: inline-block;
  background: #64b620;
  height: .8rem;
  width: .8rem;
  border-radius: 50%;
  margin-right: 4px;
}
.e-gray:before {
  content: " ";
  display: inline-block;
  background: #757575;
  height: .8rem;
  width: .8rem;
  border-radius: 50%;
  margin-right: 4px;
}

.q-field--with-bottom {
    padding-top: 20px;
}
</style>
