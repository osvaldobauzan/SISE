<template>
  <div v-show="!autoPrint" id="qr-container">
    <table id="qr-container" style="height: 210px; width: 360px">
      <caption></caption>
      <tr>
        <th></th>
        <th></th>
      </tr>
      <tr>
        <td>
          <img id="qr-code" alt="" />
        </td>
        <td style="height: 210px; width: 150px">
          <div
            style="font-size: 14px; font-family: Arial, sans-serif"
            v-if="esHtml"
            id="qrDescripcion"
          ></div>
          <div style="font-size: 14px; font-family: Arial, sans-serif" v-else>
            {{ descripcion }}
          </div>
        </td>
      </tr>
    </table>
  </div>
</template>

<script setup>
import QRious from "qrious";
import { computed, onMounted } from "vue";

const props = defineProps({
  // v-model
  modelValue: {
    default: "",
    required: true,
  },
  autoPrint: {
    default: false,
  },
  esHtml: {
    default: false,
  },
  descripcion: {
    default: "",
  },
});

const emit = defineEmits({
  // v-model event with validation
  "update:modelValue": (value) => value !== null,
  print: (value) => value !== null,
});

const value = computed({
  get() {
    return props.modelValue;
  },
  set(value) {
    emit("update:modelValue", value);
  },
});

async function GeneraQr() {
  await getQrCodeImgData();
  if (props.esHtml && document.getElementById("qrDescripcion")) {
    document.getElementById("qrDescripcion").innerHTML = props.descripcion;
  }
  if (props.autoPrint) {
    imprimirQr();
  }
}
function getQrCodeImgData() {
  return new Promise((resolve) => {
    let qr = new QRious({
      level: "H",
      size: 210,
      element: document.getElementById("qr-code"),
      value: value.value,
    });
    return resolve({
      fileName: props.descripcion || "Qr",
      imgData: qr.toDataURL()?.split(",")[1],
    });
  });
}
function imprimirQr() {
  const elemento = document.getElementById("qr-container");
  if (!elemento) {
    return false;
  }
  let ventana = window?.open("", "", "width=800px,height=800px");
  ventana.document.write("<html><head><title>" + document.title + "</title>");
  ventana.document.write("</head><body>");
  ventana.document.write(elemento.innerHTML);
  ventana.document.write("</body></html>");
  ventana.document.close();
  setTimeout(() => {
    ventana.focus();
    ventana.print();
    ventana.close();
    emit("print", true);
  }, 10);
  return true;
}

onMounted(async () => {
  await GeneraQr();
});
</script>

<style scoped></style>
