using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace swapping_window
{
    public class MyComponent : GameComponent
    {

        Form windowsGameForm;
        IGraphicsDeviceService graphicsService;
        GraphicsDevice graphics;
        private UserControl1 userControl11;

        public MyComponent(Game1 g)
            : base(g)
        {

        }

        public override void Initialize()
        {
            graphicsService = Game.Services.GetService(typeof(IGraphicsDeviceService)) as IGraphicsDeviceService;
            graphics = graphicsService.GraphicsDevice;

            windowsGameForm = Control.FromHandle(Game.Window.Handle) as Form;
            this.userControl11 = new swapping_window.UserControl1();
            InitializeComponent();

            graphicsService.DeviceResetting += OnDeviceReset;
            graphicsService.DeviceCreated += OnDeviceCreated;
            graphics.Reset();
        }

        void OnDeviceCreated(object sender, EventArgs e)
        {
            graphics = graphicsService.GraphicsDevice;
            graphics.Reset();
        }

        void OnDeviceReset(object sender, EventArgs e)
        {
            graphics.PresentationParameters.DeviceWindowHandle = userControl11.Handle;
            //graphics.PresentationParameters.BackBufferWidth = userControl11.Width;
            //graphics.PresentationParameters.BackBufferHeight = userControl11.Height;
        }
        void InitializeComponent()
        {
            this.userControl11.Location = new System.Drawing.Point(0, 0);
            this.userControl11.Name = "userControl11";
            this.userControl11.Size = new System.Drawing.Size(400, 506);
            this.userControl11.TabIndex = 1;
            windowsGameForm.Controls.Add(userControl11);
        }
    }
}
