<template>
  <q-card>
    <q-toolbar>
      <q-toolbar-title> Escanear código QR</q-toolbar-title>
      <q-btn flat round dense icon="mdi-close" v-close-popup />
    </q-toolbar>
    <q-toolbar>
      <q-icon name="mdi-information" color="info" class="q-mr-sm"></q-icon>
      <q-item-label>Posiciona el código QR frente a la cámara</q-item-label>
    </q-toolbar>
    <q-card-section>
      <QrcodeStream
        :paused="paused"
        @detect="onDetect"
        @camera-on="onCameraOn"
        @camera-off="onCameraOff"
        @error="handleQRError"
      >
        <div v-show="showScanConfirmation">
          <q-icon name="mdi-check-circle" size="lg" />
        </div>
      </QrcodeStream>
    </q-card-section>
  </q-card>
</template>

<script setup>
import { ref } from "vue";
import { QrcodeStream } from "vue-qrcode-reader";

const paused = ref(false);
const qrResult = ref("");
const showScanConfirmation = ref(false);
const emit = defineEmits({
  addOficio: (value) => value,
});

function onCameraOn() {
  showScanConfirmation.value = false;
}

function onCameraOff() {
  showScanConfirmation.value = true;
}

async function onDetect(detectedCodes) {
  qrResult.value = detectedCodes.map((code) => code.rawValue);
  emit("addOficio", qrResult.value);
  paused.value = true;
  setTimeout(() => {
    paused.value = false;
  }, 500);
}

function handleQRError(error) {
  qrResult.value = `Error: ${error.message}`;
}
</script>
