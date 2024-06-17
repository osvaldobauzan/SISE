import { shallowMount } from "@vue/test-utils";
import ErrorNotFound from "src/pages/ErrorNotFound.vue";
import { createRouter, createWebHistory } from "vue-router";
import { Quasar, QBtn } from "quasar";
import { expect, it, describe } from "vitest";

// Set up router
const routes = [{ path: "/", component: { template: "Home page content" } }];
const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

// Define Quasar Plugin for shallowMount
const quasarPlugin = {
  install: (app) => {
    app.config.globalProperties.$q = {
      // Mocking quasar globals
      platform: {},
    };
    app.use(Quasar, { components: { QBtn } }); // Specify components used
  },
};

describe("ErrorNotFound.vue", () => {
  it("renders the error message", () => {
    const wrapper = shallowMount(ErrorNotFound, {
      global: {
        plugins: [router, quasarPlugin],
      },
    });
    expect(wrapper.text()).toContain("404");
    expect(wrapper.text()).toContain("No se encuentra la página que busca...");
  });

  it('has a button with label "Ir al inicio"', () => {
    const wrapper = shallowMount(ErrorNotFound, {
      global: {
        plugins: [router, quasarPlugin],
      },
    });
    const button = wrapper.findComponent(QBtn);
    expect(button.exists()).toBe(true);
    expect(button.props().label).toBe("Ir al inicio");
  });

  it("the button routes to home page when clicked", async () => {
    const wrapper = shallowMount(ErrorNotFound, {
      global: {
        plugins: [router, quasarPlugin],
      },
    });
    router.push("/not-found-page");
    await router.isReady();
    const button = wrapper.findComponent(QBtn);
    await button.trigger("click");
    expect(router.currentRoute.value.path).toBe("/");
  });

  it("4096 scenario", () => {
    // This is a contrived example as the component does not contain dynamic behavior
    // that would result in different output for "4096" scenario.
    // Therefore, this unit test will be like the first test case.
    const wrapper = shallowMount(ErrorNotFound, {
      global: {
        plugins: [router, quasarPlugin],
      },
    });
    expect(wrapper.text()).toContain("404");
    expect(wrapper.text()).toContain("No se encuentra la página que busca...");
  });
});
