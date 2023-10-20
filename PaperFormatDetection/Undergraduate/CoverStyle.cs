using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using PaperFormatDetection.Tools;
using System.Xml;

namespace PaperFormatDetection.Undergraduate
{
    class covUtil
    {
        public static List<Paragraph> CovSectionLoction(WordprocessingDocument doc, int paperType)
        {
            string[] Undergraduate = new string[] { "摘要", "Abstract", "目录", "引言", "正文", "结论", "参考文献", "附录", "致谢" };
            string[] Master = new string[] {  };
            string[] Doctor = new string[] {  };
            string[][] type = new string[][] { Undergraduate, Master, Doctor };
            Body body = doc.MainDocumentPart.Document.Body;
            IEnumerable<Paragraph> paras = body.Elements<Paragraph>();
            List<Paragraph> list = new List<Paragraph>();
            Boolean begin = false;
            Boolean end = false;
            foreach (Paragraph p in paras)
            {
                String fullText = Util.getFullRunText(p);
                begin = true;
                for (int i = 0; i < type[paperType].Length; i++)
                {
                    if (fullText.Replace(" ", "").Length < 40 && fullText.Replace(" ", "").Equals(type[paperType][i]))
                    {
                        begin = false; end = true; break;
                    }
                }
                if (begin)
                {
                    list.Add(p);
                }
                if (end)
                {
                    break;
                }
            }
            return list;
        }
    }
}

namespace PaperFormatDetection.Undergraduate
{
    class CoverStyle : Paperbase.CoverStyle
    {
        public CoverStyle(WordprocessingDocument doc)
        {
            Init();
            detectCoverStyle(covUtil.CovSectionLoction(doc, 0), doc);
        }
        /// <summary>
        /// 从XML文件给数组变量赋值 一定注意数组与XML文件是否一致对应
        /// </summary>
        public void Init()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Util.environmentDir + @"/Template/Undergraduate/Coverstyle.xml");
            int m = 0;
            //封面大标题
            XmlNodeList covHeadlineNode = xmlDoc.SelectSingleNode("Root").SelectSingleNode("Coverstyle").SelectSingleNode("Headline").ChildNodes;
            m = 0;
            foreach (XmlNode node in covHeadlineNode)
            {
                this.coverstyleHeadline[m] = node.InnerText; m++;
            }
            //封面中文小标题
            XmlNodeList covSubChNode = xmlDoc.SelectSingleNode("Root").SelectSingleNode("Coverstyle").SelectSingleNode("SubtitleCh").ChildNodes;
            m = 0;
            foreach (XmlNode node in covSubChNode)
            {
                this.coverstyleSubtitleCh[m] = node.InnerText; m++;
            }
            //封面英文小标题
            XmlNodeList covSubEnNode = xmlDoc.SelectSingleNode("Root").SelectSingleNode("Coverstyle").SelectSingleNode("SubtitleEn").ChildNodes;
            m = 0;
            foreach (XmlNode node in covSubEnNode)
            {
                this.coverstyleSubtitleEn[m] = node.InnerText; m++;
            }
            //封面学生信息
            XmlNodeList covStuInfNode = xmlDoc.SelectSingleNode("Root").SelectSingleNode("Coverstyle").SelectSingleNode("StuInformation").ChildNodes;
            m = 0;
            foreach (XmlNode node in covStuInfNode)
            {
                this.coverstyleStuInformation[m] = node.InnerText; m++;
            }
            //封面中文学校名
            XmlNodeList covSchNamChNode = xmlDoc.SelectSingleNode("Root").SelectSingleNode("Coverstyle").SelectSingleNode("SchoolNameCh").ChildNodes;
            m = 0;
            foreach (XmlNode node in covSchNamChNode)
            {
                this.coverstyleSchoolNameCh[m] = node.InnerText; m++;
            }
            //封面英文学校名
            XmlNodeList covSchNamEnNode = xmlDoc.SelectSingleNode("Root").SelectSingleNode("Coverstyle").SelectSingleNode("Date").ChildNodes;
            m = 0;
            foreach (XmlNode node in covSchNamEnNode)
            {
                this.coverstyleDate[m] = node.InnerText; m++;
            }
        }
    }
}
