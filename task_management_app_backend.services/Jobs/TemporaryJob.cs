using Quartz;
using System;
using System.Threading.Tasks;


namespace task_management_app_backend.services.Jobs
{
    public class TemporaryJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"Temporary job executed at: {DateTime.Now}");
            return Task.CompletedTask;
        }
    }
}
