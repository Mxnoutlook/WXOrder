using WxAppWebApi.EmailUtils;
using System.Collections.Generic;
using WxAppWebApi.Entity;

namespace WxAppWebApi.service.EmailService.Impl
{
    public class EmailServiceImpl : IEmailService
    {
        async Task<SendResultEntity> IEmailService.SendVaildEmail(string VaildCode, string Addressee)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = "FileModule\\发送验证码模板.html";
            string htmlFilePath = Path.Combine(baseDirectory, relativePath);
            // 读取HTML文件内容
            string htmlContent = File.ReadAllText(htmlFilePath);
            htmlContent = htmlContent.Replace("${VaildCode}", VaildCode);

            List<string> Addresslist = new List<string>
            {
                Addressee
            };
            MailBodyEntity mailBodyEntity = new MailBodyEntity()
            {
                Body = htmlContent,
                Recipients = Addresslist,
                Subject = "季丰下单小程序验证码",
            };
            return await SendMailHelper.SendMail(mailBodyEntity);
        }

        async  Task<SendResultEntity> IEmailService.SendMail()
        {
            List<string> list = new List<String>
            {
                "a1220534572@126.com",
                "ruifeng.wang@giga-force.com"
            };
            List<MailFile> listmail = new List<MailFile>();
            MailFile mailFile = new MailFile();
            mailFile.MailFileType = "png";
            mailFile.MailFileSubType = "png";
            mailFile.MailFilePath = @"C:\Users\38980\Documents\test.png";
            listmail.Add(mailFile);
            MailBodyEntity mailBodyEntity = new MailBodyEntity()
            {
                Body = "<div id=\"header\" style=\"background-color:#FFA500;\">\r\n<h1 style=\"margin-bottom:0;\">主要的网页标题</h1></div>\r\n\r\n<div id=\"menu\" style=\"background-color:#FFD700;height:200px;width:100px;float:left;\">\r\n<b>菜单</b><br>\r\nHTML<br>\r\nCSS<br>\r\nJavaScript</div>\r\n\r\n<div id=\"content\" style=\"background-color:#EEEEEE;height:200px;width:400px;float:left;\">\r\n内容在这里</div>\r\n\r\n<div id=\"footer\" style=\"background-color:#FFA500;clear:both;text-align:center;\">\r\n版权 © runoob.com</div>\r\n\r\n</div>",
                Recipients = list,
                Subject = "测试",
                MailFiles = listmail
            };
            return await SendMailHelper.SendMail(mailBodyEntity);
        }

        async Task<SendResultEntity> IEmailService.SendOrderEmail(TbOrder tbOrder)
        {

            // 1、读取HTML邮件模板
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = "FileModule\\下单模板.html";
            string htmlFilePath = Path.Combine(baseDirectory, relativePath);
            string htmlContent = File.ReadAllText(htmlFilePath);

            // 2、收件人、委托实验室、抄送人、附件List
            string[] addresseeList = tbOrder.Addressee.Split(';');
            var addresseeTotalStr = "";
            List<string> addresseeTotalList = new List<string>(); 
            foreach (string entry in addresseeList)
            {
                string[] parts = entry.Split(',');

                if (parts.Length == 2)
                {
                    addresseeTotalStr+=","+ parts[0].Trim();

                    string emailWithBrackets = parts[1].Trim();

                    // 去掉尖括号
                    string email = emailWithBrackets.TrimStart('<').TrimEnd('>');
                    addresseeTotalList.Add(email);

                }
            };
            var ccpeopleTotalStr = "";
            string[] ccpeopleList = tbOrder.Ccpeople.Split(';');
            List<string> ccpeopleTotalList = new List<string>();
            foreach (string entry in ccpeopleList)
            {
                string[] parts = entry.Split(',');

                if (parts.Length == 2)
                {
                    ccpeopleTotalStr += "," + parts[0].Trim();

                    string emailWithBrackets = parts[1].Trim();

                    // 去掉尖括号
                    string email = emailWithBrackets.TrimStart('<').TrimEnd('>');
                    ccpeopleTotalList.Add(email);

                }
            };

            string[] labList = tbOrder.OrderLab.Split(';');
            List<string> labTotalList = new List<string>();
            var labTotalStr= "";
            foreach (string entry in labList)
            {
                string[] parts = entry.Split(',');

                if (parts.Length == 2)
                {

                    labTotalStr += "," + parts[0].Trim();
                    string emailWithBrackets = parts[1].Trim();

                    // 去掉尖括号
                    string email = emailWithBrackets.TrimStart('<').TrimEnd('>');
                    labTotalList.Add(email);
                }
            };

            List<MailFile> mailFiles = new List<MailFile>();
            string[] fileList = tbOrder.Filepath.Split(';');

            foreach (string entry in fileList)
            {
                MailFile mailFile = new MailFile();
                mailFile.MailFilePath = @$"{entry}";
                mailFile.MailFileType = Path.GetExtension(entry);
                mailFile.MailFileSubType = Path.GetExtension(entry);
                mailFiles.Add(mailFile);
            };

            // 3、主题、取件日期、备注信息
            var orderTheme = tbOrder.Ordertitle;
            var orderPickTime = tbOrder.Pickuptime.ToString();
            var mailbody = tbOrder.Mailbody;


            // 4、赋值HTML文件
            htmlContent = htmlContent.Replace("${收件人}", addresseeTotalStr).Replace("${备注信息}", mailbody).Replace("${时间}", orderPickTime).Replace("${实验室}", labTotalStr);


            // 5、组装邮件内容
     
            MailBodyEntity mailBodyEntity = new MailBodyEntity()
            {
                Body = htmlContent,
                Recipients = addresseeTotalList,
                Subject = orderTheme,
                Cc= ccpeopleTotalList,
                MailFiles= mailFiles
            };

            return await SendMailHelper.SendMail(mailBodyEntity);
        }
    }
}
