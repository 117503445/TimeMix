using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
namespace TimeMix
{
    public static class Core
    {
        private static DateTime currentTime;

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pathTime">时间表路径,文件夹</param>
        /// <param name="pathClass">课表路径,文件</param>
        /// <param name="changHeTime">长河时间</param>
        public static void Load(string pathTime, string pathClass)
        {

            //Console.WriteLine();
            //Console.WriteLine("ChangHetime={0}", changHeTime);
            for (int i = 0; i < timeSections.Length; i++)//初始化
            {
                timeSections[i] = new List<TimeSection>();
            }
            for (int i = 0; i < classSection.GetLength(0); i++)
            {
                for (int j = 0; j < classSection.GetLength(1); j++)
                {
                    classSection[i, j] = new ClassSection();
                }
            }

            XElement ClassSection = XElement.Load(pathClass);//读取课表
            foreach (var Sections in ClassSection.Elements())
            {
                foreach (var Section in Sections.Elements())
                {
                    ClassSection classsection = new ClassSection
                    {
                        Name = Section.Attribute("Name").Value
                    };
                    int week = int.Parse(Sections.Attribute("Week").Value);
                    int @class = int.Parse(Section.Attribute("Class").Value);
                    classSection[week, @class] = classsection;
                    //        Console.WriteLine(classSection[week].Count);
                    //        classSection[week][1] = classsection;
                    //       classSection[int.Parse(Sections.Attribute("Week").Value)][@class] = classsection;
                }
            }

            //for (int i = 0; i < classSection.GetLength(0); i++)
            //{
            //    for (int j = 0; j < classSection.GetLength(1); j++)
            //    {
            //        Console.WriteLine("i={0},j={1},Name={2}", i, j, classSection[i, j].Name);
            //    }
            //}


            XElement TimeFile = XElement.Load(pathTime + "TimeFile.xml");//读取总起时间表,并解析时间段

            foreach (var day in TimeFile.Elements())
            {
                foreach (var File in day.Elements())
                {
                    LoadTimeSections(int.Parse(day.Attribute("week").Value), pathTime + File.Value);
                }
            }

            for (int i = 0; i < timeSections.Length; i++)
            {
                for (int j = 0; j < timeSections[i].Count; j++)
                {
                    #region 计算结束时间
                    DateTime dt = DateTime.Now;
                    DateTime st = timeSections[i][j].beginTime;
                    timeSections[i][j].beginTime = new DateTime(dt.Year, dt.Month, dt.Day, st.Hour, st.Minute, st.Second);
                    if (j < timeSections[i].Count - 1)
                    {
                        timeSections[i][j].endTime = timeSections[i][j + 1].beginTime;
                    }
                    else
                    {
                        timeSections[i][j].endTime = new DateTime(dt.Year, dt.Month, dt.Day).AddDays(1);//末尾,置为第二天00:00
                    }
                    #endregion
                    if (timeSections[i][j].Class >= 0)
                    {
                        classSection[i, timeSections[i][j].Class].EndTime = timeSections[i][j].endTime;
                    }
                    //Console.Write(timeSections[i][j].beginTime.ToString());
                    //Console.Write(" ");
                    //Console.WriteLine(timeSections[i][j].endTime.ToString());

                    //Console.WriteLine(timeSections[i][j].ToString());
                }
            }


            for (int i = 0; i < timeSections.Length; i++)//计算最后一节课的结束时间,辅助明日课表功能
            {
                foreach (var item in timeSections[i])
                {
                    if (item.Class != -1)
                    {
                        LastClassEndTime[i] = item.endTime;
                    }
                }
            }
            //foreach (var item in lastClassEndTime)
            //{
            //    Console.WriteLine(item);
            //}


            //Console.WriteLine(currentTimeSection.beginTime);
            //Console.WriteLine(currentTimeSection.endTime);
            //Console.WriteLine();



        }

