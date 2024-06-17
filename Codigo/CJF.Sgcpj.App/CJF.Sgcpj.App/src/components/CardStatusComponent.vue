<template>
  <div class="row q-gutter-md full-width">
    <q-card
      v-for="item in listCards"
      class="col cursor-pointer"
      :class="
        item.invisible
          ? 'invisible'
          : selectedStatus === item.key
            ? 'bg-grey-8 shadow-8 text-white'
            : 'text-grey-8'
      "
      :key="item.key"
    >
      <q-list clickable v-ripple @click="SelectStatus(item.key)">
        <q-item>
          <q-item-section side v-if="item.icon">
            <q-icon
              flat
              size="xm"
              :name="item.icon"
              :class="
                selectedStatus === item.key ? 'text-white' : 'text-grey-8'
              "
            />
          </q-item-section>
          <q-item-section>
            <q-item-label>
              <span class="text-h6">{{ item.name }}</span>
            </q-item-label>
          </q-item-section>
        </q-item>
        <q-item>
          <q-item-section>
            <q-item-label>
              <div class="row fit items-end">
                <div class="col-auto text-h4 text-bold">
                  {{ item.progress }}
                </div>
                <div class="col q-ml-sm q-mb-xs">de {{ item.total }}</div>
              </div>
            </q-item-label>
            <q-item-label>
              <div class="row q-mt-sm">
                <q-item-label class="text-caption">{{
                  item.caption
                }}</q-item-label>
                <q-space></q-space>
                <q-item-label class="text-caption"
                  >{{ getProgress(item) }}%</q-item-label
                >
              </div>
            </q-item-label>
            <q-item-label>
              <div class="row q-mt-sm">
                <q-linear-progress
                  rounded
                  size="md"
                  :value="getLinearProgress(item)"
                  color="green-14"
                  :class="selectedStatus === item.key ? 'bg-white' : ''"
                >
                </q-linear-progress>
              </div>
            </q-item-label>
          </q-item-section>
        </q-item>
      </q-list>
    </q-card>
  </div>
</template>

<script setup>
import { ref } from "vue";

const selectedStatus = ref("tramite");
const emit = defineEmits(["selectStatus"]);

function SelectStatus(status) {
  selectedStatus.value = status;
  emit("selectStatus", status);
}

defineProps({
  listCards: {
    required: true,
  },
});

const getProgress = (card) => {
  if (card.total !== 0)
    return parseFloat((card.progress / card.total) * 100).toFixed(0);
  else return 0;
};

const getLinearProgress = (card) => {
  if (card.total !== 0) return card.progress / card.total;
  else return 0;
};
</script>
