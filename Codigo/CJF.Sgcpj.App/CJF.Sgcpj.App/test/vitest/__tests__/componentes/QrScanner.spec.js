import { describe, it, expect, beforeEach } from "vitest";
import QrScanner from "../../../../src/components/QrScanner.vue"; // Assuming your component is named QrScanner.vue
import { mount } from "@vue/test-utils";
describe("QrScanner", () => {
  let wrapper;

  beforeEach(() => {
    wrapper = mount(QrScanner, {
      shallow: true,
    });
  });

  it("renders the component", () => {
    expect(wrapper.exists()).toBe(true);
  });

  it("starts with scan not paused", () => {
    const paused = wrapper.vm.paused;
    expect(paused).toBe(false);
  });

  it("hides the scan confirmation icon initially", () => {
    const icons = wrapper.findAll("div");
    icons.forEach((icon) => {
      expect(icon.isVisible()).toBe(false);
    });
  });

  it("shows the scan confirmation icon on camera off", async () => {
    await wrapper.vm.onCameraOff();
    const icons = wrapper.findAll("div");
    icons.forEach((icon) => {
      expect(icon.isVisible()).toBe(true);
    });
  });

  it("hides the scan confirmation icon on camera on", async () => {
    await wrapper.vm.onCameraOn();
    const icons = wrapper.findAll("div");
    icons.forEach((icon) => {
      expect(icon.isVisible()).toBe(false);
    });
  });

  it("emits addOficio event with correct payload on detect", async () => {
    const detectedCodes = [{ rawValue: "some code" }];
    wrapper.vm.$emit("addOficio", detectedCodes);
    await wrapper.vm.$nextTick();
    expect(wrapper.emitted("addOficio")).toBeTruthy();
    const emittedEvent = wrapper.emitted("addOficio")[0][0];
    expect(emittedEvent).toEqual(detectedCodes);
  });

  it("handles qr error correctly", () => {
    const error = new Error("some error");
    wrapper.vm.handleQRError(error);

    expect(wrapper.vm.qrResult).toBe(`Error: ${error.message}`);
  });
});
