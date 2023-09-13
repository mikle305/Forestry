using System;
using System.Threading;
using System.Threading.Tasks;
using Additional.Constants;
using Cysharp.Threading.Tasks;
using Services.Notifications;

namespace Services.Server
{
    public class RequestWorker : MonoBehaviourSingleton<RequestWorker>
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

        private async UniTask ProcessRequest(
            Task request,
            float timeout,
            Action actionOnSuccess,
            MessageId? messageOnSuccess)
        {
            var cts = new CancellationTokenSource();
            TimeSpan maxRequestTimeout = TimeSpan.FromSeconds(timeout);
            cts.CancelAfterSlim(maxRequestTimeout);

            var errorResponse = new ErrorResponse();
            var success = false;
            await request
                .ContinueWith(task =>
                {
                    if (task.IsCanceled)
                        errorResponse.InternalError = ErrorId.RequestTimeout;
                    else if (task.IsFaulted)
                        if (task.Exception != null)
                            errorResponse.ExternalError = GetExternalError(task);
                        else
                            errorResponse.InternalError = ErrorId.Unknown;
                    else
                        success = true;
                }, cts.Token)
                .AsUniTask();

            if (success)
            {
                actionOnSuccess?.Invoke();
                HandleMessage(messageOnSuccess);
            }
            else
            {
                HandleError(errorResponse);
            }
        }

        private static string GetExternalError(Task task)
        {
            if (task.Exception == null)
                return string.Empty;

            Exception exception = task.Exception;
            while (true)
            {
                if (exception is not AggregateException aggregateException ||
                    aggregateException.InnerExceptions.Count == 0)
                    break;

                exception = aggregateException.InnerExceptions[0];
            }

            return exception.Message;
        }

        private void HandleMessage(MessageId? messageOnSuccess)
        {
            if (messageOnSuccess != null)
                _messageNotifier.NotifyMessage((MessageId)messageOnSuccess);
        }

        private void HandleError(ErrorResponse errorResponse)
        {
            if (errorResponse.InternalError != null)
                _messageNotifier.NotifyError((ErrorId)errorResponse.InternalError);
            else if (!string.IsNullOrEmpty(errorResponse.ExternalError))
                _messageNotifier.NotifyError(errorResponse.ExternalError);
        }

        private class ErrorResponse
        {
            public ErrorId? InternalError { get; set; }
            public string ExternalError { get; set; } = string.Empty;
        }
    }
}