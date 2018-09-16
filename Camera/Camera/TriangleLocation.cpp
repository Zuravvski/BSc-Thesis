#include "TriangleLocation.h"
#include "Line.h"
#include <opencv2/photo.hpp>
#include <opencv2/highgui.hpp>

namespace Zuravvski
{
	std::vector<RobotFrame> TriangleLocation::FindRobots(const cv::Mat& frame)
	{
		using namespace cv;

		std::vector<RobotFrame> robots;
		Mat brightenedFrame;
		frame.convertTo(brightenedFrame, -1, 1.5, 40);

		if (brightenedFrame.data)
		{
			Mat gray;
			cvtColor(brightenedFrame, gray, CV_BGR2GRAY);
			threshold(gray, gray, 128, 255, CV_THRESH_BINARY);
			std::vector<std::vector<Point>> contours;
			findContours(gray, contours, CV_RETR_TREE, CV_CHAIN_APPROX_SIMPLE);

			for (auto& contour : contours)
			{
				std::vector<Point> approxTriangle;
				approxPolyDP(contour, approxTriangle, arcLength(Mat(contour), true)*0.06, true);
				double area = contourArea(approxTriangle, false);

				if (approxTriangle.size() == 3 && area > 100)
				{
					std::vector<Line> lines = { Line(approxTriangle[0], approxTriangle[1]),
												Line(approxTriangle[1], approxTriangle[2]),
												Line(approxTriangle[2], approxTriangle[0]) };

					Line base = lines[distance(lines.begin(), max_element(lines.begin(), lines.end(),
						[](Line l1, Line l2) { return l1.length < l2.length; }))];

					Point topVertice = approxTriangle[distance(approxTriangle.begin(),
						std::find_if(approxTriangle.begin(), approxTriangle.end(),
							[&base](Point point) { return base.start != point && base.end != point; }))];

					auto angle = static_cast<int>(atan2(topVertice.y - base.center.y, topVertice.x - base.center.x) * 180 / CV_PI);
					angle = angle < 0 ? 360 + angle : angle;

					const Point rectStart(base.center.x - 40 < 0 ? 0 : base.center.x - 40, base.center.y - 40 < 0 ? 0 : base.center.y - 40);
					Mat cropped(frame, Rect(rectStart.x, rectStart.y, 80, 80));
					robots.emplace_back(RobotFrame(cropped, Position(base.center.x, base.center.y, angle)));

					circle(frame, topVertice, 3, Scalar(0, 255, 0), 3);
					line(frame, base.start, base.end, Scalar(255, 0, 0), 4);
					putText(frame, std::to_string(angle), base.center, FONT_HERSHEY_COMPLEX, 0.8, Scalar(0, 0, 255), 2);
				}
			}
			namedWindow("src", WINDOW_KEEPRATIO);
			imshow("src", frame);
		}
		return robots;
	}
}
