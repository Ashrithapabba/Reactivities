using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class List
    {
        public class Query : IRequest<Result<List<ActivitiesDto>>> {}

        public class Handler : IRequestHandler<Query, Result<List<ActivitiesDto>>>
        {
            private readonly DataContext _context;
        private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task<Result<List<ActivitiesDto>>> Handle(Query request, CancellationToken cancellationToken)
            {

                var activities = await _context.Activities
                    .ProjectTo<ActivitiesDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return Result<List<ActivitiesDto>>.Success(activities);
            }
        }
    }
}      