#define _CRT_SECURE_NO_WARNINGS
#include <opencv2/opencv.hpp>
#include "opencv2/highgui.hpp"
#include "TriangleLocation.h"
#include "LEDIndentifier.h"
#include "FrameProcessor.h"

int main()
{
	using namespace Zuravvski;

	TriangleLocation location;
	cv::Mat frame = cv::Mat(cv::imread("../photos/konf1_2rzedy.bmp"), cv::Rect(272, 2, 964, 964));

	FrameProcessor frameProcessor(std::make_unique<TriangleLocation>(), std::make_unique<LEDIndentifier>());
	frameProcessor.GetNotified(frame);

	cv::imshow("frame", frame);

	cv::waitKey(0);
}
