﻿using AutoMapper;
using Conamitary.Database.Abstract;
using Conamitary.Database.Abstract.Receipe;
using Conamitary.Dtos.Receipes;
using Conamitary.Services.Abstract.Receipe;
using System;
using System.Threading.Tasks;

namespace Conamitary.Services.Receipe
{
    public class ReceipeUpdater : IReceipeUpdater
    {
        private readonly IDbReceipeGetter _dbReceipeGetter;
        private readonly IDbReceipeUpdater _dbReceipeUpdater;
        private readonly IDbContextSaver _dbContextSaver;
        private readonly IMapper _mapper;

        public ReceipeUpdater(
            IDbReceipeGetter dbReceipeGetter,
            IDbReceipeUpdater dbReceipeUpdater,
            IDbContextSaver dbContextSaver,
            IMapper mapper)
        {
            _dbReceipeGetter = dbReceipeGetter;
            _dbReceipeUpdater = dbReceipeUpdater;
            _dbContextSaver = dbContextSaver;
            _mapper = mapper;
        }

        public async Task<ReceipeDto> Update(Guid receipeId, ReceipeDto receipeDto)
        {
            var receipe = await _dbReceipeGetter.Get(receipeId);
            if (receipe == null)
            {
                throw new ArgumentException($"Receipe with id: {receipeId} does not exist");
            }
             _mapper.Map(receipeDto, receipe);

            await _dbReceipeUpdater.Update(receipe);
            await _dbContextSaver.SaveChangesAsync();

            return _mapper.Map<ReceipeDto>(receipe);
        }
    }
}
