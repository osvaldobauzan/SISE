import { shallowMount } from "@vue/test-utils";
import { describe, it, expect } from "vitest";
import { Quasar, QInput, QIcon } from "quasar";
import InputSearchTableVue from "src/components/InputSearchTable.vue";

// Add Quasar plugin
const wrapperFactory = (options = {}) => {
  return shallowMount(InputSearchTableVue, {
    global: {
      plugins: [Quasar],
      components: { QInput, QIcon },
      ...options,
    },
  });
};

describe("InputSearchTableVue", () => {
  it("renders the input with the correct placeholder", () => {
    const wrapper = wrapperFactory();
    const input = wrapper.findComponent(QInput);
    expect(input.attributes("placeholder")).toBe("Buscar");
  });

  it("emits update:modelValue event on input change", async () => {
    const wrapper = wrapperFactory();
    const input = wrapper.findComponent(QInput);
    await input.setValue("test search");
    expect(wrapper.emitted()["update:modelValue"]).toBeTruthy();
    expect(wrapper.emitted()["update:modelValue"].length).toBe(1);
    expect(wrapper.emitted()["update:modelValue"][0]).toEqual(["test search"]);
  });

  it("emits onSearch event when hitting the Enter key", async () => {
    const wrapper = wrapperFactory();
    const input = wrapper.findComponent(QInput);
    await input.trigger("keyup.enter");
    expect(wrapper.emitted().onSearch).toBeTruthy();
    expect(wrapper.emitted().onSearch.length).toBe(1);
  });

  it("input accepts and reacts to different styleClass prop", async () => {
    const customStyleClass = "test-style-class";
    const wrapper = shallowMount(InputSearchTableVue, {
      global: {
        plugins: [Quasar],
        components: { QInput, QIcon },
      },
      props: {
        styleClass: customStyleClass,
      },
    });
    const input = wrapper.findComponent(QInput);
    expect(input.classes()).toContain(customStyleClass);
  });
});
