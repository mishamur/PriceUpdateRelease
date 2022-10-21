using Interfaces;

namespace Logger
{
    public class ConsoleLogger : ILogger
    {
        /// <summary>
        /// Логгирует сообщение в консоль
        /// </summary>
        /// <param name="message">сообщение</param>
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}