/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Web.Mvc;
using Phi.MobileWebApp.HtmlHelpers;

namespace Phi.MobileWebApp.Controllers
{
    /// <summary>
    /// Simple image generator for Captcha.
    /// </summary>
    public class CaptchaImageController : Controller
    {
        #region Public methods

        /// <summary>
        /// Renders Captcha graphic image.
        /// </summary>
        public void Render(string challengeGuid)
        {
            string key = CaptchaHelper.SESSION_KEY_PREFIX + challengeGuid;

            if (HttpContext.Session == null)
            {
                return;
            }

            string solution = (string)HttpContext.Session[key];

            if (solution == null)
            {
                return;
            }

            // Create empty graphic field for drawing...
            using (Bitmap bmp = new Bitmap(WIDTH, HEIGHT))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    using (Font font = new Font(FONT_FAMILY, 1f))
                    {
                        g.Clear(BACKGROUND);
                        // Create test image to detect best font size.
                        SizeF testSize = g.MeasureString(solution, font);
                        float bestFontSize = Math.Min(WIDTH / testSize.Width,
                                                      HEIGHT / testSize.Height) * 0.95f;
                        SizeF finalSize;
                        using (Font finalFont = new Font(FONT_FAMILY, bestFontSize))
                        {
                            finalSize = g.MeasureString(solution, finalFont);
                        }

                        // Get path of the text centered in the field.
                        g.PageUnit = GraphicsUnit.Point;
                        PointF textTopLeft = new PointF((WIDTH - finalSize.Width) / 2,
                                                        (HEIGHT - finalSize.Height) / 2);
                        using (GraphicsPath path = new GraphicsPath())
                        {
                            path.AddString(solution, new FontFamily(FONT_FAMILY), 0,
                                            bestFontSize, textTopLeft, StringFormat.GenericDefault);
                            // Visualize path into bit shape.
                            g.SmoothingMode = SmoothingMode.HighQuality;
                            g.FillPath(FOREGROUND, _Deform(path));//path);
                            g.Flush();
                            // Send image in GIF format.
                            Response.ContentType = "image/gif";
                            bmp.Save(Response.OutputStream, ImageFormat.Gif);
                        }
                    }
                }
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// 
        /// </summary>
        private GraphicsPath _Deform(GraphicsPath path)
        {
            PointF[] deformed = new PointF[path.PathPoints.Length];
            Random rng = new Random();
            double xSeed = rng.NextDouble() * 2 * Math.PI;
            double ySeed = rng.NextDouble() * 2 * Math.PI;

            for (int i = 0; i < path.PathPoints.Length; i++)
            {
                PointF original = path.PathPoints[i];
                double val = X_FREQ * original.X + Y_FREQ * original.Y;
                int xOffset = (int)(X_AMP * Math.Sin(val + xSeed));
                int yOffset = (int)(Y_AMP * Math.Sin(val + ySeed));
                deformed[i] = new PointF(original.X + xOffset, original.Y + yOffset);
            }

            return new GraphicsPath(deformed, path.PathTypes);
        }

        #endregion

        #region Private constants

        private const int WARP_FACTOR = 5;
        private const double X_AMP = WARP_FACTOR * WIDTH / 100;
        private const double Y_AMP = WARP_FACTOR * HEIGHT / 85;
        private const double X_FREQ = 2 * Math.PI / WIDTH;
        private const double Y_FREQ = 2 * Math.PI / HEIGHT;
        private const int WIDTH = 200;
        private const int HEIGHT = 70;

        /// <summary>
        /// "Rockwell" was used as default, but it is absent on my clearly new Windows.
        /// </summary>
        private const string FONT_FAMILY = "Calibri";
        private readonly static Brush FOREGROUND = Brushes.Navy;
        private readonly static Color BACKGROUND = Color.Silver;

        #endregion
    }
}
