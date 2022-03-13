using System;

namespace MineWalk.Core
{
	public class ConsoleLogger : ILogger
	{
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}

