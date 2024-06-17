<template>
  <q-card style="min-width: 40vw">
    <q-form ref="form" @submit="guardar">
      <div class="stickyTop">
        <q-toolbar>
          <q-toolbar-title class="text-bold">
            {{
              edicion
                ? "Editar acuerdo"
                : expediente
                  ? "Subir acuerdo"
                  : "Subir acuerdo de estado de autos"
            }}
          </q-toolbar-title>
          <q-btn flat round dense icon="mdi-close" @click="emit('cerrar')" />
        </q-toolbar>
        <q-separator></q-separator>
      </div>

      <q-card-section class="q-gutter-sm">
        <q-item-label class="text-subtitle1 text-bold pad-left"
          >Datos del expediente</q-item-label
        >
        <template v-if="expediente">
          <div class="row wrap">
            <q-item class="col-4">
              <q-item-section>
                <q-item-label class="text-grey-6">Expediente</q-item-label>
                <q-item-label>
                  {{ expediente?.expediente?.asuntoAlias }}
                </q-item-label>
              </q-item-section>
            </q-item>
            <q-item class="col-4">
              <q-item-section>
                <q-item-label class="text-grey-6">Tipo de asunto</q-item-label>
                <q-item-label>
                  {{ expediente?.expediente?.catTipoAsunto }}
                </q-item-label>
              </q-item-section>
            </q-item>
            <q-item class="col-4">
              <q-item-section>
                <q-item-label class="text-grey-6">Cuaderno</q-item-label>
                <q-item-label>
                  {{ expediente?.nombreTipoCuaderno }}
                </q-item-label>
              </q-item-section>
            </q-item>
          </div>
          <div
            v-if="expediente?.expediente?.tipoProcedimiento"
            class="row wrap"
          >
            <q-item class="col">
              <q-item-section>
                <q-item-label class="text-grey-6"
                  >Tipo de procedimiento</q-item-label
                >
                <q-item-label>
                  {{ expediente?.expediente?.tipoProcedimiento }}
                </q-item-label>
              </q-item-section>
            </q-item>
          </div>
        </template>
        <template v-else>
          <div class="row wrap">
            <q-select
              clearable
              v-cortarLabel
              @input-value="expedienteEncontrado = null"
              class="col"
              label="Buscar un expediente existente *"
              v-model="expedienteEncontrado"
              @filter="buscarExpedientePorNumero"
              :options="opcionesExpediente"
              option-value="asuntoNeunId"
              use-input
              input-debounce="0"
              hint="Formato Número/AAAA"
              @update:model-value="cambioExpediente"
              dense
              filled
              :rules="[(val) => Validaciones.validaSelectRequerido(val)]"
            >
              <template v-slot:append>
                <q-btn flat round icon="mdi-magnify" />
              </template>
              <template v-slot:no-option>
                <q-item>
                  <q-item-section class="text-red row">
                    <span>
                      <q-icon name="info" /> No se encontraron resultados
                    </span>
                  </q-item-section>
                </q-item>
              </template>
              <template v-slot:option="scope">
                <q-item v-bind="scope.itemProps">
                  <q-item-section>
                    <q-item-label>{{ scope.opt.asuntoAlias }}</q-item-label>
                    <q-item-label caption>
                      {{ scope.opt.tipoAsunto }}
                    </q-item-label>
                    <q-item-label
                      class="text-caption"
                      v-if="scope.opt.tipoProcedimiento !== ''"
                    >
                      {{ scope.opt.tipoProcedimiento }}
                    </q-item-label>
                  </q-item-section>
                </q-item>
              </template>
            </q-select>
            <q-select
              v-if="esAmparoEnRevision"
              v-cortarLabel
              @input-value="clasificacionAmparoEnRevision = null"
              dense
              filled
              class="col q-pl-lg"
              use-input
              input-debounce="0"
              v-model="clasificacionAmparoEnRevision"
              label="Clasificación amparo en revision *"
              option-label="descripcion"
              option-value="id"
              @filter="filtrarAmparoEnRevision"
              @update:model-value="cambioAmparoEnRevision"
              :options="amparoEnRevisionOptions"
              :loading="buscandoAmparoEnRevision"
              :rules="[(val) => Validaciones.validaSelectRequerido(val?.id)]"
            ></q-select>
            <q-select
              v-cortarLabel
              @input-value="cuaderno = null"
              dense
              filled
              class="col q-pl-lg"
              use-input
              input-debounce="0"
              v-model="cuaderno"
              label="Cuaderno *"
              option-label="cuaderno"
              option-value="cuadernoId"
              @filter="filtrarCuaderno"
              @update:model-value="cambioForm"
              :options="cuadernoOptions"
              :loading="buscandoCuadernos"
              :rules="[
                (val) => Validaciones.validaSelectRequerido(val?.cuadernoId),
              ]"
            >
            </q-select>
          </div>
        </template>
        <q-item-label class="text-subtitle1 text-bold pad-left"
          >Acuerdo</q-item-label
        >
        <div class="row wrap">
          <q-input
            ref="fechaAcuerdo"
            dense
            filled
            class="col-6"
            v-model="fechaPromocion"
            label="Fecha de acuerdo *"
            :rules="reglasFecha"
            @update:model-value="cambioFecha"
          >
            <template v-slot:append>
              <q-icon name="mdi-calendar-month" class="cursor-pointer">
                <q-popup-proxy
                  cover
                  transition-show="scale"
                  transition-hide="scale"
                >
                  <q-date
                    @update:model-value="cambioFecha"
                    v-model="fechaPromocion"
                    mask="DD/MM/YYYY"
                  >
                    <div class="row items-center justify-end">
                      <q-btn
                        v-close-popup
                        label="Cerrar"
                        color="primary"
                        flat
                      />
                    </div>
                  </q-date>
                </q-popup-proxy>
              </q-icon>
            </template>
          </q-input>
          <q-select
            v-cortarLabel
            @input-value="contenido = null"
            dense
            filled
            class="col q-pl-lg"
            use-input
            input-debounce="0"
            v-model="contenido"
            label="Contenido *"
            option-label="descripcion"
            @filter="filtrarContenido"
            option-value="id"
            :options="contenidoOptions"
            @update:model-value="cambiaContenido"
            :loading="buscnadoContenidos"
            :rules="[(val) => Validaciones.validaSelectRequerido(val?.id)]"
          ></q-select>
        </div>

        <div v-if="muestraAudiencia">
          <div class="row wrap">
            <q-item-label class="text-subtitle1 text-bold pad-left"
              >Datos de la audiencia</q-item-label
            >
          </div>
          <div class="row wrap">
            <q-select
              v-cortarLabel
              @input-value="tipoAudiencia = null"
              dense
              filled
              class="col-6"
              use-input
              input-debounce="0"
              v-model="tipoAudiencia"
              label="Tipo de audiencia *"
              option-label="descripcion"
              option-value="id"
              :options="tipoAudienciaOptions"
              @update:model-value="cambiaTipoAudiencia"
              :loading="buscnadoTipoAudiencia"
              :rules="[(val) => Validaciones.validaSelectRequerido(val?.id)]"
            ></q-select>
            <q-select
              v-if="muestraResultadoAudiencia"
              v-cortarLabel
              @input-value="resultadoAudiencia = null"
              dense
              filled
              class="col q-pl-lg"
              use-input
              input-debounce="0"
              v-model="resultadoAudiencia"
              label="Resultado de la audiencia *"
              option-label="descripcion"
              option-value="idResultado"
              :options="resultadoAudienciaOptions"
              @update:model-value="cambioForm"
              :loading="buscnadoResultadoAudiencia"
              :rules="[
                (val) => Validaciones.validaSelectRequerido(val?.idResultado),
              ]"
            ></q-select>
            <q-select
              v-if="muestraAudiencias"
              v-cortarLabel
              @input-value="idAgenda = null"
              dense
              filled
              class="col q-pl-lg"
              use-input
              input-debounce="0"
              v-model="idAgenda"
              label="Audiencias *"
              option-label="fechaAudienciaCadena"
              option-value="idAgenda"
              :options="audienciasOptions"
              @update:model-value="cambioForm"
              :loading="buscnadoAudiencia"
              :rules="[
                (val) => Validaciones.validaSelectRequerido(val?.idAgenda),
              ]"
            ></q-select>
          </div>
        </div>
        <div :style="file !== null ? '' : 'border: 3px dashed #ccc'">
          <q-file
            :readonly="edicion && file !== null && file?.name == fileCopy?.name"
            :disable="expediente?.estado == 3 || expediente?.estado == 4"
            ref="fileAcuerdo"
            :model-value="file"
            borderless
            @update:model-value="(val) => updateFiles(val, index)"
            class="full-width full-height"
            accept=".docx, .doc"
            max-file-size="30000000"
            @rejected="(err) => manejoErrores.archivoInvalido(err, 'Word')"
            :rules="[
              (val) => Validaciones.validaInputRequerido(val),
              async (val) =>
                edicion && (fileCopy == null || file?.name == fileCopy?.name)
                  ? true
                  : await Validaciones.validaExtension(val, extraerExtension(val)),
            ]"
          >
            <template v-if="!file" v-slot:prepend>
              <div class="row label-file">
                <div class="col">
                  <q-item-label>
                    <q-icon name="mdi-upload" />Arrastra y suelta o
                    <q-btn no-caps flat padding="0px" color="light-blue"
                      >busca un archivo</q-btn
                    >
                  </q-item-label>
                </div>
              </div>
            </template>
            <template v-slot:file>
              <q-chip class="full-width full-height q-my-xs" square>
                <q-avatar>
                  <q-icon :name="'insert_drive_file'" color="primary" />
                </q-avatar>
                <div style="width: 30%" class="ellipsis relative-position">
                  <span class="text-bold text-body2">{{ file.name }}</span>
                  <span
                    class="q-ml-md text-grey text-caption"
                    style="width: 15%"
                  >
                    {{
                      file.size / 1024 < 1024
                        ? (file.size / 1024).toFixed(1) + "KB"
                        : (file.size / 1024 / 1024).toFixed(1) + "MB"
                    }}
                  </span>
                </div>
                <q-tooltip>
                  {{ file.name }}
                </q-tooltip>
              </q-chip>
            </template>
            <template v-if="file" v-slot:after>
              <q-btn
                v-if="
                  edicion && (fileCopy == null || file?.name == fileCopy?.name)
                "
                dense-toggle
                class="q-field-after"
                color="blue"
                flat
                dense
                no-caps
                :disable="expediente?.estado == 3 || expediente?.estado == 4"
                @click="
                  fileCopy = fileCopy ? fileCopy : file;
                  updateFiles(null);
                "
              >
                <q-tooltip
                  v-if="expediente?.estado == 3 || expediente?.estado == 4"
                >
                  Ya no es posible reemplazar el archivo una vez firmado
                </q-tooltip>
                <q-tooltip v-else> Reemplazar </q-tooltip>
                <q-item-section
                  class="text-caption text-capitalize items-center justify"
                >
                  <q-icon :name="'mdi-replay'" color="blue" />
                  Reemplazar
                </q-item-section>
              </q-btn>
              <q-item
                v-else
                dense-toggle
                class="q-field-after"
                clickable
                @click="updateFiles(null)"
              >
                <q-item-section align="left">
                  <q-icon size="1.2em" :name="'mdi-close'" color="primary" />
                </q-item-section>
              </q-item>
            </template>
          </q-file>
          <div
            class="column justify-end content-end"
            v-if="edicion && fileCopy && fileCopy != file"
          >
            <q-btn
              @click="
                file = fileCopy;
                cambioArchivo = false;
              "
              color="secondary"
              flat
              dense
              class="q-mr-ms"
            >
              <span class="text-caption text-capitalize">
                Cancelar reemplazo
              </span>
            </q-btn>
          </div>
        </div>
        <div class="row">
          <div class="col">
            <q-item-label class="q-pt-sm q-pb-sm text-grey-6">
              <q-icon
                name="mdi-information"
                size="1.2em"
                color="light-blue"
              />Solo puedes subir archivos menores a 30MB en formato Word.
            </q-item-label>
          </div>
        </div>
        <q-item-label class="text-subtitle1 text-bold pad-left"
          >Vincular promociones</q-item-label
        >
        <q-item-label class="pad-left">
          Selecciona las promociones que serán respondidas por este acuerdo.
        </q-item-label>
        <div class="row wrap">
          <q-select
            dense
            filled
            class="col-6"
            multiple
            use-chips
            use-input
            input-debounce="0"
            v-model="promociones"
            :label="`Promociones ${expediente && !edicion ? '*' : ''}`"
            option-label="numeroRegistro"
            @filter="filtrarContenido"
            option-value="numeroRegistro"
            @update:model-value="cambioForm"
            :options="rowsPromociones"
            :rules="[
              (val) =>
                expediente && !edicion
                  ? Validaciones.validaSelectMultipleRequerido(val)
                  : true,
            ]"
          >
          </q-select>
        </div>
        <q-item-label class="text-subtitle1 text-bold pad-left">
          Partes
        </q-item-label>

        <q-item-label class="pad-left q-pb-md">
          Indica la forma como serán notificadas las partes.
        </q-item-label>
        <div class="row justify-between">
          <q-item v-ripple class="rounded-borders" bordered>
            <q-item-section class="items-center">
              <q-item-label> Notificaciones </q-item-label>
              <q-item-label class="text-subtitle1 text-bold pad-left">
                {{ countTipoNoty(true) }} de {{ totalNotificados() }}
              </q-item-label>
              <q-item-label> por lista </q-item-label>
            </q-item-section>
          </q-item>

          <q-item v-ripple class="rounded-borders">
            <q-item-section class="items-center">
              <q-item-label> Notifiaciones </q-item-label>
              <q-item-label class="text-subtitle1 text-bold pad-left">
                {{ countTipoNoty(false) }} de {{ totalNotificados() }}
              </q-item-label>
              <q-item-label> otro tipo </q-item-label>
            </q-item-section>
          </q-item>

          <q-item v-ripple class="rounded-borders">
            <q-item-section class="items-center">
              <q-item-label> Promoventes </q-item-label>

              <q-item-label class="text-subtitle1 text-bold pad-left">
                {{ notyPromo() }}
              </q-item-label>

              <q-item-label> Asignados </q-item-label>
            </q-item-section>
          </q-item>
        </div>

        <div align="center">
          <q-btn
            icon="mdi-email"
            flat
            label="Asignar tipo de notificación"
            color="primary"
            @click="diagNoty = true"
            :disable="expediente?.estado > 2 && expediente?.estado < 5"
          />
        </div>

        <q-dialog
          v-model="diagNoty"
          persistent
          full-width
          transition-show="flip-down"
          transition-hide="flip-up"
        >
          <q-card>
            <q-bar>
              <a>Asignar Notificaciones</a>
            </q-bar>
            <q-card-section class="q-pt-none">
              <br />
              Indica la forma como serán notificadas y genera automaticamente el
              oficio de las autoridades. Oficio libre de la oportunidad de tener
              un contenido diferente. En caso de notificar a un promovente,
              seleccionalo de la lista
              <br />

              <div v-if="tab !== 'autoridadesyPartes'">
                Por defecto los promoventes serán notificados por lista, si
                requiere otro tipo de notificación seleccione una de las
                siguientes opciones.
              </div>
            </q-card-section>
            <q-tabs
              v-model="tab"
              keep-alive
              dense
              class="text-grey"
              active-color="primary"
              indicator-color="primary"
              align="justify"
              narrow-indicator
            >
              <div class="row full-width">
                <div class="col-3">
                  <q-tab
                    :disable="expediente?.estado > 2 && expediente?.estado < 5"
                    name="autoridadesyPartes"
                    label="Autoridades y Partes"
                  />
                </div>
                <div class="col-2">
                  <q-tab
                    :disable="expediente?.estado > 2 && expediente?.estado < 5"
                    name="promoventes"
                    label="Promoventes"
                  />
                </div>
                <q-input
                  v-if="tab === 'autoridadesyPartes'"
                  filled
                  label="Buscar Autoridades y Partes"
                  class="q-pl-xl col-7"
                  dense
                  debounce="300"
                  color="primary"
                  v-model="filter"
                >
                  <template v-slot:append>
                    <q-icon name="search" />
                  </template>
                </q-input>
                <q-input
                  v-else
                  filled
                  label="Buscar Promoventes"
                  class="q-pl-xl col-7"
                  dense
                  debounce="300"
                  color="primary"
                  v-model="filterPromoventes"
                >
                  <template v-slot:append>
                    <q-icon name="search" />
                  </template>
                </q-input>
              </div>
            </q-tabs>
            {{ monitoredVariable }}
            <q-separator />
            <p></p>
            <!--Primera tabla-->
            <q-tab-panels v-model="tab" animated>
              <q-tab-panel name="autoridadesyPartes" class="q-pa-none">
                <q-table
                  style="max-height: 20em"
                  virtual-scroll
                  :pagination="{ rowsPerPage: 0 }"
                  flat
                  :filter="filter"
                  :rows="rowsTodos"
                  :columns="columns"
                  row-key="index"
                  hide-pagination
                >
                  <template v-slot:body="props">
                    <q-tr>
                      <!--Nombres y Descripciones-->
                      <q-td>
                        <q-item class="text-left">
                          <q-item-section>
                            <q-item-label>
                              {{ props.row.nombre }}
                            </q-item-label>
                            <q-item-label class="text-secondary" caption>
                              {{ props.row.descripcion }}
                              {{ props.row.tipoParte }}
                            </q-item-label>
                          </q-item-section>
                        </q-item>
                      </q-td>

                      <!--Lista-->

                      <q-td>
                        <q-item class="text-center justify-center">
                          <q-radio
                            class="text-center justify-center"
                            name="noty"
                            v-model="props.row.noty"
                            @update:model-value="cambioForm()"
                            val="6"
                          />
                        </q-item>
                      </q-td>

                      <!--Personal-->

                      <q-td>
                        <q-item class="text-center justify-center">
                          <q-radio
                            class="text-center justify-center"
                            name="noty"
                            v-model="props.row.noty"
                            @update:model-value="cambioForm()"
                            val="1"
                          />
                        </q-item>
                      </q-td>

                      <!--Electrónica-->

                      <q-td>
                        <q-item class="text-center justify-center">
                          <q-radio
                            class="text-center justify-center"
                            name="noty"
                            v-model="props.row.noty"
                            @update:model-value="cambioForm()"
                            val="3"
                            :disable="
                              props.row.notiElect == null ? true : false
                            "
                          />
                        </q-item>
                        <div v-if="props.row.notiElect != null">
                          <q-item-label
                            class="text-secondary"
                            caption
                            align="center"
                          >
                            Usuario PSL: {{ props.row.usuarioRegistro }}
                          </q-item-label>
                        </div>
                      </q-td>

                      <!--Edictos-->

                      <q-td>
                        <q-item class="text-center justify-center">
                          <q-radio
                            class="text-center justify-center"
                            name="noty"
                            v-model="props.row.noty"
                            @update:model-value="cambioForm()"
                            val="12"
                          />
                        </q-item>
                      </q-td>

                      <!--Punto Oficio-->

                      <q-td>
                        <q-item class="text-center justify-center">
                          <q-radio
                            class="text-center justify-center"
                            name="noty"
                            v-model="props.row.noty"
                            @update:model-value="cambioForm()"
                            val="5"
                          />
                        </q-item>
                      </q-td>

                      <!--Oficio Libre partes autoridades-->

                      <q-td>
                        <q-item class="text-center justify-center">
                          <q-radio
                            class="text-center justify-center"
                            @click="
                              emit('showOficio', {
                                ...props.row,
                                archivo: file?.blob,
                              })
                            "
                            name="noty"
                            v-model="props.row.noty"
                            @update:model-value="
                              () => {
                                if (validoOficio) {
                                  cambioForm();
                                }
                              }
                            "
                            val="11"
                          />
                        </q-item>
                      </q-td>

                      <!--Acciones mostrar y eliminar oficio libre-->

                      <q-td>
                        <q-item-section v-show="props.row.noty === '11'">
                          <q-item>
                            <q-icon
                              class="q-pr-xs cursor-pointer"
                              size="1.7em"
                              color="secondary"
                              name="mdi-file-document-edit-outline"
                              @click="
                                emit('showOficio', {
                                  ...props.row,
                                  archivo: file?.blob,
                                })
                              "
                            >
                              <q-tooltip>Editar</q-tooltip>
                            </q-icon>
                            <q-icon
                              class="cursor-pointer"
                              size="1.7em"
                              color="red-6"
                              name="mdi-delete"
                              @click="
                                () => {
                                  showBorrarEditarOficio = true;
                                  indexDelete = props.row.index;
                                }
                              "
                            >
                              <q-tooltip>Borrar oficio libre</q-tooltip>
                            </q-icon>
                          </q-item>
                        </q-item-section>
                      </q-td>
                    </q-tr>
                  </template>
                </q-table>
              </q-tab-panel>

              <!--Tabla promoventes-->

              <q-tab-panel name="promoventes" class="q-pa-none">
                <div class="q-pa-xs">
                  <q-table
                    flat
                    bordered
                    :rows="rowsPromoventes"
                    :columns="columnsPromoventes"
                    :filter="filterPromoventes"
                    row-key="autoridadJudicialId"
                    selection="multiple"
                    v-model:selected="selected"
                  >
                    <template v-slot:header="props">
                      <q-tr :props="props">
                        <q-th> </q-th>
                        <q-th v-for="col in props.cols" :key="col.name">
                          {{ col.label }}
                        </q-th>
                      </q-tr>
                    </template>

                    <template v-slot:body="props">
                      <q-tr :props="props">
                        <!--Check Box-->
                        <q-td>
                          <q-checkbox v-model="props.row.selected" />
                        </q-td>
                        <!--Nombre y Descripción-->
                        <q-td>
                          <q-item class="text-left">
                            <q-item-section>
                              <q-item-label
                                v-if="props.row.denominacionDeAutoridad"
                              >
                                {{ props.row.denominacionDeAutoridad }}
                              </q-item-label>
                              <q-item-label v-else>
                                {{
                                  props.row.nombre +
                                    " " +
                                    props.row.aPaterno +
                                    " " +
                                    props.row.aMaterno || ""
                                }}
                              </q-item-label>
                              <q-item-label class="text-secondary" caption>
                                {{ props.row.promoventeTipo }}
                              </q-item-label>
                            </q-item-section>
                          </q-item>
                        </q-td>

                        <!--lista-->
                        <q-td>
                          <q-item class="text-center justify-center">
                            <q-radio
                              class="text-center justify-center"
                              name="noty"
                              v-model="props.row.noty"
                              @update:model-value="cambioForm()"
                              val="6"
                            />
                          </q-item>
                        </q-td>

                        <!--Personal-->
                        <q-td>
                          <q-item class="text-center justify-center">
                            <q-radio
                              class="text-center justify-center"
                              name="noty"
                              v-model="props.row.noty"
                              @update:model-value="cambioForm()"
                              val="1"
                            />
                          </q-item>
                        </q-td>

                        <!--Electrónica-->

                        <q-td>
                          <q-item class="text-center justify-center">
                            <q-radio
                              class="text-center justify-center"
                              name="noty"
                              v-model="props.row.noty"
                              @update:model-value="cambioForm()"
                              val="3"
                            />
                          </q-item>
                        </q-td>

                        <!--Edictos-->

                        <q-td>
                          <q-item class="text-center justify-center">
                            <q-radio
                              class="text-center justify-center"
                              name="noty"
                              v-model="props.row.noty"
                              @update:model-value="cambioForm()"
                              val="12"
                            />
                          </q-item>
                        </q-td>

                        <!--Oficios-->

                        <q-td>
                          <q-item class="text-center justify-center">
                            <q-radio
                              class="text-center justify-center"
                              name="noty"
                              v-model="props.row.noty"
                              @update:model-value="cambioForm()"
                              val="5"
                            />
                          </q-item>
                        </q-td>

                        <!--Oficio Libre promoventes-->

                        <q-td>
                          <q-item class="text-center justify-center">
                            <q-radio
                              class="text-center justify-center"
                              @click="
                                emit('showOficio', {
                                  ...props.row,
                                  archivo: file?.blob,
                                })
                              "
                              name="noty"
                              v-model="props.row.noty"
                              @update:model-value="
                                () => {
                                  if (validoOficio) {
                                    cambioForm();
                                  }
                                }
                              "
                              val="11"
                            />
                          </q-item>
                        </q-td>

                        <!--Acciones mostrar y eliminar oficio libre-->

                        <q-td>
                          <q-item-section v-show="props.row.noty === '11'">
                            <q-item>
                              <q-icon
                                class="q-pr-xs cursor-pointer"
                                size="1.7em"
                                color="secondary"
                                name="mdi-file-document-edit-outline"
                                @click="
                                  emit('showOficio', {
                                    ...props.row,
                                    archivo: file?.blob,
                                  })
                                "
                              >
                                <q-tooltip>Editar</q-tooltip>
                              </q-icon>
                              <q-icon
                                class="cursor-pointer"
                                size="1.7em"
                                color="red-6"
                                name="mdi-delete"
                                @click="
                                  () => {
                                    showBorrarEditarOficio = true;
                                    indexDelete = props.row.index;
                                  }
                                "
                              >
                                <q-tooltip>Borrar oficio libre</q-tooltip>
                              </q-icon>
                            </q-item>
                          </q-item-section>
                        </q-td>
                      </q-tr>
                    </template>
                  </q-table>
                </div>
              </q-tab-panel>
            </q-tab-panels>
            <q-separator />

            <div class="q-pa-md q-gutter-sm">
              <q-btn color="primary" label="Continuar" @click="asignNoty()" />
              <q-btn
                outline
                color="primary"
                label="Cancelar"
                @click="diagNoty = false"
              />
            </div>
          </q-card>
        </q-dialog>

        <q-inner-loading :showing="cargando"> </q-inner-loading>
      </q-card-section>
      <div class="stickyBottom">
        <q-separator></q-separator>
        <q-card-actions align="left">
          <q-btn
            no-caps
            class="q-ml-sm"
            :label="edicion ? 'Guardar' : 'Subir acuerdo'"
            :color="formValido ? 'blue' : 'grey-6'"
            style="min-width: 164px"
            type="submit"
            :disable="!formValido"
          />
          <q-btn
            no-caps
            @click="emit('cerrar')"
            outline
            label="Cancelar"
            :color="'blue'"
            style="min-width: 164px"
          />
          <q-inner-loading :showing="cargandoGuardado">
            <template v-slot> </template>
          </q-inner-loading>
        </q-card-actions>
      </div>
    </q-form>
    <q-inner-loading :showing="cargandoGuardado">
      <template v-slot>
        <q-spinner size="40px" />
        <div v-html="mensajeLoader"></div>
      </template>
    </q-inner-loading>
  </q-card>
  <DialogConfirmacion
    v-model="showBorrarEditarOficio"
    label-btn-cancel="No borrar"
    label-btn-ok="Sí, borrar"
    titulo="¿Deseas borrar el oficio libre?"
    :subTitulo="`Se eliminará el oficio libre para la Autoridad responsable y se regresará al oficio ordinario.`"
    @aceptar="
      () => {
        emit('borrarOficioLibre:event');
        rows.forEach((e) => {
          if (e.index === indexDelete) {
            rows[indexDelete].noty = '1';
            rows[indexDelete].text = '';
          }
        });
        indexDelete = null;
        noty.correcto(`Oficio libre borrado`);
        cambioForm();
      }
    "
    @cancelar="
      () => {
        showBorrarEditarOficio = false;
      }
    "
  >
  </DialogConfirmacion>