        public static string FormatProgress(double sourceProgress)
        {
            string progress = sourceProgress.ToString();
            int p = progress.IndexOf('.');
            string finalProgress = progress;
            if (p != -1)
            {
                finalProgress = progress.Substring(0, p + 2);//保留小数点一位
            }
            if (p == 1)
            {
                finalProgress = " " + finalProgress;
            }
            if (p == -1)
            {
                finalProgress += ".0";
            }
            finalProgress += "%";

            return finalProgress;

        }
        /// <summary>
        /// 填入时间段
        /// </summary>
        /// <param name="week"></param>
        /// <param name="xmlPath"></param>
        private static void LoadTimeSections(int week, string xmlPath)
        {
            XDocument day = XDocument.Load(xmlPath);
            foreach (var TimeSection in day.Elements())
            {
                foreach (var Section in TimeSection.Elements())
                {
                    TimeSection timeSection = new TimeSection
                    {
                        beginTime = Convert.ToDateTime(Section.Attribute("BeginTime").Value),
                        name = Section.Attribute("Name").Value,
                    };
                    if (Section.Attribute("Class").Value != "")
                    {
                        timeSection.Class = int.Parse(Section.Attribute("Class").Value);
                        timeSection.name = classSection[week, timeSection.Class].Name;
                    }
                    else
                    {
                        timeSection.Class = -1;
                    }
                    timeSections[week].Add(timeSection);
                }
            }
        }
        public static List<ClassSection> GetClass()
        {
            return GetClass((int)DateTime.Now.DayOfWeek);
        }
        public static List<ClassSection> GetClass(int week)
        {
            //List<string> list = new List<string>();
            //for (int i = 0; i < 9; i++)
            //{
            //    list.Add(classSection[week, i].Name);
            //}
            //return list;
            List<ClassSection> list = new List<ClassSection>();
            for (int i = 0; i < classSection.GetLength(1); i++)
            {
                list.Add(classSection[week, i]);
            }
            return list;
        }

        private static List<TimeSection>[] timeSections = new List<TimeSection>[7];
        private static ClassSection[,] classSection = new ClassSection[7, 9];
        public class TimeSection
        {
            public override string ToString()
            {
                return string.Format("BeginTime={0};EndTime={1};name={2};Class={3}", beginTime.ToString(), endTime.ToString(), name, Class);
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
            /// 课程,无课程为-1,index从0开始
            /// </summary>
            public int Class = -1;
        }
        public class ClassSection
        {
            /// <summary>
            /// 名称
            /// </summary>
            public string Name;
            public DateTime EndTime;
        }
        /// <summary>
        /// 进度
        /// </summary>
        private static double progress;
        /// <summary>
        /// 进度
        /// </summary>
        public static double Progress
        {
            get
            {
                double preProgress = ((CurrentTime - currentTimeSection.beginTime).TotalSeconds / (currentTimeSection.endTime - currentTimeSection.beginTime).TotalSeconds * 100);//计算进度
                                                                                                                                                                                  //progress = FormatProgress(preProgress);
                                                                                                                                                                                  //return progress;
                                                                                                                                                                                  //Console.WriteLine(CurrentTimeSection.ToString());
                                                                                                                                                                                  //Console.WriteLine(progress);
                return preProgress;
            }
            set => progress = value;
        }
        public static TimeSection CurrentTimeSection
        {
            get
            {

                int currentWeek = (int)DateTime.Now.DayOfWeek;

                for (int i = 0; i < timeSections[currentWeek].Count; i++)
                {
                    if (timeSections[currentWeek][i].beginTime.CompareTo(currentTime) <= 0 & timeSections[currentWeek][i].endTime.CompareTo(currentTime) > 0)
                    {
                        currentTimeSection = timeSections[currentWeek][i];
                    }
                    //Console.WriteLine(timeSections[currentWeek][i].beginTime);
                    //Console.WriteLine(timeSections[currentWeek][i].endTime);
                    //Console.WriteLine();
                }
                return currentTimeSection;

            }
            set => currentTimeSection = value;
        }
        /// <summary>
        /// 第九节课的结束时间
        /// </summary>
        public static DateTime[] LastClassEndTime { get; set; } = new DateTime[7];
        public static DateTime CurrentTime { get => currentTime; set => currentTime = value; }

        private static TimeSection currentTimeSection;
    }

}
