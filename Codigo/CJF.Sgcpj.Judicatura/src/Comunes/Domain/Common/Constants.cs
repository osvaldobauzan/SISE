namespace CJF.Sgcpj.Judicatura.Common.Domain.Common;
public class Constants
{
    public const string MSG_FILEINVALID = "El archivo seleccionado tiene una extensión no válida. Por favor, elija un archivo con una extensión permitida. Archivos permitidos: Documento portable (.pdf) o Word 2007 en adelante (.docx).";
    public const string MSG_FILELENGTHINVALID = "El tamaño de archivo debe ser menos a {0} Mb";
    public const string MSG_UPLOADFILEREQUIRED = "El archivo o lista de archivos a subir son requeridos.";
    public const string MSG_INVALIDFILESIGNATURE = "Formato de archivo no aceptado";
    public const string NAS_FILESIZEFROMSISE3NAST = "SISE3:BackEnd:NASTamanioArchivo";
    public const string NAS_VALIDEXTENSIONFILESISE3NAST = "SISE3:BackEnd:NASExtensionValidaArchivos";
    public const string SISE3_EXTENSIONWORD2007FILE = ".docx";
    public const string SISE3_EXTENSIONPDFADOBEFILE = ".pdf";
    public const string SISE3_EXTENSIONWORDFILE = ".doc";
    public const string SISE3_SIGNATUREHEXDOCX = "SISE3:BackEnd:ArchivoFirmaHexDOCX";
    public const string SISE3_SIGNATUREHEXPDF = "SISE3:BackEnd:ArchivoFirmaHexPDF";
    public const string SISE3_FILEBYTESARRAYEMPTY = "El contenido del archivo que se pretende subir es vacío. Revise el archivo o archivos seleccionados y verifique que el archivo tenga información.";
    public const string SISE3_ALPHACOLLECTION = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
    public const string SISE3_XSSDETECTED = "Se detectó inyección XSS. Solicitud cancelada. Detalle: ";
}
