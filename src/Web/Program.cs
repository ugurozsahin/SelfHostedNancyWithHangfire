using Implementation.Concretes.Common;
using Microsoft.Owin.Hosting;
using Mono.Unix;
using Mono.Unix.Native;
using Owin;
using System;

namespace Web
{
    class Program
    {
        static void Main(string[] args)
        {

            var strPort = Environment.GetEnvironmentVariable("PORT") ?? "8080";

            var strUrl = string.Format("http://*:{0}", strPort);

            var options = new StartOptions();
            options.Urls.Add(strUrl);
            using (WebApp.Start(options, builder =>
            {
                ServiceContext.Instance.HangfireContext.Initialize(builder);

                builder.UseNancy();
            }))
            {
                Console.WriteLine("Started Nancy on {0}...", strUrl);

                if (IsRunningOnMono())
                {
                    var terminationSignals = GetUnixTerminationSignals();
                    UnixSignal.WaitAny(terminationSignals);
                }
                else
                {
                    Console.ReadKey();
                }
            }
        }

        private static bool IsRunningOnMono()
        {
            return Type.GetType("Mono.Runtime") != null;
        }

        private static UnixSignal[] GetUnixTerminationSignals()
        {
            return new[]
            {
                new UnixSignal(Signum.SIGINT),
                new UnixSignal(Signum.SIGTERM),
                new UnixSignal(Signum.SIGQUIT),
                new UnixSignal(Signum.SIGHUP)
            };
        }
    }
}