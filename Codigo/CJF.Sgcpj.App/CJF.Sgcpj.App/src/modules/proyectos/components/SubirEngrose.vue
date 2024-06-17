<template>
  <q-card style="min-width: 900px">
    <q-toolbar>
      <q-toolbar-title> Subir Engrose </q-toolbar-title>
      <q-btn flat round dense icon="mdi-close" v-close-popup />
    </q-toolbar>
    <q-stepper v-model="step" ref="stepper" color="primary" animated>
      <q-step :name="1" title="Sentencia" prefix="1" :done="step > 1">
        <q-list>
          <q-item>
            <q-item-section>
              <q-item-label class="text-subtitle1 text-bold">
                Datos del Expediente
              </q-item-label>
              <div class="row wrap q-py-none">
                <q-item class="col-4 q-py-none">
                  <q-item-section>
                    <q-item-label class="text-grey-6">Expediente</q-item-label>
                    <q-item-label>{{ item?.AsuntoAlias }}</q-item-label>
                  </q-item-section>
                </q-item>
                <q-item class="col-4 q-py-none">
                  <q-item-section>
                    <q-item-label class="text-grey-6"
                      >Tipo de asunto</q-item-label
                    >
                    <q-item-label>{{ item?.CatTipoAsunto }}</q-item-label>
                  </q-item-section>
                </q-item>
                <q-item class="col-4 q-py-none">
                  <q-item-section>
                    <q-item-label class="text-grey-6">Cuaderno</q-item-label>
                    <q-item-label>{{ item?.Cuaderno }}</q-item-label>
                  </q-item-section>
                </q-item>
              </div>
            </q-item-section>
          </q-item>
          <q-item>
            <q-item-section>
              <q-item-label class="text-subtitle1 text-bold">
                Datos del Proyecto
              </q-item-label>
              <div class="row wrap">
                <q-item class="col">
                  <q-item-section>
                    <q-item-label caption>Titular</q-item-label>
                    <q-item-label>{{ item?.TitularNombre }}</q-item-label>
                  </q-item-section>
                </q-item>
                <q-item class="col">
                  <q-item-section>
                    <q-item-label caption>Secretario</q-item-label>
                    <q-item-label>{{ item?.SecretarioNombre }}</q-item-label>
                  </q-item-section>
                </q-item>
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
                    label="Tipo de sentencia o resolución"
                    :options="optionsTipoSentencia"
                    v-model="tipoSentenciaSelected"
                  ></q-select>
                </div>
                <div class="col">
                  <q-select
                    dense
                    filled
                    label="Selecciona el sentido"
                    :options="optionsTipoSentido"
                    v-model="tipoSentidoSelected"
                  ></q-select>
                </div>
              </div>
            </q-item-section>
          </q-item>
          <q-item>
            <q-item-section>
              <q-item-label>
                Sube el archivo word del proyecto generado para este expediente
              </q-item-label>
              <q-item-label>
                <q-uploader
                  flat
                  url="http://localhost:4444/upload"
                  class="full-width"
                  color="transparent"
                  style="
                    border-radius: 7;
                    border-style: dashed;
                    border-width: 2px;
                    border-color: #cfcfcf;
                  "
                >
                  <template v-slot:header="scope">
                    <q-item>
                      <q-item-section side>
                        <q-icon name="mdi-upload" color="grey-8" />
                      </q-item-section>
                      <q-item-section>
                        <q-item-label class="text-grey-8">
                          Arrastra y suelta el archivo aquí o
                          <q-btn
                            type="a"
                            flat
                            dense
                            no-caps
                            accept=".pdf, application/pdf"
                            class="q-pa-none"
                            color="info"
                            @click="scope.pickFiles"
                            label="busca un archivo"
                          >
                            <q-uploader-add-trigger
                          /></q-btn>
                        </q-item-label>
                      </q-item-section>
                    </q-item>
                  </template>
                </q-uploader>
              </q-item-label>
              <q-item-label class="q-mt-xs">
                <q-icon name="mdi-information" color="info" class="q-mr-sm" />
                <span class="text-grey-8"
                  >Sólo puedes subir archivos menores a 20 Mb en formato
                  PDF</span
                >
              </q-item-label>
            </q-item-section>
          </q-item>
          <q-item>
            <q-item-section>
              <q-item-label>
                Tema sobre el que versa la sentencia
              </q-item-label>
              <q-editor v-model="sintesis" style="height: 100px"></q-editor>
            </q-item-section>
          </q-item>
        </q-list>
      </q-step>
      <q-step :name="2" title="Estadística" prefix="2" :done="step > 2">
        <q-list>
          <q-item>
            <q-item-section>
              <q-item-label class="text-subtitle1 text-bold">
                Estadística
              </q-item-label>
            </q-item-section>
          </q-item>
          <q-item>
            <q-item-section>
              <q-item-label
                >¿El asunto se relaciona con delincuencia
                organizada?</q-item-label
              >
              <div class="q-gutter-sm">
                <q-radio v-model="delincuencia" val="si" label="Sí" />
                <q-radio v-model="delincuencia" val="no" label="No" />
              </div>
            </q-item-section>
          </q-item>
          <q-item>
            <q-item-section>
              <q-item-label
                >¿Se debe guardar sigilo o se actualiza un supuesto de reserva
                que restrinja la publicidad de todo el documento?</q-item-label
              >
              <div class="q-gutter-sm">
                <q-radio v-model="sigilo" val="si" label="Sí" />
                <q-radio v-model="sigilo" val="no" label="No" />
              </div>
              <div class="q-my-sm" v-if="sigilo === 'si'">
                <q-input
                  dense
                  v-model="textSigilo"
                  filled
                  type="textarea"
                  label="Justificación"
                />
              </div>
            </q-item-section>
          </q-item>
          <q-item>
            <q-item-section>
              <q-item-label
                >¿La sentencia fue dicatada por un órgano jurisdiccional
                auxiliar?</q-item-label
              >
              <div class="q-gutter-sm">
                <q-radio v-model="auxiliar" val="si" label="Sí" />
                <q-radio v-model="auxiliar" val="no" label="No" />
              </div>
            </q-item-section>
          </q-item>
          <q-item>
            <q-item-section>
              <q-item-label
                >La sentencia contiene información clasificada como confidencial
                (Art. 113 LFTAIP)?</q-item-label
              >
              <div class="q-gutter-sm">
                <q-radio v-model="confidencial" val="si" label="Sí" />
                <q-radio v-model="confidencial" val="no" label="No" />
              </div>
              <div class="q-my-sm" v-if="confidencial === 'si'">
                <div class="row q-my-sm">
                  <q-select
                    class="col"
                    dense
                    filled
                    label="Fracción"
                    v-model="fraccion113"
                    :options="options113"
                  ></q-select>
                </div>
                <div class="row q-gutter-sm">
                  <div class="col">
                    <q-input
                      dense
                      v-model="textMotivacion113"
                      filled
                      type="textarea"
                      label="Motivación de la clasificación"
                    />
                  </div>
                  <div class="col">
                    <q-input
                      dense
                      v-model="textObservaciones113"
                      filled
                      type="textarea"
                      label="Observaciones"
                    />
                  </div>
                </div>
              </div>
            </q-item-section>
          </q-item>
          <q-item>
            <q-item-section>
              <q-item-label
                >¿La sentencia contiene información clasificada como reservada
                (Art. 110 LFTAIP)?</q-item-label
              >
              <div class="q-gutter-sm">
                <q-radio v-model="reservada" val="si" label="Sí" />
                <q-radio v-model="reservada" val="no" label="No" />
              </div>
              <div class="q-my-sm" v-if="reservada === 'si'">
                <div class="row q-my-sm">
                  <q-select
                    class="col"
                    dense
                    filled
                    label="Fracción"
                    v-model="reservada110"
                    :options="options110"
                  ></q-select>
                </div>
                <div class="row q-gutter-sm">
                  <div class="col">
                    <q-input
                      dense
                      v-model="textMotivacion110"
                      filled
                      type="textarea"
                      label="Motivación de la clasificación"
                    />
                  </div>
                  <div class="col">
                    <q-input
                      dense
                      v-model="textObservaciones110"
                      filled
                      type="textarea"
                      label="Observaciones"
                    />
                  </div>
                </div>
                <div class="row q-my-sm">
                  <q-select
                    class="col"
                    dense
                    filled
                    label="Secretario que certifica"
                    v-model="secretario110"
                    :options="optionsSecretario110"
                  ></q-select>
                </div>
              </div>
            </q-item-section>
          </q-item>
          <q-item>
            <q-item-section>
              <q-item-label
                >¿Se elaboró bajo el formato de Lectura Fácil?</q-item-label
              >
              <div class="q-gutter-sm">
                <q-radio v-model="facil" val="si" label="Sí" />
                <q-radio v-model="facil" val="no" label="No" />
              </div>
            </q-item-section>
          </q-item>
          <q-item>
            <q-item-section>
              <q-item-label
                >Criterio novedoso o relevante (Acdos. 68/2004 y 69/2004 Sec.
                Ejec. Vigilancia)</q-item-label
              >
              <div class="q-gutter-sm">
                <q-radio v-model="novedoso" val="si" label="Sí" />
                <q-radio v-model="novedoso" val="no" label="No" />
              </div>
            </q-item-section>
          </q-item>
          <q-item>
            <q-item-section>
              <q-item-label
                >Asunto trascendental a la opinión pública (Acdos. 28/2007 -
                Comunicación Social)</q-item-label
              >
              <div class="q-gutter-sm">
                <q-radio v-model="opinion" val="si" label="Sí" />
                <q-radio v-model="opinion" val="no" label="No" />
              </div>
            </q-item-section>
          </q-item>
          <q-item>
            <q-item-section>
              <q-item-label
                >La sentencia o resolución se emitió conforme a un Tratado
                Internacional en materia de Derechos Humanos</q-item-label
              >
              <div class="q-gutter-sm">
                <q-radio v-model="dh" val="si" label="Sí" />
                <q-radio v-model="dh" val="no" label="No" />
              </div>
              <div class="row q-my-sm" v-if="dh === 'si'">
                <q-select
                  class="col"
                  dense
                  filled
                  label="Tratado Internacional que se utilizó para resolver"
                  v-model="dhTratado"
                  :options="optionsTratado"
                ></q-select>
              </div>
            </q-item-section>
          </q-item>
          <q-item>
            <q-item-section>
              <q-item-label
                >Se aplicaron criterios de perspectiva de género</q-item-label
              >
              <div class="q-gutter-sm">
                <q-radio v-model="genero" val="si" label="Sí" />
                <q-radio v-model="genero" val="no" label="No" />
              </div>
              <div class="row q-my-sm" v-if="genero === 'si'">
                <q-input
                  class="col"
                  dense
                  v-model="textGenero"
                  filled
                  type="textarea"
                  label="Criterio de perspectiva de género aplicado"
                />
              </div>
              <div class="row q-mb-sm q-gutter-sm" v-if="genero === 'si'">
                <q-select
                  class="col"
                  dense
                  filled
                  label="Derechos Humanos Fundamentales Analizados"
                  v-model="dhAnalizados"
                  :options="optionsAnalizados"
                ></q-select>
                <q-select
                  class="col"
                  dense
                  filled
                  label="Derechos Humanos Fundamentales Analizados Específico"
                  v-model="dhAnalizadosEspecificos"
                  :options="optionsAnalizadoEspecificos"
                ></q-select>
              </div>
            </q-item-section>
          </q-item>
          <q-item>
            <q-item-section>
              <q-item-label
                >¿Esta sentencia se emitió con aplicación efectiva de un
                ordenamiento internacional y/o nacional de protección a los
                derechos de las mujeres a la igualdad y no
                discriminación?</q-item-label
              >
              <div class="q-gutter-sm">
                <q-radio v-model="mujeres" val="si" label="Sí" />
                <q-radio v-model="mujeres" val="no" label="No" />
              </div>
              <div class="row q-my-sm" v-if="mujeres === 'si'">
                <q-select
                  class="col"
                  dense
                  filled
                  label="¿La sentencia versó sobre alguno de los asuntos internacionales siguientes?"
                  v-model="mujeresInternacionales"
                  :options="optionsMujeresInternacionales"
                ></q-select>
              </div>
              <div class="row" v-if="mujeres === 'si'">
                <q-input
                  class="col"
                  dense
                  v-model="textMujeres"
                  filled
                  type="textarea"
                  label="Escriba brevemente el tema del asunto (Síntesis)"
                />
              </div>
            </q-item-section>
          </q-item>
          <q-item>
            <q-item-section>
              <q-item-label
                >Hubo solicitud de reparación del daño o fue decretada en la
                sentencia</q-item-label
              >
              <div class="q-gutter-sm">
                <q-radio v-model="reparacion" val="si" label="Sí" />
                <q-radio v-model="reparacion" val="no" label="No" />
              </div>
              <div class="row q-my-sm" v-if="reparacion === 'si'">
                <q-select
                  class="col"
                  dense
                  filled
                  label="Selecciona una opción"
                  v-model="reparacionSeleccionada"
                  :options="optionsReparacion"
                ></q-select>
              </div>
            </q-item-section>
          </q-item>
        </q-list>
      </q-step>
      <q-step :name="3" title="Partes" prefix="3" :done="step > 3">
        <q-list>
          <q-item>
            <q-item-section>
              <q-item-label class="text-subtitle1 text-bold">
                Partes
              </q-item-label>
            </q-item-section>
          </q-item>
          <q-item>
            <q-item-section>
              <q-item-label>Cúmplase</q-item-label>
              <div class="q-gutter-sm">
                <q-radio v-model="cumplase" val="si" label="Sí" />
                <q-radio v-model="cumplase" val="no" label="No" />
              </div>
            </q-item-section>
          </q-item>
          <q-item v-if="cumplase === 'no'">
            <q-item-section>
              <q-item-label>
                Indica la forma de cómo serán notificadas las partes. Se
                generará automáticamente el oficio de las autoridades. O si lo
                prefieres crea libremente tu oficio.
              </q-item-label>
            </q-item-section>
          </q-item>
          <q-card flat v-if="cumplase === 'no'">
            <q-toolbar>
              <q-space></q-space>
              <q-input
                dense
                outlined
                rounded
                label="Buscar autoridades"
                class="q-pl-xl"
                color="primary"
                v-model="filter"
                :pagination="initialPagination"
              >
                <template v-slot:append>
                  <q-icon name="search" />
                </template>
              </q-input>
            </q-toolbar>
            <q-card-section>
              <q-table
                flat
                dense
                bordered
                :filter="filter"
                :rows="rows"
                :columns="columns"
                row-key="autoridadJudicialId"
                :pagination="initialPagination"
              >
                <template v-slot:body="props">
                  <q-tr>
                    <q-td>
                      <q-item class="text-left">
                        <q-item-section>
                          <q-item-label>
                            {{ props.row.Parte || "" }}
                          </q-item-label>
                          <q-item-label class="text-secondary" caption>
                            {{ props.row.Caracter }}
                          </q-item-label>
                        </q-item-section>
                      </q-item>
                    </q-td>
                    <q-td>
                      <q-item>
                        <q-radio name="noty" v-model="props.row.noty" val="1" />
                      </q-item>
                    </q-td>
                    <q-td>
                      <q-item class="text-center">
                        <q-radio name="noty" v-model="props.row.noty" val="2" />
                      </q-item>
                    </q-td>
                    <q-td>
                      <q-item class="text-center">
                        <q-radio name="noty" v-model="props.row.noty" val="3" />
                      </q-item>
                    </q-td>
                    <q-td>
                      <q-item>
                        <q-radio name="noty" v-model="props.row.noty" val="4" />
                      </q-item>
                    </q-td>
                    <q-td>
                      <q-item class="text-center">
                        <q-radio name="noty" v-model="props.row.noty" val="5" />
                      </q-item>
                    </q-td>
                    <q-td>
                      <q-item class="text-center">
                        <q-item-section>
                          <q-radio
                            name="noty"
                            v-model="props.row.noty"
                            val="7"
                          />
                        </q-item-section>
                        <q-item-section side>
                          <q-item v-if="props.row.noty === '6'">
                            <q-icon
                              class="q-pr-xs cursor-pointer"
                              size="1.7em"
                              color="secondary"
                              name="mdi-file-document-edit-outline"
                              @click="
                                parteSelected = props.row;
                                showOficioLibre = true;
                              "
                            >
                              <q-tooltip>Editar</q-tooltip>
                            </q-icon>
                            <q-icon
                              class="cursor-pointer"
                              size="1.7em"
                              color="red-6"
                              name="mdi-delete"
                            >
                              <q-tooltip>Borrar oficio libre</q-tooltip>
                            </q-icon>
                          </q-item>
                        </q-item-section>
                      </q-item>
                    </q-td>
                  </q-tr>
                </template>
              </q-table>
            </q-card-section>
          </q-card>
        </q-list>
      </q-step>
      <template v-slot:navigation>
        <q-stepper-navigation class="q-gutter-md">
          <q-btn
            no-caps
            @click="$refs.stepper.next()"
            color="primary"
            :label="step === 4 ? 'Subir' : 'Continuar'"
            style="width: 120px"
          />
          <q-btn
            v-if="step > 1"
            flat
            no-caps
            color="primary"
            @click="$refs.stepper.previous()"
            label="Regresar"
            style="width: 120px"
          />
        </q-stepper-navigation>
      </template>
    </q-stepper>
    <!-- <q-card-actions class="q-gutter-xl q-px-lg">
      <q-btn
        class="col"
        unelevated
        no-caps
        color="primary"
        label="Subir"
        v-close-popup
        @click="noty.correcto('Acuse subido correctamente.')"
      ></q-btn>
      <q-btn class="col" outline no-caps label="Cancelar" v-close-popup></q-btn>
    </q-card-actions> -->
  </q-card>

  <q-diaglog>
    <q-card>
      <q-card-section> </q-card-section>
    </q-card>
  </q-diaglog>