</template>

<script setup>
import { date } from "quasar";
import { manejoErrores } from "src/helpers/manejo-errores";
import { Utils } from "src/helpers/utils";
import { Validaciones } from "src/helpers/validaciones";
import { useOficialiaStore } from "src/modules/oficialia/stores/oficialia-store";
import { ref, onMounted, watch, computed } from "vue";
import { useTramiteStore } from "../store/tramite-store";
import { useCatalogosStore } from "src/stores/catalogos-store";
import { useUsuariosStore } from "src/stores/usuarios-store";
import { AutoridadExistente } from "src/data/autoridad-existente";
import { ParteExistente } from "src/data/parte-existente";
import { PromoventeExistente } from "src/data/promovente-existente";
import { noty } from "src/helpers/notify";
import { useAuthStore } from "src/modules/auth/stores/useAuthStore";
import DialogConfirmacion from "src/components/DialogConfirmacion.vue";

const fileCopy = ref(null);
const cargando = ref(false);
const cargandoGuardado = ref(false);
const mensajeLoader = ref("");
const fechaAcuerdo = ref(null);
const expedienteEncontrado = ref(null);
const showBorrarEditarOficio = ref(false);
const indexDelete = ref(null);
const tab = ref("autoridadesyPartes");
const catalogosStore = useCatalogosStore();
const oficialiaStore = useOficialiaStore();
const tramiteStore = useTramiteStore();
const usuariosStore = useUsuariosStore();
const promociones = ref([]);
const cuadernoOptions = ref([]);
const cuaderno = ref(null);
const contenidoOptions = ref([]);
const contenido = ref(null);
const opcionesExpediente = ref([]);
const file = ref(null);
const fechaPromocion = ref("");
const diagNoty = ref(false);
const buscandoCuadernos = ref(false);
const buscnadoContenidos = ref(false);
const neun = ref(null);
const esAmparoEnRevision = ref(false);
const buscandoAmparoEnRevision = ref(false);
const amparoEnRevisionOptions = ref([]);
const clasificacionAmparoEnRevision = ref([]);
const reglasFecha = ref([
  (val) => Validaciones.validaInputRequerido(val),
  (val) => Validaciones.validaFecha(val),
]);
const rowsPromociones = ref([]);
const filter = ref("");
const rowsTodos = ref([]);

