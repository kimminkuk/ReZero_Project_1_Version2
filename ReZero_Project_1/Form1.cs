using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

///////Excel/////////
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

///////Crawler/////////
using HtmlAgilityPack;
using System.Net;
using System.Web;
using System.IO;

namespace ReZero_Project_1
{
    public partial class Form1 : Form
    {
        //All Reference....
        Excel.Application excelApp = null;
        Excel.Workbook wb = null;
        Excel.Worksheet ws = null;
        private Timer timer;
        bool timer_end = false;
        bool excel_load_flg = false;
        
        private const int ROW_MAX = 3722;

        string jusik_code = null;
        int err_cnt = 0;

        //엑셀 표 대체
        sangjang s1 = new sangjang();

        //Method Set
        MethodClass call_method = new MethodClass();

        public Form1()
        {
            InitializeComponent();

            // progressbar1 timer
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer_Tick);
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int index = BAP.SelectedIndex;
            //string item = BAP.SelectedItem.ToString();
            //
            //textBox9.Text = index + "/" + item + "Selected";
        }

        //Excel Load
        private void button2_Click(object sender, EventArgs e)
        {
        
            //progressbar1 + time
            timer.Start();
            progressBar1.PerformStep();

            string st_bt2 = textBox1.Text;

            sangjang sj = new sangjang();

            //3721 Company Max
            for (int i = 0; i < 3721; i++)
            {
                if (sj.jongmok[i].Equals(st_bt2))
                {
                    //종목 명
                    textBox1.Text = sj.jongmok[i];

                    //종목코드
                    //textBox2.Text = call_method.IsParseNumber(sj.company[i]);
                    textBox2.Text = Convert.ToString(sj.company[i]);
                    break;
                }
                if (i == 3720) textBox2.Text = "ERR";
            }
            timer_end = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //CheckedListBox.
            //BAP.SetItemChecked(0, true);
            //
            //BAP.SetItemChecked(1, true);
            //
            //BAP.SetItemChecked(2, true);

            string item = BAP.SelectedItem.ToString();

            textBox9.Text = item + "Selected";
        }


        // Add Func
        void timer_Tick(object sender, EventArgs e)
        {
            if (timer_end == true)
            {
                timer.Stop();
                progressBar1.Enabled = false;
            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        //회사 명
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.MaxLength = 15;
            if (excel_load_flg == true) textBox1.ReadOnly = true;
            else textBox1.ReadOnly = false; // 초기화 후 회사 명 다시 입력 가능
        }

        //종목 코드
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.MaxLength = 15;
            if (excel_load_flg == true) textBox2.ReadOnly = true;
        }

