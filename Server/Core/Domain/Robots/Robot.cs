using System;
using Core.Domain.Robots.Enums;
using Core.Domain.Users;

namespace Core.Domain.Robots
{
    /// <summary>
    /// Represents Pololu3PI robot
    /// </summary>
    public class Robot
    {
        public uint ID { get; }
        public ERobotLED LEDS { get; protected set; }
        public int LeftMotorSpeed { get; protected set; }
        public int RightMotorSpeed { get; protected set; }
        public Position Position { get; protected set; }
        public uint Battery { get; protected set; }
        public User BoundUser { get; protected set; }

        public Robot(uint id)
        {
            ID = id;
        }

        public void BindUser(User user)
        {
            if (BoundUser != user)
            {
                BoundUser = user;
            }
        }

        public void UnbindUser(User user)
        {
            if (BoundUser == user)
            {
                user = null;
            }
        }
    }
}
