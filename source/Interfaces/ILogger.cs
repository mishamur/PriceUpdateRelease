namespace Interfaces
{
    public interface ILogger
    {
        /// <summary>
        /// Логгирует сообщение
        /// </summary>
        /// <param name="message">сообщение</param>
        public void Log(string message);
    }
}
