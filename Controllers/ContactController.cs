using Elite.BiznessLayer.Concreate;
using Elite.DataAccsessLayer.Concreate;
using Elite.DataAccsessLayer.EFRepository;
using Elite.EntityLayer.Concreate;
using EliteStoreCore.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace EliteStoreCore.Controllers
{

	public class ContactController : Controller
	{
		ContactManager contactManager= new ContactManager(new EFContactRepository());
		NewsLettermanager nm = new NewsLettermanager(new EFNewsLetterRepository());
        [Authorize(Roles = "Admin")]

        [HttpGet]
        public IActionResult AdminNewsletter()
        {
            List<SelectListItem> values1 = (from x in nm.TGetList()
                                            select new SelectListItem
                                            {
                                                Text = x.NewsLetterMail,
                                                Value = x.NewsLetterMail.ToString(),
                                            }).ToList();
            ViewBag.v1 = values1;
            return View();
        }
        [Authorize(Roles = "Admin")]

        [HttpPost]
        public IActionResult AdminNewsletter([FromForm] MailRequestAdmin mailRequest)
        {
            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddressFrom = new MailboxAddress("Admin", "ficihaciyev@gmail.com");

            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress("User", mailRequest.ReceiverMail);
            mimeMessage.To.Add(mailboxAddressTo);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = mailRequest.Body;
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            mimeMessage.Subject = mailRequest.Subject;
            SmtpClient client = new SmtpClient();
            try
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("ficihaciyev@gmail.com", "qcjrvjxbbuvckfcv\r\n");
                client.Send(mimeMessage);
                client.Disconnect(true);
                // display a success message
                ViewBag.Message = "Email sent successfully.";
            }
            catch (Exception ex)
            {
                // display an error message
                ViewBag.Message = "Email failed to send: " + ex.Message;
            }

            return RedirectToAction("AdminNewsletter");
        }
        [Authorize(Roles = "Admin")]

        [HttpGet]
        public IActionResult Us()
        {
            List<SelectListItem> values1 = (from x in contactManager.TGetList()
                                            select new SelectListItem
                                            {
                                                Text = x.Mail,
                                                Value = x.Mail.ToString(),
                                            }).ToList();
            ViewBag.v1 = values1;
            return View();
        }
        [Authorize(Roles = "Admin")]

        [HttpPost]
        public IActionResult Us([FromForm] MailRequestAdmin mailRequest)
        {
            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddressFrom = new MailboxAddress("Admin", "ficihaciyev@gmail.com");

            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress("User", mailRequest.ReceiverMail);
            mimeMessage.To.Add(mailboxAddressTo);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = mailRequest.Body;
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            mimeMessage.Subject = mailRequest.Subject;
            SmtpClient client = new SmtpClient();
            try
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("ficihaciyev@gmail.com", "qcjrvjxbbuvckfcv\r\n");
                client.Send(mimeMessage);
                client.Disconnect(true);
                // display a success message
                ViewBag.Message = "Email sent successfully.";
            }
            catch (Exception ex)
            {
                // display an error message
                ViewBag.Message = "Email failed to send: " + ex.Message;
            }

            return RedirectToAction("AdminNewsletter");
        }
        [Authorize(Roles = "Admin")]

        public IActionResult AdminContact(int page = 1)
        {
            var values = contactManager.TGetList().ToPagedList();
            return View(values);
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
		{
			return View();
		}
		[AllowAnonymous]
		[HttpPost]
		public IActionResult Index(Contact p)
		{
			p.Status = true;
			p.Date = DateTime.Parse(DateTime.Now.ToShortDateString());
			contactManager.TAdd(p);
			return RedirectToAction("Index");
		}
		[AllowAnonymous]
		[HttpGet]
		public IActionResult NewsLetter()
		{
			return View();
		}
		[AllowAnonymous]
		[HttpPost]
		public IActionResult NewsLetter(NewsLetter p)
		{
			p.Status = true;
			nm.TAdd(p);
			return RedirectToAction("Index", "Home");
		}

	}
}
