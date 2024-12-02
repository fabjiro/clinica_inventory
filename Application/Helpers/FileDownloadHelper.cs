namespace Application.Helpers;

using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

public static class FileDownloadHelper
{
    /// <summary>
    /// Descarga un archivo desde una URL y lo guarda en el sistema de archivos.
    /// </summary>
    /// <param name="url">URL del archivo a descargar.</param>
    /// <param name="outputPath">Ruta donde se guardará el archivo descargado.</param>
    /// <returns>Devuelve un valor booleano indicando si la descarga fue exitosa.</returns>
    public static async Task<bool> DownloadFileAsync(string url, string outputPath)
    {
        try
        {
            using (var httpClient = new HttpClient())
            using (var response = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
            {
                // Verifica si la respuesta es exitosa
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error al descargar archivo: {response.ReasonPhrase}");
                    return false;
                }

                // Crea el stream para leer el contenido del archivo
                using (var contentStream = await response.Content.ReadAsStreamAsync())
                {
                    // Llama al método que guarda el archivo en la ruta especificada
                    await SaveFileAsync(contentStream, outputPath, response.Content.Headers.ContentLength);
                }
            }

            Console.WriteLine($"Archivo descargado exitosamente en: {outputPath}");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al descargar el archivo: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Guarda el contenido del archivo en el sistema de archivos mientras muestra el progreso.
    /// </summary>
    /// <param name="contentStream">Stream del contenido del archivo.</param>
    /// <param name="outputPath">Ruta donde se guardará el archivo.</param>
    /// <param name="totalBytes">Tamaño total del archivo en bytes (puede ser nulo).</param>
    private static async Task SaveFileAsync(Stream contentStream, string outputPath, long? totalBytes)
    {
        const int bufferSize = 8192; // Tamaño del buffer de lectura
        long totalBytesRead = 0;
        byte[] buffer = new byte[bufferSize];
        int bytesRead;

        // Asegúrate de que el directorio exista
        Directory.CreateDirectory(Path.GetDirectoryName(outputPath) ?? throw new InvalidOperationException());

        // Escribe el contenido en el archivo
        using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize, true))
        {
            while ((bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                await fileStream.WriteAsync(buffer, 0, bytesRead);
                totalBytesRead += bytesRead;

                // Mostrar progreso si el tamaño total es conocido
                if (totalBytes.HasValue)
                {
                    Console.Write($"\rProgreso: {totalBytesRead}/{totalBytes.Value} bytes ({(totalBytesRead * 100.0 / totalBytes.Value):F2}%)");
                }
            }
        }

        Console.WriteLine(); // Salto de línea al terminar
    }
}
