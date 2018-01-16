#pragma once
#include "IFrameObserver.h"
#include <vector>
#include <memory>

namespace Zuravvski
{
	class FrameObservable
	{
	public:
		virtual ~FrameObservable() = default;
		void Subscribe(const std::shared_ptr<IFrameObserver>& observer);
		void Unsubscribe(const std::shared_ptr<IFrameObserver>& observer);
		void NotifyObservers(const cv::Mat& currentFrame);

	private:
		std::vector<std::shared_ptr<IFrameObserver>> _observers;
	};
}