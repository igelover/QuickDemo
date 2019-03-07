using System;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.WebSockets;
using System.Web.WebSockets;
using System.Threading.Tasks;
using System.Globalization;
using System.Threading;
using System.Text;

namespace QuickDemo.API.Controllers
{
    /// <summary>
    /// Websocket time controller
    /// </summary>
    public class WsTimeController : ApiController
    {
        /// <summary>
        /// Endpoint exposed to connect to the websocket
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetMessage()
        {
            var status = HttpStatusCode.BadRequest;
            var context = HttpContext.Current;

            if (context.IsWebSocketRequest)
            {
                context.AcceptWebSocketRequest(ProcessRequest);
                status = HttpStatusCode.SwitchingProtocols;
            }

            return new HttpResponseMessage(status);
        }

        private async Task ProcessRequest(AspNetWebSocketContext context)
        {
            var ws = context.WebSocket;
            await Task.WhenAll(WriteTask(ws), ReadTask(ws));
        }

        private async Task ReadTask(WebSocket ws)
        {
            var buffer = new ArraySegment<byte>(new byte[1024]);
            while (true)
            {
                await ws.ReceiveAsync(buffer, CancellationToken.None).ConfigureAwait(false);
                if (ws.State != WebSocketState.Open)
                {
                    break;
                }
            }
        }

        private async Task WriteTask(WebSocket ws)
        {
            while (true)
            {
                var message = DateTime.UtcNow.ToString("MMM dd yyyy HH:mm:ss.fff UTC", CultureInfo.InvariantCulture);
                var buffer = Encoding.UTF8.GetBytes(message);

                if (ws.State != WebSocketState.Open)
                {
                    break;
                }

                var sendTask = ws.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                await sendTask.ConfigureAwait(false);

                if (ws.State != WebSocketState.Open)
                {
                    break;
                }

                await Task.Delay(1000).ConfigureAwait(false);
            }
        }
    }
}
