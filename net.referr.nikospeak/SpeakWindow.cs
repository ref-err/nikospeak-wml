using OneShotMG;
using OneShotMG.src.TWM;
using OneShotMG.src.Util;
using OneShotMG.src.EngineSpecificCode;
using WorldMachineLoader.API.UI;
using WorldMachineLoader.API.UI.Controls;
using OneShotMG.src.EngineSpecificCode.fmod;
using System.Threading.Tasks;
using System.Reflection;
using System.Threading;

namespace net.referr.nikospeak
{
    internal class SpeakWindow : ModWindow
    {
        private Button button;

        private Label clickCountLabel;

        private int clickCount = 0;

        private bool clicked = false;

        private CancellationTokenSource cts;

        public SpeakWindow(string title, string icon, int width, int height, bool addCloseButton = true, bool addMinimizeButton = true) : base(title, icon, width, height, addCloseButton, addMinimizeButton)
        {
            clickCountLabel = new Label(clickCount.ToString(), new Vec2(6, 6));

            Vec2 btnSize = new Vec2(56, 16);

            button = new Button("meow", new Vec2(
                width / 2 - btnSize.X / 2,
                height - btnSize.Y - 8
                ), () =>
            {
                clickCount++;
                clickCountLabel.Text = clickCount.ToString();
                Game1.soundMan.PlaySound("cat_2");

                cts?.Cancel();
                cts = new CancellationTokenSource();
                var token = cts.Token;

                clicked = true;

                Task.Run(async () =>
                {
                    try
                    {
                        await Task.Delay(450);
                        clicked = false;
                    }
                    catch (TaskCanceledException) { }
                });
            });

            AddControl(clickCountLabel);
            AddControl(button);
        }

        protected override void OnDraw(TWMTheme theme, Vec2 pos, byte alpha)
        {
            Vec2 size = Game1.gMan.TextureSize("facepics/niko");

            if (clicked)
            {
                Game1.gMan.MainBlit("facepics/niko_speak", new Vec2(ContentsSize.X / 2 - size.X / 2, 8) + pos, 1, 1, GraphicsManager.BlendMode.Normal, 2, default, 1f, 1f, 1f);
            }
            else
            {
                Game1.gMan.MainBlit("facepics/niko", new Vec2(ContentsSize.X / 2 - size.X / 2, 8) + pos, 1, 1, GraphicsManager.BlendMode.Normal, 2, default, 1f, 1f, 1f);
            }

        }
    }
}
