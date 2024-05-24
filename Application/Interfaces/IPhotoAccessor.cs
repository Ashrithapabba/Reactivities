using Application.Photos;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface IPhotoAccessor
    {
        Task<PhotoUploadresult> AddPhoto(IFormFile file);
        Task<string> DeletePhoto(string PublicId);
    }
}