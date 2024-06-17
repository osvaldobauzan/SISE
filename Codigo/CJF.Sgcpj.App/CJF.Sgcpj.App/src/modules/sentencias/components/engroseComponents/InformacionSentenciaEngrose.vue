<template>
  <q-form ref="formValido">
    <q-expansion-item icon="assignment_late" label="Información general de la sentencia" caption="Campos obligatorios"
      default-opened="true">
      <q-card>
        <q-card-section>
          <q-list>
            <q-item>
              <q-item-section>
                <q-item-label>
                  Hubo solicitud de reparación del daño o fue decretada en la sentencia
                </q-item-label>
              </q-item-section>
              <q-item-section side>
                <q-radio v-model="SolicitudReparacion" @update:model-value="cambioForm" :val="1" label="Sí" />
              </q-item-section>
              <q-item-section side>
                <q-radio v-model="SolicitudReparacion" @update:model-value="cambioForm" :val="0" label="No" />
              </q-item-section>
            </q-item>
            <q-item v-if="SolicitudReparacion === 1">
              <q-item-section>
                <div class="row q-col-gutter-md">
                  <div class="col-12">
                    <q-select dense filled :loading="loadingCatalogs" option-label="label" label="Selecciona una opción"
                      v-model="SolicitudReparacionOpcion" @update:model-value="cambioForm" :options="optionsReparacion">
                    </q-select>
                  </div>
                  <div class="col-12" v-if="SolicitudReparacionOpcion?.value === 1 && SolicitudReparacion === 1">
                    <!-- {{reparacionSeleccionada}} -->
                    <q-input dense v-model="SolicitudReparacionOtro" filled type="textarea"
                      label="Escriba brevemente la reparación del daño" />
                  </div>
                </div>
              </q-item-section>
            </q-item>
            <q-separator spaced inset />


            <q-item>
              <q-item-section>
                <q-item-label>
                  ¿La sentencia fue dictada por un órgano jurisdiccional auxiliar?
                </q-item-label>
              </q-item-section>
              <q-item-section side>
                <q-radio v-model="EsJDA" :val="true" @update:model-value="cambioForm" label="Sí" />
              </q-item-section>
              <q-item-section side>
                <q-radio v-model="EsJDA" :val="false" @update:model-value="cambioForm" label="No" />
              </q-item-section>
            </q-item>
            <q-separator spaced inset />
            <q-item>
              <q-item-section>
                <q-item-label>
                  Criterio novedoso o relevante (Acdos. 68/2004 y 69/2004 Sec. Ejec. Vigilancia)
                </q-item-label>
              </q-item-section>
              <q-item-section side>
                <q-radio v-model="Criterio" :val="1" @update:model-value="cambioForm" label="Sí" />
              </q-item-section>
              <q-item-section side>
                <q-radio v-model="Criterio" :val="0" @update:model-value="cambioForm" label="No" />
              </q-item-section>
            </q-item>
            <q-separator spaced inset />
            <q-item>
              <q-item-section>
                <q-item-label>
                  Asunto trascendental a la opinión pública (Acdos. 28/2007 - Comunicación Social)
                </q-item-label>
              </q-item-section>
              <q-item-section side>
                <q-radio v-model="Trascendental" :val="1" @update:model-value="cambioForm" label="Sí" />
              </q-item-section>
              <q-item-section side>
                <q-radio v-model="Trascendental" :val="0" @update:model-value="cambioForm" label="No" />
              </q-item-section>
            </q-item>
            <q-separator spaced inset />
            <q-item>
              <q-item-section>
                <q-item-label>
                  La sentencia o resolución se emitió conforme a un Tratado Internacional en materia de Derechos Humanos
                </q-item-label>
              </q-item-section>
              <q-item-section side>
                <q-radio v-model="EsTratadoInternacional" :val="1" @update:model-value="cambioForm" label="Sí" />
              </q-item-section>
              <q-item-section side>
                <q-radio v-model="EsTratadoInternacional" :val="0" @update:model-value="cambioForm" label="No" />
              </q-item-section>
            </q-item>

            <q-item>
              <q-item-section>
                <q-select class="col" dense filled :loading="loadingCatalogs"
                  label="Nombre del Tratado Internacional que se utilizó para resolver" @update:model-value="cambioForm"
                  option-label="descripcion" v-model="TemaAsuntosInternacionales"
                  :options="optionsTratadosInternacionales"></q-select>
              </q-item-section>
            </q-item>
            <q-separator spaced inset />
          </q-list>
        </q-card-section>
      </q-card>
    </q-expansion-item>
    <q-separator />
    <q-expansion-item expand-separator icon="import_contacts" label="Información sentencia versión pública"
      caption="Campos indispensables">
      <q-card>
        <q-card-section>
          <q-list>
            <q-item>
              <q-item-section>
                <q-item-label>¿El asunto se relaciona con delincuencia organizada?</q-item-label>
              </q-item-section>
              <q-item-section side>
                <q-radio v-model="DelincuenciaOrganizada" :val="1" @update:model-value="declaracionVP" label="Sí" />
              </q-item-section>
              <q-item-section side>
                <q-radio v-model="DelincuenciaOrganizada" :val="0" @update:model-value="declaracionVP" label="No" />
              </q-item-section>
            </q-item>
            <q-separator spaced inset />
            <q-item>
              <q-item-section>
                <q-item-label>¿Se debe guardar sigilo o se actualiza un supuesto de reserva
                  que restrinja la publicidad de todo el documento?</q-item-label>
              </q-item-section>
              <q-item-section side>
                <q-radio v-model="Sigilo" :disable="DelincuenciaOrganizada == 1" :val="true" @update:model-value="cambioForm" label="Sí" />
              </q-item-section>
              <q-item-section side>
                <q-radio v-model="Sigilo" :disable="DelincuenciaOrganizada == 1" :val="false" @update:model-value="cambioForm" label="No" />
              </q-item-section>
            </q-item>

            <q-item v-if="Sigilo">
              <q-item-section>
                <q-input dense v-model="Justificacion" @update:model-value="declaracionVP" filled
                  type="textarea" label="Justificación" />
              </q-item-section>
            </q-item>
            <q-separator spaced inset />

            <q-item>
              <q-item-section>
                <q-select dense filled :loading="loadingCatalogs" @update:model-value="cambioForm"
                  label="Secretario que certifica" option-label="nombreEmpleado" v-model="UsuarioCaptura"
                  :options="optionsSecretario">
                </q-select>
              </q-item-section>
            </q-item>
            <q-separator spaced inset />
            <q-item>
              <q-item-section>
                <q-item-label>
                  La sentencia contiene información clasificada como confidencial (Art. 113 LFTAIP)?
                </q-item-label>
              </q-item-section>
              <q-item-section side>
                <q-radio v-model="Confidencial" :val="1" @update:model-value="declaracionVP" label="Sí" />
              </q-item-section>
              <q-item-section side>
                <q-radio v-model="Confidencial" :val="0" @update:model-value="declaracionVP" label="No" />
              </q-item-section>
            </q-item>
            <q-item class="q-col-gutter-md" style="flex-wrap:wrap" v-if="Confidencial === 1">
              <div class="col-12">
                  <q-item-section>
                <q-select dense filled label="Fracción" option-label="descripcion" @update:model-value="declaracionVP"
                  v-model="FraccionConfidencial" :options="optionsFraccionConfidencial"></q-select>
              </q-item-section>
                </div>
                <div class="col-6">
                  <q-input dense v-model="MotivacionConfidencial" @update:model-value="declaracionVP" filled
                  type="textarea" label="Motivación de la clasificación" />
                </div>
                <div class="col-6">
                  <q-input dense v-model="ObservacionesConfidencial" @update:model-value="declaracionVP" filled
                      type="textarea" label="Observaciones" />
                </div>


            </q-item>
            <q-separator spaced inset />

            <q-item>
              <q-item-section>
                <q-item-label>
                  ¿La sentencia contiene información clasificada como reservada (Art. 110 LFTAIP)?
                </q-item-label>
              </q-item-section>
              <q-item-section side>
                <q-radio v-model="Reservada" :val="1" @update:model-value="declaracionVP" label="Sí" />
              </q-item-section>
              <q-item-section side>
                <q-radio v-model="Reservada" :val="0" @update:model-value="declaracionVP" label="No" />
              </q-item-section>
            </q-item>

            <q-item class="q-col-gutter-md" style="flex-wrap:wrap" v-if="Reservada === 1">
              <div class="col-12">
                <q-item-section>
                  <q-select dense filled label="Fracción" option-label="descripcion" v-model="FraccionReservada"
                    @update:model-value="declaracionVP" :options="optionsFraccionReservada"></q-select>
                </q-item-section>
              </div>
              <div class="col-6">
                <q-item-section>
                  <q-input dense v-model="MotivacionReservada" filled type="textarea"
                    @update:model-value="declaracionVP" label="Motivación de la clasificación" />
                </q-item-section>
              </div>
              <div class="col-6">
                <q-item-section>
                    <q-input dense v-model="ObservacionesReservada" filled @update:model-value="declaracionVP"
                      type="textarea" label="Observaciones" />
                  </q-item-section>
              </div>
            </q-item>
          </q-list>
        </q-card-section>
      </q-card>
    </q-expansion-item>
  </q-form>
