/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
 */

namespace Phi.MobileWebApp.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;
    using Microsoft.AspNet.Identity;
    using Phi.Models.Models;
    using Phi.Repository.Stores;
    using Phi.MobileWebApp.Models;
    using Phi.MobileWebApp.Helpers;

    [Authorize]
    public class APIDressRoomController : BaseApiController
    {
        #region Private fields

        private readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IDataStore _dataStore;
        private readonly IUserProfileStore _userProfileStore;
        private readonly IUserStore<PhiUser> _userStore;

        #endregion

        #region Public constructor

        public APIDressRoomController(IDataStore dataService, IUserProfileStore userProfileStore, IUserStore<PhiUser> userStore)
        {
            this._dataStore = dataService;
            this._userProfileStore = userProfileStore;
            this._userStore = userStore;
        }

        #endregion

        #region Public methods

        [HttpPost]
        public Task<IEnumerable<UploadedFileModel>> PostImage()
        {
            string PATH = HttpContext.Current.Server.MapPath("~/user_dress_images/");
            //var folderName = "uploads";
            //var PATH = HttpContext.Current.Server.MapPath("~/" + folderName);
            //var rootUrl = Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.AbsolutePath, String.Empty);
            if (Request.Content.IsMimeMultipartContent())
            {
                Task<IEnumerable<UploadedFileModel>> task = null;

                try
                {
                    var streamProvider = new CustomMultipartFormDataStreamProvider(PATH, false);
                    task = Request.Content.ReadAsMultipartAsync(streamProvider).ContinueWith<IEnumerable<UploadedFileModel>>(t =>
                    {
                        if (t.IsFaulted || t.IsCanceled)
                        {
                            // TODO Log, admin email, etc.
                            throw new HttpResponseException(HttpStatusCode.InternalServerError);
                        }

                        var fileInfo = streamProvider.FileData.Select(i =>
                        {
                            FileInfo fi = new FileInfo(i.LocalFileName);
                            string path = Path.Combine("/user_dress_images/", fi.Name);

                            return new UploadedFileModel(
                                fi.Name,
                                path,
                                0);//i.Headers.ContentLength != null ? i.Headers.ContentLength.Value / 1024 : 0);
                        });
                        return fileInfo;
                    });
                }
                catch (Exception ex)
                {
                    _logger.Error("Exception API PostImage", ex);
                }

                return task;
            }
            else
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
            }
        }

        #endregion
    }
}
