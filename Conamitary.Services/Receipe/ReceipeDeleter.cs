using AutoMapper;
using Conamitary.Database.Abstract;
using Conamitary.Database.Abstract.Receipe;
using Conamitary.Dtos.Receipes;
using Conamitary.Services.Abstract.Commons;
using Conamitary.Services.Abstract.Receipe;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Conamitary.Services.Receipe
{
    public class ReceipeDeleter : IReceipeDeleter
    {
        private readonly IDbReceipeGetter _dbReceipeGetter;
        private readonly IDbReceipeDeleter _dbReceipeDeleter;
        private readonly IDbContextSaver _dbContextSaver;
        private readonly IMapper _mapper;
        private readonly IFileApiHttpClient _fileApiHttpClient;
        private readonly ILogger<ReceipeDeleter> _logger;

        public ReceipeDeleter(
            IDbReceipeGetter dbReceipeGetter,
            IDbReceipeDeleter dbReceipeDeleter,
            IDbContextSaver dbContextSaver,
            IMapper mapper,
            IFileApiHttpClient fileApiHttpClient,
            ILogger<ReceipeDeleter> logger)
        {
            _dbReceipeGetter = dbReceipeGetter;
            _dbReceipeDeleter = dbReceipeDeleter;
            _dbContextSaver = dbContextSaver;
            _mapper = mapper;
            _fileApiHttpClient = fileApiHttpClient;
            _logger = logger;
        }

        public async Task<ReceipeDto> Remove(Guid receipeId)
        {
            var receipe = await _dbReceipeGetter.Get(receipeId, true);
            if (receipe == null)
            {
                throw new ArgumentException($"Receipe with id: {receipeId} does not exist.");
            }
            await _dbReceipeDeleter.Delete(receipe);
            await _dbContextSaver.SaveChangesAsync();

            _logger.LogDebug($"Removed receipe with id: {receipeId}");

            await _fileApiHttpClient.InvokeDeleteFiles(receipe.Images.Select(x => x.Id));

            return _mapper.Map<ReceipeDto>(receipe);
        }
    }
}
