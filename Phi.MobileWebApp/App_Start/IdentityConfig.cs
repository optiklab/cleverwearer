/*
 * COPYRIGHT 2014-2016 Anton Yarkov
 * 
 * Email: anton.yarkov@gmail.com
 * Skype: optiklab
*/

namespace Phi.MobileWebApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mail;
    using System.Net.Mime;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using Microsoft.AspNet.Identity;
    using Phi.MobileWebApp.Resources;
    using Phi.Models.Models;
    using Phi.Repository;
    using Phi.Repository.Services;
    using Phi.Repository.Stores;

    public class MyIdentityResult
    {
        public MyIdentityResult(IEnumerable<String> errors)
        {
            Errors = errors;

            if (errors == null || !errors.Any())
            {
                IsSucceeded = true;
            }
            else
            {
                IsSucceeded = false;
            }
        }

        public Boolean IsSucceeded { get; private set; }

        public IEnumerable<String> Errors { get; private set; }
    }

    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your sms service here to send a text message.
            return Task.FromResult(0);
        }
    }

    //public class ApplicationUserManager : UserManager<ApplicationUser>
    //{
    //    public ApplicationUserManager(IUserStore<ApplicationUser> store)
    //        : base(store)
    //    {
    //    }

    //    public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
    //    {
    //        var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
    //        // Configure validation logic for usernames
    //        manager.UserValidator = new UserValidator<ApplicationUser>(manager)
    //        {
    //            AllowOnlyAlphanumericUserNames = false,
    //            RequireUniqueEmail = true
    //        };
    //        // Configure validation logic for passwords
    //        manager.PasswordValidator = new PasswordValidator
    //        {
    //            RequiredLength = 6,
    //            RequireNonLetterOrDigit = true,
    //            RequireDigit = true,
    //            RequireLowercase = true,
    //            RequireUppercase = true,
    //        };
    //        var dataProtectionProvider = options.DataProtectionProvider;
    //        if (dataProtectionProvider != null)
    //        {
    //            manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
    //        }
    //        return manager;
    //    }
    //}

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class PhiUserManager : UserManager<PhiUser>
    {
        #region Private fields

        private IUserStore<PhiUser> _store;

        private readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Constructor

        public PhiUserManager(IUserStore<PhiUser> store)
            : base(store)
        {
            _store = store;
        }

        #endregion

        #region Public methods

        public static PhiUserManager Create(IdentityFactoryOptions<PhiUserManager> options, IOwinContext context)
        {
            var manager = new PhiUserManager(ModelContainer.Instance.GetInstance<UserStore>());
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<PhiUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug in here.
            manager.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<PhiUser>
            {
                MessageFormat = "Your security code is: {0}"
            });
            manager.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<PhiUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is: {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<PhiUser>
                        (dataProtectionProvider.Create("ASP.NET Identity"))
                    {
                        TokenLifespan = TimeSpan.FromHours(3)
                    };
            }
            return manager;
        }
        public override async Task<String> GenerateEmailConfirmationTokenAsync(string userId)
        {
            return await Task.Run(() =>
            {
                string token = System.IO.Path.GetRandomFileName().Replace(".", string.Empty);

                var user = _store.FindByIdAsync(userId).Result;

                if (user != null)
                {
                    user.SecurityStamp = token;

                    _store.UpdateAsync(user);
                }

                return token;
            });
        }
        public new async Task<MyIdentityResult> ConfirmEmailAsync(string userId, string code)
        {
            return await Task.Run(() =>
            {
                var user = _store.FindByIdAsync(userId).Result;

                if (user != null && user.SecurityStamp == code)
                {
                    user.SecurityStamp = null;

                    _store.UpdateAsync(user);

                    return new MyIdentityResult(null);
                }

                return new MyIdentityResult(new[] { GlobalResources.RefreshToken });
            });
        }
        public override async Task<IList<UserLoginInfo>> GetLoginsAsync(string userId)
        {
            return await Task.Run(() =>
            {
                return GetLogins(userId);
            });
        }
        public IList<UserLoginInfo> GetLogins(string userId)
        {
            var userLoginInfo = new List<UserLoginInfo>();

            PhiUser user = _store.FindByIdAsync(userId).Result;

            if (user != null && user.UserLogins != null)
            {
                foreach (var userLogin in user.UserLogins)
                {
                    userLoginInfo.Add(new UserLoginInfo(userLogin.LoginProvider, userLogin.ProviderKey));
                }
            }

            return userLoginInfo;
        }
        public override Task SendEmailAsync(string userId, string subject, string bodyPlainText)
        {
            try
            {
                string toAddress = "cleverwearer@gmail.com";
                if (userId != null)
                {
                    PhiUser user = _store.FindByIdAsync(userId).Result;
                    toAddress = user.Email;
                }
                var fromAddress = new MailAddress("support@cleverwearer.com", "CleverWearer");
                var smtpClient = new SmtpClient
                {
                    Host = "mail.cleverwearer.com",
                    Port = 8889,
                    EnableSsl = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(fromAddress.Address, "Qwerty12@")
                };

                using (var msg = new System.Net.Mail.MailMessage())
                {
                    msg.From = new MailAddress(fromAddress.Address);
                    msg.To.Add(new MailAddress(toAddress));
                    msg.Subject = subject;
                    msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(bodyPlainText, null, MediaTypeNames.Text.Plain));

                    smtpClient.Send(msg);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Exception SendEmailAsync userId:" + (userId ?? "[Me]") + ", subject: " + subject, ex);

                throw;
            }

            return Task.FromResult(0);
        }
        public Task SendEmailAsync(string userId, string subject, string body, string bodyPlainText)
        {
            try
            {
                string toAddress = "cleverwearer@gmail.com";
                if (userId != null)
                {
                    PhiUser user = _store.FindByIdAsync(userId).Result;
                    toAddress = user.Email;
                }

                var fromAddress = new MailAddress("support@cleverwearer.com", "CleverWearer");
                var smtpClient = new SmtpClient
                {
                    Host = "mail.cleverwearer.com",
                    Port = 8889,
                    EnableSsl = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(fromAddress.Address, "Qwerty12@")
                };

                using (var msg = new System.Net.Mail.MailMessage())
                {
                    msg.From = new MailAddress(fromAddress.Address);
                    msg.To.Add(new MailAddress(toAddress));
                    msg.Subject = subject;
                    msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(bodyPlainText, null, MediaTypeNames.Text.Plain));
                    msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html));

                    smtpClient.Send(msg);
                }
            }
            catch(Exception ex)
            {
                _logger.Error("Exception SendEmailAsync userId:" + (userId ?? "[Me]") + ", subject: " + subject, ex);

                throw;
            }

            return Task.FromResult(0);
        }

        public override async Task<bool> IsEmailConfirmedAsync(string userId)
        {
            return await Task.Run(() =>
            {
                PhiUser user = _store.FindByIdAsync(userId).Result;

                if (user != null)
                {
                    return user.EmailConfirmed;
                }

                return false;
            });
        }

        public override async Task<string> GeneratePasswordResetTokenAsync(string userId)
        {
            return await Task.Run(() =>
            {
                string token = System.IO.Path.GetRandomFileName().Replace(".", string.Empty);

                var user = _store.FindByIdAsync(userId).Result;

                if (user != null)
                {
                    user.SecurityStamp = token;

                    _store.UpdateAsync(user);
                }

                return token;
            });
        }

        public new async Task<MyIdentityResult> ResetPasswordAsync(string userId, string token, string newPassword)
        {
            return await Task.Run(() =>
            {
                var user = _store.FindByIdAsync(userId).Result;

                if (user != null && user.SecurityStamp == token)
                {
                    user.SecurityStamp = null;
                    user.PasswordHash = this.PasswordHasher.HashPassword(newPassword);

                    _store.UpdateAsync(user);

                    return new MyIdentityResult(null);
                }

                return new MyIdentityResult(new[] { GlobalResources.RefreshToken });
            });
        }

        #endregion
    }
}
