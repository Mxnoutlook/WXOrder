using MediatR;

namespace WxAppWebApi.EmailUtils.CommandHandler
{
    public class SendMailCommand : IRequest<SendResultEntity>
    {
        public MailBodyEntity _mailBodyEntity;

        public SendMailCommand(MailBodyEntity mailBodyEntity)
        {
            _mailBodyEntity = mailBodyEntity;
        }
    }
}
