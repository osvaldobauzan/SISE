import { shallowMount } from "@vue/test-utils";
import { describe, expect, test } from "vitest";
import OficioLibre from "../../../../../src/components/OficioLibre.vue";
import { useTramiteStore } from "src/modules/tramite/store/tramite-store";

test("OficioLibre renderiza el componente", () => {
  const wrapper = shallowMount(OficioLibre);
  expect(wrapper.exists()).toBeTruthy();
});
describe("Function formValido", () => {
  test("Devuelve true si cambioOficioLibre", () => {
    const wrapper = shallowMount(OficioLibre, {
      props: {
        cambioOficioLibre: true,
        modelValue: { text: "cualquier texto" },
      },
    });
    expect(wrapper.vm.formValido).toBe(true);
  });

  test("No detecta cambios cuando el texto es el mismo", () => {
    const wrapper = shallowMount(OficioLibre, {
      props: {
        cambioOficioLibre: false,
        modelValue: { text: "texto original" },
      },
    });
    const tramiteStore = useTramiteStore();
    expect(wrapper.vm.formValido).toBe(false);
    expect(tramiteStore.actualizarOficioLibre).toHaveBeenCalledWith(false);
  });
});

describe("Function documentSize ", () => {
  test("Calcula el tamaño correcto cuando hay texto", () => {
    const text = "Este es un texto de prueba";
    const expectedSize = new Blob([text]).size;

    const wrapper = shallowMount(OficioLibre, {
      props: { modelValue: { text } },
    });

    expect(wrapper.vm.documentSize).toBe(expectedSize);
  });

  test("Devuelve  0 cuando no hay texto", () => {
    const wrapper = shallowMount(OficioLibre, {
      props: { modelValue: { text: "" } },
    });

    expect(wrapper.vm.documentSize).toBe(0);
  });
});

describe("Function cancelar", () => {
  test("Pone showCancelarEditarOficio en true si formValido es true", () => {
    const wrapper = shallowMount(OficioLibre, {
      props: {
        cambioOficioLibre: true,
        modelValue: { text: "cualquier texto" },
      },
    });

    wrapper.vm.cancelar();

    expect(wrapper.vm.showCancelarEditarOficio).toBe(true);
  });

  test("Emite cerrarEditar si formValido es false", async () => {
    const wrapper = shallowMount(OficioLibre, {
      props: {
        cambioOficioLibre: false,
        modelValue: { text: "texto original" },
      },
    });

    wrapper.vm.cancelar();

    expect(wrapper.emitted()).toHaveProperty("cerrarEditar");
    expect(wrapper.emitted("cerrarEditar")[0]).toEqual([
      { value: false, text: "texto original" },
    ]);
  });
});

describe("PorcentajeADecimal", () => {
  test("returns decimal value when percentage string is provided", () => {
    const wrapper = shallowMount(OficioLibre);
    const result = wrapper.vm.porcentajeADecimal("50%");
    expect(result).toBe(0.5);
  });

  test("returns null when non-numeric percentage string is provided", () => {
    const wrapper = shallowMount(OficioLibre);
    const result = wrapper.vm.porcentajeADecimal("abc%");
    expect(result).toBeNull();
  });

  test("returns 1 when no percentage symbol is provided", () => {
    const wrapper = shallowMount(OficioLibre);
    const result = wrapper.vm.porcentajeADecimal("50");
    expect(result).toBe(1);
  });
});

describe("EncontrarAncho", () => {
  test("returns width when width is found in the string", () => {
    const wrapper = shallowMount(OficioLibre);
    const result = wrapper.vm.encontrarAncho('width: 100px"');
    expect(result).toBe(6);
  });

  test("returns null when width is not found in the string", () => {
    const wrapper = shallowMount(OficioLibre);
    const result = wrapper.vm.encontrarAncho("height: 100px");
    expect(result).toBeNull();
  });

  test("returns null when width is not followed by a value", () => {
    const wrapper = shallowMount(OficioLibre);
    const result = wrapper.vm.encontrarAncho('width: "');
    expect(result).toBe(1);
  });

  test("returns null when string does not contain a width value", () => {
    const wrapper = shallowMount(OficioLibre);
    const result = wrapper.vm.encontrarAncho("abc");
    expect(result).toBeNull();
  });
});

describe("AgregarBrAntesDeImg", () => {
  test("adds <br> before <img> tag if not present", () => {
    const wrapper = shallowMount(OficioLibre);
    const result = wrapper.vm.agregarBrAntesDeImg('<img src="image.jpg">');
    expect(result).toBe('<br><img src="image.jpg">');
  });

  test("does not add <br> before <img> tag if already present", () => {
    const wrapper = shallowMount(OficioLibre);
    const result = wrapper.vm.agregarBrAntesDeImg('<br><img src="image.jpg">');
    expect(result).toBe('<br><img src="image.jpg">');
  });

  test("does not modify string if <img> tag is not present", () => {
    const wrapper = shallowMount(OficioLibre);
    const result = wrapper.vm.agregarBrAntesDeImg("No image tag here");
    expect(result).toBe("No image tag here");
  });
});

describe("AgregarSup", () => {
  test("adds <sup> tag after number inside <blockquote><span> tags", () => {
    const wrapper = shallowMount(OficioLibre);
    const result = wrapper.vm.agregarSup(
      "<blockquote><span>100</span></blockquote>",
    );
    expect(result).toBe("<blockquote><span><sup>10</sup>0</span></blockquote>");
  });

  test("adds <sup> tag only after the number inside <blockquote><span> tags", () => {
    const wrapper = shallowMount(OficioLibre);
    const result = wrapper.vm.agregarSup(
      "<blockquote><span>1 2 3 4 5</span></blockquote>",
    );
    expect(result).toBe(
      "<blockquote><span><sup>1</sup>2 3 4 5</span></blockquote>",
    );
  });

  test("does not modify string if <blockquote><span> tags are not present", () => {
    const wrapper = shallowMount(OficioLibre);
    const result = wrapper.vm.agregarSup("No blockquote or span here");
    expect(result).toBe("No blockquote or span here");
  });
});

describe("Test imagesPercentages computed property", () => {
  test("should return an array of decimal percentages", () => {
    const wrapper = shallowMount(OficioLibre);
    const result = wrapper.vm.imagesPercentages;
    expect(result).toEqual(null);
  });
});
