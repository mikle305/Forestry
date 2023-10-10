using System;
using System.Threading;
using System.Threading.Tasks;
using Additional.Constants;
using Additional.Game;
using Cysharp.Threading.Tasks;
using Services.Notifications;

namespace Services.Server
{
    public class RequestWorker : MonoSingleton<RequestWorker>
    {
        private MessageNotifier _messageNotifier;


        private void Start()
        {
            _messageNotifier = MessageNotifier.Instance;
        }

        public async UniTask<TResponse> Work<TResponse>(Task<TResponse> request,
            MessageId? messageOnSuccess = null,
            int timeout = ServerConstants.DefaultRequestTimeout)
            where TResponse : class
        {
            TResponse response = null;
            await ProcessRequest(request, timeout, actionOnSuccess: () => response = request.Result, messageOnSuccess);
            return response;
        }

        public async UniTask<bool> Work(
            Task request,
            MessageId? messageOnSuccess = null,
            int timeout = ServerConstants.DefaultRequestTimeout)
        {
            var response = false;
            await ProcessRequest(request, timeout, actionOnSuccess: () => response = true, messageOnSuccess);
            return response;
        }

        public async UniTask OperationNotSupported()
        {
            await UniTask.CompletedTask;
            _messageNotifier.NotifyError(ErrorId.OperationNotSupported);
        }

        private async UniTask ProcessRequest(
            Task request,
            float timeout,
            Action actionOnSuccess,
            MessageId? messageOnSuccess)
        {
            var cts = new CancellationTokenSource();
            TimeSpan maxRequestTimeout = TimeSpan.FromSeconds(timeout);
            cts.CancelAfterSlim(maxRequestTimeout);

            try
            {
                await request.AsUniTask().AttachExternalCancellation(cts.Token);
                
                actionOnSuccess?.Invoke();
                if (messageOnSuccess != null)
                    _messageNotifier.NotifyMessage((MessageId)messageOnSuccess);
            }
            catch (OperationCanceledException)
            {
                _messageNotifier.NotifyError(ErrorId.RequestTimeout);
            }
            catch (Exception ex)
            {
                string externalError = GetExternalError(ex);
                if (string.IsNullOrEmpty(externalError))
                    _messageNotifier.NotifyError(ErrorId.Unknown);
                else
                    _messageNotifier.NotifyError(externalError);
            }
        }

        private static string GetExternalError(Exception exception)
        {
            if (exception == null)
                return string.Empty;
            
            while (true)
            {
                if (exception is not AggregateException aggregateException ||
                    aggregateException.InnerExceptions.Count == 0)
                    break;

                exception = aggregateException.InnerExceptions[0];
            }

            return exception.Message;
        }
    }
}