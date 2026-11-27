using System.Net;
using System.Net.Mail;
namespace _31241021966_BuiCongDanh.Extension;
public class EmailServiceNew {
    public static async Task<bool> SendMailGoogleSmtp(string from,string to,string subject,string body,string gmail,string password) {
        if(string.IsNullOrWhiteSpace(gmail)||string.IsNullOrWhiteSpace(password)) return false;
        try {
            using var client=new SmtpClient("smtp.gmail.com",587){Credentials=new NetworkCredential(gmail,password),EnableSsl=true};
            using var message=new MailMessage(from,to,subject,body){IsBodyHtml=true,BodyEncoding=System.Text.Encoding.UTF8,SubjectEncoding=System.Text.Encoding.UTF8};
            await client.SendMailAsync(message); return true;
        } catch(Exception ex) { Console.WriteLine("SMTP: "+ex.GetType().Name); return false; }
    }
}
