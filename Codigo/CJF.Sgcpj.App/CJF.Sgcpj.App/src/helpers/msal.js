import {
  BrowserCacheLocation,
  PublicClientApplication,
} from "@azure/msal-browser";
import { ref } from "vue";
import { useAuthStore } from "../modules/auth/stores/useAuthStore";
const msalConfig = {
  auth: {
    clientId: process.env.VUE_APP_MSAL_CLIENT_ID,
    authority: process.env.VUE_APP_MSAL_LOGIN_AUTHORITY,
    knownAuthorities: [process.env.VUE_APP_MSAL_KNOWN_AUTHORITY],
  },
  cache: {
    cacheLocation: BrowserCacheLocation.LocalStorage,
  },
  system: {
    loggerOptions: {
      loggerCallback: (level, message, containsPii) => {
        if (containsPii) {
          return;
        }
      },
      piiLoggingEnabled: false,
    },
  },
};

export class MSAL {
  Instance = ref(new PublicClientApplication(msalConfig));
  constructor() {
    this.Instance.value.initialize().then(() => {});
  }
  refreshToken = async function () {
    try {
      await useAuthStore()?.refreshToken(false, true);
    } catch {}
  };
  // eslint-disable-next-line no-unused-vars
  cancelRequest = function (url, method) {
    try {
      if (useAuthStore()?.user?.privilegios?.length < 1) {
        return true;
      }
      return false;
    } catch {
      return false;
    }
  };
}
