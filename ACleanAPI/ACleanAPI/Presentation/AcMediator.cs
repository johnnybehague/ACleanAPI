using ACleanAPI.Presentation.Interfaces;
using LiteBus.Commands.Abstractions;
using LiteBus.Queries.Abstractions;

namespace ACleanAPI.Presentation;

public class AcMediator : IAcMediator
{
    private readonly IQueryMediator _queryMediator;
    private readonly ICommandMediator _commandMediator;

    public AcMediator(IQueryMediator queryMediator, ICommandMediator commandMediator)
    {
        _queryMediator = queryMediator;
        _commandMediator = commandMediator;
    }

    public Task<TQueryResult> QueryAsync<TQueryResult>(IQuery<TQueryResult> query, CancellationToken cancellationToken = default)
        => _queryMediator.QueryAsync(query, cancellationToken);

    public Task<TQueryResult> QueryAsync<TQueryResult>(IQuery<TQueryResult> query, QueryMediationSettings? queryMediationSettings = null, CancellationToken cancellationToken = default) 
        => _queryMediator.QueryAsync(query, queryMediationSettings, cancellationToken);

    public Task SendAsync(ICommand command, CancellationToken cancellationToken = default)
        => _commandMediator.SendAsync(command, cancellationToken);

    public Task SendAsync(ICommand command, CommandMediationSettings? commandMediationSettings = null, CancellationToken cancellationToken = default) 
        => _commandMediator.SendAsync(command, commandMediationSettings, cancellationToken);

    public Task<TCommandResult> SendAsync<TCommandResult>(ICommand<TCommandResult> command, CancellationToken cancellationToken = default)
        => _commandMediator.SendAsync(command, cancellationToken);

    public Task<TCommandResult> SendAsync<TCommandResult>(ICommand<TCommandResult> command, CommandMediationSettings? commandMediationSettings = null, CancellationToken cancellationToken = default) 
        => _commandMediator.SendAsync(command, commandMediationSettings, cancellationToken);
}
