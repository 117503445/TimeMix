using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace TimeMix
{
    /// <summary>
    /// 时间节,包含开始时间,结束时间,名称,进度,额外字符信息
    /// </summary>
    public struct TimeSection
    {
        public override string ToString()
        {
            return string.Format("BeginTime={0};EndTime={1};name={2};progress={3};ExtraString={4}", beginTime.ToShortTimeString(), endTime.ToShortTimeString(), name, progress, extraString);
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime beginTime;
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime endTime;
        /// <summary>
        /// 名称
        /// </summary>
        public string name;
        /// <summary>
        /// 进度
        /// </summary>
        public string progress;
        /// <summary>
        /// 额外字符信息,用于表示是否引用课程表,是否隐藏课表等
        /// </summary>
        public string extraString;
        /// <summary>
        /// 周几,Like周一
        /// </summary>
        public string week;
        /// <summary>
        /// 是否被赋值了
        /// </summary>
        public bool available;
    }
    /// <summary>
    /// 课表节,包含周几,名称
    /// </summary>
    public struct ClassTableSection
    {
        /// <summary>
        /// 周几,Like周一
        /// </summary>
        public string week;
        /// <summary>
        /// 初始名称
        /// </summary>
        public string sourceName;
        /// <summary>
        /// 最终名称
        /// </summary>
        public string Replacedname;

    }
    public static class Core
    {
        /// <summary>
        /// 当前的节
        /// </summary>
        static TimeSection currentSection;
        /// <summary>
        /// 下一个节
        /// </summary>
        static TimeSection nextSection;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="timePath">时间表文本路径</param>
        /// <param name="classTablePath">课表文本路径</param>
        /// <param name="deltaTime">时间差,长河时间=北京时间+时间差,允许负数</param>
        public static void Update(string timePath, string classTablePath, DateTime changHeTime)
        {


            string[] sourceTimeSections = File.ReadAllLines(timePath, Encoding.Default);
            string[] sourceClassTableSections = File.ReadAllLines(classTablePath, Encoding.Default);


            string todayWeekStr = changHeTime.DayOfWeek.ToString();
            string tomorrowWeekStr = "";
            switch (todayWeekStr)
            {
                case "Monday": todayWeekStr = "周一"; tomorrowWeekStr = "周二"; break;
                case "Tuesday": todayWeekStr = "周二"; tomorrowWeekStr = "周三"; break;
                case "Wednesday": todayWeekStr = "周三"; tomorrowWeekStr = "周四"; break;
                case "Thursday": todayWeekStr = "周四"; tomorrowWeekStr = "周五"; break;
                case "Friday": todayWeekStr = "周五"; tomorrowWeekStr = "周六"; break;
                case "Saturday": todayWeekStr = "周六"; tomorrowWeekStr = "周日"; break;
                case "Sunday": todayWeekStr = "周日"; tomorrowWeekStr = "周一"; break;
            }
            // Console.WriteLine(weekstr);
            List<string> todayClassTable = new List<string>();
            List<string> tomorrowClassTable = new List<string>();
            for (int i = 0; i < sourceClassTableSections.Length; i++)
            {
                string[] FirstCut = sourceClassTableSections[i].Split(';');
                for (int j = 0; j < FirstCut.Length; j++)
                {
                    string[] SecondCut = FirstCut[j].Split('=');

                    switch (SecondCut[0])
                    {
                        case "week":
                            if (SecondCut[1] == todayWeekStr)
                            {
                                todayClassTable.Add(sourceClassTableSections[i]);
                            }
                            else if (SecondCut[1] == tomorrowWeekStr)
                            {
                                tomorrowClassTable.Add(sourceClassTableSections[i]);
                            }

                            break;
                        default:
                            break;

                    }
                }
            }//筛选课表,以后使用LINQ表达式!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!


            //foreach (var item in todayClassTable)
            //{
            //    Console.WriteLine(item.ToString());
            //}
            List<string> todayTimeSections = new List<string>();
            for (int i = 0; i < sourceTimeSections.Length; i++)
            {
                string[] FirstCut = sourceTimeSections[i].Split(';');
                for (int j = 0; j < FirstCut.Length; j++)
                {
                    string[] SecondCut = FirstCut[j].Split('=');

                    switch (SecondCut[0])
                    {
                        case "week":
                            if (IsToday(SecondCut[1]))
                            {
                                todayTimeSections.Add(sourceTimeSections[i]);
                            }
                            break;
                        default:
                            break;
                    }

                }
            }//筛选时间表,以后使用LINQ表达式!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            ClassTableSection[] classTableSections = new ClassTableSection[todayClassTable.Count];
            TimeSection[] timeSections = new TimeSection[todayTimeSections.Count];


            for (int i = 0; i < todayClassTable.Count; i++)
            {
                string[] FirstCut = todayClassTable[i].Split(';');
                for (int j = 0; j < FirstCut.Length; j++)
                {
                    string[] SecondCut = FirstCut[j].Split('=');
                    for (int k = 0; k < SecondCut.Length; k++)
                    {
                        switch (SecondCut[0])
                        {
                            case "week":
                                classTableSections[i].week = SecondCut[1];
                                break;
                            case "sourceName":
                                classTableSections[i].sourceName = SecondCut[1];
                                break;
                            case "Replacedname":
                                classTableSections[i].Replacedname = SecondCut[1];
                                break;
                            default:
                                break;
                        }
                    }
                }
            }//解析课表

            //foreach (var item in classTableSections)
            //{
            //    Console.WriteLine(item.sourceName + "  " + item.Replacedname + "  " + item.week);
            //}
            List<string> todayClassList = new List<string>();
            for (int i = 0; i < todayTimeSections.Count; i++)
            {
                string[] FirstCut = todayTimeSections[i].Split(';');
                for (int j = 0; j < FirstCut.Length; j++)
                {
                    string[] SecondCut = FirstCut[j].Split('=');

                    switch (SecondCut[0])
                    {
                        case "beginTime":
                            timeSections[i].beginTime = Convert.ToDateTime(SecondCut[1]);
                            break;
                        case "name":
                            timeSections[i].name = SecondCut[1];
                            break;
                        case "week":
                            timeSections[i].week = SecondCut[1];
                            break;
                        case "extraString":
                            timeSections[i].extraString = SecondCut[1];
                            if (timeSections[i].extraString.Contains("replace"))
                            {
                                foreach (var item in classTableSections)
                                {
                                    if (item.sourceName == timeSections[i].name)
                                    {
                                        timeSections[i].name = item.Replacedname;
                                        todayClassList.Add(item.Replacedname);
                                    }
                                }
                            }
                            break;
                        case (default):
                            Console.WriteLine("无法解析");//以后写入logger!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                            break;
                    }
                    timeSections[i].available = true;

                }
            }//解析时间表
            List<string> tempTomorrowClassTable = new List<string>();
            for (int i = 0; i < tomorrowClassTable.Count; i++)
            {
                string[] fisrtCut = tomorrowClassTable[i].Split(';');
                for (int j = 0; j < fisrtCut.Length; j++)
                {
                    string[] secondCut = fisrtCut[j].Split('=');
                    if (secondCut[0] == "Replacedname")
                    {
                        tempTomorrowClassTable.Add(secondCut[1]);
                    }
                }
            }
            //foreach (var item in sections)
            //{
            //    Console.WriteLine("开始时间_{0},名称_{1},额外信息_{2}",item.beginTime.ToShortTimeString(),item.name,item.extraString);
            //}

            //若干年后一定要再写记录为0,1的情况!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            for (int i = 0; i < timeSections.Length; i++)
            {
                //Console.WriteLine();
                //Console.WriteLine(sections[i].beginTime);
                //Console.WriteLine((sections[i].beginTime.CompareTo(changHeTime)));
                //Console.WriteLine();
                if (timeSections[i].beginTime.CompareTo(changHeTime) <= 0)
                {
                    //现在更晚一点   
                    currentSection = timeSections[i];
                }
                else
                {
                    nextSection = timeSections[i];
                    break;
                }
            }//定位当前Section
            //  Console.WriteLine("现在name={0},下一name={1}",currentSection.name,nextSection.name);

            if (nextSection.available)//看上去快到最后一位了
            {
                currentSection.endTime = nextSection.beginTime;
            }
            else
            {
                currentSection.endTime = changHeTime.Date.AddDays(1);//设置结束时间为第二天00:00
            }


            string preProgress = ((changHeTime - currentSection.beginTime).TotalSeconds / (currentSection.endTime - currentSection.beginTime).TotalSeconds * 100).ToString();//计算进度
            int p = preProgress.IndexOf('.');
            string finalProgress = preProgress.Substring(0, p + 2);//保留小数点一位
            if (p == 1)
            {
                finalProgress = " " + finalProgress;
            }
            finalProgress += "%";
            currentSection.progress = finalProgress;


            for (int i = 0; i < 9; i++)//填充课表
            {
                Core.todayClassTable[i] = todayClassList[i];
                Core.tomorrowClassTable[i] = tempTomorrowClassTable[i];
            }
            //foreach (var item in preTodayTimeSections)
            //{
            //    Console.WriteLine(item);
            //}

            //foreach (var item in tempTomorrowClassTable)
            //{
            //    Console.WriteLine(item);
            //}

        }
        /// <summary>
        /// 返回当前的节
        /// </summary>
        public static TimeSection Section { get => currentSection; set => currentSection = value; }
        public static string[] TodayClassTable { get => todayClassTable; }
        public static string[] TomorrowClassTable { get => tomorrowClassTable; }


        /// <summary>
        /// 检查是否是今天
        /// </summary>
        /// <param name="week">例:"周一"</param>
        /// <returns></returns>
        static bool IsToday(string week)
        {
            string today = DateTime.Now.DayOfWeek.ToString();
            if (today == "Monday")
            {
                today = "周一";
            }
            else
            {
                today = "日常";
            }
            if (today == week)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 返回今天的课程表
        /// </summary>
        private static string[] todayClassTable = new string[9];
        /// <summary>
        /// 返回明天的课程表
        /// </summary>
        private static string[] tomorrowClassTable = new string[9];
    }

}
