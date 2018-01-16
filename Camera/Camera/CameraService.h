#pragma once
#include <opencv2/videoio.hpp>
#include "FrameObservable.h"

namespace Zuravvski
{
	class CameraService : public FrameObservable
	{
	public:
		CameraService();
		~CameraService();

		bool IsRunning() const;
		bool IsWindowed() const;
		void SetWindowed(bool windowed);

	private:
		cv::VideoCapture _video;
		bool _windowed;

		void Capture();
		void Initialize();
		void ReleaseVideo();
	};
}