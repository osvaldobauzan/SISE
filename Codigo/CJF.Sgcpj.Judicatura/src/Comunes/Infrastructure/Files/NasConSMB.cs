using System.Net;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Exceptions;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using Microsoft.Extensions.Configuration;
using SMBLibrary;
using SMBLibrary.Client;

namespace CJF.Sgcpj.Judicatura.Common.Infrastructure.Files;
public class NasConSMB : INasArchivo
{

    private readonly IConfiguration _configuration;

    public NasConSMB(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    static bool EsDireccionIP(string cadena)
    {
        IPAddress address;
        bool esValida = IPAddress.TryParse(cadena, out address);
        return esValida;
    }
    public DocumentoBase64Dto ObtenerArchivoComoBase64String(string uncPath)
    {
        var archivoBase64Dto = new DocumentoBase64Dto();
        var nasActivo = _configuration.GetValue<bool>("SISE3:BackEnd:NASActivo");

        if (nasActivo)
        {
            SMB2Client client = new SMB2Client();

            var ip = ObtenerHost(uncPath);
            var user = _configuration["SISE3:BackEnd:NASUsuario"];
            var pass = _configuration["SISE3:BackEnd:NASContrasena"];
            var isConnected = false;

            if (EsDireccionIP(ip))
            {
                isConnected = client.Connect(IPAddress.Parse(ip), SMBTransportType.DirectTCPTransport);
            }
            else
            {
                isConnected = client.Connect(ip, SMBTransportType.DirectTCPTransport);
            }
         
            if (isConnected)
            {
                NTStatus status = client.Login(_configuration["SISE3:BackEnd:NASDominioUsuario"], user, pass);
                ISMBFileStore fileStore = null;
                try
                {
                    var carpetaCompartida = ObtenerCarpetaCompartida(uncPath);
                    fileStore = client.TreeConnect(carpetaCompartida, out status);
                    object fileHandle;
                    FileStatus fileStatus;

                    uncPath = CortarPath(uncPath,carpetaCompartida);
                    var nasMaxReadSize = _configuration.GetValue<int>("SISE3:BackEnd:NASMaxReadSize");
                    status = fileStore.CreateFile(out fileHandle, out fileStatus, uncPath, AccessMask.GENERIC_READ | AccessMask.SYNCHRONIZE, SMBLibrary.FileAttributes.Normal, ShareAccess.Read, CreateDisposition.FILE_OPEN, CreateOptions.FILE_NON_DIRECTORY_FILE | CreateOptions.FILE_SYNCHRONOUS_IO_ALERT, null);

                    if (status == NTStatus.STATUS_SUCCESS)
                    {
                        MemoryStream stream = new MemoryStream();
                        byte[] data;
                        long bytesRead = 0;
                        while (true)
                        {
                            status = fileStore.ReadFile(out data, fileHandle, bytesRead, nasMaxReadSize);
                            if (status != NTStatus.STATUS_SUCCESS && status != NTStatus.STATUS_END_OF_FILE)
                            {
                                throw new Exception("Failed to read from file");
                            }

                            if (status == NTStatus.STATUS_END_OF_FILE || data.Length == 0)
                            {
                                break;
                            }
                            bytesRead += data.Length;
                            stream.Write(data, 0, data.Length);
                        }

                        archivoBase64Dto.Base64 = Convert.ToBase64String(stream.ToArray());
                        status = fileStore.CloseFile(fileHandle);
                    }
                    else
                    {
                        return null;
                    }
                    Console.WriteLine("SE completó lectura del archivo");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    fileStore.Disconnect();
                }
            }
            else
            {
                throw new Exception("Failed to connect to NAS");
            }

            return archivoBase64Dto;
        }
        else
        {
            byte[] pdfBytes;
            using (FileStream fileStream = new FileStream(uncPath, FileMode.Open, FileAccess.Read))
            {
                pdfBytes = new byte[fileStream.Length];
                fileStream.Read(pdfBytes, 0, pdfBytes.Length);
            }

            archivoBase64Dto.Base64 = Convert.ToBase64String(pdfBytes);

            return archivoBase64Dto;
        }
    }

    private string ObtenerHost(string uncPath)
    {
        var dominio = _configuration["SISE3:BackEnd:NASDominio"];
        Uri uri = new Uri(uncPath);
        string host = uri.Host;

        if (!EsDireccionIP(host))
        {
            host += dominio;
        }

        return host;
    }

    private string ObtenerCarpetaCompartida(string uncPath)
    {
        Uri uri = new Uri(uncPath);
        string absolutePath = uri.AbsolutePath;
        string primeraCarpeta = string.Empty;
        var split = absolutePath.Split('/');
        if (split.Length>2)
        {
            primeraCarpeta = split[1];
        }
        else
        {
            throw new ArgumentException("No se encontró carpeta raíz en unc path" + uncPath);
        }

        return primeraCarpeta;
    }

    private string CortarPath(string uncPath,string carpetaCompartida)
    {
        string searchString = $"\\{carpetaCompartida}\\";
        int index = uncPath.IndexOf(searchString);
        if (index != -1)
        {
            uncPath = uncPath.Substring(index + searchString.Length);
        }

        return uncPath;
    }

    public void AlmacenarArchivo(string uncPath, byte[] data)
    {
        SMB2Client client = new SMB2Client(); 
        string ip = ObtenerHost(uncPath);
        string user = _configuration["SISE3:BackEnd:NASUsuario"];
        string pass = _configuration["SISE3:BackEnd:NASContrasena"];
        var carpetaCompartida = ObtenerCarpetaCompartida(uncPath);

        bool isConnected = false;

        if (EsDireccionIP(ip))
        {
            isConnected = client.Connect(IPAddress.Parse(ip), SMBTransportType.DirectTCPTransport);
        }
        else
        {
            isConnected = client.Connect(ip, SMBTransportType.DirectTCPTransport);
        }

        if (isConnected)
        {
            NTStatus status = client.Login(_configuration["SISE3:BackEnd:NASDominioUsuario"], user, pass);
            ISMBFileStore fileStore = client.TreeConnect(carpetaCompartida, out status);

            Console.WriteLine("Conectado a nas");
            object handle;
            FileStatus fileStatus;

            if (status == NTStatus.STATUS_SUCCESS)
            {
                Console.WriteLine("estado de cnn NTStatus.STATUS_SUCCESS");

                var completePath = CortarPath(uncPath, carpetaCompartida);

                status = fileStore.CreateFile(out handle, out fileStatus, completePath, AccessMask.GENERIC_WRITE | AccessMask.SYNCHRONIZE, SMBLibrary.FileAttributes.Normal, ShareAccess.None, CreateDisposition.FILE_OVERWRITE_IF, CreateOptions.FILE_NON_DIRECTORY_FILE | CreateOptions.FILE_SYNCHRONOUS_IO_ALERT, null);
                if (status == NTStatus.STATUS_SUCCESS)
                {
                    status = SaveFile(data, client, fileStore, handle);
                }
                else if (status == NTStatus.STATUS_OBJECT_PATH_NOT_FOUND)
                {
                    var pathToCreate = Path.GetDirectoryName(completePath);
                    status = fileStore.CreateFile(out handle, out fileStatus, pathToCreate, AccessMask.GENERIC_WRITE | AccessMask.SYNCHRONIZE, SMBLibrary.FileAttributes.Normal, ShareAccess.None, CreateDisposition.FILE_CREATE, CreateOptions.FILE_DIRECTORY_FILE, null);

                    if (status == NTStatus.STATUS_OBJECT_PATH_NOT_FOUND)
                    {
                        var directories = pathToCreate.Split("\\");
                        string rutaCompleta = string.Empty;
                        for (int i = 0; i < directories.Length; i++)
                        {
                            rutaCompleta = Path.Combine(rutaCompleta, directories[i]);
                            status = fileStore.CreateFile(out handle, out fileStatus, rutaCompleta, AccessMask.GENERIC_WRITE | AccessMask.SYNCHRONIZE, SMBLibrary.FileAttributes.Normal, ShareAccess.None, CreateDisposition.FILE_CREATE, CreateOptions.FILE_DIRECTORY_FILE, null);
                        }
                    }

                    if (status == NTStatus.STATUS_SUCCESS)
                    {
                        status = fileStore.CreateFile(out handle, out fileStatus, completePath, AccessMask.GENERIC_WRITE | AccessMask.SYNCHRONIZE, SMBLibrary.FileAttributes.Normal, ShareAccess.None, CreateDisposition.FILE_OVERWRITE_IF, CreateOptions.FILE_NON_DIRECTORY_FILE | CreateOptions.FILE_SYNCHRONOUS_IO_ALERT, null);
                        status = SaveFile(data, client, fileStore, handle);
                    }
                }
                else 
                {
                    throw new RuleException("No se pudo guardar el archivo");
                }

                client.Disconnect();
            }
            else
            {
                Console.WriteLine("Error al intentar conectarse a la Nas");
                throw new Exception("Error al intentar conectarse a la Nas");
            }
        }
        else
        {
            throw new Exception("Failed to connect to NAS");
        }
    }


    private static NTStatus SaveFile(byte[] data, SMB2Client client, ISMBFileStore fileStore, object handle)
    {
        NTStatus status;
        int writeOffset = 0;

        Stream localFileStream = new MemoryStream(data);
        while (localFileStream.Position < localFileStream.Length)
        {
            byte[] buffer = new byte[(int)client.MaxWriteSize];
            int bytesRead = localFileStream.Read(buffer, 0, buffer.Length);
            if (bytesRead < (int)client.MaxWriteSize)
            {
                Array.Resize(ref buffer, bytesRead);
            }
            int numberOfBytesWritten;
            status = fileStore.WriteFile(out numberOfBytesWritten, handle, writeOffset, buffer);
            if (status != NTStatus.STATUS_SUCCESS)
            {
                throw new Exception("Failed to write to file");
            }
            writeOffset += bytesRead;
        }
        status = fileStore.CloseFile(handle);
        return status;
    }
}
