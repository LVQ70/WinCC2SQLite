using CCHMIRUNTIME;
using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace WinCC2SQLiteLVQ
{
    public partial class SQLite2WinCCControl : UserControl
    {

        public string HT_SQLitePath { get; set; } //数据库存放地址
        public string HT_SQL { get; set; } // SQL语句
        public string HT_Tag2WinCC { get; set; } // 存入WinCC数据点

        public SQLite2WinCCControl()
        {
            InitializeComponent();
        }

        public void initSQLiteDB()
        {
            string conStr = "data source= " + HT_SQLitePath + ";version=3;";

            using (SQLiteConnection connect = new SQLiteConnection(conStr))
            {
                //打开文件
                connect.Open();
                DataTable dt = new DataTable();
                SQLiteDataAdapter sqliteAdapter = new SQLiteDataAdapter(HT_SQL, connect);
                sqliteAdapter.Fill(dt);//结果填充到DataTable中
                this.dataGridView1.DataSource = dt;
            }
        }

        private void ChooseTable_Click(object sender, EventArgs e)
        {
            HMIRuntime cac = new HMIRuntime();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Selected == true)
                {
                    cac.Tags[HT_Tag2WinCC].Write(dataGridView1.Rows[i].Cells[0].Value.ToString());
                }
            }
        }

        private void HT_SQLite2WinCC_Load(object sender, EventArgs e)
        {
            initSQLiteDB();
        }
    }
}
