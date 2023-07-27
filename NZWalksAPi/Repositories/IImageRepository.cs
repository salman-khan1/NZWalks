using NZWalksAPi.Models.Domain;

namespace NZWalksAPi.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