</template>

<script setup>
import { ref } from "vue";
// import { noty } from "src/helpers/notify";

const tipoSentenciaSelected = ref(null);
const tipoSentidoSelected = ref(null);
const step = ref(1);
const delincuencia = ref("no");
const sigilo = ref("no");
const auxiliar = ref("no");
const confidencial = ref("no");
const reservada = ref("no");
const facil = ref("no");
const novedoso = ref("no");
const opinion = ref("no");
const dh = ref("no");
const genero = ref("no");
const mujeres = ref("no");
const reparacion = ref("no");
const cumplase = ref("no");
const textSigilo = ref("");
const fraccion113 = ref("");
const textMotivacion113 = ref("");
const textObservaciones113 = ref("");
const textMotivacion110 = ref("");
const textObservaciones110 = ref("");
const reservada110 = ref("");
const secretario110 = ref("");
const dhTratado = ref("");
const textGenero = ref("");
const dhAnalizados = ref("");
const dhAnalizadosEspecificos = ref("");
const mujeresInternacionales = ref("");
const textMujeres = ref("");
const reparacionSeleccionada = ref("");
const sintesis = ref("");

const optionsReparacion = [
  "Restitución de bienes",
  "En su caso, pago del bien o bienes",
  "Entrega de objeto igual",
  "Pago de salarios o percepciones",
];
const optionsMujeresInternacionales = [
  "Consignaciones sobre Desaparición Forzada de Personas",
  "Consignaciones sobre Detención Arbitraria o ilegal como parte del delito de abuso de autoridad",
  "Consignación sobre Trata de Personas",
  "Sentencia sobre Desaparición Forzada de Personas",
];
const optionsAnalizados = [
  "Derechos de la persona",
  "Derechos económicos, sociales y culturales",
  "Protección de grupos específicos",
  "Derechos de los pueblos indígenas",
];
const optionsAnalizadoEspecificos = [
  "Acceso a la información pública",
  "Asilo político",
  "Igualdad ante la ley",
  "Igualdad de género",
];
const optionsTratado = [
  "Carta de la organización de los estados americanos",
  "Carta de las naciones unidas",
  "Código sanitario panamericano",
  "Convención americana sobre derechos humanos",
];
const options110 = [
  "Fracción I: Comprometa la seguridad nacional, la seguridad pública o la defensa nacional",
  "Fracción II: Puede menoscabar la conducción de las negociaciones y relaciones internancionales",
  "Fracción III: Se entregó al Estado mexicano expresamente con ese carácter o el de confidencialidad...",
  "Fracción IV: Afecta la efectividad de las medidas adoptadas en relación con las políticas en ...",
];
const options113 = [
  "Fracción I: Datos personales concernientes a una persona física identificada o identificable",
  "Fracción II: Secretos bancarios, fiduciarios, comerciales, fiscal bursátil y postal cuando ...",
  "Fracción III: La presentada por los particulares como confidencial siemmpre que tenga el derecho...",
];
const optionsSecretario110 = ["Pedro Rosas Pérez"];
const optionsTipoSentencia = [
  { label: "Sentencia definitiva", value: "Sentencia definitiva" },
  { label: "Resolución", value: "Resolución" },
  { label: "Aclaración de sentencia", value: "Aclaración de sentencia" },
  { label: "Interlocutoria", value: "Interlocutoria" },
];
const optionsTipoSentido = [
  { label: "Ampara", value: "Ampara" },
  { label: "No ampara", value: "No ampara" },
  { label: "Sobresee", value: "Sobresee" },
  { label: "Otro", value: "Otro" },
];
const rows = ref([
  {
    Parte: "Tribunal Superior Interconexión - OIJ Puebla",
    Caracter: "AUTORIDAD RESPONSABLE",
  },
  {
    Parte: "Subprocuraduría Fiscal Federal de Amparos",
    Caracter: "AUTORIDAD RESPONSABLE",
  },
  {
    Parte:
      "Director General de Investigación de la Comisión Federal de Competencia",
    Caracter: "AUTORIDAD RESPONSABLE",
  },
  {
    Parte:
      "COMPAÑIA EMBOTELLADORA DE SINALOA, SOCIEDAD ANÓNIMA DE CAPITAL VARIABLE",
    Caracter: "QUEJOSO",
  },
  {
    Parte: "LA VICTORIA, SOCIEDAD ANÓNIMA DE CAPITAL VARIABLE",
    Caracter: "QUEJOSO",
  },
  {
    Parte: "CARMEN ZAMORA JUÁREZ",
    Caracter: "QUEJOSO",
  },
]);
const columns = [
  {
    name: "oficios",
    label: "",
    align: "left",
    field: (row) => `${row.nombres} ${row.descripcionCaracterPersona}`,
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
    align: "left",
    sortable: false,
  },
  {
    name: "Electrónica",
    label: "Electrónica",
    align: "left",
    sortable: false,
  },
  {
    name: "C.O.E.",
    label: "C.O.E.",
    align: "left",
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
    align: "left",
    sortable: false,
  },
];

const initialPagination = {
  rowsPerPage: 0,
};

defineProps({
  item: {
    type: Object,
    required: true,
  },
});
</script>
