﻿using CleanArq.Application.Common.Models;
using CleanArq.Application.Users.Common.Dtos;
using ErrorOr;
using MediatR;

namespace CleanArq.Application.Users.Queries.GetUserListPaged;

public record GetUserListPagedCommand(
    string? Search,
    string? Sort,
    int? PageIndex,
    int? PageSize
    ) : IRequest<ErrorOr<PaginatedList<UserDto>>>;