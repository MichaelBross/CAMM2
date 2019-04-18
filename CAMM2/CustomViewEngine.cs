using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation
{
    public class CustomViewEngine : RazorViewEngine
    {
        public CustomViewEngine()
        {
            string[] viewLocationFormatArr = new string[4];
            viewLocationFormatArr[0] = "~/{1}/Views/{0}.cshtml";
            viewLocationFormatArr[1] = "~/{1}/Views/{0}.cshtml";
            viewLocationFormatArr[2] = "~/Views/{0}.cshtml";
            viewLocationFormatArr[3] = "~/views/Shared/{1}/{0}.cshtml";
            this.ViewLocationFormats = viewLocationFormatArr;

            string[] masterLocationFormatArr = new string[4];
            masterLocationFormatArr[0] = "~/Views/{0}.cshtml";
            masterLocationFormatArr[1] = "~/UI/{1}/{0}.vbhtml";
            masterLocationFormatArr[2] = "~/Views/Shared/{1}/{0}.cshtml";
            masterLocationFormatArr[3] = "~/UI/Shared/{1}/{0}.vbhtml";
            this.MasterLocationFormats = masterLocationFormatArr;

            string[] partialViewLocationFormatArr = new string[4];
            partialViewLocationFormatArr[0] = "~/Views/{1}/{0}.cshtml";
            partialViewLocationFormatArr[1] = "~/{1}/Views/{0}.cshtml";
            partialViewLocationFormatArr[2] = "~/Views/Shared/{1}/{0}.cshtml";
            partialViewLocationFormatArr[3] = "~/UI/Shared/{1}/{0}.vbhtml";
            this.ViewLocationFormats = partialViewLocationFormatArr;

        }
    }
}