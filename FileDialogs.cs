using System.Windows.Forms;

namespace Sem3
{
    class FileDialogs
    {
        public static string OpenFile()
        {
            using (OpenFileDialog d = new OpenFileDialog())
            {
                d.Filter = "Image Files (*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG";

                if (d.ShowDialog() == DialogResult.OK)
                {
                    return d.FileName;
                }
            }

            return null;
        }

        public static string SaveFile()
        {
            using (SaveFileDialog d = new SaveFileDialog())
            {
                d.Filter = "PNG File|*.png|BMP File|*.bmp|JPG/JPEG File|*.jpg;*.jpeg";
                d.AddExtension = true;
                d.DefaultExt = "PNG";

                if (d.ShowDialog() == DialogResult.OK)
                {
                    return d.FileName;
                }
            }

            return null;
        }
    }
}