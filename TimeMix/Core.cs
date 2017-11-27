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
        public static void Update(string pathTime, string pathClass)
        {
            Update(pathTime, pathClass, DateTime.Now);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pathTime">时间表路径,文件夹</param>
        /// <param name="pathClass">课表路径,文件</param>
        /// <param name="changHeTime">长河时间</param>
        public static void Update(string pathTime, string pathClass, DateTime changHeTime)
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

                    //Console.Write(timeSections[i][j].beginTime.ToString());
                    //Console.Write(" ");
                    //Console.WriteLine(timeSections[i][j].endTime.ToString());

                    //Console.WriteLine(timeSections[i][j].ToString());
                }
            }

            int currentWeek = (int)DateTime.Now.DayOfWeek;

            for (int i = 0; i < timeSections[currentWeek].Count; i++)
            {
                if (timeSections[currentWeek][i].beginTime.CompareTo(changHeTime) <= 0 & timeSections[currentWeek][i].endTime.CompareTo(changHeTime) > 0)
                {
                    currentTimeSection = timeSections[currentWeek][i];
                }
                //Console.WriteLine(timeSections[currentWeek][i].beginTime);
                //Console.WriteLine(timeSections[currentWeek][i].endTime);
                //Console.WriteLine();
            }
            //Console.WriteLine(currentTimeSection.beginTime);
            //Console.WriteLine(currentTimeSection.endTime);
            //Console.WriteLine();
            string preProgress = ((changHeTime - currentTimeSection.beginTime).TotalSeconds / (currentTimeSection.endTime - currentTimeSection.beginTime).TotalSeconds * 100).ToString();//计算进度
            progress = FormatProgress(preProgress);
            //Console.WriteLine(CurrentTimeSection.ToString());
            //Console.WriteLine(progress);

        }
        private static string FormatProgress(string progress)
        {
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
                    timeSections[week].Add(timeSection);
                }
            }
        }
        public static List<string> GetClass()
        {
            return GetClass((int)DateTime.Now.DayOfWeek);
        }
        public static List<string> GetClass(int week)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < 9; i++)
            {
                list.Add(classSection[week, i].Name);
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
            /// 课程,
            /// </summary>
            public int Class;
        }
        public class ClassSection
        {
            /// <summary>
            /// 名称
            /// </summary>
            public string Name;
        }
        /// <summary>
        /// 进度
        /// </summary>
        private static string progress;
        public static string Progress { get => progress; set => progress = value; }
        public static TimeSection CurrentTimeSection { get => currentTimeSection; set => currentTimeSection = value; }
        private static TimeSection currentTimeSection;
    }

}
