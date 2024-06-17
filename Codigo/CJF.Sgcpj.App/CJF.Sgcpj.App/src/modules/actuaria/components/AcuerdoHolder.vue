<script setup>
import { date } from "quasar";
defineProps({
    ta: {
        expediente: String,
        asuntoNeunId: Number,
        tipoAsunto: String,
        clasificacionCuaderno: String,
        quejoso: String,
        fechaAuto: String,
        sintesis: String,
        actuario: String,
        aceptaOponePublicarDatos: Boolean
    },
    index: Number,
});
</script>
<template>
    <div class="col-4">
        <q-list class="fit">
            <q-item>
                <q-item-section side>
                    <q-avatar text-color="grey-9">
                        {{ index + 1 }}
                    </q-avatar>
                </q-item-section>
                <q-item-section>
                    <q-item-label class="text-bold">
                        {{ ta.expediente }}
                    </q-item-label>
                    <q-item-label class="no-print">
                        {{ ta.asuntoNeunId }}
                    </q-item-label>
                    <q-item-label caption>{{
                        ta.tipoAsunto
                        }}</q-item-label>
                    <q-chip :label="ta.clasificacionCuaderno" square dense class="q-mx-none"></q-chip>

                </q-item-section>
            </q-item>
        </q-list>
    </div>
    <div class="col-8">
        <div class="row q-gutter-md">
            <q-item>
                <q-item-section>
                    <q-item-label caption>Quejoso</q-item-label>
                    <q-item-label class="print-only">
                        {{ !ta.aceptaOponePublicarDatos ? ta.quejoso : '***********' }}
                    </q-item-label>
                    <q-item-label class="no-print">
                        {{ ta.quejoso }}
                    </q-item-label>
                </q-item-section>
            </q-item>
            <q-item>
                <q-item-section>
                    <q-item-label caption>Fecha de acuerdo</q-item-label>
                    <q-item-label>
                        {{
                            date.formatDate(ta.fechaAuto, 'DD-MM-YYYY')
                        }}
                    </q-item-label>
                </q-item-section>
            </q-item>
        </div>
        <q-item>
            <q-item-section>
                <q-item-label caption>Síntesis</q-item-label>
                <q-item-label>
                    {{ ta.sintesis }}
                </q-item-label>
            </q-item-section>
        </q-item>
    </div>
    <div class="col-12 text-center no-print">
        <small> Actuario: <span class="text-bold"> {{ ta.actuario }} </span> </small>
        <hr class="q-my-md" />
    </div>
</template>

<style lang="scss" scoped>
.print-only {
    display: none;
}

@media print {
    .no-print {
        display: none;
    }

    .print-only {
        display: block;
    }
}
</style>
