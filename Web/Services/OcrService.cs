using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tesseract;

namespace Web.Services
{
    public class OcrService:IOcrService
    {
        private readonly string _enginePath = $"{HttpRuntime.AppDomainAppPath}/tessdata";
        private readonly TesseractEngine _englishEngine;
        private readonly TesseractEngine _amharicEngine;

        public OcrService()
        {
            _englishEngine = new TesseractEngine(_enginePath, "eng");
            _amharicEngine = new TesseractEngine(_enginePath, "amh");
        }


        public string ParseText(string filePath,Lang lang)
        {
            var img = Pix.LoadFromFile(filePath);
            Page result;
            if (lang == Lang.Amh)
            {
                result = _amharicEngine.Process(img);
                return result.GetText();
            }
            if (lang == Lang.Eng)
            {
                result =  _englishEngine.Process(img);
                return result.GetText();
            }

            return "The text didn't get processed.";
             
        }
    }
    public interface IOcrService
    {
        string ParseText(string filePath,Lang lang);

    }
    public enum Lang
    {
        Amh = 1,Eng 
    }
}