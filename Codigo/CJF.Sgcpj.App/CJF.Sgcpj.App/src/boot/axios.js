import { boot } from "quasar/wrappers";
import axios from "axios";
import VueApexCharts from "vue3-apexcharts";
import { MSAL } from "../helpers/msal";
import { setupCache } from "axios-cache-interceptor";

const msalClass = new MSAL();
// Be careful when using SSR for cross-request state pollution
// due to creating a Singleton instance here;
// If any client changes this (global) instance, it might be a
// good idea to move this instance creation inside of the
// "export default () => {}" function below (which runs individually
// for each client)

const requestInterceptor = {
  config: async (config) => {
    const controller = new AbortController();
    config.headers = {
      Authorization: `Bearer ${localStorage.getItem("token")}`,
      Accept: "application/json",
      "Cache-Control": "no-cache, no-store, must-revalidate",
      "Content-Security-Policy": `default-src 'self'; connect-src 'self' ${process.env.API_CONNECT_CSP}; script-src 'self'; style-src 'self' 'unsafe-inline'; img-src 'self' blob: data:; object-src 'self' blob:; frame-src 'self' blob:; form-action 'self'`,
      "Content-Type": "application/json",
      ...config.headers,
    };
    if (msalClass.cancelRequest(config.url, config.method)) {
      controller.abort();
    }
    config.responseType = "json";
    return { ...config, signal: controller.signal };
  },
  error: (error) => Promise.reject(error),
};
const responseInterceptor = {
  response: (response) => response,
  error: async function (error, axiosApiInstance) {
    let originalRequest = error.config;
    if (error?.code == "ERR_CANCELED") {
      return Promise.resolve(true);
    }
    if (
      error.response.status === 401 &&
      !originalRequest._retry &&
      (originalRequest.url !== "api/sesion" || originalRequest.method !== "put")
    ) {
      originalRequest._retry = true;
      await msalClass.refreshToken();
      originalRequest.headers["Authorization"] = `Bearer ${localStorage.getItem(
        "token",
      )}`;
      return axiosApiInstance(originalRequest);
    }
    return Promise.reject(error);
  },
};

const apiOficialia = axios.create({ baseURL: process.env.API_OFICIALIA });
apiOficialia.interceptors.request.use(
  requestInterceptor.config,
  requestInterceptor.error,
);
apiOficialia.interceptors.response.use(responseInterceptor.response, (error) =>
  responseInterceptor.error(error, apiOficialia),
);

const apiSentencias = axios.create({ baseURL: process.env.API_SENTENCIAS });
apiSentencias.interceptors.request.use(
  requestInterceptor.config,
  requestInterceptor.error,
);
apiSentencias.interceptors.response.use(responseInterceptor.response, (error) =>
  responseInterceptor.error(error, apiSentencias),
);

const apiProyectos = axios.create({ baseURL: process.env.API_PROYECTOS });
apiProyectos.interceptors.request.use(
  requestInterceptor.config,
  requestInterceptor.error,
);
apiProyectos.interceptors.response.use(responseInterceptor.response, (error) =>
  responseInterceptor.error(error, apiProyectos),
);

