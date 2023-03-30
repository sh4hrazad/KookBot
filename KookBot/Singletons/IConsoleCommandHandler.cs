using KookBot.Components;

namespace KookBot.Singletons;

public interface IConsoleCommandHandler {
        public static IConsoleCommandHandler Instance => DependencyInjection.Get<IConsoleCommandHandler>();

        public event EventHandler OnKilled;
        
        public IConsoleCommandHandler RegisterCommandsClass<TClass>();

        public void StartCommandListener();
}