const validoOficio = computed(() => {
  return tramiteStore.cambioOficioLibre;
});

const rows = ref(new Array(new AutoridadExistente()).splice(0, 0));
const columns = ref([
  {
    name: "oficios",
    label: "",
    align: "left",
    field: (row) => `${row.nombre} ${row.descripcion}`,
    sortable: true,
  },
  {
    name: "Lista",
    label: "Lista",
    align: "center",
    sortable: false,
  },
  {
    name: "Personal",
    label: "Personal",
    align: "center",
    sortable: false,
  },
  {
    name: "Electrónica",
    label: "Electrónica",
    align: "center",
    sortable: false,
  },
  {
    name: "Edictos",
    label: "Edictos",
    align: "center",
    sortable: false,
  },
  {
    name: "Oficio",
    label: "Oficio",
    align: "center",
    sortable: false,
  },
  {
    name: "Oficio_libre",
    label: "Oficio libre",
    align: "center",
    sortable: false,
  },
]);
const filterPromoventes = ref("");

const rowsPartes = ref(new Array(new ParteExistente()).splice(0, 0));

const rowsPromoventes = ref(new Array(new PromoventeExistente()).splice(0, 0));
const columnsPromoventes = ref([
  {
    name: "oficios",
    label: "Promovente",
    align: "center",
    field: (row) =>
      `${row.nombre} ${row.aPaterno || ""} ${row.aMaterno || ""} ${row.personaId || ""}`,
    sortable: true,
  },
  {
    name: "Lista",
    label: "Lista",
    align: "center",
    sortable: false,
  },
  {
    name: "Personal",
    label: "Personal",
    align: "center",
    sortable: false,
  },
  {
    name: "Electrónica",
    label: "Electrónica",
    align: "center",
    sortable: false,
  },
  {
    name: "Edictos",
    label: "Edictos",
    align: "center",
    sortable: false,
  },
  {
    name: "Oficio",
    label: "Oficio",
    align: "center",
    sortable: false,
  },
  {
    name: "OficioLibre",
    label: "Oficio Libre",
    align: "center",
    sortable: false,
  },
]);

