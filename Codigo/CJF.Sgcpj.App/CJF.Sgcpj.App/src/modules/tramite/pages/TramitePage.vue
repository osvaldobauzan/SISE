<template>
  <q-page class="q-pa-sm">
    <q-toolbar>
      <q-toolbar-title class="text-bold text-h4 text-primary">
        Trámite
      </q-toolbar-title>
      <q-btn
        dense
        no-caps
        unelevated
        color="primary"
        label="Preautorizar Acuerdos Seleccionados"
        icon="mdi-playlist-check"
        @click="showPreaAcuerdos = true"
        v-if="
          authStore.user.privilegios?.some((x) => x == 14) ||
          authStore.user.privilegios?.some((x) => x == 18)
        "
        class="q-px-lg"
      />
      <q-btn
        dense
        no-caps
        unelevated
        color="primary"
        label="Autorizar Acuerdos Seleccionados"
        icon="mdi-playlist-check"
        @click="showPreaAcuerdos = true"
        v-else-if="
          authStore.user.privilegios?.some((x) => x == 15) ||
          authStore.user.privilegios?.some((x) => x == 19)
        "
        class="q-px-lg"
      />

      <q-btn
        dense
        v-permitido="16"
        outline
        no-caps
        label="Acuerdo de estado de autos"
        icon="mdi-upload"
        @click="
          showSubirAcuerdo = true;
          esEditar = false;
          selectedItem = null;
        "
        class="q-ml-sm q-px-lg"
      >
        <q-tooltip>Subir estado de autos</q-tooltip>
      </q-btn>
    </q-toolbar>
    <q-toolbar>
      <SelectDateComponent
        ref="selectDateComponent"
        title="Filtrar por fecha"
        @update:selectedDate="setSelectedDate"
      >
      </SelectDateComponent>
      <q-space></q-space>
      <SelectStatusComponent
        :filter="filter.status"
        :listStatus="coloresList"
        @update:filterStatus="setFilterStatus"
      >
      </SelectStatusComponent>
      <q-space></q-space>
      <InputSearchTable v-model="textoBuscar" @onSearch="buscaEnBD()" />
    </q-toolbar>
    <FiltrosColumnas
      :model-value="valoresFiltros"
      @cambio-filtro="cambioFiltro"
    />
    <div class="row q-mt-sm">
      <div class="col">
        <q-table
          flat
          bordered
          dense
          wrap-cells
          binary-state-sort
          class="my-sticky-header-table q-mx-md"
          :rows="rows"
          :columns="columns"
          :filter="filter"
          row-key="index"
          v-model:pagination="pagination"
          loading-label="Cargando..."
          :rows-per-page-options="rowsPerPageOptions"
          @request="onRequest"
          rows-per-page-label="Registros por página:"
          :loading="cargando"
        >
          <template #no-data>
            <TablaSinDatos
              :titulo="
                textoBuscar ||
                filter.status !== 0 ||
                (selectedDate.from == selectedDate.to &&
                  selectedDate.from !=
                    date.formatDate(new Date(), 'DD/MM/YYYY')) ||
                selectedDate.from != selectedDate.to
                  ? 'Sin resultados'
                  : 'Sin trámite'
              "
              :subTitulo="
                textoBuscar ||
                filter.status !== 0 ||
                (selectedDate.from == selectedDate.to &&
                  selectedDate.from !=
                    date.formatDate(new Date(), 'DD/MM/YYYY')) ||
                selectedDate.from != selectedDate.to
                  ? 'Intenta seleccionar otros criterios para realizar tu filtrado.'
                  : 'No hay documentos.'
              "
              :icono="
                textoBuscar || filter.status !== 0 ? 'mdi-filter' : 'mdi-file'
              "
            ></TablaSinDatos>
          </template>
          <template v-slot:body="props">
            <q-tr :class="getColor(props.row.estado)">
              <q-td
                :style="`width: 200px; border-left: 10px solid ${getBookColorHex(
                  props.row.expediente.catTipoAsunto,
                  props.row.expediente.nombreCorto,
                )}`"
              >
                <q-item
                  v-ripple
                  clickable
                  class="q-pa-none"
                  @click="
                    selectedItem = props.row.expediente;
                    maximizedToggle = false;
                    expedientes.push(props.row.expediente);
                    showExpediente = true;
                  "
                >
                  <q-item-section side>
                    <q-icon
                      size="md"
                      :color="`${getBookColor(
                        props.row.expediente.catTipoAsunto,
                        props.row.expediente.nombreCorto,
                      )}`"
                    >
                      <svg
                        width="24px"
                        height="24px"
                        viewBox="0 0 24 24"
                        fill="none"
                        xmlns="http://www.w3.org/2000/svg"
                      >
                        <path
                          fill-rule="evenodd"
                          clip-rule="evenodd"
                          d="M5.99976 2C4.89519 2 3.99976 2.89543 3.99976 4V20C3.99976 21.1046 4.89519 22 5.99976 22H17.9998C19.1043 22 19.9998 21.1046 19.9998 20V4C19.9998 2.89543 19.1043 2 17.9998 2H5.99976ZM12 10.0001C13.6569 10.0001 15 8.65698 15 7.00012C15 5.34327 13.6569 4.00012 12 4.00012C10.3431 4.00012 9 5.34327 9 7.00012C9 8.65698 10.3431 10.0001 12 10.0001ZM18 12.0001V14.0001H6V12.0001H18ZM13 15.0001H6V17.0001H13V15.0001Z"
                          :fill="
                            getBookColorHex(
                              props.row.expediente.catTipoAsunto,
                              props.row.expediente.nombreCorto,
                            )
                          "
                        />
                      </svg>
                    </q-icon>
                  </q-item-section>
                  <q-item-section>
                    <q-item-label
                      class="text-bold text-secondary"
                      style="text-decoration: underline"
                    >
                      {{ props.row.expediente.asuntoAlias }}
                    </q-item-label>
                    <q-item-label
                      v-if="
                        props.row.expediente.catTipoAsunto !==
                        props.row.expediente.nombreCorto
                      "
                    >
                      {{ props.row.expediente.catTipoAsunto }}
                    </q-item-label>
                    <q-item-label v-if="props.row.expediente.nombreCorto">
                      <q-badge
                        :class="`bg-${getBookColor(
                          props.row.expediente.catTipoAsunto,
                          props.row.expediente.nombreCorto,
                        )} text-${getBookColor(
                          props.row.expediente.catTipoAsunto,
                          props.row.expediente.nombreCorto,
                        )}`"
                        :label="props.row.expediente.nombreCorto"
                      >
                      </q-badge>
                    </q-item-label>
                    <q-item-label
                      v-if="props.row.expediente.tipoProcedimiento"
                      caption>
                      {{ props.row.expediente.tipoProcedimiento }}
                    </q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td>
                <q-item class="text-center">
                  <q-item-section>
                    <q-item-label>
                      {{ props.row.userNameSecretario || "" }}
                    </q-item-label>
                    <q-item-label class="text-secondary" caption>
                      {{ props.row.mesa }}
                    </q-item-label>
                  </q-item-section>
                  <q-tooltip v-if="props.row.userNameSecretario">
                    {{ props.row.secretarioDescripcion }}
                  </q-tooltip>
                </q-item>
              </q-td>
              <q-td
                class="text-center"
                style="border-left: rgb(190, 190, 190) solid 1px"
              >
                <div
                  v-if="
                    props.row.archivoPromocion != null &&
                    props.row.nombreOrigen != 'SIN ORIGEN'
                  "
                >
                  <q-btn
                    v-permitido="23"
                    flat
                    stack
                    color="blue"
                    icon="mdi-paperclip"
                    @click="
                      selectedItem = props.row;
                      setDocumento('promocion', { ...props.row });
                    "
                    :label="
                      props.row.numeroRegistro === 0
                        ? ''
                        : props.row.numeroRegistro
                    "
                  >
                    <q-item-label caption class="text-blue">
                      {{ props.row.nombreOrigen }}
                    </q-item-label>
                    <q-tooltip> Ver promoción </q-tooltip>
                  </q-btn>
                </div>
                <div
                  v-else-if="
                    props.row.NumeroRegistro != 0 &&
                    props.row.nombreOrigen != 'SIN ORIGEN'
                  "
                >
                  <q-item-label caption class="text-blue">
                    SIN ARCHIVO
                    <q-item-label
                      caption
                      class="text-blue text-weight-bold"
                      style="font-size: 14px"
                    >
                      {{ props.row.numeroRegistro }}
                    </q-item-label>
                    <q-item-label caption class="text-blue">
                      {{ props.row.nombreOrigen }}
                    </q-item-label>
                  </q-item-label>
                </div>
                <div v-else>
                  <q-item-label caption class="text-blue">
                    Sin Promoción
                  </q-item-label>
                </div>
              </q-td>
              <q-td class="text-center">
                <template v-if="props.row.numeroRegistro > 0">
                  {{ date.formatDate(props.row.fechaRecibido, "DD/MM/YYYY")
                  }}<br />
                  <q-item-label class="text-secondary">
                    {{ date.formatDate(props.row.fechaRecibido, "HH:mm") }}
                  </q-item-label>
                </template>
              </q-td>
              <q-td>
                <q-item-label
                  v-if="props.row.nombreOrigen === 'SIN ORIGEN'"
                  caption
                  class="text-blue"
                >
                  Estado de autos
                </q-item-label>
                <q-item-label v-else>{{
                  props.row.promovente
                }}</q-item-label></q-td
              >
              <q-td
                style="border-left: rgb(190, 190, 190) solid 1px"
                class="text-center"
              >
                <q-btn
                  v-if="props.row.estadoAutorizacion > 0"
                  v-permitido="24"
                  flat
                  stack
                  color="blue"
                  icon="mdi-paperclip"
                  :label="props.row.fechaAuto_F"
                  @click="
                    selectedItem = props.row;
                    selectedItem.firmarOficios = false;
                    setDocumento('acuerdo');
                  "
                >
                  <q-tooltip>Ver acuerdo</q-tooltip>
                </q-btn>
                <q-btn
                  v-else
                  v-permitido="17"
                  flat
                  stack
                  no-caps
                  color="negative"
                  icon="mdi-upload"
                  label="Sin archivo"
                  @click="
                    showSubirAcuerdo = true;
                    esEditar = false;
                    selectedItem = props.row;
                  "
                >
                  <q-tooltip>Subir acuerdo</q-tooltip>
                </q-btn>

                <br />
                <q-item-label class="text-secondary">{{
                  props.row.nombreDocumento
                }}</q-item-label>
              </q-td>
              <q-td class="text-center">
                <div
                  v-if="
                    props.row.estado == 3 &&
                    (authStore.user.privilegios?.some((x) => x == 15) ||
                      authStore.user.privilegios?.some((x) => x == 19))
                  "
                >
                  <q-checkbox v-model="props.row.selected" />
                </div>
                <div
                  v-else-if="
                    props.row.estado == 2 &&
                    (authStore.user.privilegios?.some((x) => x == 14) ||
                      authStore.user.privilegios?.some((x) => x == 18))
                  "
                >
                  <q-checkbox v-model="props.row.selected" />
                </div>
              </q-td>
              <q-td class="text-center">{{ props.row.contenido }}</q-td>
              <q-td class="text-center"
                >{{ props.row.userNameOficial || "" }}<br />
                <q-item-label class="text-secondary">{{
                  date.formatDate(props.row.fechaAuto, "DD/MM/YYYY")
                }}</q-item-label>
                <q-item-label class="text-secondary">{{
                  date.formatDate(props.row.fechaAuto, "HH:mm")
                }}</q-item-label>
              </q-td>
              <q-td>
                <div class="text-center">
                  {{ props.row.empleadoPreAutoriza }}<br />
                  <q-item-label class="text-secondary">{{
                    date.formatDate(props.row.fechaPreAutoriza, "DD/MM/YYYY")
                  }}</q-item-label>
                  <q-item-label class="text-secondary">{{
                    date.formatDate(props.row.fechaPreAutoriza, "HH:mm")
                  }}</q-item-label>
                </div>
              </q-td>
              <q-td>
                <div class="text-center">
                  {{ props.row.empleadoAutoriza }}<br />
                  <q-item-label class="text-secondary">{{
                    date.formatDate(props.row.fechaAutoriza, "DD/MM/YYYY")
                  }}</q-item-label>
                  <q-item-label class="text-secondary">{{
                    date.formatDate(props.row.fechaAutoriza, "HH:mm")
                  }}</q-item-label>
                </div>
              </q-td>
              <q-td>
                <div class="text-center">
                  {{ props.row.empleadoCancela }}<br />
                  <q-item-label class="text-secondary">{{
                    date.formatDate(props.row.fechaCancela, "DD/MM/YYYY")
                  }}</q-item-label>
                  <q-item-label class="text-secondary">{{
                    date.formatDate(props.row.fechaCancela, "HH:mm")
                  }}</q-item-label>
                  <q-item-label v-if="props.row.empleadoCancela">{{
                    props.row.canceloCuenta
                  }}</q-item-label>
                </div>
              </q-td>
              <q-td>
                <div class="row">
                  <div class="col">
                    <q-btn
                      v-permitido="[20, 21, 22]"
                      v-if="
                        (authStore.user.privilegios?.some((x) => x == 20) &&
                          (props.row.estado == 3 || props.row.estado == 4)) ||
                        (authStore.user.privilegios?.some((x) =>
                          [21, 22].some((y) => y === x),
                        ) &&
                          (props.row.estado == 2 || props.row.estado == 5))
                      "
                      flat
                      round
                      color="blue"
                      icon="mdi-dots-vertical"
                    >
                      <q-menu auto-close>
                        <q-list style="min-width: 100px">
                          <q-item
                            v-if="
                              (props.row.estado == 3 ||
                                props.row.estado == 4) &&
                              authStore.user.privilegios?.some((x) => x == 15)
                            "
                            v-permitido="20"
                            clickable
                            @click="
                              selectedItem = props.row;
                              showCancelarAcuerdo = true;
                            "
                          >
                            <q-item-section side
                              ><q-icon
                                name="mdi-close"
                                color="negative"
                              ></q-icon
                            ></q-item-section>
                            <q-item-section>
                              <q-item-label class="text-negative"
                                >Cancelar</q-item-label
                              >
                            </q-item-section>
                          </q-item>
                          <q-item
                            v-else-if="props.row.estado == 3"
                            v-permitido="20"
                            clickable
                            @click="
                              selectedItem = props.row;
                              showCancelarAcuerdo = true;
                            "
                          >
                            <q-item-section side
                              ><q-icon
                                name="mdi-close"
                                color="negative"
                              ></q-icon
                            ></q-item-section>
                            <q-item-section>
                              <q-item-label class="text-negative"
                                >Cancelar Preautorizado</q-item-label
                              >
                            </q-item-section>
                          </q-item>
                          <q-item
                            v-if="
                              props.row.estado == 2 || props.row.estado == 5
                            "
                            v-permitido="21"
                            clickable
                            @click="
                              showSubirAcuerdo = true;
                              esEditar = true;
                              selectedItem = props.row;
                              titlePromocion = 'Editar Acuerdo';
                            "
                          >
                            <q-item-section side
                              ><q-icon
                                name="mdi-file-edit-outline"
                                color="blue"
                              ></q-icon
                            ></q-item-section>
                            <q-item-section>
                              <q-item-label class="text-blue" lines="1"
                                >Editar Acuerdo</q-item-label
                              >
                            </q-item-section>
                          </q-item>
                          <q-item
                            v-if="props.row.estadoAutorizacion > 0"
                            v-permitido="21"
                            clickable
                            @click="descargarAcuerdo(props.row)"
                          >
                            <q-item-section side
                              ><q-icon name="mdi-download" color="blue"></q-icon
                            ></q-item-section>
                            <q-item-section>
                              <q-item-label class="text-blue" lines="1"
                                >Descargar Acuerdo</q-item-label
                              >
                            </q-item-section>
                          </q-item>
                          <q-item
                            v-if="
                              props.row.estado == 2 || props.row.estado == 5
                            "
                            v-permitido="22"
                            clickable
                            @click="
                              selectedItem = props.row;
                              showDialogEliminar = true;
                            "
                          >
                            <q-item-section side
                              ><q-icon
                                name="mdi-delete-outline"
                                color="negative"
                              ></q-icon
                            ></q-item-section>
                            <q-item-section>
                              <q-item-label class="text-negative"
                                >Eliminar</q-item-label
                              >
                            </q-item-section>
                          </q-item>
                        </q-list>
                      </q-menu>
                    </q-btn>
                  </div>
                </div>
              </q-td>
            </q-tr>
          </template>
          <template v-slot:loading>
            <q-inner-loading :showing="cargando" />
          </template>
        </q-table>
      </div>
      <div class="col-1" v-if="expedientes.length > 0">
        <q-list class="q-gutter-y-xs">
          <q-card
            flat
            bordered
            v-for="(expediente, index) in expedientes"
            :key="expediente.asuntoAlias"
          >
            <q-card-section class="q-pa-none">
              <q-btn
                flat
                dense
                round
                size="sm"
                color="negative"
                icon="mdi-close"
                style="
                  position: absolute;
                  top: 0px;
                  right: 0px;
                  margin: 2px;
                  z-index: 1;
                "
                @click="expedientes.splice(index, 1)"
              ></q-btn>

              <q-item
                clickable
                v-ripple
                @click="
                  selectedItem = expediente;
                  showExpediente = true;
                  maximizedToggle = false;
                "
              >
                <q-item-section>
                  <q-item-label
                    class="text-bold text-secondary"
                    style="text-decoration: underline"
                  >
                    {{ expediente.asuntoAlias }}
                  </q-item-label>

                  <q-item-label caption>
                    {{ expediente.catTipoAsunto }}
                  </q-item-label>
                </q-item-section>
              </q-item>
            </q-card-section>
          </q-card>
        </q-list>
      </div>
    </div>
    <q-dialog v-model="showExpediente" :maximized="maximizedToggle">
      <ModalWindowComponent
        :selected-item="selectedItem"
        :maximizedToggle="maximizedToggle"
        @toggle-maximized="maximizedToggle = !maximizedToggle"
      >
        <ExpedientePage
          :asuntoNeunId="selectedItem.asuntoNeunId"
          :asuntoAlias="selectedItem.asuntoAlias"
          :tipoAsunto="selectedItem.catTipoAsunto"
          :cuadernoDesc="selectedItem.nombreCorto"
        />
      </ModalWindowComponent>
    </q-dialog>
    <q-dialog v-model="showDialogPdf" full-height full-width>
      <VerAcuerdo :model-value="selectedItem">
        <template v-slot:default>
          <div class="q-gutter-lg q-mr-xl">
            <div class="row q-gutter-x-md items-center">
              <q-item-label
                v-if="
                  selectedItem.estado == 3 && !!!selectedItem.oficiosFirmados
                "
              >
                Los oficios también serán firmados
              </q-item-label>
              <q-checkbox
                v-permitido="18"
                v-model="selectedItem.firmarOficios"
                v-if="selectedItem.estado == 2"
                >¿Firmar oficios?</q-checkbox
              >
              <q-btn
                v-permitido="18"
                no-caps
                @click="firmadorInicio(1)"
                unelevated
                icon="mdi-check"
                color="info"
                label="Preautorizar"
                v-if="selectedItem.estado == 2"
              ></q-btn>
              <q-btn
                v-permitido="19"
                @click="firmadorInicio(2)"
                unelevated
                icon="mdi-check"
                color="positive"
                label="Autorizar"
                v-if="selectedItem.estado == 3"
              ></q-btn>
              <q-btn
                v-permitido="20"
                @click="showCancelarAcuerdo = true"
                unelevated
                icon="mdi-close"
                color="negative"
                label="Cancelar"
                v-if="selectedItem.estado == 3 || selectedItem.estado == 4"
              ></q-btn>
            </div>
          </div>
        </template>
        <template v-slot:loading>
          <q-inner-loading :showing="cargandoEstadoAcuerdo" />
        </template>
      </VerAcuerdo>
    </q-dialog>
    <q-dialog style="min-width: 50vw" v-model="showSubirAcuerdo" persistent>
      <SubirAcuerdo
        @refrescarTabla="setRows"
        @cerrar="
          (val) => {
            val ? (showCancelarSubirAcuerdo = val) : (showSubirAcuerdo = val);
            cambioOficioLibre = false;
          }
        "
        :expediente="selectedItem"
        :oficio-libre="oficioLibre"
        :edicion="esEditar"
        :cambio-oficio-libre="cambioOficioLibre"
        @borrarOficioLibre:event="
          () => {
            oficioLibre.text = '';
          }
        "
        @show-oficio="
          (val) => {
            showOficioLibre = true;
            oficioLibre = val;
          }
        "
      ></SubirAcuerdo>
    </q-dialog>
    <q-dialog v-model="showPromocion" full-height full-width>
      <DetallePromocion
        :model-value="selectedItem"
        :es-tramite="true"
      ></DetallePromocion>
    </q-dialog>
    <q-dialog v-model="showTableroAutorizar" persistent>
      <TableroAutorizar
        @cerrar="(showTableroAutorizar = false), removeFiltrosLocalStorage()"
        :selected-date="selectedDate"
        :estado="filter.status"
        @seleccionados="
          (val) => {
            seleccionAutorizar = val;
          }
        "
      ></TableroAutorizar>
    </q-dialog>
    <q-dialog v-model="showOficioLibre" persistent>
      <OficioLibre
        :model-value="oficioLibre"
        :edicion="esEditar"
        :cambio-oficio-libre="cambioOficioLibre"
        @cerrarEditar="
          (val) => {
            if (val.value) {
              oficioLibre.text = val.text;
              showOficioLibre = false;
            } else {
              showOficioLibre = false;
            }
          }
        "
        @update:modelValue="
          (val) => {
            oficioLibre = val;
            showOficioLibre = false;
            cambioOficioLibre = true;
          }
        "
      >
      </OficioLibre>
    </q-dialog>
    <q-dialog v-model="showPreaAcuerdos" persistent>
      <q-card style="min-width: 40vw">
        <div class="stickyTop" v-if="validEstado() == 0">
          <q-toolbar>
            <q-toolbar-title class="text-bold"> Error </q-toolbar-title>
          </q-toolbar>
          <q-separator></q-separator>
          <q-card-section class="q-gutter-sm">
            <q-item-label> No se ha seleccionado ningún acuerdo. </q-item-label>
          </q-card-section>
          <q-separator></q-separator>
          <q-card-actions>
            <q-space></q-space>
            <q-btn outline color="primary" @click="showPreaAcuerdos = false">
              Cerrar
            </q-btn>
          </q-card-actions>
        </div>
        <div class="stickyTop" v-else-if="!validEstado()">
          <q-toolbar>
            <q-toolbar-title class="text-bold"> Error </q-toolbar-title>
          </q-toolbar>
          <q-separator></q-separator>
          <q-card-section class="q-gutter-sm">
            <q-item-label>
              Solo se puede preautorizar o autorizar, no ambas a la vez
            </q-item-label>
          </q-card-section>
          <q-separator></q-separator>
          <q-card-actions>
            <q-space></q-space>
            <q-btn outline color="primary" @click="showPreaAcuerdos = false">
              Cerrar
            </q-btn>
          </q-card-actions>
        </div>

        <div class="stickyTop" v-else-if="validEstado() == 3">
          <q-toolbar>
            <q-toolbar-title class="text-bold">
              Autorizar Acuerdos
            </q-toolbar-title>
          </q-toolbar>
          <q-separator></q-separator>
          <q-card-section class="q-gutter-sm">
            <q-item-label
              >Se autorizarán todos los acuerdos seleccionados así como los
              oficios relacionados
              <br />
              ¿Desea continuar?
            </q-item-label>
          </q-card-section>
          <q-separator></q-separator>
          <q-card-actions>
            <q-space></q-space>
            <q-btn color="primary" @click="preautorizarMasivo(true)">
              Continuar
            </q-btn>
            <q-btn outline color="primary" @click="showPreaAcuerdos = false">
              Cancelar
            </q-btn>
          </q-card-actions>
        </div>

        <div class="stickyTop" v-else-if="validEstado() == 2">
          <q-toolbar>
            <q-toolbar-title class="text-bold">
              Preautorizar Acuerdos
            </q-toolbar-title>
          </q-toolbar>
          <q-separator></q-separator>
          <q-card-section class="q-gutter-sm">
            <q-checkbox v-model="firmarOficios">¿Firmar oficios?</q-checkbox>
            <q-item-label
              >Se preautorizarán todos los acuerdos seleccionados
              <br />
              ¿Desea continuar?
            </q-item-label>
          </q-card-section>
          <q-separator></q-separator>
          <q-card-actions>
            <q-space></q-space>
            <q-btn color="primary" @click="preautorizarMasivo(false)">
              Continuar
            </q-btn>
            <q-btn outline color="primary" @click="showPreaAcuerdos = false">
              Cancelar
            </q-btn>
          </q-card-actions>
        </div>
      </q-card>
    </q-dialog>
    <DialogConfirmacion
      v-model="showDialogEliminar"
      label-btn-cancel="Cancelar"
      label-btn-ok="Aceptar"
      titulo="¿Deseas eliminar el acuerdo?"
      :subTitulo="`Al dar clic se eliminará el acuerdo con fecha ${
        selectedItem?.fechaAuto_F
      } del expediente ${selectedItem?.expediente?.asuntoAlias} ${
        selectedItem?.expediente?.catTipoAsunto
      } ${selectedItem?.expediente?.tipoProcedimiento || ''}`"
      @aceptar="eliminarAcuerdo"
    ></DialogConfirmacion>
    <DialogConfirmacion
      v-model="showCancelarSubirAcuerdo"
      titulo="¿Deseas cancelar?"
      :subTitulo="`Si continúas se perderán los cambios que has realizado.`"
      @aceptar="showSubirAcuerdo = false"
    >
    </DialogConfirmacion>
    <DialogConfirmacion
      v-model="showCancelarAcuerdo"
      label-btn-cancel="Cancelar"
      label-btn-ok="Aceptar"
      titulo="¿Deseas cancelar el acuerdo?"
      :subTitulo="`Al dar clic en aceptar se cancelará el acuerdo con fecha ${
        selectedItem?.fechaAuto_F
      } del expediente ${selectedItem?.expediente?.asuntoAlias} ${
        selectedItem?.expediente?.catTipoAsunto
      } ${selectedItem?.expediente?.tipoProcedimiento || ''}`"
      @aceptar="cambiarEstadoAcuerdo(3, selectedItem)"
    >
    </DialogConfirmacion>
  </q-page>
</template>

<script setup>
import { date } from "quasar";
import { ref, reactive, onMounted, onBeforeUnmount } from "vue";
import { Firmador } from "src/helpers/firmadorInicio";
import { useRoute } from "vue-router";
import { useFirmadorStore } from "src/stores/firmador-store";
import { useAuthStore } from "src/modules/auth/stores/useAuthStore";
import { catTipoAsunto } from "src/data/catalogos";
import { FiltrosColumnasDatos } from "../data/filtros-columnas";
import { useTramiteTabStore } from "../store/tramite-tab-store";
import { noty } from "src/helpers/notify";
import { manejoErrores } from "src/helpers/manejo-errores";
import { useTramiteStore } from "../store/tramite-store";
import SubirAcuerdo from "../components/UploadAcuerdos.vue";
import SelectDateComponent from "src/components/SelectDateComponent.vue";
import SelectStatusComponent from "src/components/SelectStatusComponent.vue";
import TablaSinDatos from "src/components/TablaSinDatos.vue";
import DetallePromocion from "src/modules/oficialia/components/DetallePromocion.vue";
import TableroAutorizar from "../components/TableroAutorizar.vue";
import OficioLibre from "../../../components/OficioLibre.vue";
import DialogConfirmacion from "src/components/DialogConfirmacion.vue";
import FiltrosColumnas from "../components/FiltrosColumnas.vue";
import VerAcuerdo from "../components/VerAcuerdo.vue";
import InputSearchTable from "src/components/InputSearchTable.vue";
import ModalWindowComponent from "src/components/ModalWindowComponent.vue";
import ExpedientePage from "src/modules/expediente/pages/ExpedientePage.vue";

const authStore = useAuthStore();
const selectDateComponent = ref(null);
const route = useRoute();
const tramiteTabStore = useTramiteTabStore();
const cargando = ref(false);
const cargandoEstadoAcuerdo = ref(false);
const esEditar = ref(false);
const showCancelarSubirAcuerdo = ref(false);
const showCancelarAcuerdo = ref(false);
const seleccionAutorizar = ref([]);
const tramiteStore = useTramiteStore();
const showSubirAcuerdo = ref(false);
const showOficioLibre = ref(false);
const showDialogEliminar = ref(false);
const showPreaAcuerdos = ref(false);
const oficioLibre = ref({});
const showDialogPdf = ref(false);
const showPromocion = ref(false);
const showTableroAutorizar = ref(false);
const selectedDate = ref({});
const selectedItem = ref({});
const cambioOficioLibre = ref(false);
const statusFirma = ref(route?.query["status-firma"]);
const firmadorStore = useFirmadorStore();
const estadosError = ["preautorización", "autorización", "cancelación"];
const estados = ["preautorizado", "autorizado", "cancelado"];
const firmarOficios = ref(false);
const maximizedToggle = ref(false);
const expedientes = ref([]);
const showExpediente = ref(false);

let refrescar = ref(false);
let rows = ref([]);
let textoBuscar = ref("");
let rowsPerPageOptions = ref([5, 7, 10, 15, 20, 25, 50, 0]);

// const getBookColor = (ta, nc) =>
//   catTipoAsunto.find((t) => t.name.toLowerCase() === ta.toLowerCase() && t.book.toLowerCase() === nc.toLowerCase())?.color;

// `${getBookColor(
//   props.row.expediente.catTipoAsunto,
//   props.row.expediente.nombreCorto
// )}; `

const getBookColor = (ta, nc) =>
  catTipoAsunto.find(
    (t) =>
      t.name?.toLowerCase() === ta?.toLowerCase() &&
      t.book?.toLowerCase() === nc?.toLowerCase(),
  )?.shortName || "empty";

const getBookColorHex = (ta, nc) =>
  catTipoAsunto.find(
    (t) =>
      t.name?.toLowerCase() === ta?.toLowerCase() &&
      t.book?.toLowerCase() === nc?.toLowerCase(),
  )?.color || "empty";

const filter = reactive({
  text: "",
  status: 0,
});
const coloresList = ref([]);

async function setDocumento(tipo) {
  cargando.value = true;
  if (tipo === "acuerdo") {
    showDialogPdf.value = true;
  } else {
    showPromocion.value = true;
  }
  cargando.value = false;
}

onMounted(async () => {
  selectedDate.value = {
    from: date.formatDate(Date.now(), "DD/MM/YYYY"),
    to: date.formatDate(Date.now(), "DD/MM/YYYY"),
  };
  await obtenerAcuerdosStorage();
});
async function obtenerAcuerdosStorage() {
  const acuerdoFirmar = localStorage.getItem("acuerdoFirmar");

  if (acuerdoFirmar && statusFirma.value) {
    const estado = localStorage.getItem("cambioEstadoAcuerdo");
    const acuerdos = JSON.parse(acuerdoFirmar);
    const filtros = JSON.parse(localStorage.getItem("filtrosTramite"));
    if (filtros) {
      textoBuscar.value = filtros.text;
      filter.status = filtros.status;
      Object.assign(valoresFiltros, filtros.valoresFiltros);
      selectDateComponent.value.setFecha(filtros.selectedDate);
      selectedDate.value = filtros.selectedDate;
      Object.assign(pagination.value, filtros.pagination);
      if (filtros.status == 0) await setRows();
    }

    try {
      const documentosTransanccion =
        await firmadorStore.obtenerStatusTransaccion();
      if (documentosTransanccion) {
        const acuerdosFirmados = acuerdos.filter((elementoDeIDs) => {
          return documentosTransanccion.some((elementoTransaccion) => {
            return (
              elementoTransaccion.id === elementoDeIDs.guidDocumento &&
              parseInt(elementoTransaccion.estatus) === 2
            );
          });
        });
        const acuerdosNoFirmados = acuerdos.filter((elementoDeIDs) => {
          return documentosTransanccion.some((elementoTransaccion) => {
            return (
              elementoTransaccion.id === elementoDeIDs.guidDocumento &&
              parseInt(elementoTransaccion.estatus) === 3
            );
          });
        });

        if (acuerdosNoFirmados && acuerdosNoFirmados.length > 1) {
          noty.error(
            `No se completó exitosamente el firmado de ${
              acuerdosNoFirmados.length
            } acuerdos. No es posible su ${estadosError[estado - 1]}`,
          );
        }
        if (acuerdosNoFirmados && acuerdosNoFirmados.length === 1) {
          const acuerdo = acuerdosNoFirmados[0];
          noty.error(
            `No se completó exitosamente el firmado del acuerdo con fecha ${
              acuerdo.fechaAuto_F
            } del expediente ${acuerdo.expediente.asuntoAlias} ${
              acuerdo.expediente.catTipoAsunto
            } ${acuerdo.expediente.tipoProcedimiento || ""}. No es posible su ${
              estadosError[estado - 1]
            }`,
          );
        }
        if (acuerdosFirmados.length == 1) {
          await cambiarEstadoAcuerdo(estado, acuerdosFirmados[0]);
        } else {
          const parametros = acuerdosFirmados.map((acuerdo) => {
            const params = {
              asuntoNeunId: acuerdo.expediente.asuntoNeunId,
              asuntoDocumentoId: acuerdo.asuntoDocumentoId,
              tipoUpdate: estado,
              nombreDocumento: acuerdo.nombreArchivo?.includes(".")
                ? acuerdo.nombreArchivo?.split(".")[0]
                : acuerdo.nombreArchivo,
              tipoAsunto: acuerdo.expediente.catTipoAsunto,
              tipoProcedimiento: acuerdo.expediente.tipoProcedimiento || "",
              numeroExpediente: acuerdo.expediente.asuntoAlias,
              mesa: acuerdo.mesa,
              secretarioId: acuerdo.secretarioId,
              enviarAlerta: estado == 3,
              numeroPromocion: acuerdo.numeroRegistro,
              id: acuerdo.guidDocumento,
            };
            return params;
          });

          const resultadoCambioEstadoAcuerdos =
            await tramiteStore.cambiarEstadoAcuerdoMasivo(parametros);
          if (
            resultadoCambioEstadoAcuerdos.filter((a) => a.status == "fulfilled")
              .length > 0
          ) {
            await setRows();
            noty.correcto(
              `Se han ${estados[estado - 1]} ${
                resultadoCambioEstadoAcuerdos.filter(
                  (a) => a.status == "fulfilled",
                ).length
              } acuerdos exitosamente`,
            );
          }
        }
        localStorage.removeItem("acuerdoFirmar");
        localStorage.removeItem("cambioEstadoAcuerdo");
        localStorage.removeItem("cveTransaccion");
      }
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
  }
  removeFiltrosLocalStorage();
}

const columns = [
  {
    name: "Expediente",
    required: true,
    label: "Expediente",
    align: "left",
    field: (row) => row.Expediente.AsuntoAlias,
    sortable: true,
  },
  {
    name: "Secretario",
    align: "center",
    label: "Mesa",
    field: "SecretarioUserName",
    sortable: true,
  },
  {
    name: "promocion",
    align: "center",
    label: "Promoción",
    field: "Promocion",
  },
  {
    name: "fechaPromocion",
    align: "center",
    label: "Fecha",
    field: "FechaPresentacion",
  },
  {
    name: "Promovente",
    align: "left",
    label: "Promovente",
    field: "promovente",
    sortable: true,
    headerStyle: "min-width: 120px",
  },
  {
    name: "acuerdo",
    align: "center",
    label: "Acuerdo",
    field: "OrigenPromocionDescripcion",
  },
  {
    name: "autorizar",
    align: "center",
    label: "Autorizar",
  },
  {
    name: "Contenido",
    align: "center",
    label: "Contenido",
    field: "OrigenPromocionDescripcion",
  },
  {
    name: "Capturo",
    align: "center",
    label: "Capturó",
    field: "UserNameOficial",
    sortable: true,
  },
  {
    name: "Preautorizo",
    align: "center",
    label: "Preautorizó",
    field: "EmpleadoPreAutoriza",
    sortable: true,
  },
  {
    name: "Autorizo",
    align: "center",
    label: "Autorizó",
    field: "EmpleadoAutoriza",
    sortable: true,
  },
  {
    name: "Cancelo",
    align: "center",
    label: "Canceló",
    field: "EmpleadoCancela",
    sortable: true,
  },
  {
    name: "acciones",
    align: "center",
    label: "",
    sortable: false,
  },
];
const valoresFiltros = reactive(new FiltrosColumnasDatos());

function setFilterStatus(value) {
  filter.status = value;
  pagination.value.page = 1;
}

function setSelectedDate(value) {
  const filtros = JSON.parse(localStorage.getItem("filtrosTramite"));
  if (!filtros) {
    selectedDate.value = value;
    pagination.value.page = 1;
    setRows();
  }
}

const getColor = (e) => coloresList.value.find((i) => i.status === +e).color;
function setColoresList() {
  coloresList.value = [
    {
      color: "bg-grey-4",
      status: 0,
      label: "Ver todas",
      number: tramiteStore.dataTramites.metaDatos?.totalTramites || 0,
      icon: "mdi-filter-off",
    },
    {
      color: "bg-red-2",
      status: 1,
      label: "Sin Acuerdo",
      number: tramiteStore.dataTramites.metaDatos?.totalSinAcuerdo || 0,
    },
    {
      color: "bg-orange-2",
      status: 5,
      label: "Cancelados",
      number: tramiteStore.dataTramites.metaDatos?.totalCancelados || 0,
    },
    {
      color: "bg-purple-2",
      status: 2,
      label: "Con Acuerdo",
      number: tramiteStore.dataTramites.metaDatos?.totalConAcuerdo || 0,
    },
    {
      color: "bg-blue-2",
      status: 3,
      label: "Preautorizados",
      number: tramiteStore.dataTramites.metaDatos?.totalPreAutorizados || 0,
    },
    {
      color: "bg-green-2",
      status: 4,
      label: "Autorizados",
      number: `${
        tramiteStore.dataTramites.metaDatos?.totalAutorizados || 0
      }  (${
        Math.round(
          (tramiteStore.dataTramites.metaDatos?.totalAutorizados * 100) /
            tramiteStore.dataTramites.metaDatos?.totalTramites,
        ) || 0
      }%)`,
    },
  ];
}

const pagination = ref({
  sortBy: "promocion",
  descending: true,
  page: 1,
  rowsPerPage: 0,
  rowsNumber: 50,
});

/**
 * va a store para obtener los registros
 */
async function setRows() {
  if (!selectedDate.value) {
    // setHoy();
    return;
  } else if (selectedDate.value && !selectedDate.value.from) {
    selectedDate.value = {
      from: date.formatDate(Date.parse(selectedDate.value), "DD/MM/YYYY"),
      to: date.formatDate(Date.parse(selectedDate.value), "DD/MM/YYYY"),
    };
    return;
  }
  pagination.value.rowsPerPage =
    pagination.value.rowsPerPage === 0 &&
    selectedDate.value.from !== selectedDate.value.to
      ? 50
      : pagination.value.rowsPerPage;

  cargando.value = true;
  if (tramiteStore.dashboardSelection.estado > 0) {
    valoresFiltros.secretario = tramiteStore.dashboardSelection.secretarioId;
    filter.status = tramiteStore.dashboardSelection.estado;
    tramiteStore.dashboardSelection = {};
  }
  try {
    await tramiteStore.obtenerTramites({
      ...selectedDate.value,
      status: filter.status,
      text: textoBuscar.value,
      ...pagination.value,
      valoresFiltros,
    });
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  //obtiene los tramite del api

  //setea data en rows
  rows.value = tramiteStore.dataTramites.datos || [];
  rows.value.forEach((row, index) => {
    row.index = index;
    row.selected = false;
  });
  pagination.value.rowsNumber = tramiteStore.dataTramites.totalRegistros;
  rowsPerPageOptions.value =
    selectedDate.value.from === selectedDate.value.to
      ? [5, 7, 10, 15, 20, 25, 50, 0]
      : [5, 7, 10, 15, 20, 25, 50];
  setColoresList();
  cargando.value = false;
}
/**
 * Busca en el server (Api)
 */
async function buscaEnBD() {
  pagination.value.page = 1;
  if (textoBuscar.value?.trim()) {
    refrescar.value = true;
    filter.text = textoBuscar.value;
  } else if (refrescar.value) {
    refrescar.value = false;
    filter.text = textoBuscar.value;
  }
}
async function onRequest(props) {
  const filtros = JSON.parse(localStorage.getItem("filtrosTramite"));
  pagination.value = filtros ? filtros.pagination : props.pagination;
  await setRows();
}

async function descargarAcuerdo(propiedad) {
  const parametrosArchivo = {
    tipoModulo: 2,
    asuntoNeunId: propiedad.expediente.asuntoNeunId,
    asuntoDocumentoId: propiedad.asuntoDocumentoId,
  };
  await tramiteStore.obtenerArchivoAcuerdo(parametrosArchivo);
  if (tramiteStore.archivoAcuerdo?.anexos[0]?.guidDocumento) {
    await tramiteStore.descargarDocumentos(
      tramiteStore.archivoAcuerdo.anexos[0].guidDocumento,
    );
  }
}

async function eliminarAcuerdo() {
  cargando.value = true;
  const params = {
    asuntoNeunId: selectedItem.value.expediente.asuntoNeunId,
    asuntoDocumentoId: selectedItem.value.asuntoDocumentoId,
  };
  try {
    await tramiteStore.eliminarAcuerdo(params);
    noty.correcto(
      `Acuerdo eliminado para el expediente ${selectedItem.value.expediente.asuntoAlias}`,
    );
    await setRows();
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  cargando.value = false;
}

async function cambiarEstadoAcuerdo(estado, acuerdo) {
  const params = {
    asuntoNeunId: acuerdo.expediente.asuntoNeunId,
    asuntoDocumentoId: acuerdo.asuntoDocumentoId,
    tipoUpdate: estado,
    nombreDocumento: acuerdo.nombreArchivo?.includes(".")
      ? acuerdo.nombreArchivo?.split(".")[0]
      : acuerdo.nombreArchivo,
    tipoAsunto: acuerdo.expediente.catTipoAsunto,
    tipoProcedimiento: acuerdo.expediente.tipoProcedimiento || "",
    numeroExpediente: acuerdo.expediente.asuntoAlias,
    mesa: acuerdo.mesa,
    secretarioId: acuerdo.secretarioId,
    enviarAlerta: estado == 3,
    numeroPromocion: acuerdo.numeroRegistro,
    id: acuerdo.guidDocumento,
  };
  cargandoEstadoAcuerdo.value = true;
  try {
    const result = await tramiteStore.cambiarEstadoAcuerdo(params);
    if (result) {
      await setRows();

      noty.correcto(
        `Se ha ${estados[estado - 1]} el acuerdo con fecha ${
          acuerdo.fechaAuto_F
        } del expediente ${acuerdo.expediente.asuntoAlias} ${
          acuerdo.expediente.catTipoAsunto
        } ${acuerdo.expediente.tipoProcedimiento || ""}`,
      );
    }
    showDialogPdf.value = false;
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  cargandoEstadoAcuerdo.value = false;
}

async function cambioFiltro(seleccionado) {
  Object.assign(valoresFiltros, seleccionado);
  await setRows();
}

async function firmadorInicio(estado) {
  const documentosAFirmar = {
    documentos: [
      {
        nombre: tramiteStore.archivoAcuerdo.anexos[0]?.nombre,
        id: tramiteStore.archivoAcuerdo.anexos[0]?.guidDocumento,
        tipoArchivo: "acuerdo",
        modulo: 2,
      },
    ],
    firmarOficios: selectedItem.value.firmarOficios,
    accion: estado,
  };

  const documentosStorageFirmar = [];
  documentosStorageFirmar[0] = selectedItem.value;

  try {
    localStorage.setItem("cambioEstadoAcuerdo", estado);
    localStorage.setItem(
      "acuerdoFirmar",
      JSON.stringify(documentosStorageFirmar),
    );
    setFiltros();
    await Firmador.obtenerURLGrafico(documentosAFirmar);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
}

async function preautorizarMasivo(autorizar) {
  const estadoCat = autorizar ? 2 : 1; //1 para preautorizar, 2 autorizar
  const params = [];
  try {
    let i = 0;
    rows.value.forEach((x) => {
      if (x.selected == true) {
        x.firmarOficios = false;
        if (firmarOficios.value) {
          x.firmarOficios = true;
        }
        params[i] = { ...x };
        i++;
      }
    });

    if (params.length < 1) {
      noty.error(`No se seleccionó ningún acuerdo para firmar`);
      return;
    }
    localStorage.setItem("cambioEstadoAcuerdo", estadoCat);
    localStorage.setItem("acuerdoFirmar", JSON.stringify(params));

    let documentos = [{}];

    documentos = params?.map((x) => ({
      nombre: x.nombreArchivo,
      id: x.guidDocumento,
      tipoArchivo: "acuerdo",
      modulo: 2,
    }));

    const documentosAFirmar = {
      documentos: documentos,
      firmarOficios: firmarOficios.value,
      accion: estadoCat,
    };
    await Firmador.obtenerURLGrafico(documentosAFirmar);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
}

function validEstado() {
  let i = 0;
  let params = [];
  var BreakException = {};
  rows.value.forEach((x) => {
    if (x.selected == true) {
      params[i] = { ...x };
      i++;
    }
  });

  if (params.length < 1) {
    return 0;
  } else {
    try {
      let muestra = params[0].estado;
      params.forEach((element) => {
        if (muestra != element.estado) {
          throw BreakException;
        }
      });

      if (muestra == 2) {
        return 2;
      } else if (muestra == 3) {
        return 3;
      }
    } catch (e) {
      if (e !== BreakException) {
        return false;
      }
    }
  }
}

function setFiltros() {
  localStorage.setItem(
    "filtrosTramite",
    JSON.stringify({
      selectedDate: selectedDate.value,
      status: filter.status,
      text: textoBuscar.value,
      pagination: pagination.value,
      valoresFiltros: valoresFiltros,
    }),
  );
}
onBeforeUnmount(() => {
  tramiteTabStore.setTabActive("tramite");
});
function removeFiltrosLocalStorage() {
  localStorage.removeItem("filtrosTramite");
}
</script>

<style lang="sass">
.my-sticky-header-table
  thead tr th
    position: sticky
    z-index: 1
  thead tr th:has(i)
    min-width:96px
  thead tr:first-child th
    top: 0
    background-color: #fff
</style>
