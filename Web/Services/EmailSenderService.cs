using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Web.Infrastructure;

namespace Web.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        public ApplicationUserManager UserManager { get; set; }
        public EmailSenderService(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public async Task SendEmailForSignUp(long userId, string callBackUrl)
        {

            await UserManager.SendEmailAsync(userId,
                "Welcome to Chereta ",
                "<!DOCTYPE html><html lang='en'><head><meta http-equiv='Content-Type' content='text/html; charset=UTF-8'><meta charset='UTF-8'><title>Welcome</title></head>" +
                "<body style='font-family: Arial, Helvetica, sans-serif;'><div class='main_con' style='width: 275px; margin: 0 auto;'>" +
                "<div class='mc' style='text-align: center; margin: 0 auto;' align='center'> " +
                "<img class='logo' src='http://chereta.net/Content/images/cheretalogo.png' style='width: 100px; border-radius: 50px !important; margin: 0 auto;'></div><div class='mc' style='text-align: center; margin: 0 auto;' align='center'><p>Welcome to Tender Bird</p><p class='des' style='font-size: 12px; color: #8d8c8e;'>Get a daily notice of tender with Tender bird.</p></div><div class='mc' style='text-align: center; margin: 0 auto;' align='center'><table><tr><td class='button fc mc' style='text-align: center; border-radius: 5px; background-color: #60a8e5; margin: 0 auto; padding: 10px; border: #5491bc;' align='center' bgcolor='#ffa22d'> " +
                "<a href='" + callBackUrl + "' style='color: #ffffff; display: block; font-size: 14px; text-decoration: none; text-transform: uppercase;'>Click to confirm your account</a></td></tr></table></div> <br> <br> <br> <br><div class='mc' style='text-align: center; margin: 0 auto;' align='center'><p class='sm' style='font-size: 12px;'>In front of Civil Service University, Palm Building 6th floor, Addis Ababa Ethiopia</p><p class='sm' style='font-size: 12px;'>0924144733</p><p class='email' style='color: #3f88ff;'>tenderbird@gmail.com</p><h4 class='ui inverted header'>Tatarri © Copyright 2018</h4></div></div></body></html>");
        }

        public async Task SendEmailForActiveUsers(long userId, string header, string message)
        {
            await UserManager.SendEmailAsync(userId,header,
                "<!DOCTYPE html><html lang='en'><head><meta http-equiv='Content-Type' content='text/html; charset=UTF-8'><meta charset='UTF-8'>" +
                "<title>Welcome</title><style>body{font-family:Arial,Helvetica,sans-serif}</style></head><body style='font-family: Arial, Helvetica," +
                " sans-serif;'><div style='width: 275px; margin: 0 auto;'> <br><div><div style='display: inline-block;'>" +
                " <img src='http://chereta.net/Content/images/cheretalogo.png' style='width: 60px; border-radius: 30px !important;' align='left'>" +
                "<h1 style='float: left; font-size: 20px; margin-left: 10px; margin-top: 20px;'>Chereta.net</h1> " +
                "<br></div></div><br><div><p style='text-decoration: underline;'>" +
                header +
                "</p><p style='font-size: 13px; color: #6e6e6e;'> "+ message + 
                "</p></div><div><table><tr><td style='border-radius: 5px; margin: 0 auto; padding: 10px; border: #ff8b3b;' align='center' bgcolor='#ffa22d'> " +
                "<a href='http://chereta.net/User' style='color: #ffffff; display: block; font-size: 14px; text-decoration: none; text-transform: uppercase;'>" +
                "see all</a></td></tr></table></div> <br> <br> <br> <br><div><p style='font-size: 12px;'>In front of Civil Service University, Palm Building 6th floor, Addis Ababa Ethiopia</p><p style='font-size: 12px;'>011 8 93 19 90</p>" +
                "<h4 style='text-decoration: underline;'>Chereta.net © Copyright 2018</h4></div></div></body></html>");
        }

        public async Task SendWelcomeEmail(long userId,string fullName)
        {
            await UserManager.SendEmailAsync(userId, "Welcome To Chereta.net " + fullName ,
                "<!DOCTYPE html><html lang='en'><head><meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>" +
                "<meta charset='UTF-8'><title>Welcome</title><style>body{font-family:Arial,Helvetica,sans-serif}</style>" +
                "</head><body style='font-family: Arial, Helvetica, sans-serif;'><div style='max-width: 400px; margin: 0 auto;'>" +
                "<div style='margin: 0 auto;' align='center'> " +
                "<img src='http://chereta.net/Content/images/cheretalogo.png' style='width: 100px; border-radius: 50px !important; margin: 0 auto;'>" +
                "</div><div style='margin: 0 auto;' align='center'>" +
                "<p>Welcome to Chereta.net</p><p>Dear "+ fullName + "</p><p style='font-size: 14px; color: #6c6b6d;'>Thank you for your interest in our services. Please hereby find the attached Tatarri Consultancy P.L.C company profile for your consideration. As you may well know, our online business service outsourcing boards Tatarri.com and Chereta.net are both fully automated business tools on different pricing packs.</p>" +
                "<p style='font-size: 14px; color: #6c6b6d;'>Chereta.net is a fully automated technology that assist businesses to view bid announcements from every corner of Ethiopia. This method is very beneficial to you as a business whenever you are seeking to bid on tender announcements, because of the ease of access to receive tender announcements based on your custom need.</p>" +
                "<p style='font-size: 14px; color: #6c6b6d;'>You can review and save different bid announcements at your own leisure using our mobile application, website and by our email alert. Our database currently has over 50 new tender announcements posted daily and is additionally composed of 5 different newspapers.</p><p style='font-size: 14px; color: #6c6b6d;'> Please refer to the attached corporate profile for the details of the packages we offer. When you decide to use our service, call us on <a href='tel:251118931990'>+251 118 93 19 90</a> and we will immediately set up your account, email you login credentials with user manual and prepare a VAT receipt right in few minutes after we receive the payment.</p>" +
                "<div style='margin: 0 auto;' align='center'>" +
                "<table style='text-align: center; margin: 0 auto;'><tr><td style='border-radius: 5px; margin: 0 auto; padding: 5px; border: #388fff;' align='center' bgcolor='#5c92ff'> " +
                "<a href='http://chereta.net/Content/Other/TCF.pdf' style='color: #ffffff; display: block; font-size: 10px; text-decoration: none; text-transform: uppercase;'>Click here to download the attached corporate profile.</a></td></tr></table>" +
                "</div>"+
                "</div><br> <br> <br> <br><div style='margin: 0 auto;' align='center'>" +
                "<p style='font-size: 12px;'>In front of Civil Service University, Palm Building 6th floor, Addis Ababa Ethiopia</p><p style='font-size: 12px;'>+251 118 93 19 90</p><p style='font-size: 12px;'>+251 9 24 14 47 33</p><p style='font-size: 12px;'>+251 9 32 23 22 63</p><p style='color: #3f88ff;'>connect@chereta.com</p><h4>Chereta © Copyright 2018</h4></div></div></body></html>");
        }

        public async Task SendAccountRenewalEmail(long userId,string fullName,string email,string password,string timeSpan)
        {
            await UserManager.SendEmailAsync(userId, "Chereta.net : Account Activated",
                "<!DOCTYPE html><html lang='en'><head><meta http-equiv='Content-Type' content='text/html; charset=UTF-8'><meta charset='UTF-8'>" +
                "<title>Welcome</title><style>body{font-family:Arial,Helvetica,sans-serif}</style></head>" +
                "<body style='font-family: Arial, Helvetica, sans-serif;'><div style='max-width: 400px; margin: 0 auto;'>" +
                "<div style='margin: 0 auto;' align='center'> <img src='http://chereta.net/Content/images/cheretalogo.png' style='width: 100px; border-radius: 50px !important; margin: 0 auto;'></div>" +
                "<div style='margin: 0 auto;' align='center'><p>Your account is activated</p><p>Dear "+fullName+"</p>" +
                "<p style='font-size: 14px; color: #6c6b6d;'> You have subscribed for "+timeSpan+" on Chereta.net</p>" +
                "<p style='font-size: 14px; color: #6c6b6d;'> We have activated your account. You can login in to your account with the following credentials on www.chereta.net</p>" +
                "<div style='width: 100%; height: 1px; background-color: #ffa825;'></div>" +
                "<p style='font-size: 14px; color: #6c6b6d;'> Email = "+email+" <br> "+password+"</p>" +
                "<div style='width: 100%; height: 1px; background-color: #ffa825;'></div>" +
                "<p style='font-size: 14px; color: #6c6b6d;'> To download android mobile application please follow this link: <a href='https://play.google.com/store/apps/details?id=net.chereta.chereta&amp;hl=en'>Here</a>" +
                "</p>" +
                "<div style='margin: 0 auto;' align='center'>" +
                "<table style='text-align: center; margin: 0 auto;'><tr><td style='border-radius: 5px; margin: 0 auto; padding: 5px; border: #388fff;' align='center' bgcolor='#5c92ff'> " +
                "<a href='http://chereta.net/Content/Other/UserManual.pdf' style='color: #ffffff; display: block; font-size: 10px; text-decoration: none; text-transform: uppercase;'>Click here to download the user manual.</a></td></tr></table>" +
                "</div>" +
                "<p style='font-size: 14px; color: #6c6b6d;'>Thank you.</p></div><br> <br> <br> <br><div style='margin: 0 auto;' align='center'><p style='font-size: 12px;'>In front of Civil Service University, Palm Building 6th floor, Addis Ababa Ethiopia</p><p style='font-size: 12px;'>+251 118 93 19 90</p><p style='font-size: 12px;'>+251 9 24 14 47 33</p><p style='font-size: 12px;'>+251 9 32 23 22 63</p><p style='color: #3f88ff;'>connect@chereta.com</p><h4>Chereta © Copyright 2018</h4></div></div></body></html>");
        }
    }

    public interface IEmailSenderService
    {
        Task SendEmailForSignUp(long userId, string callBackUrl);

        Task SendEmailForActiveUsers(long userId, string header, string message);

        Task SendWelcomeEmail(long userId,string fullName);
        Task SendAccountRenewalEmail(long userId,string fullName, string email, string password, string timeSpan);
    }
}