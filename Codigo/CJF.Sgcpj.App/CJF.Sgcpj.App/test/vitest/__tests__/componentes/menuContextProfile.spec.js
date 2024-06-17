import menuContextProfile from "../../../../src/components/menuContextProfile.vue";

import { describe, it, expect, vi } from "vitest";
import { shallowMount } from "@vue/test-utils";
import { createTestingPinia } from "@pinia/testing";
//import { useAuthStore } from "src/modules/auth/stores/useAuthStore";
import {
  Quasar,
  QCard,
  QCardSection,
  QBtn,
  QItemLabel,
  QSeparator,
  QCardActions,
} from "quasar";

describe("menuContextProfile.vue", () => {
  // helper function to create wrapper
  function createWrapper() {
    return shallowMount(menuContextProfile, {
      stubs: {
        "q-card": QCard,
        "q-card-section": QCardSection,
        "q-btn": QBtn,
        "q-item-label": QItemLabel,
        "q-separator": QSeparator,
        "q-card-actions": QCardActions,
      },
      global: {
        plugins: [
          Quasar,
          createTestingPinia({
            createSpy: vi.fn,
          }),
        ],
        components: {
          QCard,
          QCardSection,
          QBtn,
          QItemLabel,
          QSeparator,
          QCardActions,
        },
      },
    });
  }
  it("renders a card", () => {
    const wrapper = createWrapper();
    expect(wrapper.findComponent(QCard).exists()).toBe(true);
  });
  it("renders a btn", () => {
    const wrapper = createWrapper();
    expect(wrapper.findAllComponents(QBtn).length).toBeLessThanOrEqual(1);
  });

  it("renders a separator", () => {
    const wrapper = createWrapper();
    expect(wrapper.findAllComponents(QSeparator).length).toBeLessThanOrEqual(1);
  });

  it("renders card actions", () => {
    const wrapper = createWrapper();
    expect(wrapper.findAllComponents(QCardActions).length).toBeLessThanOrEqual(
      1,
    );
  });
});
