using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Negocio
{
    public static class HerramientasImagenes
    {
        public static string GuardarImagenOptimizada(Stream archivoSubido, string nombreOriginal, string carpetaFisicaDestino, int anchoMaximo = 1280, long calidadJpg = 75L)
        {
            if (archivoSubido == null || archivoSubido.Length == 0)
            {
                throw new Exception("No se selecciono ningun archivo.");
            }

            string extension = Path.GetExtension(nombreOriginal).ToLower();
            string[] tiposPermitidos = { ".jpg", ".jpeg", ".png", ".jfif", ".bmp"};

            if (!tiposPermitidos.Contains(extension))
            {
                throw new Exception("Formato de imagen no permitido");
            }

            if (!Directory.Exists(carpetaFisicaDestino))
            {
                Directory.CreateDirectory(carpetaFisicaDestino);
            }

            string nombreArchivo = Guid.NewGuid().ToString() + ".jpg";
            string rutaCompleta = Path.Combine(carpetaFisicaDestino, nombreArchivo);

            using (Image imgOriginal = Image.FromStream(archivoSubido))
            {
                int nuevoAncho = imgOriginal.Width;
                int nuevoAlto = imgOriginal.Height;
                
                if (imgOriginal.Width > anchoMaximo)
                {
                    nuevoAncho = anchoMaximo;
                    nuevoAlto = (int)((double)imgOriginal.Height * anchoMaximo / imgOriginal.Width);
                }

                using (Bitmap bmp = new Bitmap(nuevoAncho, nuevoAlto))
                {
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.Clear(Color.White);
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.CompositingQuality = CompositingQuality.HighQuality;
                        g.DrawImage(imgOriginal, 0, 0, nuevoAncho, nuevoAlto);
                    }

                    ImageCodecInfo jpgEncoder = ImageCodecInfo.GetImageDecoders().First(c => c.FormatID == ImageFormat.Jpeg.Guid);

                    EncoderParameters encoderParams = new EncoderParameters(1);
                    encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, calidadJpg);

                    bmp.Save(rutaCompleta, jpgEncoder, encoderParams);
                }
            }
            return nombreArchivo;
        }
    }
}
