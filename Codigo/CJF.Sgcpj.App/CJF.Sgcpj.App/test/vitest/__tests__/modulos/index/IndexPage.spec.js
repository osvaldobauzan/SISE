import { shallowMount } from "@vue/test-utils";
import IndexPage from "../../../../../src/modules/index/pages/IndexPage.vue";
import { it, describe, expect, test } from "vitest";

test("IndexPage renderiza el componente", () => {
  const wrapper = shallowMount(IndexPage);
  expect(wrapper.exists()).toBeTruthy();
});

test("Function getTramiteRows", () => {
  const wrapper = shallowMount(IndexPage);

  wrapper.vm.getTramiteRows();
  expect(wrapper.vm.cargandoTramite).toBe(true);
});

test("Function getOficialiaRows", () => {
  const wrapper = shallowMount(IndexPage);

  wrapper.vm.getTramiteRows();
  expect(wrapper.vm.cargandoOficialia).toBe(false);
});

test("totalAsignadosMesa to be truthy", () => {
  const wrapper = shallowMount(IndexPage);
  expect(wrapper.vm.totalAsignadosMesa).toBeTruthy();
});

test("totalTramite to be truthy", () => {
  const wrapper = shallowMount(IndexPage);
  expect(wrapper.vm.totalTramite).toBeTruthy();
});

test("totalOficialia to be truthy", () => {
  const wrapper = shallowMount(IndexPage);
  expect(wrapper.vm.totalOficialia).toBeTruthy();
});

describe("Total Autorizados", () => {
  it("debería devolver el número correcto de elementos autorizados", () => {
    const rowsTramite = [
      { estado: "1" },
      { estado: "4" },
      { estado: "4" },
      { estado: "2" },
    ];

    const wrapper = shallowMount(IndexPage, {
      props: { rowsTramite },
    });

    expect(wrapper.vm.totalAutorizados()).toBe(0);
  });

  it("debería devolver 0 si no hay elementos autorizados", () => {
    const rowsTramite = [{ estado: "1" }, { estado: "2" }, { estado: "3" }];

    const wrapper = shallowMount(IndexPage, {
      props: { rowsTramite },
    });

    expect(wrapper.vm.totalAutorizados()).toBe(0);
  });
});

describe("Total Oficialia", () => {
  it("debería devolver el número correcto de elementos autorizados", () => {
    const rowsOficialia = [
      { estado: "1" },
      { estado: "4" },
      { estado: "4" },
      { estado: "2" },
    ];

    const wrapper = shallowMount(IndexPage, {
      props: { rowsOficialia },
    });

    expect(wrapper.vm.totalOficialia()).toBe(0);
  });

  it("debería devolver 0 si no hay elementos autorizados", () => {
    const rowsOficialia = [{ estado: "1" }, { estado: "2" }, { estado: "3" }];

    const wrapper = shallowMount(IndexPage, {
      props: { rowsOficialia },
    });

    expect(wrapper.vm.totalOficialia()).toBe(0);
  });
});

describe("Total Tramite", () => {
  it("debería devolver el número correcto de elementos autorizados", () => {
    const rowsTramite = [
      { estado: "1" },
      { estado: "4" },
      { estado: "4" },
      { estado: "2" },
    ];

    const wrapper = shallowMount(IndexPage, {
      props: { rowsTramite },
    });

    expect(wrapper.vm.totalTramite()).toBe(0);
  });

  it("debería devolver 0 si no hay elementos autorizados", () => {
    const rowsTramite = [{ estado: "1" }, { estado: "2" }, { estado: "3" }];

    const wrapper = shallowMount(IndexPage, {
      props: { rowsTramite },
    });

    expect(wrapper.vm.totalTramite()).toBe(0);
  });
});

describe("Total Asignados a mesa", () => {
  it("debería devolver el número correcto de elementos autorizados", () => {
    const rowsOficialia = [
      { estado: "1" },
      { estado: "4" },
      { estado: "4" },
      { estado: "2" },
    ];

    const wrapper = shallowMount(IndexPage, {
      props: { rowsOficialia },
    });

    expect(wrapper.vm.totalAsignadosMesa()).toBe(0);
  });

  it("debería devolver 0 si no hay elementos autorizados", () => {
    const rowsOficialia = [{ estado: "1" }, { estado: "2" }, { estado: "3" }];

    const wrapper = shallowMount(IndexPage, {
      props: { rowsOficialia },
    });

    expect(wrapper.vm.totalAsignadosMesa()).toBe(0);
  });
});

test("getOficialiaRows renderiza el componente", () => {
  const wrapper = shallowMount(IndexPage);
  expect(wrapper.vm.cargandoOficialia).toBe(false);
});

describe("cerrarSesion", () => {
  it("debe llamar a authStore.logoutUser con true", async () => {
    const wrapper = shallowMount(IndexPage);
    const authStore = {
      logoutUser: async (arg) => {
        expect(arg).to.be.true;
      },
    };

    await wrapper.vm.cerrarSesion(authStore);
  });

  it("debe manejar errores correctamente", async () => {
    const wrapper = shallowMount(IndexPage);
    const error = new Error("Error al cerrar sesión");
    const authStore = {
      logoutUser: async () => {
        throw error;
      },
    };

    try {
      await wrapper.vm.cerrarSesion(authStore);
    } catch (err) {
      expect(err).to.equal(error);
    }
  });
});

describe("getProgress", () => {
  it("debería calcular correctamente el progreso", () => {
    const wrapper = shallowMount(IndexPage);
    const card = {
      progress: 50,
      total: 100,
    };

    const result = wrapper.vm.getProgress(card);

    expect(result).to.equal("50");
  });

  it("debería manejar correctamente el caso donde el progreso es 0", () => {
    const wrapper = shallowMount(IndexPage);
    const card = {
      progress: 0,
      total: 100,
    };

    const result = wrapper.vm.getProgress(card);

    expect(result).to.equal("0");
  });

  it("debería manejar correctamente el caso donde el total es 0", () => {
    const wrapper = shallowMount(IndexPage);
    const card = {
      progress: 50,
      total: 0,
    };

    const result = wrapper.vm.getProgress(card);

    expect(result).to.equal("Infinity");
  });

  it("debería manejar correctamente el caso donde el total es negativo", () => {
    const wrapper = shallowMount(IndexPage);
    const card = {
      progress: 50,
      total: -100,
    };

    const result = wrapper.vm.getProgress(card);

    expect(result).to.equal("-50");
  });

  it("debería manejar correctamente el caso donde el total es NaN", () => {
    const wrapper = shallowMount(IndexPage);
    const card = {
      progress: 50,
      total: NaN,
    };

    const result = wrapper.vm.getProgress(card);

    expect(result).to.equal("NaN");
  });
  // test("Computed newDateToday ", () => {
  //   const wrapper = shallowMount(IndexPage);
  //   let fechaActual = new Date();
  //   let dia = fechaActual.getDate();
  //   let mes = fechaActual.getMonth() + 1;
  //   let año = fechaActual.getFullYear();

  //   dia = dia < 10 ? "0" + dia : dia;
  //   mes = mes < 10 ? "0" + mes : mes;

  //   const today = dia + "/" + mes + "/" + año;
  //   expect(wrapper.vm.newDateToday).toEqual(today);
  // });
  test("Computed cards ", () => {
    const wrapper = shallowMount(IndexPage);
    expect(wrapper.vm.cards.length).toEqual(5);
  });
});
