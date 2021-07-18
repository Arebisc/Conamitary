using Conamitary.Database;
using Conamitary.Services.Abstract.Receipe;
using Conamitary.Services.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Conamitary.Services.Receipe
{
    public class ReceipeFileRemover: IReceipeFileRemover
    {
        private readonly ConamitaryContext _conamitaryContext;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _fileApiUrl;

        public ReceipeFileRemover(
            ConamitaryContext conamitaryContext,
            IHttpClientFactory httpClientFactory)
        {
            _conamitaryContext = conamitaryContext;
            _httpClientFactory = httpClientFactory;
        }

        public async Task RemoveFileFromReceipe(Guid fileId, Guid receipeId)
        {
            var receipeEntity = await _conamitaryContext.Receipes
                .Include(x => x.Images)
                .FirstOrDefaultAsync(x => x.Id == receipeId);
            if (receipeEntity == null)
            {
                throw new ArgumentException($"Receipe with id: {receipeId} does not exist");
            }
            if (!receipeEntity.Images.Any(x => x.Id == fileId))
            {
                throw new ArgumentException($"File with id: {fileId} does not exist in receipe with id: {receipeId}");
            }

            var fileToRemove = receipeEntity.Images.FirstOrDefault(x => x.Id == fileId);
            var file = receipeEntity.Images.Remove(fileToRemove);

            if(file == false)
            {
                throw new DbUpdateException($"Cannot remove file with id: {fileId} from receipe with id: {receipeId}");
            }

            await _conamitaryContext.SaveChangesAsync();
            await SendFileRemovedFromReceipe(fileId);
        }

        public async Task SendFileRemovedFromReceipe(Guid fileId)
        {
            var url = $"/api/file/{fileId}";
            using var client = _httpClientFactory.CreateClient(ApiTypes.FileApi);
            await client.DeleteAsync(url);
        }
    }
}
