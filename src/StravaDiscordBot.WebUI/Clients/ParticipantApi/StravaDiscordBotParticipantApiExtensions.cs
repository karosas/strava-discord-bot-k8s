// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace StravaDiscordBot.WebUI.Clients.ParticipantApi
{
    using Models;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension methods for StravaDiscordBotParticipantApi.
    /// </summary>
    public static partial class StravaDiscordBotParticipantApiExtensions
    {
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='leaderboardId'>
            /// </param>
            /// <param name='participantId'>
            /// </param>
            /// <param name='fromParameter'>
            /// </param>
            public static IList<SummaryActivityResponse> GetAllActivitiesForPeriod(this IStravaDiscordBotParticipantApi operations, string leaderboardId, string participantId, System.DateTime? fromParameter = default(System.DateTime?))
            {
                return operations.GetAllActivitiesForPeriodAsync(leaderboardId, participantId, fromParameter).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='leaderboardId'>
            /// </param>
            /// <param name='participantId'>
            /// </param>
            /// <param name='fromParameter'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<IList<SummaryActivityResponse>> GetAllActivitiesForPeriodAsync(this IStravaDiscordBotParticipantApi operations, string leaderboardId, string participantId, System.DateTime? fromParameter = default(System.DateTime?), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetAllActivitiesForPeriodWithHttpMessagesAsync(leaderboardId, participantId, fromParameter, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='leaderboardId'>
            /// </param>
            /// <param name='participantId'>
            /// </param>
            public static DetailedAthleteResponse GetAthlete(this IStravaDiscordBotParticipantApi operations, string leaderboardId, string participantId)
            {
                return operations.GetAthleteAsync(leaderboardId, participantId).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='leaderboardId'>
            /// </param>
            /// <param name='participantId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<DetailedAthleteResponse> GetAthleteAsync(this IStravaDiscordBotParticipantApi operations, string leaderboardId, string participantId, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetAthleteWithHttpMessagesAsync(leaderboardId, participantId, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='body'>
            /// </param>
            public static StravaOauthResponse StartAuthentication(this IStravaDiscordBotParticipantApi operations, StartAuthenticationRequest body = default(StartAuthenticationRequest))
            {
                return operations.StartAuthenticationAsync(body).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='body'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<StravaOauthResponse> StartAuthenticationAsync(this IStravaDiscordBotParticipantApi operations, StartAuthenticationRequest body = default(StartAuthenticationRequest), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.StartAuthenticationWithHttpMessagesAsync(body, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='body'>
            /// </param>
            public static void FinishAuthentication(this IStravaDiscordBotParticipantApi operations, FinishAuthenticationRequest body = default(FinishAuthenticationRequest))
            {
                operations.FinishAuthenticationAsync(body).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='body'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task FinishAuthenticationAsync(this IStravaDiscordBotParticipantApi operations, FinishAuthenticationRequest body = default(FinishAuthenticationRequest), CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.FinishAuthenticationWithHttpMessagesAsync(body, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='leaderboardId'>
            /// </param>
            public static IList<Participant> GetAll(this IStravaDiscordBotParticipantApi operations, string leaderboardId)
            {
                return operations.GetAllAsync(leaderboardId).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='leaderboardId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<IList<Participant>> GetAllAsync(this IStravaDiscordBotParticipantApi operations, string leaderboardId, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetAllWithHttpMessagesAsync(leaderboardId, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='leaderboardId'>
            /// </param>
            /// <param name='participantId'>
            /// </param>
            public static Participant Get(this IStravaDiscordBotParticipantApi operations, string leaderboardId, string participantId)
            {
                return operations.GetAsync(leaderboardId, participantId).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='leaderboardId'>
            /// </param>
            /// <param name='participantId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<Participant> GetAsync(this IStravaDiscordBotParticipantApi operations, string leaderboardId, string participantId, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetWithHttpMessagesAsync(leaderboardId, participantId, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='leaderboardId'>
            /// </param>
            /// <param name='participantId'>
            /// </param>
            public static void Delete(this IStravaDiscordBotParticipantApi operations, string leaderboardId, string participantId)
            {
                operations.DeleteAsync(leaderboardId, participantId).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='leaderboardId'>
            /// </param>
            /// <param name='participantId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task DeleteAsync(this IStravaDiscordBotParticipantApi operations, string leaderboardId, string participantId, CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.DeleteWithHttpMessagesAsync(leaderboardId, participantId, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

    }
}
