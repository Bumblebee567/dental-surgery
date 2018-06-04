﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace DentalSurgery.Services
{
    public class EmailSender
    {
        private static readonly string _ownerEmail = "maciekgasiorek9@gmail.com";
        private static readonly string _surgeryEmail = "dentalsurgery429@gmail.com";
        public static void SendNewOpinionNotification(string userEmail, string link)
        {
            var fromAddress = new MailAddress(_surgeryEmail);
            var fromPassword = "haslomaslo1!";
            var toAddress = new MailAddress(_ownerEmail);

            string subject = "Napisano nową opinię";
            string body = link;

            Send(fromAddress, toAddress, fromPassword, subject, body);
        }
        public static void Send(MailAddress fromAddress, MailAddress toAddress, string fromPassword, string subject, string body)
        {
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)

            };
            var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            };
            smtp.Send(message);
        }
    }
}