using System.Net.Sockets;

public interface ICommand
{
    void Execute(RobotControl robot, TcpClient client);
}
