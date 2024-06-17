<template>
  <div>
    <q-btn dense flat v-close-popup @click="toggleCamera">
      <div class="no-wrap">
        <q-icon color="primary" :name="myIcon" class="text-grey-8" />
      </div>
    </q-btn>

    <q-dialog v-model="dialog" position="top" v-if="!qrResult">
      <q-card>
        <q-card-section>
          <div class="text-h6">Escanear QR</div>
        </q-card-section>
        <q-card-section>
          <div v-if="cameraActive">
            <QrcodeStream
              :track="selected.value"
              @error="handleQRError"
              @decode="handleQRDecode"
              :deviceId="selectedDeviceId"
              :video-id="videoElementId"
              ref="qrcodeStreamRef"
            />
          </div>
        </q-card-section>
        <q-card-actions>
          <q-btn v-close-popup label="Cerrar" color="primary" />
        </q-card-actions>
      </q-card>
    </q-dialog>
  </div>
</template>

<script setup>
import { ref, onBeforeUnmount, nextTick } from "vue";
import { QrcodeStream, setZXingModuleOverrides } from "vue-qrcode-reader";
import wasmFile from "assets/zxing_reader.wasm";

setZXingModuleOverrides({
  locateFile: (path, prefix) => {
    if (path.endsWith(".wasm")) {
      return wasmFile;
    }
    return prefix + path;
  },
});

const option = [{ text: "Camara", value: readQrContent }];
const selected = option[0];
const qrResult = ref("");
const cameraActive = ref(false);
const dialog = ref(false);
const qrcodeStreamRef = ref(null);

const videoElementId = "videoElement";
let selectedDeviceId = null;

const emit = defineEmits(["update:qrContent"]);

defineProps({
  myIcon: {
    type: String,
    default: "qr_code_scanner",
  },
});

function readQrContent(detectedCodes) {
  for (const detectedCode of detectedCodes) {
    const { rawValue } = detectedCode;
    const qrValue = rawValue.toString();
    qrResult.value = qrValue;
    cameraActive.value = false;
    qrcodeStreamRef.value = null;
    if (qrResult.value) {
      emitResult();
    }
  }
}

function handleQRError(error) {
  qrResult.value = `Error: ${error.message}`;
}

function handleQRDecode(result) {
  qrResult.value = result;
}

function emitResult() {
  emit("update:qrContent", qrResult);
}

async function startCamera() {
  try {
    const devices = await navigator.mediaDevices.enumerateDevices();
    const videoDevices = devices.filter(
      (device) => device.kind === "videoinput",
    );

    if (videoDevices.length > 0) {
      selectedDeviceId = videoDevices[0].deviceId;
      cameraActive.value = true;
      qrcodeStreamRef.value = ref(null);
      await nextTick();
    } else {
      handleQRError(new Error("No se encontraron dispositivos de cámara."));
    }
  } catch (error) {
    handleQRError(error);
  }
}

onBeforeUnmount(() => {
  if (cameraActive.value) {
    cameraActive.value = false;
    qrcodeStreamRef.value = null;
  }
});

function toggleCamera() {
  if (cameraActive.value) {
    cameraActive.value = false;
    qrcodeStreamRef.value = null;
    dialog.value = false;
  } else {
    startCamera();
    dialog.value = true;
    qrResult.value = null;
  }
}
</script>
