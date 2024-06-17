import { describe, it, expect, beforeEach } from "vitest";
import ViewPdfComponent from "../../../../src/components/ViewPdfComponent.vue"; // Assuming your component is named QrScanner.vue
import { mount } from "@vue/test-utils";
describe("ViewPdfComponent", () => {
  let wrapper;

  beforeEach(() => {
    wrapper = mount(ViewPdfComponent, {
      shallow: true,
    });
  });

  it("renders the component", () => {
    expect(wrapper.exists()).toBe(true);
  });
});
