﻿using Conamitary.Database.Abstract.Receipe;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conamitary.Database.Services.File
{
    public class DbFileGetter : IDbFileGetter
    {
        private readonly ConamitaryContext _context;

        public DbFileGetter(ConamitaryContext context)
        {
            _context = context;
        }

        public Task<Models.File> Get(Guid fileId, bool includeFiles = false)
        {
            if (includeFiles)
            {
                return _context.Files.
                    Include(x => x.Receipes)
                    .FirstOrDefaultAsync(x => x.Id == fileId);
            }
            return _context.Files.FirstOrDefaultAsync(x => x.Id == fileId);
        }

        public Task<IEnumerable<Models.File>> Get(bool includeFiles = false)
        {
            if (includeFiles)
            {
                return Task.FromResult(_context.Files
                    .Include(x => x.Receipes)
                    .AsEnumerable());
            }

            return Task.FromResult(_context.Files.AsEnumerable());
        }

        public Task<Models.File> Get(string md5Checksum)
        {
            return _context.Files.FirstOrDefaultAsync(x => x.Md5Checksum == md5Checksum);
        }

        public Task<bool> IsFileStillInUse(Guid fileId)
        {
            return _context.Files
                .Include(x => x.Receipes)
                .AnyAsync(x => x.Id == fileId && x.Receipes.Any());
        }
    }
}