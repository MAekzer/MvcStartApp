using MvcStartApp.Middleware.Repositories;
using MvcStartApp.Models.db;

namespace MvcStartApp.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        ///  Middleware-компонент должен иметь конструктор, принимающий RequestDelegate
        /// </summary>
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        ///  Необходимо реализовать метод Invoke  или InvokeAsync
        /// </summary>
        public async Task InvokeAsync(HttpContext context, ILogRepository logger)
        {
            var request = new Request()
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Url = $"https://{context.Request.Host.Value + context.Request.Path}"
            };

            await logger.AddRequestLog(request);

            string logline = $"[{request.Date}]: New request to {request.Url}\n";
            // Для логирования данных о запросе используем свойста объекта HttpContext
            Console.Write(logline);

            string logpath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "Logs", "RequestLogs.txt");
            File.AppendAllText(logpath, logline);

            // Передача запроса далее по конвейеру
            await _next.Invoke(context);
        }
    }
}
