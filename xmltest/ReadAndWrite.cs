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
    class ReadAndWrite
    {
        public static DataTable ReadFromXml(string address)
        {
            DataTable dt = new DataTable();

            try
            {
                if (!File.Exists(address))
                {
                    MessageBox.Show("文件不存在");
                }

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(address);

                XmlNode root = xmlDoc.SelectSingleNode("DataTable");

                //读取表名
                dt.TableName = ((XmlElement)root).GetAttribute("TableName");
                //Console.WriteLine("读取表名： {0}", dt.TableName);

                //读取行数
                int CountOfRows = 0;
                if (!int.TryParse(((XmlElement)root).
                    GetAttribute("CountOfRows").ToString(), out CountOfRows))
                {
                    MessageBox.Show("行数转换失败");
                }

                //读取列数
                int CountOfColumns = 0;
                if (!int.TryParse(((XmlElement)root).
                    GetAttribute("CountOfColumns").ToString(), out CountOfColumns))
                {
                    MessageBox.Show("列数转换失败");
                }

                //从第一行中读取记录的列名
                foreach (XmlAttribute xa in root.ChildNodes[0].Attributes)
                {
                    dt.Columns.Add(xa.Value);
                    //Console.WriteLine("建立列： {0}", xa.Value);
                }

                //从后面的行中读取行信息
                for (int i = 1; i < root.ChildNodes.Count; i++)
                {
                    string[] array = new string[root.ChildNodes[0].Attributes.Count];
                    for (int j = 0; j < array.Length; j++)
                    {
                        array[j] = root.ChildNodes[i].Attributes[j].Value.ToString();
                    }
                    dt.Rows.Add(array);
                    //Console.WriteLine("行插入成功");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new DataTable();
            }

            return dt;
        }
        public static bool WriteToXml(DataTable dt, string address)
        {
            try
            {
                //如果文件DataTable.xml存在则直接删除
                if (File.Exists(address))
                {
                    File.Delete(address);
                }

                XmlTextWriter writer =
                    new XmlTextWriter(address, Encoding.GetEncoding("GBK"));
                writer.Formatting = Formatting.Indented;

                //XML文档创建开始
                writer.WriteStartDocument();

                writer.WriteComment("DataTable: " + dt.TableName);

                writer.WriteStartElement("DataTable"); //DataTable开始

                writer.WriteAttributeString("TableName", dt.TableName);
                writer.WriteAttributeString("CountOfRows", dt.Rows.Count.ToString());
                writer.WriteAttributeString("CountOfColumns", dt.Columns.Count.ToString());

                writer.WriteStartElement("ClomunName", ""); //ColumnName开始

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    writer.WriteAttributeString(
                        "Column" + i.ToString(), dt.Columns[i].ColumnName);
                }

                writer.WriteEndElement(); //ColumnName结束

                //按行各行
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    writer.WriteStartElement("Row" + j.ToString(), "");

                    //打印各列
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        writer.WriteAttributeString(
                            "Column" + k.ToString(), dt.Rows[j][k].ToString());
                    }

                    writer.WriteEndElement();
                }

                writer.WriteEndElement(); //DataTable结束

                writer.WriteEndDocument();
                writer.Close();

                //XML文档创建结束
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }
    }
}
