#pragma once
#include "../sole/sole.hpp"

namespace cv {
	class Mat;
}

namespace Zuravvski
{
	class IFrameObserver
	{
	public:
		virtual ~IFrameObserver() = default;
		virtual void GetNotified(const cv::Mat& currentFrame) = 0;
	};
}