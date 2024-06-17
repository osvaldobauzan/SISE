<template>
  <q-card flat class="bg-blue-grey-1">
    <q-toolbar class="bg-white">
      <q-toolbar-title>
        Sesión {{ date.formatDate(fechaSesion, "DD/MM/YYYY") }}
      </q-toolbar-title>
      <q-item>
        <q-item-section>
          <q-item-label>
            Proyectos listados <q-chip outline>{{ listados }}</q-chip>
          </q-item-label>
        </q-item-section>
      </q-item>
      <q-space></q-space>
      <q-btn flat round icon="mdi-close" v-close-popup></q-btn>
    </q-toolbar>
    <q-separator></q-separator>
    <q-card-section class="q-gutter-md">
      <q-list bordered class="rounded-borders">
        <q-expansion-item
          default-opened
          expand-separator
          icon="perm_identity"
          label="José Alfonso Montalvo Martínez"
          caption="Ponencia 1"
          header-class="bg-indigo-2"
        >
          <q-separator></q-separator>
          <q-card>
            <q-toolbar class="q-gutter-sm" v-if="!publicada">
              <q-space></q-space>
              <q-btn-dropdown
                no-caps
                color="primary"
                label="Agregar proyecto"
                icon="mdi-plus"
              >
                <q-list>
                  <q-item
                    clickable
                    v-close-popup
                    @click="showAgregarLista = true"
                  >
                    <q-item-section>
                      <q-item-label>Aprobado</q-item-label>
                    </q-item-section>
                  </q-item>

                  <q-item
                    clickable
                    v-close-popup
                    @click="showAgregarLista = true"
                  >
                    <q-item-section>
                      <q-item-label>Aún no aprobado</q-item-label>
                    </q-item-section>
                  </q-item>
                </q-list>
              </q-btn-dropdown>
              <q-btn
                v-if="false"
                dense
                no-caps
                icon="mdi-plus"
                label="Agregar"
                class="q-px-lg"
                @click="showAgregarLista = true"
                color="primary"
              />
              <q-btn
                dense
                no-caps
                outline
                icon="mdi-file-sign"
                label="Firmar"
                class="q-px-lg"
                @click="showFirmar = true"
                color="primary"
              />
            </q-toolbar>
            <q-toolbar class="q-gutter-sm" v-else>
              <q-space></q-space>
              <q-btn
                dense
                no-caps
                icon="mdi-printer"
                label="Imprimir lista"
                class="q-px-lg"
                @click="showImprimirListaPonencia = true"
                color="primary"
              />
            </q-toolbar>
            <q-separator></q-separator>
            <q-table
              flat
              dense
              wrap-cells
              :rows="rows"
              :columns="columns"
              row-key="asuntoNeunId"
            >
              <template v-slot:body="props">
                <q-tr>
                  <q-td style="width: 200px">
                    <q-item
                      v-ripple
                      clickable
                      class="q-pa-none"
                      @click="
                        selectedItem = props.row;
                        maximizedToggle = false;
                        expedientes.push(props.row);
                        showExpediente = true;
                      "
                    >
                      <q-item-section avatar>
                        <q-avatar
                          icon="mdi-book-open-variant-outline"
                          :text-color="`${getBookColor(
                            props.row.catTipoAsunto,
                            props.row.cuaderno,
                          )} `"
                          :color="`${getBookColor(
                            props.row.catTipoAsunto,
                            props.row.cuaderno,
                          )} `"
                        ></q-avatar>
                      </q-item-section>
                      <q-item-section>
                        <q-item-label>
                          {{ props.row.asuntoAlias }}
                        </q-item-label>
                        <q-item-label caption>
                          {{ props.row.catTipoAsunto }}
                        </q-item-label>
                        <q-item-label>
                          <q-badge
                            :class="`bg-${getBookColor(
                              props.row.catTipoAsunto,
                              props.row.cuaderno,
                            )} text-${getBookColor(
                              props.row.catTipoAsunto,
                              props.row.cuaderno,
                            )}`"
                            :label="props.row.cuaderno"
                          >
                          </q-badge>
                        </q-item-label>
                      </q-item-section>
                    </q-item>
                  </q-td>
                  <q-td>
                    <q-btn
                      flat
                      stack
                      no-caps
                      color="primary"
                      icon="mdi-paperclip"
                      :label="
                        date.formatDate(props.row.fechaProyecto, 'DD/MM/YYYY')
                      "
                    ></q-btn>
                  </q-td>
                  <q-td>
                    <q-item class="q-pl-none">
                      <q-item-section>
                        <q-item-label>
                          {{ props.row.usuarioSecretario }}
                        </q-item-label>
                        <q-item-label caption>
                          {{ props.row.mesa }}
                        </q-item-label>
                      </q-item-section>
                    </q-item>
                  </q-td>
                  <q-td>
                    <q-item class="q-pl-none">
                      <q-item-section>
                        <q-item-label>
                          {{ props.row.parte }}
                        </q-item-label>
                        <q-item-label caption>
                          {{ props.row.caracterParte }}
                        </q-item-label>
                        <q-item-label caption>
                          <q-label v-if="props.row.identidadReservada === 'Sí'"
                            >Identidad reservada</q-label
                          >
                          <q-label v-else
                            >Sin oposición de datos personales</q-label
                          >
                        </q-item-label>
                      </q-item-section>
                    </q-item>
                  </q-td>
                  <q-td>
                    <q-item class="q-pl-none">
                      <q-item-section>
                        <q-item-label>
                          {{ props.row.autoridad }}
                        </q-item-label>
                        <q-item-label caption>
                          +{{ props.row.totalAutoridades }} más
                        </q-item-label>
                      </q-item-section>
                    </q-item>
                  </q-td>
                  <q-td>
                    {{ props.row.materia }}
                  </q-td>
                  <q-td> {{ props.row.tema.slice(0, 100) }}... </q-td>
                  <q-td>
                    <q-item v-if="props.row.primeraVez === 'Sí'">
                      <q-item-section>
                        <q-item-label>
                          {{ props.row.primeraVez }}
                        </q-item-label>
                        <q-item-label caption>
                          {{
                            props.row.primeraVez === "Sí" ? "" : "Previamente"
                          }}
                        </q-item-label>
                      </q-item-section>
                    </q-item>
                  </q-td>
                  <q-td>
                    <div v-if="props.rowIndex === 0">
                      <q-btn-dropdown
                        no-caps
                        dense
                        outline
                        color="primary"
                        icon="mdi-plus"
                        label="Agregar sentido"
                      >
                        <q-list dense>
                          <q-item
                            clickable
                            v-ripple
                            v-close-popup
                            @click="listSentidos1.push('Ampara')"
                          >
                            <q-item-section> Ampara </q-item-section>
                          </q-item>
                          <q-item
                            clickable
                            v-ripple
                            v-close-popup
                            @click="listSentidos1.push('No Ampara')"
                          >
                            <q-item-section>
                              <q-item-label> No ampara </q-item-label>
                            </q-item-section>
                          </q-item>
                          <q-item
                            clickable
                            v-ripple
                            v-close-popup
                            @click="listSentidos1.push('Sobresee')"
                          >
                            <q-item-section>
                              <q-item-label> Sobresee </q-item-label>
                            </q-item-section>
                          </q-item>
                        </q-list>
                      </q-btn-dropdown>
                      <q-list dense>
                        <q-item
                          v-for="(sentido, index) in listSentidos1"
                          :key="sentido"
                        >
                          <q-chip square>
                            <q-avatar class="text-bold">{{
                              index + 1
                            }}</q-avatar>
                            {{ sentido }}
                            &nbsp;
                            <q-btn
                              flat
                              round
                              size="sm"
                              icon="mdi-close"
                              color="red"
                              @click="listSentidos1.splice(index, 1)"
                            ></q-btn>
                          </q-chip>
                        </q-item>
                      </q-list>
                    </div>
                    <div v-if="props.rowIndex === 1">
                      <q-btn-dropdown
                        no-caps
                        dense
                        outline
                        color="primary"
                        icon="mdi-plus"
                        label="Agregar sentido"
                      >
                        <q-list dense>
                          <q-item
                            clickable
                            v-ripple
                            v-close-popup
                            @click="listSentidos2.push('Ampara')"
                          >
                            <q-item-section> Ampara </q-item-section>
                          </q-item>
                          <q-item
                            clickable
                            v-ripple
                            v-close-popup
                            @click="listSentidos2.push('No Ampara')"
                          >
                            <q-item-section>
                              <q-item-label> No ampara </q-item-label>
                            </q-item-section>
                          </q-item>
                          <q-item
                            clickable
                            v-ripple
                            v-close-popup
                            @click="listSentidos2.push('Sobresee')"
                          >
                            <q-item-section>
                              <q-item-label> Sobresee </q-item-label>
                            </q-item-section>
                          </q-item>
                        </q-list>
                      </q-btn-dropdown>
                      <q-list dense>
                        <q-item
                          v-for="(sentido, index) in listSentidos2"
                          :key="sentido"
                        >
                          <q-chip square>
                            <q-avatar class="text-bold">{{
                              index + 1
                            }}</q-avatar>
                            {{ sentido }}
                            &nbsp;
                            <q-btn
                              flat
                              round
                              size="sm"
                              icon="mdi-close"
                              color="red"
                              @click="listSentidos2.splice(index, 1)"
                            ></q-btn>
                          </q-chip>
                        </q-item>
                      </q-list>
                    </div>
                    <div v-if="props.rowIndex === 2">
                      <q-btn-dropdown
                        no-caps
                        dense
                        outline
                        color="primary"
                        icon="mdi-plus"
                        label="Agregar sentido"
                      >
                        <q-list dense>
                          <q-item
                            clickable
                            v-ripple
                            v-close-popup
                            @click="listSentidos3.push('Ampara')"
                          >
                            <q-item-section> Ampara </q-item-section>
                          </q-item>
                          <q-item
                            clickable
                            v-ripple
                            v-close-popup
                            @click="listSentidos3.push('No Ampara')"
                          >
                            <q-item-section>
                              <q-item-label> No ampara </q-item-label>
                            </q-item-section>
                          </q-item>
                          <q-item
                            clickable
                            v-ripple
                            v-close-popup
                            @click="listSentidos3.push('Sobresee')"
                          >
                            <q-item-section>
                              <q-item-label> Sobresee </q-item-label>
                            </q-item-section>
                          </q-item>
                        </q-list>
                      </q-btn-dropdown>
                      <q-list dense>
                        <q-item
                          v-for="(sentido, index) in listSentidos3"
                          :key="sentido"
                        >
                          <q-chip square>
                            <q-avatar class="text-bold">{{
                              index + 1
                            }}</q-avatar>
                            {{ sentido }}
                            &nbsp;
                            <q-btn
                              flat
                              round
                              size="sm"
                              icon="mdi-close"
                              color="red"
                              @click="listSentidos3.splice(index, 1)"
                            ></q-btn>
                          </q-chip>
                        </q-item>
                      </q-list>
                    </div>

                    <!-- <q-item class="q-pl-none" v-if="publicada">
                      <q-item-section>
                        <q-item-label>
                          {{ props.row.sentido }}
                        </q-item-label>
                        <q-item-label caption>
                          {{ props.row.resolucion }}
                        </q-item-label>
                      </q-item-section>
                    </q-item> -->
                  </q-td>
                  <q-td>
                    <q-btn
                      flat
                      round
                      dense
                      icon="mdi-dots-vertical"
                      color="grey-9"
                      v-if="!publicada"
                    >
                      <q-menu auto-close>
                        <q-list>
                          <q-item
                            clickable
                            v-ripple
                            @click="showResolucion = true"
                          >
                            <q-item-section side
                              ><q-icon name="mdi-account-group-outline"></q-icon
                            ></q-item-section>
                            <q-item-section>Resultado de sesión</q-item-section>
                          </q-item>
                          <!-- <q-item clickable v-ripple>
                        <q-item-section side><q-icon name="mdi-eye"></q-icon></q-item-section>
                        <q-item-section>Ver detalle</q-item-section>
                      </q-item> -->
                          <q-item
                            clickable
                            v-ripple
                            @click="quitarDeLista(props.row)"
                          >
                            <q-item-section side
                              ><q-icon
                                name="mdi-close"
                                color="negative"
                              ></q-icon
                            ></q-item-section>
                            <q-item-section
                              ><q-item-label class="text-negative"
                                >Quitar de la lista</q-item-label
                              ></q-item-section
                            >
                          </q-item>
                        </q-list>
                      </q-menu>
                    </q-btn>
                  </q-td>
                </q-tr>
              </template>
            </q-table>
          </q-card>
        </q-expansion-item>
      </q-list>
      <q-list bordered class="rounded-borders">
        <q-expansion-item
          default-opened
          expand-separator
          icon="perm_identity"
          label="Omar Alonso Ortiz Sánchez"
          caption="Ponencia 2"
        >
          <q-card>
            <q-separator></q-separator>
            <q-table
              wrap-cells
              flat
              dense
              :rows="rows"
              :columns="columns"
              row-key="asuntoAlias"
            >
              <template v-slot:body="props">
                <q-tr>
                  <q-td>
                    <q-item class="q-pl-none">
                      <q-item-section>
                        <q-item-label>
                          {{ props.row.asuntoAlias }}
                        </q-item-label>
                        <q-item-label caption>
                          {{ props.row.catTipoAsunto }}
                        </q-item-label>
                      </q-item-section>
                    </q-item>
                  </q-td>
                  <q-td>
                    <q-btn
                      flat
                      stack
                      no-caps
                      color="primary"
                      icon="mdi-paperclip"
                      :label="
                        date.formatDate(props.row.fechaProyecto, 'DD/MM/YYYY')
                      "
                    ></q-btn>
                  </q-td>
                  <q-td>
                    <q-item class="q-pl-none">
                      <q-item-section>
                        <q-item-label>
                          {{ props.row.usuarioSecretario }}
                        </q-item-label>
                        <q-item-label caption>
                          {{ props.row.mesa }}
                        </q-item-label>
                      </q-item-section>
                    </q-item>
                  </q-td>
                  <q-td>
                    <q-item class="q-pl-none">
                      <q-item-section>
                        <q-item-label>
                          {{ props.row.parte }}
                        </q-item-label>
                        <q-item-label caption>
                          {{ props.row.caracterParte }}
                        </q-item-label>
                        <q-item-label caption>
                          <q-label v-if="props.row.identidadReservada === 'Sí'"
                            >Identidad reservada</q-label
                          >
                          <q-label v-else
                            >Sin oposición de datos personales</q-label
                          >
                        </q-item-label>
                      </q-item-section>
                    </q-item>
                  </q-td>
                  <q-td>
                    <q-item class="q-pl-none">
                      <q-item-section>
                        <q-item-label>
                          {{ props.row.autoridad }}
                        </q-item-label>
                        <q-item-label caption>
                          +{{ props.row.totalAutoridades }} más
                        </q-item-label>
                      </q-item-section>
                    </q-item>
                  </q-td>
                  <q-td>
                    {{ props.row.materia }}
                  </q-td>
                  <q-td> {{ props.row.tema.slice(0, 100) }}... </q-td>
                  <q-td>
                    <q-item v-if="props.row.primeraVez === 'Sí'">
                      <q-item-section>
                        <q-item-label>
                          {{ props.row.primeraVez }}
                        </q-item-label>
                      </q-item-section>
                    </q-item>
                    <q-item v-else>
                      <q-item-section>
                        <q-item-label caption>
                          Previamente listado
                        </q-item-label>
                        <q-item-label>
                          {{ props.row.primeraVez }}
                        </q-item-label>
                      </q-item-section>
                    </q-item>
                  </q-td>

                  <q-td>
                    <q-item class="q-pl-none" v-if="publicada">
                      <q-item-section>
                        <q-item-label>
                          {{ props.row.sentido }}
                        </q-item-label>
                        <q-item-label caption>
                          {{ props.row.resolucion }}
                        </q-item-label>
                      </q-item-section>
                    </q-item>
                  </q-td>
                  <q-td>
                    <q-btn
                      flat
                      round
                      dense
                      icon="mdi-dots-vertical"
                      color="grey-9"
                    >
                      <q-menu auto-close>
                        <q-list>
                          <q-item
                            clickable
                            v-ripple
                            @click="showResolucion = true"
                          >
                            <q-item-section side
                              ><q-icon name="mdi-check"></q-icon
                            ></q-item-section>
                            <q-item-section>Resolución</q-item-section>
                          </q-item>
                          <!-- <q-item clickable v-ripple>
                        <q-item-section side><q-icon name="mdi-eye"></q-icon></q-item-section>
                        <q-item-section>Ver detalle</q-item-section>
                      </q-item> -->
                          <q-item clickable v-ripple>
                            <q-item-section side
                              ><q-icon
                                name="mdi-close"
                                color="negative"
                              ></q-icon
                            ></q-item-section>
                            <q-item-section
                              ><q-item-label class="text-negative"
                                >Quitar de la lista</q-item-label
                              ></q-item-section
                            >
                          </q-item>
                        </q-list>
                      </q-menu>
                    </q-btn>
                  </q-td>
                </q-tr>
              </template>
            </q-table>
          </q-card>
        </q-expansion-item>
      </q-list>
      <q-list bordered class="rounded-borders">
        <q-expansion-item
          default-opened
          expand-separator
          icon="perm_identity"
          label="Ricardo Alfonso Dorantes"
          caption="Ponencia 3"
        >
          <q-separator></q-separator>
          <q-card>
            <q-table
              wrap-cells
              flat
              dense
              :rows="rows"
              :columns="columns"
              row-key="asuntoAlias"
            >
              <template v-slot:body="props">
                <q-tr>
                  <q-td>
                    <q-item class="q-pl-none">
                      <q-item-section>
                        <q-item-label>
                          {{ props.row.asuntoAlias }}
                        </q-item-label>
                        <q-item-label caption>
                          {{ props.row.catTipoAsunto }}
                        </q-item-label>
                      </q-item-section>
                    </q-item>
                  </q-td>
                  <q-td>
                    <q-btn
                      flat
                      stack
                      no-caps
                      color="primary"
                      icon="mdi-paperclip"
                      :label="
                        date.formatDate(props.row.fechaProyecto, 'DD/MM/YYYY')
                      "
                    ></q-btn>
                  </q-td>
                  <q-td>
                    <q-item class="q-pl-none">
                      <q-item-section>
                        <q-item-label>
                          {{ props.row.usuarioSecretario }}
                        </q-item-label>
                        <q-item-label caption>
                          {{ props.row.mesa }}
                        </q-item-label>
                      </q-item-section>
                    </q-item>
                  </q-td>
                  <q-td>
                    <q-item class="q-pl-none">
                      <q-item-section>
                        <q-item-label>
                          {{ props.row.parte }}
                        </q-item-label>
                        <q-item-label caption>
                          {{ props.row.caracterParte }}
                        </q-item-label>
                        <q-item-label caption>
                          <q-label v-if="props.row.identidadReservada === 'Sí'"
                            >Identidad reservada</q-label
                          >
                          <q-label v-else
                            >Sin oposición de datos personales</q-label
                          >
                        </q-item-label>
                      </q-item-section>
                    </q-item>
                  </q-td>
                  <q-td>
                    <q-item class="q-pl-none">
                      <q-item-section>
                        <q-item-label>
                          {{ props.row.autoridad }}
                        </q-item-label>
                        <q-item-label caption>
                          +{{ props.row.totalAutoridades }} más
                        </q-item-label>
                      </q-item-section>
                    </q-item>
                  </q-td>
                  <q-td>
                    {{ props.row.materia }}
                  </q-td>
                  <q-td> {{ props.row.tema.slice(0, 100) }}... </q-td>
                  <q-td>
                    <q-item v-if="props.row.primeraVez === 'Sí'">
                      <q-item-section>
                        <q-item-label>
                          {{ props.row.primeraVez }}
                        </q-item-label>
                      </q-item-section>
                    </q-item>
                    <q-item v-else>
                      <q-item-section>
                        <q-item-label caption>
                          Previamente listado
                        </q-item-label>
                        <q-item-label>
                          {{ props.row.primeraVez }}
                        </q-item-label>
                      </q-item-section>
                    </q-item>
                  </q-td>

                  <q-td>
                    <q-item class="q-pl-none" v-if="publicada">
                      <q-item-section>
                        <q-item-label>
                          {{ props.row.sentido }}
                        </q-item-label>
                        <q-item-label caption>
                          {{ props.row.resolucion }}
                        </q-item-label>
                      </q-item-section>
                    </q-item>
                  </q-td>
                  <q-td>
                    <q-btn
                      flat
                      round
                      dense
                      icon="mdi-dots-vertical"
                      color="grey-9"
                    >
                      <q-menu auto-close>
                        <q-list>
                          <q-item
                            clickable
                            v-ripple
                            @click="showResolucion = true"
                          >
                            <q-item-section side
                              ><q-icon name="mdi-check"></q-icon
                            ></q-item-section>
                            <q-item-section>Resolución</q-item-section>
                          </q-item>
                          <!-- <q-item clickable v-ripple>
                        <q-item-section side><q-icon name="mdi-eye"></q-icon></q-item-section>
                        <q-item-section>Ver detalle</q-item-section>
                      </q-item> -->
                          <q-item clickable v-ripple>
                            <q-item-section side
                              ><q-icon
                                name="mdi-close"
                                color="negative"
                              ></q-icon
                            ></q-item-section>
                            <q-item-section
                              ><q-item-label class="text-negative"
                                >Quitar de la lista</q-item-label
                              ></q-item-section
                            >
                          </q-item>
                        </q-list>
                      </q-menu>
                    </q-btn>
                  </q-td>
                </q-tr>
              </template>
            </q-table>
          </q-card>
        </q-expansion-item>
      </q-list>
    </q-card-section>
  </q-card>
  <q-dialog v-model="showAgregarLista" full-width class="q-ma-xl">
    <AgregarProyectoComponent></AgregarProyectoComponent>
  </q-dialog>
  <q-dialog v-model="showListar" full-width>
    <PublicarListaComponent></PublicarListaComponent>
  </q-dialog>
  <q-dialog v-model="showResolucion">
    <ResolucionComponent></ResolucionComponent>
  </q-dialog>
  <q-dialog v-model="showImprimirListaPonencia" full-height full-width>
    <ViewPdfComponent :nombreArchivo="nombreArchivo" titulo="Lista de sesión" />
  </q-dialog>
</template>

<script setup>
import { ref } from "vue";
import { catTipoAsunto } from "src/data/catalogos";
import { date, Notify, useQuasar } from "quasar";

import PublicarListaComponent from "../components/PublicarListaComponent.vue";
import AgregarProyectoComponent from "../components/AgregarProyectoComponent.vue";
import ResolucionComponent from "../components/ResolucionComponent.vue";
import ViewPdfComponent from "src/components/ViewPdfComponent.vue";
import listaRasd from "../docs/Sesion_29-02-24.pdf";

const showImprimirListaPonencia = ref(false);
const showAgregarLista = ref(false);
const showListar = ref(false);
const showFirmar = ref(false);
const showResolucion = ref(false);
const nombreArchivo = ref(listaRasd);
const listSentidos1 = ref([]);
const listSentidos2 = ref([]);
const listSentidos3 = ref([]);
const maximizedToggle = ref(false);
const expedientes = ref([]);
const showExpediente = ref(false);
const $q = useQuasar();

function quitarDeLista() {
  $q.dialog({
    title: "Eliminar de la lista",
    message: "¿Está seguro de eliminar el elemento de la lista?",
    cancel: true,
  }).onOk(() => {
    Notify.create({
      message: "Proyecto eliminado de la lista",
      color: "positive",
      position: "top-right",
      icon: "mdi-check",
    });
  });
}

defineProps({
  fechaSesion: String,
  listados: Number,
  publicada: Boolean,
});

const getBookColor = (ta, nc) =>
  catTipoAsunto.find(
    (t) =>
      t.name?.toLowerCase() === ta?.toLowerCase() &&
      t.book?.toLowerCase() === nc?.toLowerCase(),
  )?.shortName || "empty";

const rows = [
  {
    asuntoNeunId: 23070603,
    asuntoAlias: "12/2024",
    catTipoAsunto: "Amparo indirecto",
    cuaderno: "Principal",
    fechaProyecto: "2024-01-12T19:18:00.777",
    usuarioPonencia: "José Luis Vargas Valdez",
    ponencia: "Ponencia 1",
    usuarioSecretario: "oalvarez",
    mesa: "Mesa 1",
    parte: "Pedro Pérez López",
    caracterParte: "Quejoso",
    autoridad: "Fiscalía General de la República",
    totalAutoridades: 10,
    materia: "Penal",
    tema: "Se otorga el amparo al quejoso frente a actos de molestia por parte de autoridades fiscales que realizaron inspecciones y requerimientos de información sin cumplir con los requisitos legales necesarios, afectando el derecho de seguridad jurídica y privacidad del contribuyente.",
    sentido: "Sentido",
    resolucion: "Ampara",
    primeraVez: "Sí",
    identidadReservada: "Sí",
  },
  {
    asuntoNeunId: 30301689,
    asuntoAlias: "23/2024",
    catTipoAsunto: "Amparo indirecto",
    cuaderno: "Principal",
    fechaProyecto: "2024-02-14T19:18:00.777",
    usuarioPonencia: "Alfredo Fuentes Barrera",
    ponencia: "Ponencia 2",
    usuarioSecretario: "jperaltar",
    mesa: "Mesa 1",
    parte: "María Hernández Pérez",
    caracterParte: "Quejoso",
    autoridad: "Instituto Mexicano del Seguro Social",
    totalAutoridades: 10,
    materia: "Laboral",
    tema: "La sentencia otorga el amparo al quejoso por actos de autoridad que limitaban de manera indebida su derecho a la libre expresión en redes sociales, bajo el argumento de que dichas restricciones no estaban justificadas ni eran proporcionales según lo establecido por la normativa aplicable.",
    sentido: "Sentido",
    resolucion: "No ampara",
    primeraVez: "16/04/2024",
    identidadReservada: "Sí",
  },
  {
    asuntoNeunId: 30301348,
    asuntoAlias: "13/2024",
    catTipoAsunto: "Amparo indirecto",
    cuaderno: "Principal",
    fechaProyecto: "2024-04-16T19:18:00.777",
    usuarioPonencia: "Benito Nacif Hernández",
    ponencia: "Ponencia 3",
    usuarioSecretario: "aortega",
    mesa: "Mesa 1",
    parte: "José Pérez Martínez",
    caracterParte: "Quejoso",
    autoridad: "Secretaría de Hacienda y Crédito Público",
    totalAutoridades: 10,
    materia: "Mercantil",
    tema: "Se concede el amparo al promovente ante la negativa de acceso a tratamientos médicos especializados por parte de la institución de salud pública, considerando dicha negativa como una violación al derecho a la protección de la salud garantizado por la Constitución.",
    sentido: "Aplazado",
    resolucion: "",
    primeraVez: "16/04/2024",
    identidadReservada: "No",
  },
];

const columns = [
  {
    name: "expediente",
    align: "left",
    label: "Expediente",
    field: "expediente",
    sortable: true,
  },
  {
    name: "proyecto",
    align: "center",
    headerStyle: "width: 110px",
    label: "Proyecto",
    field: "fechaProyecto",
    sortable: true,
  },
  {
    name: "secretario",
    align: "left",
    label: "Secretario",
    field: "secretario",
    sortable: true,
  },
  {
    name: "parte",
    align: "left",
    label: "Quejoso/Promovente",
    field: "parte",
    sortable: true,
  },
  {
    name: "autoridades",
    align: "left",
    label: "Autoridades",
    field: "autoridades",
    sortable: true,
  },
  {
    name: "materia",
    align: "left",
    label: "Materia",
    field: "materia",
    sortable: true,
  },
  {
    name: "tema",
    align: "left",
    label: "Tema",
    field: "tema",
    sortable: true,
  },
  {
    name: "primeraVez",
    align: "left",
    label: "Listado por Primera vez",
    field: "primeraVez",
    sortable: true,
  },
  {
    name: "resolucion",
    align: "left",
    label: "Resultado sesión",
    field: "resolucion",
    sortable: true,
  },
  {
    name: "Acciones",
    align: "rigth",
    label: "",
    field: "Acciones",
    sortable: true,
  },
];
</script>
