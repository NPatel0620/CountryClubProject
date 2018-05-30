using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryClubProject
{
    public class SendEmailResult  
    {
        public SendEmailResult()
        {
            this.Errors = new HashSet<SendEmailError>();
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public ICollection<SendEmailError> Errors { get; set; }

        public class SendEmailError
        {
            public string Message { get; set; }
            public string Field { get; set; }
            public string Help { get; set; }
        }
    }

    public class EmailService
    {
        private SendGrid.SendGridClient _sendGridClient;
        public EmailService(string apiKey)
        {
            this._sendGridClient = new SendGrid.SendGridClient(apiKey);
            //this._sendGridClient = new SendGrid.SendGridClient("");
        }

        public async Task<SendEmailResult> SendEmailAsync(string recipient, string subject, string htmlContent, string plainTextContent)
        {
            var from = new SendGrid.Helpers.Mail.EmailAddress("admin@codingtemplegolf.com", "Coding Temple Golf");
            var to = new SendGrid.Helpers.Mail.EmailAddress(recipient);            
            var message = SendGrid.Helpers.Mail.MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            message.SetTemplateId("cfd4e7f3-abdd-437d-b5aa-5b1f987431c0");
            var mailResult = await _sendGridClient.SendEmailAsync(message);

            SendEmailResult result = new SendEmailResult();
            if ((mailResult.StatusCode == System.Net.HttpStatusCode.OK) || (mailResult.StatusCode == System.Net.HttpStatusCode.Accepted))
            {
                //return new SendEmailResult
                //{
                //    Success = true
                //};
                result.Success = true;
            }
            else
            {
                //return new SendEmailResult
                var badMailResponse = mailResult.DeserializeResponseBody(mailResult.Body);
                result.Success = false;
                foreach (var error in badMailResponse["errors"])
                {
                    //Success = false,
                    //Message = await mailResult.Body.ReadAsStringAsync()
                    result.Errors.Add(new SendEmailResult.SendEmailError
                    {
                        Message = error.message,
                        Field = error.field,
                        Help = error.help
                    });
                }
            }

            return result;
                
        }
    }
}
