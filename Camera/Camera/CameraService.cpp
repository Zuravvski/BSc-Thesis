#include "CameraService.h"
#include <iostream>
#include <opencv2/highgui.hpp>

CameraService::CameraService() : _windowed(true)
{
	Initialize();
}


CameraService::~CameraService()
{
	ReleaseVideo();
}

void CameraService::Initialize()
{
	if(IsRunning())
	{
		return;
	}

	try
	{
		bool result = _video.open(0);
		if (result)
		{
			 // Camera settings
		    _video.set(cv::CAP_PROP_FRAME_WIDTH, 2592 / 2);
            _video.set(cv::CAP_PROP_FRAME_HEIGHT, 1944 / 2);
			_video.set(cv::CAP_PROP_FPS, 25);
			Capture();
		}
		else
		{
			std::cerr << "Could not open camera.";
			exit(EXIT_FAILURE);
		}
	}
	catch (...)
	{
		std::cerr << "Camera cannot be initialized." << std::endl;
		exit(EXIT_FAILURE);
	}
}

bool CameraService::IsRunning() const
{
	return _video.isOpened();
}

bool CameraService::IsWindowed() const
{
	return _windowed;
}

void CameraService::SetWindowed(bool windowed)
{
	_windowed = windowed;
}

cv::Mat CameraService::GetCurrentFrame() const
{
	return _currentFrame;
}

void CameraService::Capture()
{
	while (_video.isOpened())
	{
		cv::Mat currentFrame;
		bool isSuccessful = _video.read(currentFrame);
		char keyPressed = cv::waitKey(25);

		if (!isSuccessful || keyPressed == 27)
		{
			ReleaseVideo();
			break;
		}

		// Crop the board
		//_currentFrame = cv::Mat(currentFrame, cv::Rect(272, 2, 964, 964));
		_currentFrame = currentFrame;

		if (_windowed)
		{
			imshow("Camera feed", currentFrame);
		}
	}
}

void CameraService::ReleaseVideo()
{
	cv::destroyAllWindows();
	_video.release();
	exit(0);
}

