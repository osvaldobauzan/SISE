<template>
  <q-list>
    <q-form ref="formValido">
    <q-item>
      <q-item-section>
        <q-item-label class="text-subtitle1 text-bold">
          Información del Expediente
        </q-item-label>
        <div class="row wrap q-py-none">
          <q-item class="col-4 q-py-none">
            <q-item-section>
              <q-item-label class="text-grey-6">Expediente</q-item-label>
              <q-item-label>{{ item?.expediente.asuntoAlias }}</q-item-label>
            </q-item-section>
          </q-item>
          <q-item class="col-4 q-py-none">
            <q-item-section>
              <q-item-label class="text-grey-6">Tipo de asunto</q-item-label>
              <q-item-label>{{ item?.expediente.catTipoAsunto}}</q-item-label>
            </q-item-section>
          </q-item>
          <q-item class="col-4 q-py-none">
            <q-item-section>
              <q-select
                dense
                filled
                use-input
                input-debounce="0"
                v-model="cuadernoSelected"
                label="Cuaderno *"
                option-label="cuaderno"
                option-value="cuadernoId"
                @update:model-value="cambioForm"
                :loading="loadingCatalogs"
                :options="optionsCuaderno"
                :rules="[
                  (val) => Validaciones.validaSelectRequerido(val?.cuadernoId),
                ]"
                >
              </q-select>
            </q-item-section>
          </q-item>
        </div>
      </q-item-section>
    </q-item>

          <q-item>
            <q-item-section>
              <q-item-label class="text-subtitle1 text-bold">
              Información principal de la sentencia
              </q-item-label>
              <div class="row q-gutter-md">
                <div class="col">
                  <q-input ref="fechaAuto" dense filled class="col-6" v-model="fechaPromocion" label="Fecha de sentencia"
                    :rules="reglasFecha" @update:model-value="cambioFecha">
                    <template v-slot:append>
                      <q-icon name="mdi-calendar-month" class="cursor-pointer">
                        <q-popup-proxy cover transition-show="scale" transition-hide="scale">
                          <q-date @update:model-value="cambioFecha" v-model="fechaPromocion" mask="DD/MM/YYYY">
                            <div class="row items-center justify-end">
                              <q-btn v-close-popup label="Cerrar" color="primary" flat />
                            </div>
                          </q-date>
                        </q-popup-proxy>
                      </q-icon>
                    </template>
                  </q-input>
                </div>
                <div class="col">
                    <q-select
                      dense
                      filled
                      label="Tipo de Archivo"
                      option-label="descripcion"
                      :options="optionsSentido"
                      :loading="loadingCatalogs"
                      :rules="[
                        (val) => Validaciones.validaSelectRequerido(val?.empleadoId),
                      ]"
                      v-model="tipoArchivoSelected"
                      >
                    </q-select>
                    <!-- <q-item-label class="text-grey-6">Contenido</q-item-label>
                    <q-item-label>Sentencia</q-item-label> -->
                </div>
              </div>
            </q-item-section>
          </q-item>
          <q-item>
            <q-item-section>
              <div class="row q-gutter-md">
                <div class="col">
                  <q-select
                    dense
                    filled
                    label="Titular"
                    :options="optionsTitular"
                    v-model="titularSelected"
                    option-value="empleadoId"
                    option-label="nombreEmpleado"
                    :loading="loadingCatalogs"
                    @update:model-value="cambioForm"
                    :rules="[
                      (val) => Validaciones.validaSelectRequerido(val?.empleadoId),
                    ]"
                  >
                  </q-select>
                </div>
                <div class="col">
                  <q-select
                    dense
                    filled
                    class="col"
                    label="Secretario"
                    :options="optionsSecretario"
                    v-model="secretarioSelected"
                    option-value="empleadoId"
                    option-label="nombreEmpleado"
                    :loading="loadingCatalogs"
                    @update:model-value="cambioForm"
                    :rules="[
                      (val) => Validaciones.validaSelectRequerido(val?.empleadoId),
                    ]"
                  >
                  </q-select>
                </div>
              </div>
            </q-item-section>
          </q-item>

          <q-item>
            <q-item-section>
              <q-item-label>
                Sube el archivo word del engrose generado para este expediente
              </q-item-label>
              <q-item-label
                :style="file !== null ? '' : 'border: 3px dashed #ccc'"
                >
                <q-file
                  v-model="file"
                  borderless
                  class="full-width full-height"
                  accept=".doc, .docx"
                  max-file-size="30000000"
                  @update:model-value="updateFile"
                  :rules="[(val) => Validaciones.validaSelectRequerido(val)]"
                >
                <template v-if="!file" v-slot:prepend>
                  <div class="row label-file">
                    <div class="col">
                      <q-item-label>
                        <q-icon name="mdi-upload" />
                        Arrastra y suelta o
                        <q-btn no-caps flat padding="0px" color="light-blue">
                          busca un archivo
                        </q-btn>
                      </q-item-label>
                    </div>
                  </div>
                </template>
                <template v-slot:file>
                  <q-chip class="full-width full-height q-my-xs" square>
                    <q-avatar>
                      <q-icon :name="'insert_drive_file'" color="primary" />
                    </q-avatar>
                    <span class="ellipsis relative-position text-bold">
                      {{ file.name }}
                    </span>
                    <span
                      class="q-ml-xs text-grey q-pl-sm text-caption"
                      style="width: 15%"
                    >
                      {{
                        file.size / 1024 < 1024
                          ? (file.size / 1024).toFixed(1) + "KB"
                          : (file.size / 1024 / 1024).toFixed(1) + "MB"
                      }}
                    </span>
                    <q-tooltip>
                      {{ file.name }}
                    </q-tooltip>
                  </q-chip>
                </template>
                <template v-if="file" v-slot:after>
                  <q-item
                    dense-toggle
                    class="q-field-after"
                    clickable
                    @click="updateFile(null)"
                  >
                    <q-item-section align="left">
                      <q-icon
                        size="1.2em"
                        :name="'mdi-close'"
                        color="primary"
                      />
                    </q-item-section>
                  </q-item>
                </template>
              </q-file>
              </q-item-label>
              <q-item-label class="q-mt-xs">
                <q-icon name="mdi-information" color="info" class="q-mr-sm" />
                <span class="text-grey-8">Sólo puedes subir archivos menores a 30 Mb en formato
                  .DOC o .DOCX</span>
              </q-item-label>
            </q-item-section>
          </q-item>

        <!-- </q-card>
    </q-expansion-item> -->
    <q-space />
    <q-space />

    <!-- <q-expansion-item
        expand-separator
        icon="perm_identity"
        label="Información de la temática del asunto"
        caption="Campos indispensables"
        default-opened="true"
      >
        <q-card> -->
          <q-item>
            <q-item-section>
              <!-- TODO: precargar el tema que viene del proyecto en caso de ser nuevo registro y este cuente con un proyecto -->
              <q-item-label>
                Escribe brevemente el tema del asunto
              </q-item-label>
              <q-input
                type="textarea"
                outlined
                placeholder="Describir la temática del asunto o lo relevante, no colocar datos personales y/o datos sensibles. La longitud mínima es de 100 caracteres."
                @update:model-value="cambioForm"
                v-model="sintesis"
                    :rules="[
                      (val) => Validaciones.validaSelectRequerido(val),
                      (val) => Validaciones.validaLongitudMinima(val, 100),
                    ]"
              >

              </q-input>

              <!-- <q-input class="col" dense v-model="sintesis" filled type="textarea"
                  label="Escriba brevemente la temática del asunto" /> -->
              <!-- <q-editor v-model="sintesis" style="height: 100px;"></q-editor> -->
            </q-item-section>
          </q-item>
        <!-- </q-card>
    </q-expansion-item> -->

  </q-form>

  </q-list>

