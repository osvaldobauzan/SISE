import { shallowMount } from "@vue/test-utils";
import tramiteCard from "../../../../../src/modules/index/components/tramite/tramiteCard.vue";
import { expect, test } from "vitest";

test("tramiteCard renderiza el componente", () => {
  const wrapper = shallowMount(tramiteCard);
  expect(wrapper.exists()).toBeTruthy();
});
test("computed  mesasArray    ", () => {
  const wrapper = shallowMount(tramiteCard);
  expect(wrapper.vm.mesasArray).toBeTruthy();
});
test("computed  noSortedArray     ", () => {
  const wrapper = shallowMount(tramiteCard, {
    props: { rowsTramite: [{ mesa: 1 }] },
  });
  expect(wrapper.vm.noSortedArray).toBeTruthy();
});
test("computed  seriesAcuerdos", () => {
  const wrapper = shallowMount(tramiteCard, {
    props: { rowsTramite: [{ estado: 1 }] },
  });
  expect(wrapper.vm.seriesAcuerdos.length).toEqual(5);
});
test("Funcion  calcularEstadoAcuerdo  ", () => {
  const wrapper = shallowMount(tramiteCard, {
    props: { rowsTramite: [{ estado: 1 }] },
  });
  expect(wrapper.vm.calcularEstadoAcuerdo(1)).toEqual(1);
});
test("computed  seriesDonutAcuerdos ", () => {
  const wrapper = shallowMount(tramiteCard, {
    props: { rowsTramite: [{ estado: 1 }] },
  });
  expect(wrapper.vm.seriesDonutAcuerdos.length).toEqual(5);
});
test("computed  chartOptionsAcuerdos", () => {
  const wrapper = shallowMount(tramiteCard, {
    props: { rowsTramite: [{ mesa: 1 }] },
  });
  expect(wrapper.vm.chartOptionsAcuerdos).toBeTruthy();
});
