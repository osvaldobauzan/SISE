<template>
  <q-page class="q-pa-sm">
    <q-toolbar>
      <q-toolbar-title class="text-bold text-h4 text-primary">
        Oficialía de partes
      </q-toolbar-title>
      <q-btn
        dense
        no-caps
        unelevated
        outline
        color="primary"
        label="Firmar Promociones Seleccionadas"
        icon="mdi-playlist-check"
        @click="showFirma = true"
        class="q-ml-sm q-px-lg"
      >
      </q-btn>

      <q-btn
        dense
        no-caps
        unelevated
        color="primary"
        label="Capturar promoción"
        icon="mdi-plus"
        v-permitido="3"
        @click="
          selectedItem = null;
          selectedOrigen = 'OFICIALÍA';
          showAgregarPromo = true;
          titlePromocion = 'Capturar promoción';
          esEdicion = false;
        "
        class="q-ml-sm q-px-lg"
      >
      </q-btn>
      
      <q-btn
        dense
        no-caps
        outline
        color="primary"
        label="Varias promociones"
        icon="mdi-upload-multiple"
        v-permitido="6"
        @click="
          showUploadPromociones = true;
          selectedItem = null;
        "
        class="q-ml-sm q-px-lg"
      >
        <q-tooltip self="top end">Vincular varias promociones</q-tooltip>
      </q-btn>
    </q-toolbar>
    <q-toolbar>
      <SelectDateComponent
        title="Fecha de Promoción"
        @update:selectedDate="setSelectedDate"
      ></SelectDateComponent>
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
    <div class="q-gutter-sm q-mt-xs text-center">
      <q-btn
        flat
        dense
        no-caps
        label="Pendientes"
        class="bg-blue-5 text-white q-px-md"
      >
      </q-btn>

      <q-btn
        flat
        dense
        no-caps
        size="md"
        label="Promociones Electrónicas"
        class="bg-blue-2 q-px-md"
        @click="filterRows('JUICIO EN LÍNEA', 'Promoción Electrónica')"
      >
        <q-badge
          class="q-ml-sm text-bold"
          :color="
            parseInt(PanelElectronicas['Promociones Electrónicas']) > 0
              ? 'negative'
              : 'primary'
          "
          >{{ PanelElectronicas["Promociones Electrónicas"] }}</q-badge
        >
      </q-btn>
      <q-btn
        flat
        dense
        no-caps
        size="md"
        label="Demandas Electrónicas"
        class="bg-blue-2 q-px-md"
        @click="filterRows('JUICIO EN LÍNEA', 'Demanda Electrónica')"
      >
        <q-badge
          class="q-ml-sm text-bold"
          :color="
            parseInt(PanelElectronicas['Demandas Electrónicas']) > 0
              ? 'negative'
              : 'primary'
          "
          >{{ PanelElectronicas["Demandas Electrónicas"] }}</q-badge
        >
      </q-btn>
      <q-btn
        flat
        dense
        no-caps
        size="md"
        label="Comunicaciones Electrónicas"
        class="bg-blue-2 q-px-md"
        @click="filterRows('OCC', 'Comunicación Oficial')"
      >
        <q-badge
          class="q-ml-sm text-bold"
          :color="
            parseInt(PanelElectronicas['Comunicaciones Electrónicas']) > 0
              ? 'negative'
              : 'primary'
          "
          >{{ PanelElectronicas["Comunicaciones Electrónicas"] }}</q-badge
        >
      </q-btn>
      <q-btn
        flat
        dense
        no-caps
        size="md"
        label="Promociones Interconexión IOJ"
        class="bg-blue-2 q-px-md"
        @click="filterRows('INTERCONEXIÓN OJ', '')"
      >
        <q-badge
          class="q-ml-sm text-bold"
          :color="
            parseInt(PanelElectronicas['Promociones Interconexión IOJ']) > 0
              ? 'negative'
              : 'primary'
          "
          >{{ PanelElectronicas["Promociones Interconexión IOJ"] }}</q-badge
        >
      </q-btn>
      <q-btn
        flat
        dense
        no-caps
        size="md"
        label="Promociones Interconexión"
        class="bg-blue-2 q-px-md"
        @click="filterRows('INTERCONEXIÓN', '')"
      >
        <q-badge
          class="q-ml-sm text-bold"
          :color="
            parseInt(PanelElectronicas['Promociones Interconexión']) > 0
              ? 'negative'
              : 'primary'
          "
          >{{ PanelElectronicas["Promociones Interconexión"] }}</q-badge
        >
      </q-btn>
      <q-btn
        flat
        dense
        no-caps
        size="md"
        label="Recursos o Demandas OCC"
        class="bg-blue-2 q-px-md"
        @click="filterRows('OCC', 'Demanda Electrónica')"
      >
        <q-badge
          class="q-ml-sm text-bold"
          :color="
            parseInt(PanelElectronicas['Recursos o Demandas OCC']) > 0
              ? 'negative'
              : 'primary'
          "
          >{{ PanelElectronicas["Recursos o Demandas OCC"] }}</q-badge
        >
      </q-btn>
      <q-btn
        flat
        dense
        no-caps
        size="md"
        label="Promociones OCC"
        class="bg-blue-2 q-px-md"
        @click="filterRows('OCC', 'Promoción Electrónica')"
      >
        <q-badge
          class="q-ml-sm text-bold"
          :color="
            parseInt(PanelElectronicas['Promociones OCC']) > 0
              ? 'negative'
              : 'primary'
          "
          >{{ PanelElectronicas["Promociones OCC"] }}</q-badge
        >
      </q-btn>
    </div>
    <FiltrosColumnas @cambio-filtro="cambioFiltro" />
    <div class="row q-mt-sm">
      <div class="col">
        <q-table
          flat
          dense
          bordered
          wrap-cells
          binary-state-sort
          class="q-mx-md"
          :rows="rows"
          :columns="columns"
          :filter="filter"
          :loading="loading"
          :rows-per-page-options="rowsPerPageOptions"
          row-key="index"
          v-model:pagination="pagination"
          @request="onRequest"
          rows-per-page-label="Registros por página:"
          loading-label="Cargando..."
        >
          <template v-slot:loading>
            <q-inner-loading showing color="primary" />
          </template>
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
                  : 'Sin promociones'
              "
              :subTitulo="
                textoBuscar ||
                filter.status !== 0 ||
                (selectedDate.from == selectedDate.to &&
                  selectedDate.from !=
                    date.formatDate(new Date(), 'DD/MM/YYYY')) ||
                selectedDate.from != selectedDate.to
                  ? 'Intenta seleccionar otros criterios para realizar tu filtrado.'
                  : 'No hay documentos por asignar.'
              "
              :icono="
                textoBuscar || filter.status !== 0 ? 'mdi-filter' : 'mdi-file'
              "
            ></TablaSinDatos>
          </template>
          <template v-slot:body="props">
            <q-tr :props="props" :class="getColor(props.row.estado)">
              <q-td
                :style="`width: 200px; border-left: 10px solid ${getBookColorHex(
                  props.row.expediente.catTipoAsunto,
                  props.row.expediente.nombreCorto,
                )}`"
              >
                <q-item
                  v-ripple
                  clickable
                  v-if="props.row.expediente.asuntoNeunId"
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
                      caption
                    >
                      {{ props.row.expediente.tipoProcedimiento }}
                    </q-item-label>
                    <q-item-label>
                      <q-chip
                        outline
                        square
                        dense
                        color="negative"
                        icon="mdi-file-alert"
                        :label="props.row.cuadernoNombre"
                        v-if="
                          props.row.cuadernoNombre &&
                          props.row.cuadernoNombre === 'Sin capturar'
                        "
                      >
                      </q-chip>
                    </q-item-label>
                  </q-item-section>
                </q-item>
              </q-td>
              <q-td class="text-center minWidth">
                <q-btn
                  v-if="props.row.origenPromocion === 0"
                  flat
                  stack
                  color="secondary"
                  icon="mdi-upload"
                  @click="
                    showUploadPromociones = true;
                    selectedItem = props.row;
                  "
                  :label="
                    props.row.NumeroRegistro === 0
                      ? ''
                      : props.row.numeroRegistro
                  "
                >
                  <q-tooltip>Vincular promoción</q-tooltip>
                </q-btn>
                <q-btn
                  v-else-if="!props.row.conArchivo"
                  v-permitido="7"
                  flat
                  stack
                  no-caps
                  color="negative"
                  icon="mdi-upload"
                  @click="
                    showUploadPromociones = true;
                    selectedItem = props.row;
                    titlePromocion = 'Vincular promoción';
                  "
                >
                  <q-item-label>Sin Archivo</q-item-label>
                  <q-item-label class="text-secondary">{{
                    props.row.numeroRegistro === 0
                      ? ""
                      : props.row.numeroRegistro
                  }}</q-item-label>

                  <q-tooltip>Vincular promoción</q-tooltip>
                </q-btn>
                <q-btn
                  v-else-if="props.row.conArchivo"
                  v-permitido="12"
                  flat
                  stack
                  color="secondary"
                  :icon="props.row.firmado != null ? 'mdi-paperclip-check' : 'mdi-paperclip'"
                  @click="visualizaExpediente(props.row)"
                  :label="
                    props.row.numeroRegistro === 0
                      ? ''
                      : props.row.numeroRegistro
                  "
                >
                  <q-tooltip>Ver promoción</q-tooltip>
                </q-btn>
              </q-td>
              <q-td class="minWidth">
                {{ props.row.origenPromocionDescripcion }}
                <q-item-label
                  v-if="props.row.estado === 1 || props.row.esPromocionE"
                  class="text-secondary"
                >
                  {{ props.row.nombreOrigen }}
                </q-item-label>
              </q-td>
              <q-td class="text-center">
                <div v-if="props.row.estado == 4 && props.row.firmado == null && props.row.conArchivo">
                  <q-checkbox v-model="props.row.selected" />
                </div>
              </q-td>
              <q-td class="text-left minWidth">
                <span>
                  {{
                    date.formatDate(props.row.fechaPresentacion, "DD/MM/YYYY")
                  }}</span
                >
                <br />
                <q-item-label class="text-secondary">
                  {{ date.formatDate(props.row.fechaPresentacion, "HH:mm") }}
                </q-item-label>
              </q-td>
              <q-td class="minWidth">
                <q-item class="text-left q-pl-none">
                  <q-item-section>
                    <q-item-label>
                      {{ props.row.secretarioUserName || "" }}
                    </q-item-label>
                    <q-item-label
                      class="text-secondary"
                      caption
                      v-if="props.row.secretarioUserName"
                    >
                      {{ props.row.mesa }}
                    </q-item-label>
                  </q-item-section>
                  <q-tooltip v-if="props.row.secretarioUserName">
                    {{ props.row.secretarioDescripcion }}
                  </q-tooltip>
                </q-item>
              </q-td>
              <q-td class="minWidth">{{
                props.row.tipoContenidoDescripcion
              }}</q-td>
              <q-td class="minWidth">
                <q-item-section>
                  <q-item-label>
                    {{ props.row.parteDescripcion || "" }}
                  </q-item-label>
                  <q-item-label class="text-secondary" caption>
                    {{ props.row.clasePromoventeDescripcion }}
                  </q-item-label>
                </q-item-section>
              </q-td>
              <q-td class="minWidth">
                <q-item-section>
                  <q-item-label>
                    {{ props.row.usuarioCaptura || "" }}
                  </q-item-label>
                  <q-item-label class="text-secondary" caption>
                    {{ date.formatDate(props.row.fechaCaptura, "DD/MM/YYYY") }}
                  </q-item-label>
                </q-item-section>
              </q-td>
              <q-td>
                <div class="row">
                  <div class="row" style="width: 90px">
                    <div class="col-6">
                      <q-btn
                        v-permitido="8"
                        flat
                        round
                        color="blue"
                        icon="mdi-file-edit-outline"
                        @click="
                          showAgregarPromo = true;
                          selectedItem = props.row;
                          titlePromocion = 'Editar promoción';
                        "
                      >
                        <q-tooltip> Editar promoción </q-tooltip>
                      </q-btn>
                    </div>
                    <div class="col-6">
                      <q-btn flat round color="blue" icon="mdi-dots-vertical">
                        <q-menu auto-close>
                          <q-list style="min-width: 100px">
                            <q-item
                              v-permitido="9"
                              clickable
                              v-ripple
                              v-if="
                                !(
                                  props.row.OrigenPromocion === 0 ||
                                  props.row.OrigenPromocion === 4 ||
                                  props.row.OrigenPromocion === 7
                                )
                              "
                              @click="
                                showDetalle = true;
                                selectedItem = props.row;
                              "
                            >
                              <q-item-section side
                                ><q-icon name="mdi-eye"></q-icon
                              ></q-item-section>
                              <q-item-section>Ver detalle</q-item-section>
                            </q-item>
                            <q-item
                              v-if="props.row.estado === 4"
                              v-permitido="10"
                              clickable
                              v-ripple
                              @click="
                                selectedItem = props.row;
                                imprimirQR();
                              "
                            >
                              <q-item-section side
                                ><q-icon name="mdi-qrcode"></q-icon
                              ></q-item-section>
                              <q-item-section>Reimprimir QR</q-item-section>
                            </q-item>
                            <q-item
                              v-if="
                                props.row.estadoAcuerdo !== 4 &&
                                props.row.estado !== 1
                              "
                              v-permitido="11"
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
                </div>
              </q-td>
            </q-tr>
            <q-tr colspan="13" v-show="props.expand" :props="props">
              <q-td colspan="100%">
                <q-table
                  flat
                  hide-header
                  hide-bottom
                  :rows="getTableRows(props.row.detalle)"
                  :cols="getTableCols(props.row.detalle)"
                ></q-table>
              </q-td>
            </q-tr>
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
            class=""
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
    <q-dialog v-model="showFirma" persistent>
      <q-card style="min-width: 20vw">
        <div class="stickyTop" v-if="!validEstado()">
          <q-toolbar>
            <q-toolbar-title class="text-bold"> Error </q-toolbar-title>
          </q-toolbar>
          <q-separator></q-separator>
          <q-card-section class="q-gutter-sm">
            <q-item-label> No se ha seleccionado ninguna promoción. </q-item-label>
          </q-card-section>
          <q-separator></q-separator>
          <q-card-actions>
            <q-space></q-space>
            <q-btn outline color="primary" @click="showFirma = false">
              Cerrar
            </q-btn>
          </q-card-actions>
        </div>
        <div class="stickyTop" v-else-if="validEstado()">
          <q-toolbar>
            <q-toolbar-title class="text-bold">
              Firmar Promociones
            </q-toolbar-title>
          </q-toolbar>
          <q-separator></q-separator>
          <q-card-section class="q-gutter-sm">
            <q-item-label
              >Se firmarán todas las promociones seleccionadas
              <br />
              ¿Desea continuar?
            </q-item-label>
          </q-card-section>
          <q-separator></q-separator>
          <q-card-actions>
            <q-space></q-space>
            <q-btn color="primary" :loading="cargaFirma" @click="firmaMasiva()">
              Continuar
            </q-btn>
            <q-btn outline color="primary" @click="showFirma = false">
              Cancelar
            </q-btn>
          </q-card-actions>
        </div>
      </q-card>
    </q-dialog>
    <q-dialog v-model="showExpediente" :maximized="maximizedToggle">
      <ModalWindowComponent
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
    <q-dialog
      :style="{ visibility: dialogVisible ? 'visible' : 'hidden' }"
      v-model="showDetalle"
      full-height
      full-width
    >
      <DetallePromocion
        :model-value="selectedItem"
        :expediente="selectedItem"
        @update:promocion-found="promoEncontrada"
      >
      </DetallePromocion>
    </q-dialog>
    <q-dialog
      persistent
      v-model="showAgregarPromo"
      :expediente="selectedItem"
      full-width
    >
      <AddPromocion
        @cerrar="
          (val) => {
            showAlertCancelarCaptura = val;
            showAgregarPromo = val;
          }
        "
        :title="titlePromocion"
        :origen="selectedOrigen"
        :promocion="selectedItem"
        @add:anexo="
          showAddAnexo = true;
          editarAnexo = false;
        "
        @update:anexo="
          (val) => {
            updateAnexo = val;
            editarAnexo = true;
            showAddAnexo = true;
          }
        "
        :addAnexo="anexo"
        :updateAnexo="updateAnexo"
        :es-editar="esEdicion"
        @refrescar-tabla="setRows"
      >
      </AddPromocion>
    </q-dialog>
    <q-dialog v-model="showUploadPromociones" persistent>
      <UploadPromociones
        :expediente="selectedItem"
        :multiple="selectedItem === null"
        :promociones="promociones"
        @refrescar-tabla="setRows"
        @cancelar="showAlertaCancelarCargaMasiva = true"
        @cerrar="showUploadPromociones = false"
      >
      </UploadPromociones>
    </q-dialog>
    <q-dialog v-model="showDialogPdf" full-height full-width>
      <DetallePromocion :model-value="promocionSeleccionada"></DetallePromocion>
    </q-dialog>
    <DialogConfirmacion
      v-model="showDialogEliminar"
      label-btn-cancel="Cancelar"
      label-btn-ok="Eliminar"
      titulo="¿Deseas eliminar la promoción?"
      :subTitulo="`Se eliminará la promoción número ${selectedItem?.numeroRegistro} del expediente ${selectedItem?.expediente?.asuntoAlias} ${selectedItem?.expediente?.catTipoAsunto}.`"
      @aceptar="eliminarPromocion"
    ></DialogConfirmacion>
    <DialogConfirmacion
      v-model="showAlertCancelarCaptura"
      titulo="¿Deseas cancelar?"
      :subTitulo="`Si continúas se perderán los cambios que has realizado.`"
      @aceptar="showAgregarPromo = false"
    >
    </DialogConfirmacion>
    <DialogAnexo
      v-if="showAddAnexo"
      v-model="showAddAnexo"
      @add:anexoValue="setAnexo"
      @update:anexoValue="setUpdateAnexo"
      :anexoValue="updateAnexo"
      :esEditar="editarAnexo"
      :es-edicion="esEdicion"
      :promocion="selectedItem"
    ></DialogAnexo>
    <GenerateQr
      @print="generateQrCode = false"
      v-if="generateQrCode"
      v-model="jsonParaQr"
      :descripcion="descripcionQr"
      :es-html="true"
      :auto-print="true"
    ></GenerateQr>
    <DialogConfirmacion
      v-model="showAlertaCancelarCargaMasiva"
      :titulo="`¿Deseas cancelar la vinculación ${
        selectedItem === null ? 'múltiple' : ''
      }?`"
      :subTitulo="
        selectedItem === null
          ? `Ningún archivo será vinculado.`
          : `El archivo no será vinculado.`
      "
      @aceptar="showUploadPromociones = false"
    ></DialogConfirmacion>
  </q-page>
</template>

<script setup>
import { date } from "quasar";
import { Buffer } from "buffer";
import { noty } from "src/helpers/notify";
import { useRoute } from "vue-router";
import { catTipoAsunto } from "src/data/catalogos";
import { useOficialiaStore } from "../stores/oficialia-store";
import { useCatalogosStore } from "../../../stores/catalogos-store";
import { useUsuariosStore } from "../../../stores/usuarios-store";
import { useOficialiaTabStore } from "../stores/oficialia-tab-store";
import { manejoErrores } from "src/helpers/manejo-errores";
import { FiltrosColumnasDatos } from "../data/filtros-columnas";
import { ref, reactive, onMounted, onBeforeUnmount } from "vue";
import SelectDateComponent from "src/components/SelectDateComponent.vue";
import SelectStatusComponent from "src/components/SelectStatusComponent.vue";
import AddPromocion from "../components/AddPromocion.vue";
import UploadPromociones from "../components/UploadPromociones.vue";
import DetallePromocion from "../components/DetallePromocion.vue";
import TablaSinDatos from "src/components/TablaSinDatos.vue";
import DialogConfirmacion from "src/components/DialogConfirmacion.vue";
import DialogAnexo from "../components/DialogAnexo.vue";
import GenerateQr from "src/components/GenerateQr.vue";
import InputSearchTable from "src/components/InputSearchTable.vue";
import FiltrosColumnas from "../components/FiltrosColumnas.vue";
import ModalWindowComponent from "src/components/ModalWindowComponent.vue";
import ExpedientePage from "src/modules/expediente/pages/ExpedientePage.vue";
import { Firmador } from "src/helpers/firmadorInicio";

const PanelElectronicas = ref([]);
const oficialiaTabStore = useOficialiaTabStore();
const oficialiaStore = useOficialiaStore();
const catalogosStore = useCatalogosStore();
const usuariosStore = useUsuariosStore();
const showDialogEliminar = ref(false);
const showAddAnexo = ref(false);
const showAlertCancelarCaptura = ref(false);
const showAlertaCancelarCargaMasiva = ref(false);
const titlePromocion = ref("Capturar Promoción");
const esEdicion = ref(false);
const showDialogPdf = ref(false);
const showAgregarPromo = ref(false);
const showUploadPromociones = ref(false);
const showDetalle = ref(false);
const showFirma = ref(false);
const selectedItem = ref({ expediente: {} });
const selectedOrigen = ref("Oficialía");
const anexo = ref(null);
const updateAnexo = ref(null);
const editarAnexo = ref(false);
const generateQrCode = ref(false);
const jsonParaQr = ref("");
const descripcionQr = ref("");
const promociones = ref([]);
const identificadorBuscado = ref("");
const dialogVisible = ref();
const valoresFiltros = reactive(new FiltrosColumnasDatos());
const urlVerPromo = ref("");
const maximizedToggle = ref(false);
const expedientes = ref([]);
const showExpediente = ref(false);
const cargaFirma = ref(false);

const filter = reactive({
  text: "",
  status: 0,
});

const selectedDate = reactive({
  from: date.formatDate(Date.now(), "DD/MM/YYYY"),
  to: date.formatDate(Date.now(), "DD/MM/YYYY"),
});

function setAnexo(a) {
  anexo.value = a;
}
function setUpdateAnexo(a) {
  updateAnexo.value = a;
}

const coloresList = ref([]);
const getColor = (e) => coloresList.value.find((i) => i.status === e)?.color;
function setColoresList() {
  coloresList.value = [
    {
      color: "bg-grey-4",
      status: 0,
      label: "Ver todas",
      number: oficialiaStore.data.metaDatos.totalPromociones || 0,
      icon: "mdi-filter-off",
    },
    {
      color: "bg-blue-2",
      status: 1,
      label: "Electrónicas sin captura",
      number: oficialiaStore.data.metaDatos.totalSinCaptura || 0,
    },
    {
      color: "bg-yellow-2",
      status: 2,
      label: "Físicas sin archivo",
      number: oficialiaStore.data.metaDatos.totalCapturadas || 0,
    },
    {
      color: "bg-green-2",
      status: 4,
      label: "Turnadas a mesa",
      number: `${oficialiaStore.data.metaDatos.enviadasAMesa}  (${
        Math.round(
          (oficialiaStore.data.metaDatos.enviadasAMesa * 100) /
            oficialiaStore.data.metaDatos.totalPromociones,
        ) || 0
      }%)`,
    },
  ];
}
let promocionSeleccionada = {};
let loading = ref(false);
let refrescar = ref(false);
let rows = ref([]);
let textoBuscar = ref(oficialiaStore.textoBuscar);
let rowsPerPageOptions = ref([5, 7, 10, 15, 20, 25, 50, 0]);

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

function getTableCols() {
  return [
    {
      name: "Propiedad",
      style: "width: 50px",
    },
    { name: "Valor" },
  ];
}

function getTableRows(obj) {
  const prop = Object.keys(obj || []).map((i) => ({
    name: i,
  }));

  return prop.map((i) => ({
    Propiedad: i.name,
    Valor: obj[i.name],
  }));
}

function promoEncontrada(promocionEncontrada) {
  if (promocionEncontrada != undefined) {
    showDetalle.value = promocionEncontrada;
    dialogVisible.value = true;
  }
  if (promocionEncontrada === false) {
    if (selectedItem.value.expediente.asuntoNeunId != 0) {
      identificadorBuscado.value =
        "con el NEUN: " + selectedItem.value.expediente.asuntoNeunId;
    } else {
      identificadorBuscado.value =
        "con el Id: " + selectedItem.value.kIdElectronica;
    }
    const miMensajeError =
      "La promoción " +
      identificadorBuscado.value +
      " no se encontró en este OJ";
    manejoErrores.mostrarError(miMensajeError);
  }
}

function Decode64(cadenaCodificada) {
  const cadenaDecodificada = Buffer.from(cadenaCodificada, "base64").toString(
    "utf-8",
  );
  mostrarDetallePromocionCorreo(cadenaDecodificada);
}

const route = useRoute();
const mostrarDetallePromocionCorreo = (url) => {
  const params = new URLSearchParams(url);
  selectedItem.value.expediente.asuntoNeunId = params.get("asuntoNeunId");
  selectedItem.value.origen = params.get("origen");
  selectedItem.value.numeroOrden = params.get("numeroOrden");
  selectedItem.value.yearPromocion = params.get("yearPromocion");
  selectedItem.value.kIdElectronica = params.get("kIdElectronica");
  selectedItem.value.expediente.catOrganismoId = params.get("catOrganismoId");
  selectedItem.value.esPromocionE = params.get("esPromocionE");
  selectedItem.value.estado = params.get("estado");
  dialogVisible.value = false;
  showDetalle.value = true;
  promoEncontrada();
};

onMounted(async () => {
  urlVerPromo.value = route?.query.urlVerPromo;
  if (urlVerPromo.value) {
    Decode64(urlVerPromo.value);
  }
  try {
    await catalogosStore.obtenerAsuntos();
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  try {
    await catalogosStore.obtenerTipos();
  } catch (error) {
    manejoErrores.mostrarError(error);
  }

  try {
    await usuariosStore.obtenerSecretarios();
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
});
onBeforeUnmount(() => {
  oficialiaStore.actualizaTextoBuscar("");
  oficialiaTabStore.setTabActive("oficialia");
});
/**
 * Va a al store para obtenerExpediente y mostrarlo
 * @param {*} id asuntoNeunId
 */
async function visualizaExpediente(promocion) {
  promocionSeleccionada = promocion;
  showDialogPdf.value = true;
}

const columns = [
  {
    name: "Expediente",
    label: "Expediente",
    align: "left",
    sortable: true,
    field: (row) => row.Expediente.AsuntoAlias,
    style: "width: 10px",
  },
  {
    name: "Promoción",
    align: "center",
    label: "Promoción",
    field: "NumeroRegistro",
    sortable: true,
    style: "width: 126px; ",
  },
  {
    name: "Origen",
    align: "left",
    label: "Origen",
    field: "OrigenPromocionDescripcion",
    sortable: true,
  },
  {
    name: "fimar",
    align: "center",
    label: "Firmar",
  },
  {
    name: "Fecha",
    align: "left",
    label: "Presentado",
    field: "FechaPresentacion",
    sortable: true,
  },
  {
    name: "Secretario",
    align: "left",
    label: "Mesa",
    field: "SecretarioUserName",
    sortable: true,
  },
  {
    name: "Contenido",
    align: "left",
    label: "Contenido",
    field: "TipoContenidoDescripcion",
    sortable: true,
  },
  {
    name: "Promovente",
    align: "left",
    label: "Promovente",
    field: "ParteDescripcion",
    sortable: true,
  },
  {
    name: "Capturo",
    align: "left",
    label: "Capturó",
    field: "SecretarioUserName",
    sortable: true,
  },
  {
    name: "acciones",
    align: "center",
    label: "",
    sortable: false,
  },
];

function setFilterStatus(value) {
  filter.status = value;
  pagination.value.page = 1;
}

function setSelectedDate(value) {
  selectedDate.value = value;
  pagination.value.page = 1;
  setRows();
}
/**
 * va a store para obtener los registros
 */

async function setRows() {
  if (!selectedDate.value) {
    return;
  } else if (selectedDate.value && !selectedDate.value.from) {
    const fecha = `${selectedDate.value.split("/")[1]}/${
      selectedDate.value.split("/")[0]
    }/${selectedDate.value.split("/")[2]}`;
    selectedDate.value = {
      from: date.formatDate(Date.parse(fecha), "DD/MM/YYYY"),
      to: date.formatDate(Date.parse(fecha), "DD/MM/YYYY"),
    };
    return;
  }
  pagination.value.rowsPerPage =
    pagination.value.rowsPerPage === 0 &&
    selectedDate.value.from !== selectedDate.value.to
      ? 50
      : pagination.value.rowsPerPage;
  loading.value = true;
  //obtiene las promiciones del api
  try {
    await oficialiaStore.obtenerPromociones({
      ...selectedDate.value,
      status: filter.status,
      text: textoBuscar.value,
      ...pagination.value,
      valoresFiltros,
    });

  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  //setea data en rows
  rows.value = oficialiaStore.data.datos;
  rows.value.forEach((row, index) => {
    row.index = index;
    row.selected = false;
  });
  pagination.value.rowsNumber = oficialiaStore.data.totalRegistros;
  rowsPerPageOptions.value =
    selectedDate.value.from === selectedDate.value.to
      ? [5, 7, 10, 15, 20, 25, 50, 0]
      : [5, 7, 10, 15, 20, 25, 50];
  setColoresList();

  promociones.value = rows.value.map((item) => ({
    numeroRegistro: item.numeroRegistro,
    yearPromocion: item.yearPromocion,
    conArchivo: item.conArchivo,
  }));

  PanelElectronicas.value = {
    "Promociones Electrónicas": rows.value.filter(
      (i) =>
        i.estado === 1 &&
        i.origenPromocionDescripcion === "JUICIO EN LÍNEA" &&
        i.nombreOrigen === "Promoción Electrónica",
    ).length,
    "Demandas Electrónicas": rows.value.filter(
      (i) =>
        i.estado === 1 &&
        i.origenPromocionDescripcion === "JUICIO EN LÍNEA" &&
        i.nombreOrigen === "Demanda Electrónica",
    ).length,
    "Comunicaciones Electrónicas": rows.value.filter(
      (i) =>
        i.estado === 1 &&
        i.origenPromocionDescripcion === "OCC" &&
        i.nombreOrigen === "Comunicación Oficial",
    ).length,
    "Promociones Interconexión IOJ": rows.value.filter(
      (i) =>
        i.estado === 1 && i.origenPromocionDescripcion === "INTERCONEXIÓN OJ",
    ).length,
    "Promociones Interconexión": rows.value.filter(
      (i) => i.estado === 1 && i.origenPromocionDescripcion === "INTERCONEXIÓN",
    ).length,
    "Recursos o Demandas OCC": rows.value.filter(
      (i) =>
        i.estado === 1 &&
        i.origenPromocionDescripcion === "OCC" &&
        i.nombreOrigen === "Demanda Electrónica",
    ).length,
    "Promociones OCC": rows.value.filter(
      (i) =>
        i.estado === 1 &&
        i.origenPromocionDescripcion === "OCC" &&
        i.nombreOrigen === "Promoción Electrónica",
    ).length,
  };

  loading.value = false;
}

function filterRows(origenPromocion, nombreOrigen) {
  if (nombreOrigen.length>0) {
  rows.value = oficialiaStore.data.datos.filter((i) =>
        i.estado === 1 &&
        i.origenPromocionDescripcion === origenPromocion &&
        i.nombreOrigen === nombreOrigen);
  } else {
    rows.value = oficialiaStore.data.datos.filter((i) =>
        i.estado === 1 &&
        i.origenPromocionDescripcion === origenPromocion);
  }
};

const pagination = ref({
  sortBy: "Promoción",
  descending: false,
  page: 1,
  rowsPerPage: 0,
  rowsNumber: 50,
});
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

async function cambioFiltro(seleccionado) {
  Object.assign(valoresFiltros, seleccionado);
  await setRows();
}

async function onRequest(props) {
  pagination.value = props.pagination;
  await setRows();
}
/**
 * Elimina promoción
 */
async function eliminarPromocion() {
  let correcto = true;
  const parametros = {
    asuntoNeunId: selectedItem.value.expediente.asuntoNeunId,
    yearPromocion: selectedItem.value.yearPromocion,
    numeroOrden: selectedItem.value.numeroOrden,
    numeroPromocion: selectedItem.value.numeroRegistro,
    expediente: selectedItem.value.expediente.asuntoAlias,
    catIdOrganismo: selectedItem.value.expediente.catOrganismoId,
  };
  try {
    await oficialiaStore.eliminarPromocion(parametros);
    noty.correcto(
      `Se ha eliminado la promoción ${parametros.numeroPromocion} del expediente ${parametros.expediente}`,
    );
    correcto = true;
  } catch (error) {
    manejoErrores.mostrarError(error);
    correcto = false;
  }

  if (correcto) {
    await setRows();
  }
}
async function imprimirQR() {
  const parametros = {
    asuntoNeunId: selectedItem.value.expediente.asuntoNeunId,
    origen: selectedItem.value.origen,
    numeroOrden: selectedItem.value.numeroOrden,
    yearPromocion: selectedItem.value.yearPromocion,
    kIdElectronica: selectedItem.value.kIdElectronica,
    catOrganismoId: selectedItem.value.expediente.catOrganismoId,
    esPromocionE: selectedItem.value.esPromocionE,
    estado: selectedItem.value.estado,
  };
  try {
    await oficialiaStore.detallePromocion(parametros);
  } catch (error) {
    manejoErrores.mostrarError(error);
  }
  const detalle = oficialiaStore.promocion;
  const jsonQr = {"E": {"N": detalle.asuntoNeunId},"P": {"NP": `${detalle.numeroRegistro}/${selectedItem.value.yearPromocion}`,"O": selectedItem.value.numeroOrden},
  };
  descripcionQr.value = `${detalle.catOrganismo}
    <br/>
    ${detalle.expediente}
    <br/>
    ${detalle.asuntoNeunId}
    <br/>
    ${date.formatDate(detalle.fechaPresentacion, "DD/MM/YYYY")}
    <br/>
    ${detalle.horaPresentacion}
    <br/>
    ${detalle.secretarioNombre}`;
  jsonParaQr.value = JSON.stringify(jsonQr);
  generateQrCode.value = true;
}

async function firmaMasiva() {
  cargaFirma.value = true;
  const params = [];
  let i = 0;
  rows.value.forEach((x) => {
    if (x.selected == true) {
      params[i] = { ...x };
      i++;
    }
  });

  if (params.length < 1) {
    noty.error(`No se seleccionó ningún acuerdo para firmar`);
    return;
  }
  //console.log(selectedDate);
  try {
    let datosDocs = [];
    let j = 0;
    for (const element of params) {
      datosDocs[j] = await oficialiaStore.obtenerArchivosYAnexos(
        element.expediente?.asuntoNeunId,

        element.yearPromocion,
        element.numeroOrden,
        1,
        null,
        element.origen,
        element.kIdElectronica,
      );
      j++;
    }
    let documentos = [{}];

    documentos = datosDocs?.map((x) => ({
      nombre: x.archivos[0].nombre,
      id: x.archivos[0].guidDocumento,
      tipoArchivo: "promocion",
      modulo: 1,
    }));

    const documentosAFirmar = {
      documentos: documentos,
      firmarOficios: true,
      accion: 1,
    };
    await Firmador.obtenerURLGraficoOficialia(documentosAFirmar);
  } catch (error) {
    cargaFirma.value = false;
    manejoErrores.mostrarError(error);
  }
}

function validEstado() {
  let i = 0;
  let params = [];
  rows.value.forEach((x) => {
    if (x.selected == true) {
      params[i] = { ...x };
      i++;
    }
  });
  return params.length < 1 ? false : true;
}

</script>
<script>
export default {
  inheritAttrs: false,
};
</script>

<style lang="css" scoped>
.minWidth {
  min-width: 100px;
}

.q-splitter--vertical > .q-splitter__panel {
  height: unset;
}

.q-gutter-x-xs > *,
.q-gutter-xs > * {
  margin-left: 8px;
}

.q-table tbody td {
  font-size: 14px;
}

.q-table--dense .q-table th {
  padding: 16px 8px;
}

.btn-width {
  min-width: -webkit-fill-available;
}
</style>
