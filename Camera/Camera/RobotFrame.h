#pragma once
#include <opencv2/core/mat.hpp>
#include "Position.h"

class RobotFrame
{
public:
	RobotFrame(const cv::Mat& frame, const Position& position);
	~RobotFrame() = default;

	// TODO: Provide copy and move operations handling

	// This is a read only class 
	cv::Mat GetFrame() const;
	Position GetPosition() const;
	void Identify(bool value);

private:
	cv::Mat _frame;
	Position _position;
};

