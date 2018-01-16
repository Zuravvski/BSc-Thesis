#pragma once
#include "IMovementTracking.h"
#include "IFrameObserver.h"
#include "IIdentifier.h"

namespace Zuravvski
{
	class FrameProcessor : public IFrameObserver
	{
	public:
		explicit FrameProcessor(std::unique_ptr<IMovementTracking> movementTracking, std::unique_ptr<IIdentifier> identifier);
		void GetNotified(const cv::Mat& currentFrame) override;
		~FrameProcessor();

	private:
		std::unique_ptr<IMovementTracking> _movementTracking;
		std::unique_ptr<IIdentifier> _identifier;
	};
}
