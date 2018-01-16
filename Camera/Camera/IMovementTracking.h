#pragma once
#include <opencv2/core/mat.hpp>
#include "RobotFrame.h"

struct IMovementTracking
{
	virtual ~IMovementTracking() = default;
	virtual std::vector<RobotFrame> FindRobots(const cv::Mat& frame) = 0;
};
