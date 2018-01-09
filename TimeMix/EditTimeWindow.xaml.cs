using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Xml.Linq;
using System.Collections.Specialized;

namespace TimeMix
{
    /// <summary>
    /// EditTimeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class EditTimeWindow : Window
    {
        string pathXml = AppDomain.CurrentDomain.BaseDirectory + "/File/Data";
        /// <summary>
        /// 指向的TextBox被选中的行数
        /// </summary>
        int pLine = 0;
        public EditTimeWindow()
        {
            InitializeComponent();
            CboBigSelect.ItemsSource = new DirectoryInfo(pathXml).GetDirectories();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void CboBigSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CboSmallSelect.ItemsSource = new DirectoryInfo(pathXml + @"/" + CboBigSelect.SelectedValue).GetFiles();
        }

        private void CboSmallSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                TbEdit.Text = XDocument.Load(pathXml + @"/" + CboBigSelect.SelectedValue + "/" + CboSmallSelect.SelectedValue).ToString();
            }
            catch
            {
                TbEdit.Text = "";
            }
        }

        private void TbTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && TbTime.Text != "")
            {
                StringCollection s = GetLines(TbEdit);
                s.Add("");
                for (int i = s.Count - 1; i > pLine; i--)
                {
                    s[i] = s[i - 1];
                }
                string last = s[pLine];
                DateTime d = DateTime.Parse(last.Split('"')[1]);
                string beginTime = d.AddMinutes(double.Parse(TbTime.Text)).ToShortTimeString();
                s[pLine + 1] = string.Format("  <Section BeginTime=\"{0}\" Name=\"{1}\" Class=\"\"/>\n", beginTime, TbName.Text);
                pLine++;
                SetLines(TbEdit, s);
                TbTime.Text = "";
            }
        }

        private void TbName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                StringCollection s = GetLines(TbEdit);
                string s1 = s[pLine];
                if (s1.Contains("<S"))
                {
                    string name = s1.Split('"')[3];
                    string beginTime = s1.Split('"')[1];
                    string content = string.Format("  <Section BeginTime=\"{0}\" Name=\"{1}\" Class=\"\"/>\n", beginTime, TbName.Text);
                    s[pLine] = content;
                    //Console.WriteLine(name);
                    pLine++;
                    SetLines(TbEdit, s);
                    TbName.Text = "";
                }
                else
                {
                    return;
                }
            }
        }
        /// <summary>
        /// 返回Textbox的Line集合
        /// </summary>
        /// <param name="textBox"></param>
        /// <returns></returns>
        private StringCollection GetLines(TextBox textBox)
        {
            StringCollection lines = new StringCollection();
            for (int line = 0; line < textBox.LineCount; line++)
                lines.Add(textBox.GetLineText(line));
            return lines;
        }
        /// <summary>
        /// 根据Line集合设置Textbox
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="stringCollection"></param>
        private void SetLines(TextBox textBox, StringCollection stringCollection)
        {
            textBox.Text = "";
            foreach (var item in stringCollection)
            {
                textBox.Text += item;
            }
        }

        private void TbEdit_LostFocus(object sender, RoutedEventArgs e)
        {
            int p = TbEdit.SelectionStart;
            for (int i = 0; i < TbEdit.LineCount; i++)
            {
                //Console.Write(Tb.GetLineLength(i) + ";");
                if (p - TbEdit.GetLineLength(i) > 0)
                {
                    p = p - TbEdit.GetLineLength(i);
                }
                else
                {
                    pLine = i;
                    break;
                }
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                File.WriteAllText(pathXml + @"/" + CboBigSelect.SelectedValue + "/" + CboSmallSelect.SelectedValue, TbEdit.Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show("保存失败:(\n" + ex.ToString());
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
