
using System.Diagnostics;
using System.Runtime.InteropServices;
using Application.Queries.Status;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
public class StatusController : ControllerBase
{
    private readonly IMediator _mediator;

    public StatusController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categorys = await _mediator.Send(new GetAllStatusQuery());
        return Ok(categorys.Value);
    }


     [HttpGet("cpu")]
    public IActionResult GetSystemStats()
    {
        try
        {
            var stats = GetSystemUsage();
            return Ok(stats);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    private object GetSystemUsage()
    {
        var process = Process.GetCurrentProcess();

        // Memoria usada por el proceso actual en MB
        var memoryUsageMb = process.WorkingSet64 / (1024 * 1024);
        // Tiempo de CPU del proceso actual
        var cpuUsage = GetCpuUsage();

        // Información del sistema operativo
        var osPlatform = RuntimeInformation.OSDescription;

        return new
        {
            CpuUsage = $"{cpuUsage:F2}%",
            MemoryUsage = $"{memoryUsageMb} MB",
            OSPlatform = osPlatform
        };
    }

    private double GetCpuUsage()
    {
        // Tiempo inicial
        var startTime = Process.GetCurrentProcess().TotalProcessorTime;
        var startCpuTime = Stopwatch.GetTimestamp();

        // Esperar un momento para medir el tiempo transcurrido
        System.Threading.Thread.Sleep(500);

        // Tiempo final
        var endTime = Process.GetCurrentProcess().TotalProcessorTime;
        var endCpuTime = Stopwatch.GetTimestamp();

        // Cálculo del porcentaje de uso de CPU
        var cpuUsedMs = (endTime - startTime).TotalMilliseconds;
        var elapsedMs = (endCpuTime - startCpuTime) * 1000 / Stopwatch.Frequency;

        return cpuUsedMs / (Environment.ProcessorCount * elapsedMs) * 100;
    }

    
}