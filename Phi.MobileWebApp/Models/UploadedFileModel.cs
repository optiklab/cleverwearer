/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

namespace Phi.MobileWebApp.Models
{
    using System;

    public class UploadedFileModel
    {
        public UploadedFileModel(string n, string p, Int64 s)
        {
            Name = n;
            Path = p;
            Size = s;
        }

        public string Name { get; set; }
        public string Path { get; set; }
        public Int64 Size { get; set; }
    }
}