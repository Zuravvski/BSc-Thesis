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

		using ObserverList = std::vector<std::weak_ptr<IFrameObserver>>;

	private:
		ObserverList _observers;
	};
}