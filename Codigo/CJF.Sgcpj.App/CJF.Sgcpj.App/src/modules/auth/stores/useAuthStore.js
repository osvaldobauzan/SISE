import { defineStore } from "pinia";
import { msal, apiSeguridad } from "../../../boot/axios";
import { Utils } from "../../../helpers/utils";
import { Cifrado } from "../../../helpers/cifrado";

export const useAuthStore = defineStore("auth", {
  state: () => ({
    user: {},
    token: null,
    organismos: [],
  }),
  actions: {
    async logIn() {
      // handle auth redired/do all initial setup for msal
      await msal
        .handleRedirectPromise()
        .then(async (r) => {
          if (r) {
            msal.setActiveAccount(r);
            await this.refreshToken(true);
          }
          // Check if user signed in
          const account = msal.getActiveAccount();
          if (!account) {
            // redirect anonymous user to login page
            msal.loginRedirect({
              scopes: ["openid", "profile", "offline_access"],
              onRedirectNavigate: async () => {
                localStorage.setItem(
                  "sesionId",
                  Cifrado.encrypteData(Utils.uuidv4()),
                );
                await this.refreshToken(true);
              },
            });
          }
        })
        .catch(() => {});
    },
    async logoutUser(omitirBack = false) {
      try {
        if (!omitirBack) {
          await this.cerrarSesion();
        }
      } catch {}

      // handle auth redired/do all initial setup for msal
      msal
        .handleRedirectPromise()
        .then(() => {
          // redirect anonymous user to login page
          msal.logoutRedirect({
            // postLogoutRedirectUri: "/#/auth/login"
          });
        })
        .catch(() => {});
      localStorage.removeItem("token");
      localStorage.removeItem("user");
      localStorage.removeItem("org_id");
      localStorage.removeItem("sesionId");
      localStorage.removeItem("wsConnection");
      localStorage.removeItem("organismos");
      this.user = {};
      this.token = null;
    },
    async refreshToken(isLogin = false, refrescar = false) {
      try {
        let account = msal.getActiveAccount();
        const expiroToken =
          !!!account ||
          new Date(account.idTokenClaims.exp * 1000) < new Date() ||
          refrescar;
        if (expiroToken) {
          account = await msal.acquireTokenSilent({
            account: msal.getActiveAccount(),
          });
          msal.setActiveAccount(account);
        }
        this.token = account.idToken || account.idTokenClaims;
        localStorage.setItem("token", this.token);
        try {
          this.user = localStorage.getItem("user")
            ? JSON.parse(Cifrado.decrypteData(localStorage.getItem("user")))
            : localStorage.getItem("user");
          this.organismos = localStorage.getItem("organismos")
            ? JSON.parse(
                Cifrado.decrypteData(localStorage.getItem("organismos")),
              )
            : localStorage.getItem("organismos");
          if (!isLogin && expiroToken) {
            await this.refrescarSesion();
          }
        } catch (error) {
          if (!isLogin) {
            this.logoutUser(true);
          }
        }
        if (!this.user) {
          this.user = {
            empleadoId:
              msal.getActiveAccount().idTokenClaims?.extension_EmpleadoId,
          };
        }
      } catch {
        await msal.acquireTokenRedirect({ account: msal.getActiveAccount() });
      }
    },
    async getOrganismos() {
      this.organismos = (await apiSeguridad.get("api/organismos")).data;
    },
    async crearSesion(organismo) {
      localStorage.setItem(
        "org_id",
        Cifrado.encrypteData(organismo.catOrganismoId),
      );
      localStorage.setItem(
        "organismos",
        Cifrado.encrypteData(JSON.stringify(this.organismos)),
      );
      this.user = (
        await apiSeguridad.post("api/sesion", {
          idCatOrganismo: organismo.catOrganismoId,
          refreshToken: Cifrado.decrypteData(localStorage.getItem("sesionId")),
        })
      ).data;
      localStorage.setItem(
        "user",
        Cifrado.encrypteData(JSON.stringify(this.user)),
      );
      localStorage.setItem("pInicio", this.user.paginaInicio);
    },
    async refrescarSesion() {
      if (this.user?.catOrganismoId) {
        await apiSeguridad.put("api/sesion", {
          idCatOrganismo: Cifrado.decrypteData(localStorage.getItem("org_id")),
          refreshToken: Cifrado.decrypteData(localStorage.getItem("sesionId")),
        });
      }
    },
    async cerrarSesion() {
      await apiSeguridad.delete("api/sesion", {
        data: {
          idCatOrganismo: Cifrado.decrypteData(localStorage.getItem("org_id")),
          refreshToken: Cifrado.decrypteData(localStorage.getItem("sesionId")),
        },
      });
    },
  },
});
