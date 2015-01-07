namespace shareclass
{
    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Text;

    public class OQSSMail
    {
        private MailMessage a;
        private SmtpClient b;

        public OQSSMail(string sEmailAccount_UserName, string sEmailAccount_PassWord, string sEmailAccount_SMTPHost)
        {
            this.b = new SmtpClient(sEmailAccount_SMTPHost);
            this.b.Credentials = new NetworkCredential(sEmailAccount_UserName, sEmailAccount_PassWord);
        }

        public void clearEmailObj()
        {
            this.a.Dispose();
        }

        public string sendMail(string sSenderEmail, string sTargetEmail, string sSubject, string sMailContent)
        {
            this.a = new MailMessage(new MailAddress(sSenderEmail), new MailAddress(sTargetEmail));
            this.a.Subject = sSubject;
            this.a.IsBodyHtml = true;
            this.a.Body = sMailContent.Replace("<$Email$>", sSenderEmail);
            this.b.EnableSsl = true;
            this.b.Timeout = 0xc350;
            try
            {
                if ((1 != 0) && (0 != 0))
                {
                }
                this.b.Send(this.a);
            }
            catch
            {
                this.b.EnableSsl = false;
                this.b.Send(this.a);
            }
            return (sTargetEmail + ":OK");
        }

        public string sendMultiMail(string sSenderEmail, string sTargetEmailStr, string sSubject, string sMailContent)
        {
            StringBuilder builder;
        Label_001B:
            builder = new StringBuilder();
            string[] strArray = sTargetEmailStr.Split(new char[] { ';' });
            int index = 0;
            int num2 = 3;
        Label_0002:
            switch (num2)
            {
                case 0:
                    if (index < strArray.Length)
                    {
                        builder.Append(this.sendMail(sSenderEmail, strArray[index], sSubject, sMailContent));
                        index++;
                        if ((1 != 0) && (0 != 0))
                        {
                        }
                        num2 = 1;
                    }
                    else
                    {
                        num2 = 2;
                    }
                    goto Label_0002;

                case 1:
                case 3:
                    num2 = 0;
                    goto Label_0002;

                case 2:
                    return builder.ToString();
            }
            goto Label_001B;
        }
    }
}

