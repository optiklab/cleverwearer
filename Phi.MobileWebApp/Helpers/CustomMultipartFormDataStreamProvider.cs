/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

namespace Phi.MobileWebApp.Helpers
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Net.Http;

    /// <summary>
    /// Implements custom logic for getting names for uploaded files.
    /// </summary>
    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        private Boolean _isAvatar;

        public CustomMultipartFormDataStreamProvider(string path, Boolean isAvatar)
            : base(path)
        {
            _isAvatar = isAvatar;
        }

        public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
        {
            //var name = !string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName) ? headers.ContentDisposition.FileName : "NoName";

            // this is here because Chrome submits files in quotation marks which get treated as part of the filename and get escaped
            //name = name.Replace("\"", string.Empty);

            // Get extension from existing file.
            string extension = null;
            if (!string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName))
            {
                try
                {
                    FileInfo fi = new FileInfo(headers.ContentDisposition.FileName.Trim(new[] { '"' }));
                    extension = fi.Extension;
                }
                catch (Exception)
                {
                    Debug.Assert(false);
                }
            }

            // Create new unique name and add extension.
            string name = null;
            if (extension != null)
            {
                FileInfo fi = new FileInfo(Path.GetRandomFileName());
                name = fi.Name + extension;
            }
            else
            {
                name = Path.GetRandomFileName();
            }

            return _isAvatar ? "avatar_" + name : "cloth_" + name;
        }
    }
}