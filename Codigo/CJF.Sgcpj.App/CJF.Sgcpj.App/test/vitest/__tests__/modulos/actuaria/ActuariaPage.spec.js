import { shallowMount } from "@vue/test-utils";
import { expect, test } from "vitest";
import ActuariaPage from "../../../../../src/modules/actuaria/pages/ActuariaPage.vue";

test("ActuariaPage renderiza el componente", () => {
  const wrapper = shallowMount(ActuariaPage);
  expect(wrapper.exists()).toBeTruthy();
});

test("Function setFilter", () => {
  const wrapper = shallowMount(ActuariaPage);

  wrapper.vm.setFilterStatus("activo");
  expect(wrapper.vm.pagination.page).toBe(1);
});

test("Function setSelectedDate", () => {
  const wrapper = shallowMount(ActuariaPage);

  wrapper.vm.setSelectedDate("11-10-2023");
  expect(wrapper.vm.pagination.page).toBe(1);
});
// test("Function tabAcuerdoKey", () => {
//   const wrapper = shallowMount(ActuariaPage);

//   const testIndex = 5;
//   const expectedResult = "tabAcuerdoKey5";

//   const result = wrapper.vm.tabAcuerdoKey(testIndex);

//   expect(result).toBe(expectedResult);
// });
