<template>
  <q-card>
    <q-toolbar>
      <q-toolbar-title>Sentencia</q-toolbar-title>
      <q-btn label="Aprobar" icon="mdi-check" color="positive"> </q-btn>
      <q-btn
        label="No Aprobar"
        icon="mdi-close"
        color="negative"
        class="q-mx-md"
      >
      </q-btn>
      <q-btn label="Corregir" icon="mdi-clock-alert" color="warning"> </q-btn>
    </q-toolbar>
    <q-separator></q-separator>
    <q-splitter
      v-model="splitterModel"
      :limits="[50, 100]"
      style="height: 100%"
    >
      <template v-slot:before>
        <q-card-section class="q-gutter-sm">
          <q-item v-if="expediente">
            <q-item-section>
              <q-item-label class="text-bold">{{
                expediente?.AsuntoAlias
              }}</q-item-label>
              <q-item-label caption>{{
                expediente?.CatTipoAsunto
              }}</q-item-label>
            </q-item-section>
          </q-item>
          <q-input
            v-else
            v-model="searchExpediente"
            placeholder="Buscar expediente"
          >
            <template v-slot:append>
              <q-btn flat round icon="mdi-qrcode" />
            </template>
          </q-input>
          <div class="row">
            <div class="col">
              <q-input v-model="fechaPromocion" label="Fecha promoción">
                <template v-slot:append>
                  <q-icon name="mdi-calendar-blank" class="cursor-pointer">
                    <q-popup-proxy
                      cover
                      transition-show="scale"
                      transition-hide="scale"
                    >
                      <q-date v-model="fechaPromocion" mask="DD-MM-YYYY">
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
                </template>
              </q-input>
            </div>
            <div class="col q-ml-sm">
              <q-input v-model="horaPromocion" label="Hora acuerdo">
                <template v-slot:append>
                  <q-icon
                    name="mdi-clock-time-four-outline"
                    class="cursor-pointer"
                  >
                    <q-popup-proxy
                      cover
                      transition-show="scale"
                      transition-hide="scale"
                    >
                      <q-time
                        with-seconds
                        v-model="horaPromocion"
                        mask="HH:mm:ss"
                      >
                        <div class="row items-center justify-end">
                          <q-btn
                            v-close-popup
                            label="Close"
                            color="primary"
                            flat
                          />
                        </div>
                      </q-time>
                    </q-popup-proxy>
                  </q-icon>
                </template>
              </q-input>
            </div>
          </div>
          <div style="border: 3px dashed #ccc">
            <q-file
              :model-value="files"
              @update:model-value="updateFiles"
              borderless
              multiple
              :clearable="!isUploading"
              class="full-width full-height"
            >
              <template v-slot:file="{ index, file }">
                <q-chip
                  class="full-width q-my-xs"
                  :removable="isUploading && uploadProgress[index].percent < 1"
                  square
                  @remove="cancelFile(index)"
                >
                  <q-linear-progress
                    class="absolute-full full-height"
                    :value="uploadProgress[index].percent"
                    :color="uploadProgress[index].color"
                    track-color="grey-2"
                  />

                  <q-avatar>
                    <q-icon :name="uploadProgress[index].icon" />
                  </q-avatar>

                  <div class="ellipsis relative-position">
                    {{ file.name }}
                  </div>

                  <q-tooltip>
                    {{ file.name }}
                  </q-tooltip>
                </q-chip>
              </template>

              <template v-slot:after v-if="canUpload">
                <q-btn
                  dense
                  flat
                  size="lg"
                  icon="mdi-upload"
                  round
                  @click="upload"
                  :disable="!canUpload"
                  :loading="isUploading"
                />
              </template>
            </q-file>
          </div>
        </q-card-section>
      </template>
      <template v-slot:after>
        <q-card-section>
          <q-list bordered separator>
            <q-item>
              <q-item-section side>
                <q-btn
                  flat
                  round
                  icon="mdi-folder"
                  color="secondary"
                  to="#/expediente"
                ></q-btn>
              </q-item-section>
              <q-item-section>
                <q-item-label class="text-bold">
                  {{ item.AsuntoAlias }}
                </q-item-label>
                <q-item-label caption>
                  {{ item.CatTipoAsunto }}
                </q-item-label>
              </q-item-section>
              <q-item-section side>
                <q-chip
                  square
                  icon="mdi-calendar-blank"
                  label="07/08/2023"
                  class="grey-1"
                >
                  <q-tooltip>Fecha de creación</q-tooltip>
                </q-chip>
              </q-item-section>
            </q-item>
            <q-item>
              <q-item-section>
                <q-item-label class="text-h5">
                  Solicitud de copias</q-item-label
                >
                <q-item-label caption>{{
                  date.formatDate(item.FechaAlta, "DD/MM/YYYY HH:mm:ss")
                }}</q-item-label>
              </q-item-section>
            </q-item>
            <!-- <q-item-label header>Síntesis</q-item-label> -->
            <q-editor v-model="sintesis" />
          </q-list>
        </q-card-section>
      </template>
    </q-splitter>
    <q-separator></q-separator>
    <q-card-actions align="right">
      <q-btn label="Cancelar" color="primary" flat />
      <q-btn label="Guardar" color="primary" />
    </q-card-actions>
  </q-card>
</template>

<script setup>
import { ref } from "vue";
import { date } from "quasar";

const sintesis = ref("");
const splitterModel = ref(100);

// eslint-disable-next-line no-unused-vars
const props = defineProps({
  item: {
    type: Object,
    required: true,
  },
});
</script>
