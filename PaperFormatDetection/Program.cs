using System;
using System.IO;
using PaperFormatDetection.Tools;
using System.Text.RegularExpressions;

namespace PaperFormatDetection.Frame
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Util.paperType = "本科";
            DateTime start = DateTime.Now;
            Util.paperPath = Util.environmentDir + "\\Papers\\基于哈希编码的网络流量分类方法的研究.docx";
            
            if (args.Length > 0)
                Util.paperPath = Util.environmentDir + "\\Papers\\" + args[0];

            if (args.Length > 2)
                Util.environmentDir = args[2];

            //获取页码
            Console.WriteLine("正在获取页码...");
            MSWord msword = new MSWord();
            if (Util.paperPath.EndsWith(".doc"))
            {
                Console.WriteLine("正在将doc文件转为docx...");
                Util.paperPath = msword.DocToDocx(Util.paperPath);
                Console.WriteLine(Util.paperPath);
                Console.WriteLine("文件转换成功！");
            }
            Util.pageDic = msword.getPage(Util.paperPath);
            Console.WriteLine("test");


            foreach (var item in Util.pageDic)
            {
                Console.WriteLine(item.Key + "  " + item.Value);
            }
            Console.WriteLine("成功获取页码信息！");

            Undergraduate.PaperDetection UndergraduatePD = null;
            UndergraduatePD = new Undergraduate.PaperDetection(Util.paperPath);

            DateTime end = DateTime.Now;
            TimeSpan ts = end - start;
            Console.WriteLine("");
            Console.WriteLine(" <= 检测用时： " + ts.TotalSeconds + " =>");
            //Console.ReadKey();
            while(true)
            {

            }
        }
    }
}