const apiCatalogos = setupCache(
  axios.create({ baseURL: process.env.API_CATALOGOS }),
);
apiCatalogos.interceptors.request.use(
  requestInterceptor.config,
  requestInterceptor.error,
);
apiCatalogos.interceptors.response.use(responseInterceptor.response, (error) =>
  responseInterceptor.error(error, apiCatalogos),
);
const apiUsuarios = setupCache(
  axios.create({ baseURL: process.env.API_USUARIOS }),
);
apiUsuarios.interceptors.request.use(
  requestInterceptor.config,
  requestInterceptor.error,
);
apiUsuarios.interceptors.response.use(responseInterceptor.response, (error) =>
  responseInterceptor.error(error, apiUsuarios),
);
const apiPromoventes = setupCache(
  axios.create({ baseURL: process.env.API_PROMOVENTES }),
);
apiPromoventes.interceptors.request.use(
  requestInterceptor.config,
  requestInterceptor.error,
);
apiPromoventes.interceptors.response.use(
  responseInterceptor.response,
  (error) => responseInterceptor.error(error, apiPromoventes),
);
const apiAlertas = axios.create({ baseURL: process.env.API_ALERTAS });
apiAlertas.interceptors.request.use(
  requestInterceptor.config,
  requestInterceptor.error,
);
apiAlertas.interceptors.response.use(responseInterceptor.response, (error) =>
  responseInterceptor.error(error, apiAlertas),
);
const apiTramites = axios.create({ baseURL: process.env.API_TRAMITES });
apiTramites.interceptors.request.use(
  requestInterceptor.config,
  requestInterceptor.error,
);
apiTramites.interceptors.response.use(responseInterceptor.response, (error) =>
  responseInterceptor.error(error, apiTramites),
);
const apiActuaria = axios.create({ baseURL: process.env.API_ACTUARIA });
apiActuaria.interceptors.request.use(
  requestInterceptor.config,
  requestInterceptor.error,
);
apiActuaria.interceptors.response.use(responseInterceptor.response, (error) =>
  responseInterceptor.error(error, apiActuaria),
);
const apiSeguridad = axios.create({ baseURL: process.env.API_SEGURIDAD });
apiSeguridad.interceptors.request.use(
  requestInterceptor.config,
  requestInterceptor.error,
);
apiSeguridad.interceptors.response.use(responseInterceptor.response, (error) =>
  responseInterceptor.error(error, apiSeguridad),
);
const apiLibreta = axios.create({ baseURL: process.env.API_LIBRETA });
apiLibreta.interceptors.request.use(
  requestInterceptor.config,
  requestInterceptor.error,
);
apiLibreta.interceptors.response.use(responseInterceptor.response, (error) =>
  responseInterceptor.error(error, apiLibreta),
);

const apiSeguimiento = axios.create({ baseURL: process.env.API_SEGUIMIENTO });
apiSeguimiento.interceptors.request.use(
  requestInterceptor.config,
  requestInterceptor.error,
);
apiSeguimiento.interceptors.response.use(
  responseInterceptor.response,
  (error) => responseInterceptor.error(error, apiSeguimiento),
);

const apiExpedienteElectronico = axios.create({
  baseURL: process.env.API_EXPEDIENTE,
});
apiExpedienteElectronico.interceptors.request.use(
  requestInterceptor.config,
  requestInterceptor.error,
);
apiExpedienteElectronico.interceptors.response.use(
  responseInterceptor.response,
  (error) => responseInterceptor.error(error, apiExpedienteElectronico),
);
const apiAgenda = axios.create({ baseURL: process.env.API_AGENDA });

apiAgenda.interceptors.request.use(
  requestInterceptor.config,
  requestInterceptor.error,
);
apiAgenda.interceptors.response.use(responseInterceptor.response, (error) =>
  responseInterceptor.error(error, apiAgenda),
);

const msal = msalClass.Instance.value;

export default boot(({ app }) => {
  // for use inside Vue files (Options API) through this.$axios and this.$api
  // eslint-disable-next-line no-unused-vars
  app.config.warnHandler = (msg, instance, trace) => {
    // Desactiva los warnings
  };
  app.config.globalProperties.$axios = axios;
  // ^ ^ ^ this will allow you to use this.$axios (for Vue Options API form)
  //       so you won't necessarily have to import axios in each vue file

  //app.config.globalProperties.$api = api;
  // ^ ^ ^ this will allow you to use this.$api (for Vue Options API form)
  //       so you can easily perform requests against your app's API
  app.config.globalProperties.$msal = msal;
  app.use(VueApexCharts);
  // app.use(msal);
});

export {
  axios,
  apiOficialia,
  apiCatalogos,
  apiUsuarios,
  apiPromoventes,
  apiAlertas,
  apiTramites,
  apiSeguridad,
  apiLibreta,
  apiProyectos,
  apiSeguimiento,
  apiActuaria,
  apiExpedienteElectronico,
  apiSentencias,
  apiAgenda,
  msal,
};
