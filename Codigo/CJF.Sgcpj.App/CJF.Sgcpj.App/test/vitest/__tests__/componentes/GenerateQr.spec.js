import { shallowMount } from "@vue/test-utils";
import GenerateQr from "../../../../src/components/GenerateQr.vue"; // replace with the path to your actual component
// eslint-disable-next-line no-unused-vars
import { describe, it, expect, afterEach, vi } from "vitest";
import QRious from "qrious";

vi.mock("qrious");

describe("QR Code Component", () => {
  let wrapper;

  const props = {
    modelValue: "example data",
    autoPrint: false,
    esHtml: false,
    descripcion: "QR Description",
  };

  const createComponent = (propsData = props) => {
    wrapper = shallowMount(GenerateQr, {
      propsData,
    });
  };

  afterEach(() => {
    wrapper.unmount();
    vi.clearAllMocks();
  });

  it("should generate a QR code and not auto print on mount", async () => {
    createComponent();
    window.open = vi.fn().mockImplementation(() => ({
      document: { write: vi.fn(), close: vi.fn() },
      focus: vi.fn(),
      print: vi.fn(),
      close: vi.fn(),
    }));
    const printSpy = vi.spyOn(wrapper.vm, "imprimirQr");

    await wrapper.vm.$nextTick();

    expect(printSpy).not.toHaveBeenCalled();
    expect(QRious).toHaveBeenCalledTimes(1);
  });
  it("renders description if esHtml is false", () => {
    createComponent({ descripcion: "QR code description", esHtml: false });
    const description = wrapper.find("#qr-container div:not([id])");
    expect(description.text()).toBe("QR code description");
  });
  it("should insert HTML into qrDescripcion when esHtml is true", async () => {
    createComponent({ ...props, esHtml: true });
    await wrapper.vm.$nextTick();

    const qrDescripcion = wrapper.find("#qrDescripcion");
    expect(qrDescripcion.exists()).toBe(true);
  });

  it("should not print automatically if autoPrint is false", async () => {
    vi.spyOn(window, "open").mockImplementation(() => ({
      focus: vi.fn(),
      print: vi.fn(),
      close: vi.fn(),
      document: {
        write: vi.fn(),
        close: vi.fn(),
      },
    }));
    createComponent({ autoPrint: false });
    expect(window.open).not.toHaveBeenCalled();
  });

  it('should update "modelValue" when calling set method of computed property value', async () => {
    createComponent();
    const newValue = "new example data";

    wrapper.vm.value = newValue;
    await wrapper.vm.$nextTick();

    expect(wrapper.emitted()["update:modelValue"][0]).toEqual([newValue]);
  });
  it("should printing", async () => {
    createComponent();
    const val = wrapper.vm.imprimirQr();
    await wrapper.vm.$nextTick();
    expect(val).toEqual(false);
  });
});
