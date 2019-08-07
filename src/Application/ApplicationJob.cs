using AnotherApplication;

namespace Application
{
    public class ApplicationJob
    {
        private readonly AnotherJob anotherJob;

        public ApplicationJob(AnotherJob anotherJob)
        {
            this.anotherJob = anotherJob;
        }

        public void ReceiveMessages()
        {
            anotherJob.ReceiveMessages();
        }
    }
}
