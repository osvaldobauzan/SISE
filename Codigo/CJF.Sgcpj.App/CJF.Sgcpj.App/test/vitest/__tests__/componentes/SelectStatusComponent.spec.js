import { describe, it, expect, beforeEach } from "vitest";
import SelectStatusComponent from "../../../../src/components/SelectStatusComponent.vue"; // Assuming your component is named QrScanner.vue
import { mount } from "@vue/test-utils";
describe("SelectStatusComponent", () => {
  let wrapper;

  beforeEach(() => {
    wrapper = mount(SelectStatusComponent, {
      shallow: true,
    });
  });

  it("renders the component", () => {
    expect(wrapper.exists()).toBe(true);
  });
});
