namespace PclTest
{
    public class MyClass
    {
        public void Test()
        {

            TcpListener listner = new TcpListener(new IPEndPoint(PiggyRemoteHost.Me.Ip, 1339));
            listner.Start();
            TcpClient newConnection;
            newConnection = listner.AcceptTcpClient();
        }
    }
}