using Microsoft.Extensions.DependencyInjection;

namespace KookBot.Core;

public static class DependencyInjection {
        private static volatile IServiceProvider? _provider;

        public static bool Ready { get; private set; } = false;

        public static void Dispose() {
                if (_provider is IDisposable disposable) {
                        disposable.Dispose();
                }
        }

        public static async ValueTask DisposeAsync() {
                if (_provider is IAsyncDisposable disposable) {
                        await disposable.DisposeAsync();
                } else {
                        Dispose();
                }
        }

        private static void ConfigureServices(IServiceProvider provider) {
                Interlocked.CompareExchange(ref _provider, provider, null);

                Ready = true;
        }

        public static void ConfigureServices(IServiceCollection services, Action<IServiceCollection> configureFunc) {
                configureFunc(services);
                ConfigureServices(services.BuildServiceProvider());
        }

        public static void ConfigureServices(IServiceCollection services) {
                ConfigureServices(services.BuildServiceProvider());
        }

        private static Exception GetDiGetFailException(Type serviceType) {
                var msg = $"DI.Get<{serviceType}>() failed. Service provider is probably not be configured.";

                return new Exception(msg);
        }

        public static TService Get<TService>() where TService : notnull {
                if (_provider == null) {
                        throw GetDiGetFailException(typeof(TService));
                }

                return _provider.GetRequiredService<TService>();
        }
}
