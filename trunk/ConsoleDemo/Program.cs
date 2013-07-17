using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaasOne;
using MaasOne.Finance.YahooFinance;
using MaasOne.Finance.YahooFinance.Support;
using MaasOne.Finance;
using MaasOne.Base;

namespace ConsoleDemo
{
    class Program
    {
        static void GetStockByExchangeUseSearch()
        {
            foreach (MaasOne.Finance.YahooFinance.Support.StockExchange ex in MaasOne.Finance.YahooFinance.Support.WorldMarket.DefaultStockExchanges)
            {
                MaasOne.Finance.YahooFinance.IDSearchDownload sd = new MaasOne.Finance.YahooFinance.IDSearchDownload();
                if (string.IsNullOrEmpty(ex.Suffix))
                    continue;

                int iIndex = 0;
                while (true)
                {
                    MaasOne.Finance.YahooFinance.IDQuerySearchDownloadSettings ss = new MaasOne.Finance.YahooFinance.IDQuerySearchDownloadSettings();
                    ss.Query = ex.Suffix;
                    ss.Type = SecurityType.Stock;
                    ss.ResultsIndex = 1020;
                    Response<MaasOne.Finance.YahooFinance.IDSearchResult> sr = sd.Download(ss);

                    foreach (IDSearchData sdd in sr.Result.Items)
                    {
                        Console.WriteLine(sdd.ID + "\t" + sdd.Name);
                    }

                    if (sr.Result.Items.Length < 1)
                        break;

                    iIndex += sr.Result.Items.Length;

                    Console.WriteLine("new index:" + iIndex);
                }

                Console.WriteLine("");
            }
        }

