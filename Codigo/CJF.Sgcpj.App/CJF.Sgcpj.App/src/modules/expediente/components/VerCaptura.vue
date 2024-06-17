<template>
  <div class="row q-ma-md">
    <div class="col">
      <q-list>
        <q-expansion-item
          icon="file"
          label="Ficha técnica"
          class="shadow-1 overflow-hidden q-mb-md"
          style="border-radius: 10px"
          header-class="bg-indigo-2"
          default-opened
        >
          <q-list>
            <q-item
              v-for="(item, index) in fechaTecnicaExpediente"
              :key="index"
            >
              <q-item-section avatar>
                <q-icon color="grey-7" :name="item.icon" />
              </q-item-section>
              <q-item-section>
                <q-item-label>{{ item.Valor }}</q-item-label>
                <q-item-label caption>{{ item.Campo }}</q-item-label>
              </q-item-section>
            </q-item>
          </q-list>
        </q-expansion-item>
        <q-expansion-item
          v-for="parte in resp"
          :key="parte.Parte"
          class="shadow-1 overflow-hidden q-mb-md"
          style="border-radius: 10px"
          header-class="bg-indigo-2"
        >
          <template v-slot:header>
            <q-item-section side>
              <q-icon color="primary" name="person" />
            </q-item-section>
            <q-item-section>
              <q-item-label overline>{{ parte.status }}</q-item-label>
              <q-item-label>{{ parte.Parte }}</q-item-label>
              <q-item-label caption>{{ parte.tipoPersona }}</q-item-label>
            </q-item-section>

            <q-item-section side top>
              <q-item-label caption>{{ parte.administracion }}</q-item-label>
            </q-item-section>
          </template>
          <q-list bordered>
            <div
              v-for="seccion in parte.Secciones"
              :key="seccion.PadreDescripcion"
            >
              <q-item-label header class="bg-grey-3">{{
                seccion.PadreDescripcion
              }}</q-item-label>
              <q-item v-for="dato in seccion.Datos" :key="dato.Valor">
                <q-item-section>
                  <q-item-label>
                    {{ dato.Valor }}
                  </q-item-label>
                  <q-item-label caption>{{ dato.Descripcion }}</q-item-label>
                </q-item-section>
              </q-item>
            </div>
          </q-list>
        </q-expansion-item>
      </q-list>
    </div>
    <div class="col q-ml-md">
      <q-card class="overflow-hidden q-mb-md" style="border-radius: 10px">
        <q-toolbar class="bg-indigo-2">
          <q-item>
            <q-item-section side>
              <q-icon name="document_scanner"></q-icon>
            </q-item-section>
            <q-item-section>
              <q-item-label>Promociones</q-item-label>
            </q-item-section>
          </q-item>
        </q-toolbar>
        <q-separator></q-separator>
        <q-list separator>
          <q-item v-for="(item, index) in dataResp" :key="index">
            <q-item-section avatar>
              <q-btn
                flat
                round
                icon="mdi-qrcode-scan"
                v-if="item.ArchivoPromocion !== null"
                @click="showDialogPdf = true"
              ></q-btn>
            </q-item-section>
            <q-item-section>
              <q-item-label overline>{{ item.OrigenPromo }}</q-item-label>
              <q-item-label>{{ item.Promovente }}</q-item-label>
              <q-item-label caption>{{ item.CaracterPromovente }}</q-item-label>
            </q-item-section>
            <q-item-section side top>
              <q-item-label caption>{{
                item.TipoContenidoDescripcion
              }}</q-item-label>
              <q-chip
                dense
                square
                icon="mdi-calendar-blank"
                outline
                color="primary"
                v-if="item.FechaPresentacion"
                :label="date.formatDate(item.FechaPresentacion, 'DD/MM/YYYY')"
              ></q-chip>
            </q-item-section>
          </q-item>
        </q-list>

        <!-- <q-timeline>
            <q-timeline-entry
              v-for="item in dataResp"
              :title="item.OrigenPromo"
              icon="mdi-qrcode-scan"
              :subtitle="date.formatDate(item.FechaPresentacion, "DD/MM/YYYY")"
              :key="item.value"
            >
              <q-item>
                <q-item-section side v-if="item.TipoContenidoDescripcion">
                  <q-icon name="" color="grey-7"> </q-icon>
                </q-item-section>
                <q-item-section>
                  <q-item-label>{{
                    item.TipoContenidoDescripcion
                  }}</q-item-label>
                  <q-item-label caption>
                    {{ item.TipoPromocionDescripcion }}
                  </q-item-label>
                </q-item-section>
              </q-item>
              <q-item>
                <q-item-section side v-if="item.Promovente">
                  <q-icon name="" color="grey-7"> </q-icon>
                </q-item-section>
                <q-item-section>
                  <q-item-label>{{ item.Promovente }}</q-item-label>
                  <q-item-label caption>
                    {{ item.CaracterPromovente }}</q-item-label
                  >
                </q-item-section>
              </q-item>
            </q-timeline-entry>
          </q-timeline> -->
      </q-card>
    </div>
  </div>
  <q-dialog v-model="showDialogPdf">
    <ViewPdfComponent :nombreArchivo="documentoPDF" />
  </q-dialog>
</template>

<script setup>
import { ref } from "vue";
import { date } from "quasar";
import { dataResp } from "../data/expedientePage";
import { resp } from "../data/capturaPartes.js";
import ViewPdfComponent from "components/ViewPdfComponent.vue";
import documentoPDF from "assets/PromocionPrueba.pdf";
const showDialogPdf = ref(false);
</script>