</template>

<script setup>
  import { ref, onMounted } from "vue";
  import { Validaciones } from "src/helpers/validaciones";
  import { manejoErrores } from "src/helpers/manejo-errores";
  import { useCatalogosStore } from "src/stores/catalogos-store";
  import { useEngroseStore } from "../../store/engrose-store";

  const catalogosStore = useCatalogosStore();
  const engroseStore = useEngroseStore();

  const sintesis = ref("");

  const optionsCuaderno = ref([]);
  const optionsTitular = ref([]);
  const optionsSecretario = ref([]);
  const optionsSentido = ref([]);

  const titularSelected = ref(null);
  const secretarioSelected = ref(null);
  const cuadernoSelected = ref(null);
  const tipoArchivoSelected = ref(null);
  const fechaPromocion = ref(null);

  const estatusFormulario = ref(false);

  const formValido = ref(false);

  const loadingCatalogs = ref(false);

  const file = ref(null);

  const props = defineProps({
    item: {
      type: Object,
      required: true,
    }
  });

  onMounted( async() => {
    loadingCatalogs.value = true;
    try {
      await catalogosStore.obtenerCuadernos(props.item.expediente.catTipoAsuntoId);
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
    optionsCuaderno.value = catalogosStore.cuadernos;

    try {
      await catalogosStore.obtenerTitulares();
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
    optionsTitular.value = catalogosStore.titulares;

    try {
      await catalogosStore.obtenerSecretarios();
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
    optionsSecretario.value = catalogosStore.secretarios;

    try {
      await catalogosStore.obtenerSentidosSentencias(props.item.expediente.catTipoAsuntoId);
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
    optionsSentido.value = catalogosStore.sentidosSentencia;
    loadingCatalogs.value = false;
  });

  async function updateFile(value) {
    engroseStore.file = value;
  }

  function cleanString(string) {
    return string.replaceAll(/&nbsp;/g, " ").replaceAll(/<[^>]*>?/gm, "");
  }

  async function cambioFecha(value) {
    engroseStore.sentenciaData.FechaAuto = convertDateFormat(value);
  }
  function convertDateFormat(inputDate) {
    let [day, month, year] = inputDate.split('/');
    let date = new Date(year, month - 1, day);
    let formattedDate = date.toISOString().split('.')[0];
    return formattedDate;
  }

  async function cambioForm() {
    estatusFormulario.value = await formValido.value?.validate(false);
    await formValido.value?.resetValidation();
    engroseStore.sentenciaData.TipoCuadernoId = cuadernoSelected.value?.id;
    engroseStore.sentenciaData.TitularId = titularSelected.value?.empleadoId;
    engroseStore.sentenciaData.SecretarioPId = secretarioSelected.value?.empleadoId;
    engroseStore.sentenciaData.tipoArchivoSelected = tipoArchivoSelected.value?.id;
    engroseStore.sentenciaData.Resumen = cleanString(sintesis.value);
  }

</script>
<style>
.q-file {
  [role="alert"] {
    text-align: center;
    margin-bottom: 10px;
  }
}
.fileUp {
  .q-field__bottom {
    margin-bottom: 1rem;
  }
}
.text-subtitle1 {
  overflow: auto;
  text-overflow: initial;
  white-space: normal;
}
</style>
