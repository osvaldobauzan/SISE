import DialogConfirmacionVue from "../../../../src/components/DialogConfirmacion.vue";
import { shallowMount } from "@vue/test-utils";
import { expect, test, it } from "vitest";

it("DialogConfirmacion Component renders the correct titulo", () => {
  const wrapper = shallowMount(DialogConfirmacionVue, {
    props: { titulo: "Hola" },
  });

  expect(wrapper.props().titulo).toBe("Hola");
});

test("DialogConfirmacion Component renders the correct subTitulo", () => {
  const wrapper = shallowMount(DialogConfirmacionVue, {
    props: { subTitulo: "Mundo" },
  });
  expect(wrapper.props().subTitulo).toBe("Mundo");
});

test("DialogConfirmacion Component emit cancelar", async () => {
  const wrapper = shallowMount(DialogConfirmacionVue);
  wrapper.vm.$emit("cancelar");
  await wrapper.vm.$nextTick();
  expect(wrapper.emitted("cancelar")).toBeTruthy();
});
