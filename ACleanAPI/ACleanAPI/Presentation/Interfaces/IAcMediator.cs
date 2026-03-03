using LiteBus.Commands.Abstractions;
using LiteBus.Queries.Abstractions;

namespace ACleanAPI.Presentation.Interfaces;

/// <summary>
/// Mediator interface to manage queries and commands.
/// </summary>
public interface IAcMediator
{
    /// <summary>
    /// Asynchronously executes a query.
    /// </summary>
    /// <typeparam name="TQueryResult">Query</typeparam>
    /// <param name="query">Query</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Task of <see cref="TQueryResult" /></returns>
    Task<TQueryResult> QueryAsync<TQueryResult>(IQuery<TQueryResult> query, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously executes a query.
    /// </summary>
    /// <typeparam name="TQueryResult">Query</typeparam>
    /// <param name="query">Query</param>
    /// <param name="queryMediationSettings">Mediation settings.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Task of <see cref="TQueryResult" /></returns>
    Task<TQueryResult> QueryAsync<TQueryResult>(IQuery<TQueryResult> query, QueryMediationSettings queryMediationSettings, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously executes a command.
    /// </summary>
    /// <param name="command">Command</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Task</returns>
    Task SendAsync(ICommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously executes a command.
    /// </summary>
    /// <param name="command">Command</param>
    /// <param name="commandMediationSettings">Mediation settings.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Task</returns>
    Task SendAsync(ICommand command, CommandMediationSettings commandMediationSettings, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously executes a command.
    /// </summary>
    /// <typeparam name="TCommandResult">Command</typeparam>
    /// <param name="command">Command</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Task of <see cref="TCommandResult" /></returns>
    Task<TCommandResult> SendAsync<TCommandResult>(ICommand<TCommandResult> command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously executes a command.
    /// </summary>
    /// <typeparam name="TCommandResult">Command</typeparam>
    /// <param name="command">Command</param>
    /// <param name="commandMediationSettings">Mediation settings.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Task of <see cref="TCommandResult" /></returns>
    Task<TCommandResult> SendAsync<TCommandResult>(ICommand<TCommandResult> command, CommandMediationSettings commandMediationSettings, CancellationToken cancellationToken = default);
}