        static void GetStockByExchange()
        {
            AlphabeticIDIndexDownload dl1 = new AlphabeticIDIndexDownload();
            dl1.Settings.TopIndex = null;
            Response<AlphabeticIDIndexResult> resp1 = dl1.Download();

            Console.WriteLine("Id|Isin|Name|Exchange|Type|Industry");

            foreach (var alphabeticalIndex in resp1.Result.Items)
            {
                AlphabeticalTopIndex topIndex = (AlphabeticalTopIndex)alphabeticalIndex;
                dl1.Settings.TopIndex = topIndex;
                Response<AlphabeticIDIndexResult> resp2 = dl1.Download();

                foreach (var index in resp2.Result.Items)
                {
                    IDSearchDownload dl2 = new IDSearchDownload();
                    Response<IDSearchResult> resp3 = dl2.Download(index);


                    int i = 0;
                    foreach (var item in resp3.Result.Items)
                    {
                        Console.WriteLine(item.ID + "|" + item.ISIN + "|" + item.Name + "|" + item.Exchange + "|" + item.Type + "|" + item.Industry);
                    }

                }
            }
            return;
            foreach (MaasOne.Finance.YahooFinance.Support.StockExchange ex in MaasOne.Finance.YahooFinance.Support.WorldMarket.DefaultStockExchanges)
            {
                MaasOne.Finance.YahooFinance.CompanyInfoDownloadSettings cs = new MaasOne.Finance.YahooFinance.CompanyInfoDownloadSettings();
                MaasOne.Finance.YahooFinance.CompanyInfoDownload cd = new MaasOne.Finance.YahooFinance.CompanyInfoDownload();
                cd.Download("yhoo");

                Console.WriteLine("");
            }
        }
        static void Main(string[] args)
        {
            GetStockByExchange();
            /*
            MaasOne.Finance.YahooFinance.MarketDownload dl = new MaasOne.Finance.YahooFinance.MarketDownload();

            SectorResponse resp = dl.DownloadAllSectors();
            int iCount = 0;

            //Response/Result
            if (resp.Connection.State == ConnectionState.Success)
            {
                foreach (SectorData d in resp.Result.Items)
                {
                    MaasOne.Finance.YahooFinance.Sector sectorID = d.ID;
                    string sectorName = d.Name;
                    List<MaasOne.Finance.YahooFinance.IndustryData> industries = d.Industries;
                    int industryCount = industries.Count;

                    //Download Industries
                    MaasOne.Finance.YahooFinance.IndustryResponse respIndustries = dl.DownloadIndustries(industries);
                    foreach (IndustryData industry in respIndustries.Result.Items)
                    {
                        Console.WriteLine("Industry Name:"+industry.Name);
                        foreach (CompanyInfoData com in industry.Companies)
                        {
                            Console.Write(com.Name + ":");
                        }
                        iCount += industry.Companies.Count;
                    }
                    Console.WriteLine("\n======================="+iCount+"===============");
                }
            }*/

            
            return;
            /*
            MaasOne.Finance.YahooScreener.Criterias.PriceGainerLosersCriteria priceGainLoss = new MaasOne.Finance.YahooScreener.Criterias.PriceGainerLosersCriteria();
            priceGainLoss.PercentValues = true;
            priceGainLoss.ValueRelativeTo = MaasOne.Finance.YahooScreener.StockTradingAbsoluteTimePoint.TodaysOpen;
            priceGainLoss.GainOrLoss = MaasOne.Finance.YahooScreener.StockPriceChangeDirection.Gain;
            priceGainLoss.MinimumValue = 1.5;

            MaasOne.Finance.YahooScreener.StockScreenerDownload dl = new MaasOne.Finance.YahooScreener.StockScreenerDownload();
            Response<MaasOne.Finance.YahooScreener.StockScreenerResult> resp = dl.Download(new MaasOne.Finance.YahooScreener.Criterias.StockCriteriaDefinition[] { priceGainLoss });

            foreach (MaasOne.Finance.YahooScreener.StockScreenerData res in resp.Result.Items)
            {
                foreach (MaasOne.Finance.YahooFinance.QuoteProperty qProp in res.ProvidedQuoteProperties)
                {
                    if (res.Values(qProp) != null)
                    {
                        Console.Write(qProp.ToString());
                        Console.Write(": ");
                        Console.WriteLine(res.Values(qProp).ToString());
                    }
                }
                foreach (MaasOne.Finance.YahooScreener.StockScreenerProperty sProp in res.ProvidedScreenerProperties)
                {
                    {
                        System.Console.Write(sProp.ToString());
                        System.Console.Write(": ");
                        System.Console.WriteLine(res.AdditionalValues.ToString());
                    }
                }

                System.Console.WriteLine("");
                System.Console.WriteLine("");
            }
            
            IEnumerable<string> ids = new string[] {
                "000001.SS"
            };
            System.DateTime fromDate = new System.DateTime(2005, 1, 1);
            System.DateTime toDate = System.DateTime.Today;
            HistQuotesInterval interval = HistQuotesInterval.Monthly;
            /*
            MaasOne.Finance.YahooFinance.MarketDownload dl = new MaasOne.Finance.YahooFinance.MarketDownload();
            SectorResponse resp = dl.DownloadAllSectors();

            //Response/Result
            if (resp.Connection.State == ConnectionState.Success)
            {
                foreach (SectorData d in resp.Result.Items)
                {
                    MaasOne.Finance.YahooFinance.Sector sectorID = d.ID;
                    string sectorName = d.Name;
                    List<MaasOne.Finance.YahooFinance.IndustryData> industries = d.Industries;
                    int industryCount = industries.Count;

                    //Download Industries
                    MaasOne.Finance.YahooFinance.IndustryResponse respIndustries = dl.DownloadIndustries(industries);

                    Console.WriteLine("Sector:"+sectorName+"("+industryCount+")");

                    //Response/Result
                    if (respIndustries.Connection.State == MaasOne.Base.ConnectionState.Success)
                    {
                        foreach (MaasOne.Finance.YahooFinance.IndustryData industry in respIndustries.Result.Items)
                        {
                            int industryID = (int)industry.ID;
                            string industryName = industry.Name;
                            List<MaasOne.Finance.YahooFinance.CompanyInfoData> companies = industry.Companies;
                            int companyCount = companies.Count;

                            foreach (MaasOne.Finance.YahooFinance.CompanyInfoData company in companies)
                            {
                                string companyID = company.ID;
                                string companyName = company.Name;
                                int employees = company.FullTimeEmployees;
                                System.DateTime start = company.StartDate;
                                string industryNameByCompany = company.IndustryName;

                                Console.WriteLine(companyID+"\t"+companyName);
                            }
                        }
                    }
                }
            }


            return;
            
            //Download
            HistQuotesDownload dl = new HistQuotesDownload();
            MaasOne.Base.Response<HistQuotesResult> resp = dl.Download(ids, fromDate, toDate, interval);

            //Response/Result
            if (resp.Connection.State == ConnectionState.Success)
            {
                foreach (HistQuotesDataChain qd in resp.Result.Chains)
                {
                    string id = qd.ID;
                    Console.WriteLine(qd.ID+":("+qd.Count+")");
                    foreach(HistQuotesData _d in qd)
                    {
                        Console.Write(_d.TradingDate.Year+"-"+_d.TradingDate.Month+":"+_d.Close + "\t");
                    }
                    Console.WriteLine("");
                }
            }
            /*
            //Parameters
            IEnumerable<MaasOne.Finance.YahooFinance.Sector> sectors = new MaasOne.Finance.YahooFinance.Sector[] { MaasOne.Finance.YahooFinance.Sector.Basic_Materials };
            
            //Download Sectors
            MaasOne.Finance.YahooFinance.MarketDownload dl = new MaasOne.Finance.YahooFinance.MarketDownload();
            MaasOne.Finance.YahooFinance.SectorResponse respSectors = dl.DownloadSectors(sectors);

            //Response/Result
            if (respSectors.Connection.State == MaasOne.Base.ConnectionState.Success)
            {
                foreach (MaasOne.Finance.YahooFinance.SectorData sector in respSectors.Result.Items)
                {
                    MaasOne.Finance.YahooFinance.Sector sectorID = sector.ID;
                    string sectorName = sector.Name;
                    List<MaasOne.Finance.YahooFinance.IndustryData> industries = sector.Industries;
                    int industryCount = industries.Count;


                    //Download Industries
                    MaasOne.Finance.YahooFinance.IndustryResponse respIndustries = dl.DownloadIndustries(industries);

                    Console.WriteLine(industryCount);

                    //Response/Result
                    if (respIndustries.Connection.State == MaasOne.Base.ConnectionState.Success)
                    {
                        foreach (MaasOne.Finance.YahooFinance.IndustryData industry in respIndustries.Result.Items)
                        {
                            int industryID = (int)industry.ID;
                            string industryName = industry.Name;
                            List<MaasOne.Finance.YahooFinance.CompanyInfoData> companies = industry.Companies;
                            int companyCount = companies.Count;

                            foreach (MaasOne.Finance.YahooFinance.CompanyInfoData company in companies)
                            {
                                string companyID = company.ID;
                                string companyName = company.Name;
                                int employees = company.FullTimeEmployees;
                                System.DateTime start = company.StartDate;
                                string industryNameByCompany = company.IndustryName;

                                //            Console.WriteLine(companyID+"\t"+companyName);
                            }
                        }
                    }

                }
            }
            */
        }
    }
}
