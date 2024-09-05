using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace WxAppWebApi.EmailUtils.CommandHandler
{
    public class SendMailCommandHandler : IRequestHandler<SendMailCommand, SendResultEntity>
    {
        public async Task<SendResultEntity> Handle(SendMailCommand request, CancellationToken cancellationToken)
        {
            return await SendMailHelper.SendMail(request._mailBodyEntity);
        }
    }
}
