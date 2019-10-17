using MindfireSolutions.ViewModel;

namespace MindfireSolutions.Service.ServiceInterface
{
   public interface IContactMessage
    {
        bool CreateMessage(VMMessage message);
        VMBloggerDetails GetBloggerDetails(int id);
    }
}
