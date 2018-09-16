using System.Net.Sockets;

public class MoveRobot : ICommand
{
    public LEDStateE LED { get; set; }
    public double LeftMotor { get; set; }
    public double RightMotor { get; set; }

    public MoveRobot()
    {
        LED = LEDStateE.OFF;
    }

    public void Execute(RobotControl robot, TcpClient client)
    {
        robot.ControlLEDs(LED);
        robot.Move(LeftMotor, RightMotor);
    }
}
