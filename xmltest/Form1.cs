using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace xmltest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

       }
        

        private void button1_Click(object sender, EventArgs e)
        {
            try{
            //1.将DataTable内容写入到XML文件
            //建立DataTable取名Table4Test
            DataTable dt1 = new DataTable("Table1");
            //生成列
            dt1.Columns.Add("基础伤");
            dt1.Columns.Add("属性");
            dt1.Columns.Add("副伤");
            dt1.Columns.Add("暴伤");
            dt1.Columns.Add("背伤");
            dt1.Columns.Add("终伤");
            dt1.Columns.Add("加成");
            dt1.Columns.Add("打击面");
            dt1.Columns.Add("抗性");
            dt1.Columns.Add("非爆减伤");
            //生成行
            dt1.Rows.Add(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text, comboBox1.Text,textBox9.Text,textBox10.Text);
            //写入到XML
            ReadAndWrite.WriteToXml(dt1, "DataTable.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try{
            string address = "DataTable.xml";
            //读取XML
            DataTable dt2 = new DataTable("Table2");
            dt2 = ReadAndWrite.ReadFromXml(address);
            textBox1.Text = dt2.Rows[0][0].ToString();
            textBox2.Text = dt2.Rows[0][1].ToString();
            textBox3.Text = dt2.Rows[0][2].ToString();
            textBox4.Text = dt2.Rows[0][3].ToString();
            textBox5.Text = dt2.Rows[0][4].ToString();
            textBox6.Text = dt2.Rows[0][5].ToString();
            textBox7.Text = dt2.Rows[0][6].ToString();
            comboBox1.Text = dt2.Rows[0][7].ToString();
            textBox9.Text = dt2.Rows[0][8].ToString();
            textBox10.Text = dt2.Rows[0][9].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try{
            double num1, num2, num3, num4, num5, num6, num7, num8, num9,num10;

            num1 = Convert.ToInt32(textBox1.Text);
            num2 = Convert.ToInt32(textBox2.Text);
            num3 = Convert.ToInt32(textBox3.Text);
            num4 = Convert.ToInt32(textBox4.Text);
            num5 = Convert.ToInt32(textBox5.Text);
            num6 = Convert.ToInt32(textBox6.Text);
            num7 = Convert.ToInt32(textBox7.Text);
            switch (comboBox1.Text)
            {
                case "正":
                    num8 = 1;
                    break;
                case "侧":
                    num8 = 1.1;
                    break;
                case "背":
                    num8 = 1.25;
                    break;
                default:
                    num8 = 1;
                    break;
            }
            num9 = Convert.ToInt32(textBox9.Text);
            num10 = Convert.ToInt32(textBox10.Text);
            double damage1, damage2, damage3, damage4;
            //是否计算背伤
            if (num8 != 1.25)
            {
                num5 = 0;
            }
            damage1 = num1 * (100 + num2 + num3 -num10 + num5) / 100 * (100 + num6) / 100 * (100 + num7) / 100 * num8 * System.Math.Pow(0.8, ((num9) / 100));
            LBdamege1.Text = damage1.ToString();
            damage2 = 1.25 * num1 * (100 + num2 + num3 + num4 + num5) / 100 * (100 + num6) / 100 * (100 + num7) / 100 * num8 * System.Math.Pow(0.8, ((num9) / 100));
            LBdamege2.Text = damage2.ToString();
            damage3 = 0.8 * damage1;
            LBdamege3.Text = damage3.ToString();
            damage4 = 0.8 * damage2;
            LBdamege4.Text = damage4.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
