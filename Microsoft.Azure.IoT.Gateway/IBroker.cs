namespace Microsoft.Azure.IoT.Gateway
{
    public interface IBroker
    {
        void Publish(Message message);
    }
}