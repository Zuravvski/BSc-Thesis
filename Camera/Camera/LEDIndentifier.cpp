#include "LEDIndentifier.h"
#include <opencv2/imgproc.hpp>

bool FilterAndFind(const std::vector<std::vector<cv::Point>>& contours);

void LEDIndentifier::Identify(std::vector<RobotFrame>& robots)
{
	using namespace cv;

	const auto RED_MIN = Scalar(0, 200, 0);
	const auto RED_MAX = Scalar(19, 255, 255);
	const auto GREEN_MIN = Scalar(40, 156, 20);
	const auto GREEN_MAX = Scalar(85, 256, 226);

	for(auto& robot : robots)
	{
		if (!robot.GetFrame().data)
		{
			return;
		}

		Mat HSV, filteredRed, filteredGreen;
		cvtColor(robot.GetFrame(), HSV, COLOR_BGR2HSV);
		inRange(HSV, RED_MIN, RED_MAX, filteredRed);
		inRange(HSV, GREEN_MIN, GREEN_MAX, filteredGreen);

		std::vector<std::vector<Point>> contoursOfRed, contoursOfGreen;
		findContours(filteredRed, contoursOfRed, CV_RETR_TREE, CV_CHAIN_APPROX_SIMPLE);
		findContours(filteredGreen, contoursOfGreen, CV_RETR_TREE, CV_CHAIN_APPROX_SIMPLE);

		bool identified = FilterAndFind(contoursOfRed) ^ FilterAndFind(contoursOfGreen);
		robot.Identify(identified);
	}
}

bool FilterAndFind(const std::vector<std::vector<cv::Point>>& contours)
{
	std::vector<cv::Point> approxRect;

	for (auto& contour : contours)
	{
		approxPolyDP(contour, approxRect, cv::arcLength(cv::Mat(contour), true)*0.05, true);
		int area = contourArea(approxRect);
		if (area > 0)
		{
			return true;
		}
	}
	return false;
}
