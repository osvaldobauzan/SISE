import CardStatusComponent from "../../../../src/components/CardStatusComponent.vue";
import { mount } from "@vue/test-utils";
import { describe, expect, test, beforeEach } from "vitest";
import { Quasar } from "quasar";
describe("CardStatusComponent", () => {
  let listCards;
  let wrapper;

  beforeEach(() => {
    listCards = [
      {
        key: "tramite",
        name: "In Process",
        progress: 50,
        total: 100,
        icon: "icon-name-1",
      },
      {
        key: "completed",
        name: "Completed",
        progress: 100,
        total: 100,
        icon: "icon-name-2",
      },
      {
        key: "pending",
        name: "Pending",
        progress: 0,
        total: 100,
        icon: "icon-name-3",
      },
    ];

    wrapper = mount(CardStatusComponent, {
      propsData: { listCards: listCards },
      global: {
        plugins: [Quasar],
      },
    });

    wrapper.vm.selectedStatus = "tramite"; // starting with 'tramite' as selected
  });

  test("renders correct number of list cards", () => {
    expect(wrapper.findAll(".q-card").length).toBe(listCards.length);
  });

  test("check if icons are displayed correctly", () => {
    const iconWrappers = wrapper.findAll(".q-icon");
    expect(iconWrappers.length).toBe(listCards.length);
    iconWrappers.forEach((iconWrapper, index) => {
      expect(iconWrapper.classes()).toContain(listCards[index].icon);
    });
  });

  test("getProgress returns correct value", () => {
    listCards.forEach((card) => {
      const progress = wrapper.vm.getProgress(card);
      const expectedProgress = parseFloat(
        (card.progress / card.total) * 100,
      ).toFixed(0);
      expect(progress).toBe(expectedProgress);
    });
  });
});
