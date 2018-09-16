#include <opencv2/opencv.hpp>
#include "opencv2/highgui.hpp"
#include "TriangleLocation.h"
#include "LEDIndentifier.h"
#include "FrameProcessor.h"
#include "CameraService.h"

int main()
{
	using namespace Zuravvski;

	cv::Mat frame = cv::Mat(cv::imread("../photos/konf3_3rzedy.bmp"), cv::Rect(272, 2, 964, 964));

	//CameraService camera;
	auto frameProcessor = std::make_shared<FrameProcessor>(std::make_unique<TriangleLocation>(), 
														   std::make_unique<LEDIndentifier>());
	//camera.Subscribe(frameProcessor);
	frameProcessor->GetNotified(frame);

	cv::imshow("frame", frame);
	cv::waitKey(0);
}
