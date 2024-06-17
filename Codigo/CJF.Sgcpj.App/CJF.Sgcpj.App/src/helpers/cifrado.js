import CryptoJS from "crypto-js";
const getKey = () => {
  let keyValue = process.env.CRYPTO_SECRET_KEY;
  let key = "";
  keyValue = btoa(keyValue);
  while (keyValue.length < 65) {
    keyValue = btoa(keyValue);
  }
  for (let index = 0; index < 65; index++) {
    if (index % 2 === 0) {
      key += keyValue.split("")[index];
    }
  }
  return key;
};
export class Cifrado {
  static encrypteData(data) {
    if (data) {
      const cipher = CryptoJS.AES.encrypt(`${data}`, getKey(), {
        mode: CryptoJS.mode.ECB,
        padding: CryptoJS.pad.Pkcs7,
      });
      return cipher.toString();
    }
  }
  static decrypteData(data) {
    if (data) {
      const cipher = CryptoJS.AES.decrypt(data.toString(), getKey(), {
        mode: CryptoJS.mode.ECB,
        padding: CryptoJS.pad.Pkcs7,
      });
      return cipher.toString(CryptoJS.enc.Utf8);
    }
  }
}
