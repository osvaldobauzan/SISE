import { describe, it, expect, beforeEach } from "vitest";
import SelectDateComponent from "../../../../src/components/SelectDateComponent.vue"; // Assuming your component is named QrScanner.vue
import { shallowMount } from "@vue/test-utils";
describe("SelectDateComponent", () => {
  let wrapper;

  beforeEach(() => {
    wrapper = shallowMount(SelectDateComponent, {});
  });

  it("renders the component", () => {
    expect(wrapper.exists()).toBe(true);
  });
  it("Function setUltimos7dias", () => {
    wrapper.vm.setUltimos7dias();
    const fechaIni = new Date();
    fechaIni.setDate(fechaIni.getDate() - 6);
    const fechaString = `${fechaIni.getDate().toString().length < 2 ? "0" + fechaIni.getDate() : fechaIni.getDate()}/${(fechaIni.getMonth() + 1).toString().length < 2 ? "0" + (fechaIni.getMonth() + 1) : fechaIni.getMonth() + 1}/${fechaIni.getFullYear()}`;
    expect(wrapper.vm.selectedDate.from).toEqual(fechaString);
  });
  it("Function setUltimos30dias", () => {
    wrapper.vm.setUltimos30dias();
    const fechaIni = new Date();
    fechaIni.setDate(fechaIni.getDate() - 29);
    const fechaString = `${fechaIni.getDate().toString().length < 2 ? "0" + fechaIni.getDate() : fechaIni.getDate()}/${(fechaIni.getMonth() + 1).toString().length < 2 ? "0" + (fechaIni.getMonth() + 1) : fechaIni.getMonth() + 1}/${fechaIni.getFullYear()}`;
    expect(wrapper.vm.selectedDate.from).toEqual(fechaString);
  });
});
