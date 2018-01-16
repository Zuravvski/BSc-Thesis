#pragma once
#include <opencv2/core/mat.hpp>
#include "IMovementTracking.h"

namespace Zuravvski
{
	struct TriangleLocation : IMovementTracking
	{
		std::vector<RobotFrame> FindRobots(const cv::Mat& frame) override;
	};
}