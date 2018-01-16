#pragma once
#include <complex.h>
#include <opencv2/core/mat.hpp>

struct Line
{
	Line(cv::Point start, cv::Point end) : start(start), end(end)
	{
		length = sqrt(pow(end.x - start.x, 2) + pow(end.y - start.y, 2));
		center = cv::Point((start.x + end.x) / 2, (start.y + end.y) / 2);
	}

	cv::Point start;
	cv::Point end;
	cv::Point center;
	int length;
};

