using System.Net.Sockets;
using System.Net;
using System.Text;

namespace BasicHttpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var listener = new TcpListener(IPAddress.Any, 5150);
            listener.Start();
            Console.WriteLine("Web Server Running on port 5150... Press Ctrl-C to Stop...");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                NetworkStream stream = client.GetStream();

                // Read and discard the request header, assuming that it is a valid http request.
                // The end of the header is identified by two consecutive line breaks.
                // We want to read the whole header before starting to send the response. We don't care about the body (if any).
                while (true)
                {
                    // Very rudimentary search for two consecutive line breaks ("\r\n\r\n" or "\n\n") that marks the end of the header
                    byte b = 0;
                    if (!TryReadNextByte(stream, ref b)) break;
                    if (b == '\n')
                    {
                        if (!TryReadNextByte(stream, ref b)) break;
                        if (b == '\n') break;
                        if (!TryReadNextByte(stream, ref b)) break;
                        if (b == '\n') break;
                    }
                }

                // Prepare and sent the response
                string response =
                    "HTTP/1.1 200 OK\r\n" +
                    //"Content-Length: 12\r\n" + // Uncomment this to have the HttpClient working
                    "\r\n" +
                    "Hello world!";
                stream.Write(Encoding.ASCII.GetBytes(response));

                client.Close(); // Close the connection to mark the end of the response
            }
        }

        private static bool TryReadNextByte(Stream stream, ref byte b)
        {
            int i = stream.ReadByte();
            if (i == -1) return false;
            b = (byte)i;
            return true;
        }
    }
}