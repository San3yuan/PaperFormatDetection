﻿using System.Xml;
using System.Collections.Generic;
using System;

using DocumentFormat.OpenXml.Packaging;
using PaperFormatDetection.Tools;

namespace PaperFormatDetection.Undergraduate
{
    /**
     * 论文检测类，整合论文检测的流程 
     */
    class PaperDetection
    {
        private string paperPath;
        WordprocessingDocument wd;
        public PaperDetection(string paperpath)
        {
            paperPath = paperpath;
            try
            {
                Console.WriteLine("fie opening");
                wd = WordprocessingDocument.Open(paperPath, true);
                
                detectProcess();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void detectProcess()
        {
            try
            {
                new CoverStyle(wd);
            }
            catch (Exception e)
            {

            }
            Util.printError("");
            new Abstract(wd);
            Util.printError("");
            new Catalog(wd);
            Util.printError("");
            new Text(wd);
            Util.printError("");
            //new Formula(wd);
            new Figure(wd);
            Util.printError("");
            new Tabledect(wd);
            Util.printError("");
            //new HeaderFooter(wd, "under");
            new ConclusionAndThanks(wd);
            Util.printError("");
            new Reference(wd);
            Util.printError("");
            new Appendix(wd);
            Util.printError("");
            new Punctuation(wd);

            Console.WriteLine("正在生成报告，请稍后...");
            new ErrorReport(paperPath.Substring(1 + paperPath.LastIndexOf("\\")), Util.errorLists);
            Console.WriteLine("报告生成成功！");
        }
    }
}
