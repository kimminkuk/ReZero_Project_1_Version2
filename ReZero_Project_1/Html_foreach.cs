﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReZero_Project_1
{
    class Global_days
    {
        public int _days = 5;
    }
    class stock_info_
    {
        public int s_date;
        public int s_dcp_int;
        public int s_dtv_int;
        public int s_dmp_int;
        public int s_dhp_int;
        public int s_dlp_int;
    }
    class Html_foreach
    {
        public stock_info_ html_get_event(stock_info_ get)
        {

            return get;
        }

    }
    class html_addr
    {
        public string html_HtmlDoc_page1(string jusik_code)
        {
            //Initial
            string put = "";
            Global_days GG = new Global_days();
            stock_info_[] stock = new stock_info_[GG._days];

            //Method Set
            MethodClass call_method = new MethodClass();

            var html = @"https://finance.naver.com/item/sise_day.nhn?code=";
            var test = jusik_code + "&page=1";
            html += test; // 주식 정보 종합

            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
            var HtmlDoc = web.Load(html);

            //html_addr html_Addr = new html_addr();
            //html_Addr.html_HtmlDoc(HtmlDoc);

            int carry = 0; 
            string[] s_string = new string[GG._days];

            HtmlAgilityPack.HtmlNodeCollection[] htmlNodes = new HtmlAgilityPack.HtmlNodeCollection[GG._days];

            //3,4,5,6,7
            //11,12,13,14,15
            for (int i = 0; i < GG._days; i++)
            {
                int jump = 3;
                if(i >= 5)
                {
                    // ex) i=5 + jump -> 11 
                    jump = 6;
                }
                htmlNodes[i] = HtmlDoc.DocumentNode.SelectNodes("//body/table[1]/tr["+i+jump+"]");
                if (htmlNodes[i] == null) { return i+jump+"err"; }

                //td1 날짜, td2 종가, td3 전일비, td4 시가, td5 고가, td6 저가 td7 거래량
                foreach (var node in htmlNodes[i])
                {
                    var data_date = node.SelectSingleNode("td[1]").InnerText;
                    var data_closing_price = node.SelectSingleNode("td[2]").InnerText;
                    var data_market_price = node.SelectSingleNode("td[4]").InnerText;
                    var data_high_price = node.SelectSingleNode("td[5]").InnerText;
                    var data_low_price = node.SelectSingleNode("td[6]").InnerText;
                    var data_transaction_volume = node.SelectSingleNode("td[7]").InnerText;

                     put = "Date:" + data_date + " 종가:" + data_closing_price + " 시가:" + data_market_price +
                        " 고가:" + data_high_price + " 저가:" + data_low_price + " 거래량:" + data_transaction_volume + Environment.NewLine;

                    stock[carry].s_date = call_method.CnvStringToInt_4(data_date);
                    stock[carry].s_dcp_int = call_method.CnvStringToInt(data_closing_price);
                    stock[carry].s_dtv_int = call_method.CnvStringToInt(data_transaction_volume);
                    stock[carry].s_dmp_int = call_method.CnvStringToInt(data_market_price);
                    stock[carry].s_dhp_int = call_method.CnvStringToInt(data_high_price);
                    stock[carry].s_dlp_int = call_method.CnvStringToInt(data_low_price);
                    carry++;
                }
            }
            return put;
        }
    } 

}