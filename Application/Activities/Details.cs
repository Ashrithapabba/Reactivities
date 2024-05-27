using Application.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using static Application.Activities.Details;

namespace Application.Activities
{
    public class Details
    {
        public class Query : IRequest<Result<ActivitiesDto>>
        {
            public Guid Id { get; set; }
        }
    }

    public class Handler : IRequestHandler<Query, Result<ActivitiesDto>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;

        public Handler(DataContext context, IMapper mapper, IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result<ActivitiesDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var activity = await _context.Activities
            .ProjectTo<ActivitiesDto>(_mapper.ConfigurationProvider, new {currentUsername = _userAccessor.GetUsername()})
            .FirstOrDefaultAsync(x => x.Id == request.Id);

            return Result<ActivitiesDto>.Success(activity);
        }
    }
}