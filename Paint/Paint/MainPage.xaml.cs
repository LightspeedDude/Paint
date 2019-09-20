using SkiaSharp;
using SkiaSharp.Views.Forms;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Paint
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private List<SKPath> paths = new List<SKPath>();
        private Dictionary<long, SKPath> temporaryPaths = new Dictionary<long, SKPath>();
        private Color but;
        private SKSurface surface;
        private SKCanvas canvas;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnPainting(object sender, SKPaintSurfaceEventArgs e)
        {
            surface = e.Surface;

            canvas = surface.Canvas;

            canvas.Clear(SKColors.White);

            if (but == null)
            {
                but = Color.Red;
            }

            SKPaint stroke = new SKPaint
            {

                Color = but.ToSKColor(),
                StrokeWidth = 2,
                Style = SKPaintStyle.Stroke,
                StrokeCap = SKStrokeCap.Round
            };

            foreach (var touchPath in paths)
            {
                canvas.DrawPath(touchPath, stroke);
            }

        }
        private void OnTouch(object sender, SKTouchEventArgs e)
        {
            switch (e.ActionType)
            {
                case SKTouchAction.Pressed:
                    var p = new SKPath();
                    p.MoveTo(e.Location);
                    temporaryPaths[e.Id] = p;
                    break;
                case SKTouchAction.Moved:
                    if (e.InContact)
                        temporaryPaths[e.Id].LineTo(e.Location);
                    break;
                case SKTouchAction.Released:
                    paths.Add(temporaryPaths[e.Id]);
                    temporaryPaths.Remove(e.Id);
                    break;
            }

            e.Handled = true;

            ((SKCanvasView)sender).InvalidateSurface();
        }

        void OnClear(object sender, EventArgs args)
        {
            canvas = surface.Canvas;

            canvas.Clear(SKColors.White);
        }

        void OnChange(Object ob, EventArgs ar)
        {
            var button = (Button)ob;
            but = button.BackgroundColor;
        }

    }
}
