import { expect, it, describe } from "vitest";
import { mount } from "@vue/test-utils";
import { Quasar, QCard, QCardSection, QBtn, QTooltip } from "quasar";
import menuContextApps from "../../../../src/components/menuContextApps.vue"; // Update with actual path

describe("menuContextApps", () => {
  const wrapper = mount(menuContextApps, {
    stubs: {
      "q-card": QCard,
      "q-card-section": QCardSection,
      "q-btn": QBtn,
      "q-tooltip": QTooltip,
      "router-link": true,
    },
    global: {
      plugins: [Quasar],
      components: { QCard, QCardSection, QBtn, QTooltip },
    },
  });

  it("renders the correct number of buttons", () => {
    expect(wrapper.findAllComponents(QBtn).length).toBe(5);
  });
  let index = 0;
  it.each([
    { icon: "mdi-notebook", to: "oficios", tooltip: "Libreta de oficios" },
    {
      icon: "mdi-arrow-decision",
      to: "seguimiento",
      tooltip: "Seguimiento de expedientes",
    },
    { icon: "mdi-folder", to: "expediente", tooltip: "Expediente electrónico" },
    { icon: "mdi-calculator", to: "calculadoras", tooltip: "Calculadoras" },
    { icon: "mdi-cog", to: "configuracion", tooltip: "Configuración" },
  ])('renders a button with icon "$icon" and route "$to"', ({ icon, to }) => {
    const button = wrapper.findAllComponents(QBtn).at(index);

    expect(button.exists()).toBe(true);
    expect(button.props("icon")).toBe(icon);
    expect(button.props("to")).toBe(to);
    index++;
  });

  it("buttons have flat style and correct color", () => {
    const buttons = wrapper.findAllComponents(QBtn);
    buttons.forEach((btnWrapper) => {
      expect(btnWrapper.props("flat")).toBe(true);
      expect(btnWrapper.props("color")).toBe("grey-8");
      expect(btnWrapper.props("size")).toBe("xl");
    });
  });
});
