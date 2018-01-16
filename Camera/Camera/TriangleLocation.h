#pragma once
#include <opencv2/core/mat.hpp>
#include "IMovementTracking.h"

struct TriangleLocation : IMovementTracking
{
	std::vector<RobotFrame> FindRobots(const cv::Mat& frame) override;
};
