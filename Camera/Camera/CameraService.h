#pragma once
#include <opencv2/videoio.hpp>

class CameraService
{
public:
	CameraService();
	~CameraService();

	bool IsRunning() const;
	bool IsWindowed() const;
	void SetWindowed(bool windowed);
	cv::Mat GetCurrentFrame() const;

private:
	cv::VideoCapture _video;
	cv::Mat _currentFrame;
	bool _windowed;

	void Capture();
	void Initialize();
	void ReleaseVideo();
};

