using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace TimeCore
{
    /// <summary>
    /// 节,包含开始时间,结束时间,名称,进度,额外字符信息
    /// </summary>
    public struct Section
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
    }
    public class Core
    {
        /// <summary>
        /// 当前的节
        /// </summary>
        Section currentSection;
        /// <summary>
        /// 下一个节
        /// </summary>
        Section nextSection;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="schedulePath">时间表文本路径</param>
        /// <param name="classTablePath">课表文本路径</param>
        public Core(string schedulePath, string classTablePath)
        {

            string[] sourceSections = File.ReadAllLines(schedulePath,Encoding.Default);
            Section[] sections = new Section[sourceSections.Length];
            for (int i = 0; i < sourceSections.Length; i++)
            {
                string[] FirstCut = sourceSections[i].Split(';');
                for (int j = 0; j < FirstCut.Length; j++)
                {
                    string[] SecondCut = FirstCut[j].Split('=');
                    for (int k = 0; k < SecondCut.Length; k++)
                    {
                        switch (SecondCut[0])
                        {
                            case "beginTime":
                                sections[i].beginTime = Convert.ToDateTime(SecondCut[1]);
                                break;
                            case "name":
                                sections[i].name = SecondCut[1];
                                break;
                            case "extraString":
                                sections[i].extraString = SecondCut[1];
                                break;
                            case (default):
                                break;
                        }
                    }
                }
            }//解析
            //foreach (var item in sections)
            //{
            //    Console.WriteLine("开始时间_{0},名称_{1},额外信息_{2}",item.beginTime.ToShortTimeString(),item.name,item.extraString);
            //}
          
            //若干年后一定要再写记录为0,1的情况!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            for (int i = 0; i < sections.Length; i++)
            {
                //Console.WriteLine();
                //Console.WriteLine(sections[i].beginTime);
                //Console.WriteLine((sections[i].beginTime.CompareTo(DateTime.Now)));
                //Console.WriteLine();
                if (sections[i].beginTime.CompareTo(DateTime.Now)<=0)
                {
                    //现在更晚一点   
                    currentSection = sections[i];
                }
                else
                {
                    nextSection = sections[i];
                    break;
                }
            }
      //      Console.WriteLine("现在name={0},下一name={1}",currentSection.name,nextSection.name);

        }

        public Section Section { get => currentSection; set => currentSection = value; }

    }
}
