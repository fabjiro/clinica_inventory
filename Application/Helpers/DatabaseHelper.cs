using System.Diagnostics;

namespace Application.Helpers;

public static class DatabaseHelper
{
    public static async Task<bool> BackupDatabase(string connectionString, string pathToSave)
    {
        try
        {
            // Se divide la cadena de conexión en sus partes individuales.
            var connectionStringParts = ParseConnectionString(connectionString);

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "pg_dump", // Comando pg_dump.
                    Arguments = $"-h {connectionStringParts.Host} -U {connectionStringParts.Username} -d {connectionStringParts.Database} -p {connectionStringParts.Port} --exclude-table=public.backup --no-password -F c -f {pathToSave}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false, // No usar el shell.
                    CreateNoWindow = true // Sin ventana.
                }
            };

            // Se configura la contraseña de la base de datos, si es necesario.
            Environment.SetEnvironmentVariable("PGPASSWORD", connectionStringParts.Password);

            // Ejecuta el proceso.
            var result = process.Start();
            if (!result)
            {
                Console.WriteLine("Error al iniciar el proceso.");
                return false;
            }

            // Espera a que el proceso finalice de forma asíncrona.
            await process.WaitForExitAsync();

            // Verifica si se completó correctamente.
            Console.WriteLine($"ExitCode: {process.ExitCode}");
            return process.ExitCode == 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }


    public static async Task<bool> RestoreDatabase(string connectionString, string pathToRestore)
    {
        try
        {
            // Se divide la cadena de conexión en sus partes individuales.
            var connectionStringParts = ParseConnectionString(connectionString);

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "pg_restore", // Comando pg_restore.
                    Arguments = $"-h {connectionStringParts.Host} -U {connectionStringParts.Username} -d {connectionStringParts.Database} -p {connectionStringParts.Port} --no-password --clean --if-exists -F c {pathToRestore}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false, // No usar el shell.
                    CreateNoWindow = true // Sin ventana.
                }
            };

            // Se configura la contraseña de la base de datos, si es necesario.
            Environment.SetEnvironmentVariable("PGPASSWORD", connectionStringParts.Password);

            // Ejecuta el proceso.
            var result = process.Start();
            if (!result)
            {
                Console.WriteLine("Error al iniciar el proceso.");
                return false;
            }

            // Espera a que el proceso finalice de forma asíncrona.
            await process.WaitForExitAsync();

            // Verifica si se completó correctamente.
            Console.WriteLine($"ExitCode: {process.ExitCode}");
            return process.ExitCode == 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }
    private static (string Host, string Database, string Username, string Password, string Port) ParseConnectionString(string connectionString)
    {
        var parameters = connectionString.Split(';');
        string host = "", database = "", username = "", password = "", port = "";

        foreach (var param in parameters)
        {
            var keyValue = param.Split('=');
            if (keyValue.Length == 2)
            {
                var key = keyValue[0].Trim();
                var value = keyValue[1].Trim();

                if (key.Equals("Host", StringComparison.OrdinalIgnoreCase)) host = value;
                if (key.Equals("Database", StringComparison.OrdinalIgnoreCase)) database = value;
                if (key.Equals("Username", StringComparison.OrdinalIgnoreCase)) username = value;
                if (key.Equals("Password", StringComparison.OrdinalIgnoreCase)) password = value;
                if (key.Equals("Port", StringComparison.OrdinalIgnoreCase)) port = value;
            }
        }

        return (host, database, username, password, port);
    }
}
