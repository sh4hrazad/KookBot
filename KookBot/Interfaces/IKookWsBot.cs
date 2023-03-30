using KookBot.Components;
using TouchSocket.Core;

namespace KookBot.Interfaces; 

public interface IKookWsBot {
        public static IKookWsBot Instance = DependencyInjection.Get<IKookWsBot>();
        
        public IKookWsBot Setup(string url);
        
        public Result TryConnect();

        public void Info(string message);

        public void Close();
}
