import { describe, it, expect, beforeEach, vi } from "vitest";
import SelectOrganismo from "../../../../src/components/SelectOrganismo.vue"; // Assuming your component is named QrScanner.vue
import { mount } from "@vue/test-utils";
import { createTestingPinia } from "@pinia/testing";
describe("SelectOrganismo", () => {
  let wrapper;

  beforeEach(() => {
    wrapper = mount(SelectOrganismo, {
      shallow: true,
      global: {
        plugins: [
          createTestingPinia({
            createSpy: vi.fn,
          }),
        ],
      },
    });
  });

  it("renders the component", () => {
    expect(wrapper.exists()).toBe(true);
  });
  it("funcion cambioForm", () => {
    expect(wrapper.vm.cambioForm(null)).toBeTruthy();
  });
  it("funcion cerrarSesion", () => {
    expect(wrapper.vm.cerrarSesion()).toBeTruthy();
  });
  it("funcion guardaOrganismo", () => {
    expect(wrapper.vm.guardaOrganismo()).toBeTruthy();
  });
});
