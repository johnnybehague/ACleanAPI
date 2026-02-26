using LiteBus.Commands.Abstractions;
using LiteBus.Queries.Abstractions;

namespace ACleanAPI.Presentation.Interfaces;

public interface IAcMediator
{
    Task<TQueryResult> QueryAsync<TQueryResult>(IQuery<TQueryResult> query, CancellationToken cancellationToken = default);

    Task<TQueryResult> QueryAsync<TQueryResult>(IQuery<TQueryResult> query, QueryMediationSettings? queryMediationSettings = null, CancellationToken cancellationToken = default);

    Task SendAsync(ICommand command, CancellationToken cancellationToken = default);

    Task SendAsync(ICommand command, CommandMediationSettings? commandMediationSettings = null, CancellationToken cancellationToken = default);

    Task<TCommandResult> SendAsync<TCommandResult>(ICommand<TCommandResult> command, CancellationToken cancellationToken = default);

    Task<TCommandResult> SendAsync<TCommandResult>(ICommand<TCommandResult> command, CommandMediationSettings? commandMediationSettings = null, CancellationToken cancellationToken = default);
}
