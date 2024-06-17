<template>
  <q-list>
    <q-form ref="formValido">
    <q-expansion-item
        expand-separator
        icon="assignment_late"
        label="Información de captura de la sentencia"
        caption="Campos opcionales"
        default-opened="true"
      >
      <q-card>
        <q-item>
          <q-item-section>
            <q-item-label>¿Se elaboró bajo el formato de Lectura Fácil?</q-item-label>
            <div class="q-gutter-sm">
              <q-radio v-model="LecturaFacil"
                :val="1"
                @update:model-value="cambioForm"
                label="Sí" />
              <q-radio v-model="LecturaFacil"
                :val="0"
                @update:model-value="cambioForm"
                label="No" />
            </div>
          </q-item-section>
        </q-item>

        <q-item>
          <q-item-section>
            <q-item-label>Se aplicaron criterios de perspectiva de género</q-item-label>
            <div class="q-gutter-sm">
              <q-radio v-model="AplicaCriteriosPerspectivaGenero"
                :val="1"
                @update:model-value="cambioForm"
                label="Sí" />
              <q-radio v-model="AplicaCriteriosPerspectivaGenero"
                :val="0"
                @update:model-value="cambioForm"
                label="No" />
            </div>
          </q-item-section>
        </q-item>

        <q-item>
          <q-item-section>
            <div class="q-gutter-sm">
              <q-input
                dense
                v-model="CriterioPerspectivaGeneroAplicado"
                @update:model-value="cambioForm"
                filled
                type="textarea"
                label="Criterio de perspectiva de género aplicado"/>
            </div>
          </q-item-section>
        </q-item>
        <q-item>
          <q-item-section>
            <div class="row q-mb-sm">
              <q-select
                class="col"
                dense
                filled
                label="Derechos Humanos Fundamentales Analizados"
                option-label="descripcion"
                @update:model-value="cambioForm"
                v-model="Derechos"
                :options="optionsDerecho">
              </q-select>
              <q-select class="col"
                dense
                filled
                label="Derechos Humanos Fundamentales Analizados Específico"
                option-label="descripcion"
                @update:model-value="cambioForm"
                v-model="DerechoEspecifico"
                :options="optionsDerechoEspecifico">
              </q-select>
            </div>
          </q-item-section>
        </q-item>
        <q-item>
          <q-item-section>
            <q-item-label>¿Esta sentencia se emitió con aplicación efectiva de un
              ordenamiento internacional y/o nacional de protección a los
              derechos de las mujeres a la igualdad y no
              discriminación?</q-item-label>
            <div class="q-gutter-sm">
              <q-radio v-model="AplicacionEfectivaDerechoMujeres" :val="1"
                @update:model-value="cambioForm"
                label="Sí" />
              <q-radio v-model="AplicacionEfectivaDerechoMujeres"  :val="0"
                @update:model-value="cambioForm"
                label="No" />
            </div>
            <div class="row q-my-sm">
              <q-select class="col"
                dense
                filled
                label="¿La sentencia versó sobre alguno de los asuntos internacionales siguientes?"
                option-label="descripcion"
                @update:model-value="cambioForm"
                v-model="TemaAsuntosInternacionales"
                :options="optionsTemaAsuntosInternacionales">
              </q-select>
            </div>
          </q-item-section>
        </q-item>
      </q-card>
    </q-expansion-item>
   </q-form>
  </q-list>
  <!--
  <q-expansion-item
      expand-separator
      icon="link"
      label="Promociones pendientes en este asunto"
      caption="Campos opcionales"
      default-opened="true"
    >
    <q-table
        flat bordered
        :rows="rows"
        :columns="columns"
        row-key="noOrden"
        color="amber"
      />

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
            :label="Promociones"
            option-label="numeroRegistro"
            @filter="filtrarContenido"
            option-value="numeroRegistro"
            @update:model-value="cambioForm"
            :options="rowsPromociones"
          >
          </q-select>
        </div>
  </q-expansion-item>
  -->
</template>

<script setup>
  import { ref, onMounted } from "vue";
  import { useEngroseStore } from "../../store/engrose-store";
  import { useCatalogosStore } from "src/stores/catalogos-store";
  import { manejoErrores } from "src/helpers/manejo-errores";

  const formValido = ref(false);
  const estatusFormulario = ref(false);

  const LecturaFacil = ref(0);
  const AplicaCriteriosPerspectivaGenero = ref(0);
  const CriterioPerspectivaGeneroAplicado = ref("");
  const Derechos = ref("");
  const DerechoEspecifico = ref("");
  const AplicacionEfectivaDerechoMujeres = ref(0);
  const TemaAsuntosInternacionales = ref("");

  const optionsDerecho = ref([]);
  const optionsDerechoEspecifico = ref([]);
  const optionsTemaAsuntosInternacionales = ref([]);

  const engroseStore = useEngroseStore();
  const catalogosStore = useCatalogosStore();
  const loadingCatalogs = ref(false);

  const props = defineProps({
    item: {
      type: Object,
      required: true,
    }
  });

  onMounted( async() => {
    loadingCatalogs.value = true;
    try {
      await catalogosStore.obtenerTiposAnexoTipoCatalogo(props.item?.expediente.catTipoAsuntoId, props.item?.expediente.catOrganismoId,589);
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
    optionsDerecho.value = [...catalogosStore.tiposAnexo];

    try {
      await catalogosStore.obtenerTiposAnexoTipoCatalogo(props.item?.expediente.catTipoAsuntoId, props.item?.expediente.catOrganismoId,845);
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
    optionsTemaAsuntosInternacionales.value = [...catalogosStore.tiposAnexo];

    loadingCatalogs.value = false;

   /* try {
    await engroseStore.promocionesPorExpediente(props.item?.expediente.asuntoNeunId,props.item?.expediente.numeroExpediente);
    } catch (error) {
      console.error(error);
      manejoErrores.mostrarError(error);
    }
    rowsPromociones.value = engroseStore.promocionesXExpediente; */

  });

  async function cambioForm() {
    estatusFormulario.value = await formValido.value?.validate(false);
    await formValido.value?.resetValidation();
    engroseStore.sentenciaData.LecturaFacil = LecturaFacil.value;
    engroseStore.sentenciaData.AplicaCriteriosPerspectivaGenero = AplicaCriteriosPerspectivaGenero.value;
    engroseStore.sentenciaData.CriterioPerspectivaGeneroAplicado = CriterioPerspectivaGeneroAplicado.value;
    engroseStore.sentenciaData.Derechos = Derechos.value?.id;
    if (Derechos.value?.id) {
        try {
        await catalogosStore.obtenerOpcionesCatalogosDependientes( Derechos.value?.id, 589);
      } catch (error) {
        manejoErrores.mostrarError(error);
      }
    }
    optionsDerechoEspecifico.value = [...catalogosStore.catalogoDependiente];

    engroseStore.sentenciaData.DerechoEspecifico = DerechoEspecifico.value?.id;
    engroseStore.sentenciaData.AplicacionEfectivaDerechoMujeres = AplicacionEfectivaDerechoMujeres.value;
    engroseStore.sentenciaData.TemaAsuntosInternacionales = TemaAsuntosInternacionales.value?.id;
  }


</script>