</template>

<script setup>
  import { ref, onMounted } from "vue";
  import { useEngroseStore } from "../../store/engrose-store";
  import { useCatalogosStore } from "src/stores/catalogos-store";

  const engroseStore = useEngroseStore();

  const SolicitudReparacionOpcion = ref("Restitución de bienes");
  const SolicitudReparacionOtro = ref("");
  const DelincuenciaOrganizada = ref(0);
  const SolicitudReparacion = ref(0);
  const Sigilo = ref(false);
  const EsJDA = ref(false);
  const Confidencial = ref(0);
  const Reservada = ref(0);
  const Justificacion = ref("");
  const Criterio = ref(0);
  const Trascendental = ref(0);
  const EsTratadoInternacional = ref(0);
  const TemaAsuntosInternacionales = ref(null);
  const FraccionConfidencial = ref("");
  const MotivacionConfidencial = ref("");
  const ObservacionesConfidencial = ref("");
  const MotivacionReservada = ref("");
  const ObservacionesReservada = ref("");
  const FraccionReservada = ref("");
  const UsuarioCaptura = ref("");

  const estatusFormulario = ref(false);

  const optionsSecretario = ref([]);
  const optionsFraccionConfidencial = ref([]);
  const optionsFraccionReservada = ref([]);
  const optionsTratadosInternacionales = ref([]);

  const formValido = ref(false);

  const optionsReparacion = [
    { label: "Restitución de bienes", value: 0 },
    { label: "Otro", value: 1 }
  ];

  const props = defineProps({
    item: {
      type: Object,
      required: true,
    }
  });

  const catalogosStore = useCatalogosStore();
  const loadingCatalogs = ref(false);

  onMounted( async() => {
    loadingCatalogs.value = true;
    try {
      await catalogosStore.obtenerTiposAnexoTipoCatalogo(props.item?.expediente.catTipoAsuntoId, props.item?.expediente.catOrganismoId,1147);
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
    optionsFraccionConfidencial.value = [...catalogosStore.tiposAnexo];

    try {
      await catalogosStore.obtenerTiposAnexoTipoCatalogo(props.item?.expediente.catTipoAsuntoId, props.item?.expediente.catOrganismoId,1616);
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
    optionsFraccionReservada.value = [...catalogosStore.tiposAnexo];

    try {
      await catalogosStore.obtenerTiposAnexoTipoCatalogo(props.item?.expediente.catTipoAsuntoId, props.item?.expediente.catOrganismoId,555);
    } catch (error) {
      manejoErrores.mostrarError(error);
    }
    optionsTratadosInternacionales.value = [...catalogosStore.tiposAnexo];

    optionsSecretario.value = catalogosStore.secretarios;
    loadingCatalogs.value = false;
  });

  async function cambioForm() {
    estatusFormulario.value = await formValido.value?.validate(false);
    await formValido.value?.resetValidation();
    engroseStore.sentenciaData.SolicitudReparacion = SolicitudReparacion.value;
    engroseStore.sentenciaData.SolicitudReparacionOpcion = SolicitudReparacionOpcion.value?.value;
    engroseStore.sentenciaData.SolicitudReparacionOtro = SolicitudReparacionOtro.value;
    engroseStore.sentenciaData.EsJDA = EsJDA.value;
    engroseStore.sentenciaData.Criterio = Criterio.value;
    engroseStore.sentenciaData.Trascendental = Trascendental.value;
    engroseStore.sentenciaData.EsTratadoInternacional = EsTratadoInternacional.value;
    engroseStore.sentenciaData.TemaAsuntosInternacionales = TemaAsuntosInternacionales.value?.id;
    engroseStore.sentenciaData.Sigilo = Sigilo.value;
    engroseStore.sentenciaData.Justificacion = Justificacion.value;
  }

  async function declaracionVP() {
    estatusFormulario.value = await formValido.value?.validate(false);
    await formValido.value?.resetValidation();

    if(DelincuenciaOrganizada.value == 1)
    {
      Sigilo.value = true;
      cambioForm();
    }

    engroseStore.sentenciaVP.DelincuenciaOrganizada = DelincuenciaOrganizada.value;
    engroseStore.sentenciaVP.UsuarioCaptura = UsuarioCaptura.value?.id;

    engroseStore.sentenciaVP.Confidencial = Confidencial.value;
    engroseStore.sentenciaVP.FraccionConfidencial  = FraccionConfidencial.value?.id;
    engroseStore.sentenciaVP.MotivacionConfidencial = MotivacionConfidencial.value;

    engroseStore.sentenciaVP.Reservada = Reservada.value;
    engroseStore.sentenciaVP.FraccionReservada = FraccionReservada.value?.id;
    engroseStore.sentenciaVP.MotivacionReservada = MotivacionReservada.value;
  }
</script>
