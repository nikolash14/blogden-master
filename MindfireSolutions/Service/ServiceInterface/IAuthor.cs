using MindfireSolutions.ViewModel;

namespace MindfireSolutions.Service.ServiceInterface
{
    public interface IAuthor
    {
        VMAuthor Details(string email);
        bool Edit(VMAuthor author);
        bool Delete(string email);
        VMAuthorData Data(string email);
        VMAllBlog AllBlogs(string email);
        bool DeleteBlog(int blogId);
        bool SaveEditedBlog(VMEditBlog blog);
        VMEditBlog EditBlog(int blogId);
        VMPersonalDetails PersonalDetails(string email);
        bool SavePersonalData(VMPersonalDetails data);
        bool ArchiveBlog(int blogId);
        bool Extract(int blogId);
        bool Draft(VMDraftBlog data);
        VMAllBlog AllArchivedBlog(string email);
        VMAllBlog AllDraftedContent(string email);
    }
}
