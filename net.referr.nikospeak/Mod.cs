using WorldMachineLoader.API.Core;
using WorldMachineLoader.API.Events.Lifecycle;
using WorldMachineLoader.API.Interfaces;
using WorldMachineLoader.API.UI;

namespace net.referr.nikospeak
{
    public class Mod : IMod
    {
        public void OnLoad(ModContext modContext)
        {
            Globals.Context = modContext;
            EventBus.Subscribe<GraphicsDeviceInit>(GDeviceInit);
            WindowRegistry.Register<SpeakWindow>(modContext, "Niko Speak!");
        }

        public void OnShutdown() { }

        private void GDeviceInit(GraphicsDeviceInit e)
        {
            Globals.GraphicsDevice = e.GraphicsDevice;
        }
    }
}
