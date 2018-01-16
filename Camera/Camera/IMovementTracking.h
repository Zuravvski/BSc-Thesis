#pragma once
#include "RobotFrame.h"

namespace Zuravvski
{
	struct IMovementTracking
	{
		virtual ~IMovementTracking() = default;
		virtual std::vector<RobotFrame> FindRobots(const cv::Mat& frame) = 0;
	};
}