#include "FrameObservable.h"

namespace Zuravvski
{
	void FrameObservable::Subscribe(const std::shared_ptr<IFrameObserver>& observer)
	{
		_observers.emplace_back(observer);
	}

	void FrameObservable::Unsubscribe(const std::shared_ptr<IFrameObserver>& observer)
	{
		//_observers.erase(std::find(_observers.begin(), _observers.end(), [&observer](auto currentObserver) { currentObserver == observer; }));
	}

	void FrameObservable::NotifyObservers(const cv::Mat& currentFrame)
	{
		for (auto& observer : _observers)
		{
			observer->GetNotified(currentFrame);
		}
	}
}