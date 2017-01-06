using System.Text;
using System.Net;
using System.Net.Mail;


namespace SmartIT.Module.Helpers.Email
{
    /*
     *
       Postman pm = new Postman(myComp.EMailServer, myComp.EMailPort, myComp.EMailUseSSL, myComp.EMailUsername, myComp.EMailPassword, myComp.EMailFrom);
                        if (pm.SendMail(curEmpl.Email, "Добро пожаловать в программу myClinicOn!",
                            "Здравствуйте " + curEmpl.FirstName +
                            "!<br>Вам назначена роль " + curEmpl.EmployeeRoles[0].Name +
                            ".<br>Вы можете зайти в программу по адресу <a href=\"https://myclinicon.ru/vitaloga\">https://myclinicon.ru/vitaloga</a>" +
                            "<br>Ваше имя пользователя: " + curEmpl.UserName +
                            "<br>Ваш пароль: 1234567yY"
                            ))
                        {
                            new GenericMessageBox(e.ShowViewParameters, Application, "Сообщение успешно отправлено!");
        }
         */
    public class Postman
    {
        private string smtpAddress;//= "smtp-mail.outlook.com";
        private int portNumber;//= 587;
        private bool enableSSL;// = true;
        private string emailFrom;// = "sedoy1@outlook.com";
        private string login;// = "sedoy1@outlook.com";
        private string password;// = "Sd9hkl^1";
        private MailMessage mail;
        private SmtpClient smtp;
        public Postman(string smtpAddress, int portNumber, bool enableSSL, string login,string password, string emailFrom )
        {
            this.smtpAddress = smtpAddress;
            this.portNumber = portNumber;
            this.enableSSL = enableSSL;
            this.login = login;
            this.password = password;
            this.emailFrom = emailFrom;

            // var mycomp = Session.FindObject<MyCompany>(null);
            mail = new MailMessage();
            mail.BodyEncoding = Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.From = new MailAddress(emailFrom);
            mail.To.Clear();

           smtp = new SmtpClient(smtpAddress, portNumber);
           smtp.Credentials = new NetworkCredential(login, password);
          smtp.EnableSsl = enableSSL;
        }

        
        public bool SendMail(string emailTo, string subject, string body)
        {
            emailTo = "pavel.kuznetsov@outlook.com";
            // subject = "Hello from XAF";
            // body = "Hello, I'm just writing this to say Hi!";

          
                mail.From = new MailAddress(emailFrom);
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
            // Can set to false, if you are sending pure text.

            //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
            //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));
            try
            {
                smtp.Send(mail);
                return true;
            }
            catch
            {
                return false;
            }
                

        }

    }

  
}
