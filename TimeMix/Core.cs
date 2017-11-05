using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace TimeCore
{
    /// <summary>
    /// 时间节,包含开始时间,结束时间,名称,进度,额外字符信息
    /// </summary>
    public struct TimeSection
    {
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
    }
    /// <summary>
    /// 课表节,包含周几,名称
    /// </summary>
    public struct classTableSection
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
    public class Core
    {
        /// <summary>
        /// 当前的节
        /// </summary>
        TimeSection currentSection;
        /// <summary>
        /// 下一个节
        /// </summary>
        TimeSection nextSection;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="timePath">时间表文本路径</param>
        /// <param name="classTablePath">课表文本路径</param>
        public Core(string timePath, string classTablePath)
        {
            string[] sourceTimeSections = File.ReadAllLines(timePath, Encoding.Default);
            string[] sourceClassTableSections = File.ReadAllLines(classTablePath, Encoding.Default);

            TimeSection[] timeSections = new TimeSection[sourceTimeSections.Length];
            classTableSection[] classTableSections = new classTableSection[sourceClassTableSections.Length];
            for (int i = 0; i < sourceClassTableSections.Length; i++)
            {
                string[] FirstCut = sourceClassTableSections[i].Split(';');
                for (int j = 0; j < FirstCut.Length; j++)
                {
                    string[] SecondCut = FirstCut[j].Split('=');
                    for (int k = 0; k < SecondCut.Length; k++) {
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
            //foreach (var item in scheduleSections)
            //{
            //    Console.WriteLine(item.sourceName+"  "+item.Replacedname+"  "+item.week);
            //}
            for (int i = 0; i < sourceTimeSections.Length; i++)
            {
                string[] FirstCut = sourceTimeSections[i].Split(';');
                for (int j = 0; j < FirstCut.Length; j++)
                {
                    string[] SecondCut = FirstCut[j].Split('=');
                    for (int k = 0; k < SecondCut.Length; k++)
                    {
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
                                        if (item.week==timeSections[i].week&item.sourceName==timeSections[i].name)
                                        {
                                            timeSections[i].name = item.Replacedname;
                                        }
                                    }
                                }
                                break;
                            case (default):
                                Console.WriteLine("无法解析");//以后写入logger!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                                break;
                        }
                    }
                }
            }//解析时间表
             //foreach (var item in sections)
             //{
             //    Console.WriteLine("开始时间_{0},名称_{1},额外信息_{2}",item.beginTime.ToShortTimeString(),item.name,item.extraString);
             //}

            //若干年后一定要再写记录为0,1的情况!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            for (int i = 0; i < timeSections.Length; i++)
            {
                //Console.WriteLine();
                //Console.WriteLine(sections[i].beginTime);
                //Console.WriteLine((sections[i].beginTime.CompareTo(DateTime.Now)));
                //Console.WriteLine();
                if (timeSections[i].beginTime.CompareTo(DateTime.Now) <= 0)
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
            currentSection.endTime = nextSection.beginTime;
            string preProgress = ((DateTime.Now - currentSection.beginTime).TotalSeconds / (currentSection.endTime - currentSection.beginTime).TotalSeconds * 100).ToString();//计算进度
            int p = preProgress.IndexOf('.');
            string finalProgress = preProgress.Substring(0, p + 2);//保留小数点一位
            if (p == 1)
            {
                finalProgress = " " + finalProgress;
            }
            finalProgress += "%";
            currentSection.progress = finalProgress;
            Console.WriteLine("BeginTime={0},EndTime={1},name={2},progress={3},ExtraString={4}", currentSection.beginTime, currentSection.endTime, currentSection.name, currentSection.progress, currentSection.extraString);
        }

        public TimeSection Section { get => currentSection; set => currentSection = value; }

    }
}
