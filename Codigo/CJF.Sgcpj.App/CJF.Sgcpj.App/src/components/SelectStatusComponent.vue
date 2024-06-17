<template>
  <div class="q-gutter-xs row">
    <q-card
      flat
      bordered
      :class="filter === item.status ? 'selected' : 'cursor-pointer'"
      v-for="(item, index) in listStatus"
      :key="index"
      :style="item.width ? `'width: ${item.width} !important'` : 'width: 140px'"
      :hidden="item.isHidden"
    >
      <q-card-section horizontal style="height: 100%">
        <q-icon
          size="sm"
          :name="item.icon"
          :class="item.color"
          class="col-3 flex flex-center"
          v-if="item.icon"
        ></q-icon>
        <q-item-section
          v-else
          class="col-2 flex flex-center"
          :class="item.color"
        >
        </q-item-section>
        <q-item
          clickable
          v-ripple
          :active-class="item.color"
          :active="filter === item.status"
          @click="SelectStatus(item.status)"
          :style="
            item.width ? `'width: ${item.width} !important'` : 'width: 150px'
          "
        >
          <q-item-section>
            <q-item-label caption>{{ item.label }}</q-item-label>
            <q-item-label class="text-bold text-secondary">
              {{ item.number }}</q-item-label
            >
          </q-item-section>
        </q-item>
      </q-card-section>
    </q-card>
  </div>
</template>

<script setup>
import { ref } from "vue";

const selectedStatus = ref("");

const emit = defineEmits(["update:filterStatus"]);

defineProps({
  filter: {
    required: true,
  },
  listStatus: {
    required: true,
  },
});

function SelectStatus(status) {
  selectedStatus.value = status;
  emit("update:filterStatus", status);
}
</script>

<style scoped>
.selected {
  border: 2px solid #311b92;
}
</style>
