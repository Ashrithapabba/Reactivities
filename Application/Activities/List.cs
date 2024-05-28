using System.IO.Compression;
using System.Security.Cryptography.X509Certificates;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<ActivitiesDto>>> 
        {
            public ActivityParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<ActivitiesDto>>>
        {
            private readonly DataContext _context;
             private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;
            public Handler(DataContext context, IMapper mapper, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _mapper = mapper;
                _context = context;
            }
            public async Task<Result<PagedList<ActivitiesDto>>> Handle(Query request, CancellationToken cancellationToken)
            {

                var query = _context.Activities
                    .Where(d=>d.Date >= request.Params.StartDate)
                    .OrderBy(d=>d.Date)
                    .ProjectTo<ActivitiesDto>(_mapper.ConfigurationProvider, new {currentUsername = _userAccessor.GetUsername()})
                    .AsQueryable();

                if (request.Params.IsGoing && !request.Params.IsHost)
                {
                    query = query.Where(x=>x.Attendess.Any(a=>a.Username == _userAccessor.GetUsername()));
                }

                if (request.Params.IsHost && !request.Params.IsGoing)
                {
                    query = query.Where(x=>x.HostUsername == _userAccessor.GetUsername());
                }

                return Result<PagedList<ActivitiesDto>>.Success(await PagedList<ActivitiesDto>.CreateAsync(query, request.Params.PageNumber, request.Params.PageSize));
            }
        }
    }
}      