        //대표자명
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.MaxLength = 15;
            if (excel_load_flg == true) textBox3.ReadOnly = true;
        }

        //결산 월
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            textBox5.MaxLength = 15;
            if (excel_load_flg == true) textBox5.ReadOnly = true;
        }

        //지역
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            textBox6.MaxLength = 15;
            if (excel_load_flg == true) textBox6.ReadOnly = true;
        }

        //업종
        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            textBox7.MaxLength = 1000;
            if (excel_load_flg == true) textBox7.ReadOnly = true;
        }

        //주요제품
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            textBox4.MaxLength = 1000;
            if (excel_load_flg == true) textBox4.ReadOnly = true;
        }

        //초기화 버튼
        private void button3_Click(object sender, EventArgs e)
        {
            excel_load_flg = false;

            //회사 명
            textBox1.Text = "검색어 입력";

            //종목 코드
            textBox2.Text = null;

            //대표자명
            textBox3.Text = null;

            //결산 월
            textBox5.Text = null;

            //지역
            textBox6.Text = null;

            //주요제품
            textBox4.Text = null;

            //업종
            textBox7.Text = null;

            //타이머 바
            progressBar1.Value = 0;

            jusik_code = null;

            //크롤링 정보
            textBox8.Text = null;

            // AI 선택
            textBox9.Text = null;
        }

        //Data 수집 버튼 
        //Naver 주식 정보 크롤링
        //https://html-agility-pack.net/
        ////https://finance.naver.com/item/sise_day.nhn?code=084680 (일별 시세)
        //https://finance.naver.com/item/sise_day.nhn?code=084680&page=1
        //jusik_code = "084680";
        private void button4_Click(object sender, EventArgs e)
        {
            var html = @"https://finance.naver.com/item/sise_day.nhn?code=";
            var test = jusik_code + "&page=1";
            html += test; // 주식 정보 종합

            HtmlWeb web = new HtmlWeb();
            var HtmlDoc = web.Load(html);

            int []s_dcp_int = new int[10];
            int []s_dtv_int = new int[10];
            string[] s_string = new string[10];

            var htmlNodes_1 = HtmlDoc.DocumentNode.SelectNodes("//body/table[1]/tr[3]");
            var htmlNodes_2 = HtmlDoc.DocumentNode.SelectNodes("//body/table[1]/tr[4]");
            var htmlNodes_3 = HtmlDoc.DocumentNode.SelectNodes("//body/table[1]/tr[5]");
            var htmlNodes_4 = HtmlDoc.DocumentNode.SelectNodes("//body/table[1]/tr[6]");
            var htmlNodes_5 = HtmlDoc.DocumentNode.SelectNodes("//body/table[1]/tr[7]");


            if (htmlNodes_1 == null) { err_cnt++; textBox8.Text = "error " + err_cnt + "\n"; return; }
            if (htmlNodes_2 == null) { err_cnt++; textBox8.Text = "error " + err_cnt + "\n"; return; }
            if (htmlNodes_3 == null) { err_cnt++; textBox8.Text = "error " + err_cnt + "\n"; return; }
            if (htmlNodes_4 == null) { err_cnt++; textBox8.Text = "error " + err_cnt + "\n"; return; }
            if (htmlNodes_5 == null) { err_cnt++; textBox8.Text = "error " + err_cnt + "\n"; return; }

            foreach (var node in htmlNodes_1)
            {
                if (node != null)
                {
                    //td1 날짜, td 2 종가, td7 거래량
                    var data_date               = node.SelectSingleNode("td[1]").InnerText;
                    var data_closing_price      = node.SelectSingleNode("td[2]").InnerText;
                    var data_transaction_volume = node.SelectSingleNode("td[7]").InnerText;
                    textBox8.Text = data_date + " \n" + data_closing_price + " \n" + data_transaction_volume + Environment.NewLine;

                    s_dcp_int[0] = call_method.CnvStringToInt(data_closing_price);
                    s_dtv_int[0] = call_method.CnvStringToInt(data_transaction_volume);
                }
            }

            foreach (var node in htmlNodes_2)
            {
                if (node != null)
                {
                    //td1 날짜, td 2 종가, td7 거래량
                    var data_date = node.SelectSingleNode("td[1]").InnerText;
                    var data_closing_price = node.SelectSingleNode("td[2]").InnerText;
                    var data_transaction_volume = node.SelectSingleNode("td[7]").InnerText;
                    textBox8.Text += data_date + " \n" + data_closing_price + " \n" + data_transaction_volume + Environment.NewLine;

                    s_dcp_int[1] = call_method.CnvStringToInt(data_closing_price);
                    s_dtv_int[1] = call_method.CnvStringToInt(data_transaction_volume);
                }
            }

            foreach (var node in htmlNodes_3)
            {
                if (node != null)
                {
                    //td1 날짜, td 2 종가, td7 거래량
                    var data_date = node.SelectSingleNode("td[1]").InnerText;
                    var data_closing_price = node.SelectSingleNode("td[2]").InnerText;
                    var data_transaction_volume = node.SelectSingleNode("td[7]").InnerText;
                    textBox8.Text += data_date + " \n" + data_closing_price + " \n" + data_transaction_volume + Environment.NewLine;

                    s_dcp_int[2] = call_method.CnvStringToInt(data_closing_price);
                    s_dtv_int[2] = call_method.CnvStringToInt(data_transaction_volume);
                }
            }

            foreach (var node in htmlNodes_4)
            {
                if (node != null)
                {
                    //td1 날짜, td 2 종가, td7 거래량
                    var data_date = node.SelectSingleNode("td[1]").InnerText;
                    var data_closing_price = node.SelectSingleNode("td[2]").InnerText;
                    var data_transaction_volume = node.SelectSingleNode("td[7]").InnerText;
                    textBox8.Text += data_date + " \n" + data_closing_price + " \n" + data_transaction_volume + Environment.NewLine;

                    s_dcp_int[3] = call_method.CnvStringToInt(data_closing_price);
                    s_dtv_int[3] = call_method.CnvStringToInt(data_transaction_volume);
                }
            }

            foreach (var node in htmlNodes_5)
            {
                if (node != null)
                {
                    //td1 날짜, td 2 종가, td7 거래량
                    var data_date = node.SelectSingleNode("td[1]").InnerText;
                    var data_closing_price = node.SelectSingleNode("td[2]").InnerText;
                    var data_transaction_volume = node.SelectSingleNode("td[7]").InnerText;
                    textBox8.Text += data_date + " \n" + data_closing_price + " \n" + data_transaction_volume + Environment.NewLine;

                    s_dcp_int[4] = call_method.CnvStringToInt(data_closing_price);
                    s_dtv_int[4] = call_method.CnvStringToInt(data_transaction_volume);
                }
            }




            //https://finance.naver.com/item/sise_time.nhn?code=084680&thistime=20200224161036
        }

        //크롤링 테스트 중
        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }
    }

    //c# 크롤링 class
    class agility_parse
    {
        public Encoding utf = Encoding.GetEncoding("utf-8");
        public HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
        public WebClient web = new WebClient();

        public Stream stream_source;
    }
}
