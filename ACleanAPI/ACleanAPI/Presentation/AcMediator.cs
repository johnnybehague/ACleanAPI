using ACleanAPI.Presentation.Interfaces;
using LiteBus.Commands.Abstractions;
using LiteBus.Queries.Abstractions;

namespace ACleanAPI.Presentation;

/// <summary>
/// Represents the mediator for executing query and command operations within the application.
/// </summary>
/// <remarks>
/// This class provides common logic for handling CRUD operations for entities.
/// </remarks>
public class AcMediator : IAcMediator
{
    /// <summary>
    /// Mediator for executing query operations within the application.
    /// </summary>
    private readonly IQueryMediator _queryMediator;

    /// <summary>
    /// Mediator for executing command operations within the application.
    /// </summary>
    private readonly ICommandMediator _commandMediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="AcMediator"/> class with the specified query mediator and command mediator.
    /// </summary>
    /// <param name="queryMediator">Mediator for executing query operations within the application.</param>
    /// <param name="commandMediator">Mediator for executing command operations within the application.</param>
    public AcMediator(IQueryMediator queryMediator, ICommandMediator commandMediator)
    {
        _queryMediator = queryMediator;
        _commandMediator = commandMediator;
    }

    /// <summary>
    /// Asynchronously executes a query.
    /// </summary>
    /// <typeparam name="TQueryResult">Query</typeparam>
    /// <param name="query">Query</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Task of <see cref="TQueryResult" /></returns>
    public Task<TQueryResult> QueryAsync<TQueryResult>(IQuery<TQueryResult> query, CancellationToken cancellationToken = default)
        => _queryMediator.QueryAsync(query, cancellationToken);

    /// <summary>
    /// Asynchronously executes a query.
    /// </summary>
    /// <typeparam name="TQueryResult">Query</typeparam>
    /// <param name="query">Query</param>
    /// <param name="queryMediationSettings">Mediation settings.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Task of <see cref="TQueryResult" /></returns>
    public Task<TQueryResult> QueryAsync<TQueryResult>(IQuery<TQueryResult> query, QueryMediationSettings queryMediationSettings, CancellationToken cancellationToken = default) 
        => _queryMediator.QueryAsync(query, queryMediationSettings, cancellationToken);

    /// <summary>
    /// Asynchronously executes a command.
    /// </summary>
    /// <param name="command">Command</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Task</returns>
    public Task SendAsync(ICommand command, CancellationToken cancellationToken = default)
        => _commandMediator.SendAsync(command, cancellationToken);

    /// <summary>
    /// Asynchronously executes a command.
    /// </summary>
    /// <param name="command">Command</param>
    /// <param name="commandMediationSettings">Mediation settings.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Task</returns>
    public Task SendAsync(ICommand command, CommandMediationSettings commandMediationSettings, CancellationToken cancellationToken = default) 
        => _commandMediator.SendAsync(command, commandMediationSettings, cancellationToken);

    /// <summary>
    /// Asynchronously executes a command.
    /// </summary>
    /// <typeparam name="TCommandResult">Command</typeparam>
    /// <param name="command">Command</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Task of <see cref="TCommandResult" /></returns>
    public Task<TCommandResult> SendAsync<TCommandResult>(ICommand<TCommandResult> command, CancellationToken cancellationToken = default)
        => _commandMediator.SendAsync(command, cancellationToken);

    /// <summary>
    /// Asynchronously executes a command.
    /// </summary>
    /// <typeparam name="TCommandResult">Command</typeparam>
    /// <param name="command">Command</param>
    /// <param name="commandMediationSettings">Mediation settings.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Task of <see cref="TCommandResult" /></returns>
    public Task<TCommandResult> SendAsync<TCommandResult>(ICommand<TCommandResult> command, CommandMediationSettings commandMediationSettings, CancellationToken cancellationToken = default) 
        => _commandMediator.SendAsync(command, commandMediationSettings, cancellationToken);
}
