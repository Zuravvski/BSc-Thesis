#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <opencv2/opencv.hpp>
#include "opencv2/highgui.hpp"
#include "TriangleLocation.h"
#include "LEDIndentifier.h"

int main()
{
	TriangleLocation location;
	cv::Mat frame = cv::Mat(cv::imread("../photos/konf2_2rzedy.bmp"), cv::Rect(272, 2, 964, 964));

	//cv::imshow("Frame", frame);

	auto robots = location.FindRobots(frame);
	LEDIndentifier identifier;
	identifier.Identify(robots);

	for(auto& robot : robots)
	{
		std::cout << robot.GetPosition() << std::endl;
	}

	cv::waitKey(0);
}