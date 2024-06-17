import { shallowMount } from "@vue/test-utils";
import { describe, expect, test } from "vitest";
import AcuerdoSintesis from "../../../../../src/modules/actuaria/components/AcuerdoSintesis.vue";

test("AcuerdoSintesis renderiza el componente", () => {
  const wrapper = shallowMount(AcuerdoSintesis, {
    props: {
      item: {
        sintesis: "valor de prueba",
      },
    },
  });
  expect(wrapper.exists()).toBeTruthy();
});

describe("Function cerrarSintesis", () => {
  test("Verifica que no cambio sintesis", async () => {
    const wrapper = shallowMount(AcuerdoSintesis, {
      props: { item: { sintesis: "original value" } },
    });
    wrapper.vm.sintesis = "new value";
    wrapper.vm.showCancelar = false;
    wrapper.vm.cambioSintesis = true;

    await wrapper.vm.cerrarSintesis();
    expect(wrapper.vm.showCancelar).toBe(true);
  });

  test("Emite el evento cerrar ", async () => {
    const wrapper = shallowMount(AcuerdoSintesis, {
      props: { item: { sintesis: "same value" } },
    });

    wrapper.vm.sintesis = "same value";

    await wrapper.vm.cerrarSintesis();
    expect(wrapper.emitted()).toHaveProperty("cerrar");
  });
  test("funcion guardarSintesis Crear", async () => {
    const wrapper = shallowMount(AcuerdoSintesis, {
      props: {
        item: {
          sintesis: "same value",
          tipoCuaderno: 1,
          nombreArchivo: "Test",
          expediente: { asuntoNeunId: 123456 },
        },
      },
    });
    await wrapper.vm.guardarSintesis();
    expect(wrapper.vm.guardarSintesis()).toBeTruthy();
  });
  test("funcion guardarSintesis Editar", async () => {
    const wrapper = shallowMount(AcuerdoSintesis, {
      props: {
        item: {
          sintesis: "same value",
          tipoCuaderno: 1,
          nombreArchivo: "Test",
          expediente: { asuntoNeunId: 123456 },
        },
        title: "Editar",
      },
    });
    await wrapper.vm.guardarSintesis();
    expect(wrapper.vm.guardarSintesis()).toBeTruthy();
  });
});
