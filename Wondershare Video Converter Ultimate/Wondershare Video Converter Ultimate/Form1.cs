using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Wondershare_Video_Converter_Ultimate
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll")]
        private static extern int WritePrivateProfileString(
               string lpAppName,
               string lpKeyName,
               string lpString,
               string lpFileName
               );

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        bool beginMove = false;//初始化鼠标位置  
        int currentXPosition;
        int currentYPosition;



        public Form1()
        {
            InitializeComponent();
        }
        public class var
        {
            public static string path;
            public static string inipath;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RegistryKey reg = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32);

            try
            {
                if (Environment.Is64BitOperatingSystem)
                {
                    var.path = reg.OpenSubKey(@"SOFTWARE\WOW6432Node\Wondershare\Wondershare Video Converter Ultimate").GetValue("InstallPath").ToString();
                }
                else
                {
                    var.path = reg.OpenSubKey(@"SOFTWARE\Wondershare\Wondershare Video Converter Ultimate").GetValue("InstallPath").ToString();
                }
            }
            catch
            {
                MessageBox.Show("请先安装 Wondershare Video Converter Ultimate 产品！", "提示：", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Process.Start("https://www.muruoxi.com/2866.html");
            }
            finally
            {
                reg.Close();
            }
            Assembly asm = Assembly.GetEntryAssembly();
            FileStream fs;
            BinaryWriter bw;
            byte[] temp1;
            byte[] temp2;
            byte[] temp3;
            try
            {
                Stream dll1 = asm.GetManifestResourceStream("Wondershare_Video_Converter_Ultimate.Resources.Chs.dat");
                Stream dll2 = asm.GetManifestResourceStream("Wondershare_Video_Converter_Ultimate.Resources.Chs2.dat");
                Stream exe = asm.GetManifestResourceStream("Wondershare_Video_Converter_Ultimate.Resources.VideoConverterUltimate.exe");
                temp1 = new byte[dll1.Length];
                dll1.Read(temp1, 0, temp1.Length);
                dll1.Seek(0, SeekOrigin.Begin);
                temp2 = new byte[dll2.Length];
                dll1.Read(temp2, 0, temp2.Length);
                dll1.Seek(0, SeekOrigin.Begin);
                temp3 = new byte[exe.Length];
                exe.Read(temp3,0,temp3.Length);
                exe.Seek(0,SeekOrigin.Begin);
                fs = new FileStream(var.path + @"\Languages\Chs.dat", FileMode.Create);
                bw = new BinaryWriter(fs);
                bw.Write(temp1);
                fs.Close();
                bw.Close();
                fs = new FileStream(var.path + @"\Languages\MediaServer\Chs.dat", FileMode.Create);
                bw = new BinaryWriter(fs);
                bw.Write(temp2);
                fs.Close();
                bw.Close();
                fs = new FileStream(var.path + @"\VideoConverterUltimate.exe", FileMode.Create);
                bw = new BinaryWriter(fs);
                bw.Write(temp3);
                fs.Close();
                bw.Close();
                var.inipath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Wondershare\Video Converter Ultimate\VideoConverterUltimate.ini";
                WritePrivateProfileString("Product", "DefLanguage", "CHS", var.inipath);
                WritePrivateProfileString("Product", "DefSkin", "CHS", var.inipath);
                MessageBox.Show("设置成功！");
                Process.Start("https://www.muruoxi.com/");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }






        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                beginMove = true;
                currentXPosition = MousePosition.X;//鼠标的x坐标为当前窗体左上角x坐标  
                currentYPosition = MousePosition.Y;//鼠标的y坐标为当前窗体左上角y坐标  
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (beginMove)
            {
                this.Left += MousePosition.X - currentXPosition;//根据鼠标x坐标确定窗体的左边坐标x  
                this.Top += MousePosition.Y - currentYPosition;//根据鼠标的y坐标窗体的顶部，即Y坐标  
                currentXPosition = MousePosition.X;
                currentYPosition = MousePosition.Y;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                currentXPosition = 0; //设置初始状态  
                currentYPosition = 0;
                beginMove = false;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