const form = ref(null);
const formValido = ref(false);
const acuerdo = ref(null);
const cambioArchivo = ref(false);
// eslint-disable-next-line no-unused-vars
const props = defineProps({
  expediente: {
    type: Object,
  },
  oficioLibre: {
    type: Object,
  },
  edicion: {
    type: Boolean,
    default: false,
  },
  cambioOficioLibre: {
    type: Boolean,
    default: false,
  },
});

watch(
  () => props.cambioOficioLibre,
  // async () => {
  //   await cambioForm();
  // },
);

watch(
  () => props.oficioLibre?.text,
  // eslint-disable-next-line no-unused-vars
  async (_newValue, _oldValue) => {
    if (
      props.oficioLibre &&
      props.oficioLibre.nIndex >= 0 &&
      rowsTodos.value.find((x) => x.nIndex == props.oficioLibre.nIndex)
    ) {
      rowsTodos.value.find((x) => x.nIndex == props.oficioLibre.nIndex).text =
        _newValue;
    } else if (
      props.oficioLibre &&
      props.oficioLibre.indexPromo >= 0 &&
      rowsPromoventes.value.find(
        (x) => x.indexPromo == props.oficioLibre.indexPromo,
      )
    ) {
      rowsPromoventes.value.find(
        (x) => x.indexPromo == props.oficioLibre.indexPromo,
      ).text = _newValue;
    }
  },
  {
    immediate: true,
  },
);
watch(
  () => promociones.value,
  async (_newValue) => {
    eliminarPromo(_newValue);
  },
  {
    immediate: true,
  },
);
const emit = defineEmits({
  // v-model event with validation
  showOficio: (value) => value !== null,
  cerrar: () => true,
  refrescarTabla: () => true,
});
onMounted(async () => {
  fechaPromocion.value = "";
  buscnadoContenidos.value = true;
  try {
    await catalogosStore.obtenerContenidoTramite();
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  contenidoOptions.value = catalogosStore.contenidosTramite;
  contenidoOptions.value.sort((a, b) =>
    a.descripcion.localeCompare(b.descripcion),
  );
  if (contenidoOptions.value?.length > 0) {
    //contenido.value = contenidoOptions.value[0];
  }
  buscnadoContenidos.value = false;
  if (props.expediente) {
    cargando.value = true;
    mensajeLoader.value = "";
    await obtenCatalogosDependientes(
      props.expediente.expediente.asuntoNeunId,
      props.expediente.expediente.asuntoAlias,
      props.expediente.expediente.catTipoAsuntoId,
    );
    if (props.edicion) {
      await obtenerDetallesAcuerdo(props.expediente);
    } else {
      if (rowsPromociones.value && rowsPromociones.value.length > 0) {
        if (
          rowsPromociones.value.find(
            (x) => x.numeroRegistro === props.expediente.numeroRegistro,
          )
        )
          promociones.value.push(
            rowsPromociones.value.find(
              (x) => x.numeroRegistro === props.expediente.numeroRegistro,
            ),
          );
        rowsPromociones.value.forEach((e) => {
          if (e.numeroRegistro === props.expediente.numeroRegistro) {
            e.disable = true;
          } else {
            e.disable = false;
          }
        });
      }
    }
    juntarAutoPartes(props.expediente.expediente.asuntoAlias);
    cargando.value = false;
  } else {
    catalogosStore.$state.cuadernos = [];
    catalogosStore.$state.contenidos = [];
  }
  await cambioForm();
});

function asignNoty() {
  rowsTodos.value.forEach((element) => {
    if (element.tipo == 0) {
      // si es autoridad
      rows.value[element.index].noty = element.noty;
      rows.value[element.index].text = element?.text;
    }
    if (element.tipo == 1) {
      // si es parte
      rowsPartes.value[element.index].noty = element.noty;
      rowsPartes.value[element.index].text = element?.text;
    }
  });
  diagNoty.value = false;
}

function notyPromo() {
  let notificado = 0;
  rowsPromoventes.value.forEach((element) => {
    if (element.selected == true) {
      notificado++;
    }
  });
  return notificado;
}
async function obtenerDetallesAcuerdo(expediente) {
  try {
    const params = {
      idCatOrganismo: useAuthStore().user?.catOrganismoId,
      idAsuntoNeun: expediente.expediente.asuntoNeunId,
      ordenSintesis: expediente.sintesisOrden,
      asuntoDocumentoId: expediente.asuntoDocumentoId,
    };
    acuerdo.value = await tramiteStore.obtenerAcuerdo(params);

    fechaPromocion.value = date.formatDate(
      acuerdo.value.cabeceraTramite[0]?.fechaAlta,
      "DD/MM/YYYY",
    );
    promociones.value = acuerdo.value.promociones;
    acuerdo.value.promociones.forEach((p) => {
      if (
        !rowsPromociones.value.find((x) => x.numeroRegistro == p.numeroRegistro)
      ) {
        rowsPromociones.value.push(p);
      }
    });
    contenido.value = contenidoOptions.value.find(
      (x) => x.id == acuerdo.value.cabeceraTramite[0].catContenidoId,
    );
    acuerdo.value.partes?.forEach((item) => {
      if (rows.value.find((x) => x.autoridadJudicialId == item.personaId)) {
        rows.value.find((x) => x.autoridadJudicialId == item.personaId).noty =
          item.tipoNotificacion.toString();
        rows.value.find((x) => x.autoridadJudicialId == item.personaId).text =
          item.texto;
      }
    });
    acuerdo.value.partes?.forEach((item) => {
      if (rowsPartes.value.find((x) => x.personaId == item.personaId)) {
        rowsPartes.value.find((x) => x.personaId == item.personaId).noty =
          item.tipoNotificacion.toString();
        rowsPartes.value.find((x) => x.personaId == item.personaId).text =
          item.texto;
      }
    });

    juntarAutoPartes();

    acuerdo.value.partes?.forEach((item) => {
      if (rowsPromoventes.value.find((x) => x.personaId == item.promoventeId)) {
        rowsPromoventes.value.find(
          (x) => x.personaId == item.promoventeId,
        ).noty = item.tipoNotificacion.toString();
        rowsPromoventes.value.find(
          (x) => x.personaId == item.promoventeId,
        ).selected = true;
        rowsPromoventes.value.find(
          (x) => x.personaId == item.promoventeId,
        ).text = item.texto;
      }
    });

    await obtenerArchivoAcuerdo(expediente);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
}

async function obtenerArchivoAcuerdo(expediente) {
  try {
    const parametrosArchivo = {
      anioPromocion: expediente.yearPromocion,
      numeroOrden: expediente.numeroOrden,
      tipoModulo: 2,
      asuntoNeunId: expediente.expediente.asuntoNeunId,
      asuntoDocumentoId: expediente.asuntoDocumentoId,
    };
    await tramiteStore.obtenerArchivoAcuerdo(parametrosArchivo);
    if (tramiteStore.archivoAcuerdo?.anexos[0]?.guidDocumento) {
      await tramiteStore.obtenerAcuerdoEnBase64(
        tramiteStore.archivoAcuerdo.anexos[0].guidDocumento,
      );
    }
    if (tramiteStore.acuerdoBase64) {
      const blobResult = Utils.base64ToBlobWord(tramiteStore.acuerdoBase64);
      file.value = {
        blob: blobResult,
        size: blobResult.size,
        name: expediente.nombreArchivo,
      };
    } else {
      noty.error("No se encontró el archivo");
    }
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
}
async function obtenCatalogosDependientes(
  asuntoNeunId,
  numeroExpediente,
  catTipoAsuntoId,
) {
  buscandoCuadernos.value = true;
  buscnadoContenidos.value = true;
  neun.value = asuntoNeunId;
  try {
    await oficialiaStore.promocionesPorExpediente({
      AsuntoNeunId: asuntoNeunId,
      NoExpediente: numeroExpediente,
    });
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  rowsPromociones.value = oficialiaStore.promocionesXExpediente;

  try {
    await usuariosStore.obtenAutoridad(asuntoNeunId, numeroExpediente, 2, 2);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }

  rows.value = usuariosStore.autoridadXExpediente;
  rows.value = rows.value?.map((x, i) => {
    x.noty = "6";
    x.text = "";
    x.expediente = numeroExpediente;
    x.index = i;
    return x;
  });

  try {
    await usuariosStore.obtenerParteExistente(
      asuntoNeunId,
      numeroExpediente,
      2,
      1,
    );
  } catch (error) {
    manejoErrores.mostrarError(error);
  }

  rowsPartes.value = usuariosStore.parteExistente;
  rowsPartes.value = rowsPartes.value?.map((x, i) => {
    x.noty = "6";
    x.text = "";
    x.index = i;
    return x;
  });

  try {
    await usuariosStore.obtenerPromoventeExistente(asuntoNeunId);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }

  let i = 0;
  rowsPromoventes.value = usuariosStore.promoventeExistente;
  rowsPromoventes.value = rowsPromoventes.value?.map((x) => {
    x.noty = "1";
    x.selected = false;
    x.indexPromo = i++;
    x.text = "";
    x.expediente = numeroExpediente;
    return x;
  });
  if (catTipoAsuntoId == 11) {
    esAmparoEnRevision.value = true;
    buscandoAmparoEnRevision.value = true;
    try {
      await catalogosStore.obtenerAmparoEnRevision(247);
      amparoEnRevisionOptions.value = catalogosStore.amparoEnRevision;
      if (amparoEnRevisionOptions.value.length > 0) {
        buscarAmparoEnRevision(catTipoAsuntoId, asuntoNeunId);
      }
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
    buscandoAmparoEnRevision.value = false;
  } else {
    esAmparoEnRevision.value = false;
    try {
      await catalogosStore.obtenerCuadernos(catTipoAsuntoId);
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
  }

  cuadernoOptions.value = catalogosStore.cuadernos;
  if (cuadernoOptions.value?.length > 0) {
    cuaderno.value = cuadernoOptions.value[0];
    contenido.value = contenidoOptions.value[0];
  }
  buscandoCuadernos.value = false;
  buscnadoContenidos.value = false;
}

function cambioAmparoEnRevision(val){
  buscarAmparoEnRevision(val.id, null);
}
async function buscarAmparoEnRevision(catTipoAsuntoId, asuntoNeunId) {
  buscandoCuadernos.value = true;
  try {
    await catalogosStore.obtenerCuadernos(catTipoAsuntoId, asuntoNeunId);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  buscandoCuadernos.value = false;
  if (catalogosStore.cuadernos.length > 0) {
    catalogosStore.cuadernos.forEach((cuaderno) => {
      const clasificacionCuadernoId = cuaderno.clasificacionCuadernoId;
      clasificacionAmparoEnRevision.value = amparoEnRevisionOptions.value?.find(
        (x) => x.id === clasificacionCuadernoId,
      );
    });
    if(!clasificacionAmparoEnRevision.value){
      clasificacionAmparoEnRevision.value = amparoEnRevisionOptions.value?.find(
        (x) => x.id === catTipoAsuntoId,
      );
    }
    cuaderno.value = catalogosStore.cuadernos[0];
  }
}

async function updateFiles(newFile) {
  file.value = await Utils.fileToBlob(newFile);
  cambioArchivo.value = true;
  await cambioForm();
}
async function cambioForm() {
  formValido.value =
    (await form.value?.validate(false)) &&
    (await fechaAcuerdo.value?.validate(fechaPromocion.value));
}
async function cambioFecha(val) {
  fechaPromocion.value = val;
  await cambioForm();
}

async function buscarExpedientePorNumero(val, update, abort) {
  update(
    async () => {
      if (
        val === "" ||
        val.length <= 5 ||
        typeof Validaciones.validaNoExpediente(val) === "string"
      ) {
        abort();
        return;
      } else {
        try {
          await oficialiaStore.buscarExpediente(val, null, null, 2);
        } catch (error) {
          manejoErrores.mostrarError(error);
        }

        opcionesExpediente.value = oficialiaStore.expediente;
      }
    },
    // "ref" is the Vue reference to the QSelect
    (ref) =>
      setTimeout(() => {
        Utils.marcaPrimeraOpcionCombo(val, ref);
      }, 700),
  );
}
/**
 * cuando cambia expediente
 * @param {} val
 */
async function cambioExpediente(val) {
  cuaderno.value = null;
  contenido.value = null;
  promociones.value = null;
  clasificacionAmparoEnRevision.value = null;

  if (val) {
    await obtenCatalogosDependientes(
      val.asuntoNeunId,
      val.asuntoAlias,
      val.catTipoAsuntoId,
    );
    juntarAutoPartes(val.asuntoAlias);
  }
  cambioForm();
}

function juntarAutoPartes(expediente) {
  let aux2 = [];
  let aux = {};
  rows.value.forEach((item) => {
    aux.nombre = item.nombres.split("-")[0] || "";
    aux.descripcion = item.autoridadJudicialDescripcion;
    aux.noty = item.noty;
    aux.tipo = 0;
    aux.text = item.text;
    aux.index = item.index;
    aux.tipoParte = "(Autoridad)";
    aux.notiElect = null;
    aux.usuarioRegistro = null;
    if (item.notiElect != null) {
      aux.notiElect = item.notiElect;
      aux.usuarioRegistro = item.usuarioRegistro;
    }
    aux2.push({ ...aux });
  });

  rowsPartes.value.forEach((item) => {
    const lastIndex = item.personaTipo.lastIndexOf("-");
    aux.nombre = lastIndex !== -1 ? item.personaTipo.slice(0, lastIndex) : "";
    aux.descripcion = item.descripcionCaracterPersona;
    aux.noty = item.noty;
    aux.tipo = 1;
    aux.text = item.text;
    aux.index = item.index;
    aux.notiElect = null;
    aux.usuarioRegistro = null;
    aux.tipoParte = "(" + item.descripcionTipoPersona + ")";
    if (item.notiElect != null) {
      aux.notiElect = item.notiElect;
      aux.usuarioRegistro = item.usuarioRegistro;
    }
    aux2.push({ ...aux });
  });
  rowsTodos.value = aux2?.map((x, i) => ({
    nombre: x.nombre,
    descripcion: x.descripcion,
    noty: x.noty,
    tipo: x.tipo,
    index: x.index,
    text: x.text,
    nIndex: i,
    tipoParte: x.tipoParte,
    notiElect: x.notiElect,
    usuarioRegistro: x.usuarioRegistro,
    expediente: expediente,
  }));
}

async function guardar() {
  if (
    !(await form.value?.validate(false)) &&
    (await fechaAcuerdo.value?.validate(fechaPromocion.value))
  )
    return;
  let data = new FormData();
  let promocionesAutoridad = [];
  let promocionesParte = [];
  let promocionesPromoventes = [];

  if (rows.value.length > 0) {
    promocionesAutoridad = rows.value.map((e) => ({
      tipoAnexoId: e.noty,
      anexoParteId: e.autoridadJudicialId,
      anexoParteDescripcion: e.autoridadJudicialDescripcion,
      nombreAutoridad: e.nombres,
      textoOficioLibre: e.text,
    }));
  } else {
    promocionesAutoridad = null;
  }

  if (rowsPartes.value.length > 0) {
    promocionesParte = rowsPartes.value.map((e) => ({
      personaId: e.personaId,
      promoventeId: null,
      tipoNotificacionId: e.noty,
      tipoConstanciaId: null,
      descripcionConstancia: null,
      descripcionPromovente: e.descripcionCaracterPersona,
      numIntentosNotificacion: null,
      textoOficioLibre: e?.text,
      nombreParte: e.personaTipo,
    }));
  } else {
    promocionesParte = null;
  }

  if (rowsPromoventes.value.length > 0) {
    let promoAux = [...rowsPromoventes.value];
    let vuelta = 0;
    do {
      for (let i = 0; i < promoAux.length; i++) {
        if (promoAux[i].selected != true) {
          promoAux.splice(i, 1);
          i = 0;
        }
      }
      vuelta++;
    } while (vuelta <= 1);
    promocionesPromoventes = promoAux.map((e) => ({
      personaId: null,
      promoventeId: e.promoventeId,
      tipoNotificacionId: e.noty,
      tipoConstanciaId: null,
      descripcionPromovente: e.promoventeTipo,
      numIntentosNotificacion: null,
      textoOficioLibre: e?.text,
      nombreParte: e.nombre.concat(" ", e.aPaterno, " ", e.aMaterno),
    }));
  } else {
    promocionesPromoventes = null;
  }

  data.append("fechaAcuerdo", fechaPromocion.value);
  data.append(
    "asuntoDocumentoIdOficio",
    props.expediente ? props.expediente.asuntoDocumentoId : null,
  );
  data.append(
    "catTipoAsunto",
    props.expediente
      ? props.expediente?.expediente?.catTipoAsunto
      : expedienteEncontrado.value.tipoAsunto,
  );
  data.append(
    "asuntoAlias",
    props.expediente
      ? props.expediente?.expediente?.asuntoAlias
      : expedienteEncontrado.value.asuntoAlias,
  );
  data.append(
    "mesa",
    props.expediente ? props.expediente.mesa : expedienteEncontrado.value.mesa,
  );

  let promocionesAEnviaR = [];
  if (props.edicion) {
    data.append("sintesisOrden", props.expediente.sintesisOrden);
    data.append("asuntoDocumentoId", props.expediente.asuntoDocumentoId);
    if (promociones.value) {
      promociones.value.forEach((x) => {
        if (
          !acuerdo.value.promociones?.some(
            (p) => p.numeroRegistro == x.numeroRegistro,
          )
        ) {
          //indica 0 para cuando se quiere vincular una promoción
          promocionesAEnviaR.push({ ...x, proceso: 0 });
        }
      });
    }
    acuerdo.value.promociones?.forEach((x) => {
      if (
        !promociones.value?.some((p) => p.numeroRegistro == x.numeroRegistro)
      ) {
        if (!promociones.value) {
          promociones.value = [];
        }
        //1 cuando se quiere desvincular
        promocionesAEnviaR.push({ ...x, proceso: 1 });
      }
    });
    if (cambioArchivo.value) {
      data.append(
        file.value.name,
        Utils.blobToFile(file.value.blob, file.value.name),
        file.value.name,
      );
    }
  } else {
    if (promociones.value) {
      promocionesAEnviaR = promociones.value.map((x) => {
        //indica 0 para cuando se quiere vincular una promoción
        x.proceso = 0;
        return x;
      });
    }
    data.append(
      file.value.name,
      Utils.blobToFile(file.value.blob, file.value.name),
      file.value.name,
    );
  }

  data.append(
    "promociones",
    promocionesAEnviaR.length > 0 ? JSON.stringify(promocionesAEnviaR) : null,
  );
  data.append("contenido", contenido.value.id);
  if (props.expediente) {
    data.append("asuntoNeunId", props.expediente.expediente.asuntoNeunId);
    data.append("tipoCuaderno", props.expediente.tipoCuadernoId);
  } else {
    data.append("asuntoNeunId", expedienteEncontrado.value.asuntoNeunId);
    data.append("tipoCuaderno", cuaderno.value.cuadernoId);
  }
  data.append(
    "promocionesAutoridad",
    promocionesAutoridad ? JSON.stringify(promocionesAutoridad) : null,
  );
  data.append(
    "promocionesParte",
    promocionesParte ? JSON.stringify(promocionesParte) : null,
  );
  data.append(
    "promocionesPromoventes",
    promocionesPromoventes ? JSON.stringify(promocionesPromoventes) : null,
  );

  data.append(
    "idAgenda",
    idAgenda?.value?.idAgenda == undefined &&
      tramiteStore.datosAudiencia?.audiencias?.find(
        (x) =>
          x.tipoAudienciaId == tipoAudiencia?.value?.id && !x.tieneResultado,
      )?.idAgenda == undefined
      ? null
      : idAgenda?.value?.idAgenda
        ? idAgenda?.value?.idAgenda
        : tramiteStore.datosAudiencia?.audiencias?.find(
            (x) =>
              x.tipoAudienciaId == tipoAudiencia?.value?.id &&
              !x.tieneResultado,
          )?.idAgenda,
  );
  data.append(
    "idResultado",
    resultadoAudiencia?.value?.idResultado == undefined
      ? null
      : resultadoAudiencia?.value?.idResultado,
  );

  if (props.edicion) {
    await editarAcuerdo(data);
  } else {
    await guardarAcuerdo(data);
  }
}
async function editarAcuerdo(data) {
  const numeroExpediente =
    props.expediente && data
      ? props.expediente?.expediente?.asuntoAlias
      : expedienteEncontrado.value.asuntoAlias;
  const tipoAsunto =
    props.expediente && data
      ? props.expediente?.expediente?.catTipoAsunto
      : expedienteEncontrado.value.tipoAsunto;
  const procedimiento = props.expediente
    ? props.expediente?.expediente?.tipoProcedimiento
    : expedienteEncontrado.value.tipoProcedimiento;
  cargandoGuardado.value = true;
  mensajeLoader.value = `<b>Actualizando acuerdo y generando oficios</b><br/>Vinculando al expediente ${numeroExpediente} ${tipoAsunto} ${procedimiento}`;
  try {
    await tramiteStore.editarAcuerdo(data);
    noty.correcto(
      `Se ha editado el acuerdo del expediente ${numeroExpediente} ${tipoAsunto} ${procedimiento}`,
    );
    emit("refrescar-tabla");
    emit("cerrar", false);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  cargandoGuardado.value = false;
}
async function guardarAcuerdo(data) {
  const numeroExpediente =
    props.expediente && data
      ? props.expediente?.expediente?.asuntoAlias
      : expedienteEncontrado.value.asuntoAlias;
  const tipoAsunto =
    props.expediente && data
      ? props.expediente?.expediente?.catTipoAsunto
      : expedienteEncontrado.value.tipoAsunto;
  const procedimiento = props.expediente
    ? props.expediente?.expediente?.tipoProcedimiento
    : expedienteEncontrado.value.tipoProcedimiento;
  cargandoGuardado.value = true;
  mensajeLoader.value = `<b>Subiendo acuerdo y generando oficios</b><br/>Vinculando al expediente ${numeroExpediente} ${tipoAsunto} ${procedimiento}`;

  try {
    await tramiteStore.subirAcuerdos(data);
    noty.correcto(
      `Se ha vinculado el acuerdo al expediente ${numeroExpediente} ${tipoAsunto} ${procedimiento}`,
    );
    emit("refrescar-tabla");
    emit("cerrar", false);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  cargandoGuardado.value = false;
}
/**
 * filtra contenido en combo
 * @param {*} val valor a buscar
 */
function filtrarContenido(val, update) {
  update(
    async () => {
      contenidoOptions.value = Utils.filtrarCombo(
        val,
        catalogosStore.contenidosTramite,
        "descripcion",
      );
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref),
  );
}
/**
 * filtra cuaderno en combo
 * @param {*} val valor a buscar
 */
function filtrarCuaderno(val, update) {
  update(
    async () => {
      cuadernoOptions.value = Utils.filtrarCombo(
        val,
        catalogosStore.cuadernos,
        "cuaderno",
      );
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref),
  );
}

/**
 * filtra amparo en revisión en combo
 * @param {*} val valor a buscar
 */
function filtrarAmparoEnRevision(val, update) {
  update(
    async () => {
      amparoEnRevisionOptions.value = Utils.filtrarCombo(
        val,
        catalogosStore.amparoEnRevision,
        "descripcion",
      );
    },
    (ref) => Utils.marcaPrimeraOpcionCombo(val, ref),
  );
}

function totalNotificados() {
  let cont = 0;
  cont += rows.value.length;
  cont += rowsPartes.value.length;
  rowsPromoventes.value.forEach((element) => {
    if (element.selected) {
      cont++;
    }
  });
  return cont;
}

function countTipoNoty(value) {
  let cont = 0;
  rows.value.forEach((x) => {
    if (value && x.noty == "6") {
      cont++;
    } else if (!value && x.noty != "6") {
      cont++;
    }
  });

  rowsPartes.value.forEach((x) => {
    if (value && x.noty == "6") {
      cont++;
    } else if (!value && x.noty != "6") {
      cont++;
    }
  });

  rowsPromoventes.value.forEach((x) => {
    if (x.selected) {
      if (value && x.noty == "6") {
        cont++;
      } else if (!value && x.noty != "6") {
        cont++;
      }
    }
  });
  return cont;
}

function eliminarPromo(value) {
  if (
    !props.edicion &&
    props.expediente &&
    props.expediente.numeroRegistro &&
    value.length == 0 &&
    rowsPromociones.value.find(
      (x) => x.numeroRegistro == props.expediente.numeroRegistro,
    )
  ) {
    promociones.value = [
      rowsPromociones.value.find(
        (x) => x.numeroRegistro == props.expediente.numeroRegistro,
      ),
    ];
  }
}

const audienciaId = 5723;
async function cambiaContenido(value) {
  var expediente = expedienteEncontrado?.value?.asuntoNeunId;
  if (
    expediente != undefined &&
    expediente != null &&
    value.id == audienciaId
  ) {
    buscnadoTipoAudiencia.value = true;
    try {
      await tramiteStore.obtenerDatosAudiencia(expediente);
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
    tipoAudienciaOptions.value = tramiteStore.datosAudiencia.tiposAudiencias;

    buscnadoTipoAudiencia.value = false;
  }
  cambioForm();
}
function cambiaTipoAudiencia(value) {
  resultadoAudienciaOptions.value =
    tramiteStore.datosAudiencia?.resultados?.filter(
      (x) => x.tipoAudienciaId == value.id,
    );
  audienciasOptions.value = tramiteStore.datosAudiencia?.audiencias?.filter(
    (x) => x.tipoAudienciaId == value.id && x.tieneResultado,
  );
}

const muestraAudiencia = computed(() => {
  //var expediente = expedienteEncontrado?.value?.asuntoNeunId;
  obtieneOpcionesAudiencia(neun.value);
  //if(expediente != undefined && expediente != null && contenido?.value?.id == audienciaId){
  if (neun.value != null && contenido?.value?.id == audienciaId) {
    return true;
  } else return false;
});
const muestraResultadoAudiencia = computed(() => {
  if (tramiteStore.datosAudiencia) {
    var audiencias = tramiteStore.datosAudiencia?.audiencias;
    if (
      audiencias?.length > 0 &&
      audiencias?.find(
        (x) =>
          x.tipoAudienciaId == tipoAudiencia?.value?.id && !x.tieneResultado,
      )
    ) {
      //      var idAgendaRes = audiencias?.find((x)=> x.tipoAudienciaId == tipoAudiencia?.value?.id && !x.tieneResultado).idAgenda;
      //idAgenda?.value?.idAgenda = audiencias?.find((x)=> x.tipoAudienciaId == tipoAudiencia?.value?.id && !x.tieneResultado)?.idAgenda;
      return true;
    } else return false;
  } else return false;
});

const muestraAudiencias = computed(() => {
  if (tramiteStore.datosAudiencia) {
    var audiencias = tramiteStore.datosAudiencia?.audiencias;
    if (
      audiencias?.length > 0 &&
      audiencias?.find(
        (x) =>
          x.tipoAudienciaId == tipoAudiencia?.value?.id && x.tieneResultado,
      ) &&
      !audiencias?.find(
        (x) =>
          x.tipoAudienciaId == tipoAudiencia?.value?.id && !x.tieneResultado,
      )
    ) {
      return true;
    } else return false;
  } else return false;
});

async function obtieneOpcionesAudiencia(neun) {
  try {
    await tramiteStore.obtenerDatosAudiencia(neun);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  tipoAudienciaOptions.value = tramiteStore.datosAudiencia.tiposAudiencias;
}

function extraerExtension(doc){
  const extension = doc.name.substring(doc.name.lastIndexOf('.')+1);
  return extension;
}

const buscnadoTipoAudiencia = ref(false);
const tipoAudiencia = ref(null);
const tipoAudienciaOptions = ref([]);

const resultadoAudiencia = ref(null);
const resultadoAudienciaOptions = ref([]);
const buscnadoResultadoAudiencia = ref(false);

const idAgenda = ref(null);
//const idAgendaResultado = ref(null);
const audienciasOptions = ref([]);
const buscnadoAudiencia = ref(false);
</script>
<style scoped>
:deep(.q-field__after) {
  padding-left: unset;
}

.simple-view {
  height: 100vh;
}
</style>
