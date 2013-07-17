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
        static void Main(string[] args)
        {
            foreach (MaasOne.Finance.YahooFinance.Support.StockExchange ex in MaasOne.Finance.YahooFinance.Support.WorldMarket.DefaultStockExchanges)
            {
                MaasOne.Finance.YahooFinance.IDSearchDownload dl = new MaasOne.Finance.YahooFinance.IDSearchDownload();
                Response<IDSearchResult> resp = dl.Download("bas.de");
                Console.WriteLine(ex.ID + ":" + ex.Name + ":");

                if (resp.Connection.State == ConnectionState.Success && resp.Result.Items.Length > 0)
                {
                    IDSearchData _s = resp.Result.Items[0];

                    MaasOne.Finance.YahooFinance.Support.YID id = new MaasOne.Finance.YahooFinance.Support.YID(_s);

                    MaasOne.Finance.IID iid = id;

                    string name = id.Name;
                    string idString = id.ID;
                    string baseID = id.BaseID;
                    string suffix = id.Suffix;
                    MaasOne.Finance.YahooFinance.SecurityType type = id.Type;

                    MaasOne.Finance.YahooFinance.Support.StockExchange stockExchange = id.StockExchange;
                    if (stockExchange != null)
                    {
                        string excIdString = id.StockExchange.ID;
                        string excName = id.StockExchange.Name;
                        string excSuffix = id.StockExchange.Suffix;
                        MaasOne.Finance.YahooFinance.Support.CountryInfo country = id.StockExchange.Country;
                        //                        YahooManaged.Finance.Currency currency = id.StockExchange.Currency;
                        //                        bool isActiveNow = id.StockExchange.IsActiveLocal(System.DateTime.Now);
                    }

                }
            }
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
