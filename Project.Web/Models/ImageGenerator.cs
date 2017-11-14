using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class ImageGenerator
    {
        public void SaveCardToDisk(string templateFilepath, string finalImagePath, string cardMessage)
        {
            //creating a image object
            System.Drawing.Image bitmap = (System.Drawing.Image)Bitmap.FromFile(templateFilepath); // set image 

            Graphics graphicsImage = Graphics.FromImage(bitmap);

            //Set the alignment based on the coordinates   
            StringFormat stringformat = new StringFormat();
            stringformat.Alignment = StringAlignment.Center;
            stringformat.LineAlignment = StringAlignment.Far;


            //Set the font color/format/size etc..  
            Font f = new Font("Impact", 70, FontStyle.Bold, GraphicsUnit.Pixel);
            //Color StringColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");//direct color adding
            string Str_TextOnImage = cardMessage;//Your Text On Image

            Pen p = new Pen(ColorTranslator.FromHtml("#000000"), 8);
            p.LineJoin = LineJoin.Round;

            //this makes the gradient repeat for each text line
            Rectangle fr = new Rectangle(0, bitmap.Height - f.Height, bitmap.Width, f.Height);
            LinearGradientBrush b = new LinearGradientBrush(fr,
                                                            ColorTranslator.FromHtml("#FFFFFF"),
                                                            ColorTranslator.FromHtml("#FFFFFF"),
                                                            90);

            //this will be the rectangle used to draw and auto-wrap the text.
            //basically = image size
            Rectangle r = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            GraphicsPath gp = new GraphicsPath();
            gp.AddString(Str_TextOnImage, f.FontFamily, (int)FontStyle.Bold, 70, r, stringformat);         

            //these affect lines such as those in paths. Textrenderhint doesn't affect
            //text in a path as it is converted to ..well, a path.    
            graphicsImage.SmoothingMode = SmoothingMode.AntiAlias;
            graphicsImage.PixelOffsetMode = PixelOffsetMode.HighQuality;


            graphicsImage.DrawPath(p, gp);
            graphicsImage.FillPath(b, gp);
            // + g.FillPath if you want it filled as well

            //graphicsImage.DrawString(Str_TextOnImage, new Font("Impact", 100,
            //FontStyle.Bold), new SolidBrush(StringColor), new Point(368, 545),
            //stringformat);

            //bitmap.Save(finalImagePath);
            using (MemoryStream memory = new MemoryStream())
            {
                using(FileStream fs = new FileStream(finalImagePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    bitmap.Save(memory, ImageFormat.Jpeg);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
        }
    }
}