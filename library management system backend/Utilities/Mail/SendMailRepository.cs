﻿using library_management_system.Database;
using MailSend.DB;
using MailSend.DB.Entity;
using MailSend.Enums;
using MailSend.Models;

namespace MailSend.Repo
{
    public class SendMailRepository(LibraryDbContext _Context)
    {
        public async Task<EmailTemplate> GetTemplate(EmailTypes emailTypes)
        {
            var  template = _Context.EmailTemplates.Where(x=> x.emailTypes==emailTypes).FirstOrDefault();
            return template;
        }

    }
}
