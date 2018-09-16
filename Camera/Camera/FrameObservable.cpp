#include "FrameObservable.h"

namespace Zuravvski
{
	void FrameObservable::Subscribe(const std::shared_ptr<IFrameObserver>& observer)
	{
		_observers.emplace_back(observer);
	}

	void FrameObservable::Unsubscribe(const std::shared_ptr<IFrameObserver>& observer)
	{
		for(auto it = _observers.begin(); it != _observers.end(); ++it)
		{
			if(auto sp = it->lock())
			{
				if(sp == observer)
				{
					_observers.erase(it);
					break;
				}
			}
		}
	}

	void FrameObservable::NotifyObservers(const cv::Mat& currentFrame)
	{
		for (auto& observer : _observers)
		{
			if(auto ptr = observer.lock())
			{
				ptr->GetNotified(currentFrame);
			}
		}
	}
}
