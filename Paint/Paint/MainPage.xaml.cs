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
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnPainting(object sender, SKPaintSurfaceEventArgs e)
        {
            SKSurface surface = e.Surface;

            SKCanvas canvas = surface.Canvas;

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

        void OnClear(object sender, SKPaintSurfaceEventArgs e)
        {
            var surface = e.Surface;

            var canvas = surface.Canvas;

            canvas.Clear(SKColors.White);
        }

        public void OnChange(Object ob, EventArgs ar)
        {
            var button = (Button)ob;
            but = button.BackgroundColor;
        }

        //async void OnSaving(object sender, EventArgs e) {

            //using (SKImage image = SKImage.FromBitmap(saveBitmap))
            //{
            //    SKData data = image.Encode();
            //    DateTime dt = DateTime.Now;
            //    string filename = String.Format("FingerPaint-{0:D4}{1:D2}{2:D2}-{3:D2}{4:D2}{5:D2}{6:D3}.png",
            //                                    dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond);

            //    IPhotoLibrary photoLibrary = DependencyService.Get<IPhotoLibrary>();
            //    bool result = await photoLibrary.SavePhotoAsync(data.ToArray(), "FingerPaint", filename);

            //    if (!result)
            //    {
            //        await DisplayAlert("FingerPaint", "Artwork could not be saved. Sorry!", "OK");
            //    }
            //}



        //}

    }
}
