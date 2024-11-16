using System_FishKoi.Domain.BO.Common.Outputs;

namespace System_FishKoi.Domain.BUS
{
    public class BaseBUS
    {
        public ResponseMessage _reponseMessage = new ResponseMessage();

        public BaseBUS()
        {
            _reponseMessage.Status = MessageStatus.Success;
            _reponseMessage.Message = string.Empty;
        }

        public ResponseMessage GetReponseMessage()
        {
            return _reponseMessage;
        }
    }
}