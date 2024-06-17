import { mount, shallowMount } from "@vue/test-utils";
import EssentialLink from "../../../../src/components/EssentialLink.vue"; // Assuming the component is located at src/components/EssentialLink.vue
import { describe, expect, it, beforeEach } from "vitest";
import { Quasar } from "quasar";
describe("EssentialLink", () => {
  let props;

  beforeEach(() => {
    props = {
      title: "Test Title",
      caption: "Test Caption",
      link: "https://www.example.com",
      icon: "mdi-email-outline",
    };
  });

  it("renders with required props", () => {
    const wrapper = mount(EssentialLink, {
      propsData: props,
      global: {
        plugins: [Quasar],
      },
    });

    expect(wrapper.text()).toContain(props.title);
    expect(wrapper.find("a").attributes().href).toContain(props.link);
  });

  it("renders with all props given", () => {
    const wrapper = mount(EssentialLink, {
      propsData: props,
      global: {
        plugins: [Quasar],
      },
    });
    expect(wrapper.text()).toContain(props.title);
    expect(wrapper.text()).toContain(props.caption);
    expect(wrapper.find("a").attributes().href).toBe(props.link);
    expect(wrapper.find(".q-icon").exists()).toBe(true);
    expect(wrapper.find(".q-icon").classes()).toContain(props.icon);
  });

  it("does not render an icon if the icon prop is not provided", () => {
    const wrapper = shallowMount(EssentialLink, {
      propsData: {
        title: props.title,
        caption: props.caption,
        link: props.link,
      },
    });

    expect(wrapper.find("q-icon").exists()).toBe(false);
  });

  it("renders default link if link prop is not provided", () => {
    const wrapper = mount(EssentialLink, {
      propsData: {
        title: props.title,
        caption: props.caption,
      },
      global: {
        plugins: [Quasar],
      },
    });

    expect(wrapper.find("a").attributes().href).toBe("#");
  });

  it("renders default caption if caption prop is not provided", () => {
    const wrapper = mount(EssentialLink, {
      propsData: {
        title: props.title,
        link: props.link,
      },
      global: {
        plugins: [Quasar],
      },
    });

    expect(wrapper.text()).toContain(props.title);
    expect(wrapper.find("q-item-caption").exists()).toBe(true);
    expect(wrapper.find("q-item-caption").text()).toBe("");
  });

  it("renders default icon value if icon prop is not provided", () => {
    const wrapper = shallowMount(EssentialLink, {
      propsData: {
        title: props.title,
        caption: props.caption,
        link: props.link,
      },
    });

    expect(wrapper.vm.icon).toBe("");
  });
});
