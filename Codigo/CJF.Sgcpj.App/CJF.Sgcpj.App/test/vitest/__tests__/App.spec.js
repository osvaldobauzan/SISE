// All necessary imports
import { shallowMount } from "@vue/test-utils";
import AppVue from "src/App.vue";
import { createRouter, createWebHistory } from "vue-router";
import { expect, it, describe, beforeEach } from "vitest";
// Create mock router
const router = createRouter({
  history: createWebHistory(),
  routes: [],
});
// Unit tests covering all scenarios
describe("App.vue", () => {
  // eslint-disable-next-line no-unused-vars
  let wrapper;
  beforeEach(() => {
    // Mount the component with router
    wrapper = shallowMount(AppVue, {
      global: {
        plugins: [router],
      },
    });
  });
  it("renders the component", () => {
    expect(wrapper.exists()).toBe(true);
  });
  it("should mount the router-view component", () => {
    expect(wrapper.findComponent({ name: "RouterView" }).exists()).toBe(true);
  });
});
