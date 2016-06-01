using System;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Windows.Forms;

namespace Kemono_City_Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 调试输出
        /// </summary>
        /// <param name="Log">输出内容</param>
        public void Output (object Log) { Console.WriteLine(Convert.ToString(Log)); }

        /// <summary>
        /// 分割竖条图片
        /// </summary>
        /// <param name="LoadedImage">欲分割的图片</param>
        /// <returns>分割后的图片</returns>
        public static Bitmap SplitLoadedImage(Image LoadedImage)
        {
            Bitmap ResultImage = new Bitmap(LoadedImage.Width * 2 + 2, LoadedImage.Height / 2);
            Graphics WorkingG = Graphics.FromImage(ResultImage);
            WorkingG.DrawImage(LoadedImage,
                new Rectangle(-1, -1, LoadedImage.Width, LoadedImage.Height / 2),
                new Rectangle(0, 0, LoadedImage.Width, LoadedImage.Height / 2 - 2), GraphicsUnit.Pixel);
            WorkingG.DrawImage(LoadedImage,
                new Rectangle(LoadedImage.Width, 0, LoadedImage.Width, LoadedImage.Height / 2),
                new Rectangle(0, LoadedImage.Height / 2, LoadedImage.Width, LoadedImage.Height / 2 - 2), GraphicsUnit.Pixel);
            WorkingG.Dispose();
            return ResultImage;
        }

        /// <summary>
        /// 添加头部标题
        /// </summary>
        /// <param name="LoadedImage">要处理的图片</param>
        /// <param name="Height">黑底的高度</param>
        /// <param name="Title">要添加的标题</param>
        /// <returns></returns>
        public static Bitmap AddImageTitle(Image LoadedImage,int Height,string Title)
        {
            Bitmap ResultImage = new Bitmap(LoadedImage.Width, LoadedImage.Height + Height);
            Graphics WorkingG = Graphics.FromImage(ResultImage);
            //把图片画到下面
            WorkingG.DrawImage(LoadedImage, 0, Height, 
                new Rectangle(0, 0, LoadedImage.Width, LoadedImage.Height + Height), GraphicsUnit.Pixel);
            //在上面画黑色矩形
            WorkingG.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, LoadedImage.Width, Height));
            //写文字
            int FontSize = Height / 2;
            WorkingG.DrawString(Title, new Font("微软雅黑", FontSize),
                new SolidBrush(Color.White), new PointF
                (LoadedImage.Width / 2 - Title.Length * (int)Math.Ceiling(FontSize / 1.4), FontSize / 5));
            //输出
            WorkingG.Dispose();
            return ResultImage;
            
        }

        /// <summary>  
        /// 取文本中间内容  
        /// </summary>  
        /// <param name="str">原文本</param>  
        /// <param name="leftstr">左边文本</param>  
        /// <param name="rightstr">右边文本</param>  
        /// <returns>返回中间文本内容</returns>  
        public static string GetBetween(string str, string leftstr, string rightstr)
        {
            int i = str.IndexOf(leftstr) + leftstr.Length;
            if (str.IndexOf(rightstr, i) <= 0) { throw new ArgumentNullException("Wrong Length. 解析错误"); }
            string temp = str.Substring(i, str.IndexOf(rightstr, i) - i);
            return temp;
        }

        /// <summary>
        /// 取文本最后一次出现的位置
        /// </summary>
        /// <param name="str">想要寻找的文本</param>
        /// <param name="strALL">被查找的文本</param>
        /// <returns>文本最后一次出现的位置</returns>
        public static int GetLastIndex(string str,string strALL)
        {
            int i = strALL.IndexOf(str);
            int temp = 0;
            while (i != -1)
            {
                temp = i;
                i = strALL.IndexOf(str,i + 1);
            }
            return temp;
        }

        /// <summary>
        /// 取网页源代码
        /// </summary>
        /// <param name="url">网页网址</param>
        /// <returns>网页源码</returns>
        private static string GetWebClient(string url)
        {
            string strHTML = "";
            WebClient myWebClient = new WebClient();
            Stream myStream = myWebClient.OpenRead(url);
            StreamReader sr = new StreamReader(myStream, System.Text.Encoding.GetEncoding("big5"));
            strHTML = sr.ReadToEnd();
            myStream.Close();
            myWebClient.Dispose();
            return strHTML;
        }

        Bitmap ComicImage;
        private void button_Load_Click(object sender, EventArgs e)
        {
            Output("Start getting...");
            GetAndLoadImage();
            Output("结束 按钮");
        }

        private void button_RefreshPage_Click(object sender, EventArgs e)
        {
            try { File.Delete("image/" + numericUpDown_Page.Value.ToString()); }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        FileStream fs;
        /// <summary>
        /// 读取并加载图片
        /// </summary>
        private async void GetAndLoadImage()
        {
            Output("1开始禁止组件");
            button_Load.Enabled = false;
            numericUpDown_Page.Enabled = false;
            listView_Marks.Enabled = false;
            Thread.Sleep(200);
            Output("2开始尝试");
            try
            {
                if (Directory.Exists("image") == false) { Directory.CreateDirectory("image"); }

                Output("3打开FileStream");
                // 查看是否有缓存
                fs = new FileStream("image\\" + numericUpDown_Page.Text, FileMode.OpenOrCreate, FileAccess.ReadWrite);

                string Page;
                if (numericUpDown_Page.Value < 10) { Page = 0 + numericUpDown_Page.Value.ToString(); }
                else { Page = numericUpDown_Page.Value.ToString(); }
                string ComicPageIntroduce;
                string ComicPageTitle;
                MemoryStream MemStream = new MemoryStream();
                Output("4判断fs长度=0");
                //若没有，则下载
                if (fs.Length == 0)
                {
                    //以下if判断全部是因为847和848话的网页格式的改变
                    string extension;
                    if (Int32.Parse(Page) < 848) { extension = ".htm"; }
                    else { extension = ".html"; }
                    if (Int32.Parse(Page) < 10) { Output(Page); }
                    string ComicPageURL = @"http://lightcanvas.web.fc2.com/original2/comic" + Page + extension;

                    Output("5异步获取网页源码");
                    string ComicPage = await Task.Run(() => GetWebClient(ComicPageURL));
                    Output("6获取网页源码完成");
                    string ComicURL = @"http://lightcanvas.web.fc2.com/original2/" +
                        GetBetween(ComicPage, "<div align=\"center\"><img src=\"", "\" ");
                    ComicPageIntroduce = GetBetween(ComicPage, "<p><font size=\"2\">", "</font></p>");
                    ComicPageTitle = GetBetween(ComicPage, "color=\"#FFFFFF\">", "</font>");
                    if (ComicPageTitle == "" | ComicPageTitle.Length > 18)
                    {
                        ComicPageTitle = GetBetween(ComicPage, "size=\"5\">", "</font>");
                    }

                    Output("7WebRequest生成");
                    WebRequest Request = WebRequest.Create(ComicURL);
                    //设置5秒超时
                    Request.Timeout = 5000;
                    Output("8异步获取网页Response");
                    WebResponse Response = await Task.Run(() => Request.GetResponse());
                    Output("9获取网页完成");
                    //Stream复制到Memory中
                    Response.GetResponseStream().CopyTo(MemStream);
                    Response.Close();

                    ComicImage = new Bitmap(Image.FromStream(MemStream));
                    Output("11新建bitmap ComicImage");

                    //分割处理图片
                    Output("12图片处理开始");
                    ComicImage = SplitLoadedImage(ComicImage);
                    ComicImage = AddImageTitle(ComicImage, 50, ComicPageTitle);

                    //压缩保存图片
                    MemStream.Close();
                    MemStream = new MemoryStream();
                    ComicImage.Save(MemStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    MemStream.Seek(0, SeekOrigin.Begin);
                    fs.Seek(0, SeekOrigin.Begin);
                    GZipStream GZips = new GZipStream(fs, CompressionMode.Compress);
                    MemStream.CopyTo(GZips);

                    GZips.Close();
                    //fs.Flush();
                    //fs.Close();


                    Output("13图片处理完成,Save");
                    textBox_Introduce.Text = ComicPageIntroduce;

                    Text = "Kemono City Client - " + ComicPageTitle + " - " +
                        numericUpDown_Page.Value.ToString() + "/" + numericUpDown_Page.Maximum + " Pages";

                    //把说明保存到XML中
                    Output("14保存XML文件Introduce");
                    _XmlEInnerText = Xml.CreateElement("I" + numericUpDown_Page.Value.ToString());
                    _XmlEInnerText.InnerText = ComicPageIntroduce;
                    XmlE.SelectSingleNode("ComicIntroduces").AppendChild(_XmlEInnerText);
                    Xml.Save("Config.xml");

                    Output("Title:" + ComicPageTitle);
                }
                //若有，则读取
                else
                {
                    Output("5新建bitmap");
                    //解压缩保存图片
                    GZipStream GZips = new GZipStream(fs, CompressionMode.Decompress);
                    MemStream.Seek(0, SeekOrigin.Begin);
                    GZips.CopyTo(MemStream);
                    ComicImage = new Bitmap(MemStream);
                    Text = "Kemono City Client - Offline Mode";
                    fs.Close();
                    GZips.Close();

                    //读XML中的Introduce
                    Output("6开始尝试读取XML文件Introduce");
                    try
                    {
                        textBox_Introduce.Text = XmlE.SelectSingleNode("ComicIntroduces").
                        SelectSingleNode("I" + numericUpDown_Page.Value.ToString()).InnerText;
                    }
                    catch { Output("Introdueces Loading Failed."); }
                    Output("7读取文件完成");
                    Output("8-15");

                }

                //调整窗口大小
                Output("16调整窗口大小");
                pictureBox_Show.Height = ComicImage.Height;
                pictureBox_Show.Width = ComicImage.Width - 2;
                textBox_Introduce.Width = pictureBox_Show.Width;
                textBox_Introduce.Location = new Point(textBox_Introduce.Location.X, pictureBox_Show.Location.Y + pictureBox_Show.Height + 5);
                
            }
            catch (Exception ex)
            {
                Output("？关闭fs");
                fs.Close(); fs.Dispose();

                Output("？启用组件");
                button_Load.Enabled = true;
                numericUpDown_Page.Enabled = true;
                listView_Marks.Enabled = true;

                Output("？显示错误信息");
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Output("Finished getting.");
            Output("17启用窗口组件");
            button_Load.Enabled = true;
            numericUpDown_Page.Enabled = true;
            listView_Marks.Enabled = true;

            Output("18加载Image到picbox");
            pictureBox_Show.Image = ComicImage;
        }

        XmlDocument Xml = new XmlDocument();
        XmlElement XmlE;
        XmlElement XmlEMarks;
        XmlElement _XmlEInnerText;
        /// <summary>
        /// 加载配置
        /// </summary>
        private void Config_Load()
        {
            Xml.Load("Config.xml");
            XmlE = (XmlElement)Xml.SelectSingleNode("Settings");
            XmlEMarks = (XmlElement)XmlE.SelectSingleNode("Marks");
            //读取最大集数
            numericUpDown_Page.Maximum = int.Parse(XmlE.SelectSingleNode("numMax").InnerText);
            //读取上次看到
            numericUpDown_Page.Value = int.Parse(XmlE.SelectSingleNode("LastSeen").InnerText);
            listView_Marks.Items.Add("Last Seen:" + XmlE.SelectSingleNode("LastSeen").InnerText,
                int.Parse(XmlE.SelectSingleNode("LastSeen").InnerText));
            //遍历Marks
            foreach (XmlNode item in XmlEMarks.ChildNodes)
            {
                listView_Marks.Items.Add("Page " + item.InnerText, int.Parse(item.InnerText));
            }
        }

        /// <summary>
        /// 创建配置
        /// </summary>
        private void Config_Create()
        {
            Xml.AppendChild(Xml.CreateXmlDeclaration("1.0", "big5", ""));
            XmlE = Xml.CreateElement("Settings");

            _XmlEInnerText = Xml.CreateElement("LastSeen");
            _XmlEInnerText.InnerText = "1";
            XmlE.AppendChild(_XmlEInnerText);

            _XmlEInnerText = Xml.CreateElement("numMax");
            _XmlEInnerText.InnerText = "1";
            XmlE.AppendChild(_XmlEInnerText);

            _XmlEInnerText = Xml.CreateElement("ComicIntroduces");
            XmlE.AppendChild(_XmlEInnerText);

            XmlEMarks = Xml.CreateElement("Marks");
            Xml.AppendChild(XmlE);
            XmlE.AppendChild(XmlEMarks);

            刷新最大集数ToolStripMenuItem_Click(null, null);
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        private void Config_Save()
        {
            //保存上次看到
            XmlE.SelectSingleNode("LastSeen").InnerText = numericUpDown_Page.Value.ToString();
            //重保存列表
            XmlEMarks.RemoveAll();
            for (int i = 1; i < listView_Marks.Items.Count; i++)
            {
                _XmlEInnerText = Xml.CreateElement("M" + i.ToString());
                _XmlEInnerText.InnerText = listView_Marks.Items[i].ImageIndex.ToString();
                XmlEMarks.AppendChild(_XmlEInnerText);
            }
            Xml.Save("Config.xml");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //注册热键 Ctrl + ?
            HotKey.RegisterHotKey(Handle, 100, HotKey.KeyModifiers.Ctrl, Keys.A);  //A键
            HotKey.RegisterHotKey(Handle, 101, HotKey.KeyModifiers.Ctrl, Keys.D);  //D键
            HotKey.RegisterHotKey(Handle, 102, HotKey.KeyModifiers.Ctrl, Keys.M);  //M键

            //有则读取配置文件
            if (File.Exists("Config.xml")) { Config_Load(); }
            //没有则创建
            else { Config_Create(); }

            Text = "Kemono City Client - 0/" + numericUpDown_Page.Maximum.ToString() + " Pages";
            
        }
        
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 注销热键
            HotKey.UnregisterHotKey(Handle, 100);
            HotKey.UnregisterHotKey(Handle, 101);

            // 保存配置
            if (Reseted == false) { Config_Save(); }
            
        }

        private void listView_Marks_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            numericUpDown_Page.Value = listView_Marks.SelectedItems[0].ImageIndex;
            GetAndLoadImage();
        }

        private void button_Mark_Click(object sender, EventArgs e)
        {
            listView_Marks.Items.Add("Page " + numericUpDown_Page.Value.ToString(), int.Parse(numericUpDown_Page.Value.ToString()));
        }

        private void 加载此MarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView_Marks.SelectedItems.Count >= 1)
            {
                numericUpDown_Page.Value = listView_Marks.SelectedItems[0].ImageIndex;
                GetAndLoadImage();
            }
            else { MessageBox.Show("未选择任何Mark！", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void 删除MarksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView_Marks.SelectedItems.Count >= 1)
            {
                foreach (ListViewItem item in listView_Marks.SelectedItems)
                {
                    item.Remove();
                }
            }
            else { MessageBox.Show("未选择任何Mark！", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        //重写WndProc拦截热键
        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            //获取到快捷键
            if (m.Msg == WM_HOTKEY)
            {
                switch (m.WParam.ToInt32())
                {
                    case 100:
                        if (!button_Load.Enabled) { return; }
                        Output("按下Ctrl+A");
                        numericUpDown_Page.Value -= 1;
                        GetAndLoadImage();
                        break;
                    case 101:
                        if (!button_Load.Enabled) { return; }
                        Output("按下Ctrl+D");
                        numericUpDown_Page.Value += 1;
                        GetAndLoadImage();
                        break;
                    case 102:
                        Output("按下Ctrl+M");
                        button_Mark_Click(null, new EventArgs());
                        break;
                }
            }
            base.WndProc(ref m);
        }

        private async void 刷新最大集数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            contextMenuStrip_listView.Items[3].Enabled = false;

            try
            {
                string MenuPage = await Task.Run(() => GetWebClient("http://lightcanvas.web.fc2.com/original2/b-menu.htm"));
                int numMaxIndex = GetLastIndex("target=\"mainFrame\">", MenuPage);
                string numMax = "1" + MenuPage.Substring(numMaxIndex + "target=\"mainFrame\">".Length, 3);
                Output("numMax:" + numMax);
                numericUpDown_Page.Maximum = Int32.Parse(numMax);

                //自助添加numMax
                try { XmlE.SelectSingleNode("numMax").InnerText = numMax; }
                catch
                {
                    _XmlEInnerText = Xml.CreateElement("numEMax");
                    _XmlEInnerText.InnerText = numMax;
                    XmlE.AppendChild(_XmlEInnerText);
                }

                Text = Text.Replace(GetBetween(Text, "/", " Page"), numericUpDown_Page.Maximum.ToString());
                MessageBox.Show("更新最大集数成功！", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                contextMenuStrip_listView.Items[3].Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                contextMenuStrip_listView.Items[3].Enabled = true;
            }
        }

        private void 清除缓存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("image")) { MessageBox.Show("没有可以清除的缓存。", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);return; }
            DialogResult choice = MessageBox.Show(
                "这将会清除你所有缓存的文件，让其重新加载。你确定要清除全部缓存吗？",
                "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (choice == DialogResult.Yes)
            {
                DirectoryInfo DI = new DirectoryInfo("image");
                DI.Delete(true);
                DI.Create();
                XmlE.SelectSingleNode("ComicIntroduces").RemoveAll();
                Xml.Save("Config.xml");
                MessageBox.Show("清除完成。", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        bool Reseted = false;
        private void 重置数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult choice = MessageBox.Show(
                "这将会重置配置并清除缓存，程序将退出，你确定要这么做吗？",
                "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            Reseted = true;
            if (choice == DialogResult.Yes)
            {
                if (Directory.Exists("image"))
                {
                    DirectoryInfo DI = new DirectoryInfo("image");
                    DI.Delete(true);
                }
                if (File.Exists("Config.xml")) { File.Delete("Config.xml"); }
                Close();
                
            }
        }

        private void 加载所有缓存()
        {
            for (int i = 1; i < numericUpDown_Page.Maximum; i++)
            {
                numericUpDown_Page.Value = i;
                GetAndLoadImage();
                while (!button_Load.Enabled) { Delay(1); }
            }
        }

        /// <summary>
        /// 延时函数
        /// </summary>
        /// <param name="delayTime">需要延时多少秒</param>
        /// <returns></returns>
        public static bool Delay(int delayTime)
        {
            DateTime now = DateTime.Now;
            int s;
            do
            {
                TimeSpan spand = DateTime.Now - now;
                s = spand.Seconds;
                Application.DoEvents();
            }
            while (s < delayTime);
            return true;
        }

    }
}
