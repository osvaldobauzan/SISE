using System.Security.Cryptography;
using System.Text;
using CJF.Sgcpj.Judicatura.Application.Utils;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Common.Models;

namespace Proyectos.Application.Common.Helpers;
public class CryptographicHelper
{
    readonly byte[] _salt = new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 };
    public CryptographicHelper()
    {

    }

    private string CrearContrasenia(ParametrosClave clave)
    {
        // Combinar los nombres de usuario y el nombre del proyecto
        string baseString = string.Join("-", clave.Usuarios) + "-" + clave.Expediente + "_" + clave.Fecha;

        // Aplicar una función de hash
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // Convertir el string en un array de bytes
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(baseString));

            // Convertir los bytes en un string hexadecimal
            StringBuilder builder = new();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

    public async Task<byte[]> EncryptDataAsync(byte[] inputFile, ParametrosClave clave)
    {
        var contrasenia = CrearContrasenia(clave);
        var streamDocument = Functions.MallocFromArrayBytes(inputFile);

        // Configurar el algoritmo de encriptación
        using (Aes aesAlg = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new("contrasenia", _salt);
            aesAlg.Key = pdb.GetBytes(32);
            aesAlg.IV = pdb.GetBytes(16);

            // Crear cifrador
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // Abrir el archivo de entrada
            using (MemoryStream inputFileStream = new MemoryStream(inputFile))
            {
                // Crear el archivo de salida
                using (MemoryStream outputFileStream = new MemoryStream(inputFile))
                {
                    // Crear un stream de cifrado
                    using (CryptoStream cryptoStream = new CryptoStream(outputFileStream, encryptor, CryptoStreamMode.Write))
                    {
                        // Copiar el archivo de entrada al archivo de salida mientras se cifra
                        await inputFileStream.CopyToAsync(cryptoStream);
                        using (var output = new MemoryStream())
                        {
                            cryptoStream.CopyTo(output);
                            return output.ToArray();
                        }
                    }
                }
            }
        }
    }

    public async Task<byte[]> DecryptDataAsync(byte[] inputFile, ParametrosClave clave)
    {
        var contrasenia = CrearContrasenia(clave);
        var streamDocument = Functions.MallocFromArrayBytes(inputFile);

        // Configurar el algoritmo de desencriptación
        using (Aes aesAlg = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new("contrasenia", _salt);
            aesAlg.Key = pdb.GetBytes(32);
            aesAlg.IV = pdb.GetBytes(16);

            // Crear descifrador
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            // Abrir el archivo encriptado
            using (MemoryStream inputFileStream = new MemoryStream(inputFile))
            {
                // Crear el archivo de salida
                using (MemoryStream outputFileStream = new MemoryStream())
                {
                    // Crear un stream de descifrado
                    using (CryptoStream cryptoStream = new CryptoStream(inputFileStream, decryptor, CryptoStreamMode.Read))
                    {
                        // Copiar el archivo encriptado al archivo de salida mientras se descifra
                        await cryptoStream.CopyToAsync(outputFileStream);
                        using (var output = new MemoryStream())
                        {
                            outputFileStream.CopyTo(output);
                            return output.ToArray();
                        }
                    }
                }
            }
        }
    }

    public byte[] EncryptData(byte[] data, ParametrosClave clave)
    {
        var contrasenia = CrearContrasenia(clave);
        using (Aes aesAlg = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new(contrasenia, _salt);
            aesAlg.Key = pdb.GetBytes(32);
            aesAlg.IV = pdb.GetBytes(16);

            using (MemoryStream msEncrypt = new())
            {
                using (CryptoStream csEncrypt = new(msEncrypt, aesAlg.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    csEncrypt.Write(data, 0, data.Length);
                    csEncrypt.Close();
                }
                return msEncrypt.ToArray();
            }
        }
    }

    public byte[] DecryptData(byte[] data, ParametrosClave clave)
    {
        var contrasenia = CrearContrasenia(clave);
        using (Aes aesAlg = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new(contrasenia, _salt);
            aesAlg.Key = pdb.GetBytes(32);
            aesAlg.IV = pdb.GetBytes(16);

            using (MemoryStream msDecrypt = new())
            {
                using (CryptoStream csDecrypt = new(msDecrypt, aesAlg.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    csDecrypt.Write(data, 0, data.Length);
                    csDecrypt.Close();
                }
                return msDecrypt.ToArray();
            }
        }
    }
}
