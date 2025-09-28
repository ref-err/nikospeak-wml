using OneShotMG;
using WorldMachineLoader.API.Core;
using WorldMachineLoader.API.Events.Environment;
using WorldMachineLoader.API.Events.Lifecycle;
using WorldMachineLoader.API.Interfaces;

namespace net.referr.nikospeak
{
    public class Mod : IMod
    {
        public void OnLoad(ModContext modContext)
        {
            Globals.Context = modContext;
            EventBus.Subscribe<GraphicsDeviceInit>(GDeviceInit);
            EventBus.Subscribe<WindowManagerInitializedEvent>(OnWinManInit);
        }

        public void OnShutdown() { }

        private void OnWinManInit(WindowManagerInitializedEvent e)
        {
            Game1.windowMan.AddWindow(new SpeakWindow("Niko Speak!", "oneshot", 150, 100, false));
        }

        private void GDeviceInit(GraphicsDeviceInit e)
        {
            Globals.GraphicsDevice = e.GraphicsDevice;
        }
    }
}
